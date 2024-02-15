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
using DevExpress.XtraReports.UI;
using LandSoftBuilding.Lease.GanHetHan;


namespace LandSoftBuilding.Lease.TrungTin
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public frmManager()
        {
            InitializeComponent();
        }

        #region Ham xu ly
        void LoadData()
        {
            try
            {
                gcHopDong.DataSource = null;
                gcHopDong.DataSource = linqInstantFeedbackSource1;
                //if(Common.User.MaTN)
            }
            catch { }
        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        void Add()
        {
            try
            {
                using (frmEdit frm = new frmEdit { MaTN = (byte?)itemToaNha.EditValue })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch { }
        }

        void Edit()
        {
            try
            {
                var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
                
                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn [hợp đồng], xin cảm ơn.");
                    return;
                }

                using (frmEdit frm = new frmEdit() { MaTN = (byte?)itemToaNha.EditValue, ID = id })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch { }     
        }

        void Delete()
        {
            int[] indexs = gvHopDong.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn những Hợp đồng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (int i in indexs)
                {
                    var objHD = db.ctHopDongs.Single(p => p.ID == (int)gvHopDong.GetRowCellValue(i, "ID"));
                    db.ctHopDongs.DeleteOnSubmit(objHD);
                }

                db.SubmitChanges();

                this.RefreshData();
            }
            catch
            {
                DialogBox.Alert("Lưu dữ liệu không thành công! Ràng buộc dữ liệu không cho phép thực hiện thao tác này hoặc đường truyền không ổn định!");
            }
            finally
            {
                db.Dispose();
            }
        }

        void Detail()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    switch (tabMain.SelectedTabPageIndex)
                    {
                        case 0:
                            gcChiTiet.DataSource = null;
                            break;
                        case 1:
                            gcLTT.DataSource = null;
                            break;
                        case 2:
                            gcSuaChua.DataSource = null;
                            break;
                        case 3:
                            gcTienCoc.DataSource = null;
                            break;
                        case 4:
                            gcPhuLuc.DataSource = null;
                            break;
                        case 5:
                            gcBangGia.DataSource = null;
                            break;
                        case 6:
                            gcLichSuDieuChinh.DataSource = null;
                            break;
                        case 7 :
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
                        gcChiTiet.DataSource = (from ct in db.ctChiTiets
                                                join mb in db.mbMatBangs on ct.MaMB equals mb.MaMB
                                                join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB
                                                join lgt in db.LoaiGiaThues on ct.MaLG equals lgt.ID
                                                where ct.MaHDCT == id
                                                orderby mb.MaSoMB
                                                select new
                                                {
                                                    lmb.TenLMB,
                                                    mb.MaSoMB,
                                                    lgt.TenLG,
                                                    ct.ID,
                                                    ct.DienTich,
                                                    ct.DonGia,
                                                    ct.PhiDichVu,
                                                    ct.TongGiaThue,
                                                    ct.TyLeVAT,
                                                    ct.TienVAT,
                                                    ct.TyLeCK,
                                                    ct.TienCK,
                                                    ct.PhiSuaChua,
                                                    ct.ThanhTien,
                                                    ct.DienGiai
                                                }).ToList();
                        break;
                    case 1:
                        gcLTT.DataSource = (from l in db.ctLichThanhToans
                                            join mb in db.mbMatBangs on l.MaMB equals mb.MaMB
                                            where l.MaHD == id & l.MaLDV == 2
                                            orderby mb.MaSoMB, l.DotTT
                                            select new
                                            {
                                                mb.MaSoMB,
                                                l.DotTT,
                                                l.TuNgay,
                                                l.DenNgay,
                                                l.SoThang,
                                                l.SoTien,
                                                l.SoTienQD,
                                                l.DienGiai
                                            }).ToList();
                        break;
                    case 2:
                        gcSuaChua.DataSource = (from l in db.ctLichThanhToans
                                                join mb in db.mbMatBangs on l.MaMB equals mb.MaMB
                                                where l.MaHD == id & l.MaLDV == 3
                                                orderby mb.MaSoMB, l.DotTT
                                                select new
                                                {
                                                    mb.MaSoMB,
                                                    l.DotTT,
                                                    l.TuNgay,
                                                    l.DenNgay,
                                                    l.SoThang,
                                                    l.SoTien,
                                                    l.SoTienQD
                                                }).ToList();
                        break;
                    case 3:
                        gcTienCoc.DataSource = (from l in db.ctLichThanhToans
                                                where l.MaHD == id & l.MaLDV == 4
                                                orderby l.DotTT
                                                select new
                                                {
                                                    l.DotTT,
                                                    l.TuNgay,
                                                    l.DenNgay,
                                                    l.SoThang,
                                                    l.SoTien,
                                                    l.SoTienQD
                                                }).ToList();
                        break;
                    case 4:
                        gcPhuLuc.DataSource = (from p in db.ctHopDongs
                                               join kh in db.tnKhachHangs on p.MaKH equals kh.MaKH
                                               join lt in db.LoaiTiens on p.MaLT equals lt.ID
                                               join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                               join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into viewNhanVienSua
                                               from nvs in viewNhanVienSua.DefaultIfEmpty()
                                               where p.MaTN == (byte?)itemToaNha.EditValue && p.IsPhuLuc == true && p.ParentID == id
                                               orderby p.NgayKy descending
                                               select new
                                               {
                                                   p.ID,
                                                   p.SoHDCT,
                                                   p.NgayKy,
                                                   p.ThoiHan,
                                                   p.NgayHH,
                                                   p.NgayBG,
                                                   lt.KyHieuLT,
                                                   lt.TyGia,
                                                   p.GiaThue,
                                                   TenKH = kh.IsCaNhan == true ? (kh.HoKH + "" + kh.TenKH) : kh.CtyTen
                                               }).ToList();
                        break;
                    case 5:
                        gcBangGia.DataSource = (from bg in db.ctBangGiaDichVus
                                                join ldv in db.dvLoaiDichVus on bg.MaLDV equals ldv.ID into viewLoaiDV
                                                from ldv in viewLoaiDV.DefaultIfEmpty()
                                                join dvt in db.DonViTinhs on bg.MaDVT equals dvt.ID into viewDVT
                                                from dvt in viewDVT.DefaultIfEmpty()
                                                join lt in db.LoaiTiens on bg.MaLT equals lt.ID into viewLoaiTien
                                                from lt in viewLoaiTien.DefaultIfEmpty()
                                                where bg.MaHD == id
                                                select new
                                                {
                                                    TenLDV = ldv.TenHienThi,
                                                    bg.IsMienPhi,
                                                    bg.DonGia,
                                                    dvt.TenDVT,
                                                    TenLT = lt.KyHieuLT,
                                                    bg.DienGiai
                                                }).ToList();
                        break;
                    case 6:
                        gcLichSuDieuChinh.DataSource = (from dc in db.ctLichSuDieuChinhGias
                                                        join pl in db.ctLoaiDieuChinhs on dc.MaLDC equals pl.ID
                                                        join nv in db.tnNhanViens on dc.MaNVN equals nv.MaNV
                                                        where dc.MaHD == id
                                                        orderby dc.NgayDC descending
                                                        select new
                                                        {
                                                            dc.NgayDC,
                                                            pl.TenPL,
                                                            dc.GiaTriCu,
                                                            dc.TyLeDC,
                                                            dc.GiaTriMoi,
                                                            dc.DienGiai,
                                                            nv.HoTenNV,
                                                            dc.NgayNhap
                                                        }).ToList();
                        break;
                        case 7:
                            ctlTaiLieu1.FormID = 26;
                            ctlTaiLieu1.LinkID = id;
                            ctlTaiLieu1.MaNV = Common.User.MaNV;
                            ctlTaiLieu1.objNV = Common.User;
                            ctlTaiLieu1.TaiLieu_Load();
                        break;
                }
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void ThanhLy()
        {
            try
            {
                var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");

                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn [hợp đồng], xin cảm ơn.");
                    return;
                }

                using (var frm = new Liquidate.frmEdit() { MaTN = (byte?)itemToaNha.EditValue, MaHD = id })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch { }
        }
        #endregion

        #region Event
        private void frmHopDong_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lookUpToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            gvHopDong.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }


        string GetThongBaoTyGia(decimal? TyGiaTT, decimal? TyGiaHD, decimal? TyGiaAD, decimal? MucDCTG)
        {
            if (TyGiaTT != TyGiaAD)
            {
                var TyLeTD = ((TyGiaTT - TyGiaHD) / TyGiaTT).GetValueOrDefault();
                MucDCTG = MucDCTG.GetValueOrDefault();
                if (TyLeTD > MucDCTG)
                {
                    return "Lên giá";
                }
                else if (TyLeTD < -MucDCTG)
                {
                    return "Giảm giá";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var db = new MasterDataContext();

            var sql = from p in db.ctHopDongs
                      join ct in db.ctChiTiets on p.ID equals ct.MaHDCT into chitiethd
                      from ct in chitiethd.DefaultIfEmpty()
                      join mb in db.mbMatBangs on ct.MaMB equals mb.MaMB into matbang
                      from mb in matbang.DefaultIfEmpty()
                      join kh in db.tnKhachHangs on p.MaKH equals kh.MaKH
                      join lt in db.LoaiTiens on p.MaLT equals lt.ID
                      join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                      join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into viewNhanVienSua
                      from nvs in viewNhanVienSua.DefaultIfEmpty()
                      where p.MaTN == (byte?)itemToaNha.EditValue 
                      orderby p.NgayKy descending
                      select new
                      {
                          p.ID,
                          p.SoHDCT,
                          p.NgayKy,
                          p.ThoiHan,
                          p.KyTT,
                          p.HanTT,
                          p.NgayHL,
                          p.NgayHH,
                          p.NgayBG,
                          p.GiaThue,
                          p.TienCoc,
                          lt.KyHieuLT,
                          p.TyGia,
                          p.TyGiaHD,
                          TyGiaTT = lt.TyGia,
                          TyLeTDTG = (lt.TyGia - p.TyGiaHD) / lt.TyGia,
                          TBTDTG = GetThongBaoTyGia(lt.TyGia, p.TyGiaHD, p.TyGia, p.MucDCTG),
                          GiaThueQD = p.GiaThue * p.TyGia,
                          TienCocQD = p.TienCoc * p.TyGia,
                          p.TyLeDCGT,
                          p.SoNamDCGT,
                          p.MucDCTG,
                          TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                          p.NgungSuDung,
                          p.MaNVN,
                          TenNVN = nvn.HoTenNV,
                          p.NgayNhap,
                          TenNVS = nvs.HoTenNV,
                          p.NgaySua,
                          mb.MaSoMB,
                          //ct.TongGiaThue,
                          //ct.TienCK,
                          //TienTruocVAT = ct.TongGiaThue + ct.TienCK,
                          //ct.TienVAT,
                          //ct.ThanhTien,
                      };

            e.QueryableSource = sql;

            //if (Common.User.IsSuperAdmin.Value)
            //{
            //    e.QueryableSource = sql;
            //}
            //else
            //{
            //    var GetNhomOfNV = db.pqNhomNhanViens.Where(p => p.IsTruongNhom.Value & p.MaNV == Common.User.MaNV).Select(p => p.GroupID).ToList();
            //    if (GetNhomOfNV.Count > 0)
            //    {
            //        var GetListNV = db.pqNhomNhanViens.Where(p => GetNhomOfNV.Contains(p.GroupID)).Select(p => p.MaNV).ToList();
            //        e.QueryableSource = sql.Where(p => GetListNV.Contains(p.MaNVN.Value));
            //    }
            //    else
            //    {
            //        e.QueryableSource = sql.Where(p => p.MaNVN == Common.User.MaNV);
            //    }
            //}
                
            e.Tag = db;
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
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

        private void gvHopDong_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.Detail(); 
        }
        #endregion

        private void btnExportMB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new DevExpress.XtraGrid.Design.frmDesigner();
            frm.InitGrid(gcHopDong);
            frm.ShowDialog();
        }

        private void tabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            this.Detail();
        }

        private void btnThanhLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ThanhLy();
        }

        private void itemAddDieuChinhGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvChiTiet.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn chi tiết thuê để điều chỉnh giá");
                return;
            }

            frmLSDCGEdit frm = new frmLSDCGEdit();
            frm.MaCT = id;
            frm.ShowDialog();
            if (frm.IsSave == true)
            {
                LoadData();
                Detail();
            }
        }

        private void itemLichSuDieuChinhGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvChiTiet.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn chi tiết thuê để điều chỉnh giá");
                return;
            }
            frmDsDieuChinh frm = new frmDsDieuChinh(id);
            frm.ShowDialog();
        }

        private void itemDieuChinhTyGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn hợp đồng");
                return;
            }

            frmLSDCGEdit frm = new frmLSDCGEdit();
            frm.MaHD = id;
            frm.ShowDialog();
            if (frm.IsSave)
            {
                this.RefreshData();
                this.Detail();
            }
        }

        private void itemHopDongThueGianHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn hợp đồng. Xin cám ơn!");
                return;
            }

            var rpt = new rptHopDongThueGianHang(id);
            rpt.ShowPreviewDialog();
        }

        private void itemHopDongThueKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            byte? MaTN = (byte?)itemToaNha.EditValue;
            if (MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án]. Xin cám ơn!");
                return;
            }
            if (MaTN == 59 | MaTN == 60)
            {
                var frm = new frmImportPhoYen();
                frm.MaTN = MaTN.Value;
                frm.ShowDialog();
                if (frm.isSave)
                    this.LoadData();
            }
            else
            {
                var frm = new frmImport();
                frm.MaTN = MaTN.Value;
                frm.ShowDialog();
                if (frm.isSave)
                    this.LoadData();
            }
            
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcHopDong);
        }

        private void itemInLan1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null) return;
            if (gvChiTiet.RowCount > 0)
            {
                var DateHH = (DateTime?)gvHopDong.GetFocusedRowCellValue("NgayHH");
                var DateHL = (DateTime?)gvHopDong.GetFocusedRowCellValue("NgayHL");
                var DateTruocTT = DateHH.Value.AddDays(-20);
                var objTN = db.tnToaNhas.Where(p => p.MaTN == (byte)itemToaNha.EditValue).First();
                var rpt = new rptInLan1();
                //rpt.TenCT = objTN.CongTyQuanLy;
                rpt.CongTyQL = objTN.CongTyQuanLy;
                rpt.TenKH = gvHopDong.GetFocusedRowCellValue("TenKH").ToString().ToUpper();
                rpt.SoHD = gvHopDong.GetFocusedRowCellValue("SoHDCT").ToString();
                rpt.TenMatBang = gvChiTiet.GetFocusedRowCellValue("MaSoMB").ToString();
                rpt.NgayHT = DateTime.Now.Day.ToString();
                rpt.ThangHT = DateTime.Now.Month.ToString();
                rpt.NamHT = DateTime.Now.Year.ToString();
                rpt.NamHH = DateHH.Value.Year.ToString();
                rpt.ThangHH = DateHH.Value.Month.ToString();
                rpt.DateHL = DateHL.Value.ToString("dd/MM/yyyy");
                rpt.NamHL = DateHL.Value.Year.ToString();
                rpt.DateHH = DateHH.Value.ToString("dd/MM/yyyy");
                rpt.SoTien = ((decimal)(gvChiTiet.GetFocusedRowCellValue("ThanhTien"))).ToString("N0");
                rpt.NganhHang = gvChiTiet.GetFocusedRowCellValue("TenLMB").ToString();
                rpt.DateTruocTT = DateTruocTT.ToString("dd/MM/yyyy");
                rpt.TenDiaDiem = objTN.TenTN;
                rpt.LoadData();
                rpt.ShowPreviewDialog();
                new ADO_Query().executeNonQuery("[UpdateInLan]", CommandType.StoredProcedure,
                    "@ID", int.Parse(gvChiTiet.GetFocusedRowCellValue("ID").ToString()),
                    "@Lan", 1);
            }
        }

        private void itemInLan2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null) return;
            if (gvChiTiet.RowCount > 0)
            {
                var idChiTiet = int.Parse(gvChiTiet.GetFocusedRowCellValue("ID").ToString());
                var dt = new ADO_Query().getDataTable("SELECT * FROM dbo.ctChiTiet AS cct WHERE cct.ID =" + idChiTiet, CommandType.Text);
                var DateIn1 = (!String.IsNullOrEmpty(dt.Rows[0]["NgayInLan1"].ToString()))
                    ? DateTime.Parse(dt.Rows[0]["NgayInLan1"].ToString())
                    : DateTime.Now;
                var DateHH = (DateTime?)gvHopDong.GetFocusedRowCellValue("NgayHH");
                var DateHL = (DateTime?)gvHopDong.GetFocusedRowCellValue("NgayHL");
                var DateTruocTT = DateHH.Value.AddDays(-10);
                var objTN = db.tnToaNhas.Where(p => p.MaTN == (byte)itemToaNha.EditValue).First();
                var rpt = new rptInLan2();
                //rpt.TenCT = objTN.CongTyQuanLy;
                rpt.CongTyQL = objTN.CongTyQuanLy;
                rpt.TenKH = gvHopDong.GetFocusedRowCellValue("TenKH").ToString().ToUpper();
                rpt.SoHD = gvHopDong.GetFocusedRowCellValue("SoHDCT").ToString();
                rpt.TenMatBang = gvChiTiet.GetFocusedRowCellValue("MaSoMB").ToString();
                rpt.NgayHT = DateTime.Now.Day.ToString();
                rpt.ThangHT = DateTime.Now.Month.ToString();
                rpt.NamHT = DateTime.Now.Year.ToString();
                rpt.NamHH = DateHH.Value.Year.ToString();
                rpt.ThangHH = DateHH.Value.Month.ToString();
                rpt.DateHL = DateHL.Value.ToString("dd/MM/yyyy");
                rpt.NamHL = DateHL.Value.Year.ToString();
                rpt.DateHH = DateHH.Value.ToString("dd/MM/yyyy");
                rpt.SoTien = ((decimal)(gvChiTiet.GetFocusedRowCellValue("ThanhTien"))).ToString("N0");
                rpt.NganhHang = gvChiTiet.GetFocusedRowCellValue("TenLMB").ToString();
                rpt.DateTruocTT = DateTruocTT.ToString("dd/MM/yyyy");
                rpt.ngayIn1 = DateIn1.Day.ToString();
                rpt.thangIn1 = DateIn1.Month.ToString();
                rpt.namIn1 = DateIn1.Year.ToString();
                rpt.TenDiaDiem = objTN.TenTN;
                rpt.LoadData();
                rpt.ShowPreviewDialog();
                new ADO_Query().executeNonQuery("[UpdateInLan]", CommandType.StoredProcedure,
                    "@ID", idChiTiet,
                    "@Lan", 2);
            }
        }

        private void itemInLan3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null) return;
            if (gvChiTiet.RowCount > 0)
            {
                var idChiTiet = int.Parse(gvChiTiet.GetFocusedRowCellValue("ID").ToString());
                var dt = new ADO_Query().getDataTable("SELECT * FROM dbo.ctChiTiet AS cct WHERE cct.ID =" + idChiTiet, CommandType.Text);
                var DateIn1 = (!String.IsNullOrEmpty(dt.Rows[0]["NgayInLan1"].ToString()))
                    ? DateTime.Parse(dt.Rows[0]["NgayInLan1"].ToString())
                    : DateTime.Now;
                var DateIn2 = (!String.IsNullOrEmpty(dt.Rows[0]["NgayInLan2"].ToString()))
                    ? DateTime.Parse(dt.Rows[0]["NgayInLan2"].ToString())
                    : DateTime.Now;
                var DateHH = (DateTime?)gvHopDong.GetFocusedRowCellValue("NgayHH");
                var DateHL = (DateTime?)gvHopDong.GetFocusedRowCellValue("NgayHL");
                var DateTruocTT = DateHH.Value.AddDays(-10);
                var objTN = db.tnToaNhas.Where(p => p.MaTN == (byte)itemToaNha.EditValue).First();
                var rpt = new rptInLan3();
                //rpt.TenCT = objTN.CongTyQuanLy;
                rpt.CongTyQL = objTN.CongTyQuanLy;
                rpt.TenKH = gvHopDong.GetFocusedRowCellValue("TenKH").ToString().ToUpper();
                rpt.SoHD = gvHopDong.GetFocusedRowCellValue("SoHDCT").ToString();
                rpt.TenMatBang = gvChiTiet.GetFocusedRowCellValue("MaSoMB").ToString();
                rpt.NgayHT = DateTime.Now.Day.ToString();
                rpt.ThangHT = DateTime.Now.Month.ToString();
                rpt.NamHT = DateTime.Now.Year.ToString();
                rpt.NamHH = DateHH.Value.Year.ToString();
                rpt.ThangHH = DateHH.Value.Month.ToString();
                rpt.DateHL = DateHL.Value.ToString("dd/MM/yyyy");
                rpt.NamHL = DateHL.Value.Year.ToString();
                rpt.DateHH = DateHH.Value.ToString("dd/MM/yyyy");
                rpt.SoTien = ((decimal)(gvChiTiet.GetFocusedRowCellValue("ThanhTien"))).ToString("N0");
                rpt.NganhHang = gvChiTiet.GetFocusedRowCellValue("TenLMB").ToString();
                rpt.DateTruocTT = DateTruocTT.ToString("dd/MM/yyyy");
                rpt.ngayIn1 = DateIn1.Day.ToString();
                rpt.thangIn1 = DateIn1.Month.ToString();
                rpt.namIn1 = DateIn1.Year.ToString();
                rpt.ngayIn2 = DateIn2.Day.ToString();
                rpt.thangIn2 = DateIn2.Month.ToString();
                rpt.namIn2 = DateIn2.Year.ToString();
                rpt.TenDiaDiem = objTN.TenTN;
                rpt.LoadData();
                rpt.ShowPreviewDialog();
                new ADO_Query().executeNonQuery("[UpdateInLan]", CommandType.StoredProcedure,
                    "@ID", idChiTiet,
                    "@Lan", 3);
            }
        }

       

           
    }
}