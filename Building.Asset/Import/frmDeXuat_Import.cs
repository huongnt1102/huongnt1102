using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmDeXuat_Import : XtraForm
    {
        public byte MaTn { get; set; }
        public bool IsSave { get; set; }

        public frmDeXuat_Import()
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
                    System.Collections.Generic.List<DeXuat> list = Library.Import.ExcelAuto.ConvertDataTable<DeXuat>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;
                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new DeXuat
                    //{
                    //    MaPhieu = _["Mã phiếu"].ToString().Trim(),
                    //    DienGiai = _["Diễn giải"].ToString().Trim(),
                    //    MaSoNV = _["Mã số người nhận"].ToString().Trim(),
                    //    KyHieu = _["Mã số vật tư"].ToString().Trim(),
                    //    NgayPhieu = _["Ngày phiếu"].Cast<DateTime>(),
                    //    SoLuongDeXuat = _["Số lượng đề xuất"].Cast<decimal>(),
                    //    MaPhieuDeXuatSuaChua = _["Mã phiếu DXSC"].ToString().Trim()
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
                var obj = (List<DeXuat>) gc.DataSource;
                var ltError = new List<DeXuat>();
                foreach (var n in obj)
                {
                    try
                    {
                        db = new MasterDataContext();

                        #region Kiểm tra

                        var ktNguoiNhan = db.tnNhanViens.FirstOrDefault(_ => _.MaSoNV.ToLower() == n.MaSoNV.ToLower());
                        if (ktNguoiNhan == null)
                        {
                            n.Error = "Người nhận " + n.MaSoNV + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var ktVatTu = db.tbl_VatTus.FirstOrDefault(_ => _.KyHieu.ToLower() == n.KyHieu.ToLower());
                        if (ktVatTu == null)
                        {
                            n.Error = "Vật tư " + n.KyHieu + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var ktMaPhieu =
                            db.tbl_VatTu_DeXuats.FirstOrDefault(_ => _.MaPhieu.ToLower() == n.MaPhieu.ToLower() &_.MaTN == MaTn);
                        if (ktMaPhieu != null)
                        {
                            if (ktMaPhieu.TrangThaiID != 1)
                            {
                                n.Error = "Mã phiếu này đã được duyệt hoặc đang mua hàng, không được phép chỉnh sửa";
                                ltError.Add(n);
                                continue;
                            }
                        }

                        #endregion

                        var o = db.tbl_VatTu_DeXuats.FirstOrDefault(_ => _.MaPhieu.ToLower() == n.MaPhieu.ToLower() & _.MaTN==MaTn);
                        if (o != null)
                        {
                            o.NgaySua = DateTime.Now;
                            o.NguoiSua = Common.User.MaNV;
     
                            o.NgayPhieu = n.NgayPhieu;
                            o.DienGiai = n.DienGiai;
                            o.NguoiNhan = ktNguoiNhan.MaNV;

                            var ct = new tbl_VatTu_DeXuat_ChiTiet
                            {
                                VatTuID = ktVatTu.ID,
                                TenDVT = ktVatTu.tbl_VatTu_DVT.TenDVT,
                                SoLuongDeXuat = n.SoLuongDeXuat,
                                SoLuongDuyet = 0,
                                SoLuongMuaHang = 0,
                                SoLuongNhapKho = 0
                            };

                            if (n.MaPhieuDeXuatSuaChua != "")
                            {
                                var ktDxSc = db.tbl_DeXuatSuaChuas.FirstOrDefault(_ =>
                                    _.SoPhieu.ToLower() == n.MaPhieuDeXuatSuaChua.ToLower());
                                if (ktDxSc != null)
                                {
                                    var ctDxSc = new tbl_VatTu_DeXuat_PhieuSuaChua
                                    {
                                        PhieuDeXuatSuaChuaID = ktDxSc.ID, MaPhieuDeXuatSuaChua = ktDxSc.SoPhieu
                                    };

                                    o.tbl_VatTu_DeXuat_PhieuSuaChuas.Add(ctDxSc);
                                }
                            }

                            o.tbl_VatTu_DeXuat_ChiTiets.Add(ct);
                        }
                        else
                        {
                            var objCt = new tbl_VatTu_DeXuat
                            {
                                MaPhieu = n.MaPhieu,
                                NgayPhieu = n.NgayPhieu,
                                TrangThaiID = 1,
                                NguoiNhan = ktNguoiNhan.MaNV,
                                DienGiai = n.DienGiai,
                                MaTN = MaTn,
                                NguoiNhap = Common.User.MaNV,
                                NgayNhap = DateTime.Now
                            };


                            var ct = new tbl_VatTu_DeXuat_ChiTiet
                            {
                                VatTuID = ktVatTu.ID,
                                TenDVT = ktVatTu.tbl_VatTu_DVT.TenDVT,
                                SoLuongDeXuat = n.SoLuongDeXuat,
                                SoLuongDuyet = 0,
                                SoLuongMuaHang = 0,
                                SoLuongNhapKho = 0
                            };

                            if (n.MaPhieuDeXuatSuaChua != "")
                            {
                                var ktDxSc = db.tbl_DeXuatSuaChuas.FirstOrDefault(_ =>
                                    _.SoPhieu.ToLower() == n.MaPhieuDeXuatSuaChua.ToLower());
                                if (ktDxSc != null)
                                {
                                    var ctDxSc = new tbl_VatTu_DeXuat_PhieuSuaChua
                                    {
                                        PhieuDeXuatSuaChuaID = ktDxSc.ID,
                                        MaPhieuDeXuatSuaChua = ktDxSc.SoPhieu
                                    };

                                    objCt.tbl_VatTu_DeXuat_PhieuSuaChuas.Add(ctDxSc);
                                }
                            }

                            objCt.tbl_VatTu_DeXuat_ChiTiets.Add(ct);

                            db.tbl_VatTu_DeXuats.InsertOnSubmit(objCt);
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

                gc.DataSource = ltError.Count > 0 ? ltError : null;
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

    public class DeXuat
    {
        public string MaPhieu { get; set; }
        public string DienGiai { get; set; }
        public string MaSoNV { get; set; }
        public string KyHieu { get; set; }
        public string Error { get; set; }
        public string MaPhieuDeXuatSuaChua { get; set; }

        public DateTime? NgayPhieu { get; set; }

        public decimal? SoLuongDeXuat { get; set; }
    }
}