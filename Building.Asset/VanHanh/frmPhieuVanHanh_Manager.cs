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
namespace Building.Asset.VanHanh
{
    public partial class frmPhieuVanHanh_Manager : XtraForm
    {
        private MasterDataContext _db;

        public frmPhieuVanHanh_Manager()
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
                        where kh.MaTN == (byte?) beiToaNha.EditValue
                              && kh.IsTenTaiSan.GetValueOrDefault() == false
                              && SqlMethods.DateDiffDay((DateTime) beiTuNgay.EditValue, kh.TuNgay) >=
                              0 & SqlMethods.DateDiffDay(kh.TuNgay,
                                  (DateTime) beiDenNgay.EditValue) >= 0 && kh.IsPhieuBaoTri.GetValueOrDefault() == false
                              & kh.LoaiHeThong==1
                              
                        select new
                        {
                            kh.ID,
                            TenHeThong = ht.TenNhomTaiSan,
                            kh.NgayPhieu,
                            kh.NgayNhap,
                            kh.SoPhieu,
                            kh.DenNgay,
                            kh.TuNgay,
                            kh.NguoiNhap,kh.NguoiSua,
                            kh.NgaySua,
                            kh.PhanLoaiCaID,
                            idTrangThai = kh.TrangThaiPhieu ?? 0,
                            kh.TrangThaiPhieu,
                            kh.NgayDuyet,
                            kh.GhiChuDuyet,
                            KhoiNha = "",
                            kh.HeThongTaiSanID,
                            kh.tbl_KeHoachVanHanh.SoKeHoach, kh.NguoiThucHien, kh.NguoiDuyet, kh.NgayThucHien,
                            kh.NguoiTiepNhan, kh.NgayTiepNhan, kh.GhiChuTiepNhan, kh.NgayHoanThanh, kh.NguoiHoanThanh,
                            kh.GhiChuHoanThanh, StatusLevelID = kh.StatusLevelID ?? 1,
                            Color = kh.StatusLevelID != null
                                ? int.Parse(kh.tbl_PhieuVanHanh_Status_Level.Color)
                                : int.Parse(_db.tbl_PhieuVanHanh_Status_Levels.First().Color),kh.LoaiHeThong,
                            IsBatThuong=kh.IsBatThuong==true?"Bất thường":"Bình thường"
                        }).ToList();
                    // loại tài sản
                    var objByLoaiTaiSan = (from kh in _db.tbl_PhieuVanHanhs
                        join lts in _db.tbl_LoaiTaiSans on kh.LoaiTaiSanID equals lts.ID
                        where kh.MaTN == (byte?) beiToaNha.EditValue
                              && kh.LoaiHeThong == 2
                              && SqlMethods.DateDiffDay((DateTime) beiTuNgay.EditValue, kh.TuNgay) >= 0
                              && SqlMethods.DateDiffDay(kh.TuNgay, (DateTime) beiDenNgay.EditValue) >= 0
                              && kh.IsPhieuBaoTri.GetValueOrDefault() == false
                        select new
                        {
                            kh.ID, TenHeThong = lts.TenLoaiTaiSan, kh.NgayPhieu, kh.NgayNhap, kh.SoPhieu, kh.DenNgay,
                            kh.TuNgay, kh.NguoiNhap, kh.NguoiSua, kh.NgaySua, kh.PhanLoaiCaID,
                            idTrangThai = kh.TrangThaiPhieu ?? 0, kh.TrangThaiPhieu, kh.NgayDuyet, kh.GhiChuDuyet,
                            KhoiNha = "", kh.HeThongTaiSanID, kh.tbl_KeHoachVanHanh.SoKeHoach, kh.NguoiThucHien,
                            kh.NguoiDuyet, kh.NgayThucHien, kh.NguoiTiepNhan, kh.NgayTiepNhan, kh.GhiChuTiepNhan,
                            kh.NgayHoanThanh, kh.NguoiHoanThanh, kh.GhiChuHoanThanh,
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
                        where kh.MaTN == (byte?) beiToaNha.EditValue
                              && kh.IsTenTaiSan.GetValueOrDefault() == true
                              && SqlMethods.DateDiffDay((DateTime) beiTuNgay.EditValue, kh.TuNgay) >=
                              0 & SqlMethods.DateDiffDay(kh.TuNgay, (DateTime) beiDenNgay.EditValue) >= 0
                              && kh.IsPhieuBaoTri.GetValueOrDefault() == false
                              & kh.LoaiHeThong == 3
                        select new
                        {
                            kh.ID, TenHeThong = ht.TenTaiSan, kh.NgayPhieu, kh.NgayNhap, kh.SoPhieu, kh.DenNgay,
                            kh.TuNgay, kh.NguoiNhap, kh.NguoiSua, kh.NgaySua, kh.PhanLoaiCaID,
                            idTrangThai = kh.TrangThaiPhieu ?? 0, kh.TrangThaiPhieu, kh.NgayDuyet, kh.GhiChuDuyet,
                            KhoiNha = kn.TenKN, kh.HeThongTaiSanID, kh.tbl_KeHoachVanHanh.SoKeHoach, kh.NguoiThucHien,
                            kh.NguoiDuyet, kh.NgayThucHien, kh.NguoiTiepNhan, kh.NgayTiepNhan, kh.GhiChuTiepNhan,
                            kh.NgayHoanThanh, kh.NguoiHoanThanh, kh.GhiChuHoanThanh,
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
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            var db = new MasterDataContext();
            foreach (var v in objKbc.Source)
            {
                cbxKBC.Items.Add(v);
            }
            beiKBC.EditValue = objKbc.Source[7];
            SetDate(7);

            lkNhanVien.DataSource = db.tnNhanViens.Select(o => new { o.MaNV, o.HoTenNV }).ToList();
            repTrangThai.DataSource = db.tbl_PhieuVanHanh_TrangThais;
            repStatusLevels.DataSource = db.tbl_PhieuVanHanh_Status_Levels;
            repPhanLoaiCa.DataSource = db.tbl_PhanCong_PhanLoaiCas;

            var l = new List<LoaiDanhSachChiTiet>();
            l.Add(new LoaiDanhSachChiTiet { ID = 1, Name = "Hệ thống" });
            l.Add(new LoaiDanhSachChiTiet { ID = 2, Name = "Loại tài sản" });
            l.Add(new LoaiDanhSachChiTiet { ID = 3, Name = "Tên tài sản" });
            repLoaiHeThong.DataSource = l;

            //lueToaNha.DataSource = db.tnToaNhas;

            LoadData();

        }
        public class LoaiDanhSachChiTiet
        {
            public int ID { get; set; }
            public string Name { get; set; }
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
                                                    p.IsHinhAnh,
                                                    p.ID,p.DonViTinh,p.ChiSoCu,p.ChiSoMoi,p.TieuThu
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
                                cv.TenCV,
                                ls.TrangThaiID
                            }).ToList();
                        break;
                }
                gvChiTiet.ExpandAllGroups();
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

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                ((MasterDataContext)e.Tag).Dispose();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            RefreshData();
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

                using (var frm = new frmPhieuVanHanh_Edit
                {
                    MaTn = (byte?)beiToaNha.EditValue,
                    Id = 0,
                    IsSua = 0
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

                using (var frm = new frmPhieuVanHanh_Edit
                {
                    MaTn = (byte?)beiToaNha.EditValue,
                    Id = (int)gv.GetFocusedRowCellValue("ID"),
                    IsSua = 1
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

        private void gvDanhSachYeuCau_RowClick(object sender, RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void xtraTabDetail_Click(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void gvDanhSachYeuCau_RowCellStyle(object sender, RowCellStyleEventArgs e)
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

        private void gvDanhSachYeuCau_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }

        }

        private void itemDuyetPhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?) gv.GetFocusedRowCellValue("ID");
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

            var nts = (int?) gv.GetFocusedRowCellValue("HeThongTaiSanID");
            DateTime dtNgayDuyet;
            var ghiChuDuyet = "";

            #region Kiểm tra duyệt //HeThongTaiSanID

            var ktCv = _db.tbl_FromDuyet_ChucVus.Where(_ =>
                _.FormDuyetID == 6 & _.HeThongTaiSanID == nts & _.ChucVuID == Common.User.MaCV);
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
                        {
                            o.IsNguoiCuoi = true;
                        }

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

        private void itemKhongDuyetPhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                var id = (int?)gv.GetRowCellValue(i, "idTrangThai");
                if (id != null)
                {
                    if (id != 2)
                    {
                        TrangThaiPhieu = false;
                    }
                }
            }
            if (TrangThaiPhieu == false)
            {
                DialogBox.Alert("Vui lòng chọn phiếu đã hoàn thành để  không duyệt");
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
                    objPVH.TrangThaiPhieu = 4;
                    _db.SubmitChanges();
                }
            }
            RefreshData();
        }

        private void itemTiepNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void itemHoanThanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                if (id == 2)
                {
                    TrangThaiPhieu = false;
                }
            }
            if (TrangThaiPhieu == false)
            {
                DialogBox.Alert("Vui lòng chọn phiếu chưa duyệt");
                return;
            }
            DateTime dtNgayDuyet;
            var GhiChuDuyet = "";
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
            // cấu hình duyệt nhiều
            var indexs = gv.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Alert("Vui lòng chọn phiếu");
                return;
            }

            int error = 0;
            foreach (var i in indexs)
            {
                var pvh = _db.tbl_PhieuVanHanhs.FirstOrDefault(_ => _.ID == (int?) gv.GetRowCellValue(i, "ID"));
                if (pvh == null)
                {
                    error = 1;
                    continue;
                }

                if (pvh.IsDuyet == true)
                {
                    error = 2;
                    continue;
                }

                var trangThaiPhieu = true;
                if (pvh.TrangThaiPhieu != 2) trangThaiPhieu = false;
                if (trangThaiPhieu == false)
                {
                    error = 3;
                    continue;
                }

                DateTime dtNgayDuyet;
                var ghiChuDuyet = "";

                #region Kiểm tra duyệt

                var ktCv = _db.tbl_FromDuyet_ChucVus.Where(_ =>
                    _.FormDuyetID == 6 & _.HeThongTaiSanID == pvh.HeThongTaiSanID & _.ChucVuID == Common.User.MaCV);
                if (ktCv.Any())
                {
                    var ktNv = ktCv.Where(_ => _.NhanVienID != null);
                    if (ktNv.Any())
                    {
                        var ktNvCt = ktNv.FirstOrDefault(_ => _.NhanVienID == Common.User.MaNV);
                        if (ktNvCt == null)
                        {
                            error = 4;
                            continue;
                        }
                        else
                        {
                            //var ktLs = _db.tbl_PhieuVanHanh_LichSus.FirstOrDefault(_ =>
                            //    _.PhieuVanHanhID == pvh.ID & _.NguoiTao == Common.User.MaNV & _.TrangThaiID == 3);
                            //if (ktLs != null)
                            //{
                            //    error = 5;
                            //    continue;
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
                                continue;
                            }

                            var o = new tbl_PhieuVanHanh_LichSu();
                            o.PhieuVanHanhID = pvh.ID;
                            o.NguoiTao = Common.User.MaNV;
                            o.NgayTao = dtNgayDuyet;
                            o.ChucVuID = Common.User.MaCV;
                            o.TrangThaiID = 3;
                            o.DienGiai = ghiChuDuyet;
                            _db.tbl_PhieuVanHanh_LichSus.InsertOnSubmit(o);

                            if (ktNvCt.IsDuyet != true)
                            {
                                _db.SubmitChanges();
                                continue;
                            }
                            else
                                o.IsNguoiCuoi = true;
                        }
                    }
                    else
                    {
                        error = 6;
                        continue;
                    }
                }
                else
                {
                    error = 7;
                    continue;
                }

                #endregion

                pvh.NguoiDuyet = Common.User.MaNV;
                pvh.NgayDuyet = dtNgayDuyet;
                pvh.GhiChuDuyet = ghiChuDuyet;
                pvh.IsDuyet = true;
                pvh.TrangThaiPhieu = 3;
                _db.SubmitChanges();
            }

            #region code duyệt 1 cũ
            //var id = (int?)gv.GetFocusedRowCellValue("ID");
            //if (id == null)
            //{
            //    DialogBox.Error("Vui lòng chọn phiếu");
            //    return;
            //}

            //var duyet = (bool?)gv.GetFocusedRowCellValue("IsDuyet");
            //if (duyet == true)
            //{
            //    DialogBox.Error("Phiếu đã được người cuối duyệt!");
            //    return;
            //}
            //Check phiếu
            //var trangThaiPhieu = true;

            //var idTrangThai = int.Parse(gv.GetFocusedRowCellValue("idTrangThai").ToString());
            //if (idTrangThai != 2)
            //{
            //    trangThaiPhieu = false;
            //}
            //if (trangThaiPhieu == false)
            //{
            //    DialogBox.Alert("Vui lòng chọn phiếu đã hoàn thành để duyệt");
            //    return;
            //}
            //var nts = (int?)gv.GetFocusedRowCellValue("HeThongTaiSanID");
            //DateTime dtNgayDuyet;
            //var ghiChuDuyet = "";

            //#region Kiểm tra duyệt //HeThongTaiSanID

            //var ktCv = _db.tbl_FromDuyet_ChucVus.Where(_ =>
            //    _.FormDuyetID == 6 & _.HeThongTaiSanID == nts & _.ChucVuID == Common.User.MaCV);
            //if (ktCv.Any())
            //{
            //    var ktNv = ktCv.Where(_ => _.NhanVienID != null);
            //    if (ktNv.Any())
            //    {
            //        var ktNvCt = ktNv.FirstOrDefault(_ => _.NhanVienID == Common.User.MaNV);
            //        if (ktNvCt == null)
            //        {
            //            DialogBox.Error("Bạn không được duyệt kế hoạch vận hành này");
            //            return;
            //        }
            //        else
            //        {
            //            var ktLs = _db.tbl_PhieuVanHanh_LichSus.FirstOrDefault(_ =>
            //                _.PhieuVanHanhID == id & _.NguoiTao == Common.User.MaNV & _.TrangThaiID == 3);
            //            if (ktLs != null)
            //            {
            //                DialogBox.Error("Phiếu này bạn đã duyệt rồi");
            //                return;
            //            }

            //            var frm = new frmDuyetPhieu();
            //            frm.ShowDialog();
            //            if (frm.IsSave)
            //            {
            //                dtNgayDuyet = frm.NgayDuyet;
            //                ghiChuDuyet = frm.GhiChuDuyet;
            //            }
            //            else
            //            {
            //                return;
            //            }

            //            var o = new tbl_PhieuVanHanh_LichSu();
            //            o.PhieuVanHanhID = id;
            //            o.NguoiTao = Common.User.MaNV;
            //            o.NgayTao = dtNgayDuyet;
            //            o.ChucVuID = Common.User.MaCV;
            //            o.TrangThaiID = 3;
            //            o.DienGiai = ghiChuDuyet;
            //            _db.tbl_PhieuVanHanh_LichSus.InsertOnSubmit(o);

            //            if (ktNvCt.IsDuyet != true)
            //            {
            //                _db.SubmitChanges();
            //                return;
            //            }
            //            else
            //                o.IsNguoiCuoi = true;
            //        }
            //    }
            //    else
            //    {
            //        DialogBox.Error("Phiếu này không có nhân viên nào được phân quyền duyệt");
            //        return;
            //    }
            //}
            //else
            //{
            //    DialogBox.Error("Phiếu này không có nhân viên nào được phân quyền duyệt");
            //    return;
            //}

            //#endregion
            #endregion

            switch(error)
            {
                case 0:
                    DialogBox.Success();
                    break;
                case 1:
                    DialogBox.Error("Vui lòng chọn phiếu");
                    break;
                case 2:
                    DialogBox.Error("Phiếu đã được người cuối duyệt!");
                    break;
                case 3:
                    DialogBox.Alert("Vui lòng chọn phiếu đã hoàn thành để duyệt");
                    break;
                case 4:
                    DialogBox.Error("Bạn không được duyệt kế hoạch vận hành này");
                    break;
                case 5:
                    DialogBox.Error("Phiếu này bạn đã duyệt rồi");
                    break;
                case 6:
                    DialogBox.Error("Phiếu này không có nhân viên nào được phân quyền duyệt");
                    break;
                case 7:
                    DialogBox.Error("Phiếu này không có nhân viên nào được phân quyền duyệt");
                    break;
            }
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
                _.FormDuyetID == 6 & _.HeThongTaiSanID == nts & _.ChucVuID == Common.User.MaCV);
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

        private void itemThucHienLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    objPVH.IsDuyet = false;
                    objPVH.TrangThaiPhieu = 0;

                    // delete all row lichsu
                    _db.tbl_PhieuVanHanh_LichSus.DeleteAllOnSubmit(
                        _db.tbl_PhieuVanHanh_LichSus.Where(_ => _.PhieuVanHanhID == objPVH.ID));

                    _db.SubmitChanges();
                }
            }
            RefreshData();
        }

        private void btnViewImg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _ID = (long?) gvChiTiet.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn [Chi tiết].");
                return;
            }

            using (var frm = new frmViewImg())
            {
                frm.ID = _ID;
                //frm.ID = 3651;
                frm.ShowDialog();
            }
        }

        private void itemStatusLevel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("ID") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu vận hành, xin cảm ơn.");
                    return;
                }

                using (var frm = new frmPhieuVanHanh_Edit_StatusLevel
                {
                    MaTn = (byte?)beiToaNha.EditValue,
                    Id = (int)gv.GetFocusedRowCellValue("ID"),
                    IsSua = 1
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

        private void itemUpdateLoaiPhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // update loại phiếu full hệ thống lần đầu tiên, do trước đó không có cái loại này, đề phòng họ đã có dữ liệu trước đó
            // nếu sau này quá chậm thì bỏ nút này đi hoặc là cho update trong tòa nhà đó thôi
            //using (var db = new MasterDataContext())
            //{
            //    var some = db.tbl_PhieuVanHanhs.Where(_=> _.LoaiHeThong==null).ToList();
            //    some.ForEach(a => a.LoaiHeThong = a.TenTaiSanID = a.IsTenTaiSan == true ? 3 : 1);
            //    db.SubmitChanges();
            //}

        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void itemXoaDuyetAdmin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // update xóa hết lịch sử duyệt của nhân viên trong tòa bella nếu không phải duyệt cuối
            var db = new MasterDataContext();
            var o = (from p in db.tbl_PhieuVanHanhs
                join ls in db.tbl_PhieuVanHanh_LichSus on p.ID equals ls.PhieuVanHanhID
                where p.MaTN == 11 & ls.NguoiTao == 600 & ls.TrangThaiID == 3 & ls.IsNguoiCuoi == null
                select new {p.ID, TDD = ls.ID}).ToList();
            foreach (var i in o)
            {
                var pvh = db.tbl_PhieuVanHanhs.First(_ => _.ID == i.ID);
                pvh.TrangThaiPhieu = 3;
                var ls = db.tbl_PhieuVanHanh_LichSus.First(_ => _.ID == i.TDD);
                ls.IsNguoiCuoi = true;
                ls.ChucVuID = 33;
            }

            db.SubmitChanges();
        }
    }
}