using System;
using System.Linq;
using System.Data.Linq.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace Library.NoticeCtl
{
    public partial class CongNoCtl : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime now;
        List<int> GetListNV = new List<int>();
        private delegate void LoadDelegate();

        public CongNoCtl()
        {
            InitializeComponent();
            db = new MasterDataContext();
            now = db.GetSystemDate();
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            var GetNhomOfNV = db.pqNhomNhanViens.Where(p => p.IsTruongNhom.Value & p.MaNV == objnhanvien.MaNV).Select(p => p.GroupID).ToList();
            if (GetNhomOfNV.Count > 0)
            {
                GetListNV = db.pqNhomNhanViens.Where(p => GetNhomOfNV.Contains(p.GroupID)).Select(p => p.MaNV).ToList();
            }
            switch (tabNhacNo.SelectedTabPageIndex)
            {
                case 0:
                    LoadDien();
                    break;
                case 1:
                    LoadNuoc();
                    break;
                case 2:
                    LoadTheXe();
                    break;
                case 3:
                    LoadThangMay();
                    break;
                case 4:
                    LoadDVK();
                    break;
                case 5:
                    LoadThueHopDong();
                    break;
                case 6:
                    LoadHopTac();
                    break;
                case 7:
                    LoadPhiQL();
                    break;
                case 8:
                    LoadPhiVS();
                    break;
            }
            wait.Close();
        }
        
        private void CongNoCtl_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadPhiVS()
        {
            var data = db.mbMatBangs.Where(p => p.MaKH != null )
                .Select(p => new
                {
                    TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                    p.MaMB,
                    p.MaSoMB,
                    PhiVS = getPhiVS(p.MaMB),
                    TrangThai = DaThanhToanPVS(p.MaMB),
                    p.DienGiai,
                    db.mbLoaiMatBangs.SingleOrDefault(p1=>p1.MaLMB==p.MaLMB).TenLMB,
                    p.MaKH
                }).ToList()
                .Where(p => p.PhiVS > 0 & !p.TrangThai);
            gcPhiVS.DataSource = data;
        }

        private void LoadPhiQL()
        {
            var data = db.mbMatBangs.Where(p => p.MaKH != null)
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
                }).ToList()
                .Where(p => p.PhiQL > 0 & !p.TrangThai);

            gcMatBang.DataSource = data;
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

        private bool DaThanhToanPVS(int MaMB)
        {
            bool result = false;
            var PhiQLDaThu = db.PhiVeSinhs.Where(p => p.mbMatBang.MaMB == MaMB & p.ThangThanhToan.Value.Month == now.Month & p.ThangThanhToan.Value.Year == now.Year);
            if (PhiQLDaThu.Count() <= 0)
                result = false;
            else
                result = true;

            return result;
        }

        private decimal getPhiQL(int MaMB)
        {
            var pqlhdt = db.thueHopDongs.Where(p => p.MaMB == MaMB).Select(p => p.PhiQL).FirstOrDefault() ?? 0;

            return pqlhdt;
        }

        private decimal getPhiVS(int MaMB)
        {
           

            return 0;
        }

        private void LoadHopTac()
        {
            #region load
            if (objnhanvien.IsSuperAdmin.Value)
            {
                gcCongNo.DataSource = db.hthtCongNos
                    .Where(p => SqlMethods.DateDiffDay(db.GetSystemDate(), p.NgayThanhToan) <= 7
                        & SqlMethods.DateDiffDay(db.GetSystemDate(), p.NgayThanhToan) >= -7
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
            else
            {
                if (GetListNV.Count > 0)
                {
                    gcCongNo.DataSource = db.hthtCongNos
                        .Where(p => SqlMethods.DateDiffDay(db.GetSystemDate(), p.NgayThanhToan) <= 7
                            & SqlMethods.DateDiffDay(db.GetSystemDate(), p.NgayThanhToan) >= -7
                            & p.ConLai > 0 &
                            GetListNV.Contains(p.hdhtHopDong.tnNhanVien.MaNV))
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
                else
                {
                    gcCongNo.DataSource = db.hthtCongNos
                        .Where(p => SqlMethods.DateDiffDay(db.GetSystemDate(), p.NgayThanhToan) <= 7
                            & SqlMethods.DateDiffDay(db.GetSystemDate(), p.NgayThanhToan) >= -7
                            & p.ConLai > 0 &
                            p.hdhtHopDong.tnNhanVien.MaTN == objnhanvien.MaTN)
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
            }
            #endregion
        }

        private void LoadThueHopDong()
        {
            #region load
            if (objnhanvien.IsSuperAdmin.Value)
            {
                gcThueHopDong.DataSource = db.thueCongNos
                        .Where(p => p.ConNo > 0 & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ChuKyMax) <= 7 & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ChuKyMax) >= -7)
                        .OrderBy(p => p.ChuKyMax)
                        .Select(p => new
                        {
                            p.thueHopDong.MaHD,
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
            else
            {
                if (GetListNV.Count > 0)
                {
                    gcThueHopDong.DataSource = db.thueCongNos
                            .Where(p => p.ConNo > 0 & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ChuKyMax) <= 7 & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ChuKyMax) >= -7 &
                                GetListNV.Contains(p.thueHopDong.tnNhanVien.MaNV))
                            .OrderBy(p => p.ChuKyMax)
                            .Select(p => new
                            {
                                p.thueHopDong.MaHD,
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
                else
                {
                    gcThueHopDong.DataSource = db.thueCongNos
                            .Where(p => p.ConNo > 0 & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ChuKyMax) <= 7 & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ChuKyMax) >= -7 &
                                p.thueHopDong.MaNV == objnhanvien.MaNV)
                            .OrderBy(p => p.ChuKyMax)
                            .Select(p => new
                            {
                                p.thueHopDong.MaHD,
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
            }
            #endregion
        }

        private void LoadDVK()
        {
            #region load
            if (objnhanvien.IsSuperAdmin.Value)
            {
                gcdvk.DataSource = db.dvkDichVuThanhToans
                        .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ThangThanhToan) <= 7 & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ThangThanhToan) >= -7)
                        .OrderBy(p => p.ThangThanhToan)
                        .Select(p => new
                        {
                            p.ThanhToanID,
                            ThangThanhToan = string.Format("{0}/{1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year),
                            p.dvTrangThaiNhacNo.TenTT,
                            p.dvTrangThaiNhacNo.MauNen
                        });
            }
            else
            {
                if (GetListNV.Count > 0)
                {
                }
                else
                {
                }
            }
            #endregion
        }

        private void LoadThangMay()
        {
            #region load
            if (objnhanvien.IsSuperAdmin.Value)
            {
                gcThangMay.DataSource = db.dvtmThanhToanThangMays
                        .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ThangThanhToan) <= 7 & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ThangThanhToan) >= -7)
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
            else
            {
                if (GetListNV.Count > 0)
                {
                    gcThangMay.DataSource = db.dvtmThanhToanThangMays
                            .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ThangThanhToan) <= 7 & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ThangThanhToan) >= -7 &
                                GetListNV.Contains(p.dvtmTheThangMay.tnNhanVien.MaNV))
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
                else
                {
                    gcThangMay.DataSource = db.dvtmThanhToanThangMays
                            .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ThangThanhToan) <= 7 & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ThangThanhToan) >= -7 &
                                p.dvtmTheThangMay.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
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
            }
            #endregion
        }

        private void LoadTheXe()
        {
            #region load
            if (objnhanvien.IsSuperAdmin.Value)
            {
            }
            else
            {
                if (GetListNV.Count > 0)
                {
                    //gcTheXe.DataSource = db.dvgxTheXeThanhToans
                    //         .Where(p => p.DaTT == false & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ThangThanhToan) <= 7 & SqlMethods.DateDiffDay(db.GetSystemDate(), p.ThangThanhToan) >= -7 &
                    //            GetListNV.Contains(p.dvgxTheXe.tnNhanVien.MaNV))
                    //         .OrderBy(p => p.ThangThanhToan)
                    //         .Select(p => new
                    //         {
                    //             p.ThanhToanID,
                    //             p.dvgxTheXe.SoThe,
                    //             p.dvgxTheXe.BienSo,
                    //             p.dvgxTheXe.ChuThe,
                    //             KhachHang = p.dvgxTheXe.tnNhanKhau.HoTenNK,
                    //             DiaChi = p.dvgxTheXe.tnNhanKhau.mbMatBang.MaSoMB,
                    //             p.dvgxTheXe.dvgxLoaiXe.PhiGiuXe,
                    //             ThangThanhToan = string.Format("{0}/{1}", p.ThangThanhToan.Value.Month, p.ThangThanhToan.Value.Year),
                    //             p.dvTrangThaiNhacNo.TenTT,
                    //             p.dvTrangThaiNhacNo.MauNen
                    //         });
                }
                else
                {
                }
            }
            #endregion
        }

        private void LoadNuoc()
        {
            #region load
            if (objnhanvien.IsSuperAdmin.Value)
            {
                gcNuoc.DataSource = db.dvdnNuocs
                        .Where(p => p.DaTT == false & p.NgayNhap <= db.GetSystemDate() & SqlMethods.DateDiffDay(db.GetSystemDate(), p.NgayNhap) >= -7)
                        .OrderBy(p => p.NgayNhap)
                        .Select(p => new
                        {
                            p.ID,
                            MaSoMB = p.MaMB.HasValue ? p.mbMatBang.MaSoMB : "",
                            KhachHang = p.tnKhachHang.IsCaNhan.Value ? string.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                            DiaChi = p.tnKhachHang.DCLL,
                            p.SoTieuThu,
                            p.TienNuoc,
                            p.NgayNhap,
                            ThangThanhToan = string.Format("{0}/{1}", p.NgayNhap.Value.Month - 1, p.NgayNhap.Value.Year),
                            p.dvTrangThaiNhacNo.TenTT,
                            p.dvTrangThaiNhacNo.MauNen
                        });
            }
            else
            {
                if (GetListNV.Count > 0)
                {
                    gcNuoc.DataSource = db.dvdnNuocs
                            .Where(p => p.DaTT == false & p.NgayNhap <= db.GetSystemDate() & SqlMethods.DateDiffDay(db.GetSystemDate(), p.NgayNhap) >= -7 &
                                GetListNV.Contains(p.tnNhanVien.MaNV))
                            .OrderBy(p => p.NgayNhap)
                            .Select(p => new
                            {
                                p.ID,
                                MaSoMB = p.MaMB.HasValue ? p.mbMatBang.MaSoMB : "",
                                KhachHang = p.tnKhachHang.IsCaNhan.Value ? string.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                                DiaChi = p.tnKhachHang.DCLL,
                                p.SoTieuThu,
                                p.TienNuoc,
                                p.NgayNhap,
                                ThangThanhToan = string.Format("{0}/{1}", p.NgayNhap.Value.Month - 1, p.NgayNhap.Value.Year),
                                p.dvTrangThaiNhacNo.TenTT,
                                p.dvTrangThaiNhacNo.MauNen
                            });
                }
                else
                {
                    gcNuoc.DataSource = db.dvdnNuocs
                            .Where(p => p.DaTT == false & p.NgayNhap <= db.GetSystemDate() & SqlMethods.DateDiffDay(db.GetSystemDate(), p.NgayNhap) >= -7 &
                                p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
                            .OrderBy(p => p.NgayNhap)
                            .Select(p => new
                            {
                                p.ID,
                                MaSoMB = p.MaMB.HasValue ? p.mbMatBang.MaSoMB : "",
                                KhachHang = p.tnKhachHang.IsCaNhan.Value ? string.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                                DiaChi = p.tnKhachHang.DCLL,
                                p.SoTieuThu,
                                p.TienNuoc,
                                p.NgayNhap,
                                ThangThanhToan = string.Format("{0}/{1}", p.NgayNhap.Value.Month - 1, p.NgayNhap.Value.Year),
                                p.dvTrangThaiNhacNo.TenTT,
                                p.dvTrangThaiNhacNo.MauNen
                            });
                }
            }
            #endregion
        }

        private void LoadDien()
        {
            #region load
            if (objnhanvien.IsSuperAdmin.Value)
            {
                gcDien.DataSource = db.dvdnDiens
                        .Where(p => p.DaTT == false & p.NgayNhap <= db.GetSystemDate() & SqlMethods.DateDiffDay(db.GetSystemDate(), p.NgayNhap) >= -7)
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
            else
            {
                if (GetListNV.Count > 0)
                {
                    gcDien.DataSource = db.dvdnDiens
                            .Where(p => p.DaTT == false & p.NgayNhap <= db.GetSystemDate() & SqlMethods.DateDiffDay(db.GetSystemDate(), p.NgayNhap) >= -7 &
                                GetListNV.Contains(p.tnNhanVien.MaNV))
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
                else
                {
                    gcDien.DataSource = db.dvdnDiens
                            .Where(p => p.DaTT == false & p.NgayNhap <= db.GetSystemDate() & SqlMethods.DateDiffDay(db.GetSystemDate(), p.NgayNhap) >= -7 &
                                p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
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
            }
            #endregion
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void tabNhacNo_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadData();
        }
    }
}
