using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using Building.Asset.VanHanh;

namespace Building.Asset.BaoTri
{
    public partial class frmPhieuBaoTri_Manager : XtraForm
    {
        public int? Id { get; set; }
        private MasterDataContext _db;

        public frmPhieuBaoTri_Manager()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao()
            {
                Index = index
            };
            objKbc.SetToDate();
            beiTuNgay.EditValue = objKbc.DateFrom;
            beiDenNgay.EditValue = objKbc.DateTo;
        }
        private void LoadData()
        {
            try
            {
                gc.DataSource = null;
                if (beiToaNha.EditValue != null && beiTuNgay.EditValue != null && beiDenNgay.EditValue != null)
                {
                    _db = new MasterDataContext();
                    // hệ thống
                    var objKH1 = (from kh in _db.tbl_PhieuVanHanhs
                                  join ht in _db.tbl_NhomTaiSans on kh.NhomTaiSanID equals ht.ID
                                  where (Id != null ? kh.ID == Id : (kh.MaTN == (byte?)beiToaNha.EditValue
                                                                    && kh.IsTenTaiSan.GetValueOrDefault() == false
                                                                    && SqlMethods.DateDiffDay((DateTime)beiTuNgay.EditValue, kh.NgayPhieu) >=
                                                                    0 & SqlMethods.DateDiffDay(kh.NgayPhieu,
                                                                        (DateTime)beiDenNgay.EditValue) >= 0)) && kh.IsPhieuBaoTri.GetValueOrDefault() == true
                                                                   & kh.LoaiHeThong == 1
                                      

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
                                      kh.LoaiHeThong,kh.SoNgayCoTheTre,//NgayHetHanCuoiCung=kh.DenNgay.GetValueOrDefault().AddDays((double)kh.SoNgayCoTheTre.GetValueOrDefault())
                                      kh.NgayHetHanCuoiCung
                                  }).ToList();
                    // loại tài sản
                    var objByLoaiTaiSan = (from kh in _db.tbl_PhieuVanHanhs
                                           join lts in _db.tbl_LoaiTaiSans on kh.LoaiTaiSanID equals lts.ID
                                           where (Id != null ? kh.ID == Id : (kh.MaTN == (byte?)beiToaNha.EditValue
                                                                             
                                                                             && SqlMethods.DateDiffDay((DateTime)beiTuNgay.EditValue, kh.NgayPhieu) >= 0
                                                                             && SqlMethods.DateDiffDay(kh.NgayPhieu, (DateTime)beiDenNgay.EditValue) >= 0
                                                                             )) && kh.LoaiHeThong == 2 && kh.IsPhieuBaoTri.GetValueOrDefault() == true
                                               
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
                                               kh.SoNgayCoTheTre,
                                               //NgayHetHanCuoiCung = kh.DenNgay.GetValueOrDefault().AddDays((double)kh.SoNgayCoTheTre.GetValueOrDefault())
                                               kh.NgayHetHanCuoiCung
                                           }).ToList();
                    // tên tài sản
                    var objKH2 = (from kh in _db.tbl_PhieuVanHanhs
                                  join ht in _db.tbl_TenTaiSans on kh.TenTaiSanID equals ht.ID
                                  join kn in _db.mbKhoiNhas on ht.BlockID equals kn.MaKN into _knha
                                  from kn in _knha.DefaultIfEmpty()
                                  where (Id != null ? kh.ID == Id : (kh.MaTN == (byte?)beiToaNha.EditValue
                                                                    && kh.IsTenTaiSan.GetValueOrDefault() == true
                                                                    && SqlMethods.DateDiffDay((DateTime)beiTuNgay.EditValue, kh.NgayPhieu) >=
                                                                    0 & SqlMethods.DateDiffDay(kh.NgayPhieu, (DateTime)beiDenNgay.EditValue) >= 0
                                                                    )) && kh.IsPhieuBaoTri.GetValueOrDefault() == true
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
                                      kh.SoNgayCoTheTre,
                                      //NgayHetHanCuoiCung = kh.DenNgay.GetValueOrDefault().AddDays((double)kh.SoNgayCoTheTre.GetValueOrDefault())
                                      kh.NgayHetHanCuoiCung
                                  }).ToList();
                    var objKh = objKH1.Concat(objByLoaiTaiSan).Concat(objKH2);

                    gc.DataSource = objKh.ToList();
                   
                }
            }
            catch
            {
                // ignored
            }
            LoadDetail();
        }
        private void RefreshData()
        {
            LoadData();
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            //lueToaNha.DataSource = Common.TowerList;
            
            beiToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            var db = new MasterDataContext();
            lueToaNha.DataSource = db.tnToaNhas;
            foreach (var v in objKbc.Source)
            {
                cbxKBC.Items.Add(v);
            }
            beiKBC.EditValue = objKbc.Source[7];
            SetDate(7);
            lkNhanVien.DataSource = db.tnNhanViens.Select(o => new { o.MaNV, o.HoTenNV }).ToList();
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
                    case "pageChiTietCongViec":
                        gcChiTiet.DataSource = (from p in db.tbl_PhieuVanHanh_ChiTiets
                                                where p.PhieuVanHanhID == id
                                                select new
                                                {
                                                    p.TenCongViec,
                                                    p.TenNhomCongViec,
                                                    p.GiaTriChon,
                                                    GiaTriNhap = p.GiaTriNhap_Nhap,
                                                    p.TieuChuan,
                                                    p.IsChon,
                                                    p.HinhAnh,
                                                    p.GhiChu,
                                                    p.IsHinhAnh
                                                }).ToList();
                        gvChiTiet.ExpandAllGroups();
                        break;
                    case "pageChiTietTaiSan":
                        gcChiTietTaiSan.DataSource = (from p in db.tbl_PhieuVanHanh_ChiTiet_TaiSans
                                                      join ts in db.tbl_ChiTietTaiSans on p.MaTaiSanChiTietID equals ts.ID
                                                      join tt in db.tbl_PhieuVanHanh_ChiTiet_TinhTrangTaiSans on p.TinhTrangTaiSanID equals tt.ID into _ttrang
                                                      from tt in _ttrang.DefaultIfEmpty()
                                                      where p.PhieuVanHanhID == id
                                                      select new
                                                      {
                                                          p.MaTaiSanChiTietID,
                                                          p.MoTa,
                                                          p.GhiChu,
                                                          ts.TenChiTietTaiSan,
                                                          ts.MaChiTietTaiSan,
                                                          TinhTrangTaiSan = tt.Name
                                                      }).ToList();
                        break;
                    case "tabLichSuDuyet":
                        repNV.DataSource = _db.tnNhanViens;
                        repTT.DataSource = _db.tbl_PhieuVanHanh_TrangThais;
                        gcLichSuDuyet.DataSource = (from ls in _db.tbl_PhieuVanHanh_LichSus
                                                    join cv in _db.tnChucVus on ls.ChucVuID equals cv.MaCV
                                                    where ls.PhieuVanHanhID == id
                                                    select new
                                                    {
                                                        ls.ID,
                                                        ls.IsNguoiCuoi,
                                                        ls.NgayTao,
                                                        ls.NguoiTao,
                                                        cv.TenCV,ls.TrangThaiID
                                                    }).ToList();
                        break;
                }
               
            }
            catch (Exception)
            {
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
            RefreshData();
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

                using (var frm = new frmPhieuBaoTri_Edit
                {
                    MaTn = (byte?) beiToaNha.EditValue,
                    Id = 0, IsSua = 0
                })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadData();
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
                if (gv.GetFocusedRowCellValue("ID") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần sửa, xin cảm ơn.");
                    return;
                }

                using (var frm = new frmPhieuBaoTri_Edit
                {
                    MaTn = (byte?) beiToaNha.EditValue, Id = (int) gv.GetFocusedRowCellValue("ID"), IsSua = 1
                })
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
                _db = new MasterDataContext();
                int[] indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                    return;
                }
                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                foreach (var r in indexs)
                {
                    var o = _db.tbl_PhieuVanHanhs.FirstOrDefault(_ =>
                        _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                    if (o != null)
                    {
                        _db.tbl_PhieuVanHanh_ChiTiet_TaiSans.DeleteAllOnSubmit(
                           _db.tbl_PhieuVanHanh_ChiTiet_TaiSans.Where(_ => _.PhieuVanHanhID == o.ID));
                        _db.tbl_PhieuVanHanh_ChiTiets.DeleteAllOnSubmit(
                            _db.tbl_PhieuVanHanh_ChiTiets.Where(_ => _.PhieuVanHanhID == o.ID));
                        _db.tbl_PhieuVanHanh_LichSus.DeleteAllOnSubmit(
                            _db.tbl_PhieuVanHanh_LichSus.Where(_ => _.PhieuVanHanhID == o.ID));
                        _db.tbl_PhieuVanHanhs.DeleteOnSubmit(o);
                    }
                }
                _db.SubmitChanges();
                LoadData();
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void itemXLTiepNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gv.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu");
                return;
            }
            //Check phiếu
            bool TrangThaiPhieu = true;
            foreach (var i in indexs)
            {
                var id = int.Parse(gv.GetRowCellValue(i, "idTrangThai").ToString());
                if (id != null)
                {
                    if (id == 2)
                    {
                        TrangThaiPhieu = false;
                    }
                }
            }
            if (TrangThaiPhieu == false)
            {
                DialogBox.Alert("Vui lòng chọn phiếu chưa duyệt");
                return;
            }
            DateTime dtNgayDuyet;
            string GhiChuDuyet = "";
            var frm = new frmDuyetPhieu();
            frm.ShowDialog();
            if (frm.IsSave)
            {
                dtNgayDuyet = frm.NgayDuyet;
                GhiChuDuyet = frm.GhiChuDuyet;
            }
            else
            {
                return;
            }
            foreach (var i in indexs)
            {
                var id = (int?)gv.GetRowCellValue(i, "ID");
                if (id != null)
                {
                    var objPVH = _db.tbl_PhieuVanHanhs.FirstOrDefault(p => p.ID == id);
                    if (objPVH != null)
                    {
                        objPVH.NguoiTiepNhan = Common.User.MaNV;
                        objPVH.NgayTiepNhan = dtNgayDuyet;
                        objPVH.GhiChuTiepNhan = GhiChuDuyet;
                        objPVH.TrangThaiPhieu = 1;
                    }

                    _db.SubmitChanges();
                }
            }
            RefreshData();
        }

        private void itemXLHoanThanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gv.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu");
                return;
            }
            //Check phiếu
            bool TrangThaiPhieu = true;
            foreach (var i in indexs)
            {
                var id = int.Parse(gv.GetRowCellValue(i, "idTrangThai").ToString());
                if (id != null)
                {
                    if (id == 2)
                    {
                        TrangThaiPhieu = false;
                    }
                }
            }
            if (TrangThaiPhieu == false)
            {
                DialogBox.Alert("Vui lòng chọn phiếu chưa duyệt");
                return;
            }
            DateTime dtNgayDuyet;
            string GhiChuDuyet = "";
            var frm = new frmDuyetPhieu();
            frm.ShowDialog();
            if (frm.IsSave)
            {
                dtNgayDuyet = frm.NgayDuyet;
                GhiChuDuyet = frm.GhiChuDuyet;
            }
            else
            {
                return;
            }
            foreach (var i in indexs)
            {
                var id = (int?)gv.GetRowCellValue(i, "ID");
                if (id != null)
                {
                    var objPVH = _db.tbl_PhieuVanHanhs.FirstOrDefault(p => p.ID == id);
                    if (objPVH != null)
                    {
                        objPVH.NguoiHoanThanh = Common.User.MaNV;
                        objPVH.NgayHoanThanh = dtNgayDuyet;
                        objPVH.GhiChuHoanThanh = GhiChuDuyet;
                        objPVH.TrangThaiPhieu = 2;
                    }

                    _db.SubmitChanges();
                }
            }
            RefreshData();
        }

        private void itemXLDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gv.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn phiếu");
                return;
            }

            var duyet = (bool?)gv.GetFocusedRowCellValue("IsDuyet");
            if (duyet == true)
            {
                DialogBox.Error("Phiếu đã được người cuối duyệt!");
                return;
            }

            //Check phiếu
            var trangThaiPhieu = true;

            var idTrangThai = int.Parse(gv.GetFocusedRowCellValue("idTrangThai").ToString());
            if (idTrangThai != 2)
            {
                trangThaiPhieu = false;
            }

            if (trangThaiPhieu == false)
            {
                DialogBox.Alert("Vui lòng chọn phiếu đã hoàn thành để duyệt");
                return;
            }

            var nts = (int?)gv.GetFocusedRowCellValue("HeThongTaiSanID");
            DateTime dtNgayDuyet;
            var ghiChuDuyet = "";

            #region Kiểm tra duyệt //HeThongTaiSanID

            var ktCv = _db.tbl_FromDuyet_ChucVus.Where(_ =>
                _.FormDuyetID == 7 & _.HeThongTaiSanID == nts & _.ChucVuID == Common.User.MaCV);
            if (ktCv.Any())
            {
                var ktNv = ktCv.Where(_ => _.NhanVienID != null);
                if (ktNv.Any())
                {
                    var ktNvCt = ktNv.FirstOrDefault(_ => _.NhanVienID == Common.User.MaNV);
                    if (ktNvCt == null)
                    {
                        DialogBox.Error("Bạn không được duyệt kế hoạch vận hành này");
                        return;
                    }
                    else
                    {
                        //var ktLs = _db.tbl_PhieuVanHanh_LichSus.FirstOrDefault(_ =>
                        //    _.PhieuVanHanhID == id & _.NguoiTao == Common.User.MaNV);
                        //if (ktLs != null)
                        //{
                        //    DialogBox.Error("Phiếu này bạn đã duyệt rồi");
                        //    return;
                        //}

                        var frm = new frmDuyetPhieu();
                        frm.ShowDialog();
                        if (frm.IsSave)
                        {
                            dtNgayDuyet = frm.NgayDuyet;
                            ghiChuDuyet = frm.GhiChuDuyet;
                        }
                        else
                        {
                            return;
                        }

                        var o = new tbl_PhieuVanHanh_LichSu();
                        o.PhieuVanHanhID = id;
                        o.NguoiTao = Common.User.MaNV;
                        o.NgayTao = dtNgayDuyet;
                        o.ChucVuID = Common.User.MaCV;
                        o.TrangThaiID = 3;
                        o.DienGiai = ghiChuDuyet;
                        _db.tbl_PhieuVanHanh_LichSus.InsertOnSubmit(o);

                        if (ktNvCt.IsDuyet != true)
                        {
                            _db.SubmitChanges();
                            return;
                        }
                        else
                            o.IsNguoiCuoi = true;
                    }
                }
                else
                {
                    DialogBox.Error("Phiếu này không có nhân viên nào được phân quyền duyệt");
                    return;
                }
            }
            else
            {
                DialogBox.Error("Phiếu này không có nhân viên nào được phân quyền duyệt");
                return;
            }

            #endregion

            var objPvh = _db.tbl_PhieuVanHanhs.FirstOrDefault(p => p.ID == id);
            if (objPvh != null)
            {
                objPvh.NguoiDuyet = Common.User.MaNV;
                objPvh.NgayDuyet = dtNgayDuyet;
                objPvh.GhiChuDuyet = ghiChuDuyet;
                objPvh.IsDuyet = true;
                objPvh.TrangThaiPhieu = 3;
            }

            _db.SubmitChanges();

            RefreshData();
        }

        private void itemXLHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gv.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn phiếu");
                return;
            }

            var duyet = (bool?)gv.GetFocusedRowCellValue("IsDuyet");
            if (duyet == true)
            {
                DialogBox.Error("Phiếu đã được người cuối duyệt!");
                return;
            }

            //Check phiếu
            var trangThaiPhieu = true;

            var idTrangThai = int.Parse(gv.GetFocusedRowCellValue("idTrangThai").ToString());
            if (idTrangThai != 2) trangThaiPhieu = false;

            if (trangThaiPhieu == false)
            {
                DialogBox.Alert("Vui lòng chọn phiếu đã hoàn thành để duyệt");
                return;
            }

            var nts = (int?)gv.GetFocusedRowCellValue("HeThongTaiSanID");
            DateTime dtNgayDuyet;
            var ghiChuDuyet = "";

            #region Kiểm tra duyệt //HeThongTaiSanID

            var ktCv = _db.tbl_FromDuyet_ChucVus.Where(_ =>
                _.FormDuyetID == 7 & _.HeThongTaiSanID == nts & _.ChucVuID == Common.User.MaCV);
            if (ktCv.Any())
            {
                var ktNv = ktCv.Where(_ => _.NhanVienID != null);
                if (ktNv.Any())
                {
                    var ktNvCt = ktNv.FirstOrDefault(_ => _.NhanVienID == Common.User.MaNV);
                    if (ktNvCt == null)
                    {
                        DialogBox.Error("Bạn không được hủy duyệt kế hoạch vận hành này");
                        return;
                    }
                    else
                    {
                        var frm = new frmDuyetPhieu();
                        frm.ShowDialog();
                        if (frm.IsSave)
                        {
                            dtNgayDuyet = frm.NgayDuyet;
                            ghiChuDuyet = frm.GhiChuDuyet;
                        }
                        else
                            return;

                        var o = new tbl_PhieuVanHanh_LichSu();
                        o.PhieuVanHanhID = id;
                        o.NguoiTao = Common.User.MaNV;
                        o.NgayTao = dtNgayDuyet;
                        o.ChucVuID = Common.User.MaCV;
                        o.TrangThaiID = 4;
                        o.DienGiai = ghiChuDuyet;
                        _db.tbl_PhieuVanHanh_LichSus.InsertOnSubmit(o);

                        if (ktNvCt.IsDuyet != true)
                        {
                            _db.SubmitChanges();
                            return;
                        }
                        else
                            o.IsNguoiCuoi = true;

                    }
                }
                else
                {
                    DialogBox.Error("Phiếu này không có nhân viên nào được phân quyền duyệt");
                    return;
                }
            }
            else
            {
                DialogBox.Error("Phiếu này không có nhân viên nào được phân quyền duyệt");
                return;
            }

            #endregion

            var objPvh = _db.tbl_PhieuVanHanhs.FirstOrDefault(p => p.ID == id);
            if (objPvh != null)
            {
                objPvh.NguoiDuyet = Common.User.MaNV;
                objPvh.NgayDuyet = dtNgayDuyet;
                objPvh.GhiChuDuyet = ghiChuDuyet;
                objPvh.IsDuyet = false;
                objPvh.TrangThaiPhieu = 4;

                // delete all row lichsu
                _db.tbl_PhieuVanHanh_LichSus.DeleteAllOnSubmit(
                    _db.tbl_PhieuVanHanh_LichSus.Where(_ => _.PhieuVanHanhID == objPvh.ID));
            }

            _db.SubmitChanges();

            RefreshData();
        }

        private void itemXLThucHienLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gv.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu");
                return;
            }

            DateTime dtNgayDuyet;
            string GhiChuDuyet = "";
            var frm = new frmDuyetPhieu();
            frm.ShowDialog();
            if (frm.IsSave)
            {
                dtNgayDuyet = frm.NgayDuyet;
                GhiChuDuyet = frm.GhiChuDuyet;
            }
            else
            {
                return;
            }
            foreach (var i in indexs)
            {
                var id = (int?)gv.GetRowCellValue(i, "ID");
                if (id != null)
                {
                    var objPVH = _db.tbl_PhieuVanHanhs.FirstOrDefault(p => p.ID == id);
                    objPVH.NguoiDuyet = Common.User.MaNV;
                    objPVH.NgayDuyet = dtNgayDuyet;
                    objPVH.GhiChuDuyet = GhiChuDuyet;
                    objPVH.TrangThaiPhieu = 0;
                    objPVH.IsDuyet = false;

                    // delete all row lichsu
                    _db.tbl_PhieuVanHanh_LichSus.DeleteAllOnSubmit(
                        _db.tbl_PhieuVanHanh_LichSus.Where(_ => _.PhieuVanHanhID == objPVH.ID));

                    _db.SubmitChanges();
                }
            }
            RefreshData();
        
        }
    }
}