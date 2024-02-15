using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Data.Linq;
//using DichVu.ChinhSach;
using System.Data.Linq.SqlClient;

namespace DIPCRM.PriceAlert
{
    public partial class frmKhuyenMai : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public byte? MaTN;
        public int? MaMB;
        public int? MaKH;
        public int? MaPL = 0;
        public decimal? ThoiHanHD;
        public decimal? KyTT;
        public int MaHinhThuc;
        public DateTime date;
        //public kmChiTiet_CongThuc objGia;

        public frmKhuyenMai(DateTime _date)
        {
            InitializeComponent();
            date = _date;
            this.Load += frmGiaThue_Load;
            gvCongThuc.FocusedRowChanged += gvCongThuc_FocusedRowChanged;
        }

        void gvCongThuc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gcCongThucChiTiet.DataSource = null;

            var id = (int?)gvCongThuc.GetFocusedRowCellValue("ID");

            if (id == null) return;

            //gcCongThucChiTiet.DataSource = db.kmChiTiet_CongThucChiTiets.Where(o => o.MaKMCongThuc == id);
        }

        void frmGiaThue_Load(object sender, EventArgs e)
        {
            //lkHinhThuc.DataSource = db.kmHinhThucs;
            //lkDVTChiTiet.DataSource = db.kmDonViTinhs;

            List<int?> ltID = new List<int?>();

            //var ltChiTiet = from ct in db.kmChiTiets
            //                join p in db.kmKhuyenMais on ct.MaKM equals p.ID
            //                where SqlMethods.DateDiffDay(p.TuNgay, date) >= 0 
            //                & SqlMethods.DateDiffDay(date, p.DenNgay) >= 0

            //                & (ct.MaPL.GetValueOrDefault() == MaPL | ct.MaPL == null)
            //                & p.kmToaNhas.Any(o => o.MaTN == this.MaTN)
            //                & !p.NgungSuDung.GetValueOrDefault()
            //                & ct.CateID != null
            //                select new
            //                {
            //                    ct.ID,
            //                    ct.MaPL,
            //                    ct.CateID,
            //                    ct.kmChiTiet_DoiTuongApDungs,
            //                };

            var objKH = db.tnKhachHangs.Single(p => p.MaKH == MaKH);
            var objMB = db.mbMatBangs.Single(p => p.MaMB == MaMB );

            //foreach (var ct in ltChiTiet)
            //{
            //    bool IsApDung = false;

            //    switch ((Category)ct.CateID)
            //    {
            //        case Category.SoGheNgoi:
            //            IsApDung = ct.kmChiTiet_DoiTuongApDungs.Any(o => o.LinkID == objMB.idSoGheNgoi);
            //            break;
            //        case Category.Ghe:
            //            IsApDung = ct.kmChiTiet_DoiTuongApDungs.Any(o => o.LinkID == objMB.MaMB.ToString());
            //            break;
            //        case Category.Phong:
            //            IsApDung = ct.kmChiTiet_DoiTuongApDungs.Any(o => o.LinkID == objMB.MaMB.ToString());
            //            break;
            //        case Category.KhachHang:
            //            IsApDung = ct.kmChiTiet_DoiTuongApDungs.Any(o => o.LinkID == objKH.MaKH.ToString());
            //            break;
            //        case Category.LoaiPhong:
            //            IsApDung = ct.kmChiTiet_DoiTuongApDungs.Any(o => o.LinkID == objMB.MaLMB.ToString());
            //            break;
            //        case Category.MoTaPhong:
            //            IsApDung = ct.kmChiTiet_DoiTuongApDungs.Any(o => o.LinkID == objMB.idMoTaPhong);
            //            break;
            //        case Category.LoaiKhachHang:
            //            IsApDung = ct.kmChiTiet_DoiTuongApDungs.Any(o => o.LinkID == objKH.MaLoaiKH.ToString());
            //            break;
            //    }

            //    if (IsApDung)
            //        ltID.Add(ct.ID);
            //}

            //var lt = from p in db.kmChiTiet_CongThucs
            //         join ct in db.kmChiTiets on p.MaKMCT equals ct.ID
            //         join km in db.kmKhuyenMais on ct.MaKM equals km.ID
            //         where ltID.Contains(p.MaKMCT)
            //         & ((p.SoLuong <= ThoiHanHD & p.MaHinhThuc == 1) | (p.SoLuong <= KyTT & p.MaHinhThuc == 2))
            //         & (p.MaHinhThuc == MaHinhThuc | MaHinhThuc == 0)
            //         select new
            //         {
            //             p.ID,
            //             TenChinhSach = string.Format("[{0}] {1}", km.TenChinhSach, km.DienGiai),
            //             p.MaHinhThuc,
            //             p.SoLuong,
            //         };

            //var MaxSoLuong = lt.Max(o => o.SoLuong);

            //gcCongThuc.DataSource = lt.Where(o => o.SoLuong == MaxSoLuong);

        }

        private void gvLoaiGiaThue_DoubleClick(object sender, EventArgs e)
        {
            var id = (int?)gvCongThuc.GetFocusedRowCellValue("ID");

            if (id == null) return;

            //objGia = db.kmChiTiet_CongThucs.Single(o => o.ID == id);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}