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
    public partial class frmKiemTraDinhKy : XtraForm
    {
        //private MasterDataContext _db=new MasterDataContext();
        private DataTable _data;
        private DateTime _denNgay;
        private bool flag = false;
        private List<PhieuVanHanhItem> phieuVanHanh;
        private List<DataItem> nts, tts, lts;
        private List<BackgroundWorker> _ltThread = new List<BackgroundWorker>();
        private bool _isStopThread = false;
        private int nam;

        #region Class

        public class PhieuVanHanhItem
        {
            public int? ID { get; set; }
            public int? NhomTaiSanID { get; set; }
            public int? LoaiTaiSanID { get; set; }
            public int? TenTaiSanID { get; set; }
            public int? LoaiHeThong { get; set; }

            public string Profile { get; set; }
            public string TanSuat { get; set; }

            public short? MaTN { get; set; }

            public decimal? ChiPhi { get; set; }

            public DateTime TuNgay { get; set; }
            public DateTime DenNgay { get; set; }
        }

        public class DataItem
        {
            public string TenNhomTaiSan { get; set; }
            public string LoaiTaiSan { get; set; }
            public string TenTaiSan { get; set; }
            public string Profile { get; set; }
            public string TanSuat { get; set; }
            public DateTime TuNgay { get; set; }
            public DateTime DenNgay { get; set; }
            public decimal? ChiPhi { get; set; }
            public int? ID { get; set; }
            public int? SoNgayThucHien { get; set; }
        }

        #endregion

        public frmKiemTraDinhKy()
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
                        nts = (from p in phieuVanHanh
                            join n in db.tbl_NhomTaiSans on p.NhomTaiSanID equals n.ID
                            where p.LoaiHeThong == 1
                            select new DataItem
                            {
                                TenNhomTaiSan = n.TenNhomTaiSan,
                                LoaiTaiSan = "Tất cả mọi loại tài sản",
                                TenTaiSan = "Tất cả tên tài sản",
                                Profile = p.Profile,
                                TuNgay = p.TuNgay,
                                DenNgay = p.DenNgay,
                                TanSuat = p.TanSuat,
                                SoNgayThucHien = (p.DenNgay - p.TuNgay).Days + 1,
                                ChiPhi = p.ChiPhi,
                                ID = p.ID
                            }).ToList();
                        break;
                    case 2:
                        // danh sách loại tài sản
                        lts = (from p in phieuVanHanh
                            join l in db.tbl_LoaiTaiSans on p.LoaiTaiSanID equals l.ID
                            where p.LoaiHeThong == 2
                            select new DataItem
                            {
                                TenNhomTaiSan = l.tbl_NhomTaiSan.TenNhomTaiSan,
                                LoaiTaiSan = l.TenLoaiTaiSan,
                                TenTaiSan = "Tất cả tên tài sản",
                                Profile = p.Profile,
                                TuNgay = p.TuNgay,
                                DenNgay = p.DenNgay,
                                TanSuat = p.TanSuat,
                                SoNgayThucHien = (p.DenNgay - p.TuNgay).Days + 1,
                                ChiPhi = p.ChiPhi,
                                ID = p.ID
                            }).ToList();
                        break;
                    case 3:
                        // danh sách tên tài sản
                        tts = (from p in phieuVanHanh
                            join t in db.tbl_TenTaiSans on p.TenTaiSanID equals t.ID
                            where p.LoaiHeThong == 3
                            select new DataItem
                            {
                                TenNhomTaiSan = t.tbl_LoaiTaiSan.tbl_NhomTaiSan.TenNhomTaiSan,
                                LoaiTaiSan = t.tbl_LoaiTaiSan.TenLoaiTaiSan,
                                TenTaiSan = t.TenTaiSan,
                                Profile = p.Profile,
                                TuNgay = p.TuNgay,
                                DenNgay = p.DenNgay,
                                TanSuat = p.TanSuat,
                                SoNgayThucHien = (p.DenNgay - p.TuNgay).Days + 1,
                                ChiPhi = p.ChiPhi,
                                ID = p.ID
                            }).ToList();
                        break;
                    default:
                        _isStopThread = true;
                        break;
                }
            }
        }

        public void GetData()
        {
            if (nts == null) nts = new List<DataItem>();
            if (lts == null) lts = new List<DataItem>();
            if (tts == null) tts = new List<DataItem>();
            // danh sách tổng
            var objKiemTraDinhKy = nts.Concat(lts).Concat(tts);
            #region Đổ cột cố định

            _data.Columns.Add("TenNhomTaiSan");
            _data.Columns.Add("LoaiTaiSan");
            _data.Columns.Add("TenTaiSan");
            _data.Columns.Add("Profile");
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

            //gv.Columns.Add(colNhomTaiSan);
            gv.Columns.AddField("Loại tài sản");
            var colLoaiTaiSan = new BandedGridColumn();
            colLoaiTaiSan.Name = "colLoaiTaiSan";
            colLoaiTaiSan.Caption = @"Loại tài sản";
            colLoaiTaiSan.FieldName = "LoaiTaiSan";
            colLoaiTaiSan.Visible = false;
            colLoaiTaiSan.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colLoaiTaiSan.OwnerBand = band;
            //colLoaiTaiSan.VisibleIndex = 1;
            //gv.Columns.Add(colLoaiTaiSan);

            gv.Columns.AddField("Tên tài sản");
            var colTenTaiSan = new BandedGridColumn();
            colTenTaiSan.Name = "colTenTaiSan";
            colTenTaiSan.Caption = @"Tên tài sản";
            colTenTaiSan.FieldName = "TenTaiSan";
            colTenTaiSan.Visible = false;
            colTenTaiSan.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colTenTaiSan.OwnerBand = band;
            colTenTaiSan.VisibleIndex = 0;
            //gv.Columns.Add(colTenTaiSan);

            gv.Columns.AddField("Profile");
            var colProfile = new BandedGridColumn();
            colProfile.Name = "colProfile";
            colProfile.Caption = @"Profile";
            colProfile.FieldName = "Profile";
            colProfile.Visible = true;
            colProfile.VisibleIndex = 1;
            colTenTaiSan.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colProfile.OwnerBand = band;
            //gv.Columns.Add(colProfile);

            gv.Columns.AddField("Từ ngày");
            var colTuNgay = new BandedGridColumn();
            colTuNgay.Name = "colTuNgay";
            colTuNgay.Caption = @"Từ ngày";
            colTuNgay.FieldName = "TuNgay";
            colTuNgay.Visible = true;
            colTuNgay.VisibleIndex = 2;
            colTuNgay.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colTuNgay.OwnerBand = band;
            //gv.Columns.Add(colTuNgay);

            gv.Columns.AddField("Đến ngày");
            var colDenNgay = new BandedGridColumn();
            colDenNgay.Name = "colDenNgay";
            colDenNgay.Caption = @"Đến ngày";
            colDenNgay.FieldName = "DenNgay";
            colDenNgay.Visible = true;
            colDenNgay.VisibleIndex = 3;
            colDenNgay.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colDenNgay.OwnerBand = band;
            //gv.Columns.Add(colDenNgay);

            gv.Columns.AddField("Tần suất");
            var colTanSuat = new BandedGridColumn();
            colTanSuat.Name = "colTanSuat";
            colTanSuat.Caption = @"Tần suất";
            colTanSuat.FieldName = "TanSuat";
            colTanSuat.Visible = true;
            colTanSuat.VisibleIndex = 4;
            colTanSuat.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colTanSuat.OwnerBand = band;
            //gv.Columns.Add(colTanSuat);

            gv.Columns.AddField("Số ngày thực hiện");
            var colSoNgayThucHien = new BandedGridColumn();
            colSoNgayThucHien.Name = "colSoNgayThucHien";
            colSoNgayThucHien.Caption = @"Số ngày thực hiện";
            colSoNgayThucHien.FieldName = "SoNgayThucHien";
            colSoNgayThucHien.Visible = true;
            colSoNgayThucHien.VisibleIndex = 5;
            colSoNgayThucHien.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colSoNgayThucHien.OwnerBand = band;

            gv.Columns.AddField("Chi phí");
            var colChiPhi = new BandedGridColumn();
            colChiPhi.Name = "colChiPhi";
            colChiPhi.Caption = @"Chi phí";
            colChiPhi.FieldName = "ChiPhi";
            colChiPhi.Visible = true;
            colChiPhi.VisibleIndex = 6;
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
                bandThang.Caption = @"NĂM " + nam;
                bandThang.Name = "Nam" + nam;
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
                    r["LoaiTaiSan"] = row.LoaiTaiSan;
                    r["TenTaiSan"] = row.TenTaiSan;
                    r["Profile"] = row.Profile;
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

                        // danh sách tuần của năm
                        var ngayDauThang = new DateTime(nam, item, 1); // ngày đầu tháng
                        var ngayCuoiThang = ngayDauThang.AddMonths(1).AddDays(-1);
                        // ngày đầu tiên của tuần
                        while (ngayDauThang < ngayCuoiThang)
                        {
                            // tìm tuần của từ ngày
                            var cal = CultureInfo.CurrentCulture.Calendar;
                            //var tuan = cal.GetWeekOfYear(ngayDauThang, CalendarWeekRule.FirstFourDayWeek,DayOfWeek.Monday);
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

                            if (listTuan.All(_ => _ != tuan))
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

                        bandTuan.Visible = true;

                    }

                    #endregion

                    #region Đổ dữ liệu

                    // đổ dữ liệu
                    foreach (var row in objKiemTraDinhKy)
                    {
                        var r = _data.NewRow();
                        r["TenNhomTaiSan"] = row.TenNhomTaiSan;
                        r["LoaiTaiSan"] = row.LoaiTaiSan;
                        r["TenTaiSan"] = row.TenTaiSan;
                        r["Profile"] = row.Profile;
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
                            //var tuan = cal.GetWeekOfYear(ngayDau, CalendarWeekRule.FirstFourDayWeek,
                            //    DayOfWeek.Monday);
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

            gv.GroupCount = 2;
            gv.GroupFooterShowMode = GroupFooterShowMode.Hidden;
            gv.SortInfo.AddRange(new[]
                    {
                        new GridColumnSortInfo(colNhomTaiSan, DevExpress.Data.ColumnSortOrder.Ascending),
                        new GridColumnSortInfo(colLoaiTaiSan, DevExpress.Data.ColumnSortOrder.Ascending)
                    });
            gv.OptionsBehavior.Editable = false;

            gc.DataSource = _data;
            flag = false;
            itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
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
                    nam = int.Parse(itemNam.EditValue.ToString());
                    var db = new MasterDataContext();

                    // 1. từ ngày đến ngày nằm trong năm
                    // value year == year
                    // từ ngày đến ngày, có đến ngày hoặc từ ngày nằm ngoài năm, thì từ ngày hoặc đến ngày = 1/1 hoặc 31/12
                    // đổ dữ liệu từ ngày đến ngày mới cho các phiếu
                    phieuVanHanh = (from p in db.tbl_PhieuVanHanhs
                        join kh in db.tbl_KeHoachVanHanhs on p.KeHoachVanHanhID equals kh.ID
                        join ct in db.tbl_KeHoachVanHanh_ChiTiets on kh.ID equals ct.KeHoachVanHanhID
                        join pr in db.tbl_Profiles on ct.ProfileID equals pr.ID into profile
                        from pr in profile.DefaultIfEmpty()
                        where ltToaNha.Contains(p.MaTN.ToString()) & p.TuNgay != null & p.DenNgay != null &
                              p.IsPhieuBaoTri == true & (p.TuNgay.Value.Year == nam || p.DenNgay.Value.Year == nam)
                        select new PhieuVanHanhItem
                        {
                            ID=p.ID,
                            NhomTaiSanID=p.NhomTaiSanID,
                            LoaiTaiSanID=p.LoaiTaiSanID,
                            TenTaiSanID=p.TenTaiSanID,
                            Profile = ct.ProfileID != null ? pr.TenMau : "",
                            // năm kt = 2019, từ ngày = 1/12/2018, đến ngày: 30/1/2019 => dữ liệu từ: 1/1/2019=>30/1/2019
                            TuNgay = p.TuNgay.Value.Year == nam
                                ? p.TuNgay.Value.Date
                                : p.DenNgay.Value.Year == nam
                                    ? new DateTime(p.DenNgay.Value.Year, 1, 1)
                                    : p.DenNgay.Value.Year > nam
                                        ? new DateTime(nam, 1, 1)
                                        : p.TuNgay.Value.Date,
                            // năm kt = 2019, từ ngày = 1/12/2019, đến ngày 30/1/2020 => dữ liệu từ 1/12/2019 => 31/12/2019
                            DenNgay = p.DenNgay.Value.Year == nam
                                ? p.DenNgay.Value.Date
                                : p.TuNgay.Value.Year == nam
                                    ? new DateTime(p.TuNgay.Value.Year, 12, 31)
                                    : p.TuNgay.Value.Year > nam
                                        ? new DateTime(nam, 12, 31)
                                        : p.DenNgay.Value.Date,
                            MaTN=p.MaTN,
                            LoaiHeThong=p.LoaiHeThong,
                            TanSuat = kh.tbl_TanSuat.TenTanSuat,
                            ChiPhi = kh.ChiPhiThucHien
                        }).ToList();

                    int ti = 1;

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
            for (int i = 0; i < 3; i++)
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
            int ht = (int)e.Argument;
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
                    if (e.CellValue == "-")
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
                    if (e.CellValue == "-")
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

                var db = new MasterDataContext();
                var id = int.Parse(gv.GetFocusedRowCellValue("ID").ToString());
                var objPhieuBaoTri = db.tbl_PhieuVanHanhs.FirstOrDefault(_ => _.ID == id);
                if (objPhieuBaoTri != null)
                {
                    using (var frm = new frmPhieuBaoTri_Edit
                    {
                        MaTn = (byte?)objPhieuBaoTri.MaTN,
                        Id = objPhieuBaoTri.ID,
                        IsSua = 1
                    })
                    {
                        frm.ShowDialog();
                        if (frm.DialogResult == DialogResult.OK) LoadData();
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public void Xoa()
        {
            var _db = new MasterDataContext();
            try
            {
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
                using (var frm = new frmPhieuBaoTri_Edit
                {
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

        private void itemXLTiepNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _db = new MasterDataContext();
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
                var id = int.Parse(gv.GetRowCellValue(i, "ID").ToString());

                var obj = _db.tbl_PhieuVanHanhs.FirstOrDefault(_ => _.ID == id);
                if (obj != null)
                {
                    var idTrangThaiPhieu = obj.TrangThaiPhieu ?? 0;
                    if (idTrangThaiPhieu == 2)
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
                var id = int.Parse(gv.GetRowCellValue(i, "ID").ToString());
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

            //LoadData();
        }

        private void itemView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = gv.GetFocusedRowCellValue("ID")==null?(int?) null: int.Parse(gv.GetFocusedRowCellValue( "ID").ToString());
            var frm = new frmPhieuBaoTri_Manager {Id = id};
            frm.ShowDialog();
        }

        private void itemXLHoanThanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _db = new MasterDataContext();
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
                var id = int.Parse(gv.GetRowCellValue(i, "ID").ToString());

                var obj = _db.tbl_PhieuVanHanhs.FirstOrDefault(_ => _.ID == id);
                if (obj != null)
                {
                    var idTrangThaiPhieu = obj.TrangThaiPhieu ?? 0;
                    if (idTrangThaiPhieu == 2)
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
                var id = int.Parse(gv.GetRowCellValue(i, "ID").ToString());
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

            //LoadData();
        }

        private void itemXLDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();

            var id = int.Parse(gv.GetFocusedRowCellValue("ID").ToString());
            if (id == 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu");
                return;
            }

            var obj = db.tbl_PhieuVanHanhs.FirstOrDefault(_ => _.ID == id);
            if (obj == null) return;

            if (obj.IsDuyet == true)
            {
                DialogBox.Error("Phiếu đã được người cuối duyệt!");
                return;
            }

            //Check phiếu
            var trangThaiPhieu = true;

            var idTrangThai = obj.TrangThaiPhieu ?? 0;
            if (idTrangThai != 2)
            {
                trangThaiPhieu = false;
            }

            if (trangThaiPhieu == false)
            {
                DialogBox.Alert("Vui lòng chọn phiếu đã hoàn thành để duyệt");
                return;
            }

            DateTime dtNgayDuyet;
            var ghiChuDuyet = "";

            #region Kiểm tra duyệt //HeThongTaiSanID

            var ktCv = db.tbl_FromDuyet_ChucVus.Where(_ =>
                _.FormDuyetID == 7 & _.HeThongTaiSanID == obj.HeThongTaiSanID & _.ChucVuID == Common.User.MaCV);
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
                        var ktLs = db.tbl_PhieuVanHanh_LichSus.FirstOrDefault(_ =>
                            _.PhieuVanHanhID == id & _.NguoiTao == Common.User.MaNV);
                        if (ktLs != null)
                        {
                            DialogBox.Error("Phiếu này bạn đã duyệt rồi");
                            return;
                        }

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
                        db.tbl_PhieuVanHanh_LichSus.InsertOnSubmit(o);

                        if (ktNvCt.IsDuyet != true)
                        {
                            db.SubmitChanges();
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

            var objPvh = db.tbl_PhieuVanHanhs.FirstOrDefault(p => p.ID == id);
            if (objPvh != null)
            {
                objPvh.NguoiDuyet = Common.User.MaNV;
                objPvh.NgayDuyet = dtNgayDuyet;
                objPvh.GhiChuDuyet = ghiChuDuyet;
                objPvh.IsDuyet = true;
                objPvh.TrangThaiPhieu = 3;
            }

            db.SubmitChanges();

            db.Dispose();
            //LoadData();
        }

        private void itemXLHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
            var id = int.Parse(gv.GetFocusedRowCellValue("ID").ToString());
            if (id == 0)
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

            var ktCv = db.tbl_FromDuyet_ChucVus.Where(_ =>
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
                        db.tbl_PhieuVanHanh_LichSus.InsertOnSubmit(o);

                        if (ktNvCt.IsDuyet != true)
                        {
                            db.SubmitChanges();
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

            var objPvh = db.tbl_PhieuVanHanhs.FirstOrDefault(p => p.ID == id);
            if (objPvh != null)
            {
                objPvh.NguoiDuyet = Common.User.MaNV;
                objPvh.NgayDuyet = dtNgayDuyet;
                objPvh.GhiChuDuyet = ghiChuDuyet;
                objPvh.IsDuyet = false;
                objPvh.TrangThaiPhieu = 4;

                // delete all row lichsu
                db.tbl_PhieuVanHanh_LichSus.DeleteAllOnSubmit(
                    db.tbl_PhieuVanHanh_LichSus.Where(_ => _.PhieuVanHanhID == objPvh.ID));
            }

            db.SubmitChanges();
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

        private void itemXLThucHienLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gv.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu");
                return;
            }

            var _db = new MasterDataContext();

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
                var id = int.Parse(gv.GetRowCellValue(i, "ID").ToString());
                if (id != null)
                {
                    var objPvh = _db.tbl_PhieuVanHanhs.FirstOrDefault(p => p.ID == id);
                    if (objPvh != null)
                    {
                        objPvh.NguoiDuyet = Common.User.MaNV;
                        objPvh.NgayDuyet = dtNgayDuyet;
                        objPvh.GhiChuDuyet = GhiChuDuyet;
                        objPvh.TrangThaiPhieu = 0;
                        objPvh.IsDuyet = false;

                        // delete all row lichsu
                        _db.tbl_PhieuVanHanh_LichSus.DeleteAllOnSubmit(
                            _db.tbl_PhieuVanHanh_LichSus.Where(_ => _.PhieuVanHanhID == objPvh.ID));
                    }

                    _db.SubmitChanges();
                }
            }
        }

        private void btnDieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = int.Parse(gv.GetFocusedRowCellValue("ID").ToString());
                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần điều chỉnh, xin cảm ơn.");
                    return;
                }

                using (var frm = new frmKeHoachBaoTri_ChinhNgay
                {
                    Id = id
                })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
            catch
            {
                //
            }
        }
    }
}