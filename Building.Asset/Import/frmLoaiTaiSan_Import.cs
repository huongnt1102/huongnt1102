using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmLoaiTaiSan_Import : XtraForm
    {
        public short MaTn { get; set; }
        public bool IsSave { get; set; }

        public frmLoaiTaiSan_Import()
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
                    System.Collections.Generic.List<LoaiTaiSan> list = Library.Import.ExcelAuto.ConvertDataTable<LoaiTaiSan>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;
                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new LoaiTaiSan
                    //{
                    //    TenVietTat = _["Mã tài sản"].ToString().Trim(),
                    //    TenLoaiTaiSan = _["Tên loại tài sản"].ToString().Trim(),
                    //    DienGiai = _["Diễn giải"].ToString().Trim(),
                    //    NgungSuDung = _["Ngưng sử dụng"].Cast<bool>(),
                    //    TenHeThong = _["Tên hệ thống"].ToString().Trim()
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
                var objLoaiTaiSan = (List<LoaiTaiSan>) gc.DataSource;
                var ltError = new List<LoaiTaiSan>();
                foreach (var n in objLoaiTaiSan)
                {
                    try
                    {
                        db = new MasterDataContext();

                        #region Kiểm tra dữ liệu

                        var objNhomTaiSan = db.tbl_NhomTaiSans.FirstOrDefault(_ =>
                            _.MaTN == MaTn & (_.TenVietTat.ToUpper().Equals(n.TenHeThong.ToUpper()) || _.TenNhomTaiSan.ToUpper().Equals(n.TenHeThong.ToUpper())));
                        if (objNhomTaiSan == null)
                        {
                            n.Error = "Hệ thống này không tồn tại";
                            ltError.Add(n);
                            continue;
                        }
                        #endregion

                        var kt = db.tbl_LoaiTaiSans.FirstOrDefault(_ => _.TenVietTat.ToUpper().Equals(n.TenVietTat.ToUpper())&& _.NhomTaiSanID==objNhomTaiSan.ID);
                        if (kt != null)
                        {
                            n.Error = "Mã loại tài sản đã tồn tại";
                            ltError.Add(n);
                            continue;
                        }
                        else
                        {
                            var objLts = new tbl_LoaiTaiSan();
                            objLts.TenVietTat = n.TenVietTat;
                            objLts.TenLoaiTaiSan = n.TenLoaiTaiSan;
                            objLts.DienGiai = n.DienGiai;
                            objLts.NgungSuDung = n.NgungSuDung;
                            objLts.NguoiNhap = Common.User.MaNV;
                            objLts.NgayNhap = DateTime.Now;
                            objLts.NhomTaiSanID = objNhomTaiSan.ID;
                            db.tbl_LoaiTaiSans.InsertOnSubmit(objLts);
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

    public class LoaiTaiSan
    {
        public string TenVietTat { get; set; }
        public string TenLoaiTaiSan { get; set; }
        public string DienGiai { get; set; }
        public string TenHeThong { get; set; }
        public string Error { get; set; }

        public bool? NgungSuDung { get; set; }
    }
}