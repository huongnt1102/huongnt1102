using System;
using System.Drawing;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace Building.Asset.BaoCao
{
    public partial class frmBaoCao_CheckListVanHanhHangNgay_ViewItem: XtraForm
    {
        public string Id { get; set; } // list id

        private MasterDataContext _db;

        public frmBaoCao_CheckListVanHanhHangNgay_ViewItem()
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
            var strId = (Id ?? "").TrimEnd(',').TrimStart(',').Replace(" ", "");
            var listId = strId.Split(',');

            lkNhanVien.DataSource = _db.tnNhanViens;

            repStatusLevels.DataSource = _db.tbl_PhieuVanHanh_Status_Levels;

            // hệ thống
            var objKH1 = (from kh in _db.tbl_PhieuVanHanhs
                          join ht in _db.tbl_NhomTaiSans on kh.NhomTaiSanID equals ht.ID
                          where listId.Contains(kh.ID.ToString())
                                && kh.IsTenTaiSan.GetValueOrDefault() == false

                                & kh.LoaiHeThong == 1
                                & (kh.IsPhieuBaoTri==false||kh.IsPhieuBaoTri==null)
                          select new
                          {
                              kh.ID,
                              TenHeThong = ht.TenNhomTaiSan,
                              kh.NgayPhieu,
                              kh.NgayNhap,
                              kh.SoPhieu,
                              kh.DenNgay,
                              kh.TuNgay,
                              kh.NguoiNhap,
                              kh.NguoiSua,
                              kh.NgaySua,
                              kh.PhanLoaiCaID,
                              idTrangThai = kh.TrangThaiPhieu ?? 0,
                              kh.TrangThaiPhieu,
                              kh.NgayDuyet,
                              kh.GhiChuDuyet,
                              KhoiNha = "",
                              kh.HeThongTaiSanID,
                              kh.tbl_KeHoachVanHanh.SoKeHoach,
                              kh.NguoiThucHien,
                              kh.NguoiDuyet,
                              kh.NgayThucHien,
                              kh.NguoiTiepNhan,
                              kh.NgayTiepNhan,
                              kh.GhiChuTiepNhan,
                              kh.NgayHoanThanh,
                              kh.NguoiHoanThanh,
                              kh.GhiChuHoanThanh,
                              StatusLevelID = kh.StatusLevelID ?? 1,
                              Color = kh.StatusLevelID != null
                                  ? int.Parse(kh.tbl_PhieuVanHanh_Status_Level.Color)
                                  : int.Parse(_db.tbl_PhieuVanHanh_Status_Levels.First().Color),
                              kh.LoaiHeThong,
                              IsBatThuong = kh.IsBatThuong == true ? "Bất thường" : "Bình thường"
                          }).ToList();
            // loại tài sản
            var objByLoaiTaiSan = (from kh in _db.tbl_PhieuVanHanhs
                                   join lts in _db.tbl_LoaiTaiSans on kh.LoaiTaiSanID equals lts.ID
                                   where listId.Contains(kh.ID.ToString())
                                         && kh.LoaiHeThong == 2

                                         && (kh.IsPhieuBaoTri == false || kh.IsPhieuBaoTri == null)
                                   select new
                                   {
                                       kh.ID,
                                       TenHeThong = lts.TenLoaiTaiSan,
                                       kh.NgayPhieu,
                                       kh.NgayNhap,
                                       kh.SoPhieu,
                                       kh.DenNgay,
                                       kh.TuNgay,
                                       kh.NguoiNhap,
                                       kh.NguoiSua,
                                       kh.NgaySua,
                                       kh.PhanLoaiCaID,
                                       idTrangThai = kh.TrangThaiPhieu ?? 0,
                                       kh.TrangThaiPhieu,
                                       kh.NgayDuyet,
                                       kh.GhiChuDuyet,
                                       KhoiNha = "",
                                       kh.HeThongTaiSanID,
                                       kh.tbl_KeHoachVanHanh.SoKeHoach,
                                       kh.NguoiThucHien,
                                       kh.NguoiDuyet,
                                       kh.NgayThucHien,
                                       kh.NguoiTiepNhan,
                                       kh.NgayTiepNhan,
                                       kh.GhiChuTiepNhan,
                                       kh.NgayHoanThanh,
                                       kh.NguoiHoanThanh,
                                       kh.GhiChuHoanThanh,
                                       StatusLevelID = kh.StatusLevelID ?? 1,
                                       Color = kh.StatusLevelID != null
                                           ? int.Parse(kh.tbl_PhieuVanHanh_Status_Level.Color)
                                           : int.Parse(_db.tbl_PhieuVanHanh_Status_Levels.First().Color),
                                       kh.LoaiHeThong,
                                       IsBatThuong = kh.IsBatThuong == true ? "Bất thường" : "Bình thường"
                                   }).ToList();
            // tên tài sản
            var objKH2 = (from kh in _db.tbl_PhieuVanHanhs
                          join ht in _db.tbl_TenTaiSans on kh.TenTaiSanID equals ht.ID
                          join kn in _db.mbKhoiNhas on ht.BlockID equals kn.MaKN into _knha
                          from kn in _knha.DefaultIfEmpty()
                          where listId.Contains(kh.ID.ToString())
                                && kh.IsTenTaiSan.GetValueOrDefault() == true

                                && (kh.IsPhieuBaoTri == false || kh.IsPhieuBaoTri == null)
                                & kh.LoaiHeThong == 3
                          select new
                          {
                              kh.ID,
                              TenHeThong = ht.TenTaiSan,
                              kh.NgayPhieu,
                              kh.NgayNhap,
                              kh.SoPhieu,
                              kh.DenNgay,
                              kh.TuNgay,
                              kh.NguoiNhap,
                              kh.NguoiSua,
                              kh.NgaySua,
                              kh.PhanLoaiCaID,
                              idTrangThai = kh.TrangThaiPhieu ?? 0,
                              kh.TrangThaiPhieu,
                              kh.NgayDuyet,
                              kh.GhiChuDuyet,
                              KhoiNha = kn.TenKN,
                              kh.HeThongTaiSanID,
                              kh.tbl_KeHoachVanHanh.SoKeHoach,
                              kh.NguoiThucHien,
                              kh.NguoiDuyet,
                              kh.NgayThucHien,
                              kh.NguoiTiepNhan,
                              kh.NgayTiepNhan,
                              kh.GhiChuTiepNhan,
                              kh.NgayHoanThanh,
                              kh.NguoiHoanThanh,
                              kh.GhiChuHoanThanh,
                              StatusLevelID = kh.StatusLevelID ?? 1,
                              Color = kh.StatusLevelID != null
                                  ? int.Parse(kh.tbl_PhieuVanHanh_Status_Level.Color)
                                  : int.Parse(_db.tbl_PhieuVanHanh_Status_Levels.First().Color),
                              kh.LoaiHeThong,
                              IsBatThuong = kh.IsBatThuong == true ? "Bất thường" : "Bình thường"
                          }).ToList();
            var objKH = objKH1.Concat(objByLoaiTaiSan).Concat(objKH2);

            gc.DataSource = objKH.ToList();
        }

        private void itemHuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle && e.RowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                if (e.Column.FieldName == "StatusLevelID")
                {
                    e.Appearance.BackColor = Color.FromArgb((int)gv.GetRowCellValue(e.RowHandle, "Color"));
                }
            }
            if (e.Column.FieldName == "LoaiBaoHanh")
            {
                if (view != null)
                {
                    var category = view.GetRowCellDisplayText(e.RowHandle, view.Columns["LoaiBaoHanh"]);
                    if (category == "Theo chu kỳ")
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.BackColor2 = Color.White;
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
            }
            if (e.Column.FieldName == "IsBatThuong")
            {
                if (view != null)
                {
                    var category = view.GetRowCellDisplayText(e.RowHandle, view.Columns["IsBatThuong"]);
                    if (category == "Bất thường")
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.BackColor2 = Color.White;
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
            }
        }
    }
}