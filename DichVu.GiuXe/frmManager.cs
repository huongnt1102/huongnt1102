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
using LandSoftBuilding.Report;

namespace DichVu.GiuXe
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public frmManager()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            gcGiuXe.DataSource = null;
            gcGiuXe.DataSource = linqInstantFeedbackSource1;
        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        void AddGiuXe()
        {
            using (var frm = new frmEdit())
            {
                frm.MaTN = (byte?)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void EditGiuXe()
        {
            try
            {
                using (var frm = new frmEdit())
                {
                    frm.MaTN = (byte?)itemToaNha.EditValue;
                    frm.ID = (int?)gvGiuXe.GetFocusedRowCellValue("ID");
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                        this.RefreshData();
                }
            }
            catch { }
        }

        void DeleteGiuXe()
        {
            var indexs = gvGiuXe.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Vui lòng chọn thẻ xe");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (var i in indexs)
                {
                    var id = (int?)gvGiuXe.GetRowCellValue(i, "ID");
                    var tx = db.dvgxGiuXes.Single(p => p.ID == id);
                    db.dvgxGiuXes.DeleteOnSubmit(tx);
                }

                db.SubmitChanges();

                this.RefreshData();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void DetailGiuXe()
        {
            var db = new MasterDataContext();
            try
            {
                var _ID = (int?)gvGiuXe.GetFocusedRowCellValue("ID");
                if (_ID == null)
                {
                    gcTheXe.DataSource = null;
                    gcLTT.DataSource = null;
                }

                switch (tabMain.SelectedTabPageIndex)
                {
                    case 0:
                        gcTheXe.DataSource = (from tx in db.dvgxTheXes
                                              join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                                              where tx.MaGX == _ID
                                              select new
                                              {
                                                  tx.NgayDK,
                                                  tx.SoThe,
                                                  lx.TenLX,
                                                  tx.GiaNgay,
                                                  tx.GiaThang,
                                                  tx.PhiLamThe,
                                                  tx.BienSo,
                                                  tx.MauXe,
                                                  tx.DoiXe,
                                                  tx.ChuThe,
                                                  tx.DienGiai,
                                                  tx.NgungSuDung
                                              }).ToList();
                        break;
                    case 1:
                        gcLTT.DataSource = (from ltt in db.dvgxLichThanhToans
                                            join dv in db.dvgxDonViThoiGians on ltt.MaDVTG equals dv.ID
                                            where ltt.MaGX == _ID
                                            select new
                                            {
                                                ltt.NgayTT,
                                                ltt.KyTT,
                                                dv.TenDVTG,
                                                ltt.TienTT,
                                                ltt.IsLapLai,
                                                ltt.TuNgay,
                                                ltt.DenNgay,
                                                ltt.DienGiai
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

        void ImportGiuXe()
        {
            try
            {
                using (var frm = new frmImport())
                {
                    frm.IsGiuXe = true;
                    frm.MaTN = (byte)itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.isSave)
                        this.RefreshData();
                }
            }
            catch { }
            //try
            //{
            //    using (var frm = new frmImport())
            //    {
            //        frm.MaTN = (byte)itemToaNha.EditValue;
            //        frm.ShowDialog();
            //        if (frm.isSave)
            //            this.RefreshData();
            //    }
            //}
            //catch { }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            gvGiuXe.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            tabMain.SelectedTabPageIndex = 0;
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var db = new MasterDataContext();
            var maTN = (byte)itemToaNha.EditValue;
            e.QueryableSource = from gx in db.dvgxGiuXes
                                join mb in db.mbMatBangs on gx.MaMB equals mb.MaMB
                                join kh in db.tnKhachHangs on gx.MaKH equals kh.MaKH
                                join nvn in db.tnNhanViens on gx.MaNVN equals nvn.MaNV
                                join nvs in db.tnNhanViens on gx.MaNVS equals nvs.MaNV into tblNguoiSua
                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                where gx.MaTN == maTN
                                select new
                                {
                                    gx.ID,
                                    gx.NgayDK,
                                    gx.SoDK,
                                    mb.MaSoMB,
                                    TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                    SoLuong = (from tx in db.dvgxTheXes where tx.MaGX == gx.ID select tx).Count(),
                                    PhiGiuXe = (from tx in db.dvgxTheXes where tx.MaGX == gx.ID select tx.GiaThang).Sum(),
                                    gx.DienGiai,
                                    NguoiNhap = nvn.HoTenNV,
                                    gx.NgayNhap,
                                    NguoiSua = nvs.HoTenNV,
                                    gx.NgaySua,
                                    gx.NgungSuDung
                                    
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

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddGiuXe();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EditGiuXe();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteGiuXe();
        }

        private void grvGiuXe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.DetailGiuXe();
        }

        private void grvGiuXe_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.DetailGiuXe();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportGiuXe();
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            this.DetailGiuXe();
        }

        private void itemPhieuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (var f = new frmOption())
            //{
            //    f.CateID = 4;
            //   // f.ShowDialog();
            //}
        }

        private void itemDanhSachXe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (var f = new frmOption())
            //{
            //    f.CateID = 5;
            //   // f.ShowDialog();
            //}
        }

        private void itemCongNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (var f = new frmOption())
            //{
            //    f.CateID = 6;
            //  //  f.ShowDialog();
            //}
        }

        private void itemDanhSachNgungXe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (var f = new frmOption())
            //{
            //    f.CateID = 7;
            //  //  f.ShowDialog();
            //}
        }

        private void itemTongHop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (var f = new frmOption())
            //{
            //    f.CateID = 8;
            //   // f.ShowDialog();
            //}
        }
    }
}