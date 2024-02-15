using System;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace DichVu.YeuCau
{
    public partial class frmTinhHinhCongViec_ViewItem: XtraForm
    {
        public byte? MaTn { get; set; }
        public int? MaTt { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }

        private MasterDataContext _db;

        public frmTinhHinhCongViec_ViewItem()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();
            switch (MaTt)
            {
                case 99:
                    gc.DataSource = (from p in _db.tnycYeuCaus
                                     join ncv in _db.app_GroupProcesses on p.GroupProcessId equals ncv.Id into ncviec
                                     from ncv in ncviec.DefaultIfEmpty()
                                     join nv in _db.tnNhanViens on p.MaNV equals nv.MaNV into nvs
                                     from nv in nvs.DefaultIfEmpty()
                                     join ntn in _db.tnNhanViens on p.MaNTN equals ntn.MaNV into ntns
                                     from ntn in ntns.DefaultIfEmpty()
                                     where SqlMethods.DateDiffDay(TuNgay, p.NgayYC.Value) >= 0
                                           & SqlMethods.DateDiffDay(p.NgayYC.Value, DenNgay) >= 0
                                           & p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == MaTn
                                     orderby p.NgayYC descending
                                     select new
                                     {
                                         p.MaTN,
                                         p.tnToaNha.TenTN,
                                         p.ID,
                                         p.MaYC,
                                         p.NgayYC,
                                         p.TieuDe,
                                         p.NoiDung,
                                         p.Rating,
                                         p.RatingComment,
                                         p.tnycTrangThai.TenTT,
                                         p.tnPhongBan.TenPB,
                                         TenKH = p.NguoiGui,
                                         HoTenNTN = ntn.HoTenNV,
                                         HoTenNV = nv.HoTenNV,
                                         p.tnycTrangThai.MauNen,
                                         p.tnycDoUuTien.TenDoUuTien,
                                         p.mbMatBang.MaSoMB,
                                         p.tnycNguonDen.TenNguonDen,
                                         TenNhomCongViec = ncv.Name,
                                         ThoiGian = p.tnycLichSuCapNhats.OrderByDescending(_=>_.NgayCN).FirstOrDefault().NgayCN==null?0:SqlMethods.DateDiffHour(p.NgayYC,p.tnycLichSuCapNhats.OrderByDescending(_=>_.NgayCN).First().NgayCN),
                                             
                                         NgayXL = p.tnycLichSuCapNhats.Where(_=>_.MaTT==3).OrderByDescending(_=>_.NgayCN).FirstOrDefault().NgayCN,
                                            
                                         NgayDong = p.tnycLichSuCapNhats.Where(_=>_.MaTT==5).OrderByDescending(_=>_.NgayCN).FirstOrDefault().NgayCN,
                                             
                                         NoiDungXuLy = p.tnycLichSuCapNhats.Where(_=>_.MaTT==3).OrderByDescending(_=>_.NgayCN).FirstOrDefault().NoiDung,
                                         p.NgayBatDauTinh,
                                         p.NgayHanCuoiHoanThanh,
                                         GhiChuTre = SqlMethods.DateDiffHour(p.NgayHanCuoiHoanThanh ?? DateTime.Now, p.tnycLichSuCapNhats.Where(_ => _.MaTT == 3).OrderByDescending(_ => _.NgayCN).FirstOrDefault().NgayCN ?? DateTime.Now) > 0?"Trễ":""
                                     }).ToList();
                    break;
                case 98:
                    gc.DataSource = (from p in _db.tnycYeuCaus
                                     join ncv in _db.app_GroupProcesses on p.GroupProcessId equals ncv.Id into ncviec
                                     from ncv in ncviec.DefaultIfEmpty()
                                     join nv in _db.tnNhanViens on p.MaNV equals nv.MaNV into nvs
                                     from nv in nvs.DefaultIfEmpty()
                                     join ntn in _db.tnNhanViens on p.MaNTN equals ntn.MaNV into ntns
                                     from ntn in ntns.DefaultIfEmpty()
                                     where SqlMethods.DateDiffDay(TuNgay, p.NgayYC.Value) >= 0
                                           & SqlMethods.DateDiffDay(p.NgayYC.Value, DenNgay) >= 0
                                           & p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == MaTn
                                           & SqlMethods.DateDiffHour(p.NgayHanCuoiHoanThanh ?? DateTime.Now, p.tnycLichSuCapNhats.Where(_ => _.MaTT == 3).OrderByDescending(_ => _.NgayCN).FirstOrDefault().NgayCN ?? DateTime.Now) > 0
                                     orderby p.NgayYC descending
                                     select new
                                     {
                                         p.MaTN,
                                         p.tnToaNha.TenTN,
                                         p.ID,
                                         p.MaYC,
                                         p.NgayYC,
                                         p.TieuDe,
                                         p.NoiDung,
                                         p.Rating,
                                         p.RatingComment,
                                         p.tnycTrangThai.TenTT,
                                         p.tnPhongBan.TenPB,
                                         TenKH = p.NguoiGui,
                                         HoTenNTN = ntn.HoTenNV,
                                         HoTenNV = nv.HoTenNV,
                                         p.tnycTrangThai.MauNen,
                                         p.tnycDoUuTien.TenDoUuTien,
                                         p.mbMatBang.MaSoMB,
                                         p.tnycNguonDen.TenNguonDen,
                                         TenNhomCongViec = ncv.Name,
                                         ThoiGian = p.tnycLichSuCapNhats.OrderByDescending(_ => _.NgayCN).FirstOrDefault().NgayCN == null ? 0 : SqlMethods.DateDiffHour(p.NgayYC, p.tnycLichSuCapNhats.OrderByDescending(_ => _.NgayCN).First().NgayCN),

                                         NgayXL = p.tnycLichSuCapNhats.Where(_ => _.MaTT == 3).OrderByDescending(_ => _.NgayCN).FirstOrDefault().NgayCN,

                                         NgayDong = p.tnycLichSuCapNhats.Where(_ => _.MaTT == 5).OrderByDescending(_ => _.NgayCN).FirstOrDefault().NgayCN,

                                         NoiDungXuLy = p.tnycLichSuCapNhats.Where(_ => _.MaTT == 3).OrderByDescending(_ => _.NgayCN).FirstOrDefault().NoiDung,
                                         p.NgayBatDauTinh,
                                         p.NgayHanCuoiHoanThanh,
                                         GhiChuTre = SqlMethods.DateDiffHour(p.NgayHanCuoiHoanThanh ?? DateTime.Now, p.tnycLichSuCapNhats.Where(_ => _.MaTT == 3).OrderByDescending(_ => _.NgayCN).FirstOrDefault().NgayCN ?? DateTime.Now) > 0 ? "Trễ" : ""
                                     }).ToList();
                    break;
                default:
                    gc.DataSource = (from p in _db.tnycYeuCaus
                join ncv in _db.app_GroupProcesses on p.GroupProcessId equals ncv.Id into ncviec
                from ncv in ncviec.DefaultIfEmpty()
                join nv in _db.tnNhanViens on p.MaNV equals nv.MaNV into nvs
                from nv in nvs.DefaultIfEmpty()
                join ntn in _db.tnNhanViens on p.MaNTN equals ntn.MaNV into ntns
                from ntn in ntns.DefaultIfEmpty()
                where SqlMethods.DateDiffDay(TuNgay, p.NgayYC.Value) >= 0
                      & SqlMethods.DateDiffDay(p.NgayYC.Value, DenNgay) >= 0
                      & p.mbMatBang.mbTangLau.mbKhoiNha.MaTN==MaTn
                      &MaTt==(p.MaTT??1)
                orderby p.NgayYC descending
                select new
                {
                    p.MaTN,
                    p.tnToaNha.TenTN,
                    p.ID,
                    p.MaYC,
                    p.NgayYC,
                    p.TieuDe,
                    p.NoiDung,
                    p.Rating,
                    p.RatingComment,
                    p.tnycTrangThai.TenTT,
                    p.tnPhongBan.TenPB,
                    TenKH = p.NguoiGui,
                    HoTenNTN = ntn.HoTenNV,
                    HoTenNV = nv.HoTenNV,
                    p.tnycTrangThai.MauNen,
                    p.tnycDoUuTien.TenDoUuTien,
                    p.mbMatBang.MaSoMB,
                    p.tnycNguonDen.TenNguonDen,
                    TenNhomCongViec = ncv.Name,
                    ThoiGian = p.tnycLichSuCapNhats.OrderByDescending(_ => _.NgayCN).FirstOrDefault().NgayCN == null ? 0 : SqlMethods.DateDiffHour(p.NgayYC, p.tnycLichSuCapNhats.OrderByDescending(_ => _.NgayCN).First().NgayCN),

                    NgayXL = p.tnycLichSuCapNhats.Where(_ => _.MaTT == 3).OrderByDescending(_ => _.NgayCN).FirstOrDefault().NgayCN,

                    NgayDong = p.tnycLichSuCapNhats.Where(_ => _.MaTT == 5).OrderByDescending(_ => _.NgayCN).FirstOrDefault().NgayCN,

                    NoiDungXuLy = p.tnycLichSuCapNhats.Where(_ => _.MaTT == 3).OrderByDescending(_ => _.NgayCN).FirstOrDefault().NoiDung,
                    p.NgayBatDauTinh,
                    p.NgayHanCuoiHoanThanh,
                    GhiChuTre = SqlMethods.DateDiffHour(p.NgayHanCuoiHoanThanh ?? DateTime.Now, p.tnycLichSuCapNhats.Where(_ => _.MaTT == 3).OrderByDescending(_ => _.NgayCN).FirstOrDefault().NgayCN ?? DateTime.Now) > 0 ? "Trễ" : ""
                }).ToList();
                    break;
            }

            gv.BestFitColumns();
        }

        private void itemHuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle && e.RowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                if (e.Column.FieldName == "TenTT")
                {
                    e.Appearance.BackColor = Color.FromArgb((int)gv.GetRowCellValue(e.RowHandle, "MauNen"));
                }
            }
        }

        //private void gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        //{
        //    if (e.RowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle && e.RowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
        //    {
        //        if (e.Column.FieldName == "StatusLevelID")
        //        {
        //            e.Appearance.BackColor = Color.FromArgb((int)gv.GetRowCellValue(e.RowHandle, "Color"));
        //        }
        //    }
        //}
    }
}