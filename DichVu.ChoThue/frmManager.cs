using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using System.Collections.Generic;
using System.IO;
using DevExpress.XtraBars.Alerter;
//using LandsoftBuildingGeneral.DynBieuMau;
using Library.CongNoCls;

namespace DichVu.ChoThue
{
    public delegate void AddItemDelegate(int maBM, string tenBM);

    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        int countDenNgayTT;
        bool first = true;

        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        #region Load data

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var sysdt = db.GetSystemDate();
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;
                    var maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

                    if (objnhanvien.IsSuperAdmin.Value)
                    {
                        gcHopDong.DataSource = db.thueHopDongs
                            .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayBG.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayBG.Value, denNgay) >= 0 & p.MaTN == maTN)
                            .OrderByDescending(p => p.NgayHD)
                            .Select(p => new
                            {
                                p.mbMatBang.mbLoaiMatBang.TenLMB,
                                p.MaHD,
                                p.NgayHD,
                                p.NgayBG,
                                p.SoHD,
                                p.ThoiHan,
                                p.thueTrangThai.TenTT,
                                p.thueTrangThai.MauNen,
                                p.mbMatBang.MaSoMB,
                                p.DienTich,
                                p.DonGia,
                                p.ThanhTien,
                                p.PhiQL,
                                p.TienCoc,
                                TenTG = p.tnTyGia.TenVT,
                                KhachHang = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                                DienThoaiKH =p.tnKhachHang.DienThoaiKH,
                                DiaChiKH = p.tnKhachHang.DCLL,
                                p.DienGiai,
                                p.tnNhanVien.HoTenNV,
                                p.ChuKyThanhToan,
                                p.tnTyGia.MaTG,
                                p.MaMB,
                                p.MaKH,
                                TimeRemain = SqlMethods.DateDiffDay(sysdt, p.NgayBG.Value.AddMonths(p.ThoiHan ?? 0)) < 0 ? 0 : SqlMethods.DateDiffDay(sysdt, p.NgayBG.Value.AddMonths(p.ThoiHan ?? 0)),
                                DaHuy = p.DaHuy ?? false,
                                p.ChuKyThanhToanUSD,
                                p.ThanhTienKTTUSD,
                                p.DonGiaUSD,
                                p.ThanhTienUSD,
                                p.TienCocUSD,
                                p.NgayHH,
                                p.NgayBGMB,
                                p.NgayTinhTien,
                                p.ThanhTienKTT,
                                p.MaTT
                            }).OrderByDescending(p => p.NgayHD);
                        countDenNgayTT = db.thueHopDongs
                            .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayHD.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayHD.Value, denNgay) >= 0 &
                                    SqlMethods.DateDiffDay(sysdt, p.NgayBG.Value.AddMonths(p.ThoiHan ?? 0)) <= Library.Properties.Settings.Default.TimeRemain).Count();
                    }
                    else
                    {
                        var GetNhomOfNV = db.pqNhomNhanViens.Where(p => p.IsTruongNhom.Value & p.MaNV == objnhanvien.MaNV).Select(p => p.GroupID).ToList();
                        if (GetNhomOfNV.Count > 0)
                        {
                            var GetListNV = db.pqNhomNhanViens.Where(p => GetNhomOfNV.Contains(p.GroupID)).Select(p => p.MaNV).ToList();

                            gcHopDong.DataSource = db.thueHopDongs
                                .Where(p => p.MaTN == maTN & GetListNV.Contains(p.MaNV.Value) &
                                        SqlMethods.DateDiffDay(tuNgay, p.NgayHD.Value) >= 0 &
                                        SqlMethods.DateDiffDay(p.NgayHD.Value, denNgay) >= 0)
                                .OrderByDescending(p => p.NgayHD)
                                .Select(p => new
                                {
                                    p.mbMatBang.mbLoaiMatBang.TenLMB,
                                    p.MaHD,
                                    p.NgayHD,
                                    p.NgayBG,
                                    p.SoHD,
                                    p.ThoiHan,
                                    p.thueTrangThai.TenTT,
                                    p.thueTrangThai.MauNen,
                                    p.mbMatBang.MaSoMB,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.PhiQL,
                                    p.TienCoc,
                                    TenTG = p.tnTyGia.TenVT,
                                    KhachHang = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                                    DienThoaiKH = p.tnKhachHang.DienThoaiKH,
                                    DiaChiKH = p.tnKhachHang.DCLL,
                                    p.DienGiai,
                                    p.tnNhanVien.HoTenNV,
                                    p.ChuKyThanhToan,
                                    p.MaMB,
                                    p.MaKH,
                                    TimeRemain = SqlMethods.DateDiffDay(sysdt, p.NgayBG.Value.AddMonths(p.ThoiHan ?? 0)) < 0 ? 0 : SqlMethods.DateDiffDay(sysdt, p.NgayBG.Value.AddMonths(p.ThoiHan ?? 0)),
                                    DaHuy = p.DaHuy ?? false,
                                    p.NgayBGMB,
                                    p.NgayTinhTien,
                                    p.ChuKyThanhToanUSD,
                                    p.ThanhTienKTTUSD,
                                    p.DonGiaUSD,
                                    p.ThanhTienUSD,
                                    p.TienCocUSD,
                                    p.NgayHH,
                                    p.ThanhTienKTT
                                });
                        }
                        else
                        {
                            gcHopDong.DataSource = db.thueHopDongs
                                .Where(p => p.MaTN == maTN & p.MaNV == objnhanvien.MaNV &
                                        SqlMethods.DateDiffDay(tuNgay, p.NgayHD.Value) >= 0 &
                                        SqlMethods.DateDiffDay(p.NgayHD.Value, denNgay) >= 0)
                                .OrderByDescending(p => p.NgayHD)
                                .Select(p => new
                                {
                                    p.mbMatBang.mbLoaiMatBang.TenLMB,
                                    p.MaHD,
                                    p.NgayHD,
                                    p.NgayBG,
                                    p.SoHD,
                                    p.ThoiHan,
                                    p.thueTrangThai.TenTT,
                                    p.thueTrangThai.MauNen,
                                    p.mbMatBang.MaSoMB,
                                    p.DienTich,
                                    p.DonGia,
                                    p.ThanhTien,
                                    p.PhiQL,
                                    p.TienCoc,
                                    TenTG = p.tnTyGia.TenVT,
                                    KhachHang = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                                    DienThoaiKH = p.tnKhachHang.DienThoaiKH,
                                    DiaChiKH = p.tnKhachHang.DCLL,
                                    p.DienGiai,
                                    p.tnNhanVien.HoTenNV,
                                    p.ChuKyThanhToan,
                                    p.MaMB,
                                    p.MaKH,
                                    TimeRemain = SqlMethods.DateDiffDay(sysdt, p.NgayBG.Value.AddMonths(p.ThoiHan ?? 0)) < 0 ? 0 : SqlMethods.DateDiffDay(sysdt, p.NgayBG.Value.AddMonths(p.ThoiHan ?? 0)),
                                    DaHuy = p.DaHuy ?? false,
                                    p.NgayBGMB,
                                    p.NgayTinhTien,
                                    p.ChuKyThanhToanUSD,
                                    p.ThanhTienKTTUSD,
                                    p.DonGiaUSD,
                                    p.ThanhTienUSD,
                                    p.TienCocUSD,
                                    p.NgayHH,
                                    p.ThanhTienKTT
                                });
                        }
                        countDenNgayTT = db.thueHopDongs
                            .Where(p => p.MaTN == objnhanvien.MaTN &
                                    SqlMethods.DateDiffDay(tuNgay, p.NgayHD.Value) >= 0 &
                                    SqlMethods.DateDiffDay(p.NgayHD.Value, denNgay) >= 0 &
                                    SqlMethods.DateDiffDay(sysdt, p.NgayBG.Value.AddMonths(p.ThoiHan ?? 0)) <= Library.Properties.Settings.Default.TimeRemain).Count();
                    }
                }
                else
                {
                    gcHopDong.DataSource = null;
                }
            }
            catch { }
            finally { wait.Close(); }
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
        
        private void frmManager_Load(object sender, EventArgs e)
        {
            #region Tạo mẫu hợp đồng (Test only)
            //var lsthd = db.thueHopDongs;
            //Library.BieuMauCls.ReplaceHopDongCls rpl;
            //foreach (var item in lsthd)
            //{
            //    rpl = new Library.BieuMauCls.ReplaceHopDongCls();
            //    rpl.RtfText = db.tblBieuMaus.Single(p => p.MaBM == 18).Template;
            //    rpl.ThayNoiDungHD(item);
            //    item.rtfHopDong = rpl.RtfText;
            //    db.SubmitChanges();
            //}
            #endregion

            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);

            var list = Library.ManagerTowerCls.GetAllTower(objnhanvien);
            lookUpToaNha.DataSource = list;
            if (list.Count > 0)
                itemToaNha.EditValue = list[0].MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cmbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);

            tpkhachhang.PageVisible = false;
            tpKhachHangCty.PageVisible = false;
            lookBieuMau.DataSource = db.BmBieuMaus.Where(p => p.MaLBM == 1)
                .Select(p => new
                {
                    p.MaBM,
                    p.TenBM,
                    p.Description,
                    p.Template
                });
            tabChiTietHopDong.SelectedTabPageIndex = 0;
            string noidung = string.Format("<color=0, 0, 255><b>Số lượng: {0}</b></color>", countDenNgayTT);
            AlertInfo info = new AlertInfo("<b>CÔNG NỢ SẮP ĐẾN NGÀY THANH TOÁN</b>", noidung);
            alertControl1.Show(this, info);

            LoadData();
            first = false;
        }
        
        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if(!first) LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.GetFocusedRowCellValue("MaHD") == null)
            {
                DialogBox.Error("Vui lòng chọn hợp đồng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            var HDTT = (int?)grvHopDong.GetFocusedRowCellValue("MaHD");
            if (HDTT >= 2)
            {
                DialogBox.Alert("Hợp đồng đã được phê duyệt, bạn không thể xóa hợp đồng này!");
                return;
            }
            #region Xóa tập hợp những thanh toán liên quan đến hợp đồng
            var objthuethanhtoan = from p in db.ThueHopDongThanhToans
                                   where p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD")
                                   select p;
            if(objthuethanhtoan != null) db.ThueHopDongThanhToans.DeleteAllOnSubmit(objthuethanhtoan);
            #endregion

            #region Xóa lịch sử giao dịch liên quan
            var objlsgiaodinh = from p in db.lsGiaoDiches
                                where p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD")
                                select p;
            if (objlsgiaodinh != null) db.lsGiaoDiches.DeleteAllOnSubmit(objlsgiaodinh);
            #endregion

            #region Xóa công nợ liên quan
            var objcongno = from p in db.thueCongNos
                            where p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD")
                            select p;
            if(objcongno !=null) db.thueCongNos.DeleteAllOnSubmit(objcongno);
            #endregion

            #region Xóa hợp đồng
            thueHopDong objthuehopdong = new thueHopDong();
            objthuehopdong = db.thueHopDongs.Single(p=>p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD"));
         //   var objCNLS = db.cnLichSus.FirstOrDefault(p => p.MaKH == objthuehopdong.MaKH && p.MaMB == objthuehopdong.MaMB  && p.MaLDV == 5 && p.ngay);
            db.thueHopDongs.DeleteOnSubmit(objthuehopdong);
            try
            {
                var objMB = db.mbMatBangs.Single(p => p.MaMB == objthuehopdong.MaMB);
                if (objthuehopdong.MaTTMB == 3)
                {
                    objMB.MaKH = null;
                    objMB.MaKHF1 = null;
                    objMB.MaTT = objthuehopdong.MaTTMB;
                }
                else
                {
                    objMB.MaKH = objMB.MaKHF1;
                    objMB.MaTT = objthuehopdong.MaTTMB ?? 8;
                }
                /*
                   MaMB = objHD.MaMB,
                                        MaKH = objHD.MaKH,
                                        SoTien = objHD.ThanhTien * objHD.ChuKyThanhToan ?? 0,
                                        MaLDV = 5,
                                        DaThu = 0,
                                        NoTruoc = i * objHD.ThanhTien * objHD.ChuKyThanhToan,
                                        IsPay = false,
                                        NgayNhap = chuky.Max
                 */
                db.SubmitChanges();
                grvHopDong.DeleteSelectedRows();
                DialogBox.Alert("Ðã xóa hợp đồng " + objthuehopdong.SoHD);
            }
            catch
            {
                DialogBox.Error("Không xóa được hợp đồng này");
            }
            #endregion
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn hợp đồng");
                return;
            }

            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, objHD = db.thueHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD")) })
            {
                frm.MaTN = Convert.ToInt32(itemToaNha.EditValue.ToString());
                frm.IsEdit = true;
                frm.HDTT = (int?)grvHopDong.GetFocusedRowCellValue("MaHD");
                frm.ShowDialog();
                
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemToaNha.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }

            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien })
            {
                frm.MaTN = Convert.ToInt32(itemToaNha.EditValue.ToString());
                frm.ShowDialog();
                
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void grvHopDong_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvHopDong.GetRowCellValue(e.RowHandle, "MauNen").ToString()));
            }
            catch { }
        }
        #endregion

        #region Xu ly chi tiet

        int MaHD;

        private void LoadMB(int MaHD)
        {
             gcMatBang.DataSource = db.thueHopDongs
                .Where(p=>p.MaHD == MaHD)
                .Select(p=> new
                {
                    MSMB = p.mbMatBang.MaSoMB,
                    TenLoaiMB = p.mbMatBang.mbLoaiMatBang.TenLMB,
                    ThuocToaNha = p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.TenTN,
                    //DonGia = p.mbMatBang.DonGia,
                    DienTich = p.mbMatBang.DienTich,
                    //ThanhTien = p.mbMatBang.ThanhTien,
                    TrangThai = p.mbMatBang.mbTrangThai.TenTT,
                    DienGiai = p.mbMatBang.DienGiai,
                    DonGia=0, ThanhTien=0

                }).ToList();
        }

        private void LoadKhachHang(int MaHD)
        {
            bool IsCaNhan = (bool)db.tnKhachHangs.Single(kh => kh.MaKH == db.thueHopDongs.Single(p=>p.MaHD==MaHD).MaKH).IsCaNhan;
            if (IsCaNhan)
            {
                tpkhachhang.PageVisible = true;
                tpKhachHangCty.PageVisible = false;
                gckhachhang.DataSource = db.thueHopDongs
                    .Where(p => p.MaHD == MaHD)
                    .Select(p => new
                    {
                        MaKhachHang = p.tnKhachHang.MaKH,
                        TenKhachHang = String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH),
                        NgaySinhKhachHang = p.tnKhachHang.NgaySinh,
                        GioiTinhKhachHang = (bool)p.tnKhachHang.GioiTinh ? "Nam" : "Nữ",
                        DiaChiLienLac = p.tnKhachHang.DCLL,
                        EmailKhachHang = p.tnKhachHang.EmailKH,
                        DienThoaiKhachHang = p.tnKhachHang.DienThoaiKH

                    }).ToList();
            }
            else
            {
                tpkhachhang.PageVisible = false;
                tpKhachHangCty.PageVisible = true;
                gcDoanhNghiep.DataSource = db.thueHopDongs
                    .Where(p=>p.MaHD == MaHD)
                    .Select(p => new
                    {
                        TenCty = p.tnKhachHang.CtyTen,
                        TenCtyVT = p.tnKhachHang.CtyTenVT,
                        DiaChiCty = p.tnKhachHang.DCLL,
                        DienThoaiCty = p.tnKhachHang.DienThoaiKH,
                        FaxCty = p.tnKhachHang.CtyFax,
                        MaSoThueCty = p.tnKhachHang.CtyMaSoThue,
                        SoDKKDCty = p.tnKhachHang.CtySoDKKD,
                        NgayDKKDCty = p.tnKhachHang.CtyNgayDKKD,
                        NoiDKKDCty = p.tnKhachHang.CtyNoiDKKD,
                        NguoiDDCTY = p.tnKhachHang.CtyNguoiDD,
                        ChucVuNDD = p.tnKhachHang.CtyChucVuDD,
                        TenNKCty = p.tnKhachHang.CtyTenNH,
                        SoTKNHCty = p.tnKhachHang.CtySoTKNH
                    }).ToList();
            }
        }
        #endregion

        void LoadPhuLuc(int MaHD)
        {
            try
            {
                gcPhuLuc.DataSource = (from pl in db.thuePhuLucs
                                       join hdt in db.thueHopDongs on pl.MaHD equals hdt.MaHD into hptt
                                       from hd in hptt.DefaultIfEmpty()
                                       join kht in db.tnKhachHangs on hd.MaKH equals kht.MaKH into khtt
                                       from kh in khtt.DefaultIfEmpty()
                                       where pl.MaHD == MaHD
                                       select new
                                       {
                                           pl.ID,
                                           pl.SoPL,
                                           pl.NgayPL,
                                           hd.MaHD,
                                           hd.SoHD,
                                           MaSoMB = hd.mbMatBang != null ? hd.mbMatBang.MaSoMB : "",
                                           pl.ThoiHan,
                                           TenTT = hd.thueTrangThai != null ? hd.thueTrangThai.TenTT : "",
                                           pl.DienTich,
                                           pl.DonGia,
                                           pl.GiaThue,
                                           pl.DienGiai,
                                           pl.NgayGiao,
                                           TenTG = hd.tnTyGia != null ? hd.tnTyGia.TenVT : "",
                                           KhachHang = hd.MaKH != null ? (bool)kh.IsCaNhan ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen : "",
                                           DienThoaiKH = hd.MaKH != null ?  kh.DienThoaiKH : "",
                                           DiaChiKH = hd.MaKH != null ?  kh.DCLL : "",
                                           pl.tnNhanVien.HoTenNV,
                                           pl.ChuKyThanhToan,
                                           DaDuyet = pl.IsConfirm ?? false,
                                           pl.NgayTao,
                                           pl.NgayCN,
                                           HoTenNVCN = pl.MaNVCN != null ? pl.tnNhanVien1.HoTenNV : "",
                                           pl.NgayHH,
                                           pl.SoTienCoc,
                                           pl.tnTyGia.TenVT,
                                           pl.ThanhTienKTT,
                                           pl.DonGiaUSD,
                                           pl.GiaThueUSD,
                                           pl.ChuKyThanhToanUSD,
                                           pl.ThanhTienKTTUSD,
                                           pl.SoTienCocUSD,
                                       }).OrderByDescending(p => p.NgayPL).ToList();
            }
            catch
            { }
        }

        private void grvHopDong_DoubleClick(object sender, EventArgs e)
        {
            if (itemSua.Enabled == false) return;

            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn hợp đồng");
                return;
            }

            using (frmEdit frm = new frmEdit() { objnhanvien = objnhanvien, objHD = db.thueHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD")) })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnInBienBanBanGiao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                Library.DialogBox.Error("Vui lòng chọn hợp đồng");
                return;
            }

            var wait = DialogBox.WaitingForm();
            MaHD = (int)grvHopDong.GetFocusedRowCellValue("MaHD");
            thueHopDong hopdong = db.thueHopDongs.Single(p => p.MaHD == MaHD);
            var rpl = new Library.BieuMauCls.ReplaceHopDongCls() 
            { RtfText = db.BmBieuMaus.Single(p => p.MaBM == 19).Template /*HOP DONG BAN GIAO*/ };
            rpl.ThayNoiDungBanGiao(hopdong);
            rpl.ShowPrintPreview();
            wait.Close(); wait.Dispose();
        }

        private void btnInCongNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                Library.DialogBox.Error("Vui lòng chọn hợp đồng");
                return;
            }
            List<int> ListMHD = new List<int>();
            MaHD = (int)grvHopDong.GetFocusedRowCellValue("MaHD");
            ListMHD.Add(MaHD);
            //using (var frm = new frmPrintControl(ListMHD, 2, ""))
            //{
            //    frm.ShowDialog();
            //}
        }

        private void btnThahToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                Library.DialogBox.Error("Vui lòng chọn [Hợp đồng], xin cảm ơn.");
                return;
            }
            MaHD = (int)grvHopDong.GetFocusedRowCellValue("MaHD");
            if (db.thueHopDongs.Single(p => p.MaHD == MaHD).DaHuy ?? false)
            {
                Library.DialogBox.Error("Hợp đồng này đã bị hủy!!!");
                return;
            }
            using (var frm = new frmPaid())
            {
                frm.MaMB = (int)grvHopDong.GetFocusedRowCellValue("MaMB");
                frm.MaSoMB = grvHopDong.GetFocusedRowCellValue("MaSoMB").ToString();
                frm.objnhanvien = objnhanvien;
                frm.objhd = db.thueHopDongs.Single(hd => hd.MaHD == MaHD);
                frm.ShowDialog();
            }
        }

        decimal customSum;
        private void grvHopDong_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            string colName = (e.Item as GridSummaryItem).Tag.ToString();
            GridView view = sender as GridView;

            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
            {
                customSum = 0;
            }

            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
            {
                int Matigia = (int)(view.GetRowCellValue(e.RowHandle, colMaTG) ?? 1);
                decimal tigia = db.tnTyGias.Single(p => p.MaTG == Matigia).TyGia.Value ;
                customSum += Convert.ToDecimal(e.FieldValue)*tigia;
            }

            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
            {
                e.TotalValue = customSum;
            }
        }

        private void grvHopDong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Clicks();
        }

        void Clicks()
        {
            if (grvHopDong.FocusedRowHandle < 0) return;
            MaHD = (int)grvHopDong.GetFocusedRowCellValue("MaHD");

            switch (tabChiTietHopDong.SelectedTabPageIndex)
            {
                case 0:
                    LoadMB(MaHD);
                    break;
                case 1:
                case 2:
                    LoadKhachHang(MaHD);
                    break;
                case 3:
                    LoadTaiLieu(MaHD);
                    break;
                case 4:
                    LoadBieuMau(MaHD);
                    break;
                case 5:
                    LoadLichSu(MaHD);
                    break;
                case 6:
                    LoadCongNo((int)grvHopDong.GetFocusedRowCellValue("MaMB"));
                    break;
                case 7:
                    ctlTaiLieu1.FormID = 26;
                    ctlTaiLieu1.LinkID = MaHD;
                    ctlTaiLieu1.MaNV = objnhanvien.MaNV;
                    ctlTaiLieu1.objNV = objnhanvien;
                    ctlTaiLieu1.TaiLieu_Load();
                    break;
                case 8:
                    LoadPhuLuc(MaHD);
                    break;
                case 9:
                    gcTienCoc.DataSource = db.thueLTTCocs.Where(p => p.MaHD == MaHD);
                    break;
            }
        }

        private void LoadCongNo(int maMB)
        {
            gcDichVu.DataSource = db.PaidMulti(maMB);
        }

        void LoadLichSu(int maHD)
        {
            gcLichSu.DataSource = db.thueLichSu_getBy(maHD);
        }

        private void LoadBieuMau(int MaHD)
        {
            gcBieuMau.DataSource = db.thuehopdong_BieuMau_getByMaHD(MaHD);
        }

        private void LoadTaiLieu(int MaHD)
        {
            gcTaiLieu.DataSource = db.thueHopDong_DinhKems.Where(p => p.MaHD == MaHD)
                .Select(p => new
                {
                    p.ID,
                    p.TenFile,
                    p.DienGiai,
                    p.FileName
                });
        }

        private void ItemChuyenQuyenSuDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hợp đồng], xin cảm ơn.");
                return;
            }

            thueHopDong hd = db.thueHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD"));
            if (hd.MaTT != 2)
            {
                DialogBox.Alert("Chỉ bàn giao mặt bằng cho [Hợp đồng] đang ở tình trạng [Đã duyệt]. Vui lòng kiểm tra lại, xin cảm ơn.");
                return;
            }

            frmChuyenKhachHang frm = new frmChuyenKhachHang();
            frm.objnhanvien = objnhanvien;
            frm.objmb = db.mbMatBangs.Single(p => p.MaMB == (int)grvHopDong.GetFocusedRowCellValue("MaMB"));
            if (grvHopDong.GetFocusedRowCellValue("MaKH") != null)
            {
                frm.objkhachHangDestination = db.tnKhachHangs.Single(p => p.MaKH == (int)grvHopDong.GetFocusedRowCellValue("MaKH"));
                if (frm.objmb.MaKH != null)
                    frm.objkhachHangSource = db.tnKhachHangs.Single(p => p.MaKH == frm.objmb.MaKH);
            }
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                var wait = DialogBox.WaitingForm();
                using (MasterDataContext dbnew = new MasterDataContext())
                {
                    thueHopDong tthd = dbnew.thueHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD"));
                    
                    tthd.MaTT = 4; // Đã bàn giao mặt bằng

                    mbMatBang objmb = dbnew.mbMatBangs.Single(p => p.MaMB == (int)grvHopDong.GetFocusedRowCellValue("MaMB"));
                    objmb.MaTT = 4;//Đã cho thuê dài hạn

                    var objLS = new thueLichSu();
                    objLS.MaNV = objnhanvien.MaNV;
                    objLS.MaTT = 4;
                    objLS.MaHD = (int)grvHopDong.GetFocusedRowCellValue("MaHD");
                    objLS.NgayTao = db.GetSystemDate();
                    objLS.DienGiai = "Bàn giao mặt bằng";
                    db.thueLichSus.InsertOnSubmit(objLS);

                    #region Old code: Luu lịch sử giao dịch
                    //lsGiaoDich objGD = new lsGiaoDich();
                    //dbnew.lsGiaoDiches.InsertOnSubmit(objGD);
                    //objGD.MaHD = tthd.MaHD;
                    //objGD.MaKH = tthd.tnKhachHang.MaKH;
                    //objGD.MaNV = objnhanvien.MaNV;
                    //if (hd.MaMB != null) objGD.MaMB = hd.mbMatBang.MaMB;
                    //if (hd.MaLo != null) objGD.MaLo = hd.mbMatBang_ChiaLo.MaLo;
                    //if (tthd.ThoiHan > 0)  
                    //    objGD.MaTT = 4;// Đã bàn giao mặt bằng
                    //else 
                    //    objGD.MaTT = 2; //đã bán
                    //objGD.NgayLap = db.GetSystemDate();
                    //objGD.DienGiai = "Bàn giao mặt bằng";
                    #endregion

                    try
                    {
                        dbnew.SubmitChanges();
                        LoadData();
                    }
                    catch
                    { }
                    finally
                    {
                        wait.Close();
                        wait.Dispose();
                    }
                }
            }
        }

        private void btn2Excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;

                    DataTable dt = new DataTable();
                    
                    var ts = db.thueCongNos
                                .Where(p => SqlMethods.DateDiffDay(tuNgay, p.ChuKyMax.Value) >= 0
                                    & SqlMethods.DateDiffDay(p.ChuKyMax.Value, denNgay) >= 0)
                                .OrderBy(p => p.thueHopDong.mbMatBang.MaSoMB)
                                .Select(p => new
                                {
                                    p.MaCN,
                                    p.thueHopDong.SoHD,
                                    KhachHang = p.thueHopDong.MaKH.HasValue ? (p.thueHopDong.tnKhachHang.IsCaNhan.HasValue ? (p.thueHopDong.tnKhachHang.IsCaNhan.Value ? p.thueHopDong.tnKhachHang.HoKH + " " + p.thueHopDong.tnKhachHang.TenKH : p.thueHopDong.tnKhachHang.CtyTen) : "") : "",
                                    MatBang = p.thueHopDong.mbMatBang.MaSoMB,
                                    p.thueHopDong.ChuKyThanhToan,
                                    ChuKy = string.Format("{0}-{1}", p.ChuKyMin.Value.ToShortDateString(), p.ChuKyMax.Value.ToShortDateString()),
                                    p.ConNo,
                                    p.DaThanhToan
                                });
                    dt = SqlCommon.LINQToDataTable(ts);
                    ExportToExcel.exportDataToExcel("Danh sách công nợ hợp đồng thuê", dt);
                }
            }
        }

        private void btn2hdt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;

                    DataTable dt = new DataTable();

                    var ts = db.thueHopDongs
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayHD.Value) >= 0 &
                                SqlMethods.DateDiffDay(p.NgayHD.Value, denNgay) >= 0)
                        .OrderByDescending(p => p.NgayHD)
                        .Select(p => new
                        {
                            p.MaHD,
                            p.NgayHD,
                            p.NgayBG,
                            p.SoHD,
                            p.ThoiHan,
                            TrangThaiHopDong = p.thueTrangThai.TenTT,
                            p.mbMatBang.MaSoMB,
                            p.DienTich,
                            p.DonGia,
                            p.ThanhTien,
                            p.PhiQL,
                            p.TienCoc,
                            TenTG = p.tnTyGia.TenVT,
                            TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen,
                            DienThoaiKH = p.tnKhachHang.DienThoaiKH,
                            DiaChiKH = p.tnKhachHang.DCLL,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.ChuKyThanhToan,
                            p.tnTyGia.MaTG,
                            p.MaMB,
                            p.MaKH
                        });
                    dt = SqlCommon.LINQToDataTable(ts);
                    ExportToExcel.exportDataToExcel("Danh sách hợp đồng thuê", dt);
                }
            }
        }

        private void btnDaDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hợp đồng], xin cảm ơn.");
                return;
            }

            using (MasterDataContext db = new MasterDataContext())
            {
                try
                {
                    thueHopDong hd = db.thueHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD"));
                    if (hd.MaTT == 1)
                    {
                        var f = new frmDuyet();
                        f.MaHD = hd.MaHD;
                        f.ShowDialog();
                        if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            hd.MaTT = 2; // Da duyet

                            var objLS = new thueLichSu();
                            objLS = f.objLS;
                            objLS.MaNV = objnhanvien.MaNV;
                            objLS.MaTT = 2;
                            objLS.NgayTao = db.GetSystemDate();
                            db.thueLichSus.InsertOnSubmit(objLS);

                            #region Old code: Luu lịch sử giao dịch
                            //lsGiaoDich objGD = new lsGiaoDich();
                            //db.lsGiaoDiches.InsertOnSubmit(objGD);
                            //objGD.MaHD = hd.MaHD;
                            //objGD.MaKH = hd.tnKhachHang.MaKH;
                            //objGD.MaNV = objnhanvien.MaNV;
                            //if (hd.MaMB != null) objGD.MaMB = hd.mbMatBang.MaMB;
                            //if (hd.MaLo != null) objGD.MaLo = hd.mbMatBang_ChiaLo.MaLo;
                            //objGD.MaTT = 2;// Da duyet
                            //objGD.NgayLap = db.GetSystemDate();
                            //objGD.DienGiai = "Duyệt hợp đồng";
                            #endregion

                            try
                            {
                                db.SubmitChanges();
                                LoadData();
                            }
                            catch
                            { }
                        }
                    }
                    else
                    {
                        DialogBox.Alert("Chỉ duyệt những [Hợp đồng] ở tình trạng [Chờ duyệt].\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                        return;
                    }

                    #region Old code: Add vào công nợ
                    //if (hd.thueTrangThai.MaTT != 2) //Hop dong chua duyet
                    //{
                    //}
                    //else
                    //{
                    //    var congno = db.thueCongNos.FirstOrDefault(p => p.MaHD == hd.MaHD);
                    //    if (congno == null)
                    //    {
                    //        Library.CongNoCls.HopDongThue objhopdongthue = new Library.CongNoCls.HopDongThue();
                    //        Library.CongNoCls.ChuKyCls objchuky = new Library.CongNoCls.ChuKyCls();
                    //        List<Library.CongNoCls.ChuKyCls> lstchuky = new List<Library.CongNoCls.ChuKyCls>();
                    //        objchuky = objhopdongthue.LayChuKyTheoThoiDiemHienTai(hd);
                    //        lstchuky = objhopdongthue.DanhSachChuKyThanhToan(hd);

                    //        decimal solan = 0;
                    //        try
                    //        {
                    //            solan = (decimal)(hd.ThoiHan / hd.ChuKyThanhToan);
                    //        }
                    //        catch
                    //        {
                    //            if (DialogBox.Question("Hợp đồng này không có thời hạn sử dụng và chu kỳ thanh toán.\r\nBạn có muốn phát sinh công nợ thanh toán 1 lần duy nhất không?") == System.Windows.Forms.DialogResult.No)
                    //            {
                    //                return;
                    //            }
                    //        }

                    //        if (solan <= 0) solan = 1;

                    //        var objcn = from p in db.thueCongNos
                    //                    where p.MaHD == hd.MaHD
                    //                    select p;
                    //        if (objcn.Count() <= 0)
                    //        {
                    //            foreach (Library.CongNoCls.ChuKyCls chuky in lstchuky)
                    //            {
                    //                thueCongNo objcongno = new thueCongNo()
                    //                {
                    //                    MaHD = hd.MaHD,
                    //                    DaThanhToan = 0,
                    //                    ConNo = (hd.ThanhTien / (hd.ThoiHan ?? 1)) * (hd.ChuKyThanhToan ?? 1),
                    //                    //TODO: 10%
                    //                    ChuKyMin = chuky.Min,
                    //                    ChuKyMax = chuky.Max
                    //                };
                    //                db.thueCongNos.InsertOnSubmit(objcongno);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            foreach (thueCongNo item in objcn)
                    //            {
                    //                item.DaThanhToan = 0;
                    //                item.ConNo = hd.ThanhTien;
                    //            }
                    //        }
                    //    }

                    //    try
                    //    {
                    //        db.SubmitChanges();
                    //    }
                    //    catch
                    //    {
                    //    }

                    //}
                    #endregion
                }
                catch { }
            }
        }

        private void btnGiayBaoTongHop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hợp đồng], xin cảm ơn.");
                return;
            }
            List<int> SelectedMaHD = new List<int>();
            foreach (int row in grvHopDong.GetSelectedRows())
            {
                SelectedMaHD.Add((int)grvHopDong.GetRowCellValue(row, colMaHD));
            }
            using (DichVu.ChoThue.PickDate frm = new PickDate() { SelectedMaHD = SelectedMaHD })
            {
                frm.ShowDialog();
            }
        }
       
        private void grvHopDong_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (e.Column.Name != colTimeRemain.Name) return;
                DevExpress.XtraGrid.Views.Base.ColumnView view = sender as DevExpress.XtraGrid.Views.Base.ColumnView;
                if ((int)e.CellValue == 0) return;
                if ((int)e.CellValue >= Library.Properties.Settings.Default.TimeRemain) return;//TODO: Time remain for contrack (100 days)
                e.Appearance.BackColor = Color.Red;
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.GetFont().FontFamily, 8, FontStyle.Bold | FontStyle.Italic);
            }
            catch { }
        }

        private void btnThemTaiLieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.GetFocusedRowCellValue("MaHD") == null)
            {
                DialogBox.Alert("Vui lòng chọn [Hợp đồng], xin cảm ơn.");
                return;
            }
            var objhd = db.thueHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD"));
            frmUpload frm = new frmUpload();
            frm.objhd = objhd;
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) grvHopDong_FocusedRowChanged(null, null) ;
        }

        private void btnTaiTaiLieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            byte[] FileBytes = null;
            FileStream stream;
            //string filename = textBox1.Text.Substring(Convert.ToInt32(textBox1.Text.LastIndexOf("\\")) + 1, textBox1.Text.Length - (Convert.ToInt32(textBox1.Text.LastIndexOf("\\")) + 1));
            //string filetype = textBox1.Text.Substring(Convert.ToInt32(textBox1.Text.LastIndexOf(".")) + 1, textBox1.Text.Length - (Convert.ToInt32(textBox1.Text.LastIndexOf(".")) + 1));
 
            SaveFileDialog open = new SaveFileDialog();
            open.Filter = "Tất cả|*.*";
            if(open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    int id = (int)grvTaiLieu.GetFocusedRowCellValue("ID");
                    var filedb = db.thueHopDong_DinhKems.Single(p => p.ID == id);
                    string filetype = filedb.FileName.Substring(Convert.ToInt32(filedb.FileName.LastIndexOf(".")) + 1, filedb.FileName.Length - (Convert.ToInt32(filedb.FileName.LastIndexOf(".")) + 1));

                    FileBytes = (byte[])filedb.FileDinhKem.ToArray();
                    stream = new FileStream(open.FileName + "." + filetype, FileMode.Create);
                    stream.Write(FileBytes, 0, FileBytes.Length);
                    stream.Close();
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
        }

        private void btnXoaTaiLieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            int id = (int)grvTaiLieu.GetFocusedRowCellValue("ID");
            var filedb = db.thueHopDong_DinhKems.Single(p => p.ID == id);
            db.thueHopDong_DinhKems.DeleteOnSubmit(filedb);
            try
            {
                db.SubmitChanges();
                grvTaiLieu.DeleteSelectedRows();
                DialogBox.Alert("Xóa tài liệu thành công!");
                grvHopDong_FocusedRowChanged(null, null);
            }
            catch
            {
                DialogBox.Alert("Không xóa tài liệu được");
            }
        }

        private void btnHDSapHetHan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDanhSachHopDongSapHetHan frm = new frmDanhSachHopDongSapHetHan() { objnhanvien = objnhanvien };
            frm.ShowDialog();
        }

        private void btnSuaBieuMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBieuMau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Biểu mẫu], xin cảm ơn.");
                return;
            }

            using (var dc = new MasterDataContext())
            {
                var bm = dc.thuehopdong_BieuMaus.Single(p => p.ID == (int)grvBieuMau.GetFocusedRowCellValue("ID"));
                //var hd = db.thueHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD"));
                //if (bm.thueHopDong.MaTT == 2 | bm.thueHopDong.MaTT == 4 | !objnhanvien.IsSuperAdmin.Value)
                //{
                //    DialogBox.Alert("Hợp đồng đã duyệt hoặc đã bàn giao nên không thể chỉnh sửa!");
                //    return;
                //}
                //using (frmDesign frm = new frmDesign())
                //{
                //    frm.RtfText = bm.NoiDung;
                //    frm.ShowDialog();
                //    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                //    {
                //        bm.NoiDung = frm.RtfText;
                //        dc.SubmitChanges();
                //    }
                //}
            }
        }

        private void btnXemBieuMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBieuMau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Biểu mẫu], xin cảm ơn.");
                return;
            }

            using (var dc = new MasterDataContext())
            {
                int idbm = (int)grvBieuMau.GetFocusedRowCellValue(colID);
                int mahd = (int)grvHopDong.GetFocusedRowCellValue("MaHD");
                //using (frmPreview frm = new frmPreview())
                //{
                //    var wait = DialogBox.WaitingForm();
                //    try
                //    {
                //        var bm = dc.thuehopdong_BieuMaus.Single(p => p.ID == idbm);
                //        var objhd = dc.thueHopDongs.Single(p => p.MaHD == mahd);
                //        Library.BieuMauCls.ReplaceHopDongCls rpl = new Library.BieuMauCls.ReplaceHopDongCls() { RtfText = bm.NoiDung };
                //        rpl.ThayNoiDungHD(objhd);
                //        frm.RtfText = rpl.RtfText;

                //        wait.Close();
                //        frm.ShowDialog();
                //    }
                //    catch { }
                //    finally
                //    {
                //        wait.Dispose();
                //    }
                //}
            }
        }

        private void btnXoaBieuMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBieuMau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Biểu mẫu], xin cảm ơn.");
                return;
            }
            var bm = db.thuehopdong_BieuMaus.Single(p => p.ID == (int)grvBieuMau.GetFocusedRowCellValue(colID));
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
            {
                db.thuehopdong_BieuMaus.DeleteOnSubmit(bm);
                db.SubmitChanges();
                LoadData();
                grvHopDong_FocusedRowChanged(null, null);
            }
        }

        private void btnThemBieuMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAddBM frm = new frmAddBM();
            frm.AddItemCallback = new AddItemDelegate(DelegateAddBieuMau);
            frm.ShowDialog();
        }

        private void DelegateAddBieuMau(int maBM, string tenBM)
        {
            var wait = DialogBox.WaitingForm();

            int mahd = (int)grvHopDong.GetFocusedRowCellValue("MaHD");
            //var bm = db.BmBieuMaus.Single(p => p.MaBM == MaBM);
            //var objhd = db.thueHopDongs.Single(p => p.MaHD == mahd);
            //Library.BieuMauCls.ReplaceHopDongCls rpl = new Library.BieuMauCls.ReplaceHopDongCls() { RtfText = bm.Template /*HOP DONG THUE VAN PHONG*/ };
            //rpl.ThayNoiDungHD(objhd);
            var objhdbm = new thuehopdong_BieuMau()
            {
                MaHD = mahd,
                NgayThem = DateTime.Now,
                NoiDung = "",
                TenBieuMau = tenBM,
                MaBM = maBM,
                MaNV = objnhanvien.MaNV
            };

            db.thuehopdong_BieuMaus.InsertOnSubmit(objhdbm);
            db.SubmitChanges();
            LoadData();
            grvHopDong_FocusedRowChanged(null, null);

            wait.Close();
            wait.Dispose();
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBieuMau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Biểu mẫu], xin cảm ơn.");
                return;
            }

            using (var dc = new MasterDataContext())
            {
                int idbm = (int)grvBieuMau.GetFocusedRowCellValue(colID);
                int mahd = (int)grvHopDong.GetFocusedRowCellValue("MaHD");
                var bm = dc.thuehopdong_BieuMaus.Single(p => p.ID == idbm);
                var objhd = dc.thueHopDongs.Single(p => p.MaHD == mahd);
                Library.BieuMauCls.ReplaceHopDongCls rpl = new Library.BieuMauCls.ReplaceHopDongCls() { RtfText = bm.NoiDung };
                rpl.ThayNoiDungHD(objhd);
                rpl.ShowPrintDialog();
            }
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (Import.frmImport frm = new Import.frmImport() { objnhanvien = objnhanvien, TrangThaiMatBang = 2 })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnImportThue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (Import.frmImport frm = new Import.frmImport() { objnhanvien = objnhanvien, TrangThaiMatBang = 4 })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnPhatSinhCongNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region Code1
            //if (grvHopDong.FocusedRowHandle < 0)
            //{
            //    DialogBox.Alert("Vui lòng chọn [Hợp đồng], xin cảm ơn.");
            //    return;
            //}

            //thueHopDong objHD = db.thueHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD"));

            //if (DialogBox.Question("Bạn có muốn hình thành công nợ cho hợp đồng này không?") == System.Windows.Forms.DialogResult.Yes)
            //{
            //    #region Add vào công nợ
            //    var congno = db.thueCongNos.FirstOrDefault(p => p.MaHD == objHD.MaHD);
            //    if (congno == null)
            //    {
            //        Library.CongNoCls.HopDongThue objhopdongthue = new Library.CongNoCls.HopDongThue();
            //        ChuKyCls objchuky = new ChuKyCls();
            //        List<ChuKyCls> lstchuky = new List<ChuKyCls>();
            //        objchuky = objhopdongthue.LayChuKyTheoThoiDiemHienTai(objHD);
            //        lstchuky = objhopdongthue.DanhSachChuKyThanhToan(objHD);

            //        decimal solan = 0;
            //        int? CKTT = objHD.ChuKyThanhToan.GetValueOrDefault() == 0 ? objHD.ChuKyThanhToanUSD : objHD.ChuKyThanhToan;
            //        try
            //        {
            //            solan = (decimal)(objHD.ThoiHan / CKTT);
            //        }
            //        catch
            //        {
            //            if (DialogBox.Question("Hợp đồng này không có thời hạn sử dụng và chu kỳ thanh toán.\r\nBạn có muốn phát sinh công nợ thanh toán 1 lần duy nhất không?") == System.Windows.Forms.DialogResult.No)
            //            {
            //                return;
            //            }
            //        }
            //        if (solan <= 0) solan = 1;

            //        var objcn = from p in db.thueCongNos
            //                    where p.MaHD == objHD.MaHD
            //                    select p;
            //        var objLS = db.cnLichSus.Where(p => p.MaLDV == 5 && p.MaMB == objHD.MaMB);
            //        if (objHD.ChuKyThanhToan > 0)//Nếu chu kỳ thánh toán là tiên việt
            //        {
            //            if (objcn.Count() <= 0)
            //            {
            //                for (int i = 0; i < lstchuky.Count; i++)
            //                {
            //                    var chuky = lstchuky[i];
            //                    if (i != lstchuky.Count - 1)
            //                    {
            //                        thueCongNo objcongno = new thueCongNo()
            //                        {
            //                            MaHD = objHD.MaHD,
            //                            DaThanhToan = 0,
            //                            ConNo = objHD.ThanhTien * objHD.ChuKyThanhToan ?? 0,
            //                            //Sau thue
            //                            ChuKyMin = chuky.Min,
            //                            ChuKyMax = chuky.Max
            //                        };
            //                        db.thueCongNos.InsertOnSubmit(objcongno);

            //                    }
            //                    else
            //                    {
            //                        if (objHD.ThoiHan % objHD.ChuKyThanhToan != 0)
            //                        {
            //                            thueCongNo objcongno = new thueCongNo()
            //                            {
            //                                MaHD = objHD.MaHD,
            //                                DaThanhToan = 0,
            //                                ConNo = objHD.ThanhTien * (objHD.ThoiHan % objHD.ChuKyThanhToan) ?? 0,
            //                                //Sau thue
            //                                ChuKyMin = chuky.Min,
            //                                ChuKyMax = chuky.Max
            //                            };
            //                            db.thueCongNos.InsertOnSubmit(objcongno);
            //                        }
            //                        else
            //                        {
            //                            thueCongNo objcongno = new thueCongNo()
            //                            {
            //                                MaHD = objHD.MaHD,
            //                                DaThanhToan = 0,
            //                                ConNo = objHD.ThanhTien * objHD.ChuKyThanhToan ?? 0,
            //                                //Sau thue
            //                                ChuKyMin = chuky.Min,
            //                                ChuKyMax = chuky.Max
            //                            };
            //                            db.thueCongNos.InsertOnSubmit(objcongno);
            //                        }
            //                    }
            //                    if (objLS.Count() < lstchuky.Count)
            //                    {
            //                        cnLichSu objcnls = new cnLichSu()
            //                        {
            //                            MaMB = objHD.MaMB,
            //                            MaKH = objHD.MaKH,
            //                            SoTien = objHD.ThanhTien * objHD.ChuKyThanhToan ?? 0,
            //                            MaLDV = 5,
            //                            DaThu = 0,
            //                            NoTruoc = i * objHD.ThanhTien * objHD.ChuKyThanhToan,
            //                            IsPay = false,
            //                            NgayNhap = chuky.Max
            //                        };
            //                        db.cnLichSus.InsertOnSubmit(objcnls);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                foreach (thueCongNo item in objcn)
            //                {
            //                    item.DaThanhToan = 0;
            //                    item.ConNo = objHD.ThanhTien / (objHD.ThoiHan ?? 1);
            //                }
            //            }
            //        }
            //        else// Nếu chu kỳ thanh toán là usd
            //        {
            //            if (objcn.Count() <= 0)
            //            {
            //                for (int i = 0; i < lstchuky.Count; i++)
            //                {
            //                    var chuky = lstchuky[i];
            //                    if (i != lstchuky.Count - 1)
            //                    {
            //                        thueCongNo objcongno = new thueCongNo()
            //                        {
            //                            MaHD = objHD.MaHD,
            //                            DaThanhToan = 0,
            //                            ConNo = objHD.ThanhTienUSD * objHD.ChuKyThanhToanUSD ?? 0,
            //                            //Sau thue
            //                            ChuKyMin = chuky.Min,
            //                            ChuKyMax = chuky.Max
            //                        };
            //                        db.thueCongNos.InsertOnSubmit(objcongno);

            //                    }
            //                    else
            //                    {
            //                        if (objHD.ThoiHan % objHD.ChuKyThanhToanUSD != 0)
            //                        {
            //                            thueCongNo objcongno = new thueCongNo()
            //                            {
            //                                MaHD = objHD.MaHD,
            //                                DaThanhToan = 0,
            //                                ConNo = objHD.ThanhTienUSD * (objHD.ThoiHan % objHD.ChuKyThanhToanUSD) ?? 0,
            //                                //Sau thue
            //                                ChuKyMin = chuky.Min,
            //                                ChuKyMax = chuky.Max
            //                            };
            //                            db.thueCongNos.InsertOnSubmit(objcongno);
            //                        }
            //                        else
            //                        {
            //                            thueCongNo objcongno = new thueCongNo()
            //                            {
            //                                MaHD = objHD.MaHD,
            //                                DaThanhToan = 0,
            //                                ConNo = objHD.ThanhTienUSD * objHD.ChuKyThanhToanUSD ?? 0,
            //                                //Sau thue
            //                                ChuKyMin = chuky.Min,
            //                                ChuKyMax = chuky.Max
            //                            };
            //                            db.thueCongNos.InsertOnSubmit(objcongno);
            //                        }
            //                    }
            //                    if (objLS.Count() < lstchuky.Count)
            //                    {
            //                        cnLichSu objcnls = new cnLichSu()
            //                        {
            //                            MaMB = objHD.MaMB,
            //                            MaKH = objHD.MaKH,
            //                            SoTien = objHD.ThanhTienUSD * objHD.ChuKyThanhToanUSD ?? 0,
            //                            MaLDV = 5,
            //                            DaThu = 0,
            //                            NoTruoc = i * objHD.ThanhTienUSD * objHD.ChuKyThanhToanUSD,
            //                            IsPay = false,
            //                            NgayNhap = chuky.Max
            //                        };
            //                        db.cnLichSus.InsertOnSubmit(objcnls);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                foreach (thueCongNo item in objcn)
            //                {
            //                    item.DaThanhToan = 0;
            //                    item.ConNo = objHD.ThanhTienUSD / (objHD.ThoiHan ?? 1);
            //                }
            //            }

            //        }
            //    }

            //    try
            //    {
            //        db.SubmitChanges();

            //        db.thueCongNo_resetDaThuSingle(objHD.MaMB, objHD.MaHD);
            //        DialogBox.Alert("Đã phát sinh xong công nợ.");
            //    }
            //    catch (Exception ex)
            //    {
            //        DialogBox.Error(ex.Message);
            //        this.Close();
            //    }
            //    #endregion
            //}
            #endregion

            #region Code2
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hợp đồng], xin cảm ơn.");
                return;
            }
            var HDTT = (int?)grvHopDong.GetFocusedRowCellValue("MaTT");
            if (HDTT != 4)
            {
                DialogBox.Alert("Bạn chỉ có thể phát sinh công nợ với các hợp đồng đã bàn giao mặt bằng!");
                return;
            }

            thueHopDong objHD = db.thueHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD"));

            if (DialogBox.Question("Bạn có muốn hình thành công nợ cho hợp đồng này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                decimal? GiaThue = 0;
                int? ChuKyThanhToan = 0;
                #region Add vào công nợ
                var congno = db.thueCongNos.FirstOrDefault(p => p.MaHD == objHD.MaHD);
                if (congno != null)
                {
                    DialogBox.Alert("Hợp đồng này đã phát sinh công nợ!");
                    return;
                }
                else
                {
                    Library.CongNoCls.HopDongThue objhopdongthue = new Library.CongNoCls.HopDongThue();
                    ChuKyCls objchuky = new ChuKyCls();
                    List<ChuKyCls> lstchuky = new List<ChuKyCls>();
                    objchuky = objhopdongthue.LayChuKyTheoThoiDiemHienTai(objHD);
                    lstchuky = objhopdongthue.DanhSachChuKyThanhToan(objHD);
                    var objLS = db.cnLichSus.Where(p => p.MaLDV == 5 && p.MaMB == objHD.MaMB);
                    GiaThue = objHD.MaTG == 1 ? objHD.ThanhTien : objHD.ThanhTienUSD;
                  //  ChuKyThanhToan = objHD.MaTG == 1 ? objHD.ChuKyThanhToan : objHD.ChuKyThanhToanUSD; Phuong an xu ly thuc te
                    ChuKyThanhToan = 1;

                    for (int i = 0; i < lstchuky.Count; i++)
                    {
                        var chuky = lstchuky[i];
                        if (i != lstchuky.Count - 1)
                        {
                            thueCongNo objcongno = new thueCongNo()
                            {
                                MaHD = objHD.MaHD,
                                DaThanhToan = 0,
                                ConNo = GiaThue * ChuKyThanhToan,
                                ChuKyMin = chuky.Min,
                                ChuKyMax = chuky.Max
                            };
                            db.thueCongNos.InsertOnSubmit(objcongno);

                            cnLichSu objcnls = new cnLichSu()
                            {
                                MaMB = objHD.MaMB,
                                MaKH = objHD.MaKH,
                                SoTien = GiaThue * ChuKyThanhToan,
                                MaLDV = 5,
                                DaThu = 0,
                                MaHDT = objHD.MaHD,
                                NoTruoc = i * GiaThue * ChuKyThanhToan,
                                IsPay = false,
                                NgayNhap = chuky.Max
                            };
                            db.cnLichSus.InsertOnSubmit(objcnls);
                        }
                        else
                        {
                            cnLichSu objcnls = new cnLichSu()
                            {
                                MaMB = objHD.MaMB,
                                MaKH = objHD.MaKH,
                                SoTien = GiaThue * ((objHD.ThoiHan % ChuKyThanhToan) == 0 ? ChuKyThanhToan : (objHD.ThoiHan % ChuKyThanhToan)),
                                MaLDV = 5,
                                DaThu = 0,
                                MaHDT = objHD.MaHD,
                                NoTruoc = i * GiaThue * ChuKyThanhToan,
                                IsPay = false,
                                NgayNhap = chuky.Max
                            };
                            db.cnLichSus.InsertOnSubmit(objcnls);


                            thueCongNo objcongno = new thueCongNo()
                            {
                                MaHD = objHD.MaHD,
                                DaThanhToan = 0,
                                ConNo = GiaThue * ((objHD.ThoiHan % ChuKyThanhToan) == 0 ? ChuKyThanhToan : (objHD.ThoiHan % ChuKyThanhToan)),
                                //Sau thue
                                ChuKyMin = chuky.Min,
                                ChuKyMax = chuky.Max
                            };
                            db.thueCongNos.InsertOnSubmit(objcongno);
                        }
                    }

                }
                try
                {
                    db.SubmitChanges();

                    db.thueCongNo_resetDaThuSingle(objHD.MaMB, objHD.MaHD);
                    DialogBox.Alert("Đã phát sinh xong công nợ.");
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                    this.Close();
                }
                #endregion
            }
            #endregion
        }

        decimal TinhCongNo(thueHopDong objHD, DateTime NgayTCN)
        {
            decimal ConNo = 0;
            decimal solan = 0;
            var objPL = objHD.thuePhuLucs.OrderBy(p=>p.NgayPL);
            solan = (decimal)(objHD.ThoiHan / objHD.ChuKyThanhToan);
            if (solan <= 0)
                return 0;

            List<thuePhuLuc> listPL = new List<thuePhuLuc>();
            var temp = objHD.ThanhTien ?? 0;
            foreach (var p in objPL)
            {
                if (p.GiaThue != temp)
                {
                    listPL.Add(p);
                    temp = p.GiaThue.GetValueOrDefault();
                }
            }
            //tinhs soos laanf thanh toans 
            int dotTT = 0;
            for (int i = listPL.Count - 1; i >= 0; i++)
            {
                if (SqlMethods.DateDiffMonth(listPL[i].NgayPL, NgayTCN) >= 0)
                {
                    dotTT = i;
                    break;
                }
            }

            return ConNo;
        }

        private void btnHuyHopDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hợp đồng], xin cảm ơn.");
                return;
            }

            if (DialogBox.Question("Bạn có muốn hủy hợp đồng này không? Việc này sẽ hủy những công nợ chưa thanh toán của hợp đồng này!") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    thueHopDong objHD = db.thueHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD"));
                    if (objHD.MaTT == 6)
                    {
                        DialogBox.Alert("[Hợp đồng] này đã hủy rồi. Vui lòng kiểm tra lại, xin cảm ơn.");
                        return;
                    }

                    var conNo = db.cnCongNos.Where(p => p.MaMB == objHD.MaMB & !p.IsLock.GetValueOrDefault()).Select(p => new { ConNo = p.PhaiThu - p.DaThu }).Sum(p => p.ConNo) ?? 0;
                    if (conNo > 0)
                    {
                        if (DialogBox.Question("[Hợp đồng] này còn công nợ chưa thanh toán!\r\nNếu tiếp tục hủy thì hệ thống sẽ khóa công nợ của hợp đồng này!!\r\nBạn có muốn tiếp tục hủy hợp đồng không?") != System.Windows.Forms.DialogResult.Yes)
                        {
                            return;
                        }
                    }

                    db.cnCongNo_lock(objHD.MaMB);

                    var objLS = new thueLichSu();
                    objLS.MaNV = objnhanvien.MaNV;
                    objLS.MaTT = 6;
                    objLS.MaHD = objHD.MaHD;
                    objLS.NgayTao = db.GetSystemDate();
                    objLS.DienGiai = "Hủy hợp đồng";
                    db.thueLichSus.InsertOnSubmit(objLS);

                    objHD.MaTT = 6;
                    db.SubmitChanges();

                    DialogBox.Alert("Đã hủy hợp đồng, công nợ và các dịch vụ liên quan.");
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
            }

            #region Old code
            //if (DialogBox.Question("Bạn có muốn hủy hợp đồng này không? Việc này sẽ hủy những công nợ chưa thanh toán của hợp đồng này!") == System.Windows.Forms.DialogResult.Yes)
            //{
            //    thueHopDong objHD = db.thueHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD"));

            //    if (db.thueCongNos.Where(p => p.thueHopDong == objHD & p.ConNo > 0).Count() > 0)
            //    {
            //        if (DialogBox.Question("Hợp đồng còn công nợ chưa thanh toán! Nếu tiếp tục hủy, thì toàn bộ công nợ (Đã thanh toán và chưa thanh toán) sẽ bị hủy hết!!\r\nCó tiếp tục hủy hợp đồng không?") != System.Windows.Forms.DialogResult.Yes)
            //        {
            //            return;
            //        }
            //    }

            //    var huydien = db.dvdnDiens.Where(p => p.MaMB == objHD.MaMB); db.dvdnDiens.DeleteAllOnSubmit(huydien);
            //    var huynuoc = db.dvdnNuocs.Where(p => p.MaMB == objHD.MaMB); db.dvdnNuocs.DeleteAllOnSubmit(huynuoc);
            //    var huythangmay = db.dvtmTheThangMays.Where(p => p.MaMB == objHD.MaMB); db.dvtmTheThangMays.DeleteAllOnSubmit(huythangmay);
            //    var huythexe = db.dvgxTheXes.Where(p => p.MaMB == objHD.MaMB); db.dvgxTheXes.DeleteAllOnSubmit(huythexe);
            //    var huypql = db.PhiQuanLies.Where(p => p.MaMB == objHD.MaMB); db.PhiQuanLies.DeleteAllOnSubmit(huypql);

            //    var congnoHuy = db.thueCongNos.Where(p => p.thueHopDong == objHD);
            //    db.thueCongNos.DeleteAllOnSubmit(congnoHuy);
            //    try
            //    {
            //        mbMatBang objmb = db.mbMatBangs.Single(p => p.MaMB == (int)grvHopDong.GetFocusedRowCellValue("MaMB"));

            //        frmThietLapTrangThaiMatBang frmthietlap = new frmThietLapTrangThaiMatBang();
            //        while (frmthietlap.objTrangThai == null)
            //        {
            //            DialogBox.Alert("Vui lòng chọn trạng thái");
            //            frmthietlap.ShowDialog();
            //        }
            //        objmb.MaTT = frmthietlap.objTrangThai.MaTT;
            //        objmb.MaKH = null;
            //        objHD.MaTT = 6; //Tinh trang da huy

            //        db.SubmitChanges();
            //        DialogBox.Alert("Đã hủy hợp đồng, công nợ và các dịch vụ liên quan.");
            //    }
            //    catch (Exception ex)
            //    {
            //        DialogBox.Error(ex.Message);
            //    }
            //}
            #endregion
        }

        private void tabChiTietHopDong_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Clicks();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemHDTSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new Setting.frmManager();
            f.objnhanvien = objnhanvien;
            f.ShowDialog();
        }

        private void itemHDTGiaHan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn hợp đồng");
                return;
            }

            using (PhuLuc.frmEdit frm = new PhuLuc.frmEdit() { objnhanvien = objnhanvien, MaHD = (int)grvHopDong.GetFocusedRowCellValue("MaHD") })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                  LoadData();
            }
        }

        private void popLTTLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã lưu thành công!");
            }
            catch(Exception ex)
            {
                DialogBox.Error("Dữ liệu không thể lưu!");
            }
        }

        private void popLTTXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTienCoc.FocusedRowHandle >= 0)
                grvTienCoc.DeleteSelectedRows();
        }

        private void grvTienCoc_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            if (grvHopDong.FocusedRowHandle < 0)
                return;
            MaHD = (int)grvHopDong.GetFocusedRowCellValue("MaHD");
            grvTienCoc.SetFocusedRowCellValue(colMaHDLTT, MaHD);
        }

        private void itemCNPQL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region Code2
            if (grvHopDong.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hợp đồng], xin cảm ơn.");
                return;
            }
            var HDTT = (int?)grvHopDong.GetFocusedRowCellValue("MaTT");
            if (HDTT != 4)
            {
                DialogBox.Alert("Bạn chỉ có thể phát sinh công nợ với các hợp đồng đã bàn giao mặt bằng!");
                return;
            }

            thueHopDong objHD = db.thueHopDongs.Single(p => p.MaHD == (int)grvHopDong.GetFocusedRowCellValue("MaHD"));

            if (DialogBox.Question("Bạn có muốn hình thành công nợ PQL cho hợp đồng này không?") == System.Windows.Forms.DialogResult.Yes)
            {
                decimal? GiaThue = 0;
                int? ChuKyThanhToan = 0;
                #region Add vào công nợ
                var congno = db.cnLichSus.FirstOrDefault(p => p.MaMB == objHD.MaMB && p.MaKH == objHD.MaKH && p.MaHDT == objHD.MaHD && p.MaLDV == 12);
                if (congno != null)
                {
                    DialogBox.Alert("Hợp đồng này đã phát sinh công nợ PQL!");
                    return;
                }
                else
                {
                    Library.CongNoCls.HopDongThue objhopdongthue = new Library.CongNoCls.HopDongThue();
                    ChuKyCls objchuky = new ChuKyCls();
                    List<ChuKyCls> lstchuky = new List<ChuKyCls>();
                    objchuky = objhopdongthue.LayChuKyTheoThoiDiemHienTai(objHD);
                    lstchuky = objhopdongthue.DanhSachChuKyThanhToan(objHD);
                   // ChuKyThanhToan = objHD.MaTG == 1 ? objHD.ChuKyThanhToan : objHD.ChuKyThanhToanUSD; Note lai
                    ChuKyThanhToan = 1;

                    for (int i = 0; i < lstchuky.Count; i++)
                    {
                        var chuky = lstchuky[i];
                        if (i != lstchuky.Count - 1)
                        {
                            cnLichSu objcnls = new cnLichSu()
                            {
                                MaMB = objHD.MaMB,
                                MaKH = objHD.MaKH,
                                SoTien = objHD.PhiQL * ChuKyThanhToan,
                                MaLDV = 12,
                                DaThu = 0,
                                MaHDT = objHD.MaHD,
                                NoTruoc = 0,
                                IsPay = false,
                                NgayNhap = chuky.Max
                            };
                            db.cnLichSus.InsertOnSubmit(objcnls);
                        }
                        else
                        {
                            cnLichSu objcnls = new cnLichSu()
                            {
                                MaMB = objHD.MaMB,
                                MaKH = objHD.MaKH,
                                SoTien = objHD.PhiQL * ((objHD.ThoiHan % ChuKyThanhToan) == 0 ? ChuKyThanhToan : (objHD.ThoiHan % ChuKyThanhToan)),
                                MaLDV = 12,
                                DaThu = 0,
                                MaHDT = objHD.MaHD,
                                NoTruoc = 0,
                                IsPay = false,
                                NgayNhap = chuky.Max
                            };
                            db.cnLichSus.InsertOnSubmit(objcnls);
                        }
                    }

                }
                try
                {
                    db.SubmitChanges();
                    DialogBox.Alert("Đã phát sinh xong công nợ PQL.");
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                    this.Close();
                }
                #endregion
            }
            #endregion
        }

        private void btnThanhToanCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTienCoc.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn lịch thanh toán để tiến hành.");
                return;
            }
            int? MaDotTT = (int?)grvTienCoc.GetFocusedRowCellValue("ID");
            using (var frm = new frmThanhToanCoc() { objnhanvien = objnhanvien, MaDotTT = MaDotTT })
            {
                frm.ShowDialog();
                LoadData();
            }
        }
    }
}