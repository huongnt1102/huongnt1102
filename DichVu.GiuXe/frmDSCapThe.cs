using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraReports.UI;
using Library;
using LandSoftBuilding.Report;

namespace DichVu.GiuXe
{
    public partial class frmDSCapThe : DevExpress.XtraEditors.XtraForm
    {
        public frmDSCapThe()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            gcGiuXe.DataSource = null;
            gcGiuXe.DataSource = linqInstantFeedbackSource1;
        }
        void RestoreThe(dvgxTheXe TheMoi, dvgxTheXe_Backup TheBak)
        {
            using (var db = new MasterDataContext())
            {
                TheMoi.MaTN = TheBak.MaTN;
                TheMoi.MaKH = TheBak.MaKH;
                TheMoi.MaMB = TheBak.MaMB;
                TheMoi.MaNK = TheBak.MaNK;
                TheMoi.MaGX = TheBak.MaGX;
                TheMoi.SoThe = TheBak.SoThe;
                TheMoi.NgayDK = TheBak.NgayDK;
                TheMoi.ChuThe = TheBak.ChuThe;
                TheMoi.MaLX = TheBak.MaLX;
                TheMoi.BienSo = TheBak.BienSo;
                TheMoi.DoiXe = TheBak.DoiXe;
                TheMoi.MauXe = TheBak.MauXe;
                TheMoi.PhiLamThe = TheBak.PhiLamThe;
                TheMoi.GiaNgay = TheBak.GiaNgay;
                TheMoi.GiaThang = TheBak.GiaThang;
                TheMoi.NgayTT = TheBak.NgayTT;
                TheMoi.KyTT = TheBak.KyTT;
                TheMoi.TienTT = TheBak.TienTT;
                TheMoi.NgungSuDung = TheBak.NgungSuDung;
                TheMoi.DienGiai = TheBak.DienGiai;
                TheMoi.MaDM = TheBak.MaDM;
                TheMoi.MaNVN = TheBak.MaNVN;
                TheMoi.NgayNhap = TheBak.NgayNhap;
                TheMoi.MaNVS = TheBak.MaNVS;
                TheMoi.NgaySua = TheBak.NgaySua;
                TheMoi.NgayNgungSD = TheBak.NgayNgungSD;
                TheMoi.MaKhoThe = TheBak.MaKhoThe;
                TheMoi.IsTheXe = TheBak.IsTheXe;
                TheMoi.IsThangMay = TheBak.IsThangMay;
                TheMoi.GhiChu = TheBak.GhiChu;
                TheMoi.IsTheOto = TheBak.IsTheOto;
                TheMoi.IsHongThe = TheBak.IsHongThe;
                TheMoi.LyDo = TheBak.LyDo;
                TheMoi.IsSaoLuu = TheBak.IsSaoLuu;
                db.SubmitChanges();
            }
        }
        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        void AddGiuXe()
        {
            using (var frm = new frmCapThe())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void EditGiuXe()
        {
            try
            {
                if (gvGiuXe.GetFocusedRowCellValue("ID") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cấp thẻ");
                    return;
                }

                using (var frm = new frmCapThe())
                {
                    frm.MaTN = (byte?)itemToaNha.EditValue;
                    frm.ID = int.Parse(gvGiuXe.GetFocusedRowCellValue("ID").ToString());
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

                    var tx = db.KhoThe_DSCapThes.Single(p => p.ID == id);

                    // Kiểm tra thẻ đã được áp dụng cho những lần sau hay chưa
                    foreach (var item in tx.CapThe_ChiTiets)
                    {
                        int? sothecu = 0;

                        if(item.MaTheCu != null)
                            sothecu = db.dvgxTheXes.Single(o=>o.ID == item.MaTheCu).MaTheMoi;

                        var ltThe = (from p in db.KhoThe_DSCapThes
                                     join ct in db.CapThe_ChiTiets on p.ID equals ct.MaCapThe
                                     where (ct.MaTheCu == item.MaTheCu | ct.MaThe == item.MaTheCu | ct.MaThe == item.MaThe | ct.MaTheCu == item.MaThe | ct.MaTheCu == sothecu | ct.MaThe == sothecu)
                                     & p.ID != tx.ID
                                     & SqlMethods.DateDiffSecond(tx.NgayNhap,p.NgayNhap) > 0
                                     select p).ToList();
                        if (ltThe.Count > 0)
                        {
                            DialogBox.Error("Phiếu cấp thẻ này đã được áp dụng. Không thể xóa");
                            return;
                        }
                    }

                    // Xóa tất cả các thẻ thang mý đã sao lưu từ lần cấp thẻ này
                    var ltSaoLuu = db.dvgxTheXes.Where(o => o.MaCapThe == tx.ID);
                    db.dvgxTheXes.DeleteAllOnSubmit(ltSaoLuu);

                    // Khôi phục lại trạng thái ban đầu trc khi xóa
                    foreach (var item in tx.CapThe_ChiTiets)
                    {
                        // Backup Thẻ mới
                        if (item.MaThe != null)
                        {
                            try
                            {
                                var TheMoi = db.dvgxTheXes.Single(o => o.ID == item.MaThe);
                                var TheBak = db.dvgxTheXe_Backups.Single(o => o.ID == item.TheMoiBackupID);
                                RestoreThe(TheMoi, TheBak);
                            }
                            catch (Exception ex)
                            {
                                DialogBox.Error(ex.Message);
                            }

                        }

                        if (item.MaTheCu != null)
                        {
                            var TheCu = db.dvgxTheXes.Single(o => o.ID == item.MaTheCu);
                            if (item.MaLoaiDK == 2) // Trường hợp tích hợp thẻ
                            {

                                var TheBak = db.dvgxTheXe_Backups.Single(o => o.ID == item.TheCuBackupID);
                                RestoreThe(TheCu, TheBak);
                            }
                            else // Trường hợp Hủy thẻ, Đổi thẻ
                            {
                                try
                                {
                                    if (TheCu.IsSaoLuu.GetValueOrDefault()) // Nếu có sao lưu => đã ngưng sử dụng hoàn toàn thẻ đó
                                    {

                                        var the = db.dvgxTheXes.FirstOrDefault(o => o.ID == TheCu.MaTheMoi);

                                        // Chuyển lịch sử của thẻ hiện tại về lại cho thẻ cũ
                                        foreach (var ls in the.dvgxTheXe_LichSuCapNhats)
                                            ls.MaThe = TheCu.ID;

                                        db.dvgxTheXes.DeleteOnSubmit(the);
                                        db.SubmitChanges();
                                    }

                                    var TheBak = db.dvgxTheXe_Backups.Single(o => o.ID == item.TheCuBackupID);
                                    RestoreThe(TheCu, TheBak);
                                }
                                catch (Exception ex)
                                {
                                    DialogBox.Error(ex.Message);
                                }
                            }
                        }
                    }

                    db.KhoThe_DSCapThes.DeleteOnSubmit(tx);
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
                var _MaMB = (int?)gvGiuXe.GetFocusedRowCellValue("MaMB");

                if (gvGiuXe.GetFocusedRowCellValue("ID") == null)
                {
                    gcTheXe.DataSource = null;
                    return;
                }

                var _ID = int.Parse(gvGiuXe.GetFocusedRowCellValue("ID").ToString());

                switch (tabMain.SelectedTabPageIndex)
                {
                    case 0:
                        gcTheXe.DataSource = (from tx in db.CapThe_ChiTiets
                                              join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX into loaixe from lx in loaixe.DefaultIfEmpty()
                                              join t in db.KhoThe_DSCapThes on tx.MaCapThe equals t.ID
                                              join ldk in db.LoaiDangKies on tx.MaLoaiDK equals ldk.ID
                                              join lt in db.Kho_LoaiThes on tx.LoaiThe equals lt.ID
                                              join cu in db.dvgxTheXes on tx.MaTheCu equals cu.ID into dscu
                                              from cu in dscu.DefaultIfEmpty()
                                              join moi in db.dvgxTheXes on tx.MaThe equals moi.ID into dsmoi
                                              from moi in dsmoi.DefaultIfEmpty()
                                              where tx.MaCapThe == _ID
                                              select new
                                              {
                                                  t.NgayNhap,
                                                  MaThe = moi.SoThe,
                                                  MaTheCu = cu.SoThe,
                                                  lt.TenLoaiThe,
                                                  TenLX = tx.LoaiThe == 1 ? "" : lx.TenLX,
                                                  BienSo = tx.LoaiThe == 1 ? "" : tx.BienSo,
                                                  tx.ChuThe,
                                                  PhiGiuXe = tx.LoaiThe == 1 ? null : tx.PhiGiuXe,
                                                  KyTT = tx.LoaiThe == 1 ? null : tx.KyTT,
                                                  TienTT = tx.LoaiThe == 1 ? null : tx.TienTT,
                                                  NgayTT = tx.LoaiThe == 1 ? null : tx.NgayTT,
                                                  ldk.LoaiDK,
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
            using(var db = new MasterDataContext())
            {
                lkLoaiDK.DataSource = db.LoaiDangKies.ToList();
            }
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            tabMain.SelectedTabPageIndex = 0;
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var db = new MasterDataContext();
            var maTN = (byte)itemToaNha.EditValue;
            e.QueryableSource = from gx in db.KhoThe_DSCapThes
                                join mb in db.mbMatBangs on gx.MaMB equals mb.MaMB
                                join kh in db.tnKhachHangs on gx.MaKH equals kh.MaKH
                                join nvn in db.tnNhanViens on gx.NguoiNhap equals nvn.MaNV
                                join nvs in db.tnNhanViens on gx.NguoiSua equals nvs.MaNV into tblNguoiSua
                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                where gx.MaTN == maTN
                                select new
                                {
                                    gx.ID,
                                    SoDK = gx.SoCT,
                                    mb.MaSoMB,
                                    TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                    SoLuong = (from tx in db.CapThe_ChiTiets where tx.MaCapThe == gx.ID select tx).Count(),
                                    gx.Email,
                                    gx.TenChuThe,
                                    NguoiNhap = nvn.HoTenNV,
                                    NgayDK = gx.NgayNhap,
                                    NguoiSua = nvs.HoTenNV,
                                    gx.NgaySua,
                                    gx.NgayNhap,
                                    LoaiDK = gx.CapThe_ChiTiets.FirstOrDefault().MaLoaiDK,
                                    gx.GhiChu,
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

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvGiuXe.GetFocusedRowCellValue("ID") == null)
            {
                DialogBox.Error("Vui lòng chọn phiếu cấp thẻ");
                return;
            }

            var id = int.Parse(gvGiuXe.GetFocusedRowCellValue("ID").ToString());

            var rpt = new PhieuDangKy(id);
            rpt.ShowPreviewDialog();
        }

        private void itemPrintMau2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvGiuXe.GetFocusedRowCellValue("ID") == null)
            {
                DialogBox.Error("Vui lòng chọn phiếu cấp thẻ");
                return;
            }

            var id = int.Parse(gvGiuXe.GetFocusedRowCellValue("ID").ToString());

            var rpt = new PhieuNhanThe(id);
            rpt.ShowPreviewDialog();
        }
    }
}