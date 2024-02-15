using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using DevExpress.XtraEditors;
using System.Linq;
using System.Windows.Forms;
using Building.Asset.BaoTri;
using Building.Asset.VanHanh;
using Library;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace Building.Asset.BaoCao
{
    public partial class frmViewKeHoachBaoTri : XtraForm
    {
        //private MasterDataContext _db=new MasterDataContext();
        private DataTable _data;
        private DateTime _denNgay;
        private bool flag = false;
        private List<KeHoachVanHanhItem> keHoachVanHanh;
        private List<DataItem> _nts, tts, lts;
        private List<BackgroundWorker> _ltThread = new List<BackgroundWorker>();
        private bool _isStopThread = false;
        private int _nam;

        #region Class

        public class KeHoachVanHanhItem
        {
            public int? ID { get; set; }
            public int? NhomTaiSanID { get; set; }
            public int? LoaiHeThong { get; set; }

            public string TanSuat { get; set; }

            public short? MaTN { get; set; }

            public decimal? ChiPhi { get; set; }

            public DateTime TuNgay { get; set; }
            public DateTime DenNgay { get; set; }
        }

        public class DataItem
        {
            public string TenNhomTaiSan { get; set; }
            //public string LoaiTaiSan { get; set; }
            //public string TenTaiSan { get; set; }
            public string Profile { get; set; }
            public string TanSuat { get; set; }
            public DateTime TuNgay { get; set; }
            public DateTime DenNgay { get; set; }
            public decimal? ChiPhi { get; set; }
            public int? ID { get; set; }
            public int? SoNgayThucHien { get; set; }
        }

        #endregion

        public frmViewKeHoachBaoTri()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void GetTask(int i)
        {
            using (var db = new MasterDataContext())
            {
                switch (i)
                {
                    case 1:
                        // danh sách nhóm tài sản
                        _nts = (from p in keHoachVanHanh
                            join n in db.tbl_NhomTaiSans on p.NhomTaiSanID equals n.ID
                            //where p.LoaiHeThong == 1
                            select new DataItem
                            {
                                TenNhomTaiSan = n.TenNhomTaiSan,
                                //LoaiTaiSan = "-",
                                //TenTaiSan = "-",
                                TuNgay = p.TuNgay,
                                DenNgay = p.DenNgay,
                                TanSuat = p.TanSuat,
                                SoNgayThucHien = (p.DenNgay - p.TuNgay).Days + 1,
                                ChiPhi = p.ChiPhi,
                                ID = p.ID
                            }).ToList();
                        break;
                    //case 2:
                    //    // danh sách loại tài sản
                    //    lts = (from p in keHoachVanHanh
                    //        join l in db.tbl_LoaiTaiSans on p.NhomTaiSanID equals l.ID
                    //        where p.LoaiHeThong == 2
                    //        select new DataItem
                    //        {
                    //            TenNhomTaiSan = l.tbl_NhomTaiSan.TenNhomTaiSan,
                    //            LoaiTaiSan = l.TenLoaiTaiSan,
                    //            TenTaiSan = "Tất cả tên tài sản",
                    //            TuNgay = p.TuNgay,
                    //            DenNgay = p.DenNgay,
                    //            TanSuat = p.TanSuat,
                    //            SoNgayThucHien = (p.DenNgay - p.TuNgay).Days + 1,
                    //            ChiPhi = p.ChiPhi,
                    //            ID = p.ID
                    //        }).ToList();
                    //    break;
                    //case 3:
                    //    // danh sách tên tài sản
                    //    tts = (from p in keHoachVanHanh
                    //        join t in db.tbl_TenTaiSans on p.NhomTaiSanID equals t.ID
                    //        where p.LoaiHeThong == 3
                    //        select new DataItem
                    //        {
                    //            TenNhomTaiSan = t.tbl_LoaiTaiSan.tbl_NhomTaiSan.TenNhomTaiSan,
                    //            LoaiTaiSan = t.tbl_LoaiTaiSan.TenLoaiTaiSan,
                    //            TenTaiSan = t.TenTaiSan,
                    //            TuNgay = p.TuNgay,
                    //            DenNgay = p.DenNgay,
                    //            TanSuat = p.TanSuat,
                    //            SoNgayThucHien = (p.DenNgay - p.TuNgay).Days + 1,
                    //            ChiPhi = p.ChiPhi,
                    //            ID = p.ID
                    //        }).ToList();
                    //    break;
                    default:
                        _isStopThread = true;
                        break;
                }
            }
        }

        public DateTime NgayDauTuan(DateTime ngay)
        {
            DateTime ngayDauTuan = DateTime.Now;
            // kiểm tra ngày cuối tháng có phải là ngày trong tuần hay k
            var ngayTrongTuan = ngay.DayOfWeek;
            if (ngayTrongTuan == DayOfWeek.Sunday)
            {
                ngayDauTuan = ngay.AddDays(-6);
            }
            else
            {
                int offset = ngayTrongTuan - DayOfWeek.Monday;
                ngayDauTuan = ngay.AddDays(-offset);
            }

            return ngayDauTuan;
        }

        public void GetData()
        {
            if (_nts == null) _nts = new List<DataItem>();
            //if (lts == null) lts = new List<DataItem>();
            //if (tts == null) tts = new List<DataItem>();
            // danh sách tổng
            //var objKiemTraDinhKy = nts.Concat(lts).Concat(tts);
            var objKiemTraDinhKy = _nts;
            #region Đổ cột cố định

            _data.Columns.Add("TenNhomTaiSan");
            //_data.Columns.Add("LoaiTaiSan");
            //_data.Columns.Add("TenTaiSan");
            //_data.Columns.Add("Profile");
            _data.Columns.Add("TuNgay");
            _data.Columns.Add("DenNgay");
            _data.Columns.Add("TanSuat");
            _data.Columns.Add("SoNgayThucHien");
            _data.Columns.Add("ChiPhi");
            _data.Columns.Add("ID");

            // đổ band cố định
            GridBand band = new GridBand();
            band.Caption = @"THÔNG TIN HỆ THỐNG";
            band.Name = @"HeThong";
            band.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

            gv.Bands.Add(band);

            gv.Columns.AddField("ID");
            var colId = new BandedGridColumn();
            colId.Name = "colID";
            colId.Caption = @"Hệ thống";
            colId.FieldName = "ID";
            colId.Visible = false;
            colId.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colId.OwnerBand = band;

            // đổ gridview
            gv.Columns.AddField("Hệ thống");
            var colNhomTaiSan = new BandedGridColumn();
            colNhomTaiSan.Name = "colNhomTaiSan";
            colNhomTaiSan.Caption = @"Hệ thống";
            colNhomTaiSan.FieldName = "TenNhomTaiSan";
            colNhomTaiSan.Visible = false;
            colNhomTaiSan.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colNhomTaiSan.OwnerBand = band;
            //colNhomTaiSan.VisibleIndex = 0;

            ////gv.Columns.Add(colNhomTaiSan);
            //gv.Columns.AddField("Loại tài sản");
            //var colLoaiTaiSan = new BandedGridColumn();
            //colLoaiTaiSan.Name = "colLoaiTaiSan";
            //colLoaiTaiSan.Caption = @"Loại tài sản";
            //colLoaiTaiSan.FieldName = "LoaiTaiSan";
            //colLoaiTaiSan.Visible = false;
            //colLoaiTaiSan.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //colLoaiTaiSan.OwnerBand = band;
            ////colLoaiTaiSan.VisibleIndex = 1;
            ////gv.Columns.Add(colLoaiTaiSan);

            //gv.Columns.AddField("Tên tài sản");
            //var colTenTaiSan = new BandedGridColumn();
            //colTenTaiSan.Name = "colTenTaiSan";
            //colTenTaiSan.Caption = @"Tên tài sản";
            //colTenTaiSan.FieldName = "TenTaiSan";
            //colTenTaiSan.Visible = false;
            //colTenTaiSan.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //colTenTaiSan.OwnerBand = band;
            //colTenTaiSan.VisibleIndex = 0;
            ////gv.Columns.Add(colTenTaiSan);

            //gv.Columns.AddField("Profile");
            //var colProfile = new BandedGridColumn();
            //colProfile.Name = "colProfile";
            //colProfile.Caption = @"Profile";
            //colProfile.FieldName = "Profile";
            //colProfile.Visible = true;
            //colProfile.VisibleIndex = 1;
            //colTenTaiSan.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //colProfile.OwnerBand = band;
            //gv.Columns.Add(colProfile);

            gv.Columns.AddField("Từ ngày");
            var colTuNgay = new BandedGridColumn();
            colTuNgay.Name = "colTuNgay";
            colTuNgay.Caption = @"Từ ngày";
            colTuNgay.FieldName = "TuNgay";
            colTuNgay.Visible = true;
            colTuNgay.VisibleIndex = 1;
            colTuNgay.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colTuNgay.OwnerBand = band;
            //gv.Columns.Add(colTuNgay);

            gv.Columns.AddField("Đến ngày");
            var colDenNgay = new BandedGridColumn();
            colDenNgay.Name = "colDenNgay";
            colDenNgay.Caption = @"Đến ngày";
            colDenNgay.FieldName = "DenNgay";
            colDenNgay.Visible = true;
            colDenNgay.VisibleIndex = 2;
            colDenNgay.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colDenNgay.OwnerBand = band;
            //gv.Columns.Add(colDenNgay);

            gv.Columns.AddField("Tần suất");
            var colTanSuat = new BandedGridColumn();
            colTanSuat.Name = "colTanSuat";
            colTanSuat.Caption = @"Tần suất";
            colTanSuat.FieldName = "TanSuat";
            colTanSuat.Visible = true;
            colTanSuat.VisibleIndex = 3;
            colTanSuat.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colTanSuat.OwnerBand = band;
            //gv.Columns.Add(colTanSuat);

            gv.Columns.AddField("Số ngày thực hiện");
            var colSoNgayThucHien = new BandedGridColumn();
            colSoNgayThucHien.Name = "colSoNgayThucHien";
            colSoNgayThucHien.Caption = @"Số ngày thực hiện";
            colSoNgayThucHien.FieldName = "SoNgayThucHien";
            colSoNgayThucHien.Visible = true;
            colSoNgayThucHien.VisibleIndex = 4;
            colSoNgayThucHien.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colSoNgayThucHien.OwnerBand = band;

            gv.Columns.AddField("Chi phí");
            var colChiPhi = new BandedGridColumn();
            colChiPhi.Name = "colChiPhi";
            colChiPhi.Caption = @"Chi phí";
            colChiPhi.FieldName = "ChiPhi";
            colChiPhi.Visible = true;
            colChiPhi.VisibleIndex = 5;
            colChiPhi.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colChiPhi.OwnerBand = band;
            colChiPhi.BestFit();
            band.Visible = true;
            band.Fixed = FixedStyle.Left;

            #endregion
            if ((int)itemChon.EditValue == 1)
            {
                #region Đổ cột tháng tự động

                GridBand bandThang = new GridBand();
                bandThang.Caption = @"NĂM " + _nam;
                bandThang.Name = "Nam" + _nam;
                bandThang.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gv.Bands.Add(bandThang);

                for (var i = 1; i <= 12; i++)
                {
                    _data.Columns.Add("Thang" + i);

                    gv.Columns.AddField("Tháng " + i);
                    var cThang = new BandedGridColumn();
                    cThang.Name = "Thang" + i;
                    cThang.Caption = @"Tháng " + i;
                    cThang.FieldName = "Thang" + i;
                    cThang.Visible = true;
                    cThang.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    cThang.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    cThang.VisibleIndex = 5 + i;
                    cThang.OwnerBand = bandThang;
                }

                bandThang.Visible = true;

                #endregion

                #region Đổ dữ liệu

                // đổ dữ liệu
                foreach (var row in objKiemTraDinhKy)
                {
                    var r = _data.NewRow();
                    r["TenNhomTaiSan"] = row.TenNhomTaiSan;
                    //r["LoaiTaiSan"] = row.LoaiTaiSan;
                    //r["TenTaiSan"] = row.TenTaiSan;
                    //r["Profile"] = row.Profile;
                    r["TuNgay"] = String.Format("{0:dd/MM/yyyy}", row.TuNgay);
                    r["DenNgay"] = String.Format("{0:dd/MM/yyyy}", row.DenNgay);
                    r["TanSuat"] = row.TanSuat;
                    r["SoNgayThucHien"] = row.SoNgayThucHien;
                    r["ChiPhi"] = String.Format("{0:#,0.##}", row.ChiPhi);
                    r["ID"] = row.ID;

                    string duLieuNgay = "";
                    // lấy ra danh sách tháng trong từ ngày đến ngày
                    var thangDau = row.TuNgay.Month;
                    while (thangDau <= row.DenNgay.Month)
                    {
                        // trong tháng, lấy ra danh sách ngày
                        var ngayDau = thangDau == row.TuNgay.Month ? row.TuNgay.Day : 1;
                        int ngayCuoi;

                        if (thangDau == row.DenNgay.Month) ngayCuoi = row.DenNgay.Day;
                        else
                        {
                            DateTime ngayCuoiThang = new DateTime(row.TuNgay.Year, thangDau, 1);
                            ngayCuoiThang = ngayCuoiThang.AddMonths(1);
                            ngayCuoiThang = ngayCuoiThang.AddDays(-ngayCuoiThang.Day);
                            ngayCuoi = ngayCuoiThang.Day;
                        }

                        if (ngayDau == ngayCuoi) r["Thang" + thangDau] = ngayDau.ToString();
                        else
                        {
                            if (thangDau == row.TuNgay.Month)
                                r["Thang" + thangDau] = row.TuNgay.Day + " - " + row.DenNgay.Day;
                            else
                                r["Thang" + thangDau] = "-";
                        }

                        //r["Thang" + thangDau] = ngayDau + " - " + ngayCuoi;
                        thangDau = thangDau + 1;
                    }

                    _data.Rows.Add(r);
                }

                #endregion
            }
            else
            {
                try
                {
                    #region Đổ dữ liệu cột tuần tự động

                    var listTuan = new List<int>();
                    for (int item = 1; item <= 12; item++)
                    {
                        GridBand bandTuan = new GridBand();
                        bandTuan.Caption = @"Tháng " + item;
                        bandTuan.Name = @"Thang" + item;
                        bandTuan.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        gv.Bands.Add(bandTuan);

                        //var ngayDauNam = new DateTime((int)itemNam.EditValue, 1, 1);
                        ////tùy từng văn hóa khác nhau, có số tuần khác nhau
                        //var tuanDauTien = ngayDauNam.AddDays(1 - (int)(ngayDauNam.DayOfWeek));
                        //var listTuan =
                        //    Enumerable
                        //        .Range(0, 54)
                        //        .Select(i => new
                        //        {
                        //            tuanBatDau = tuanDauTien.AddDays(i * 7)
                        //        })
                        //        .TakeWhile(x => x.tuanBatDau.Year <= ngayDauNam.Year)
                        //        .Select(x => new
                        //        {
                        //            tuanBatDau = x.tuanBatDau,
                        //            tuanKetThuc = x.tuanBatDau.AddDays(4)

                        //        })
                        //        .SkipWhile(x => x.tuanKetThuc < ngayDauNam.AddDays(1))
                        //        .Select((x, i) => new
                        //        {
                        //            tuanBatDau = x.tuanBatDau,
                        //            tuanKetThuc = x.tuanKetThuc,
                        //            soTuan = i + 1
                        //        });
                        //var tuanInThang = listTuan.Where(_ =>
                        //    _.tuanKetThuc.Month == item && _.tuanKetThuc.Year == _nam);

                        // danh sách tuần của năm
                        var ngayDauThang = new DateTime(_nam, item, 1); // ngày đầu tháng
                        var ngayCuoiThang = ngayDauThang.AddMonths(1).AddDays(-1);
                        // ngày đầu tiên của tuần
                        while (ngayDauThang < ngayCuoiThang)
                        {
                            // tìm tuần của từ ngày
                            var cal = CultureInfo.CurrentCulture.Calendar;
                            var tuan = cal.GetWeekOfYear(ngayDauThang, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                            DateTime monday = DateTime.Now;

                            // trong khoãng từ ngày đến ngày, lấy ra danh sách tuần, đầu tiên lấy ra monday và sunday của từ ngày
                            var dayOfWeek = ngayDauThang.DayOfWeek; // ngày trong tuần

                            if (dayOfWeek == DayOfWeek.Sunday)
                            {
                                // nếu sét chủ nhật là ngày cuối tuần
                                monday = ngayDauThang.AddDays(-6);
                            }
                            else
                            {
                                // nếu không phải là ngày thứ 2 thì lùi lại cho đến ngày thứ 2
                                int offset = dayOfWeek - DayOfWeek.Monday;
                                monday = ngayDauThang.AddDays(-offset);
                            }

                            if(listTuan.All(_ => _ != tuan))
                            {
                                listTuan.Add(tuan);
                                // tìm ngày cuối tuần của từ ngày
                                _data.Columns.Add("Tuan" + tuan);

                                gv.Columns.AddField("T" + tuan);
                                var cTuan = new BandedGridColumn();
                                cTuan.Name = "Tuan" + tuan;
                                cTuan.Caption = @"T" + tuan;
                                cTuan.FieldName = "Tuan" + tuan;
                                cTuan.Visible = true;
                                cTuan.AppearanceCell.Options.UseTextOptions = true;
                                cTuan.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                                cTuan.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                                cTuan.VisibleIndex = 5 + tuan;

                                cTuan.OwnerBand = bandTuan;
                            }
                            // ngày đầu tuần tiếp theo
                            ngayDauThang = monday.AddDays(7);
                        }

                        //foreach (var i in tuanInThang)
                        //{
                        //    _data.Columns.Add("Tuan" + i.soTuan);

                        //    gv.Columns.AddField("T" + i.soTuan);
                        //    var cTuan = new BandedGridColumn();
                        //    cTuan.Name = "Tuan" + i.soTuan;
                        //    cTuan.Caption = @"T" + i.soTuan;
                        //    cTuan.FieldName = "Tuan" + i.soTuan;
                        //    cTuan.Visible = true;
                        //    cTuan.AppearanceCell.Options.UseTextOptions = true;
                        //    cTuan.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        //    cTuan.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        //    cTuan.VisibleIndex = 5 + i.soTuan;

                        //    cTuan.OwnerBand = bandTuan;
                        //}

                        bandTuan.Visible = true;

                    }

                    #endregion

                    #region Đổ dữ liệu

                    // đổ dữ liệu
                    foreach (var row in objKiemTraDinhKy)
                    {
                        var r = _data.NewRow();
                        r["TenNhomTaiSan"] = row.TenNhomTaiSan;
                        //r["LoaiTaiSan"] = row.LoaiTaiSan;
                        //r["TenTaiSan"] = row.TenTaiSan;
                        //r["Profile"] = row.Profile;
                        r["TuNgay"] = String.Format("{0:dd/MM/yyyy}", row.TuNgay);
                        r["DenNgay"] = String.Format("{0:dd/MM/yyyy}", row.DenNgay);
                        r["TanSuat"] = row.TanSuat;
                        r["SoNgayThucHien"] = row.SoNgayThucHien;
                        r["ChiPhi"] = row.ChiPhi;
                        //r["ChiPhi"] = row.ChiPhi;
                        r["ChiPhi"] = String.Format("{0:#,0.##}", row.ChiPhi);
                        r["ID"] = row.ID;

                        // tính ngày và khoãng ngày trong tuần, trong 1 khoãng của row có từ ngày đến ngày
                        // ví dụ: từ ngày thứ 6 tuần này tới thứ 6 tuần sau
                        // từ ngày: = thứ 6 tuần này
                        // đến ngày = thứ 6 tuần sau
                        // for trong khoãng thứ 6 tuần này đến thứ 6 tuần sau. Chạy cộng thêm 1 tuần
                        // ngày đầu tiên = từ ngày. sau đó tính ngày cuối tuần, là phải tính ngày đầu tuần của cái từ ngày, rồi tính ngày cuối tuần
                        // sau khi tính dc ngày cuối tuần thì tính tiếp ngày của tuần tiếp theo, gán ngày đầu tiên hoạt động cho ngày tiếp theo
                        // trong 1 vòng for, là 1 tuần, nên time = từ ngày đến ngày cuối tuần.
                        // sau đó qua tuần tiếp theo. Nếu tuần tiếp theo có chứa cuối tuần, thì nó sẽ = ngày dầu tuần - ngày cuối tuần, kiểm tra nếu ngày cuối tuần lớn hơn đến ngày, thì ngày đến ngày = ngày cuối tuần
                        var ngayDau = row.TuNgay;
                        //var ngayCuoi = row.DenNgay;
                        var i = 1;
                        while (ngayDau <= row.DenNgay)
                        {
                            // tìm tuần của từ ngày
                            var cal = CultureInfo.CurrentCulture.Calendar;
                            var tuan = cal.GetWeekOfYear(ngayDau, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                            DateTime monday = DateTime.Now;
                            if (ngayDau == row.DenNgay) r["Tuan" + tuan] = ngayDau.Day;
                            else
                            {
                                // trong khoãng từ ngày đến ngày, lấy ra danh sách tuần, đầu tiên lấy ra monday và sunday của từ ngày
                                var dayOfWeek = ngayDau.DayOfWeek; // ngày trong tuần

                                if (dayOfWeek == DayOfWeek.Sunday)
                                {
                                    // nếu sét chủ nhật là ngày cuối tuần
                                    monday = ngayDau.AddDays(-6);
                                }
                                else
                                {
                                    // nếu không phải là ngày thứ 2 thì lùi lại cho đến ngày thứ 2
                                    int offset = dayOfWeek - DayOfWeek.Monday;
                                    monday = ngayDau.AddDays(-offset);
                                }

                                // tìm ngày cuối tuần của từ ngày
                                //DateTime sunday = monday.AddDays(5);
                                if (i == 1) r["Tuan" + tuan] = row.TuNgay.Day + " - " + row.DenNgay.Day;
                                else r["Tuan" + tuan] = "-";

                                //if (row.DenNgay <= sunday)
                                //{
                                //r["Tuan" + tuan] = ngayDau.Day.ToString()+"/"+ngayDau.Month.ToString() + " - " + row.DenNgay.Day.ToString()+"/"+row.DenNgay.Month.ToString();
                                //}
                                //else
                                //{
                                // nếu làm trong ngày nghỉ
                                //if(ngayDau>sunday) r["Tuan" + tuan] = ngayDau.Day.ToString() + "/" + ngayDau.Month.ToString();
                                //else r["Tuan" + tuan] = ngayDau.Day.ToString()+"/"+ngayDau.Month.ToString() + " - " + sunday.Day.ToString()+"/"+sunday.Month.ToString();
                                // }
                            }

                            // ngày đầu tuần tiếp theo
                            ngayDau = monday.AddDays(7);
                            i++;
                        }

                        _data.Rows.Add(r);
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    //DialogBox.Error(ex.ToString());
                }
                
            }

            gv.GroupCount = 1;
#pragma warning disable 618
            gv.GroupFooterShowMode = GroupFooterShowMode.Hidden;
#pragma warning restore 618
            gv.SortInfo.AddRange(new[]
                    {
                        new GridColumnSortInfo(colNhomTaiSan, DevExpress.Data.ColumnSortOrder.Ascending),
                        //new GridColumnSortInfo(colLoaiTaiSan, DevExpress.Data.ColumnSortOrder.Ascending)
                    });
            gv.OptionsBehavior.Editable = false;

            gc.DataSource = _data;
            flag = false;
            itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            gv.BestFitColumns();
        }

        private void LoadData()
        {
            gv.Bands.Clear();
            gv.Columns.Clear();
            var strToaNha = (itemTN.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
            var ltToaNha = strToaNha.Split(',');
            if (strToaNha == "") return;

            _data = new DataTable();
            try
            {
                if (itemChon.EditValue != null & itemNam.EditValue != null)
                {
                    itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    _nam = int.Parse(itemNam.EditValue.ToString());
                    var db = new MasterDataContext();

                    // 1. từ ngày đến ngày nằm trong năm
                    // value year == year
                    // từ ngày đến ngày, có đến ngày hoặc từ ngày nằm ngoài năm, thì từ ngày hoặc đến ngày = 1/1 hoặc 31/12
                    // đổ dữ liệu từ ngày đến ngày mới cho các phiếu

                    keHoachVanHanh = db.tbl_KeHoachVanHanhs.Where(_ =>
                        ltToaNha.Contains(_.MaTN.ToString()) & _.TuNgay != null & _.DenNgay != null &
                        _.IsKeHoachBaoTri == true & (_.TuNgay.Value.Year == _nam || _.DenNgay.Value.Year == _nam)).Select(
                        _ => new KeHoachVanHanhItem
                        {
                            ID = _.ID,
                            NhomTaiSanID = _.NhomTaiSanID,
                            MaTN = _.MaTN,
                            LoaiHeThong = _.LoaiHeThong,
                            TanSuat = _.tbl_TanSuat.TenTanSuat,
                            ChiPhi = _.ChiPhiThucHien,
                            TuNgay = _.TuNgay.Value.Year == _nam ? _.TuNgay.Value.Date :
                                _.DenNgay.Value.Year == _nam ? new DateTime(_.DenNgay.Value.Year, 1, 1) :
                                _.DenNgay.Value.Year > _nam ? new DateTime(_nam, 1, 1) : _.TuNgay.Value.Date,
                            DenNgay = _.DenNgay.Value.Year == _nam
                                ? _.DenNgay.Value.Date
                                : _.TuNgay.Value.Year == _nam
                                    ? new DateTime(_.TuNgay.Value.Year, 12, 31)
                                    : _.TuNgay.Value.Year > _nam
                                        ? new DateTime(_nam, 12, 31)
                                        : _.DenNgay.Value.Date,
                        }).ToList();

                    var ti = 1;

                    foreach (var thread in _ltThread)
                    {
                        thread.RunWorkerAsync(ti);
                        ti++;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }


        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            cmbToaNha.DataSource = Common.TowerList;

            var l = new List<ChonClass>();
            l.Add(new ChonClass {ID = 1, Name = "Tháng"});
            l.Add(new ChonClass {ID = 2, Name = "Tuần"});
            repChon.DataSource = l;

            itemNam.EditValue = DateTime.Now.Year;

            // khởi tạo thread
            for (int i = 0; i < 1; i++)
            {
                var thread = new BackgroundWorker();
                thread.DoWork += backroundWorker_DoWork;
                thread.RunWorkerCompleted += backgroundWorker_RunWorkerComplete;
                _ltThread.Add(thread);
            }
            LoadData();
        }

        private void backgroundWorker_RunWorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!_isStopThread) ((BackgroundWorker) sender).RunWorkerAsync(4);
            else if (!_ltThread.Any(_ => _.IsBusy)) GetData();
        }

        private void backroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var ht = (int)e.Argument;
            GetTask(ht);
        }

        public class ChonClass
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gv_GroupLevelStyle(object sender, GroupLevelStyleEventArgs e)
        {
            switch (e.Level)
            {
                case 0:
                    e.LevelAppearance.Options.UseBackColor = true;
                    e.LevelAppearance.BackColor = Color.White;
                    //e.LevelAppearance.ForeColor = Color.White;
                    break;
                case 1:
                    e.LevelAppearance.Options.UseBackColor = true;
                    e.LevelAppearance.BackColor = Color.White;
                    //e.LevelAppearance.BackColor = Color.FromArgb(141, 180, 226);
                    break;
                case 2:
                    e.LevelAppearance.Options.UseBackColor = true;
                    e.LevelAppearance.BackColor = Color.White;
                    //e.LevelAppearance.BackColor = Color.FromArgb(197, 217, 241);
                    break;
                default:
                    //do nothing
                    break;
            }
        }

        private void gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName.Contains("Thang"))
            {
                if (e.CellValue != null && !string.IsNullOrEmpty(e.CellValue.ToString()))
                {
                    e.Appearance.BackColor = Color.LightSkyBlue;
                    if ((string) e.CellValue == "-")
                    {
                        e.Appearance.ForeColor = Color.LightSkyBlue;
                    }
                }

            }
            if (e.Column.FieldName.Contains("Tuan"))
            {
                if (e.CellValue != null && !string.IsNullOrEmpty(e.CellValue.ToString()))
                {
                    e.Appearance.BackColor = Color.LightSkyBlue;
                    if ((string)e.CellValue == "-")
                    {
                        e.Appearance.ForeColor = Color.LightSkyBlue;
                    }
                }
            }
        }

        private void gv_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                e.Allow = false;
                popupPhieuBaoTri.ShowPopup(gc.PointToScreen(e.Point));
            }
        }

        public void Sua()
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
                    Id = int.Parse(gv.GetFocusedRowCellValue("ID").ToString()),
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

        public void Xoa()
        {
            var db = new MasterDataContext();
            try
            {
                var indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                    return;
                }

                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                foreach (var r in indexs)
                {
                    var o = db.tbl_KeHoachVanHanhs.FirstOrDefault(_ =>
                        _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                    if (o != null)
                    {
                        db.tbl_KeHoachVanHanh_ChiTiets.DeleteAllOnSubmit(
                            db.tbl_KeHoachVanHanh_ChiTiets.Where(_ => _.KeHoachVanHanhID == o.ID));
                        db.tbl_KeHoachVanHanh_LichSuDuyets.DeleteAllOnSubmit(
                            db.tbl_KeHoachVanHanh_LichSuDuyets.Where(_ => _.MaKeHoachVanHanh == o.ID));
                        db.tbl_KeHoachVanHanhs.DeleteOnSubmit(o);
                    }

                }

                db.SubmitChanges();
                LoadData();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void itemSuaPhieuBaoTri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Sua();
        }

        private void itemXoaPhieuBaoTri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Xoa();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = 0;
                if (gridView1.GetFocusedRowCellValue("ID") != null)
                {
                    id = (int)gridView1.GetFocusedRowCellValue("ID");
                }

                using (var frm = new frmKeHoachBaoTri_Edit { IsSua = 0, Id = id })
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

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Sua();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Xoa();
        }

        private void gv_Click(object sender, EventArgs e)
        {
            if (flag == false)
            {
                gv.BestFitColumns();

                flag = true;
            }
        }

        private void itemView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = gv.GetFocusedRowCellValue("ID")==null?(int?) null: int.Parse(gv.GetFocusedRowCellValue( "ID").ToString());
            var frm = new frmKeHoachBaoTri_Manager {Id = id};
            frm.ShowDialog();
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
                        IsKeHoachBaoTri=o1.IsKeHoachBaoTri,
                        TenKeHoach=o1.TenKeHoach,GhiChu=o1.GhiChu,
                        ChiPhiTheoKh=o1.ChiPhiTheoKh,
                        ChiPhiThucHien=o1.ChiPhiThucHien,
                        LoaiHeThong=o1.LoaiHeThong,
                        SoNgayCoTheTre=o1.SoNgayCoTheTre,
                        NgayHetHanCuoiCung=o1.NgayHetHanCuoiCung
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

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Import.frmKeHoachVanHanh_Import())
                {
                    if (Common.User.MaTN != null) frm.MaTn = (byte) Common.User.MaTN;
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

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void ItemDuyetTuDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var duyet = (bool?)gv.GetFocusedRowCellValue("IsDuyet");
            if (duyet == true)
            {
                DialogBox.Error("Kế hoạch đã được duyệt!");
                return;
            }

            var _db = new MasterDataContext();
            
            //var maTn = ((byte?)beiToaNha.EditValue ?? Common.User.MaTN);
            var id = int.Parse(gv.GetFocusedRowCellValue("ID").ToString());
            var objKhvh = _db.tbl_KeHoachVanHanhs.FirstOrDefault(o => o.ID == id);
            if (objKhvh == null)
            {
                DialogBox.Error("Kế hoạch không tồn tại!");
                return;
            }
            var tuNgay = objKhvh.TuNgay.GetValueOrDefault().Date;
            var denNgay = objKhvh.DenNgay.GetValueOrDefault().Date;
            var nts = objKhvh.NhomTaiSanID;
            var tanSuatId = objKhvh.TanSuatID;
            var profileId = objKhvh.ProfileID;
            var soNgayCoTheTre = objKhvh.SoNgayCoTheTre;

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
                objKhvh.IsDuyet = true;
                objKhvh.NgayDuyet = DateTime.Now;
                objKhvh.NguoiSua = Common.User.MaNV;
                objKhvh.NgaySua = DateTime.Now;
                var maTn = (byte?)objKhvh.MaTN;

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

            var idCas = objKhvh.PhanLoaiCaIDs.Split(',');
            if (idCas.Count() > 0)
            {
                int[] itemIdCas = idCas.Select(int.Parse).ToArray();
                foreach (var item in itemIdCas)
                {
                    var kyHieuCa = _db.tbl_PhanCong_PhanLoaiCas.First(_ => _.ID == item).KyHieu;
                    CreateAutoPhieuVanHanh(maTn, objChiTiet, _tungay, _denngay, soNgay, denNgay, nts, id, profileId,
                        item, kyHieuCa, soNgayCoTheTre);
                }
            }
            else
            {
                CreateAutoPhieuVanHanh(maTn, objChiTiet, _tungay, _denngay, soNgay, denNgay, nts, id, profileId, null,
                    "", soNgayCoTheTre);
            }

            _db.SubmitChanges();
            DialogBox.Success("Duyệt thành công");
        }

        private void CreateAutoPhieuVanHanh(byte? maTn, List<tbl_KeHoachVanHanh_ChiTiet> objChiTiet, DateTime tungay,
            DateTime denngay, int soNgay, DateTime denNgay, int? nts, int? id, int? profileId, int? idPhanLoaiCa,
            string kyHieuCa, decimal? soNgayCoTheTre)
        {
            var _db = new MasterDataContext();
            #region Create Phiếu vận hành auto

            if (objChiTiet.Count > 0)
            {

                foreach (var item in objChiTiet)
                {
                    var tempTuNgay = tungay;
                    CheckTime ck = GetAbnew(tempTuNgay, denNgay, (byte)maTn);

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

                        CheckTime timeNew = GetAbnew(tempTuNgay, (DateTime)vh.DenNgay, (byte)maTn);
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
                        if (soNgayCoTheTre != null) vh.NgayHetHanCuoiCung = vh.DenNgay.Value.AddDays((double)soNgayCoTheTre);
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
                        foreach (var itemts in objCtts)
                        {
                            var ts = new tbl_PhieuVanHanh_ChiTiet_TaiSan();
                            ts.MaTaiSanChiTietID = itemts.ID;
                            ts.TinhTrangTaiSanID = 0;
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
                ck = new CheckTime { NgayBd = a, NgayKt = b, Days = (b - a).Days + 1 };
            }

            // trường hợp 2
            // ab ngoài cd (A)--(c)--(d)--(B)
            // a<c, a <d, b>c, b>d
            if (a <= c & a <= d & b >= c & b >= d)
            {
                // lấy khoãng a  đến b++
                ck = new CheckTime { NgayBd = c, NgayKt = d, Days = (d - c).Days + 1 };
            }

            // trường hợp 3
            // b nằm trong khoãng cd (A)--(c)--(B)--(d)
            if (a <= c & a <= b & b >= c & b <= d)
            {
                ck = new CheckTime { NgayBd = c, NgayKt = b, Days = (b - c).Days + 1 };
            }

            // trường hợp 4
            // a nằm trong khoãng cd (c)--(A)--(d)--(B)
            if (a >= c & a <= d & b >= c & b >= d)
            {
                // thời gian làm = d=>b++
                ck = new CheckTime { NgayBd = a, NgayKt = d, Days = (d - a).Days + 1 };
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
            int count = (int)Math.Floor(ts.TotalDays / 7); // Number of whole weeks
            int remainder = (int)(ts.TotalDays % 7); // Number of remaining days
            int sinceLastDay = (int)(end.DayOfWeek - day); // Number of days since last [day]
            if (sinceLastDay < 0) sinceLastDay += 7; // Adjust for negative days since last [day]

            // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
            if (remainder >= sinceLastDay) count++;

            return count;
        }

        public CheckTime GetAbnew(DateTime a, DateTime b, byte maTn)
        {
            int ngayNghi = 0;
            DateTime e;
            var _db = new MasterDataContext();

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
                    ngayNghi += satDays;
                    b = b.AddDays(+satDays);
                    if (b.DayOfWeek == DayOfWeek.Saturday) b = b.AddDays(1);
                }

                if (firstTlnn.IsChuNhat == true)
                {
                    var sunDays = CountDays(DayOfWeek.Sunday, a, e);
                    ngayNghi += sunDays;
                    b = b.AddDays(+sunDays);
                    if (b.DayOfWeek == DayOfWeek.Sunday) b = b.AddDays(1);
                }
            }

            CheckTime ck = new CheckTime { NgayBd = a, NgayKt = b, Days = ngayNghi };
            return ck;
        }

        private void Gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(width, gv); }));
            }
        }

        private bool Cal(int width, GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

        public class CheckTime
        {
            public int Id { get; set; }
            public DateTime NgayBd { get; set; }
            public DateTime NgayKt { get; set; }
            public int Days { get; set; }
        }

        private void itemDieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.GetFocusedRowCellValue("ID") == null)
            {
                DialogBox.Error("Vui lòng chọn phiếu cần điều chỉnh, xin cảm ơn.");
                return;
            }

            var _db = new MasterDataContext();
            var id = int.Parse(gv.GetFocusedRowCellValue("ID").ToString());

            var obj = _db.tbl_KeHoachVanHanhs.FirstOrDefault(_ => _.ID == id);
            if (obj == null) return;

            #region Kiểm tra điều kiện điều chỉnh

            // Nếu phiếu này đã được duyệt, đã tạo phiếu rồi thì mới được điều chỉnh. Còn nếu chưa duyệt thì vui lòng qua duyệt giùm,
            // hoặc sửa phiếu các kiểu để duyệt, lúc đó cứ việc sửa các phiếu xong duyệt
            //
            var duyet = obj.IsDuyet;
            if (duyet != true)
            {
                DialogBox.Error("Vui lòng chọn duyệt kế hoạch!");
                return;
            }

            #endregion

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
                    var idCas = obj.PhanLoaiCaIDs.Split(',');

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
                                obj.NhomTaiSanID, id, obj.ProfileID, item, kyHieuCa, obj.SoNgayCoTheTre);
                        }
                    }
                    else
                    {
                        CreateAutoPhieuVanHanh((byte?)obj.MaTN, obj.tbl_KeHoachVanHanh_ChiTiets.ToList(),
                            frm.TuNgayMoi, frm.DenNgayMoi.AddDays(time.Days), int.Parse(obj.tbl_TanSuat.SoNgay.ToString()),
                            frm.DenNgayMoi,
                            obj.NhomTaiSanID, id, obj.ProfileID, null, "", obj.SoNgayCoTheTre);
                    }

                    _db.SubmitChanges();
                }
            }

            _db.Dispose();
        }
    }
}