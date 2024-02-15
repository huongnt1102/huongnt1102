using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmLichTruc_Import : XtraForm
    {
        public byte MaTn { get; set; }
        public bool IsSave { get; set; }

        public frmLichTruc_Import()
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
                    System.Collections.Generic.List<LichTruc> list = Library.Import.ExcelAuto.ConvertDataTable<LichTruc>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;
                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new LichTruc
                    //{
                    //    //MaSoPC = _["Mã số phân công"].ToString().Trim(),
                    //    MaSoNV = _["Mã số nhân viên"].ToString().Trim(),
                    //    //TenKN = _["Tên tháp"].ToString().Trim(),
                    //    KyHieu = _["Ký hiệu Loại Ca"].ToString().Trim(),
                    //    Ngay = _["Ngày"].Cast<DateTime>()
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
                var obj = (List<LichTruc>) gc.DataSource;
                var ltError = new List<LichTruc>();
                foreach (var n in obj)
                {
                    try
                    {
                        #region kiểm tra

                        //var pc = db.tbl_PhanCongs.FirstOrDefault(_ =>
                        //    _.MaSoPC.ToLower() == n.MaSoPC.ToLower());
                        //if (pc == null)
                        //{
                        //    n.Error = "Mã số phân công không tồn tại!";
                        //    ltError.Add(n);
                        //    continue;
                        //}

                        var nv = db.tnNhanViens.FirstOrDefault(_ => _.MaSoNV.ToLower() == n.MaSoNV.ToLower());
                        if (nv == null)
                        {
                            n.Error = "Mã số nhân viên không tồn tại.";
                            ltError.Add(n);
                            continue;
                        }

                        //var kn = db.mbKhoiNhas.FirstOrDefault(_ => _.TenKN.ToLower() == n.TenKN.ToLower());
                        //if (kn == null)
                        //{
                        //    n.Error = "Tháp không tồn tại";
                        //    ltError.Add(n);
                        //    continue;
                        //}

                        var ca = db.tbl_PhanCong_PhanLoaiCas.FirstOrDefault(_ =>
                            _.KyHieu.ToLower() == n.KyHieu.ToLower());
                        if (ca == null)
                        {
                            n.Error = "Ca không tồn tại";
                            ltError.Add(n);
                            continue;
                        }
                        #endregion

                        db = new MasterDataContext();
                        var kt = db.tbl_PhanCong_NhanVienChiTiets.FirstOrDefault(_ =>  _.MaNV==nv.MaNV &_.Ngay.Value.Year==n.Ngay.Value.Year & _.Ngay.Value.Month==n.Ngay.Value.Month & _.Ngay.Value.Day==n.Ngay.Value.Day);
                        if (kt != null)
                        {
                            if (kt.IDPhanLoaiCa != null)
                            {
                                n.Error = "Lịch trực này đã có ca: " + kt.tbl_PhanCong_PhanLoaiCa.KyHieu +
                                          ", vui lòng vào đổi ca.";
                                ltError.Add(n);
                                continue;
                            }
                            else
                            {
                                kt.IDPhanLoaiCa = ca.ID;
                                kt.NgaySua = DateTime.Now;
                                kt.NguoiSua = Common.User.MaNV;
                            }
                        }
                        else
                        {
                            n.Error = "Nhân viên " + n.MaSoNV + " không có phân công trong ngày này.";
                            ltError.Add(n);
                            continue;
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

    public class LichTruc
    {
        //public string MaSoPC { get; set; }
        public string MaSoNV { get; set; }
        //public string TenKN { get; set; }
        public string KyHieu { get; set; }
        public string Error { get; set; }

        public DateTime? Ngay { get; set; }
    }
}