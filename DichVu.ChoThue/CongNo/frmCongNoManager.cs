using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;

namespace DichVu.ChoThue.CongNo
{
    public partial class frmCongNoManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime now;
        bool first = true;
        public frmCongNoManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmCongNoManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);

            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.DataSource = list;
            if (list.Count > 0)
                itemToaNha.EditValue = list[0].MaTN;

            now = db.GetSystemDate();
            itemDenNgay.EditValue = now.AddDays(30);
            itemTuNgay.EditValue = now.AddDays(-30);
            LoadData();

            first = false;
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

                if (itemDenNgay.EditValue == null || itemTuNgay.EditValue == null)
                {
                    gcCongNo.DataSource = null;
                    return;
                }
                else
                {
                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        gcCongNo.DataSource = db.thueCongNos
                            .Where(p => SqlMethods.DateDiffDay((DateTime)itemDenNgay.EditValue, p.ChuKyMin) <= 0 && SqlMethods.DateDiffDay(p.ChuKyMin, (DateTime)itemTuNgay.EditValue) <= 0
                                && p.thueHopDong.MaTN == maTN)//& !p.DaTT.GetValueOrDefault()
                            // & SqlMethods.DateDiffDay(p.thueHopDong.NgayBG, (DateTime)itemDenNgay.EditValue) >= 0)
                            .Select(p => new
                            {
                                p.MaCN,
                                MaHD = p.MaHD,
                                SoHopDong = p.thueHopDong.SoHD,
                                DaThanhToan = p.DaThanhToan,
                                ConNo = p.ConNo - p.DaThanhToan,
                                NoTruoc = p.NoTruoc ?? 0,
                                TongNo = p.ConNo.GetValueOrDefault() + p.NoTruoc.GetValueOrDefault() - p.DaThanhToan.GetValueOrDefault(),
                                p.NgayThanhToan,
                                p.ChuKyMin,
                                TuNgay = p.ChuKyMin,
                                DenNgay = p.ChuKyMax,
                                PhaiThanhToan = p.ConNo,
                                p.thueHopDong.mbMatBang.MaMB,
                                p.thueHopDong.mbMatBang.MaSoMB,
                                LoaiTien = p.thueHopDong.tnTyGia.TenVT,
                                HoVaTenKH = (bool)p.thueHopDong.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.thueHopDong.tnKhachHang.HoKH, p.thueHopDong.tnKhachHang.TenKH) : p.thueHopDong.tnKhachHang.CtyTen,
                                SoDienThoaiKH = p.thueHopDong.tnKhachHang.DienThoaiKH,
                                DiaChiKH = p.thueHopDong.tnKhachHang.DCLL,
                                EmailKH = p.thueHopDong.tnKhachHang.EmailKH
                            });
                    }
                    else
                    {
                        var GetNhomOfNV = db.pqNhomNhanViens.Where(p => p.IsTruongNhom.Value & p.MaNV == objnhanvien.MaNV).Select(p => p.GroupID).ToList();
                        if (GetNhomOfNV.Count > 0)
                        {
                            var GetListNV = db.pqNhomNhanViens.Where(p => GetNhomOfNV.Contains(p.GroupID)).Select(p => p.MaNV).ToList();

                            gcCongNo.DataSource = db.thueCongNos
                                .Where(p => SqlMethods.DateDiffDay((DateTime)itemDenNgay.EditValue, p.ChuKyMin) <= 0 && SqlMethods.DateDiffDay(p.ChuKyMin, (DateTime)itemTuNgay.EditValue) <= 0
                                     & p.thueHopDong.MaTN == maTN //& !p.DaTT.GetValueOrDefault()
                                    //  & SqlMethods.DateDiffDay(p.thueHopDong.NgayBG, (DateTime)itemDenNgay.EditValue) >= 0
                                    & GetListNV.Contains(p.thueHopDong.tnNhanVien.MaNV))
                                .Select(p => new
                                {
                                    MaHD = p.MaHD,
                                    SoHopDong = p.thueHopDong.SoHD,
                                    DaThanhToan = p.DaThanhToan,
                                    p.thueHopDong.mbMatBang.MaMB,
                                    p.thueHopDong.mbMatBang.MaSoMB,
                                    ConNo = p.ConNo ?? 0 - p.DaThanhToan ?? 0,
                                    NoTruoc = p.NoTruoc ?? 0,
                                    TongNo = p.ConNo.GetValueOrDefault() + p.NoTruoc.GetValueOrDefault() - p.DaThanhToan.GetValueOrDefault(),
                                    p.NgayThanhToan,
                                    p.ChuKyMin,
                                    TuNgay = p.ChuKyMin,
                                    DenNgay = p.ChuKyMax,
                                    PhaiThanhToan = p.TongNo,
                                    LoaiTien = p.thueHopDong.tnTyGia.TenVT,
                                    HoVaTenKH = (bool)p.thueHopDong.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.thueHopDong.tnKhachHang.HoKH, p.thueHopDong.tnKhachHang.TenKH) : p.thueHopDong.tnKhachHang.CtyTen,
                                    SoDienThoaiKH = p.thueHopDong.tnKhachHang.DienThoaiKH,
                                    DiaChiKH = p.thueHopDong.tnKhachHang.DCLL,
                                    EmailKH = p.thueHopDong.tnKhachHang.EmailKH
                                });
                        }
                        else
                        {
                            gcCongNo.DataSource = db.thueCongNos
                                .Where(p => SqlMethods.DateDiffDay((DateTime)itemDenNgay.EditValue, p.ChuKyMin) <= 0 && SqlMethods.DateDiffDay(p.ChuKyMin, (DateTime)itemTuNgay.EditValue) <= 0
                                    && p.thueHopDong.MaTN == maTN
                                    & SqlMethods.DateDiffDay(p.thueHopDong.NgayBG, (DateTime)itemDenNgay.EditValue) >= 0
                                    & p.thueHopDong.MaNV == objnhanvien.MaNV)
                                .Select(p => new
                                {
                                    MaHD = p.MaHD,
                                    SoHopDong = p.thueHopDong.SoHD,
                                    DaThanhToan = p.DaThanhToan,
                                    p.thueHopDong.mbMatBang.MaMB,
                                    p.thueHopDong.mbMatBang.MaSoMB,
                                    ConNo = p.ConNo - p.DaThanhToan,
                                    p.NoTruoc,
                                    p.NgayThanhToan,
                                    TongNo = p.ConNo.GetValueOrDefault() + p.NoTruoc.GetValueOrDefault() - p.DaThanhToan.GetValueOrDefault(),
                                    TuNgay = p.ChuKyMin,
                                    DenNgay = p.ChuKyMax,
                                    p.ChuKyMin,
                                    PhaiThanhToan = p.TongNo,
                                    LoaiTien = p.thueHopDong.tnTyGia.TenVT,
                                    HoVaTenKH = (bool)p.thueHopDong.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.thueHopDong.tnKhachHang.HoKH, p.thueHopDong.tnKhachHang.TenKH) : p.thueHopDong.tnKhachHang.CtyTen,
                                    SoDienThoaiKH = p.thueHopDong.tnKhachHang.DienThoaiKH,
                                    DiaChiKH = p.thueHopDong.tnKhachHang.DCLL,
                                    EmailKH = p.thueHopDong.tnKhachHang.EmailKH
                                });
                        }
                    }
                }
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCongNo.FocusedRowHandle < 0)
            {
                Library.DialogBox.Error("Vui lòng chọn [Hợp đồng], xin cảm ơn.");
                return;
            }

            int MaHD = (int)grvCongNo.GetFocusedRowCellValue("MaHD");
            if (db.thueHopDongs.Single(p => p.MaHD == MaHD).DaHuy ?? false)
            {
                Library.DialogBox.Error("Hợp đồng này đã bị hủy!!!");
                return;
            }
            using (var frm = new frmPaid())
            {
                frm.MaMB = (int)grvCongNo.GetFocusedRowCellValue("MaMB");
                frm.MaSoMB = grvCongNo.GetFocusedRowCellValue("MaSoMB").ToString();
                frm.soTien = (decimal?)grvCongNo.GetFocusedRowCellValue("TongNo");
                frm.objnhanvien = objnhanvien;
                frm.objhd = db.thueHopDongs.Single(hd => hd.MaHD == MaHD);
                frm.ShowDialog();
            }
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCongNo.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn công nợ để chỉnh sửa");
                return;                                                                                                                                                                                                                                                                                                                         
            }
            var frm = new frmEdit() { MaCN = (int?)grvCongNo.GetFocusedRowCellValue("MaCN"), objNV = objnhanvien };
            
            frm.ShowDialog();
        }
    }
}