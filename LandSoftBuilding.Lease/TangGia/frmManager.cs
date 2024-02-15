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

namespace LandSoftBuilding.Lease.TangGia
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public frmManager()
        {
            InitializeComponent();
        }

        #region Ham xu ly

        private void LoadData()
        {
            try
            {
                gcHopDong.DataSource = null;
                gcHopDong.DataSource = linqInstantFeedbackSource1;
                //if(Common.User.MaTN)

                try
                {
                    var db = new MasterDataContext();
                    var ltReport = (from rp in db.HDTTemplates
                                    where rp.MaTN == Common.User.MaTN
                                    & !rp.IsCongNo.GetValueOrDefault()
                                    select new { rp.ID, rp.TieuDe }).ToList();
                    barSub_MauIn.ItemLinks.Clear();
                    DevExpress.XtraBars.BarButtonItem itemPrint;
                    foreach (var i in ltReport)
                    {
                        itemPrint = new DevExpress.XtraBars.BarButtonItem(barManager1, i.TieuDe);
                        itemPrint.Tag = i.ID;
                        itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrint_ItemClick);
                        barManager1.Items.Add(itemPrint);
                        barSub_MauIn.ItemLinks.Add(itemPrint);
                    }
                }
                catch { }
            }
            catch
            {
            }
        }

        void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int)gvHopDong.GetFocusedRowCellValue("ID");

            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Phiếu thu] cần xem");
                return;
            }

            var maTN = (byte)itemToaNha.EditValue;

            using (var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl())
            {
                MasterDataContext db = new MasterDataContext();

                var bm = db.HDTTemplates.Single(o => o.ID == (int)e.Item.Tag);
                ctlRTF.Document.RtfText = bm.NoiDung;

                Mau.MergeField merge = new Mau.MergeField();
                merge.MaTN = (int)Common.User.MaTN;
                ctlRTF.RtfText = merge.HopDongThue(ctlRTF.RtfText, id, true);

                using (var frm = new Mau.frmDesign())
                {
                    frm.RtfText = ctlRTF.RtfText;
                    frm.ShowDialog();
                }
            }

        }

        private void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        private void Add()
        {
            try
            {
                using (frmEdit frm = new frmEdit {MaTN = (byte?) itemToaNha.EditValue})
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch
            {
            }
        }

        private void Edit()
        {
            try
            {
                var id = (int?) gvHopDong.GetFocusedRowCellValue("ID");

                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn [hợp đồng], xin cảm ơn.");
                    return;
                }

                using (LandSoftBuilding.Lease.frmEdit frm = new LandSoftBuilding.Lease.frmEdit() {MaTN = (byte?) itemToaNha.EditValue, ID = id})
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch
            {
            }
        }

        private void Delete()
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
                    var ct_bk = db.ctChiTiet_BackUps.Where(o => o.MaHDCT == (int)gvHopDong.GetRowCellValue(i, "ID"));
                    db.ctChiTiet_BackUps.DeleteAllOnSubmit(ct_bk);

                    var ctltt_bk = db.ctLichThanhToan_BackUps.Where(o => o.MaHD == (int)gvHopDong.GetRowCellValue(i, "ID"));
                    db.ctLichThanhToan_BackUps.DeleteAllOnSubmit(ctltt_bk);

                    var ct_pl = db.ctPhuLucs.Where(o => o.MaHD == (int)gvHopDong.GetRowCellValue(i, "ID"));
                    db.ctPhuLucs.DeleteAllOnSubmit(ct_pl);

                    var objHD = db.ctHopDongs.Single(p => p.ID == (int) gvHopDong.GetRowCellValue(i, "ID"));
                    db.ctHopDongs.DeleteOnSubmit(objHD);
                }
                db.SubmitChanges();

                this.RefreshData();
            }
            catch(Exception ex)
            {
                DialogBox.Error(ex.Message);
                //DialogBox.Alert(
                //    "Lưu dữ liệu không thành công! Ràng buộc dữ liệu không cho phép thực hiện thao tác này hoặc đường truyền không ổn định!");
            }
            finally
            {
                db.Dispose();
            }
        }

        private void Detail()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (int?) gvHopDong.GetFocusedRowCellValue("ID");
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
                        case 7:
                            ctlTaiLieu1.TaiLieu_Load();
                            ctlTaiLieu1.MaNV = null;
                            ctlTaiLieu1.objNV = null;
                            break;
                        case 8:
                            gcTaiSanBanGiao.DataSource = null;
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
                                                where ct.MaHDCT == id //& ct.IsXoa.GetValueOrDefault() == false
                                                orderby mb.MaSoMB
                                                select new
                                                       {
                                                           lmb.TenLMB,
                                                           mb.MaMB,
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
                                            join lt in db.LoaiTiens on l.MaLoaiTien equals lt.ID
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
                                                       l.DienGiai,
                                                       l.DienTich,
                                                       l.DonGia,
                                                       //l.NgungSuDung,
                                                       lt.TenLT,
                                                       l.TyGia,
                                                       l.ID
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
                                                           l.ID,
                                                           l.DotTT,
                                                           l.TuNgay,
                                                           l.DenNgay,
                                                           l.SoThang,
                                                           l.SoTien,
                                                           l.SoTienQD,
                                                           DaChi = db.pcChiTiets.Where(o=>o.TableName == "ctLichThanhToan" & o.LinkID == l.ID).Sum(o=>o.SoTien).GetValueOrDefault()
                                                       }).ToList();
                        break;
                    case 4:
                        gcPhuLuc.DataSource = from p in db.ctPhuLucs
                                              join nv in db.tnNhanViens on p.MaNVN equals nv.MaNV
                                              where p.MaHD == id & p.IsXoa.GetValueOrDefault() == false
                                              select new
                                              {
                                                  p.MaPL,
                                                  p.SoPL,
                                                  p.NgayPL,
                                                  nv.HoTenNV,
                                                  p.NgayNhap,
                                              };
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
                    case 8:
                        var model_ts = new { mahd = id };
                        var param_ts = new Dapper.DynamicParameters();
                        param_ts.AddDynamicParams(model_ts);
                        gcTaiSanBanGiao.DataSource = Library.Class.Connect.QueryConnect.Query<ct_taisan_get>("ct_taisan_get", param_ts);
                        break;
                }
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
            }
        }

        private void ThanhLy()
        {
            try
            {
                var id = (int?) gvHopDong.GetFocusedRowCellValue("ID");

                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn [hợp đồng], xin cảm ơn.");
                    return;
                }

                using (var frm = new Liquidate.frmEdit() {MaTN = (byte?) itemToaNha.EditValue, MaHD = id})
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Class
        public class ct_taisan_get
        {
            public System.DateTime? NgayGiao { get; set; }

            public System.DateTime? NgayNhan { get; set; }

            public System.DateTime? NgayNhap { get; set; }

            public System.DateTime? NgaySua { get; set; }

            public decimal? SoLuong { get; set; }

            public string BenGiao { get; set; }

            public string BenNhan { get; set; }

            public string ChucVu_Giao { get; set; }

            public string ChucVu_Nhan { get; set; }

            public string DiaChi_Giao { get; set; }

            public string DiaChi_Nhan { get; set; }

            public string MaBanGiao { get; set; }

            public string NguoiDaiDien_Giao { get; set; }

            public string NguoiDaiDien_Nhan { get; set; }

            public string NguoiNhap { get; set; }

            public string GhiChu { get; set; }

            public string HangMuc { get; set; }

            public string NhanHieu { get; set; }

            public string TrangThai { get; set; }
            public long? ID { get; set; }
            public long? IdCt { get; set; }

        }
        #endregion

        #region Event

        private void frmHopDong_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lookUpToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.TowerList.First().MaTN;

            gvHopDong.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            LoadData();
            barManager1.SetPopupContextMenu(gcPhuLuc, popupMenu1);
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }


        private string GetThongBaoTyGia(decimal? TyGiaTT, decimal? TyGiaHD, decimal? TyGiaAD, decimal? MucDCTG)
        {
            if (TyGiaTT != TyGiaAD)
            {
                var TyLeTD = ((TyGiaTT - TyGiaHD)/TyGiaTT).GetValueOrDefault();
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

            var _TuNgay = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);

            var sql = from p in db.ctHopDongs
                join kh in db.tnKhachHangs on p.MaKH equals kh.MaKH
                join lt in db.LoaiTiens on p.MaLT equals lt.ID
                join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into viewNhanVienSua
                from nvs in viewNhanVienSua.DefaultIfEmpty()
                join ltnt in db.LoaiTiens on p.LoaiTienNgoaiTe equals ltnt.ID into loaiTienNgoaiTe from ltnt in loaiTienNgoaiTe.DefaultIfEmpty()
                where p.MaTN == (byte?) itemToaNha.EditValue & SqlMethods.DateDiffDay(_TuNgay, p.NgayTangGiaTiepTheo) > 0
                      orderby p.NgayKy descending
                select new
                       {
                           p.MaKH,
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
                           TyLeTDTG = (lt.TyGia - p.TyGiaHD)/lt.TyGia,
                           TBTDTG = GetThongBaoTyGia(lt.TyGia, p.TyGiaHD, p.TyGia, p.MucDCTG),
                           //GiaThueQD = p.GiaThue*p.TyGia,
                           GiaThueQDM2 = p.ctChiTiets.Sum(o => o.DonGia) * p.TyGia,
                           GiaThueQD = p.GiaThue * p.TyGiaHD,
                           TienCocQD = p.TienCoc,
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
                           GiaThueCT= p.ctChiTiets.Sum(o=>o.DonGia),
                           TyGiaQDVND= p.TyGiaHD,
                           p.TyGiaNgoaiTe,
                           LoaiTienNgoaiTe = ltnt.TenLT,
                           p.GiaThueNgoaiTe,
                           p.GhiChu,
                           p.DieuKhoanDacBiet,
                           p.DieuKhoanBoSungGiamThue,
                           p.XemXetLaiTienThue,
                           p.DonGiaTuongDuongUSD,
                           p.TyGiaApDungBanDau,
                           p.ThoiGIanThayDoiTyGIa,
                           p.CachThayDoiTyGia,
                           p.NgayTangGiaTiepTheo
                       };

            e.QueryableSource = sql;

            e.Tag = db;
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender,
            DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                //(e.Tag as MasterDataContext).Dispose();
            }
            catch
            {
            }
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
            var id = (int?) gvChiTiet.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn chi tiết thuê để điều chỉnh giá");
                return;
            }

            frmLSDCGEdit frm = new frmLSDCGEdit();
            frm.ShowDialog();
        }

        private void itemLichSuDieuChinhGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?) gvChiTiet.GetFocusedRowCellValue("ID");
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
            var id = (int?) gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn hợp đồng");
                return;
            }

            frmLSDCGEdit frm = new frmLSDCGEdit();
            frm.MaHD = id;
            frm.ShowDialog();
        }

        private void itemHopDongThueGianHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?) gvHopDong.GetFocusedRowCellValue("ID");
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
            byte? MaTN = (byte?) itemToaNha.EditValue;
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

        private void itemHopDongThueNganHan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void itemTBThanhToan1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null) return;
            var rpt = new rptThongBaoThanhToan(id,(byte)itemToaNha.EditValue);
            rpt.ShowPreviewDialog();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null) return;
            var rpt = new rptThongBaoThanhToanMau2(id, (byte)itemToaNha.EditValue);
            rpt.ShowPreviewDialog();
        }

        private void itemDieuChinhGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null) return;

            using (var frm = new frmLSDCGEdit())
            {
                frm.MaHD = id;
                frm.ShowDialog();
            }
        }

        private void itemDieuChinhDienTich_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null) return;

            using (var frm = new frmDieuChinhDienTIch())
            {
                frm.MaHD = id;
                frm.ShowDialog();
            }
        }

        private void itemChuyenPhong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null) return;

            using (var frm = new frmChuyenPhong())
            {
                frm.MaHD = id;
                frm.ShowDialog();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null) return;

            using (var frm = new frmNgungSuDung())
            {
                frm.MaHD = id;
                frm.ShowDialog();
            }
        }

        private void itemDieuChinhTienCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null) return;

            using (var frm = new frmDieuChinhTienCoc())
            {
                frm.MaHD = id;
                frm.ShowDialog();
            }
        }

        private void gvPhuLuc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var id = (int?)gvPhuLuc.GetFocusedRowCellValue("MaPL");
            gcChiTiet_PhuLuc.DataSource = null;
            gc_LichThanhToanBackup.DataSource = null;
            if (id == null) return;

            using (var db = new MasterDataContext())
            {
                gcChiTiet_PhuLuc.DataSource = from p in db.ctPhuLuc_ChiTiets
                                              join mb in db.mbMatBangs on p.MaMB equals mb.MaMB into matbang from mb in matbang.DefaultIfEmpty()
                                              join mb_new in db.mbMatBangs on p.MaMB_new equals mb_new.MaMB into matbangnew
                                              from mb_new in matbangnew.DefaultIfEmpty()
                                              join lpl in db.ctLoaiPhuLucs on p.MaLoaiPL equals lpl.MaLoai
                                              where p.MaPL == id
                                              select new
                                              {
                                                  lpl.TenLoai,
                                                  mb.MaSoMB,
                                                  MaSoMB_new = mb_new.MaSoMB,
                                                  p.DienTich,
                                                  p.DonGia,
                                                  p.Chuyen_GiaThue,
                                                  p.Chuyen_MaLG,
                                                  p.TuNgay,
                                                  p.DenNgay,
                                                  p.Coc_SoTien,
                                              };
                var DoTT = (from l in db.ctLichThanhToans                           
                            join pl in db.ctPhuLucs on l.MaHD equals pl.MaHD
                            where pl.MaPL == id
                            select l.DotTT).Max();
                                       

                gc_LichThanhToanBackup.DataSource = (from l in db.ctLichThanhToan_BackUps
                                                     join mb in db.mbMatBangs on l.MaMB equals mb.MaMB
                                                     join lt in db.LoaiTiens on l.MaLoaiTien equals lt.ID
                                                     where l.id_pl == id
                                                     orderby  l.ID_BK descending
                                                     select new
                                                     {
                                                         mb.MaSoMB,
                                                         l.DotTT,
                                                         l.TuNgay,
                                                         l.DenNgay,
                                                         l.SoThang,
                                                         l.SoTien,
                                                         l.SoTienQD,
                                                         l.DienGiai,
                                                         l.DienTich,
                                                         l.DonGia,
                                                         l.NgungSuDung,
                                                         lt.TenLT,
                                                         l.TyGia,
                                                     }).Take((int)DoTT).ToList();
            }
        }

        private void itemViewHDGoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");

            //if (id == null) return;

            //using (var frm = new frmPreview())
            //{
            //    frm.ID = id;
            //    frm.ShowDialog();
            //}
        }

        private void itemLamTronTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");

            if (id == null) return;

            using (var db = new MasterDataContext())
            {
                var hd = db.ctHopDongs.Single(o => o.ID == id);

                //foreach(var ltt in hd.ctLichThanhToans)
                //{
                //    SchedulePaymentCls.ctLichThanhToan_LamTronThanhTien(ltt, true, false);
                //}

                db.SubmitChanges();
                DialogBox.Success();
            }
        }

        private void itemLamTron_Gia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null) return;
            using (var db = new MasterDataContext())
            {
                var hd = db.ctHopDongs.Single(o => o.ID == id);

                //foreach (var ltt in hd.ctLichThanhToans)
                //{
                //    SchedulePaymentCls.ctLichThanhToan_LamTronThanhTien(ltt, true, true);
                //}

                db.SubmitChanges();
                DialogBox.Success();
            }
        }

        private void itemLamTron_TienCuoi_All_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var lthd = db.ctHopDongs;

                foreach (var hd in lthd)
                {
                    //foreach (var ltt in hd.ctLichThanhToans)
                    //{
                    //    SchedulePaymentCls.ctLichThanhToan_LamTronThanhTien(ltt, true, false);
                    //}
                }

                db.SubmitChanges();
                DialogBox.Success();
            }
        }

        private void item_LamTron_Gia_all_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var lthd = db.ctHopDongs;

                foreach (var hd in lthd)
                {
                    //foreach (var ltt in hd.ctLichThanhToans)
                    //{
                    //    SchedulePaymentCls.ctLichThanhToan_LamTronThanhTien(ltt, true, true);
                    //}
                }

                db.SubmitChanges();
                DialogBox.Success();
            }
        }

        private void itemKhongLamTron_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            if (id == null) return;
            using (var db = new MasterDataContext())
            {
                var hd = db.ctHopDongs.Single(o => o.ID == id);

                //foreach (var ltt in hd.ctLichThanhToans)
                //{
                //    SchedulePaymentCls.ctLichThanhToan_LamTronThanhTien(ltt, false, true);
                //}

                db.SubmitChanges();
                DialogBox.Success();
            }
        }

        private void btnChiTien_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            
        }

        private void btnChiTien_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btnChiTien_Click(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var id = (int?)gvTienCoc.GetFocusedRowCellValue("ID");

                if (id == null) return;

                var ltt = db.ctLichThanhToans.Single(o => o.ID == id);
                var hd = ltt.ctHopDong;
                if (ltt.SoTien > 0)
                {
                    DialogBox.Error("Không thể chi tiền từ lịch thanh toán tiền cọc này. Vui lòng kiểm tra lại");
                    return;
                }

                var DaChi = db.pcChiTiets.Where(o => o.TableName == "ctLichThanhToan" & o.LinkID == id).Sum(o => o.SoTien).GetValueOrDefault();

                var ConNo = ltt.SoTien * (-1) - DaChi;

                if (ConNo <= 0)
                {
                    DialogBox.Error("Hợp đồng này đã thanh toán hết tiền cọc. Vui lòng kiểm tra lại");
                    return;
                }

                using (var frm = new Fund.Output.frmEdit())
                {
                    frm.MaKH = hd.MaKH;
                    frm.MaTN = hd.MaTN;

                    frm.ChiTiets = new List<Fund.Output.ChiTietPhieuChiItem>();

                    Fund.Output.ChiTietPhieuChiItem item = new Fund.Output.ChiTietPhieuChiItem();

                    item.TableName = "ctLichThanhToan";
                    item.LinkID = id;
                    item.DienGiai = string.Format("Chi trả tiền đặt cọc hợp đồng {0}", hd.SoHDCT);
                    item.SoTien = ConNo;
                    frm.ChiTiets.Add(item);
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        LoadData();
                }
            }
        }

        private void itemSuaPL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            var idpl = (int?)gvPhuLuc.GetFocusedRowCellValue("MaPL");
            if (id == null) return;

            using (var frm = new frmDieuChinhDienTIch())
            {
                frm.MaHD = id;
                frm.MaPL = idpl;
                frm.ShowDialog();
            }
        }

        void DeletePhuLuc()
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
            {          
            MasterDataContext db = new MasterDataContext();
            
            var idhd = (int?)gvHopDong.GetFocusedRowCellValue("ID");
            var idpl = (int?)gvPhuLuc.GetFocusedRowCellValue("MaPL");

            var objPL = db.ctPhuLucs.Single(o => o.MaPL == idpl);

            if (objPL.ctHopDong.ctPhuLucs.Any(o => SqlMethods.DateDiffSecond(objPL.NgayNhap, o.NgayNhap) >= 0 & o.MaPL != objPL.MaPL & o.IsXoa.GetValueOrDefault() == false))
            {
                DialogBox.Error("Vui lòng xóa phục lục có ngày nhập " + string.Format("{0:dd/MM/yyyy}", objPL.NgayNhap) + "");
                return;
            }

            //if (objPL.ctPhuLuc_ChiTiets.Where(o => o.MaLoaiPL == 6 & o.MaPL == objPL.MaPL).Count() > 0)
            //{
            //    var HD = db.ctHopDongs.SingleOrDefault(o => o.ID == idhd);
            //    HD.NgayHH = HD.NgayTruocGiaHan;
            //    db.SubmitChanges();
            //}

            //Update Lichthanhtoan
            var ctltt = db.ctLichThanhToans.Where(o => o.MaHD == idhd).ToList();
            db.ctLichThanhToans.DeleteAllOnSubmit(ctltt);

            var ctltt_bk = db.ctLichThanhToan_BackUps.Where(o => o.id_pl == idpl).ToList();
            foreach (var add in ctltt_bk)
            {
                ctLichThanhToan ctbk = new ctLichThanhToan();
                LandSoftBuilding.Lease.GetData.InsertUpdate(add, ctbk);
                db.ctLichThanhToans.InsertOnSubmit(ctbk);
                db.SubmitChanges();
            }
            db.ctLichThanhToan_BackUps.DeleteAllOnSubmit(ctltt_bk);

            //Update Chitiet hợp đồng
            //db.ctChiTiets.DeleteAllOnSubmit(db.ctChiTiets.Where(o => o.MaHDCT == idhd && o.IsXoa.GetValueOrDefault()== false).ToList());
            var ctChiTietHD = db.ctChiTiets.Where(o => o.MaHDCT == idhd ).ToList();
            foreach (var item in ctChiTietHD)
            {
                var ct = db.ctChiTiets.Single(o => o.ID == item.ID);
                db.ctChiTiets.DeleteOnSubmit(ct);
            }
            db.SubmitChanges();

                var sumCocDotTT = db.ctLichThanhToans.Where(p => p.MaHD == idhd && p.MaLDV == 4).Select(p => p.SoTien).Sum();
                var objHD_UCoc = db.ctHopDongs.Where(p => p.ID == idhd).Select(p=>p).FirstOrDefault();

                objHD_UCoc.TienCoc = sumCocDotTT;
                db.SubmitChanges();

                var cthd_bk = db.ctChiTiet_BackUps.Where(o => o.id_pl == idpl).ToList();
            foreach (var add in cthd_bk)
            {
                ctChiTiet ctbk = new ctChiTiet();
                LandSoftBuilding.Lease.GetData.InsertUpdate(add, ctbk);
                //ctbk.IsXoa = false;
                db.ctChiTiets.InsertOnSubmit(ctbk);
                db.SubmitChanges();
            }
            db.ctChiTiet_BackUps.DeleteAllOnSubmit(cthd_bk);
            //Ẩn phụ lục
            var pl = db.ctPhuLucs.Where(o=>o.MaPL == idpl).FirstOrDefault();
            pl.IsXoa = true;

            db.SubmitChanges();
            }
        }
        public static T Clone<T>(T source)
        {
            var clone = (T)Activator.CreateInstance(typeof(T));
            var cols = typeof(T).GetProperties()
                .Select(p => new { Prop = p, Attr = (System.Data.Linq.Mapping.ColumnAttribute)p.GetCustomAttributes(typeof(System.Data.Linq.Mapping.ColumnAttribute), true).SingleOrDefault() })
                .Where(p => p.Attr != null && !p.Attr.IsDbGenerated);
            foreach (var col in cols)
                col.Prop.SetValue(clone, col.Prop.GetValue(source, null), null);
            return clone;
        }
        private void item_XoaPL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeletePhuLuc();
            this.LoadData();
        }

        /// <summary>
        /// XÓA LỊCH THANH TOÁN CHO THUÊ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemXoaLichThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int[] indexs = gvLTT.GetSelectedRows();

                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn Lịch thanh toán cần xóa");
                    return;
                }

                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                foreach (int i in indexs)
                {
                    var id = (int?)gvLTT.GetRowCellValue(i, "ID");
                    var model_ltt = new { id = id };
                    var param_ltt = new Dapper.DynamicParameters();
                    param_ltt.AddDynamicParams(model_ltt);
                    var result = Library.Class.Connect.QueryConnect.Query<bool>("hdt_ltt_delete", param_ltt).ToList();
                    if (result.Count() > 0)
                    {
                        if (result.First() == false)
                        {
                            DialogBox.Error("Không xóa được, lịch thanh toán đã được tạo hóa đơn.");
                        }
                    }
                    else DialogBox.Error("Không tìm thấy lịch thanh toán");
                }

                Detail();
            }
            catch(System.Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            
        }

        private void btnImportLichThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            byte? MaTN = (byte?)itemToaNha.EditValue;
            if (MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án]. Xin cám ơn!");
                return;
            }

            var frm = new LandSoftBuilding.Lease.Liquidate.frmImportLichThanhToan();
            frm.MaTN = MaTN.Value;
            frm.ShowDialog();
            if (frm.isSave)
                this.LoadData();
        }

        private void itemImportTaiSanBanGiao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            byte? MaTN = (byte?)itemToaNha.EditValue;
            if (MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án]. Xin cám ơn!");
                return;
            }

            var frm = new LandSoftBuilding.Lease.TaiSanBanGiao.frmImport();
            frm.MaTN = MaTN.Value;
            frm.ShowDialog();
            if (frm.isSave)
                this.LoadData();
        }

        private void itemXoaTaiSanBanGiao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int[] indexs = gvTaiSanBanGiao.GetSelectedRows();

                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn Lịch thanh toán cần xóa");
                    return;
                }

                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                foreach (int i in indexs)
                {
                    var id = (long?)gvTaiSanBanGiao.GetRowCellValue(i, "IdCt");
                    var model_ltt = new { idct = id };
                    var param_ltt = new Dapper.DynamicParameters();
                    param_ltt.AddDynamicParams(model_ltt);
                    var result = Library.Class.Connect.QueryConnect.Query<bool>("ct_taisanbangiao_delete", param_ltt).ToList();
                }

                Detail();
            }
            catch (System.Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemInBanGiao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int[] indexs = gvTaiSanBanGiao.GetSelectedRows();

                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn bàn giao cần in");
                    return;
                }

                foreach (int i in indexs)
                {
                    var id = (long?)gvTaiSanBanGiao.GetRowCellValue(i, "ID");
                    if (id == null) return;
                    var db = new MasterDataContext();
                    var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == 124);
                    if (objForm != null)
                    {
                        var rtfText = BuildingDesignTemplate.Class.HopDongThue_TaiSanBanGiao.Merge(id, objForm.Content);
                        var frm = new BuildingDesignTemplate.FrmShow { RtfText = rtfText };
                        frm.ShowDialog();

                    }
                }

            }
            catch (System.Exception ex)
            {
                DialogBox.Error(ex.Message);
            }

            
        }

        private void itemTangGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = (int?)gvHopDong.GetFocusedRowCellValue("ID");

                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn [hợp đồng], xin cảm ơn.");
                    return;
                }

                using (frmEdit frm = new LandSoftBuilding.Lease.TangGia.frmEdit() { MaTN = (byte?)itemToaNha.EditValue, Id = id })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch
            {
            }
        }
    }
}