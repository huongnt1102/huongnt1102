using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmThietBi_Import : XtraForm
    {
        public short MaTn { get; set; }
        public bool IsSave { get; set; }

        public frmThietBi_Import()
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
                    System.Collections.Generic.List<DanhMucThietBi> list = Library.Import.ExcelAuto.ConvertDataTable<DanhMucThietBi>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;

                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new DanhMucThietBi
                    //{
                    //    KyHieu = _["Mã công cụ thiết bị"].ToString().Trim(),
                    //    Name = _["Tên công cụ thiết bị"].ToString().Trim(),
                    //    SoLuong = _["Số lượng"].Cast<decimal>(),
                    //    TenTinhTrang = _["Tình trạng"].ToString().Trim()
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
                var objCaTruc = (List<DanhMucThietBi>) gc.DataSource;
                var ltError = new List<DanhMucThietBi>();
                foreach (var n in objCaTruc)
                {
                    try
                    {
                        #region kiểm tra

                        var tt = db.tbl_TinhTrangTaiSans.FirstOrDefault(_ =>
                            _.TenTinhTrang.ToLower() == n.TenTinhTrang.ToLower());
                        if (tt == null)
                        {
                            n.Error = "Tình trạng không có trong hệ thống!";
                            ltError.Add(n);
                            continue;
                        }
                        #endregion

                        var kt = db.tbl_PhanCong_ThietBis.FirstOrDefault(_ => _.KyHieu.ToLower() == n.KyHieu.ToLower() );
                        if (kt != null)
                        {
                            kt.NgaySua = DateTime.Now;
                            kt.NguoiSua = Common.User.MaNV;
                            kt.Name = n.Name;
                            kt.SoLuong = n.SoLuong;
                        }
                        else
                        {
                            var objCt = new tbl_PhanCong_ThietBi();
                            objCt.KyHieu = n.KyHieu;
                            objCt.IDTinhTrangTaiSan = tt.ID;
                            objCt.Name = n.Name;
                            objCt.SoLuong = n.SoLuong;
                            objCt.NguoiTao = Common.User.MaNV;
                            objCt.NgayTao = DateTime.Now;
                            db.tbl_PhanCong_ThietBis.InsertOnSubmit(objCt);
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

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

    }

    public class DanhMucThietBi
    {
        public decimal? SoLuong { get; set; }

        public string KyHieu { get; set; }
        public string Name { get; set; }
        public string TenTinhTrang { get; set; }

        public string Error { get; set; }
    }
}