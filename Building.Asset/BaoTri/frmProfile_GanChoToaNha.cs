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
    public partial class frmProfile_GanChoToaNha : XtraForm
    {
        private MasterDataContext _db;

        public frmProfile_GanChoToaNha()
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
                var sql = from ds in db.tbl_Profile_DMs
                          join nd in db.tnNhanViens on ds.NguoiDuyet equals nd.MaNV into nduyet
                          from nd in nduyet.DefaultIfEmpty()
                          where ds.IsDuyet.GetValueOrDefault() == true
                          select new ProfileToaNhaClass
                          {
                              ID = ds.ID,
                              GhiChu = ds.GhiChu,
                              TenMau = ds.TenMau,
                              TenLoai = ds.tbl_Profile_Loai.Ten,
                              IsSuDung = db.tbl_Profiles.FirstOrDefault(p => p.ProfileDMID == ds.ID && p.MaTN == (byte?)beiToaNha.EditValue) != null ? true : false,
                              IsDuyet=ds.IsDuyet,
                              NgayDuyet=ds.NgayDuyet,
                              GhiChuDuyet=ds.GhiChuDuyet,
                              NguoiDuyet=nd.HoTenNV
                          };
                gc.DataSource = sql;
            }
            LoadDetail();
        }
        #endregion


        private void frmManager_Load(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            _db = new MasterDataContext();

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;
            LkTaiSan.DataSource = db.tbl_NhomTaiSans.Where(_ => _.MaTN == (byte?)beiToaNha.EditValue);
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
                    gcChiTiet.DataSource = (from p in db.tbl_Profile_DM_ChiTiets
                        where p.ProfileID == id
                        select new
                        {
                            p.TenCongViec,
                            p.TieuChuan,
                            p.GiaTriChon,
                            p.GiaTriNhap,
                            p.TenNhomCongViec, p.IsHinhAnh, p.DonViTinh
                        }).ToList();
                    gvChiTiet.ExpandAllGroups();
                    break;
            }

        }
        private class ProfileToaNhaClass
        {
            public int? ID { set; get; }
            public string TenMau { set; get; }
            public string GhiChu { set; get; }
            public string TenLoai { set; get; }
            public bool? IsSuDung { set; get; }
            public bool? IsDuyet { set; get; }
            public string NguoiDuyet { set; get; }
            public DateTime? NgayDuyet { set; get; }
            public string GhiChuDuyet { set; get; }

        }
        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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



        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool _IsLuu = false;
            gv.PostEditor();
            for (int i = 0; i < gv.RowCount; i++)
            {
                if (gv.GetRowCellValue(i, "ID") != null)
                {
                    if (Convert.ToBoolean(gv.GetRowCellValue(i, "IsSuDung")))
                    {
                        int ProfileID = Convert.ToInt32(gv.GetRowCellValue(i, "ID"));
                        var objKT = _db.tbl_Profiles.FirstOrDefault(p => p.MaTN == (byte?)beiToaNha.EditValue && p.ProfileDMID == ProfileID);
                        var objPar = _db.tbl_Profile_DMs.FirstOrDefault(p => p.ID == ProfileID);
                        if (objKT == null)
                        {
                            var objPro = new tbl_Profile();
                            objPro.TenMau = objPar.TenMau;
                            objPro.GhiChu = objPar.GhiChu;
                            objPro.MaTN = (byte?)beiToaNha.EditValue;
                            objPro.LoaiID = objPar.LoaiID;
                            objPro.ProfileDMID = ProfileID;
                            _db.tbl_Profiles.InsertOnSubmit(objPro);
                            //Thêm chi tiết profile
                            foreach (var item in objPar.tbl_Profile_DM_ChiTiets)
                            {
                                var objct = new tbl_Profile_ChiTiet();
                                objct.TenNhomCongViec = item.TenNhomCongViec;
                                objct.TenCongViec = item.TenCongViec;
                                objct.GiaTriChon = item.GiaTriChon;
                                objct.GiaTriNhap = item.GiaTriNhap;
                                objct.TieuChuan = item.TieuChuan;
                                objct.DonViTinh = item.DonViTinh;
                                objPro.tbl_Profile_ChiTiets.Add(objct);
                            }
                        }
                        else
                        {
                            objKT.TenMau = objPar.TenMau;
                            objKT.GhiChu = objPar.GhiChu;
                            objKT.MaTN = (byte?)beiToaNha.EditValue;
                            objKT.LoaiID = objPar.LoaiID;
                            objKT.ProfileDMID = ProfileID;
                        }
                        _IsLuu = true;
                    }
                }
               
            }
            
            if (_IsLuu)
            {
                _db.SubmitChanges();
                DialogBox.Alert("Lưu dữ liệu thành công!");
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn Profile để lưu");
            }
        }
    }
}