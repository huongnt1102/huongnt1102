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

namespace LandSoftBuilding.Lease.PaymentSchedule
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
                gcMatBang.DataSource = null;
                gcMatBang.DataSource = linqInstantFeedbackSource1;
            }
            catch { }
        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        void Detail()
        {
            var db = new MasterDataContext();
            try
            {
                var _MaMB = (int?)gvMatBang.GetFocusedRowCellValue("MaMB");
                if (_MaMB == null)
                {
                    switch (tabMain.SelectedTabPageIndex)
                    {
                        case 0:
                            gcLTT.DataSource = null;
                            break;
                        case 1:
                            gcLTT.DataSource = null;
                            break;
                        case 2: gcChiTiet.DataSource = null;
                            break;
                    }
                    return;
                }

                if ((gvMatBang.GetFocusedRowCellValue("NgungSuDung") as bool?).GetValueOrDefault() == true)
                {
                    itemNgungSuDung.Caption = "Sử dụng";
                }
                else
                {
                    itemNgungSuDung.Caption = "Ngừng sử dụng";
                }

                var _MaHD = (int?)gvMatBang.GetFocusedRowCellValue("MaHDCT");
                var _MaLDV = tabMain.SelectedTabPageIndex == 0 ? 2 : 3;

                var ltLTT = (from l in db.ctLichThanhToans
                             where l.MaHD == _MaHD & l.MaMB == _MaMB & l.MaLDV == _MaLDV
                             orderby l.DotTT
                             select new
                             {
                                 l.DotTT,
                                 l.TuNgay,
                                 l.DenNgay,
                                 l.SoThang,
                                 l.SoTien,
                                 l.SoTienQD,
                                 l.DienGiai
                             }).ToList();
               
                switch (tabMain.SelectedTabPageIndex)
                {
                    case 0:
                        gcLTT.DataSource = ltLTT;
                        break;
                    case 1:
                        gcSuaChua.DataSource = ltLTT;
                        break;
                    case 2:
                         var _ID = (int?)gvMatBang.GetFocusedRowCellValue("ID");
                         gcChiTiet.DataSource = (from dc in db.ctLichSuDieuChinhGias
                                                 join pl in db.ctLoaiDieuChinhs on dc.MaLDC equals pl.ID
                                                 join nv in db.tnNhanViens on dc.MaNVN equals nv.MaNV
                                                 where dc.MaCT == _ID
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
                }
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void EditLTT(int _MaLDV)
        {
            var _MaMB = (int?)gvMatBang.GetFocusedRowCellValue("MaMB");
            if (_MaMB == null)
            {
                DialogBox.Error("Vui lòng chọn bản ghi");
                return;
            }

            using (var frm = new frmEdit())
            {
                frm.MaHD = (int)gvMatBang.GetFocusedRowCellValue("MaHDCT");
                frm.MaMB = _MaMB;
                frm.MaLDV = _MaLDV;
                frm.ShowDialog(this);
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    this.RefreshData();
                }
            }
        }

        void NgungSuDung()
        {
            var _ID = (int?)gvMatBang.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn bản ghi");
                return;
            }

            if (DialogBox.Question("Bạn có muốn tiếp tục thực hiện không?") == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                var ct = db.ctChiTiets.Single(p => p.ID == _ID);
                ct.NgungSuDung = !ct.NgungSuDung.GetValueOrDefault();
                db.SubmitChanges();

                this.RefreshData();
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
            }
        }
        #endregion

        #region Event
        private void frmHopDong_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lookUpToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            gvMatBang.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var db = new MasterDataContext();
            e.QueryableSource = from ct in db.ctChiTiets
                                join p in db.ctHopDongs on ct.MaHDCT equals p.ID
                                join kh in db.tnKhachHangs on p.MaKH equals kh.MaKH
                                join lt in db.LoaiTiens on p.MaLT equals lt.ID
                                join mb in db.mbMatBangs on ct.MaMB equals mb.MaMB
                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                where p.MaTN == (byte?)itemToaNha.EditValue
                                orderby p.NgayKy descending
                                select new
                                {
                                    ct.ID,
                                    ct.MaMB,
                                    ct.MaHDCT,
                                    kn.TenKN,
                                    tl.TenTL,
                                    //MaSoMB = string.Format("{0}; {1:#,0.##} m2; {2}; Số HD: {3}", mb.MaSoMB,ct.DienTich, kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen, p.SoHDCT),
                                    MaSoMB = mb.MaSoMB + "; "
                                            + (kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen) + "; Số HD: "
                                            + p.SoHDCT,
                                    ct.DienTich,
                                    ct.DonGia,
                                    ct.PhiDichVu,
                                    ct.ThanhTien,
                                    ct.PhiSuaChua,
                                    TenLT = lt.KyHieuLT,
                                    p.TyGia,
                                    TienThue = (from ltt in db.ctLichThanhToans
                                                where ltt.MaHD == ct.MaHDCT 
                                                    & ltt.MaMB == ct.MaMB
                                                    & SqlMethods.DateDiffDay((DateTime?)ct.TuNgay, ltt.TuNgay) >= (int?)0 
                                                    & SqlMethods.DateDiffDay(ltt.TuNgay, (DateTime?)ct.DenNgay) >= (int?)0
                                                select ltt.SoTienQD).Sum(),
                                    //TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                    //p.SoHDCT,
                                    //p.NgayKy,
                                    //p.ThoiHan,
                                    //p.NgayHH,
                                    //p.NgayBG,
                                    ct.NgungSuDung,
                                    ct.TuNgay,
                                    ct.DenNgay
                                };
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
        
        private void tabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            this.Detail();
        }
        
        private void gvMatBang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Detail();
        }

        private void gvMatBang_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.Detail();
        }
        #endregion

        private void itemLTT_TienThue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EditLTT(2);
        }

        private void itemLTT_TienSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EditLTT(3);
        }

        private void itemNgungSuDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.NgungSuDung();
        }

        private void itemDieuChinhGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _ID = (int?)gvMatBang.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn bản ghi");
                return;
            }


            frmLSDCGEdit frm = new frmLSDCGEdit();
            frm.MaCT = _ID;
            frm.ShowDialog();
            if (frm.IsSave == true)
            {
                var rowhandle = gvMatBang.FocusedRowHandle;
                LoadData();
                gvMatBang.FocusedRowHandle = rowhandle;
                Detail();
            }
        }

        //private void itemLichSuDieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    var _ID = (int?)gvMatBang.GetFocusedRowCellValue("ID");
        //    if (_ID == null)
        //    {
        //        DialogBox.Error("Vui lòng chọn bản ghi");
        //        return;
        //    }


        //    frmDsDieuChinh frm = new frmDsDieuChinh(_ID);
        //    frm.ShowDialog();
        //}
    }
}