using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace LandSoftBuilding.Receivables.Import
{
    public partial class FrmDauKy_Import : XtraForm
    {
        public byte MaTn { get; set; }
        public bool IsSave { get; set; }

        public FrmDauKy_Import()
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
                    System.Collections.Generic.List<CongNoDauKy> list = Library.Import.ExcelAuto.ConvertDataTable<CongNoDauKy>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;
                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new CongNoDauKy
                    //{
                    //    KyHieu = _["Khách hàng"].ToString().Trim(),
                    //    MaSoMB = _["Mặt bằng"].ToString().Trim(),
                    //    DienGiai = _["Diễn giải"].ToString().Trim(),
                    //    TenLDV = _["Dịch vụ"].ToString().Trim(),
                    //    Nam=_["Năm"].Cast<int>(),
                    //    SoTien=_["Số tiền"].Cast<decimal>()
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
                var objCaTruc = (List<CongNoDauKy>) gc.DataSource;
                var ltError = new List<CongNoDauKy>();
                foreach (var n in objCaTruc)
                {
                    try
                    {
                        db = new MasterDataContext();

                        #region Kiểm tra

                        var khachHang = db.tnKhachHangs.FirstOrDefault(_ => _.KyHieu.ToLower() == n.KyHieu.ToLower() & _.MaTN == MaTn);
                        if (khachHang == null)
                        {
                            n.Error = "Khách hàng không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        var matBang = db.mbMatBangs.FirstOrDefault(_ => _.MaSoMB.ToLower() == n.MaSoMB.ToLower() & _.MaTN == MaTn);
                        if (matBang == null)
                        {
                            n.Error = "Mặt bằng không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        var loaiDichVu = db.dvLoaiDichVus.FirstOrDefault(_ => _.TenLDV.ToLower() == n.TenLDV.ToLower());
                        if (loaiDichVu == null)
                        {
                            n.Error = "Loại dịch vụ không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        #endregion

                        dvDauKy dk;
                        dk = db.dvDauKies.FirstOrDefault(_ => _.MaKH == khachHang.MaKH & _.Nam == n.Nam & _.MaMB == matBang.MaMB & _.MaLDV == loaiDichVu.ID);
                        if (dk != null)
                        {
                            dk.SoTien = n.SoTien;
                            dk.DienGiai = n.DienGiai;
                        }
                        else
                        {
                            dk = new dvDauKy();
                            dk.Nam = n.Nam;
                            dk.DienGiai = n.DienGiai;
                            dk.MaKH = khachHang.MaKH;
                            dk.MaLDV = loaiDichVu.ID;
                            dk.MaMB = matBang.MaMB;
                            dk.MaTN = MaTn;
                            dk.SoTien = n.SoTien;
                            db.dvDauKies.InsertOnSubmit(dk);
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

    }

    public class CongNoDauKy
    {
        public int? Nam { get; set; }

        public string KyHieu { get; set; }
        public string MaSoMB { get; set; }
        public string TenLDV { get; set; }
        public string DienGiai { get; set; }

        public decimal? SoTien { get; set; }

        public string Error { get; set; }
    }
}