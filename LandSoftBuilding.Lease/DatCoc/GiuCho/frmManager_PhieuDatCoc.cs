using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Library;
using DevExpress.XtraTab;
using System.Diagnostics;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Collections.Generic;
using LandSoftBuilding.Lease.GanHetHan;
using LandSoftBuilding.Fund.Input;
using DevExpress.XtraReports.UI;

namespace LandSoftBuilding.Lease
{
    public partial class frmManager_PhieuDatCoc : DevExpress.XtraEditors.XtraForm
    {
        public frmManager_PhieuDatCoc()
        {
            InitializeComponent();
        }

        private void frmHopDong_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lookUpToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.TowerList.First().MaTN;
            gvHopDong.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            loadMatBang();
            LoadData();
        }

        private void loadMatBang()
        {
            using (var db = new MasterDataContext())
            {
                //Load mat bang
                glMatBang.DataSource = (from mb in db.mbMatBangs
                                        join l in db.mbTangLaus on mb.MaTL equals l.MaTL
                                        join k in db.mbKhoiNhas on l.MaKN equals k.MaKN
                                        where k.MaTN == Convert.ToInt32(itemToaNha.EditValue)
                                        orderby mb.MaSoMB
                                        select new { mb.MaMB, mb.MaSoMB, l.TenTL, k.TenKN, mb.DienTich }).ToList();
            }
        }

        private void LoadData()
        {
            using (var db = new MasterDataContext())
            {
                var sql = (from pdc in db.PhieuDatCoc_GiuChos
                           join kh in db.tnKhachHangs on pdc.MaKH equals kh.MaKH
                           join nvn in db.tnNhanViens on pdc.MaNVNhap equals nvn.MaNV
                           join nvs in db.tnNhanViens on pdc.MaNVSua equals nvs.MaNV into viewNhanVienSua
                           from nvs in viewNhanVienSua.DefaultIfEmpty()
                           join nvsale in db.tnNhanViens on pdc.MaNVSale equals nvsale.MaNV into viewNhanVienSale
                           from nvsale in viewNhanVienSale.DefaultIfEmpty()
                           join tt in db.PhieuDatCoc_GiuCho_TrangThais on pdc.MaTT equals tt.ID
                           join lt in db.LoaiTiens on pdc.MaLT equals lt.ID
                           where
                                 pdc.MaTN == (byte?)itemToaNha.EditValue
                                 & (
                                    pdc.MaTT == 1 // Chờ duyệt
                                    || pdc.MaTT == 2 // Đã duyệt
                                    || pdc.MaTT == 3 // Đã thu cọc
                                 )
                           select new
                           {
                               pdc.ID,
                               pdc.SoCT,
                               tt.TrangThai,
                               NVSale = nvsale.HoTenNV,
                               TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                               pdc.NgayDatCoc,
                               pdc.ThoiHanCoc,
                               pdc.TienDatCoc,
                               pdc.NgayHHCoc,
                               pdc.DienGiai,
                               pdc.LinhVucHoatDong,
                               TenNVN = nvn.HoTenNV,
                               pdc.NgayNhap,
                               TenNVS = nvs.HoTenNV,
                               pdc.NgaySua,
                               pdc.MaTT,
                               pdc.MaKH,
                               pdc.TyGiaHD,
                               lt.KyHieuLT
                           }).OrderByDescending(p => p.NgayDatCoc).ToList();
                gcHopDong.DataSource = sql;
                gvHopDong.FocusedRowHandle = -1;
            }    
        }

        private void Add()
        {
            using (frmEdit_PhieuDatCoc frm = new frmEdit_PhieuDatCoc { MaTN = (byte?)itemToaNha.EditValue })
            {
                frm.ShowDialog();
                LoadData();
            }
        }

        private void Edit()
        {
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");

            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [phiếu đặt cọc], xin cảm ơn.");
                return;
            }

            using (var dbo = new MasterDataContext())
            {
                var objcheck = dbo.PhieuDatCoc_GiuChos.FirstOrDefault(p => p.ID == id);
                if (objcheck != null)
                {
                    if (objcheck.MaTT != 1)
                    {
                        DialogBox.Alert("Phiếu đặt cọc này đã duyệt. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                        return;
                    }
                }
            }

            using (frmEdit_PhieuDatCoc frm = new frmEdit_PhieuDatCoc() { MaTN = (byte?)itemToaNha.EditValue, ID = id })
            {
                frm.ShowDialog();
                LoadData();
            }
        }

        private void Delete()
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var ID = gvHopDong.GetFocusedRowCellValue("ID");
            if (ID == null) return;

            var db = new MasterDataContext();
            try
            {
                using (var dbo = new MasterDataContext())
                {
                    var objcheck = dbo.PhieuDatCoc_GiuChos.FirstOrDefault(p => p.ID == Convert.ToInt32(ID));
                    if (objcheck != null)
                    {
                        if (objcheck.MaTT != 1)
                        {
                            DialogBox.Alert("Phiếu đặt cọc này đã duyệt. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                            return;
                        }
                    }
                }

                var objpdc_ct = db.PhieuDatCoc_GiuCho_ChiTiets.Where(p => p.ID_PhieuDatCoc_GiuCho == Convert.ToInt32(ID));
                db.PhieuDatCoc_GiuCho_ChiTiets.DeleteAllOnSubmit(objpdc_ct);

                db.Log_CapNhatTrangThai_PDCs.DeleteAllOnSubmit(db.Log_CapNhatTrangThai_PDCs.Where(_ => _.ID_PhieuDatCoc_GiuCho == Convert.ToInt32(ID)));

                var objHD = db.PhieuDatCoc_GiuChos.FirstOrDefault(p => p.ID == Convert.ToInt32(ID));
                if(objHD != null)
                {
                    db.PhieuDatCoc_GiuChos.DeleteOnSubmit(objHD);
                    db.SubmitChanges();
                }

                LoadData();
            }
            catch(Exception ex)
            {
                DialogBox.Error("Phiếu đặt cọc đã được duyệt hoặc đã có phiếu cọc, cần liên hệ quản trị để xóa");
            }
            finally
            {
                db.Dispose();
            }
        }

        private void Detail()
        {
            var db = new MasterDataContext();
            var id = (int?) gvHopDong.GetFocusedRowCellValue("ID");
            if (id != null)
            {
                if (id == null)
                {
                    switch (tabMain.SelectedTabPageIndex)
                    {
                        case 0:
                            gcChiTiet.DataSource = null;
                            break;
                        case 1:
                            gcLichSuCapNhat.DataSource = null;
                            break;
                        case 2:
                            gcPhieuThu.DataSource = null;
                            break;
                        case 3:
                            gcPhieuChi.DataSource = null;
                            break;
                        case 4:
                            ctlTaiLieu1.TaiLieu_Load();
                            ctlTaiLieu1.MaNV = null;
                            ctlTaiLieu1.objNV = null;
                            break;
                    }
                    return;
                }

                switch (tabMain.SelectedTabPageIndex)
                {
                    case 0:
                        gcChiTiet.DataSource = (from ct in db.PhieuDatCoc_GiuCho_ChiTiets
                                                join mb in db.mbMatBangs on ct.MaMB equals mb.MaMB
                                                join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB
                                                where ct.ID_PhieuDatCoc_GiuCho == id
                                                orderby mb.MaSoMB
                                                select new
                                                {
                                                    lmb.TenLMB,
                                                    mb.MaMB,
                                                    mb.MaSoMB,
                                                    ct.TenTangLau,
                                                    ct.ID,
                                                    ct.DienTich,
                                                    ct.DonGia,
                                                    ct.ThanhTien,
                                                    ct.Commission,
                                                    ct.TienCoc
                                                }).ToList();
                        break;
                    case 1:
                        loadLichSuCapNhat(id);
                        break;
                    case 2:
                        loadPhieuThu(id);
                        break;
                    case 3:
                        loadPhieuChi(id);
                        break;
                    case 4:
                        ctlTaiLieu1.FormID = 3204;
                        ctlTaiLieu1.LinkID = id;
                        ctlTaiLieu1.MaNV = Common.User.MaNV;
                        ctlTaiLieu1.objNV = Common.User;
                        ctlTaiLieu1.TaiLieu_Load();
                        break;
                }
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Add();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            

            this.Edit();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Delete();
        }

        private void gvHopDong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Detail();
        }

        private void loadPhieuThu(int? ID)
        {
            int MaPT = 0;
            using (var dbo = new MasterDataContext())
            {
                var objMaPT = dbo.PhieuDatCoc_GiuChos.FirstOrDefault(p=>p.ID == ID);
                if(objMaPT != null)
                {
                    MaPT = objMaPT.MaPT.GetValueOrDefault();
                }
            }
            if (MaPT <= 0) return;

            using (var db = new MasterDataContext())
            {
                var objData = from p in db.ptPhieuThus
                              join mb in db.mbMatBangs on p.MaMB equals mb.MaMB into mbang
                              from mb in mbang.DefaultIfEmpty()
                              join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tlau
                              from tl in tlau.DefaultIfEmpty()
                              join pl in db.ptPhanLoais on p.MaPL equals (int?)pl.ID into tblPhanLoai
                              from pl in tblPhanLoai.DefaultIfEmpty()
                              join k in db.tnKhachHangs on p.MaKH equals (int?)k.MaKH into tblKhachHang
                              from k in tblKhachHang.DefaultIfEmpty()
                              join nkh in db.khNhomKhachHangs on k.MaNKH equals (int?)nkh.ID into tblNhomKhachHang
                              from nkh in tblNhomKhachHang.DefaultIfEmpty()
                              join nv in db.tnNhanViens on p.MaNV equals (int?)nv.MaNV into nvchinh
                              from nv in nvchinh.DefaultIfEmpty()
                              join nvn in db.tnNhanViens on p.MaNVN equals (int?)nvn.MaNV into nvnhap
                              from nvn in nvnhap.DefaultIfEmpty()
                              join nvs in db.tnNhanViens on p.MaNVS equals (int?)nvs.MaNV into tblNguoiSua
                              from nvs in tblNguoiSua.DefaultIfEmpty()
                              join tk in db.nhTaiKhoans on p.MaTKNH equals (int?)tk.ID into tblTaiKhoan
                              from tk in tblTaiKhoan.DefaultIfEmpty()
                              join nh in db.nhNganHangs on tk.MaNH equals (int?)nh.ID into tblNganHang
                              from nh in tblNganHang.DefaultIfEmpty()
                              join nt in db.ptPhieuThu_NguonThanhToans on p.NguonThanhToan equals nt.ID into nguonThanhToan
                              from nt in nguonThanhToan.DefaultIfEmpty()
                              where
                                p.ID == MaPT
                                & (p.IsKhauTru == false | p.IsKhauTru == null) & p.MaPL != 24
                              select new
                              {
                                  ID = p.ID,
                                  SoPT = p.SoPT,
                                  NgayThu = p.NgayThu,
                                  KyHieu = k.KyHieu,
                                  TenKH = ((k.IsCaNhan == (bool?)true) ? (k.HoKH + " " + k.TenKH) : k.CtyTen),
                                  NguoiThu = nv.HoTenNV,
                                  NguoiNop = p.NguoiNop,
                                  DiaChiNN = p.DiaChiNN,
                                  LyDo = p.LyDo,
                                  TenPL = pl.TenPL,
                                  ChungTuGoc = p.ChungTuGoc,
                                  NguoiNhap = nvn.HoTenNV,
                                  NgayNhap = p.NgayNhap,
                                  NguoiSua = nvs.HoTenNV,
                                  NgaySua = p.NgaySua,
                                  PhuongThuc = ((p.MaTKNH != null) ? "Chuyển khoản" : "Tiền mặt"),
                                  SoTK = tk.SoTK,
                                  TenNH = nh.TenNH,
                                  TenNKH = nkh.TenNKH,
                                  TenKN = tl.mbKhoiNha.TenKN,
                                  SoTienThu = p.ptChiTietPhieuThus.Sum(ct => ct.SoTien.GetValueOrDefault()),
                                  TienPhaiThu = p.ptChiTietPhieuThus.Sum(ct => ct.PhaiThu.GetValueOrDefault()),
                                  TienKhauTru = p.ptChiTietPhieuThus.Sum(ct => ct.KhauTru.GetValueOrDefault()),
                                  TienThuThua = p.ptChiTietPhieuThus.Sum(ct => ct.ThuThua.GetValueOrDefault()),
                                  NguonThu = nt != null ? nt.Name : "",
                                  p.PhieuChiId
                              } into p
                              select new
                              {
                                  ID = p.ID,
                                  SoPT = p.SoPT,
                                  NgayThu = p.NgayThu, //p.NgayThu.Value.Hour.ToString().PadLeft(2, '0') + ":" + p.NgayThu.Value.Minute.ToString().PadLeft(2, '0') + " | " + p.NgayThu.Value.Day.ToString().PadLeft(2, '0') + "/" + p.NgayThu.Value.Month.ToString().PadLeft(2, '0') + "/" + p.NgayThu.Value.Year.ToString(),
                                  GioThu = p.NgayThu.Value.Hour.ToString().PadLeft(2, '0') + ":" + p.NgayThu.Value.Minute.ToString().PadLeft(2, '0'),
                                  KyHieu = p.KyHieu,
                                  TenKH = p.TenKH,
                                  NguoiThu = p.NguoiThu,
                                  NguoiNop = p.NguoiNop,
                                  DiaChiNN = p.DiaChiNN,
                                  LyDo = p.LyDo,
                                  TenPL = p.TenPL,
                                  ChungTuGoc = p.ChungTuGoc,
                                  NguoiNhap = p.NguoiNhap,
                                  NgayNhap = p.NgayNhap,
                                  NguoiSua = p.NguoiSua,
                                  NgaySua = p.NgaySua,
                                  PhuongThuc = p.PhuongThuc,
                                  SoTK = p.SoTK,
                                  TenKN = p.TenKN,
                                  TenNH = p.TenNH,
                                  TenNKH = p.TenNKH,
                                  p.TienPhaiThu,
                                  p.TienKhauTru,
                                  p.TienThuThua,
                                  p.SoTienThu,
                                  p.NguonThu,
                                  p.PhieuChiId
                                  ,
                                  ChuaLuuSoQuy = db.SoQuy_ThuChis.FirstOrDefault(_ => _.IDPhieu == p.ID) != null ? false : true
                              };
                gcPhieuThu.DataSource = objData;
            }
        }

        private void loadPhieuChi(int? ID)
        {
            int MaPC = 0;
            using (var dbo = new MasterDataContext())
            {
                var objMaPT = dbo.PhieuDatCoc_GiuChos.FirstOrDefault(p => p.ID == ID);
                if (objMaPT != null)
                {
                    MaPC = objMaPT.MaPC.GetValueOrDefault();
                }
            }
            if (MaPC <= 0) return;

            using (var db = new MasterDataContext())
            {
                // chi cho khách hàng
                var list = (from p in db.pcPhieuChis
                            join pl in db.pcPhanLoais on p.MaPhanLoai equals pl.ID into tblPhanLoai
                            from pl in tblPhanLoai.DefaultIfEmpty()
                            join k in db.tnKhachHangs on p.MaNCC equals k.MaKH
                            join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                            join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                            join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                            from nvs in tblNguoiSua.DefaultIfEmpty()
                            where 
                                p.ID == MaPC &&
                                p.OutputTyleId == 3
                            select new
                            {
                                p.ID,
                                p.SoPC,
                                p.NgayChi,
                                p.SoTien,
                                TenKH = k.IsCaNhan == true ? String.Format("{0} {1}", k.HoKH, k.TenKH) : k.CtyTen,
                                NguoiChi = nv.HoTenNV,
                                p.NguoiNhan,
                                p.DiaChiNN,
                                p.LyDo,
                                pl.TenPL,
                                p.ChungTuGoc,
                                NguoiNhap = nvn.HoTenNV,
                                p.NgayNhap,
                                NguoiSua = nvs.HoTenNV,
                                p.NgaySua,
                                p.OutputTyleName,
                                p.HinhThucChiName,
                                p.HinhThucChiId,
                                p.TuMatBangNo,
                                p.PhieuThuId
                            }).ToList();
                gcPhieuChi.DataSource = list;
            }
        }

        private void loadLichSuCapNhat(int? ID)
        {
            using (var db = new MasterDataContext())
            {
                var sql = (from pdc in db.Log_CapNhatTrangThai_PDCs
                           join nvn in db.tnNhanViens on pdc.MaNV equals nvn.MaNV
                           join ttc in db.PhieuDatCoc_GiuCho_TrangThais on pdc.MaTrangThaiCu equals ttc.ID into trangthaicu
                           from ttc in trangthaicu.DefaultIfEmpty()
                           join ttm in db.PhieuDatCoc_GiuCho_TrangThais on pdc.MaTrangThaiMoi equals ttm.ID into trangthaimoi
                           from ttm in trangthaimoi.DefaultIfEmpty()
                           where
                                 pdc.ID_PhieuDatCoc_GiuCho == ID
                           select new
                           {
                               pdc.ID,
                               TenNVN = nvn.HoTenNV,
                               NgayNhap = pdc.NgayCN,
                               TrangThaiCu = ttc.TrangThai,
                               TrangThaiMoi = ttm.TrangThai,
                               pdc.GhiChu,
                           }).OrderByDescending(p => p.NgayNhap).ToList();
                gcLichSuCapNhat.DataSource = sql;
            }
        }

        private void itemCapNhatTrangThai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = gvHopDong.GetFocusedRowCellValue("ID");
            if(id != null)
            {
                var id_TrangThai = Convert.ToInt32(gvHopDong.GetFocusedRowCellValue("MaTT"));
                id_TrangThai = 2;
                using (frmCapNhatTrangThai frm = new frmCapNhatTrangThai(Convert.ToInt32(id), id_TrangThai))
                {
                    frm.ShowDialog();
                    LoadData();
                }
            }
        }

        private void itemTaoPhieuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var objHD = gvHopDong.GetFocusedRowCellValue("ID");
            if (objHD == null) return;
            var phieuDatCocChiTiets = new PhieuDatCoc_GiuCho_ChiTiet();
            using (var dbo = new MasterDataContext())
            {
                var objcheck = dbo.PhieuDatCoc_GiuChos.FirstOrDefault(p=>p.ID == Convert.ToInt32(objHD));
                if(objcheck != null)
                {
                    if (objcheck.MaTT == 1)
                    {
                        DialogBox.Alert("Phiếu đặt cọc này chưa được duyệt. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                        return;
                    }

                    if (objcheck.MaPT.GetValueOrDefault() > 0)
                    {
                        DialogBox.Alert("Phiếu đặt cọc này đã phát sinh phiếu thu. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                        return;
                    }
                    phieuDatCocChiTiets = objcheck.PhieuDatCoc_GiuCho_ChiTiets.FirstOrDefault();
                }
            }


            using (var frm = new LandSoftBuilding.Fund.Input.frmEdit())
            {
                frm.MaMB = phieuDatCocChiTiets != null ? phieuDatCocChiTiets.MaMB : null;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.IsThuCocGiuCho = true;
                frm.Id_PDC = Convert.ToInt32(objHD);
                frm.MaKH = Convert.ToInt32(gvHopDong.GetFocusedRowCellValue("MaKH"));
                frm.SoTien = Convert.ToDecimal(gvHopDong.GetFocusedRowCellValue("TienDatCoc"));
                frm.ShowDialog();
                LoadData();
            }
        }

        private void tabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            Detail();
        }

        private void itemTaoPhieuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var objHD = gvHopDong.GetFocusedRowCellValue("ID");
            if (objHD == null) return;

            using (var dbo = new MasterDataContext())
            {
                var objcheck = dbo.PhieuDatCoc_GiuChos.FirstOrDefault(p => p.ID == Convert.ToInt32(objHD));
                if (objcheck != null)
                {
                    if (objcheck.MaTT != 3)
                    {
                        DialogBox.Alert("Phiếu đặt cọc này chưa phát sinh phiếu thu. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                        return;
                    }

                    if (objcheck.MaPC.GetValueOrDefault() > 0)
                    {
                        DialogBox.Alert("Phiếu đặt cọc này đã phát sinh phiếu chi. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                        return;
                    }
                }
            }

            //using (var frm = new LandSoftBuilding.Fund.Output.frmEdit())
            //{
            //    frm.MaTN = (byte)itemToaNha.EditValue;
            //    frm.IsChiCocGiuCho = true;
            //    frm.Id_PDC = Convert.ToInt32(objHD);
            //    frm.MaKH = Convert.ToInt32(gvHopDong.GetFocusedRowCellValue("MaKH"));
            //    frm.SoTien = Convert.ToDecimal(gvHopDong.GetFocusedRowCellValue("TienDatCoc"));
            //    frm.ShowDialog();
            //    LoadData();
            //}
        }

        private void gvHopDong_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (gvHopDong.GetRowCellValue(e.RowHandle, "NgayHHCoc") != null)
            {
                int MaTT = Convert.ToInt32(gvHopDong.GetRowCellValue(e.RowHandle, "MaTT"));
                var iNum = SqlMethods.DateDiffDay(DateTime.Now, Convert.ToDateTime(gvHopDong.GetRowCellValue(e.RowHandle, "NgayHHCoc")));
                if(iNum <= 10 && (MaTT == 1 || MaTT == 2))
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.BackColor2 = Color.Aquamarine;
                }
            }
        }

        private void itemKhongDuyetPhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = gvHopDong.GetFocusedRowCellValue("ID");
            if (id != null)
            {
                var id_TrangThai = Convert.ToInt32(gvHopDong.GetFocusedRowCellValue("MaTT"));
                id_TrangThai = 1;
                using (frmCapNhatTrangThai frm = new frmCapNhatTrangThai(Convert.ToInt32(id), id_TrangThai))
                {
                    frm.ShowDialog();
                    LoadData();
                }
            }
        }

        private void itemLapHopDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = gvHopDong.GetFocusedRowCellValue("ID");
                if (id != null)
                {
                    using (var dbo = new MasterDataContext())
                    {
                        var objcheck = dbo.PhieuDatCoc_GiuChos.FirstOrDefault(p => p.ID == Convert.ToInt32(id));
                        if (objcheck != null)
                        {
                            if (objcheck.MaTT == 1)
                            {
                                DialogBox.Alert("Phiếu đặt cọc này chưa duyệt. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                                return;
                            }
                        }
                    }

                    using (frmEdit frm = new frmEdit { MaTN = (byte?)itemToaNha.EditValue })
                    {
                        frm.DatCocId = Convert.ToInt32(id);
                        frm.ShowDialog();
                        if (frm.DialogResult == DialogResult.OK)
                        {
                            LoadData();
                        }
                    }
                }
                
            }
            catch
            {
            }
        }

        // Duyệt phiếu
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = gvHopDong.GetFocusedRowCellValue("ID");
            if (id != null)
            {
                var id_TrangThai = Convert.ToInt32(gvHopDong.GetFocusedRowCellValue("MaTT"));
                id_TrangThai = 2;
                using (frmCapNhatTrangThai frm = new frmCapNhatTrangThai(Convert.ToInt32(id), id_TrangThai))
                {
                    frm.ShowDialog();
                    LoadData();
                }
            }
        }

        // Bỏ duyệt phiếu
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = gvHopDong.GetFocusedRowCellValue("ID");
            if (id != null)
            {
                var id_TrangThai = Convert.ToInt32(gvHopDong.GetFocusedRowCellValue("MaTT"));
                id_TrangThai = 1;
                using (frmCapNhatTrangThai frm = new frmCapNhatTrangThai(Convert.ToInt32(id), id_TrangThai))
                {
                    frm.ShowDialog();
                    LoadData();
                }
            }
        }

        // Thanh lý lên hợp đồng
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = gvHopDong.GetFocusedRowCellValue("ID");
                if (id != null)
                {
                    using (var dbo = new MasterDataContext())
                    {
                        var objcheck = dbo.PhieuDatCoc_GiuChos.FirstOrDefault(p => p.ID == Convert.ToInt32(id));
                        if (objcheck != null)
                        {
                            if (objcheck.MaTT == 1)
                            {
                                DialogBox.Alert("Phiếu đặt cọc này chưa duyệt. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                                return;
                            }
                        }
                    }

                    using (frmEdit frm = new frmEdit { MaTN = (byte?)itemToaNha.EditValue })
                    {
                        frm.DatCocId = Convert.ToInt32(id);
                        frm.ShowDialog();
                        if (frm.DialogResult == DialogResult.OK)
                        {
                            LoadData();
                        }
                    }
                }

            }
            catch
            {
            }
        }

        // Thanh lý hoàn trả cọc
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var objHD = gvHopDong.GetFocusedRowCellValue("ID");
            if (objHD == null) return;

            using (var dbo = new MasterDataContext())
            {
                var objcheck = dbo.PhieuDatCoc_GiuChos.FirstOrDefault(p => p.ID == Convert.ToInt32(objHD));
                if (objcheck != null)
                {
                    if (objcheck.MaTT != 3)
                    {
                        DialogBox.Alert("Phiếu đặt cọc này chưa phát sinh phiếu thu. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                        return;
                    }

                    if (objcheck.MaPC.GetValueOrDefault() > 0)
                    {
                        DialogBox.Alert("Phiếu đặt cọc này đã phát sinh phiếu chi. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                        return;
                    }
                }
            }

            //using (var frm = new LandSoftBuilding.Fund.Output.frmEdit())
            //{
            //    frm.MaTN = (byte)itemToaNha.EditValue;
            //    frm.IsChiCocGiuCho = true;
            //    frm.Id_PDC = Convert.ToInt32(objHD);
            //    frm.MaKH = Convert.ToInt32(gvHopDong.GetFocusedRowCellValue("MaKH"));
            //    frm.SoTien = Convert.ToDecimal(gvHopDong.GetFocusedRowCellValue("TienDatCoc"));
            //    frm.ShowDialog();
            //    LoadData();
            //}
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = gvHopDong.GetFocusedRowCellValue("ID");
            if (id != null)
            {
                var id_TrangThai = Convert.ToInt32(gvHopDong.GetFocusedRowCellValue("MaTT"));
                id_TrangThai = 9;
                using (frmCapNhatTrangThai frm = new frmCapNhatTrangThai(Convert.ToInt32(id), id_TrangThai))
                {
                    frm.ShowDialog();
                    LoadData();
                }
            }
        }
    }
}