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
using System.Data.Linq.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
namespace Building.Asset.BaoTri
{
    public partial class frmProfile_Manager : XtraForm
    {
        private MasterDataContext _db;

        public frmProfile_Manager()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }
        #region Code
        private void LoadData()
        {
            if (beiToaNha.EditValue != null)
            {
                var db = new MasterDataContext();
                var sql = from ds in db.tbl_Profiles
                          join nd in db.tnNhanViens on ds.NguoiDuyet equals nd.MaNV into nduyet
                          from nd in nduyet.DefaultIfEmpty() 
                          where ds.MaTN==(byte?)beiToaNha.EditValue
                          select new 
                          {
                              ID = ds.ID,
                              GhiChu = ds.GhiChu,
                              TenMau = ds.TenMau,
                              TenLoai = ds.tbl_Profile_Loai.Ten,
                              ds.IsDuyet,
                              ds.NgayDuyet,
                              ds.GhiChuDuyet,
                              NguoiDuyet=nd.HoTenNV
                          };
                gc.DataSource = sql;
            }
            LoadDetail();
        }
        #endregion


        private void frmManager_Load(object sender, EventArgs e)
        {
            var objKbc = new KyBaoCao();
            var db = new MasterDataContext();
            _db = new MasterDataContext();

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;
            LoadData();

        }

        private void LoadDetail()
        {
            var db = new MasterDataContext();

            var id = (int?)gv.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                gcChiTiet.DataSource = null;
                return;
            }
            switch (xtraTabDetail.SelectedTabPage.Name)
            {
                case "tabChiTiet":
                    gcChiTiet.DataSource = (from p in db.tbl_Profile_ChiTiets
                                            where p.ProfileID == id
                                            select new
                                            {
                                                p.TenCongViec,
                                                p.TieuChuan,
                                                p.GiaTriChon,
                                                p.GiaTriNhap,
                                                p.TenNhomCongViec,p.IsHinhAnh,p.DonViTinh
                                            }).ToList();
                    gvChiTiet.ExpandAllGroups();
                    break;
                case "tpLichSu":
                    gcLS.DataSource = (from ds in db.tbl_Profile_Logs
                                      join nd in db.tnNhanViens on ds.NguoiDuyet equals nd.MaNV into nduyet
                                      from nd in nduyet.DefaultIfEmpty()
                                      join l in db.tbl_Profile_Loais on ds.LoaiID equals l.ID
                                       where ds.ProfileParentID == (int?)gv.GetFocusedRowCellValue("ID")
                                       select new
                                      {
                                          ID = ds.ID,
                                          GhiChu = ds.GhiChu,
                                          TenMau = ds.TenMau,
                                          TenLoai = l.Ten,
                                          ds.IsDuyet,
                                          ds.NgayDuyet,
                                          ds.GhiChuDuyet,
                                          NguoiDuyet = nd.HoTenNV
                                      }).ToList();


                    loadLogChiTiet();
                    break;
            }

        }

        private void grvLS_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadLogChiTiet();
        }
        void loadLogChiTiet()
        {
            var db = new MasterDataContext();
            gcLSCT.DataSource = (from p in db.tbl_Profile_ChiTiet_Logs
                                 where p.ProfileID == Convert.ToInt32(grvLS.GetFocusedRowCellValue("ID"))
                                 select new
                                 {
                                     p.TenCongViec,
                                     p.TieuChuan,
                                     p.GiaTriChon,
                                     p.GiaTriNhap,
                                     p.TenNhomCongViec,p.IsHinhAnh
                                 }).ToList();
            grvLSCT.ExpandAllGroups();
        }
        private class ProfileToaNhaClass
        {
            public int? ID { set; get; }
            public string TenMau { set; get; }
            public string GhiChu { set; get; }
            public string TenLoai { set; get; }
            public bool? IsSuDung { set; get; }

        }
        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }                          

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void bbiThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            var id = 0;
            if (gv.GetFocusedRowCellValue("ID") != null)
            {
                id = (int)gv.GetFocusedRowCellValue("ID");
            }
            using (var frm = new frmProfile_Edit { MaTn = (byte?)beiToaNha.EditValue, IsSua = 0 })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }

        }

        private void bbiSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            var id = 0;
            if (gv.GetFocusedRowCellValue("ID") != null)
            {
                id = (int)gv.GetFocusedRowCellValue("ID");
            }
            using (var frm = new frmProfile_Edit { MaTn = (byte?)beiToaNha.EditValue, IsSua = 1, Id = id })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }

        }

        private void bbiXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
            int[] indexs = gv.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                return;
            }
            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            foreach (var r in indexs)
            {
                //kiểm tra bảng tbl_PhieuVanHanh_ChiTiets

                var o = db.tbl_Profiles.SingleOrDefault(
           p => p.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                if (o != null)
                {
                    db.tbl_Profile_ChiTiets.DeleteAllOnSubmit(o.tbl_Profile_ChiTiets);
                    db.tbl_Profiles.DeleteOnSubmit(o);
                }

            }
            db.SubmitChanges();
            DialogBox.Success("Xóa thành công!");
            LoadData();

        }

        private void xtraTabDetail_Click(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void gvDanhSachYeuCau_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void gv_RowClick(object sender, RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void beiToaNha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
        }

        private void itemDuyetProfile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.GetFocusedRowCellValue("ID") != null)
            {
                int id = Convert.ToInt32(gv.GetFocusedRowCellValue("ID"));
                frmDuyetProfile frm = new frmDuyetProfile();
                frm.ShowDialog();
                if (frm.IsSave)
                {
                    var objP = _db.tbl_Profiles.FirstOrDefault(p => p.ID == id);
                    objP.IsDuyet = frm.IsDuyet;
                    objP.NguoiDuyet = Common.User.MaNV;
                    objP.NgayDuyet = Common.GetDateTimeSystem();
                    objP.GhiChuDuyet = frm.GhiChu;
                    _db.SubmitChanges();
                    LoadData();
                }
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn Profile cần duyệt");
            }
        }

    }
}