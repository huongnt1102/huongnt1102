using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Linq.SqlClient;

namespace Building.Asset.VanHanh
{
    public partial class frmKeHoachVanHanh_Manager : XtraForm
    {
        private MasterDataContext _db=new MasterDataContext();

        public frmKeHoachVanHanh_Manager()
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
                        _db.tbl_NhomTaiSans.Where(_ => _.MaTN == ((byte?)beiToaNha.EditValue ?? Common.User.MaTN));
                    repTanXuat.DataSource = _db.tbl_TanSuats;

                    gc.DataSource = _db.tbl_KeHoachVanHanhs
                        .Where(_ =>_.MaTN == ((byte?)beiToaNha.EditValue ?? Common.User.MaTN) && _.IsKeHoachBaoTri.GetValueOrDefault()==false
                        ).ToList();
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
            //lueToaNha.DataSource = _db.tnToaNhas;
            beiToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            _db = new MasterDataContext();
            foreach (var v in objKbc.Source)
            {
                cbxKBC.Items.Add(v);
            }
            beiKBC.EditValue = objKbc.Source[7];
            SetDate(7);
            lkNhanVien.DataSource = _db.tnNhanViens.Select(o => new { o.MaNV, o.HoTenNV }).ToList();
            repProfile.DataSource = _db.tbl_Profiles;

            var l = new List<LoaiDanhSachChiTiet>();
            l.Add(new LoaiDanhSachChiTiet { ID = 1, Name = "Hệ thống" });
            l.Add(new LoaiDanhSachChiTiet { ID = 2, Name = "Loại tài sản" });
            l.Add(new LoaiDanhSachChiTiet { ID = 3, Name = "Tên tài sản" });
            repLoaiHeThong.DataSource = l;

            LoadData();

        }
        public class LoaiDanhSachChiTiet
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        private void LoadDetail()
        {
            _db = new MasterDataContext();
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
                        var objByNhomTaiSan = (from p in _db.tbl_KeHoachVanHanh_ChiTiets
                                join nts in _db.tbl_NhomTaiSans on p.MaTenTaiSanID equals nts.ID
                                where p.KeHoachVanHanhID == id &
                                      p.tbl_KeHoachVanHanh.LoaiHeThong == 1
                                orderby nts.TenNhomTaiSan
                                select new
                                {
                                    TenTaiSan = nts.TenNhomTaiSan,
                                    p.ProfileID, KhoiNha = ""
                                }).ToList();
                        var objByLoaiTaiSan = (from p in _db.tbl_KeHoachVanHanh_ChiTiets
                            join lts in _db.tbl_LoaiTaiSans on p.MaTenTaiSanID equals lts.ID
                            where p.KeHoachVanHanhID == id &
                                  p.tbl_KeHoachVanHanh.LoaiHeThong == 2
                            orderby lts.TenLoaiTaiSan
                            select new
                            {
                                TenTaiSan = lts.TenLoaiTaiSan, p.ProfileID, KhoiNha = ""
                            }).ToList();
                        var objByTenTaiSan = (from p in _db.tbl_KeHoachVanHanh_ChiTiets
                            join ts in _db.tbl_TenTaiSans on p.MaTenTaiSanID equals ts.ID
                            join kn in _db.mbKhoiNhas on ts.BlockID equals kn.MaKN into _knha
                            from kn in _knha.DefaultIfEmpty()
                            where p.KeHoachVanHanhID == id &
                                  (p.tbl_KeHoachVanHanh.LoaiHeThong == null || p.tbl_KeHoachVanHanh.LoaiHeThong == 3)
                            orderby p.TenTaiSan
                            select new
                            {
                                p.TenTaiSan,
                                p.ProfileID,
                                KhoiNha = kn.TenKN
                            }).ToList();
                        gcChiTiet.DataSource = objByNhomTaiSan.Concat(objByLoaiTaiSan).Concat(objByTenTaiSan);
                        break;
                    case "tabLichSuDuyet":
                        repNV.DataSource = _db.tnNhanViens;
                        gcLichSuDuyet.DataSource = (from ls in _db.tbl_KeHoachVanHanh_LichSuDuyets
                            join cv in _db.tnChucVus on ls.ChucVuID equals cv.MaCV
                            where ls.MaKeHoachVanHanh == id
                            select new
                            {
                                ls.ID, ls.IsNguoiCuoi, ls.MaKeHoachVanHanh, ls.NgayDuyet, ls.NguoiDuyet,
                                cv.TenCV
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
                _db.Dispose();
            }
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
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
                if (gridView1.GetFocusedRowCellValue("ID") != null)
                {
                    id = (int)gridView1.GetFocusedRowCellValue("ID");
                }
                using (var frm = new frmKeHoachVanHanh_Edit { MaTn = (byte?)beiToaNha.EditValue, IsSua = 0, Id = id })
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

                using (var frm = new frmKeHoachVanHanh_Edit
                {
                    MaTn = (byte?)beiToaNha.EditValue,
                    Id = (int?)gv.GetFocusedRowCellValue("ID"),
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
                    frm.MaTn = (byte)beiToaNha.EditValue;
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

        public CheckTime GetCdNew(DateTime a, DateTime b, DateTime c, DateTime d)
        {
            // trả về ngày nghỉ mới
            //int ngayNghi = 0;
            CheckTime ck=new CheckTime();
            // truong hop 1
            // ab trong cd: (c)---(A)----(B)--(d)
            // a>c & a<d
            // b>c & b<d
            if (a >= c & a <= d & b >= c & b <= d)
            {
                // lấy khoãng d đến d++
               //ck = new CheckTime {NgayBd = d, NgayKt = d.AddDays(((TimeSpan) (d - b)).Days)};
               ck = new CheckTime {NgayBd = a, NgayKt = b, Days = (b - a).Days+1};
               //ngayNghi = ((TimeSpan) (d - b)).Days+1;
            }
            // trường hợp 2
            // ab ngoài cd (A)--(c)--(d)--(B)
            // a<c, a <d, b>c, b>d
            if (a <= c & a <= d & b >= c & b >= d)
            {
                // lấy khoãng a  đến b++
                //ck = new CheckTime {NgayBd = a, NgayKt = b.AddDays(((TimeSpan) (d - c)).Days)};
                ck = new CheckTime {NgayBd = c, NgayKt = d, Days = (d - c).Days+1};
                //ngayNghi = ((TimeSpan) (d - c)).Days+1;
            }
            // trường hợp 3
            // b nằm trong khoãng cd (A)--(c)--(B)--(d)
            if (a <= c & a <= b & b >= c & b <= d)
            {
                //ck = new CheckTime {NgayBd = a, NgayKt = d.AddDays(((TimeSpan) (b - c)).Days)};
                ck = new CheckTime {NgayBd = c, NgayKt = b, Days = (b - c).Days+1};
                //ngayNghi = ((TimeSpan) (b - c)).Days+1;
            }
            // trường hợp 4
            // a nằm trong khoãng cd (c)--(A)--(d)--(B)
            if (a >= c & a <= d & b >= c & b >= d)
            {
                // thời gian làm = d=>b++
                //ck = new CheckTime {NgayBd = d, NgayKt = b.AddDays(((TimeSpan) (d - a)).Days)};
                ck = new CheckTime {NgayBd = a, NgayKt = d, Days = (d - a).Days + 1};
                //ngayNghi = ((TimeSpan) (d - a)).Days+1;
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
            TimeSpan ts = end - start;                       // Total duration
            int count = (int)Math.Floor(ts.TotalDays / 7);   // Number of whole weeks
            int remainder = (int)(ts.TotalDays % 7);         // Number of remaining days
            int sinceLastDay = (int)(end.DayOfWeek - day);   // Number of days since last [day]
            if (sinceLastDay < 0) sinceLastDay += 7;         // Adjust for negative days since last [day]

            // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
            if (remainder >= sinceLastDay) count++;

            return count;
        }

        public CheckTime GetAbnew(DateTime a, DateTime b, byte maTn)
        {
            //_db = new MasterDataContext();
            //CheckTime ck = new CheckTime {NgayBd = a, NgayKt = b};
            //var a = new DateTime(2019, 3, 13);
            //var b = new DateTime(2019, 3, 15); // 16 là thứ 7
            int ngayNghi = 0;

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
            if (firstTlnn != null)
            {
                if (firstTlnn.IsThuBay == true)
                {
                    b = b.AddDays(+CountDays(DayOfWeek.Saturday, a, b));
                     if (b.DayOfWeek == DayOfWeek.Saturday) b = b.AddDays(1);
                }

                if (firstTlnn.IsChuNhat == true)
                {
                    b = b.AddDays(+CountDays(DayOfWeek.Sunday, a, b));
                    if (b.DayOfWeek == DayOfWeek.Sunday) b = b.AddDays(1);
                }
            }

            CheckTime ck = new CheckTime {NgayBd = a, NgayKt = b,Days=ngayNghi};
            return ck;
        }



        private void CreateAutoPhieuVanHanh(byte? maTn, List<tbl_KeHoachVanHanh_ChiTiet> objChiTiet, DateTime tungay, DateTime denngay, int soNgay, DateTime denNgay, int? nts, int? id, int? profileId, int? idPhanLoaiCa,string kyHieuCa)
        {
            #region Create Phiếu vận hành auto
            // nếu nó có chi tiết, thì tuân theo loại hệ thống
            if (objChiTiet.Count > 0)
            {
                foreach (var item in objChiTiet)
                {
                    var idNhomTaiSan = new int?();
                    var idLoaiTaiSan = new int?();
                    var idTenTaiSan = new int?();

                    // vì trước đây dùng theo tên tài sản, nên phiếu cũ của họ không có loại hệ thống thì nó vẫn đang lấy theo tên tài sản
                    var loaiHeThong = item.tbl_KeHoachVanHanh.LoaiHeThong ?? 3;

                    switch (loaiHeThong)
                    {
                        case 1: // hệ thống
                            idNhomTaiSan = item.MaTenTaiSanID;
                            break;
                        case 2: // loại tài sản
                            var objGetLoaiTaiSan = _db.tbl_LoaiTaiSans.FirstOrDefault(_ => _.ID == item.MaTenTaiSanID);
                            if (objGetLoaiTaiSan != null)
                            {
                                idNhomTaiSan = objGetLoaiTaiSan.NhomTaiSanID;
                                idLoaiTaiSan = objGetLoaiTaiSan.ID;
                            }

                            break;
                        case 3: // tên tài sản
                            var objGetTenTaiSan = _db.tbl_TenTaiSans.FirstOrDefault(_ => _.ID == item.MaTenTaiSanID);
                            if (objGetTenTaiSan != null)
                            {
                                idNhomTaiSan = objGetTenTaiSan.tbl_LoaiTaiSan.NhomTaiSanID;
                                idLoaiTaiSan = objGetTenTaiSan.LoaiTaiSanID;
                                idTenTaiSan = objGetTenTaiSan.ID;
                            }

                            break;
                    }

                    // 14, 20

                    var tempTuNgay = tungay;

                    CheckTime ck = GetAbnew(tempTuNgay, denngay, (byte) maTn);

                    tempTuNgay = ck.NgayBd;
                    denngay = ck.NgayKt;

                    while (tempTuNgay.Date <= denngay.Date)
                    {
                        var vh = new tbl_PhieuVanHanh();
                        vh.NgayPhieu = Common.GetDateTimeSystem();
                        vh.KeHoachVanHanhID = id;
                        vh.SoPhieu = string.Format("PVH-{0:dd/MM/yyyy}-{1}", vh.TuNgay, kyHieuCa);
                        vh.NguoiDuyet = Common.User.MaNV;
                        vh.MaTN = maTn;

                        // nếu loại hệ thống == null, mặc định là tên tài sản = 3
                        vh.IsTenTaiSan = item.tbl_KeHoachVanHanh.LoaiHeThong == null ||
                                         item.tbl_KeHoachVanHanh.LoaiHeThong == 3;

                        // nếu loại hệ thống == 2 hoặc == 3: đang là loại tài sản hoặc tên tài sản, làm sao lấy dc nhóm tài sản của nó?

                        vh.NhomTaiSanID = idNhomTaiSan;
                        vh.TenTaiSanID = idTenTaiSan;
                        vh.LoaiTaiSanID = idLoaiTaiSan;
                        vh.LoaiHeThong = loaiHeThong;
                        vh.StatusLevelID = 1;
                        vh.TuNgay = tempTuNgay;
                        vh.DenNgay = soNgay == 1 ? tempTuNgay.Date : tempTuNgay.AddDays(soNgay);
                        CheckTime timeNew = GetAbnew(tempTuNgay, (DateTime) vh.DenNgay, (byte) maTn);
                        vh.DenNgay = timeNew.NgayKt;
                        //denngay = timeNew.NgayKt;
                        if (vh.DenNgay >= denngay)
                        {
                            vh.DenNgay = denngay;
                        }

                        vh.NgayNhap = Common.GetDateTimeSystem();
                        vh.NguoiNhap = Common.User.MaNV;

                        vh.PhanLoaiCaID = idPhanLoaiCa;
                        vh.TrangThaiPhieu = 0;
                        vh.HeThongTaiSanID = nts;
                        vh.IsPhieuBaoTri = false;
                        vh.IsBatThuong = false;
                        
                        _db.tbl_PhieuVanHanhs.InsertOnSubmit(vh);
                        var objProfileCT = _db.tbl_Profile_ChiTiets.Where(p => p.ProfileID == item.ProfileID);
                        foreach (var itemct in objProfileCT)
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
                            ct.DonViTinh = itemct.DonViTinh;
                            vh.ProfileLoaiID = itemct.tbl_Profile.LoaiID;
                            vh.tbl_PhieuVanHanh_ChiTiets.Add(ct);
                        }

                        #region kiểm tra tình trạng bất thường tự động
                        var flag = false;

                        foreach (var i in vh.tbl_PhieuVanHanh_ChiTiets)
                        {
                            if (i.IsChon == null || i.IsChon == false)
                            {
                                flag = true;
                            }
                        }

                        if (flag == false)
                        {
                            vh.StatusLevelID = _db.tbl_PhieuVanHanh_Status_Levels.First(_ => _.Levels == 0).ID;
                            vh.IsBatThuong = false;
                        }
                        else
                        {
                            vh.StatusLevelID = _db.tbl_PhieuVanHanh_Status_Levels.OrderByDescending(_ => _.Levels).First().ID;
                            vh.IsBatThuong = true;
                        }
                        #endregion

                        //Cập nhật lại từ ngày
                        //tempTuNgay = tempTuNgay.AddDays(soNgay);
                        tempTuNgay = timeNew.NgayKt.AddDays(1);
                    }
                }
            }
            //// nếu không có chi tiết, mặc định lấy theo nhóm tài sản
            //else
            //{
            //    while (tungay.Date <= denngay.Date)
            //    {
            //        var vh = new tbl_PhieuVanHanh();
            //        vh.MaTN = maTn;
            //        vh.IsTenTaiSan = false;
            //        vh.NhomTaiSanID = nts;
            //        vh.KeHoachVanHanhID = id;
            //        vh.TuNgay = tungay;
            //        vh.DenNgay =soNgay==1?tungay.Date: tungay.AddDays(soNgay);
            //        if (vh.DenNgay >= denNgay)
            //        {
            //            vh.DenNgay = denNgay;
            //        }
            //        vh.SoPhieu = string.Format("PVH-{0:dd/MM/yyyy}-{1}", vh.TuNgay,kyHieuCa);
            //        vh.NguoiDuyet = Common.User.MaNV;
            //        vh.NgayNhap = Common.GetDateTimeSystem();
            //        vh.NguoiNhap = Common.User.MaNV;
            //        vh.NgayPhieu = Common.GetDateTimeSystem();
            //        vh.PhanLoaiCaID = idPhanLoaiCa;
            //        vh.TrangThaiPhieu = 0;
            //        vh.HeThongTaiSanID = nts;

            //        vh.LoaiHeThong = 1; // phiếu kế hoạch lấy loại hệ thống mặc định = 1

            //        _db.tbl_PhieuVanHanhs.InsertOnSubmit(vh);
            //        var objProfileCt = _db.tbl_Profile_ChiTiets.Where(p => p.ProfileID == profileId);
            //        foreach (var item in objProfileCt)
            //        {
            //            var ct = new tbl_PhieuVanHanh_ChiTiet();
            //            ct.IsChon = item.GiaTriChon;
            //            ct.GiaTriNhap_Nhap = "";
            //            ct.ProfileID = item.ID;
            //            ct.TenCongViec = item.TenCongViec;
            //            ct.TenNhomCongViec = item.TenNhomCongViec;
            //            ct.TieuChuan = item.TieuChuan;
            //            ct.IsChon = true;
            //            ct.GiaTriChon = item.GiaTriChon;
            //            ct.IsHinhAnh = item.IsHinhAnh;
            //            vh.tbl_PhieuVanHanh_ChiTiets.Add(ct);
            //        }
            //        //Cập nhật lại từ ngày
            //        tungay = tungay.AddDays(soNgay);
            //    }

            //}
            #endregion
        }
        private void ItemDuyetTuDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                // kế hoạch tổng được duyệt, tức là người cuối đã duyệt. 
                // anh cuối đã duyệt, thì k thằng nào được duyệt nữa => return
                // chỉnh sửa duyệt tự động, khi duyệt tự động, nếu có ngày nghỉ thì tự động + ngày nghỉ
                // ví dụ: từ 1=>3, mà giữa có ngày thứ 7, chủ nhật => thành 1 => 5
                var duyet = (bool?)gv.GetFocusedRowCellValue("IsDuyet");
                if (duyet == true)
                {
                    DialogBox.Error("Kế hoạch đã được người cuối duyệt!");
                    return;
                }

                _db = new MasterDataContext();
                var nts = (int?)gv.GetFocusedRowCellValue("NhomTaiSanID");
                var id = (int?)gv.GetFocusedRowCellValue("ID");

                // thằng cuối chưa duyệt, và nó vẫn duyệt như những thằng khác, chỉ khác là khi nó duyệt thì phiếu tổng sẽ được duyệt
                #region Kiểm tra duyệt
                var ktCv = _db.tbl_FromDuyet_ChucVus.Where(_ => _.FormDuyetID == 2 & _.HeThongTaiSanID == nts & _.ChucVuID == Common.User.MaCV);
                // nếu có tức là đã phân quyền duyệt
                if (ktCv.Any()) // ktCv.Count()>0
                {
                    var ktNv = ktCv.Where(_ => _.NhanVienID != null);
                    // nếu count >0 là tính duyệt theo từng nhân viên
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
                            // kiểm tra có phải là thằng cuối k?
                            // nếu là thằng cuối thì sẽ chạy duyệt + lưu lại lịch sử
                            // còn nếu không phải thằng cuối thì chỉ lưu lịch sử
                            // kt thằng này đã duyệt trong lịch sử chưa, nếu duyệt rồi thì khỏi
                            var ktLs = _db.tbl_KeHoachVanHanh_LichSuDuyets.FirstOrDefault(_ =>
                                _.MaKeHoachVanHanh == id & _.NguoiDuyet == Common.User.MaNV);
                            if (ktLs != null)
                            {
                                DialogBox.Error("Phiếu này bạn đã duyệt rồi");
                                return;
                            }
                            var o = new tbl_KeHoachVanHanh_LichSuDuyet();
                            o.MaKeHoachVanHanh = id;
                            o.NguoiDuyet = Common.User.MaNV;
                            o.NgayDuyet = DateTime.Now;
                            o.ChucVuID = Common.User.MaCV;
                            _db.tbl_KeHoachVanHanh_LichSuDuyets.InsertOnSubmit(o);

                            if (ktNvCt.IsDuyet != true)
                            {
                                _db.SubmitChanges();
                                DialogBox.Success("Duyệt thành công");
                                return;
                            }
                            else
                            {
                                // thằng cuối đây rồi
                                o.IsNguoiCuoi = true;
                            }

                        }
                    }
                    // ngược lại là duyệt theo cả cấp độ
                    else
                    {
                        // xác định thêm trường hợp nếu không có nhân viên, thì bắt buộc phải có nhân viên => ok, không có nhân viên, khỏi duyệt
                        // cách cũ là không có nhân viên, chỉ cần mày trong cấp thì mày duyệt xong phiếu thành duyệt
                        // còn giờ k có nhân viên, khỏi duyệt
                        DialogBox.Error("Kế hoạch này không có nhân viên nào được phân quyền duyệt");
                        return;
                    }

                }
                // cái này thì hiển nhiên k dc phân quyền duyệt
                else
                {
                    DialogBox.Error("Kế hoạch này không có nhân viên nào được phân quyền duyệt");
                    return;
                }
                #endregion

                var maTn = ((byte?)beiToaNha.EditValue ?? Common.User.MaTN);

                var tuNgay = (DateTime)gv.GetFocusedRowCellValue("TuNgay");
                var denNgay = (DateTime)gv.GetFocusedRowCellValue("DenNgay");
                var tanSuatId = (int)gv.GetFocusedRowCellValue("TanSuatID");

                var profileId = (int?)gv.GetFocusedRowCellValue("ProfileID");
                var objChiTiet = _db.tbl_KeHoachVanHanh_ChiTiets.Where(p => p.KeHoachVanHanhID == id).ToList();
                var sn = (from ts in _db.tbl_TanSuats
                          where ts.ID == tanSuatId
                          select new
                          {
                              ts.SoNgay
                          }).FirstOrDefault();
                var soNgay = sn.SoNgay.Value;
                var _tungay = new DateTime(tuNgay.Year, tuNgay.Month, tuNgay.Day);
                var _denngay = new DateTime(denNgay.Year, denNgay.Month, denNgay.Day);

                var idCas = gv.GetFocusedRowCellValue("PhanLoaiCaIDs").ToString().Split(',');
                if (idCas.Count() > 0)
                {
                    try
                    {
                        int[] itemIdCas = idCas.Select(int.Parse).ToArray();
                        foreach (var item in itemIdCas)
                        {
                            var kyHieuCa = _db.tbl_PhanCong_PhanLoaiCas.First(_ => _.ID == item).KyHieu;
                            CreateAutoPhieuVanHanh(maTn, objChiTiet, _tungay, _denngay, soNgay, denNgay, nts, id, profileId, item, kyHieuCa);
                        }
                    }
                    catch
                    {
                        DialogBox.Error("Phiếu kế hoạch chưa được phân ca.");
                        return;
                    }
                }
                else
                {
                    CreateAutoPhieuVanHanh(maTn, objChiTiet, _tungay, _denngay, soNgay, denNgay, nts, id, profileId, null, "");
                }


                var objKhvh = _db.tbl_KeHoachVanHanhs.FirstOrDefault(o => o.ID == id);
                objKhvh.IsDuyet = true;
                objKhvh.NgayDuyet = DateTime.Now;
                objKhvh.NguoiSua = Common.User.MaNV;
                objKhvh.NguoiDuyet = Common.User.MaNV;
                objKhvh.NgaySua = DateTime.Now;
                _db.SubmitChanges();
                DialogBox.Success("Duyệt thành công");
                LoadData();
            }
            catch(System.Exception ex)
            {
                string mes = Translate.TranslateGoogle.TranslateText(ex.Message, "en-us", "vi-vn");
                XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                //args.AutoCloseOptions.Delay = 1000;
                args.Caption = ex.GetType().FullName;
                args.Text = ex.Message + " (" + mes + ")";
                args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Cancel };
                XtraMessageBox.Show(args).ToString();
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
                    if (beiToaNha.EditValue == null) continue;
                    var o2 = new tbl_KeHoachVanHanh
                    {
                        NgayLapKeHoach = o1.NgayLapKeHoach,
                        TuNgay = o1.TuNgay,
                        DenNgay = o1.DenNgay,
                        MaTN = o1.MaTN,
                        NhomTaiSanID = o1.NhomTaiSanID,
                        TanSuatID = o1.TanSuatID,
                        PhanLoaiCaIDs=o1.PhanLoaiCaIDs,
                        PhanLoaiCaKyHieus=o1.PhanLoaiCaKyHieus,
                        IsKeHoachBaoTri=o1.IsKeHoachBaoTri,
                        NgayNhap = DateTime.Now,
                        NguoiNhap = Common.User.MaNV,
                        ChiPhiTheoKh=o1.ChiPhiTheoKh,
                        ChiPhiThucHien=o1.ChiPhiThucHien,
                        LoaiHeThong=o1.LoaiHeThong,
                        SoKeHoach= Common.CreateKeHoachVanHanh((byte)beiToaNha.EditValue, o1.NhomTaiSanID ?? 0,
                            o1.NgayLapKeHoach!=null?o1.NgayLapKeHoach.Value.Year:DateTime.Now.Year)
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
                using (var frm = new frmKeHoachVanhanh_ChiPhi
                {
                    ListId=listId,
                    LoaiChiPhi=0
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
                using (var frm = new frmKeHoachVanhanh_ChiPhi
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

        private void itemXoaDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _db = new MasterDataContext();
                int[] indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần xóa Duyệt");
                    return;
                }
                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                foreach (var r in indexs)
                {
                    var o = _db.tbl_KeHoachVanHanhs.FirstOrDefault(_ =>
                        _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                    if (o != null)
                    {
                        // delete all phiếu vận hành

                        var o1 = _db.tbl_PhieuVanHanhs.Where(_ =>
                            _.KeHoachVanHanhID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                        foreach (var itemPvh in o1)
                        {
                            _db.tbl_PhieuVanHanh_ChiTiets.DeleteAllOnSubmit(
                                _db.tbl_PhieuVanHanh_ChiTiets.Where(_ => _.PhieuVanHanhID == itemPvh.ID));
                            _db.tbl_PhieuVanHanh_LichSus.DeleteAllOnSubmit(
                                _db.tbl_PhieuVanHanh_LichSus.Where(_ => _.PhieuVanHanhID == itemPvh.ID));
                            _db.tbl_PhieuVanHanhs.DeleteOnSubmit(itemPvh);
                        }

                        _db.tbl_KeHoachVanHanh_LichSuDuyets.DeleteAllOnSubmit(
                            _db.tbl_KeHoachVanHanh_LichSuDuyets.Where(_ => _.MaKeHoachVanHanh == o.ID));
                        o.IsDuyet = false;
                        o.NgayDuyet = new DateTime?();
                        o.NguoiSua = Common.User.MaNV;
                        o.NguoiDuyet = new int?();
                        o.NgaySua = DateTime.Now;
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //testNgay();
        }

    }
}