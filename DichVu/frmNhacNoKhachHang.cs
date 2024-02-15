using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
//using ReportMisc.DichVu;
using System.Data.Linq.SqlClient;

namespace DichVu
{
    public partial class frmNhacNoKhachHang : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        MasterDataContext db;
        DateTime now;
        public frmNhacNoKhachHang()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
            now = db.GetSystemDate();
        }
        int MaKH;
        private bool IsCaNhan
        {
            get
            {
                return tabKhachHang.SelectedTabPageIndex == 0;
            }
            set
            {
                tabKhachHang.SelectedTabPageIndex = value ? 0 : 1;
            }
        }

        private void frmNhacNoKhachHang_Load(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKBC.Items.Add(str);
            }
            itemKBC.EditValue = objKBC.Source[7];
            SetDate(7);
            LoadData();

            wait.Close();
            wait.Dispose();
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            DateTime now = db.GetSystemDate();
            var TuNgay = (DateTime)itemTuNgay.EditValue;
            var DenNgay = (DateTime)itemDenNgay.EditValue;


            var khTM = db.dvtmThanhToanThangMays
                .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(TuNgay, p.ThangThanhToan.Value) >= 0 & SqlMethods.DateDiffDay(p.ThangThanhToan.Value, DenNgay) >= 0)
                .Select(p => p.dvtmTheThangMay.MaKH).ToList();
            var khHD = db.thueCongNos
                .Where(p => p.ConNo > 0 & SqlMethods.DateDiffDay(TuNgay, p.ChuKyMin.Value) >= 0 & SqlMethods.DateDiffDay(p.ChuKyMin.Value, DenNgay) >= 0)
                .Select(p => p.thueHopDong.MaKH).ToList();
            var khNuoc = db.dvdnNuocs
                .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(TuNgay, p.NgayNhap.Value) >= 0 & SqlMethods.DateDiffDay(p.NgayNhap.Value, DenNgay) >= 0)
                .Select(p => p.MaKH).ToList();
            var khDien = db.dvdnDiens
                .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(TuNgay, p.NgayNhap.Value) >= 0 & SqlMethods.DateDiffDay(p.NgayNhap.Value, DenNgay) >= 0)
                .Select(p => p.MaKH).ToList();
            var PhiQLDaThu = db.PhiQuanLies.Where(p => SqlMethods.DateDiffDay(TuNgay, p.ThangThanhToan.Value) >= 0 & SqlMethods.DateDiffDay(p.ThangThanhToan.Value, DenNgay) >= 0).Select(p => p.MaMB).ToList();
            var KHChuaThuPhiQL = db.mbMatBangs.Where(p => p.MaKH != null & !PhiQLDaThu.Contains(p.MaMB)).Select(p => p.MaKH).ToList();

            if (IsCaNhan)
            {
                gcCaNhan.DataSource = db.tnKhachHangs
                .Where(p => p.IsCaNhan == IsCaNhan)
                .Select(p => new
                {
                    p.MaKH,
                    p.HoKH,
                    p.TenKH,
                    GioiTinh = p.GioiTinh.Value ? "Nam" : "Nữ",
                    p.NgaySinh,
                    p.CMND,
                    p.DienThoaiKH,
                    p.DCLL,
                    p.DCTT,
                    TenKV = p.MaKV != null ? p.tnKhuVuc.TenKV : "",
                });
            }
            else
            {
                gcDoanhNghiep.DataSource = db.tnKhachHangs.Where(p => khTM.Contains(p.MaKH)
                | khHD.Contains(p.MaKH)
                | khNuoc.Contains(p.MaKH)
                | khDien.Contains(p.MaKH))
                .Where(p => p.IsCaNhan == IsCaNhan)
                .Select(p => new
                {
                    p.MaKH,
                    p.CtyTenVT,
                    p.CtyTen,
                    p.CtyNoiDKKD,
                    CtyDiaChi = p.DCLL,
                    CtyDienThoai =p.DienThoaiKH,
                    p.CtyFax,
                    p.CtyNguoiDD,
                    p.CtyChucVuDD,
                    TenKV = p.MaKV != null ? p.tnKhuVuc.TenKV : "",
                    p.CtyMaSoThue
                });
            }
            
        }

        private void tabKhachHang_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadData();
        }

        private void frmNhacNoKhachHang_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = splitContainerControl1.Height / 2;
        }

        private void grvCaNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvCaNhan.FocusedRowHandle < 0) return;
            MaKH = (int)grvCaNhan.GetFocusedRowCellValue("MaKH");  
            LoadDien();
            LoadGas();
            LoadNuoc();
            LoadDVK();
            LoadThueHopDong();
            LoadThangMay();
            LoadTheXe();
            LoadHoptac();
            LoadPhiQL();
            LoadPhiVS();
        }

        private void LoadPhiVS()
        {
            var souce = db.mbMatBangs.Where(p => p.MaKH != null & p.MaKH == MaKH)
                    .Select(p => new
                    {
                        TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                        p.MaMB,
                        p.MaSoMB,
                        PhiQL = getPhiQL(p.MaMB),
                        TrangThai = DaThanhToanPQL(p.MaMB),
                        p.DienGiai,
                        db.mbLoaiMatBangs.SingleOrDefault(p1 => p1.MaLMB == p.MaLMB).TenLMB,
                        p.MaKH
                    }).ToList();
            gcPhiVS.DataSource = souce.Where(p => p.PhiQL > 0 & !p.TrangThai).ToList();
        }

        private void LoadGas()
        {
        }

        private void grvDoanhNghiep_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDoanhNghiep.FocusedRowHandle < 0) return;
            MaKH = (int)grvDoanhNghiep.GetFocusedRowCellValue("MaKH");  
            LoadDien();
            LoadGas();
            LoadNuoc();
            LoadDVK();
            LoadThueHopDong();
            LoadThangMay();
            LoadTheXe();
            LoadHoptac();
            LoadPhiQL();
        }

        private void LoadPhiQL()
        {
            var souce = db.mbMatBangs.Where(p => p.MaKH != null & p.MaKH == MaKH)
                    .Select(p => new
                    {
                        TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                        p.MaMB,
                        p.MaSoMB,
                        PhiQL = getPhiQL(p.MaMB),
                        TrangThai = DaThanhToanPQL(p.MaMB),
                        p.DienGiai,
                        db.mbLoaiMatBangs.SingleOrDefault(p1 => p1.MaLMB == p.MaLMB).TenLMB,
                        p.MaKH
                    }).ToList();
            gcMatBang.DataSource = souce.Where(p => p.PhiQL > 0 & !p.TrangThai).ToList();
        }
        
        private bool DaThanhToanPQL(int MaMB)
        {
            bool result = false;
            var PhiQLDaThu = db.PhiQuanLies.Where(p => p.mbMatBang.MaMB == MaMB & p.ThangThanhToan.Value.Month == now.Month & p.ThangThanhToan.Value.Year == now.Year);
            if (PhiQLDaThu.Count() <= 0)
                result = false;
            else
                result = true;

            return result;
        }

        private decimal getPhiQL(int MaMB)
        {
            var pql =  0;
            var pqlhdt = db.thueHopDongs.Where(p => p.MaMB == MaMB).Select(p => p.PhiQL).FirstOrDefault() ?? 0;

            return pql + pqlhdt;
        }
        private void LoadHoptac()
        {
            gcCongNo.DataSource = db.hthtCongNos
                             .Where(p => p.NgayThanhToan <= db.GetSystemDate()
                                 & p.ConLai > 0 & p.hdhtHopDong.MaKH == MaKH)
                             .Select(p => new
                             {
                                 p.MaCongNo,
                                 MaHD = p.MaHD,
                                 SoHopDong = p.hdhtHopDong.SoHD,
                                 DaThanhToan = p.SoTien,
                                 ConNo = p.ConLai,
                                 NgayThanhToan = p.NgayThanhToan,
                                 PhaiThanhToan = p.ConLai + p.SoTien,
                                 NhaCungCap = p.hdhtHopDong.MaKH.HasValue ? (p.hdhtHopDong.tnKhachHang.IsCaNhan.HasValue ? (p.hdhtHopDong.tnKhachHang.IsCaNhan.Value ? p.hdhtHopDong.tnKhachHang.HoKH + " " + p.hdhtHopDong.tnKhachHang.TenKH : p.hdhtHopDong.tnKhachHang.CtyTen) : "") : "",
                                 p.dvTrangThaiNhacNo.TenTT,
                                 p.dvTrangThaiNhacNo.MauNen
                             });
        }

        private void LoadThueHopDong()
        {
            gcThueHopDong.DataSource = db.thueCongNos
                    .Where(p => p.ConNo > 0 & p.ChuKyMin <= db.GetSystemDate() & p.thueHopDong.MaKH == MaKH)
                    .OrderBy(p => p.ChuKyMin)
                    .Select(p => new
                    {
                        p.MaCN,
                        p.thueHopDong.SoHD,
                        KhachHang = p.thueHopDong.tnKhachHang.IsCaNhan.Value ? string.Format("{0} {1}", p.thueHopDong.tnKhachHang.HoKH, p.thueHopDong.tnKhachHang.TenKH) : p.thueHopDong.tnKhachHang.CtyTen,
                        DiaChi = p.thueHopDong.tnKhachHang.DCLL,
                        p.ConNo,
                        ThangThanhToan = string.Format("{0}/{1}", p.ChuKyMax.Value.Month, p.ChuKyMax.Value.Year),
                        p.dvTrangThaiNhacNo.TenTT,
                        p.dvTrangThaiNhacNo.MauNen
                    });
        }

        private void LoadDVK()
        {
            gcdvk.DataSource = db.dvkDichVuThanhToans
                    .Where(p => p.DaTT == false & p.ThangThanhToan <= db.GetSystemDate())
                    .OrderBy(p => p.ThangThanhToan)
                    .Select(p => new
                    {
                        p.ThanhToanID,
                        ThangThanhToan = string.Format("{0}/{1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year),
                        p.dvTrangThaiNhacNo.TenTT,
                        p.dvTrangThaiNhacNo.MauNen
                    });
        }

        private void LoadThangMay()
        {
            gcThangMay.DataSource = db.dvtmThanhToanThangMays
                    .Where(p => p.DaTT == false & p.ThangThanhToan <= db.GetSystemDate() & p.dvtmTheThangMay.MaKH == MaKH)
                    .OrderBy(p => p.ThangThanhToan)
                    .Select(p => new
                    {
                        p.ThanhToanID,
                        p.dvtmTheThangMay.SoThe,
                        p.dvtmTheThangMay.ChuThe,
                        KhachHang = p.dvtmTheThangMay.tnKhachHang.IsCaNhan.Value ? string.Format("{0} {1}", p.dvtmTheThangMay.tnKhachHang.HoKH, p.dvtmTheThangMay.tnKhachHang.TenKH) : p.dvtmTheThangMay.tnKhachHang.CtyTen,
                        DiaChi = p.dvtmTheThangMay.tnKhachHang.DCLL,
                        p.dvtmTheThangMay.PhiLamThe,
                        ThangThanhToan = string.Format("{0}/{1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year),
                        p.dvTrangThaiNhacNo.TenTT,
                        p.dvTrangThaiNhacNo.MauNen
                    });
        }

        private void LoadTheXe()
        {
        }

        private void LoadNuoc()
        {
            gcNuoc.DataSource = db.dvdnNuocs
                    .Where(p => p.DaTT == false & p.NgayNhap <= db.GetSystemDate() & p.MaKH == MaKH)
                    .OrderBy(p => p.NgayNhap)
                    .Select(p => new
                    {
                        p.ID,
                        p.mbMatBang.MaSoMB,
                        KhachHang = p.tnKhachHang.IsCaNhan.Value ? string.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                        DiaChi = p.tnKhachHang.DCLL,
                        p.SoTieuThu,
                        p.TongTien,
                        p.NgayNhap,
                        ThangThanhToan = string.Format("{0}/{1}", p.NgayNhap.Value.Month - 1, p.NgayNhap.Value.Year),
                        p.dvTrangThaiNhacNo.TenTT,
                        p.dvTrangThaiNhacNo.MauNen
                    });
        }

        private void LoadDien()
        {
            gcDien.DataSource = db.dvdnDiens
                    .Where(p => p.DaTT == false & p.NgayNhap <= db.GetSystemDate() & p.MaKH == MaKH)
                    .OrderBy(p => p.NgayNhap)
                    .Select(p => new
                    {
                        p.ID,
                        p.mbMatBang.MaSoMB,
                        KhachHang = p.tnKhachHang.IsCaNhan.Value ? string.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                        DiaChi = p.tnKhachHang.DCLL,
                        p.SoTieuThu,
                        p.TienDien,
                        p.NgayNhap,
                        ThangThanhToan = string.Format("{0}/{1}", p.NgayNhap.Value.Month - 1, p.NgayNhap.Value.Year),
                        p.dvTrangThaiNhacNo.TenTT,
                        p.dvTrangThaiNhacNo.MauNen
                    });
        }

        private void grvDien_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvDien.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
            }
            catch { }
        }

        private void grvNuoc_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvNuoc.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
            }
            catch { }
        }

        private void grvTheXe_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvTheXe.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
            }
            catch { }
        }

        private void grvThangMay_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvThangMay.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
            }
            catch { }
        }

        private void grvdvk_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvdvk.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
            }
            catch { }
        }

        private void grvThueHopDong_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvThueHopDong.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
            }
            catch { }
        }

        private void btnTrangThai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                frmEditTTNNDV frm = new frmEditTTNNDV();
                switch (tabNhacNo.SelectedTabPage.Name)
                {
                    case "tpDien":
                        if (grvDien.FocusedRowHandle < 0)
                        {
                            DialogBox.Alert("Vui lòng chọn mẫu tin");  
                            return;
                        }
                        var dien = db.dvdnDiens.Single(p => p.ID == (int)grvDien.GetFocusedRowCellValue("ID"));
                        //frm.LoaiDichVu = EnumLoaiDichVu.DichVuDien;
                        frm.objdien = dien;
                        break;
                    case "tpNuoc":
                        if (grvNuoc.FocusedRowHandle < 0)
                        {
                            DialogBox.Alert("Vui lòng chọn mẫu tin");  
                            return;
                        }
                        var nuoc = db.dvdnNuocs.Single(p => p.ID == (int)grvNuoc.GetFocusedRowCellValue("ID"));
                        //frm.LoaiDichVu = EnumLoaiDichVu.DichVuNuoc;
                        frm.objnuoc = nuoc;
                        break;
                    case "tpThangMay":
                        if (grvThangMay.FocusedRowHandle < 0)
                        {
                            DialogBox.Alert("Vui lòng chọn mẫu tin");  
                            return;
                        }
                        var tm = db.dvtmThanhToanThangMays.Single(p => p.ThanhToanID == (int)grvThangMay.GetFocusedRowCellValue("ThanhToanID"));
                        //frm.LoaiDichVu = EnumLoaiDichVu.DichVuThangMay;
                        frm.objtm = tm;
                        break;
                    case "tpdvk":
                        if (grvdvk.FocusedRowHandle < 0)
                        {
                            DialogBox.Alert("Vui lòng chọn mẫu tin");  
                            return;
                        }
                        var dvk = db.dvkDichVuThanhToans.Single(p => p.ThanhToanID == (int)grvdvk.GetFocusedRowCellValue("ThanhToanID"));
                       // frm.LoaiDichVu = EnumLoaiDichVu.DichVuKhac;
                        frm.objdvk = dvk;
                        break;
                    case "tpHopDongThue":
                        if (grvThueHopDong.FocusedRowHandle < 0)
                        {
                            DialogBox.Alert("Vui lòng chọn mẫu tin");  
                            return;
                        }
                        var thue = db.thueCongNos.Single(p => p.MaCN == (int)grvThueHopDong.GetFocusedRowCellValue("MaCN"));
                        //frm.LoaiDichVu = EnumLoaiDichVu.HopDongThue;
                        frm.objthue = thue;
                        break;
                    case "tpHopTac":
                        if (grvCongNo.FocusedRowHandle < 0)
                        {
                            DialogBox.Alert("Vui lòng chọn mẫu tin");  
                            return;
                        }
                        var ht = db.hthtCongNos.Single(p => p.MaCongNo == (int)grvCongNo.GetFocusedRowCellValue("MaCongNo"));
                        //frm.LoaiDichVu = EnumLoaiDichVu.DichVuHoptac;
                        frm.objht = ht;
                        break;
                    default:
                        break;
                }
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    switch (tabNhacNo.SelectedTabPage.Name)
                    {
                        case "tpDien":
                            LoadDien();
                            break;
                        case "tpNuoc":
                            LoadNuoc();
                            break;
                        case "tpTheXe":
                            LoadTheXe();
                            break;
                        case "tpThangMay":
                            LoadThangMay();
                            break;
                        case "tpdvk":
                            LoadDVK();
                            break;
                        case "tpHopDongThue":
                            LoadThueHopDong();
                            break;
                        case "tpHopTac":
                            LoadHoptac();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void grvDien_DoubleClick(object sender, EventArgs e)
        {
            if (btnTrangThai.Enabled == false) return;
            if (grvDien.FocusedRowHandle < 0) return;
            btnTrangThai_ItemClick(null, null);
        }

        private void grvNuoc_DoubleClick(object sender, EventArgs e)
        {
            if (btnTrangThai.Enabled == false) return;
            if (grvNuoc.FocusedRowHandle < 0) return;
            btnTrangThai_ItemClick(null, null);
        }

        private void grvTheXe_DoubleClick(object sender, EventArgs e)
        {
            if (btnTrangThai.Enabled == false) return;
            if (grvTheXe.FocusedRowHandle < 0) return;
            btnTrangThai_ItemClick(null, null);
        }

        private void grvThangMay_DoubleClick(object sender, EventArgs e)
        {
            if (btnTrangThai.Enabled == false) return;
            if (grvThangMay.FocusedRowHandle < 0) return;
            btnTrangThai_ItemClick(null, null);
        }

        private void grvdvk_DoubleClick(object sender, EventArgs e)
        {
            if (btnTrangThai.Enabled == false) return;
            if (grvdvk.FocusedRowHandle < 0) return;
            btnTrangThai_ItemClick(null, null);
        }

        private void grvThueHopDong_DoubleClick(object sender, EventArgs e)
        {
            if (btnTrangThai.Enabled == false) return;
            if (grvThueHopDong.FocusedRowHandle < 0) return;
            btnTrangThai_ItemClick(null, null);
        }

        private void grvCongNo_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvCongNo.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
            }
            catch { }
        }

        private void grvCongNo_DoubleClick(object sender, EventArgs e)
        {
            if (grvCongNo.FocusedRowHandle < 0) return;
            btnTrangThai_ItemClick(null, null);
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}