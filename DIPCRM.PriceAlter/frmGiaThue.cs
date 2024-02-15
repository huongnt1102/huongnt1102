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
    public partial class frmGiaThue : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public int? MaMB;
        public int? MaKH;
        public byte? MaTN;
        int MaLDV = 2;
        public DateTime? date;
        //public kmGiaThue objGia;

        public frmGiaThue(DateTime _date, int MaLDV)
        {
            InitializeComponent();

            date = _date;
            this.MaLDV = MaLDV;

            this.Load += frmGiaThue_Load;
        }

        void frmGiaThue_Load(object sender, EventArgs e)
        {
            List<int?> ltID = new List<int?>();

            //var ltKM = (from p in db.kmKhuyenMais
            //            join ct in db.kmChiTiets on p.ID equals ct.MaKM
            //            where SqlMethods.DateDiffDay(p.TuNgay, date) >= 0 & SqlMethods.DateDiffDay(date, p.DenNgay) >= 0
            //            & ct.CateID != null
            //            & p.kmToaNhas.Any(o => o.MaTN == this.MaTN)
            //            & !p.NgungSuDung.GetValueOrDefault()
            //            select ct);

            var objKH = db.tnKhachHangs.Single(p => p.MaKH == MaKH);

            var objMB = db.mbMatBangs.Single(p => p.MaMB == MaMB );

                //foreach (var ct in ltKM)
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

                //// Xét cho mặt bằng
                //gcGiaThue.DataSource = from p in db.kmGiaThues
                //                       join ct in db.kmChiTiets on p.idkmChiTiet equals ct.ID
                //                       join km in db.kmKhuyenMais on ct.MaKM equals km.ID
                //                       join dvt in db.DonViTinhs on p.MaDVT equals dvt.ID
                //                       join lt in db.LoaiTiens on p.MaLT equals lt.ID
                //                       where ltID.Contains(p.idkmChiTiet)
                //                       & p.MaLDV == this.MaLDV
                //                       select new
                //                       {
                //                           p.ID,
                //                           TenChinhSach = string.Format("[{0}] {1}", km.TenChinhSach, km.DienGiai),
                //                           p.DonGia,
                //                           dvt.TenDVT,
                //                           lt.TenLT,
                //                           p.DienGiai,
                //                       };

        }

        private void gvLoaiGiaThue_DoubleClick(object sender, EventArgs e)
        {

            var id = (int?)gvLoaiGiaThue.GetFocusedRowCellValue("ID");

            if (id == null) return;

            //objGia = db.kmGiaThues.Single(o => o.ID == id);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}