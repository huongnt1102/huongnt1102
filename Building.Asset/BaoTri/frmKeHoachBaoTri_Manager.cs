using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using DevExpress.XtraGrid.Views.Grid;

namespace Building.Asset.BaoTri
{
    public partial class frmKeHoachBaoTri_Manager : XtraForm
    {
        public int? Id { get; set; }
        private MasterDataContext _db;

        public frmKeHoachBaoTri_Manager()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        #region Code

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
                    repNhomTaiSan.DataSource =
                        _db.tbl_NhomTaiSans.Where(_ => _.MaTN == ((byte?) beiToaNha.EditValue ?? Common.User.MaTN));
                    repTanXuat.DataSource = _db.tbl_TanSuats;

                    gc.DataSource = _db.tbl_KeHoachVanHanhs
                        .Where(_ => (Id != null ? _.ID == Id : _.MaTN == ((byte?)beiToaNha.EditValue ?? Common.User.MaTN)) 
                                 && _.IsKeHoachBaoTri == true
                            //& SqlMethods.DateDiffDay(_.TuNgay,Convert.ToDateTime(beiTuNgay.EditValue)) >= 0
                            //& SqlMethods.DateDiffDay(_.DenNgay, Convert.ToDateTime(beiDenNgay.EditValue)) >= 0
                        ).ToList();
                    if (Id != null)
                    {
                        var item = _db.tbl_KeHoachVanHanhs.First(_ => _.ID == Id).MaTN;
                        lueToaNha.DataSource = _db.tnToaNhas;
                        beiToaNha.EditValue = item.GetValueOrDefault();
                    }
                }
            }
            catch
            {
                // ignored
            }

            LoadDetail();
        }

        #endregion

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            _db = new MasterDataContext();
            foreach (var v in objKbc.Source)
            {
                cbxKBC.Items.Add(v);
            }

            beiKBC.EditValue = objKbc.Source[7];
            SetDate(7);
            lkNhanVien.DataSource = _db.tnNhanViens.Select(o => new {o.MaNV, o.HoTenNV}).ToList();
            LoadData();

        }

        private void LoadPhieuBaoTri(int? keHoachBaoTriId)
        {
            var db = new MasterDataContext();
            #region
            // hệ thống
            var objKH1 = (from kh in db.tbl_PhieuVanHanhs
                          join ht in db.tbl_NhomTaiSans on kh.NhomTaiSanID equals ht.ID
                          where kh.MaTN == (byte?)beiToaNha.EditValue
                                && kh.IsTenTaiSan.GetValueOrDefault() == false
                                 && kh.IsPhieuBaoTri.GetValueOrDefault() == true
                                & kh.LoaiHeThong == 1
                                & kh.KeHoachVanHanhID == keHoachBaoTriId
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
                                  : int.Parse(db.tbl_PhieuVanHanh_Status_Levels.First().Color),
                              kh.LoaiHeThong
                          }).ToList();
            // loại tài sản
            var objByLoaiTaiSan = (from kh in db.tbl_PhieuVanHanhs
                                   join lts in db.tbl_LoaiTaiSans on kh.LoaiTaiSanID equals lts.ID
                                   where kh.MaTN == (byte?)beiToaNha.EditValue
                                         && kh.LoaiHeThong == 2
                                         
                                         && kh.IsPhieuBaoTri.GetValueOrDefault() == true
                                         & kh.KeHoachVanHanhID == keHoachBaoTriId
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
                                           : int.Parse(db.tbl_PhieuVanHanh_Status_Levels.First().Color),
                                       kh.LoaiHeThong
                                   }).ToList();
            // tên tài sản
            var objKH2 = (from kh in db.tbl_PhieuVanHanhs
                          join ht in db.tbl_TenTaiSans on kh.TenTaiSanID equals ht.ID
                          join kn in db.mbKhoiNhas on ht.BlockID equals kn.MaKN into _knha
                          from kn in _knha.DefaultIfEmpty()
                          where kh.MaTN == (byte?)beiToaNha.EditValue
                                && kh.IsTenTaiSan.GetValueOrDefault() == true
                                
                                && kh.IsPhieuBaoTri.GetValueOrDefault() == true
                                & kh.LoaiHeThong == 3
                                & kh.KeHoachVanHanhID == keHoachBaoTriId
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
                                  : int.Parse(db.tbl_PhieuVanHanh_Status_Levels.First().Color),
                              kh.LoaiHeThong
                          }).ToList();
            var objKH = objKH1.Concat(objByLoaiTaiSan).Concat(objKH2);
            gridControl1.DataSource = objKH;
            #endregion

            db.Dispose();
        }

        private void LoadDetail()
        {
            _db = new MasterDataContext();
            try
            {
                var id = (int?) gv.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }

                switch (xtraTabDetail.SelectedTabPage.Name)
                {
                    case "tabChiTiet":
                        gcChiTiet.DataSource = (from p in _db.tbl_KeHoachVanHanh_ChiTiets
                            join ts in _db.tbl_TenTaiSans on p.MaTenTaiSanID equals ts.ID
                            join kn in _db.mbKhoiNhas on ts.BlockID equals kn.MaKN into _knha
                            from kn in _knha.DefaultIfEmpty()
                            join pr in _db.tbl_Profiles on p.ProfileID equals pr.ID into _pro
                            from pr in _pro.DefaultIfEmpty()
                            where p.KeHoachVanHanhID == id
                            orderby p.TenTaiSan
                            select new
                            {
                                p.TenTaiSan,
                                p.ProfileID,
                                pr.TenMau,
                                KhoiNha = kn.TenKN
                            }).ToList();
                        break;
                    case "tabLichSuDuyet":
                        repNV.DataSource = _db.tnNhanViens;
                        gcLichSuDuyet.DataSource = (from ls in _db.tbl_KeHoachVanHanh_LichSuDuyets
                            join cv in _db.tnChucVus on ls.ChucVuID equals cv.MaCV
                            where ls.MaKeHoachVanHanh == id
                            select new
                            {
                                ls.ID,
                                ls.IsNguoiCuoi,
                                ls.MaKeHoachVanHanh,
                                ls.NgayDuyet,
                                ls.NguoiDuyet,
                                cv.TenCV
                            }).ToList();
                        break;
                    case "tabPhieuBaoTri":
                        #region
                        // hệ thống
                        var objKh1 = (from kh in _db.tbl_PhieuVanHanhs
                                      join ht in _db.tbl_NhomTaiSans on kh.NhomTaiSanID equals ht.ID
                                      where kh.MaTN.ToString() == beiToaNha.EditValue.ToString()
                                            && kh.IsTenTaiSan.GetValueOrDefault() == false
                                             && kh.IsPhieuBaoTri.GetValueOrDefault() == true
                                            & kh.LoaiHeThong == 1
                                            & kh.KeHoachVanHanhID.ToString() == id.ToString()
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
                                          kh.LoaiHeThong
                                      }).ToList();
                        // loại tài sản
                        var objByLoaiTaiSan = (from kh in _db.tbl_PhieuVanHanhs
                                               join lts in _db.tbl_LoaiTaiSans on kh.LoaiTaiSanID equals lts.ID
                                               where kh.MaTN.ToString() == beiToaNha.EditValue.ToString()
                                                     && kh.LoaiHeThong == 2

                                                     && kh.IsPhieuBaoTri.GetValueOrDefault() == true
                                                     & kh.KeHoachVanHanhID.ToString() == id.ToString()
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
                                                   kh.LoaiHeThong
                                               }).ToList();
                        // tên tài sản
                        var objKh2 = (from kh in _db.tbl_PhieuVanHanhs
                                      join ht in _db.tbl_TenTaiSans on kh.TenTaiSanID equals ht.ID
                                      join kn in _db.mbKhoiNhas on ht.BlockID equals kn.MaKN into _knha
                                      from kn in _knha.DefaultIfEmpty()
                                      where kh.MaTN.ToString() == beiToaNha.EditValue.ToString()
                                            && kh.IsTenTaiSan.GetValueOrDefault() == true

                                            && kh.IsPhieuBaoTri.GetValueOrDefault() == true
                                            & kh.LoaiHeThong == 3
                                            & kh.KeHoachVanHanhID.ToString() == id.ToString()
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
                                          kh.LoaiHeThong
                                      }).ToList();
                        var objKh = objKh1.Concat(objByLoaiTaiSan).Concat(objKh2);
                        gridControl1.DataSource = objKh;
                        #endregion
                        
                        break;
                }
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                _db.Dispose();
            }
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit) sender).SelectedIndex);
        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            //LoadData();
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
                if (gridView1.GetFocusedRowCellValue("ID") != null)
                {
                    id = (int) gridView1.GetFocusedRowCellValue("ID");
                }

                using (var frm = new frmKeHoachBaoTri_Edit {MaTn = (byte?) beiToaNha.EditValue, IsSua = 0, Id = id})
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
                if (gv.GetFocusedRowCellValue("ID") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần sửa, xin cảm ơn.");
                    return;
                }

                using (var frm = new frmKeHoachBaoTri_Edit
                {
                    MaTn = (byte?) beiToaNha.EditValue,
                    Id = (int?) gv.GetFocusedRowCellValue("ID"),
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
                    var o = _db.tbl_KeHoachVanHanhs.FirstOrDefault(_ =>
                        _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                    if (o != null)
                    {
                        _db.tbl_KeHoachVanHanh_ChiTiets.DeleteAllOnSubmit(
                            _db.tbl_KeHoachVanHanh_ChiTiets.Where(_ => _.KeHoachVanHanhID == o.ID));
                        _db.tbl_KeHoachVanHanh_LichSuDuyets.DeleteAllOnSubmit(
                            _db.tbl_KeHoachVanHanh_LichSuDuyets.Where(_ => _.MaKeHoachVanHanh == o.ID));
                        _db.tbl_KeHoachVanHanhs.DeleteOnSubmit(o);
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
        }

        private void gvDanhSachYeuCau_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }

        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Import.frmKeHoachVanHanh_Import())
                {
                    frm.MaTn = (byte) beiToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.IsSave)
                        LoadData();
                }
            }
            catch
            {
                //
            }
        }

        public class CheckTime
        {
            public int Id { get; set; }
            public DateTime NgayBd { get; set; }
            public DateTime NgayKt { get; set; }
            public int Days { get; set; }
        }

        //public int GetNgayNghi(DateTime a, DateTime b)
        //{

        //}

        public CheckTime GetCdNew(DateTime a, DateTime b, DateTime c, DateTime d)
        {
            // trả về ngày nghỉ mới
            CheckTime ck = new CheckTime();
            // truong hop 1
            // ab trong cd: (c)---(A)----(B)--(d)
            // a>c & a<d
            // b>c & b<d
            if (a >= c & a <= d & b >= c & b <= d)
            {
                // lấy khoãng d đến d++
                ck = new CheckTime {NgayBd = a, NgayKt = b, Days = (b - a).Days + 1};
            }

            // trường hợp 2
            // ab ngoài cd (A)--(c)--(d)--(B)
            // a<c, a <d, b>c, b>d
            if (a <= c & a <= d & b >= c & b >= d)
            {
                // lấy khoãng a  đến b++
                ck = new CheckTime {NgayBd = c, NgayKt = d, Days = (d - c).Days + 1};
            }

            // trường hợp 3
            // b nằm trong khoãng cd (A)--(c)--(B)--(d)
            if (a <= c & a <= b & b >= c & b <= d)
            {
                ck = new CheckTime {NgayBd = c, NgayKt = b, Days = (b - c).Days + 1};
            }

            // trường hợp 4
            // a nằm trong khoãng cd (c)--(A)--(d)--(B)
            if (a >= c & a <= d & b >= c & b >= d)
            {
                // thời gian làm = d=>b++
                ck = new CheckTime {NgayBd = a, NgayKt = d, Days = (d - a).Days + 1};
            }
            // trường hôp 5
            // AB nằm trước cd (A)--(B)--(c)--(d)
            // cái này chẳng ảnh hưởng gì cả
            //if (a < c & a < d & b < c & b < d)
            //{
            //    ck = new CheckTime { NgayBd = a, NgayKt = b };
            //}
            // trường hợp 6
            // AB nằm ngoài cd (c)--(d)--(A)--(B)
            // tương tự ở trên, k ảnh hưởng gì

            return ck;
        }

        static int CountDays(DayOfWeek day, DateTime start, DateTime end)
        {
            TimeSpan ts = end - start; // Total duration
            int count = (int) Math.Floor(ts.TotalDays / 7); // Number of whole weeks
            int remainder = (int) (ts.TotalDays % 7); // Number of remaining days
            int sinceLastDay = (int) (end.DayOfWeek - day); // Number of days since last [day]
            if (sinceLastDay < 0) sinceLastDay += 7; // Adjust for negative days since last [day]

            // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
            if (remainder >= sinceLastDay) count++;

            return count;
        }

        public CheckTime GetAbnew(DateTime a, DateTime b, byte maTn)
        {
            int ngayNghi = 0;
            DateTime e;

            var objTlnn = (from p in _db.tbl_ThietLapNgayNghis
                join ct in _db.tbl_ThietLapNgayNghi_DanhMuc_ChiTiets on p.DanhMucID equals ct
                    .tbl_ThietLapNgayNghi_DanhMuc.ID
                where p.Nam == a.Year & p.MaTN == maTn
                orderby ct.NgayBD
                select new
                {
                    ct.ID,
                    ct.NgayBD,
                    ct.NgayKT,
                    p.IsThuBay,
                    p.IsChuNhat
                }).ToList();
            var firstTlnn = objTlnn.FirstOrDefault();

            foreach (var i in objTlnn)
            {
                if (i.NgayBD == null || i.NgayKT == null) continue;
                var c = i.NgayBD.GetValueOrDefault();
                var d = i.NgayKT.GetValueOrDefault();
                var kq = GetCdNew(a, b, c, d); // ngày nghỉ chưa trừ thứ 7, chủ nhật // ds ngày nghỉ
                if (kq == null) continue;
                var isIn = kq.Days;
                ngayNghi = ngayNghi + isIn; //2

                // kiểm tra trong khoãng a, b có bao nhiêu ngày thứ 7, chủ nhật
                if (i.IsThuBay == true)
                {
                    if (isIn > 0)
                    {
                        var ngayThu7 = CountDays(DayOfWeek.Saturday, kq.NgayBd, kq.NgayKt); //1 
                        ngayNghi = ngayNghi - ngayThu7; //1
                    }
                }

                if (i.IsChuNhat == true)
                {
                    if (isIn > 0)
                    {
                        var chuNhat = CountDays(DayOfWeek.Sunday, kq.NgayBd, kq.NgayKt); //0
                        ngayNghi = ngayNghi - chuNhat; //1
                    }

                }

                b = b.AddDays(ngayNghi);

            }
            e = b; // lấy e giữ lại b trước khi b thay đổi
            if (firstTlnn != null)
            {
                if (firstTlnn.IsThuBay == true)
                {
                    var satDays = CountDays(DayOfWeek.Saturday, a, e);
                    ngayNghi = ngayNghi + satDays;
                    b = b.AddDays(+satDays);
                    if (b.DayOfWeek == DayOfWeek.Saturday) b = b.AddDays(1);
                }

                if (firstTlnn.IsChuNhat == true)
                {
                    var sunDays = CountDays(DayOfWeek.Sunday, a, e);
                    ngayNghi = ngayNghi + sunDays;
                    b = b.AddDays(+sunDays);
                    if (b.DayOfWeek == DayOfWeek.Sunday) b = b.AddDays(1);
                }
            }

            CheckTime ck = new CheckTime {NgayBd = a, NgayKt = b, Days = ngayNghi};
            return ck;
        }

        private void CreateAutoPhieuVanHanh(byte? maTn, List<tbl_KeHoachVanHanh_ChiTiet> objChiTiet, DateTime tungay,
            DateTime denngay, int soNgay, DateTime denNgay, int? nts, int? id, int? profileId, int? idPhanLoaiCa,
            string kyHieuCa,decimal? soNgayCoTheTre)
        {
            #region Create Phiếu vận hành auto

            if (objChiTiet.Count > 0)
            {

                foreach (var item in objChiTiet)
                {
                    var tempTuNgay = tungay;
                    CheckTime ck = GetAbnew(tempTuNgay, denNgay, (byte) maTn);

                    tempTuNgay = ck.NgayBd;
                    denngay = ck.NgayKt;

                    while (tempTuNgay.Date <= denngay.Date)
                    {
                        var vh = new tbl_PhieuVanHanh();
                        vh.MaTN = maTn;
                        vh.NhomTaiSanID = item.MaTenTaiSanID;
                        vh.IsTenTaiSan = true;
                        vh.KeHoachVanHanhID = id;
                        vh.TenTaiSanID = item.MaTenTaiSanID;
                        vh.LoaiTaiSanID = _db.tbl_TenTaiSans.FirstOrDefault(p => p.ID == item.MaTenTaiSanID)
                            .LoaiTaiSanID;
                        vh.TuNgay = tempTuNgay;
                        vh.DenNgay = soNgay == 1 ? tempTuNgay.Date : tempTuNgay.AddDays(soNgay);

                        CheckTime timeNew = GetAbnew(tempTuNgay, (DateTime) vh.DenNgay, (byte) maTn);
                        vh.DenNgay = timeNew.NgayKt;

                        if (vh.DenNgay >= denngay)
                        {
                            vh.DenNgay = denngay;
                        }

                        vh.SoPhieu = string.Format("PVH-{0:dd/MM/yyyy}-{1}", vh.TuNgay, kyHieuCa);
                        vh.NguoiDuyet = Common.User.MaNV;
                        vh.NgayNhap = Common.GetDateTimeSystem();
                        vh.NguoiNhap = Common.User.MaNV;
                        vh.NgayPhieu = Common.GetDateTimeSystem();
                        vh.PhanLoaiCaID = idPhanLoaiCa;
                        vh.TrangThaiPhieu = 0;
                        vh.HeThongTaiSanID = nts;
                        vh.StatusLevelID = 1;
                        vh.LoaiHeThong = 3;
                        vh.IsPhieuBaoTri = true;
                        if (soNgayCoTheTre != null) vh.NgayHetHanCuoiCung = vh.DenNgay.Value.AddDays((double) soNgayCoTheTre);
                        vh.SoNgayCoTheTre = soNgayCoTheTre;
                        _db.tbl_PhieuVanHanhs.InsertOnSubmit(vh);

                        var objProfileCt = _db.tbl_Profile_ChiTiets.Where(p => p.ProfileID == item.ProfileID);
                        foreach (var itemct in objProfileCt)
                        {
                            var ct = new tbl_PhieuVanHanh_ChiTiet();
                            ct.ProfileID = itemct.ID;
                            ct.IsChon = itemct.GiaTriChon;
                            ct.GiaTriNhap_Nhap = "";
                            ct.TenCongViec = itemct.TenCongViec;
                            ct.TenNhomCongViec = itemct.TenNhomCongViec;
                            ct.TieuChuan = itemct.TieuChuan;
                            ct.GiaTriChon = itemct.GiaTriChon;
                            ct.IsChon = true;
                            ct.IsHinhAnh = itemct.IsHinhAnh;
                            vh.tbl_PhieuVanHanh_ChiTiets.Add(ct);
                        }

                        //Lưu chi tiết tài sản 
                        var objCtts = _db.tbl_ChiTietTaiSans.Where(p => p.TenTaiSanID == item.MaTenTaiSanID);
                        var ttts = _db.tbl_TinhTrangTaiSans.FirstOrDefault();
                        foreach (var itemts in objCtts)
                        {
                            
                            var ts = new tbl_PhieuVanHanh_ChiTiet_TaiSan();
                            ts.MaTaiSanChiTietID = itemts.ID;
                            ts.TinhTrangTaiSanID = (byte?) (ttts != null ? ttts.ID : new byte());
                            vh.tbl_PhieuVanHanh_ChiTiet_TaiSans.Add(ts);
                        }

                        //Cập nhật lại từ ngày
                        //tempTuNgay = tempTuNgay.AddDays(soNgay);
                        tempTuNgay = timeNew.NgayKt.AddDays(1);
                    }
                }
            }

            #endregion
        }

        private void ItemDuyetTuDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var duyet = (bool?) gv.GetFocusedRowCellValue("IsDuyet");
            if (duyet == true)
            {
                DialogBox.Error("Kế hoạch đã được duyệt!");
                return;
            }

            _db = new MasterDataContext();
            var nts = (int?) gv.GetFocusedRowCellValue("NhomTaiSanID");
            var maTn = ((byte?) beiToaNha.EditValue ?? Common.User.MaTN);
            var id = (int?) gv.GetFocusedRowCellValue("ID");
            var tuNgay = (DateTime) gv.GetFocusedRowCellValue("TuNgay");
            var denNgay = (DateTime) gv.GetFocusedRowCellValue("DenNgay");
            var tanSuatId = (int) gv.GetFocusedRowCellValue("TanSuatID");
            var profileId = (int?) gv.GetFocusedRowCellValue("ProfileID");
            var soNgayCoTheTre = (decimal?) gv.GetFocusedRowCellValue("SoNgayCoTheTre");

            #region Kiểm tra duyệt

            var ktCv = _db.tbl_FromDuyet_ChucVus.Where(_ =>
                _.FormDuyetID == 3 & _.HeThongTaiSanID == nts & _.ChucVuID == Common.User.MaCV);
            // nếu có tức là đã phân quyền duyệt
            if (ktCv.Any()) // ktCv.Count()>0
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
                        var ktLs = _db.tbl_KeHoachVanHanh_LichSuDuyets.FirstOrDefault(_ =>
                            _.MaKeHoachVanHanh == id & _.NguoiDuyet == Common.User.MaNV);
                        if (ktLs != null)
                        {
                            DialogBox.Error("Phiếu này bạn đã duyệt rồi");
                            return;
                        }

                        var o = new tbl_KeHoachVanHanh_LichSuDuyet
                        {
                            MaKeHoachVanHanh = id,
                            NguoiDuyet = Common.User.MaNV,
                            NgayDuyet = DateTime.Now,
                            ChucVuID = Common.User.MaCV
                        };
                        _db.tbl_KeHoachVanHanh_LichSuDuyets.InsertOnSubmit(o);

                        if (ktNvCt.IsDuyet != true)
                        {
                            _db.SubmitChanges();
                            DialogBox.Success("Duyệt thành công");
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
                    DialogBox.Error("Kế hoạch này không có nhân viên nào được phân quyền duyệt");
                    return;
                }

            }
            else
            {
                DialogBox.Error("Kế hoạch này không có nhân viên nào được phân quyền duyệt");
                return;
            }

            #endregion

            var objChiTiet = _db.tbl_KeHoachVanHanh_ChiTiets.Where(p => p.KeHoachVanHanhID == id).ToList();
            var sn = (from ts in _db.tbl_TanSuats
                where ts.ID == tanSuatId
                select new
                {
                    ts.SoNgay
                }).FirstOrDefault();
            int soNgay = int.Parse(sn.SoNgay.ToString());
            var _tungay = new DateTime(tuNgay.Year, tuNgay.Month, tuNgay.Day);
            var _denngay = new DateTime(denNgay.Year, denNgay.Month, denNgay.Day);

            var idCas = gv.GetFocusedRowCellValue("PhanLoaiCaIDs").ToString().Split(',');
            if (idCas.Count() > 0)
            {
                int[] itemIdCas = idCas.Select(int.Parse).ToArray();
                foreach (var item in itemIdCas)
                {
                    var kyHieuCa = _db.tbl_PhanCong_PhanLoaiCas.First(_ => _.ID == item).KyHieu;
                    CreateAutoPhieuVanHanh(maTn, objChiTiet, _tungay, _denngay, soNgay, denNgay, nts, id, profileId,
                        item, kyHieuCa,soNgayCoTheTre);
                }
            }
            else
            {
                CreateAutoPhieuVanHanh(maTn, objChiTiet, _tungay, _denngay, soNgay, denNgay, nts, id, profileId, null,
                    "",soNgayCoTheTre);
            }


            var objKhvh = _db.tbl_KeHoachVanHanhs.FirstOrDefault(o => o.ID == id);
            if (objKhvh != null)
            {
                objKhvh.IsDuyet = true;
                objKhvh.NgayDuyet = DateTime.Now;
                objKhvh.NguoiSua = Common.User.MaNV;
                objKhvh.NgaySua = DateTime.Now;
            }
            try
            {
                _db.SubmitChanges();
                DialogBox.Success("Duyệt thành công");
                LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error("Xảy ra lỗi: "+ex.ToString());
            }
            
        }

        private void itemCopyKeHoach_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var db = new MasterDataContext();
                int[] indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những kế hoạch cần copy");
                    return;
                }

                foreach (var r in indexs)
                {
                    var o1 = db.tbl_KeHoachVanHanhs.FirstOrDefault(_ =>
                        _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                    if (o1 == null) continue;
                    var o2 = new tbl_KeHoachVanHanh
                    {
                        NgayLapKeHoach = o1.NgayLapKeHoach,
                        TuNgay = o1.TuNgay,
                        DenNgay = o1.DenNgay,
                        MaTN = o1.MaTN,
                        NhomTaiSanID = o1.NhomTaiSanID,
                        TanSuatID = o1.TanSuatID,
                        PhanLoaiCaIDs = o1.PhanLoaiCaIDs,
                        PhanLoaiCaKyHieus = o1.PhanLoaiCaKyHieus,
                        NgayNhap = DateTime.Now,
                        NguoiNhap = Common.User.MaNV,
                        IsKeHoachBaoTri = o1.IsKeHoachBaoTri,
                        TenKeHoach = o1.TenKeHoach,
                        GhiChu = o1.GhiChu,
                        ChiPhiTheoKh = o1.ChiPhiTheoKh,
                        ChiPhiThucHien = o1.ChiPhiThucHien,
                        LoaiHeThong = o1.LoaiHeThong,
                        SoNgayCoTheTre = o1.SoNgayCoTheTre,
                        NgayHetHanCuoiCung = o1.NgayHetHanCuoiCung
                    };

                    foreach (var i in o1.tbl_KeHoachVanHanh_ChiTiets)
                    {
                        var ct1 = new tbl_KeHoachVanHanh_ChiTiet
                        {
                            MaTenTaiSanID = i.MaTenTaiSanID,
                            TenTaiSan = i.TenTaiSan,
                            ProfileID = i.ProfileID,
                        };

                        o2.tbl_KeHoachVanHanh_ChiTiets.Add(ct1);
                    }

                    db.tbl_KeHoachVanHanhs.InsertOnSubmit(o2);
                }

                db.SubmitChanges();
                LoadData();
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void btnChiPhiTheoKh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db = new MasterDataContext();
                var indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần sửa chi phí");
                    return;
                }

                var listId = new List<int>();

                foreach (var r in indexs)
                {
                    listId.Add(int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                }

                using (var frm = new Building.Asset.VanHanh.frmKeHoachVanhanh_ChiPhi
                {
                    ListId = listId,
                    LoaiChiPhi = 0
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

        private void itemChiPhiThucHien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db = new MasterDataContext();
                var indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần sửa chi phí");
                    return;
                }

                var listId = new List<int>();

                foreach (var r in indexs)
                {
                    listId.Add(int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                }

                using (var frm = new Building.Asset.VanHanh.frmKeHoachVanhanh_ChiPhi
                {
                    ListId = listId,
                    LoaiChiPhi = 1
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

        private void itemDieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.GetFocusedRowCellValue("ID") == null)
            {
                DialogBox.Error("Vui lòng chọn phiếu cần điều chỉnh, xin cảm ơn.");
                return;
            }

            _db = new MasterDataContext();

            #region Kiểm tra điều kiện điều chỉnh

            // Nếu phiếu này đã được duyệt, đã tạo phiếu rồi thì mới được điều chỉnh. Còn nếu chưa duyệt thì vui lòng qua duyệt giùm,
            // hoặc sửa phiếu các kiểu để duyệt, lúc đó cứ việc sửa các phiếu xong duyệt
            //
            var duyet = (bool?)gv.GetFocusedRowCellValue("IsDuyet");
            if (duyet != true)
            {
                DialogBox.Error("Vui lòng chọn duyệt kế hoạch!");
                return;
            }

            #endregion

            var id = int.Parse(gv.GetFocusedRowCellValue("ID").ToString());

            var obj = _db.tbl_KeHoachVanHanhs.FirstOrDefault(_ => _.ID == id);
            if (obj == null) return;

            using (var frm = new frmKeHoachBaoTri_DieuChinh
            {
                TuNgayCu = obj.TuNgay,
                DenNgayCu = obj.DenNgay,
                TanXuat = obj.tbl_TanSuat.TenTanSuat
            })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    var idCas = gv.GetFocusedRowCellValue("PhanLoaiCaIDs").ToString().Split(',');

                    #region Xóa hết phiếu từ Từ ngày mới
                    var lstPhieuCu = _db.tbl_PhieuVanHanhs.Where(_ => _.KeHoachVanHanhID == obj.ID & (_.TuNgay >= frm.TuNgayMoi || (_.TuNgay <= frm.TuNgayMoi & _.DenNgay >= frm.TuNgayMoi))).ToList();
                    foreach (var i in lstPhieuCu)
                    {
                        _db.tbl_PhieuVanHanh_ChiTiet_TaiSans.DeleteAllOnSubmit(
                            i.tbl_PhieuVanHanh_ChiTiet_TaiSans);
                        _db.tbl_PhieuVanHanh_ChiTiets.DeleteAllOnSubmit(i.tbl_PhieuVanHanh_ChiTiets);
                        _db.tbl_PhieuVanHanh_LichSus.DeleteAllOnSubmit(
                            _db.tbl_PhieuVanHanh_LichSus.Where(_ => _.PhieuVanHanhID == i.ID));
                        _db.tbl_PhieuVanHanhs.DeleteOnSubmit(i);
                    }

                    #endregion
                    _db.SubmitChanges();

                    // khoãng ngày nghỉ của đợt trước
                    var time = GetAbnew((DateTime)obj.TuNgay, frm.TuNgayMoi, (byte)obj.MaTN);

                    // tạo lại phiếu từ ngày mới đến: đến ngày mới
                    if (idCas.Count() > 0)
                    {
                        var itemIdCas = idCas.Select(int.Parse).ToArray();
                        foreach (var item in itemIdCas)
                        {
                            var kyHieuCa = _db.tbl_PhanCong_PhanLoaiCas.First(_ => _.ID == item).KyHieu;
                            CreateAutoPhieuVanHanh((byte?)obj.MaTN, obj.tbl_KeHoachVanHanh_ChiTiets.ToList(),
                                frm.TuNgayMoi, frm.DenNgayMoi.AddDays(time.Days), int.Parse(obj.tbl_TanSuat.SoNgay.ToString()),
                                frm.DenNgayMoi,
                                obj.NhomTaiSanID, id, obj.ProfileID, item, kyHieuCa,obj.SoNgayCoTheTre);
                        }
                    }
                    else
                    {
                        CreateAutoPhieuVanHanh((byte?)obj.MaTN, obj.tbl_KeHoachVanHanh_ChiTiets.ToList(),
                            frm.TuNgayMoi, frm.DenNgayMoi.AddDays(time.Days), int.Parse(obj.tbl_TanSuat.SoNgay.ToString()),
                            frm.DenNgayMoi,
                            obj.NhomTaiSanID, id, obj.ProfileID, null, "",obj.SoNgayCoTheTre);
                    }

                    _db.SubmitChanges();
                    //LoadPhieuBaoTri(obj.ID);
                }
            }

            _db.Dispose();
        }

        private void itemChinhNgay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView3.GetFocusedRowCellValue("ID") == null)
            {
                DialogBox.Error("Vui lòng chọn phiếu cần điều chỉnh, xin cảm ơn.");
                return;
            }

            using (var frm = new frmKeHoachBaoTri_ChinhNgay
            {
                Id = int.Parse(gridView3.GetFocusedRowCellValue("ID").ToString())
            })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadPhieuBaoTri(int.Parse(gv.GetFocusedRowCellValue("ID").ToString()));
                }
            }
        }

    }
}