using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmNhomTaiSan_Import : XtraForm
    {
        public bool IsSave { get; set; }

        public frmNhomTaiSan_Import()
        {
            InitializeComponent();
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue == null)
                gc.DataSource = null;
            else
                try
                {
                    var excel = new LinqToExcel.ExcelQueryFactory(Tag.ToString());
                    System.Collections.Generic.List<NhomTaiSan> list = Library.Import.ExcelAuto.ConvertDataTable<NhomTaiSan>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;
                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new NhomTaiSan
                    //{
                    //    TenVietTat = _["Mã hệ thống"].ToString().Trim(),
                    //    TenNhomTaiSan = _["Tên hệ thống"].ToString().Trim(),
                    //    DienGiai = _["Diễn giải"].ToString().Trim(),
                    //}).ToList();

                    excel = null;
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            gv.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gc.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var objNhomTaiSan = (List<NhomTaiSan>) gc.DataSource;
                var ltError = new List<NhomTaiSan>();
                foreach (var n in objNhomTaiSan)
                {
                    try
                    {
                        db = new MasterDataContext();
                        var kt = db.tbl_DanhMuc_NhomTaiSans.FirstOrDefault(_ => _.KyHieu.ToLower() == n.TenVietTat.ToLower());
                        if (kt != null)
                        {
                            kt.TenNhomTaiSan = n.TenNhomTaiSan;
                            kt.DienGiai = n.DienGiai;
                            kt.NgaySua = DateTime.Now;
                            kt.NguoiSua = Common.User.MaNV;
                        }
                        else
                        {
                            var objNts = new tbl_DanhMuc_NhomTaiSan();
                            objNts.KyHieu = n.TenVietTat;
                            objNts.TenNhomTaiSan = n.TenNhomTaiSan;
                            objNts.DienGiai = n.DienGiai;
                            objNts.NguoiNhap = Common.User.MaNV;
                            objNts.NgayNhap = DateTime.Now;
                            db.tbl_DanhMuc_NhomTaiSans.InsertOnSubmit(objNts);
                        }

                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        n.Error = ex.Message;
                        ltError.Add(n);
                    }
                }

                IsSave = true;

                // cập nhật lại tất cả các mã khác
                var kt1 = (from _ in db.tbl_NhomTaiSans
                    join dm in db.tbl_DanhMuc_NhomTaiSans on _.IDDanhMucNhomTaiSan equals dm.ID
                    where _.TenVietTat.ToLower() != dm.KyHieu.ToLower()
                    select _).ToList();
                foreach (var i in kt1)
                {
                    var nts = db.tbl_NhomTaiSans.FirstOrDefault(_ => _.ID == i.ID);
                    if (nts != null)
                    {
                        nts.TenVietTat = i.tbl_DanhMuc_NhomTaiSan.KyHieu;
                        nts.TenNhomTaiSan = i.TenNhomTaiSan;
                    }
                }

                db.SubmitChanges();

                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gc.DataSource = ltError;
                }
                else
                {
                    gc.DataSource = null;
                }
            }
            catch
            {
                wait.Close();
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                Close();
            }
            finally
            {
                wait.Dispose();
                db.Dispose();
            }
        }

        private void itemChonFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = @"(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new LinqToExcel.ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (var s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                Tag = file.FileName;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                file.Dispose();
            }
        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

    }

    public class NhomTaiSan
    {
        public string TenVietTat { get; set; }
        public string TenNhomTaiSan { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }
    }
}