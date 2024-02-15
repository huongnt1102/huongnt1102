using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Library.NoticeCtl
{
    public partial class YeuCauCtl : DevExpress.XtraEditors.XtraUserControl
    {
     
        public tnNhanVien objnhanvien;

        public YeuCauCtl()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            DevExpress.Data.Helpers.ServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;
            gcYeuCau.DataSource = null;
            gcYeuCau.DataSource = linqInstantFeedbackSource1;
        }

        private void YeuCauCtl_Load(object sender, EventArgs e)
        {
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            LoadData();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
           //if (objnhanvien.IsSuperAdmin.Value)
           // {
           //    using(var db=new MasterDataContext())
           //    {
           //        var query = (from p in db.tnycYeuCaus
           //            //join nk in db.tnNhanKhaus on p.MaKH equals nk.MaKH
           //            //    into nhanKhau
           //            //from nk in nhanKhau.DefaultIfEmpty()
           //            where p.MaTN == (byte?) itemToaNha.EditValue & p.tnycTrangThai.MaTT < 3
           //            orderby p.NgayYC descending
           //            select new
           //            {
           //                p.MaYC,
           //                p.NgayYC,
           //                p.TieuDe,
           //                p.tnycTrangThai.TenTT,
           //                p.tnPhongBan.TenPB,
           //                //TenKH = p.MaKH == null ? "" : "",//p.tnNhanKhau.HoTenNK,
           //                TenKH = p.MaKH != null ? (db.tnNhanKhaus.FirstOrDefault(nk=>nk.MaKH.Equals(p.MaKH)).HoTenNK ?? "") : "",
           //                HoTenNTN = p.MaNTN != null ? p.tnNhanVien1.HoTenNV : "",
           //                p.tnNhanVien.HoTenNV,
           //                p.tnycTrangThai.MauNen,
           //                p.tnycDoUuTien.TenDoUuTien,
           //                MaSoMB = p.mbMatBang == null ? "" : p.mbMatBang.MaSoMB,
           //            }).ToList();
           //         e.QueryableSource = query.AsQueryable();
           //    }
           // }
           // else
           // {
               // using(var db=new MasterDataContext())
               //{
               //    var query = (from p in db.tnycYeuCaus
               //        join nk in db.tnNhanKhaus on p.MaKH equals nk.MaKH into nhanKhau
               //        from nk in nhanKhau.DefaultIfEmpty()
               //        where p.MaBP == objnhanvien.MaPB
               //              & p.tnycTrangThai.MaTT != 3
               //        orderby p.NgayYC descending
               //        select new
               //        {
               //            p.MaYC,
               //            p.NgayYC,
               //            p.TieuDe,
               //            p.tnycTrangThai.TenTT,
               //            p.tnPhongBan.TenPB,
               //            TenKH = p.MaKH == null ? "" : nk.HoTenNK,
               //            HoTenNTN = p.MaNTN != null ? p.tnNhanVien1.HoTenNV : "",
               //            p.tnNhanVien.HoTenNV,
               //            p.tnycTrangThai.MauNen,
               //            p.tnycDoUuTien.TenDoUuTien,
               //            MaSoMB = p.mbMatBang == null ? "" : p.mbMatBang.MaSoMB,
               //        }).ToList();
               //     e.QueryableSource = query.AsQueryable();
               // }
            //}
        }
    }
}
