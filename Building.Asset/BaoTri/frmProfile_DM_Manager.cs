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
    public partial class frmProfile_DM_Manager : XtraForm
    {
        private MasterDataContext _db;

        public frmProfile_DM_Manager()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }
        #region Code
        private void LoadData()
        {
            var sql = (from ds in _db.tbl_Profile_DMs
                       join n in _db.tbl_Profile_Nhoms on ds.ProfileNhomID equals n.ID into _nhom
                      from n in _nhom.DefaultIfEmpty()
                       join nd in _db.tnNhanViens on ds.NguoiDuyet equals nd.MaNV into _nv
                      from nd in _nv.DefaultIfEmpty()
                      orderby ds.TenMau ascending
                      select new
                      {
                          ds.ID,
                          ds.GhiChu,
                          ds.TenMau,
                          TenLoai = ds.tbl_Profile_Loai.Ten,
                          ds.IsDuyet,
                          ds.NgayDuyet,
                          NguoiDuyet = nd.HoTenNV,
                          ds.GhiChuDuyet,
                          n.TenNhomProfile
                      }).ToList();
            gc.DataSource = sql;
           
            LoadDetail();
        }
        #endregion


        private void frmManager_Load(object sender, EventArgs e)
        {
            var objKbc = new KyBaoCao();
            var db = new MasterDataContext();
            _db = new MasterDataContext();
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            LoadData();

        }

        private void LoadDetail()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (int?)gv.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }
                switch (xtraTabDetail.SelectedTabPage.Name)
                {
                    case "tabChiTiet":
                        gcChiTiet.DataSource = (from p in db.tbl_Profile_DM_ChiTiets
                                                //join pf in db.tbl_Profile_DMs on p.ProfileID equals pf.ID
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
                        var a = db.tbl_Profile_Loais.ToList();
                        var b = _db.tbl_Profile_DM_Logs.Where(x => x.Profile_DMID == id);
                        gcLS.DataSource = (from ds in db.tbl_Profile_DM_Logs
                                           join n in db.tbl_Profile_Nhoms on ds.ProfileNhomID equals n.ID into _nhom
                                           from n in _nhom.DefaultIfEmpty()
                                           join l in db.tbl_Profile_Loais on ds.LoaiID equals l.ID into _lo
                                           from l in _lo.DefaultIfEmpty()
                                           join nd in db.tnNhanViens on ds.NguoiDuyet equals nd.MaNV into _nv
                                           from nd in _nv.DefaultIfEmpty()
                                               //orderby ds.TenMau ascending
                                           where ds.Profile_DMID == id
                                                select new
                                                {
                                                    ds.ID,
                                                    ds.GhiChu,
                                                    ds.TenMau,
                                                    TenLoai = l.Ten,
                                                    ds.IsDuyet,
                                                    ds.NgayDuyet,
                                                    NguoiDuyet = nd.HoTenNV,
                                                    ds.GhiChuDuyet,
                                                    n.TenNhomProfile
                                                }).ToList();
                        loadLogChiTiet();
                        break;
                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //
            }
            finally
            {
                db.Dispose();
            }
        }

        bool cal(Int32 width, GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void bbiThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = 0;
                if (gv.GetFocusedRowCellValue("ID") != null)
                {
                    id = (int)gv.GetFocusedRowCellValue("ID");
                }
                using (var frm = new frmProfile_DM_Edit { IsSua = 0 })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }

            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void bbiSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = 0;
                if (gv.GetFocusedRowCellValue("ID") != null)
                {
                    id = (int)gv.GetFocusedRowCellValue("ID");
                }
                using (var frm = new frmProfile_DM_Edit { IsSua = 1, Id = id })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }

            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void bbiXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
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

                    var o = db.tbl_Profile_DMs.SingleOrDefault(
               p => p.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                    if (o != null)
                    {
                        db.tbl_Profile_DM_ChiTiets.DeleteAllOnSubmit(o.tbl_Profile_DM_ChiTiets);
                        db.tbl_Profile_DMs.DeleteOnSubmit(o);
                    }
                    
                }
                db.SubmitChanges();
                LoadData();
            }
            catch (Exception)
            {

                //throw;
            }
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
        private void itemDuyetProfile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.GetFocusedRowCellValue("ID") != null)
            {
                int id = Convert.ToInt32(gv.GetFocusedRowCellValue("ID"));
                frmDuyetProfile frm = new frmDuyetProfile();
                frm.ShowDialog();
                if (frm.IsSave)
                {
                    var objP = _db.tbl_Profile_DMs.FirstOrDefault(p => p.ID == id);
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

        private void itemCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.GetFocusedRowCellValue("ID") != null)
            {
                int id = Convert.ToInt32(gv.GetFocusedRowCellValue("ID"));
                var objP = _db.tbl_Profile_DMs.FirstOrDefault(p => p.ID == id);
                var objCopy = new tbl_Profile_DM
                {
                    TenMau=objP.TenMau+"-"+"Copy",
                    GhiChu=objP.GhiChu,
                    LoaiID=objP.LoaiID,
                    ProfileNhomID=objP.ProfileNhomID,
                    IsDuyet=false
                };
                foreach (var item in objP.tbl_Profile_DM_ChiTiets)
                {
                    var ct = new tbl_Profile_DM_ChiTiet();
                    ct.GiaTriChon = item.GiaTriChon;
                    ct.GiaTriNhap = item.GiaTriNhap;
                    ct.ProfileID = item.ProfileID;
                    ct.TenCongViec = item.TenCongViec;
                    ct.TieuChuan = item.TieuChuan;
                    ct.TenNhomCongViec = item.TenNhomCongViec;
                    objCopy.tbl_Profile_DM_ChiTiets.Add(ct);
                }
                _db.tbl_Profile_DMs.InsertOnSubmit(objCopy);
                _db.SubmitChanges();
                LoadData();
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn Profile cần copy");
            }
        }

        private void grvLS_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadLogChiTiet();
        }
        void loadLogChiTiet()
        {
            var db = new MasterDataContext();
            gcLSCT.DataSource = (from p in db.tbl_Profile_DM_ChiTiet_Logs
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
    }
}