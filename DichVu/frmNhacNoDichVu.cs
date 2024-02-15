using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.IO;
//using ReportMisc.DichVu;
using System.Data.Linq.SqlClient;

namespace DichVu
{
    public partial class frmNhacNoDichVu : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        MasterDataContext db;
        DateTime now;
        public frmNhacNoDichVu()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
            now = db.GetSystemDate();
        }

        private void frmNhacNoDichVu_Load(object sender, EventArgs e)
        {
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);
            LoadData();
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
            var wait = DialogBox.WaitingForm();
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;

            LoadDien(tuNgay, denNgay);
            LoadGas(tuNgay, denNgay);
            LoadNuoc(tuNgay, denNgay);
            LoadTheXe(tuNgay, denNgay);
            LoadThangMay(tuNgay, denNgay);
            LoadDVK(tuNgay, denNgay);
            LoadThueHopDong(tuNgay, denNgay);
            LoadHopTac();
            LoadPhiQL(tuNgay,denNgay);
            LoadPhiVS(tuNgay, denNgay);
            wait.Close();
            wait.Dispose();
        }

        private void LoadPhiVS(DateTime tuNgay, DateTime denNgay)
        {
            var dattpql = db.PhiVeSinhs.Where(p => SqlMethods.DateDiffDay(tuNgay, p.ThangThanhToan.Value) >= 0 & SqlMethods.DateDiffDay(p.ThangThanhToan.Value, denNgay) >= 0)
                    .Select(p => p.MaMB).ToList();
            var souce = db.mbMatBangs.Where(p => p.MaKH != null & dattpql.Contains(p.MaMB))
                    .Select(p => new
                    {
                        TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                        p.MaMB,
                        p.MaSoMB,
                        PhiQL = getPhiQL(p.MaMB),
                        TrangThai = DaThanhToanPQL(p.MaMB),
                        p.DienGiai,
                        db.mbLoaiMatBangs.SingleOrDefault(p1=>p1.MaLMB==p.MaLMB).TenLMB,
                        p.MaKH
                    }).ToList();
            gcPhiVS.DataSource = souce.Where(p => p.PhiQL > 0 & !p.TrangThai).ToList();
        }

        private void LoadGas(DateTime tuNgay, DateTime denNgay)
        {
        }

        private void LoadPhiQL(DateTime TuNgay, DateTime DenNgay)
        {
            var dattpql = db.PhiQuanLies.Where(p => SqlMethods.DateDiffDay(TuNgay, p.ThangThanhToan.Value) >= 0 & SqlMethods.DateDiffDay(p.ThangThanhToan.Value, DenNgay) >= 0)
                    .Select(p => p.MaMB).ToList();
            var souce = db.mbMatBangs.Where(p => p.MaKH != null & dattpql.Contains(p.MaMB))
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
            var pql = 0;
            //var pqlhdt = db.thueHopDongs.Where(p => p.MaMB == MaMB).Select(p => p.PhiQL).FirstOrDefault() ?? 0;

            return pql;
        }

        private void LoadHopTac()
        {
            gcCongNo.DataSource = db.hthtCongNos
                             .Where(p => p.NgayThanhToan <= db.GetSystemDate()
                                 & p.ConLai > 0)
                             .OrderBy(p => p.NgayThanhToan)
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

        private void LoadThueHopDong(DateTime TuNgay, DateTime DenNgay)
        {
            gcThueHopDong.DataSource = db.thueCongNos
                    .Where(p => p.ConNo > 0 & SqlMethods.DateDiffDay(TuNgay, p.ChuKyMin.Value) >= 0 & SqlMethods.DateDiffDay(p.ChuKyMin.Value, DenNgay) >= 0)
                    .OrderBy(p => p.ChuKyMax)
                    .Select(p => new
                    {
                        p.thueHopDong.MaHD,
                        p.MaCN,
                        p.thueHopDong.SoHD,
                        KhachHang = p.thueHopDong.tnKhachHang.IsCaNhan.Value ? string.Format("{0} {1}", p.thueHopDong.tnKhachHang.HoKH, p.thueHopDong.tnKhachHang.TenKH) : p.thueHopDong.tnKhachHang.CtyTen,
                        DiaChi = p.thueHopDong.tnKhachHang.DCLL ,
                        p.ConNo,
                        ThangThanhToan = string.Format("{0}/{1}", p.ChuKyMax.Value.Month, p.ChuKyMax.Value.Year),
                        p.dvTrangThaiNhacNo.TenTT,
                        p.dvTrangThaiNhacNo.MauNen
                    });
        }

        private void LoadDVK(DateTime TuNgay, DateTime DenNgay)
        {
            gcdvk.DataSource = db.dvkDichVuThanhToans
                    .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(TuNgay, p.ThangThanhToan.Value) >= 0 & SqlMethods.DateDiffDay(p.ThangThanhToan.Value, DenNgay) >= 0)
                    .OrderBy(p => p.ThangThanhToan)
                    .Select(p => new
                    {
                        p.ThanhToanID,
                        ThangThanhToan = string.Format("{0}/{1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year),
                        p.dvTrangThaiNhacNo.TenTT,
                        p.dvTrangThaiNhacNo.MauNen
                    });
        }

        private void LoadThangMay(DateTime TuNgay, DateTime DenNgay)
        {
            gcThangMay.DataSource = db.dvtmThanhToanThangMays
                    .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(TuNgay, p.ThangThanhToan.Value) >= 0 & SqlMethods.DateDiffDay(p.ThangThanhToan.Value, DenNgay) >= 0)
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

        private void LoadTheXe(DateTime TuNgay, DateTime DenNgay)
        {
        }

        private void LoadNuoc(DateTime TuNgay, DateTime DenNgay)
        {
            gcNuoc.DataSource = db.dvdnNuocs
                    .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(TuNgay, p.NgayNhap.Value) >= 0 & SqlMethods.DateDiffDay(p.NgayNhap.Value, DenNgay) >= 0)
                    .OrderBy(p => p.NgayNhap)
                    .Select(p => new
                    {
                        p.ID,
                        MaSoMB = p.MaMB.HasValue ? p.mbMatBang.MaSoMB : "",
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

        private void LoadDien(DateTime TuNgay, DateTime DenNgay)
        {
            gcDien.DataSource = db.dvdnDiens
                    .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(TuNgay, p.NgayNhap.Value) >= 0 & SqlMethods.DateDiffDay(p.NgayNhap.Value, DenNgay) >= 0)
                    .OrderBy(p => p.NgayNhap)
                    .Select(p => new
                    {
                        p.ID,
                        MaSoMB = p.MaMB.HasValue ? p.mbMatBang.MaSoMB : "",
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
                        //frm.objdien = dien;
                        break;
                    case "tpNuoc":
                        if (grvNuoc.FocusedRowHandle < 0)
                        {
                            DialogBox.Alert("Vui lòng chọn mẫu tin");  
                            return;
                        }
                        var nuoc = db.dvdnNuocs.Single(p => p.ID == (int)grvNuoc.GetFocusedRowCellValue("ID"));
                        //frm.LoaiDichVu = EnumLoaiDichVu.DichVuNuoc;
                        //frm.objnuoc = nuoc;
                        break;
                    case "tpTheXe":
                        if (grvTheXe.FocusedRowHandle < 0)
                        {
                            DialogBox.Alert("Vui lòng chọn mẫu tin");  
                            return;
                        }
                        break;
                    case "tpThangMay":
                        if (grvThangMay.FocusedRowHandle < 0)
                        {
                            DialogBox.Alert("Vui lòng chọn mẫu tin");  
                            return;
                        }
                        var tm = db.dvtmThanhToanThangMays.Single(p => p.ThanhToanID == (int)grvThangMay.GetFocusedRowCellValue("ThanhToanID"));
                        //frm.LoaiDichVu = EnumLoaiDichVu.DichVuThangMay;
                        //frm.objtm = tm;
                        break;
                    case "tpdvk":
                        if (grvdvk.FocusedRowHandle < 0)
                        {
                            DialogBox.Alert("Vui lòng chọn mẫu tin");  
                            return;
                        }
                        var dvk = db.dvkDichVuThanhToans.Single(p => p.ThanhToanID == (int)grvdvk.GetFocusedRowCellValue("ThanhToanID"));
                        //frm.LoaiDichVu = EnumLoaiDichVu.DichVuKhac;
                        //frm.objdvk = dvk;
                        break;
                    case "tpHopDongThue":
                        if (grvThueHopDong.FocusedRowHandle < 0)
                        {
                            DialogBox.Alert("Vui lòng chọn mẫu tin");  
                            return;
                        }
                        var thue = db.thueCongNos.Single(p => p.MaCN == (int)grvThueHopDong.GetFocusedRowCellValue("MaCN"));
                        //frm.LoaiDichVu = EnumLoaiDichVu.HopDongThue;
                        //frm.objthue = thue;
                        break;
                    case "tpHopTac":
                        if (grvCongNo.FocusedRowHandle < 0)
                        {
                            DialogBox.Alert("Vui lòng chọn mẫu tin");  
                            return;
                        }
                        var ht = db.hthtCongNos.Single(p => p.MaCongNo == (int)grvCongNo.GetFocusedRowCellValue("MaCongNo"));
                        //frm.LoaiDichVu = EnumLoaiDichVu.DichVuHoptac;
                        //frm.objht = ht;
                        break;

                    default:
                        break;
                }
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void frmNhacNoDichVu_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = (splitContainerControl1.Height / 4) * 3;
        }

        private void grvDien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDien.FocusedRowHandle < 0) return;

            var kh = db.dvdnDiens.Single(p => p.ID == (int)grvDien.GetFocusedRowCellValue("ID"));
            vgcKhachHang.DataSource = db.tnKhachHangs.Where(p => p.MaKH == kh.MaKH)
                .Select(p => new
                {
                    HoTen = p.IsCaNhan.Value ? string.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                    DiaChi = p.DCLL,
                    DienThoai =p.DienThoaiKH ,
                    Email = p.EmailKH,
                    GioiTinh = p.GioiTinh.Value ? "Nam" : "Nữ",
                    p.NgaySinh,
                    p.CMND
                });
        }

        private void grvNuoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvNuoc.FocusedRowHandle < 0) return;
            var kh = db.dvdnNuocs.Single(p => p.ID == (int)grvNuoc.GetFocusedRowCellValue("ID"));
            vgcKhachHang.DataSource = db.tnKhachHangs.Where(p => p.MaKH == kh.MaKH)
                .Select(p => new
                {
                    HoTen = p.IsCaNhan.Value ? string.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                    DiaChi = p.DCLL,
                    DienThoai = p.DienThoaiKH,
                    Email = p.EmailKH,
                    GioiTinh = p.GioiTinh.Value ? "Nam" : "Nữ",
                    p.NgaySinh,
                    p.CMND
                });
        }

        private void grvTheXe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
        }

        private void grvThangMay_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvThangMay.FocusedRowHandle < 0) return;
            var kh = db.dvtmThanhToanThangMays.Single(p => p.ThanhToanID == (int)grvThangMay.GetFocusedRowCellValue("ThanhToanID"));
            vgcKhachHang.DataSource = db.tnKhachHangs.Where(p => p.MaKH == kh.dvtmTheThangMay.MaKH)
                .Select(p => new
                {
                    HoTen = p.IsCaNhan.Value ? string.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                    DiaChi = p.DCLL,
                    DienThoai = p.DienThoaiKH,
                    Email = p.EmailKH,
                    GioiTinh = p.GioiTinh.Value ? "Nam" : "Nữ",
                    p.NgaySinh,
                    p.CMND
                });
        }

        private void grvdvk_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvdvk.FocusedRowHandle < 0) return;
            var kh = db.dvkDichVuThanhToans.Single(p => p.ThanhToanID == (int)grvdvk.GetFocusedRowCellValue("ThanhToanID"));
            vgcKhachHang.DataSource = db.tnKhachHangs
                .Select(p => new
                {
                    HoTen = p.IsCaNhan.Value ? string.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                    DiaChi = p.DCLL,
                    DienThoai = p.DienThoaiKH,
                    Email = p.EmailKH,
                    GioiTinh = p.GioiTinh.Value ? "Nam" : "Nữ",
                    p.NgaySinh,
                    p.CMND
                });
        }

        private void grvThueHopDong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvThueHopDong.FocusedRowHandle < 0) return;

            var kh = db.thueCongNos.Single(p => p.MaCN == (int)grvThueHopDong.GetFocusedRowCellValue("MaCN"));
            vgcKhachHang.DataSource = db.tnKhachHangs.Where(p => p.MaKH == kh.thueHopDong.MaKH)
                .Select(p => new
                {
                    HoTen = p.IsCaNhan.Value ? string.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                    DiaChi = p.DCLL,
                    DienThoai = p.DienThoaiKH,
                    Email = p.EmailKH,
                    GioiTinh = p.GioiTinh.Value ? "Nam" : "Nữ",
                    p.NgaySinh,
                    p.CMND
                });
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

        private void tabNhacNo_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            switch (tabNhacNo.SelectedTabPage.Name)
            {
                case "tpDien":
                    LoadDien(tuNgay, denNgay);
                    break;
                case "tpNuoc":
                    LoadNuoc(tuNgay, denNgay);
                    break;
                case "tpTheXe":
                    LoadTheXe(tuNgay, denNgay);
                    break;
                case "tpThangMay":
                    LoadThangMay(tuNgay, denNgay);
                    break;
                case "tpdvk":
                    LoadDVK(tuNgay, denNgay);
                    break;
                case "tpHopDongThue":
                    LoadThueHopDong(tuNgay, denNgay);
                    break;
                case "tpHopTac":
                    LoadHopTac();
                    break;
                default:
                    break;
            }
        }

        private void grvCongNo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvCongNo.FocusedRowHandle < 0) return;
            var kh = db.hthtCongNos.Single(p => p.MaCongNo == (int)grvCongNo.GetFocusedRowCellValue("MaCongNo"));
            vgcKhachHang.DataSource = db.tnKhachHangs.Where(p => p.MaKH == kh.hdhtHopDong.MaKH)
                .Select(p => new
                {
                    HoTen = p.IsCaNhan.Value ? string.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                    DiaChi = p.DCLL,
                    DienThoai = p.DienThoaiKH,
                    Email = p.EmailKH,
                    GioiTinh = p.GioiTinh.Value ? "Nam" : "Nữ",
                    p.NgaySinh,
                    p.CMND
                });
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

        private void btnEmailNhacNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var lst = db.thueCongNos.Where(p => p.ConNo > 0 & p.ChuKyMin <= db.GetSystemDate()).OrderBy(p => p.ChuKyMax).Select(p => p.MaHD).Distinct();

            if (grvCongNo.FocusedRowHandle < 0 | grvDien.FocusedRowHandle < 0 | grvNuoc.FocusedRowHandle < 0 | grvCongNo.FocusedRowHandle < 0
                | grvTheXe.FocusedRowHandle < 0 | grvThangMay.FocusedRowHandle < 0 | grvdvk.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn công nợ cần gửi mail");  
                return;
            }

            object idchothue = grvThueHopDong.GetFocusedRowCellValue("MaHD") ?? -1;
            object idthuengoai = grvCongNo.GetFocusedRowCellValue("MaHD") ?? -1;
            object iddvk = grvdvk.GetFocusedRowCellValue("MaHD") ?? -1;
            
            MemoryStream stream = new MemoryStream();
            //ReportMisc.DichVu.ChoThue.ReportTemplate.rptCongNo rpt = new ReportMisc.DichVu.ChoThue.ReportTemplate.rptCongNo((int)idchothue);
            //rpt.ExportToPdf(stream);
            stream.Position = 0;

            BinaryReader reader = new BinaryReader(stream);
            byte[] file = reader.ReadBytes((int)stream.Length);

            SendMailNhacNo mnn = new SendMailNhacNo()
            {
                FileDinhKem = file,
                MaKH = 1,
                MaNV = objnhanvien.MaNV,
                NoiDung = "",
                ThoiGianGui = db.GetSystemDate(),
                TieuDe = "",
                TrangThai = 3 //dang cho gui   
            };
            reader.Close();
            stream.Close();



            #region download 
            byte[] FileBytesdl = null;
            FileStream streamdl;
            SaveFileDialog open = new SaveFileDialog();
            open.Filter = "Tất cả|*.*";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    FileBytesdl = (byte[])file.ToArray();
                    streamdl = new FileStream(open.FileName + ".pdf", FileMode.Create);
                    streamdl.Write(FileBytesdl, 0, FileBytesdl.Length);
                    streamdl.Close();
                    wait.Close();
                    wait.Dispose();
                    DialogBox.Alert("Tải file thành công");  
                }
                catch
                {
                    wait.Close();
                    wait.Dispose();
                    DialogBox.Alert("Không tải file này về được, vui lòng thử lại sau!");  
                    this.Close();
                }
            }

            #endregion
        }

        private void grvMatBang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvMatBang.FocusedRowHandle < 0) return;
            try
            {
                int kh = (int)grvMatBang.GetFocusedRowCellValue("MaKH");  
                vgcKhachHang.DataSource = db.tnKhachHangs.Where(p => p.MaKH == kh)
                    .Select(p => new
                    {
                        HoTen = p.IsCaNhan.Value ? string.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                        DiaChi = p.DCLL,
                        DienThoai =p.DienThoaiKH,
                        Email = p.EmailKH,
                        GioiTinh = p.GioiTinh.Value ? "Nam" : "Nữ",
                        p.NgaySinh,
                        p.CMND
                    });
            }
            catch { }
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvGas_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvGas.FocusedRowHandle < 0) return;

            var kh = db.dvGas.Single(p => p.ID == (int)grvGas.GetFocusedRowCellValue("ID"));
            vgcKhachHang.DataSource = db.tnKhachHangs.Where(p => p.MaKH == kh.MaKH)
                .Select(p => new
                {
                    HoTen = p.IsCaNhan.Value ? string.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                    DiaChi = p.DCLL,
                    DienThoai = p.DienThoaiKH,
                    Email = p.EmailKH,
                    GioiTinh = p.GioiTinh.Value ? "Nam" : "Nữ",
                    p.NgaySinh,
                    p.CMND
                });
        }

        private void grvPhiVS_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvPhiVS.FocusedRowHandle < 0) return;
            try
            {
                int kh = (int)grvPhiVS.GetFocusedRowCellValue("MaKH");  
                vgcKhachHang.DataSource = db.tnKhachHangs.Where(p => p.MaKH == kh)
                    .Select(p => new
                    {
                        HoTen = p.IsCaNhan.Value ? string.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen,
                        DiaChi = p.DCLL,
                        DienThoai =p.DienThoaiKH,
                        Email = p.EmailKH,
                        GioiTinh = p.GioiTinh.Value ? "Nam" : "Nữ",
                        p.NgaySinh,
                        p.CMND
                    });
            }
            catch { }
        }

    }
}