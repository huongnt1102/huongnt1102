using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmMuaHang_Import : XtraForm
    {
        public byte MaTn { get; set; }
        public bool IsSave { get; set; }

        public frmMuaHang_Import()
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
                    System.Collections.Generic.List<MuaHang> list = Library.Import.ExcelAuto.ConvertDataTable<MuaHang>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;
                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new MuaHang
                    //{
                    //    MaPhieu = _["Mã phiếu"].ToString().Trim(),
                    //    DienGiai = _["Diễn giải"].ToString().Trim(),

                    //    MaSoNvMua = _["Mã số người mua"].ToString().Trim(),
                    //    TenNhaCungCap=_["Nhà cung cấp"].ToString().Trim(),
                    //    ChungTuGoc=_["Chứng từ gốc"].ToString().Trim(),
                    //    KyHieu = _["Mã số vật tư"].ToString().Trim(),
                    //    MaPhieuDeXuat=_["Mã phiếu đề xuất"].ToString().Trim(),
                    //    NgayPhieu = _["Ngày phiếu"].Cast<DateTime>(),
                    //    SoLuong = _["Số lượng"].Cast<decimal>(),
                    //    DonGia=_["Đơn giá"].Cast<decimal>(),
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
                var obj = (List<MuaHang>) gc.DataSource;
                var ltError = new List<MuaHang>();
                foreach (var n in obj)
                {
                    try
                    {
                        db = new MasterDataContext();
                        tbl_VatTu_DeXuat_ChiTiet deXuatChiTiet = null;
                        tbl_VatTu vatTu;
                        tbl_VatTu_DeXuat deXuat=null;

                        #region Kiểm tra

                        // kiểm tra người mua có tồn tại
                        var nguoiMua = db.tnNhanViens.FirstOrDefault(_ => _.MaSoNV.ToLower() == n.MaSoNvMua.ToLower());
                        if (nguoiMua == null)
                        {
                            n.Error = "Nhân viên mua " + n.MaSoNvMua + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        // kiểm tra nhà cung cấp
                        var nhaCungCap = db.tbl_NhaCungCapTaiSans.FirstOrDefault(_ =>
                            _.TenNhaCungCap.ToLower() == n.TenNhaCungCap.ToLower());
                        if (nhaCungCap == null)
                        {
                            n.Error = "Nhà cung cấp " + n.TenNhaCungCap + " không tồn tại trong hệ thống.";
                            ltError.Add(n);
                            continue;
                        }

                        // kiểm tra phiếu đề xuất, trường hợp có phiếu đề xuất
                        if (n.MaPhieuDeXuat != "")
                        {
                            deXuat = db.tbl_VatTu_DeXuats.FirstOrDefault(_ =>
                                _.MaPhieu.ToLower() == n.MaPhieuDeXuat.ToLower());
                            if (deXuat == null)
                            {
                                n.Error = "Phiếu đề xuất " + n.MaPhieuDeXuat + " không tồn tại.";
                                ltError.Add(n);
                                continue;
                            }
                            else
                            {
                                // kiểm tra vật tư có tồn tại trong hệ thống không
                                var ktVatTu =
                                    db.tbl_VatTus.FirstOrDefault(_ => _.KyHieu.ToLower() == n.KyHieu.ToLower());
                                if (ktVatTu == null)
                                {
                                    n.Error = "Vật tư " + n.KyHieu + " không có trong hệ thống";
                                    ltError.Add(n);
                                    continue;
                                }
                                else
                                {
                                    // kiểm tra vật tư có tồn tại trong phiếu đề xuất k
                                    deXuatChiTiet = db.tbl_VatTu_DeXuat_ChiTiets.FirstOrDefault(_ =>
                                        _.DeXuatID == deXuat.ID && _.VatTuID == ktVatTu.ID);
                                    if (deXuatChiTiet == null)
                                    {
                                        n.Error = "Phiếu đề xuất " + n.MaPhieuDeXuat + " không có vật tư " + n.KyHieu;
                                        ltError.Add(n);
                                        continue;
                                    }
                                    else
                                    {
                                        vatTu = ktVatTu;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // kiểm tra vật tư có tồn tại, trường hợp không có phiếu đề xuất
                            var ktVatTu = db.tbl_VatTus.FirstOrDefault(_ => _.KyHieu.ToLower() == n.KyHieu.ToLower());
                            if (ktVatTu == null)
                            {
                                n.Error = "Vật tư " + n.KyHieu + " không có trong hệ thống";
                                ltError.Add(n);
                                continue;
                            }
                            else
                            {
                                vatTu = ktVatTu;
                            }
                        }

                        #endregion

                        tbl_VatTu_MuaHang mh;

                        // kiểm tra đã tồn tại phiếu mua hàng chưa
                        mh = db.tbl_VatTu_MuaHangs.FirstOrDefault(_ =>
                            _.MaPhieu.ToLower() == n.MaPhieu.ToLower() & _.MaTN == MaTn);
                        if (mh != null)
                        {
                            // tồn tại phiếu mua hàng
                            mh.MaPhieu = n.MaPhieu;
                            mh.NgayPhieu = n.NgayPhieu;
                            mh.DienGiai = n.DienGiai;
                            mh.NguoiMua = nguoiMua.MaNV;
                            mh.MaNCC = nhaCungCap.ID;
                            mh.ChungTuGoc = n.ChungTuGoc;
                            mh.NgaySua = DateTime.Now;
                            mh.NguoiSua = Common.User.MaNV;
                        }
                        else
                        {
                            mh = new tbl_VatTu_MuaHang();
                            mh.MaPhieu = n.MaPhieu;
                            mh.NgayPhieu = n.NgayPhieu;
                            mh.TrangThaiTraTienID = 1;
                            mh.DienGiai = n.DienGiai;
                            mh.NguoiMua = nguoiMua.MaNV;
                            mh.MaNCC = nhaCungCap.ID;
                            mh.ChungTuGoc = n.ChungTuGoc;
                            mh.TongTienDaTra = 0;
                            mh.TrangThaiNhapKhoID = 1;
                            mh.NguoiNhap = Common.User.MaNV;
                            mh.NgayNhap = DateTime.Now;
                            mh.MaTN = MaTn;

                            db.tbl_VatTu_MuaHangs.InsertOnSubmit(mh);
                        }

                        var ct = new tbl_VatTu_MuaHang_ChiTiet();
                        ct.VatTuID = vatTu.ID;
                        ct.SoLuong = n.SoLuong;
                        ct.DonGia = n.DonGia;
                        ct.ThanhTien = ct.SoLuong * ct.DonGia;
                        ct.TenDVT = vatTu.tbl_VatTu_DVT.TenDVT;
                        ct.SoLuongNhapKho = 0;

                        mh.tbl_VatTu_MuaHang_ChiTiets.Add(ct);

                        mh.TongTienPhieu = mh.tbl_VatTu_MuaHang_ChiTiets.Sum(_ => _.ThanhTien);
                        mh.TongTienConLai = mh.TongTienPhieu - mh.TongTienDaTra ?? 0;

                        if (deXuatChiTiet != null)
                        {
                            ct.DeXuatChiTietID = deXuatChiTiet.ID;
                            ct.SoLuongDeXuat = deXuat.tbl_VatTu_DeXuat_ChiTiets.Sum(_=>_.SoLuongDuyet-_.SoLuongMuaHang).GetValueOrDefault();
                            // cập nhật lại số lượng mua hàng trong phiếu đề xuất

                            var soLuongMua = db.tbl_VatTu_MuaHang_ChiTiets
                                .Where(_ => _.DeXuatChiTietID == deXuatChiTiet.ID).Sum(_ => _.SoLuong)
                                .GetValueOrDefault();
                            deXuatChiTiet.SoLuongMuaHang = soLuongMua+ct.SoLuong;

                            // cập nhật trạng thái phiếu đề xuất
                            var kt = deXuat.tbl_VatTu_DeXuat_ChiTiets.Where(
                                _ => (_.SoLuongDuyet - _.SoLuongMuaHang) > 0).ToList();
                            deXuat.TrangThaiID = kt.Count > 0 ? 3 : 4;
                            mh.DeXuatID = deXuat.ID;
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

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

    }

    public class MuaHang 
    {
        public string MaPhieu { get; set; }
        public string DienGiai { get; set; }
        public string MaSoNvMua { get; set; }
        public string TenNhaCungCap { get; set; }
        public string ChungTuGoc { get; set; }
        public string KyHieu { get; set; }
        public string Error { get; set; }
        public string MaPhieuDeXuat { get; set; }

        public DateTime? NgayPhieu { get; set; }

        public decimal? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
    }
}