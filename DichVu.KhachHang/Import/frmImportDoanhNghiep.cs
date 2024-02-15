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

namespace KyThuat.KhachHang.Import
{
    public partial class frmImportDoanhNghiep : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public byte? MaTN;
        public bool IsUpdate = false;

        public frmImportDoanhNghiep()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void btnChonTapTin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "(Excel file)|*.xls;*.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    var book = new LinqToExcel.ExcelQueryFactory(f.FileName);
                    var item = book.Worksheet(0).Select(p => new
                    {
                        KyHieu = p["Mã KH"].ToString().Trim(),
                        MaPhu = p["Mã phụ"].ToString().Trim(),
                        TenVT = p["Tên viết tắt"].ToString().Trim(),
                        TenCty = p["Tên công ty"].ToString().Trim(),
                        DiaChiCty = p["Địa chỉ"].ToString().Trim(),
                        DienThoai = p["Điện thoại"].ToString().Trim(),
                        //Email=p[7].ToString().Trim(),
                        Fax = p["Fax"].ToString().Trim(),
                        NguoiDaiDien = p["Người đại diện"].ToString().Trim(),
                        ChucVu = p["Chức vụ"].ToString().Trim(),
                        MaSoThue = p["Mã số thuế"].ToString().Trim(),
                        //SoDKKD = p["Số ĐKKD"].ToString().Trim(),
                        NgayDKKD = MyConvert(p["Ngày ĐKKD"]),
                        NoiDKKD = p["Nơi ĐKKD"].ToString().Trim(),
                        SoTaiKhoan = p["Số tài khoản"].ToString().Trim(),
                        NganHang = p["Ngân hàng"].ToString().Trim(),
                        KhuVuc = SearchKV(p["Khu vực"].ToString().Trim()),
                        NhomKH = p["Nhóm KH"].ToString().Trim(),
                        NguoiLH = p["Người liên hệ"].ToString().Trim(),
                        DiaChiNhanThu = p["Địa chỉ nhận thư"].ToString().Trim(),
                        QuocTich = p["Quốc tịch"].ToString().Trim(),
                        Website = p["Website"].ToString().Trim(),
                        NganhNgheDoanhNghiep = p["Ngành nghề"].ToString().Trim(),
                        EmailKhachThue = p["Email khách thuê"].ToString().Trim(),
                        DiaPhan = p["Địa phận"].ToString().Trim()
                        //SDTNguoiLH = p[19].ToString().Trim(),
                    });

                    List<ImportItemDN> newlist = new List<ImportItemDN>();
                    foreach (var it in item)
                    {
                        ImportItemDN importitem = new ImportItemDN()
                        {
                            KyHieu = it.KyHieu,
                            MaPhu = it.MaPhu,
                            TenVT = it.TenVT,
                            TenCty = it.TenCty,
                            DiaChiCty = it.DiaChiCty,
                            DienThoai = it.DienThoai,
                            //Email=it.Email,
                            Fax = it.Fax,
                            NguoiDaiDien = it.NguoiDaiDien,
                            ChucVu = it.ChucVu,
                            MaSoThue = it.MaSoThue,
                            //SoDKKD = it.SoDKKD,
                            NgayDKKD = it.NgayDKKD,
                            NoiDKKD = it.NoiDKKD,
                            SoTaiKhoan = it.SoTaiKhoan,
                            NganHang = it.NganHang,
                            KhuVuc = it.KhuVuc,
                            NhomKH = it.NhomKH,
                            TenNLH = it.NguoiLH,
                            DiaChiNhanThu = it.DiaChiNhanThu,
                            QuocTich = it.QuocTich,
                            Website = it.Website,
                            NganhNgheDoanhNghiep = it.NganhNgheDoanhNghiep,
                            EmailKhachThue = it.EmailKhachThue,
                            DiaPhan = it.DiaPhan
                            //SDTNLH = it.SDTNguoiLH,
                        };
                        newlist.Add(importitem);
                    }
                    gcDoanhNghiep.DataSource = newlist;
                }
                catch (Exception ex)
                {
                    DialogBox.Alert("Mẫu excel dùng để import dữ liệu không đúng định dạng, vui lòng xem lại.\r\nCode: " + ex.Message);
                }

                wait.Close();
                wait.Dispose();
            }
        }

        private DateTime? MyConvert(LinqToExcel.Cell value)
        {
            try
            {
                //return value.Cast<DateTime>(); 
                return DateTime.FromOADate(Convert.ToInt64(value));
            }
            catch
            {
                return null;
            }
        }

        private int? SearchKV(string chuoi)
        {
            try
            {
                return db.tnKhuVucs.FirstOrDefault(p => SqlMethods.Like(p.TenKV, "%" + chuoi + "%")).MaKV;
            }
            catch
            {
                return null;
            }
        }

        private void LoadData()
        {
            lookKhuVuc.DataSource = db.tnKhuVucs;
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            LoadData();

            if (IsUpdate)
                this.Text = "Cập nhật thông tin (Doanh nghiệp)";
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDoanhNghiep.DataSource == null)
            {
                DialogBox.Alert("Không có dữ liệu lưu");
                return;
            }

            if (IsUpdate)
                Update();
            else
                Insert();
        }

        void Insert()
        {
            List<tnKhachHang> listMB = new List<tnKhachHang>();
            var ltError = new List<ImportItemDN>();
            var ltSource = (List<ImportItemDN>)gcDoanhNghiep.DataSource;
            for (int i = 0; i < grvDoanhNghiep.RowCount; i++)
            {
                var maSoThue = grvDoanhNghiep.GetRowCellValue(i, colMST).ToString();
                var objKH = db.tnKhachHangs.FirstOrDefault(p => p.KyHieu == grvDoanhNghiep.GetRowCellValue(i, colKyHieu).ToString() & p.MaTN == MaTN);
                var objKHMaSoThue = db.tnKhachHangs.FirstOrDefault(p => p.CtyMaSoThue == maSoThue & p.MaTN == MaTN);
                if (objKH == null)
                {
                    if (objKHMaSoThue != null)
                    {
                        grvDoanhNghiep.SetRowCellValue(i, colError, "Mã số thuế: " + maSoThue + " đã tồn tại");
                        ltError.Add(ltSource[i]);
                        continue;
                    }
                    tnKhachHang obj = new tnKhachHang();

                    var objNhomKH = db.khNhomKhachHangs.FirstOrDefault(p => p.TenNKH == grvDoanhNghiep.GetRowCellValue(i, colNhomKH).ToString() & p.MaTN == MaTN);

                    obj.KyHieu = grvDoanhNghiep.GetRowCellValue(i, colKyHieu).ToString();
                    obj.MaPhu = grvDoanhNghiep.GetRowCellValue(i, colMaPhu).ToString();
                    obj.CtyTenVT = grvDoanhNghiep.GetRowCellValue(i, colTenVT).ToString();
                    obj.CtyTen = grvDoanhNghiep.GetRowCellValue(i, colTenCty).ToString();
                    obj.CtyDiaChi = grvDoanhNghiep.GetRowCellValue(i, colDiaChi).ToString();
                    obj.DCLL = grvDoanhNghiep.GetRowCellValue(i, colDiaChi).ToString();
                    //obj.CtyDienThoai = grvDoanhNghiep.GetRowCellValue(i, colDienThoai).ToString();
                    obj.SDTNguoiLH = grvDoanhNghiep.GetRowCellValue(i, colDienThoai).ToString();
                    obj.DienThoaiKH = grvDoanhNghiep.GetRowCellValue(i, colDienThoai).ToString();
                    obj.NguoiLH = grvDoanhNghiep.GetRowCellValue(i, NguoiLH).ToString();
                    // obj.EmailKH = grvDoanhNghiep.GetRowCellValue(i, gridColumn1).ToString();
                    obj.CtyMaSoThue = grvDoanhNghiep.GetRowCellValue(i, colMST).ToString();
                    obj.CtyChucVuDD = grvDoanhNghiep.GetRowCellValue(i, ChucVu).ToString();
                    obj.CtyFax = grvDoanhNghiep.GetRowCellValue(i, colFax).ToString();
                    obj.CtyNguoiDD = grvDoanhNghiep.GetRowCellValue(i, NguoiDaiDien).ToString();
                    if (grvDoanhNghiep.GetRowCellValue(i, colNgayDKKD) != null) obj.CtyNgayDKKD = grvDoanhNghiep.GetRowCellValue(i, colNgayDKKD).ToString();
                    if (grvDoanhNghiep.GetRowCellValue(i, colNoiDKKD) != null) obj.CtyNoiDKKD = grvDoanhNghiep.GetRowCellValue(i, colNoiDKKD).ToString();
                    obj.IsCaNhan = false;
                    if (grvDoanhNghiep.GetRowCellValue(i, colSoTaiKhoan) != null) obj.CtySoTKNH = grvDoanhNghiep.GetRowCellValue(i, colSoTaiKhoan).ToString();
                    if (grvDoanhNghiep.GetRowCellValue(i, colKhuVuc) != null) obj.MaKV = (int)grvDoanhNghiep.GetRowCellValue(i, colKhuVuc);
                    obj.MaNV = objnhanvien.MaNV;
                    obj.MaTN = MaTN;
                    if (objNhomKH != null)
                    {
                        obj.MaNKH = objNhomKH.ID;
                    }
                    else
                        obj.MaNKH = null;
                    obj.DiaChiNhanThu = grvDoanhNghiep.GetRowCellValue(i, colDiaChiNhanThu).ToString();
                    obj.QuocTich = grvDoanhNghiep.GetRowCellValue(i, colQuocTich).ToString();
                    obj.Website = grvDoanhNghiep.GetRowCellValue(i, colWebsite).ToString();
                    obj.EmailKhachThue = grvDoanhNghiep.GetRowCellValue(i, gridColumn2).ToString();
                    obj.DiaPhan = grvDoanhNghiep.GetRowCellValue(i, gridColumn3).ToString();
                    obj.NganhNgheDoanhNghiep = grvDoanhNghiep.GetRowCellValue(i, colNganhNghe).ToString();

                    //listMB.Add(obj);
                    db.tnKhachHangs.InsertOnSubmit(obj);
                    db.SubmitChanges();

                    var kh = db.tnKhachHangs.OrderByDescending(p => p.MaKH).FirstOrDefault();
                    var chuoi = grvDoanhNghiep.GetRowCellValue(i, colNganhNghe).ToString();
                    if (chuoi != null)
                    {
                        var string1 = chuoi.Split(',').ToList();
                        foreach (var z in string1)
                        {
                            var xuly = z.Trim().ToString();
                            var check = db.NgheNghieps.SingleOrDefault(p => p.MaNN.ToString() == xuly);
                            if (check != null)
                            {
                                var job = new NgheNghiepKH();
                                job.MaNN = check.MaNN;
                                job.MaKH = kh.MaKH;
                                db.NgheNghiepKHs.InsertOnSubmit(job);
                                db.SubmitChanges();
                            }
                        }
                    }
                }
            }

            var wait = DialogBox.WaitingForm();

            //db.tnKhachHangs.InsertAllOnSubmit(listMB);
            //db.SubmitChanges();

            wait.Close();
            wait.Dispose();

            DialogBox.Alert("Đã lưu");
            if (ltError.Count > 0)
            {
                gcDoanhNghiep.DataSource = ltError;
            }
            else
            {
                gcDoanhNghiep.DataSource = null;
                this.Close();
            }
        }

        void Update()
        {
            var ltError = new List<ImportItemDN>();
            var ltSource = (List<ImportItemDN>)gcDoanhNghiep.DataSource;
            for (int i = 0; i < grvDoanhNghiep.RowCount; i++)
            {
                var maSoThue = grvDoanhNghiep.GetRowCellValue(i, colMST).ToString();
                var obj = db.tnKhachHangs.SingleOrDefault(p => p.KyHieu == grvDoanhNghiep.GetRowCellValue(i, colKyHieu).ToString());
                if (obj != null)
                {
                    var objKHMaSoThe = db.tnKhachHangs.FirstOrDefault(p => p.CtyMaSoThue == maSoThue & p.MaTN == MaTN & p.MaKH != obj.MaKH);
                    if (objKHMaSoThe != null)
                    {
                        grvDoanhNghiep.SetRowCellValue(i, colError, "Mã số thuế: " + maSoThue + " đã tồn tại");
                        ltError.Add(ltSource[i]);
                        continue;
                    }
                    var objNhomKH = db.khNhomKhachHangs.FirstOrDefault(p => p.TenNKH == grvDoanhNghiep.GetRowCellValue(i, colNhomKH).ToString() & p.MaTN == MaTN);

                    obj.KyHieu = grvDoanhNghiep.GetRowCellValue(i, colKyHieu).ToString();
                    obj.MaPhu = grvDoanhNghiep.GetRowCellValue(i, colMaPhu).ToString();
                    obj.CtyTenVT = grvDoanhNghiep.GetRowCellValue(i, colTenVT).ToString();
                    obj.CtyTen = grvDoanhNghiep.GetRowCellValue(i, colTenCty).ToString();
                    obj.CtyDiaChi = grvDoanhNghiep.GetRowCellValue(i, colDiaChi).ToString();
                    obj.DCLL = grvDoanhNghiep.GetRowCellValue(i, colDiaChi).ToString();
                    //obj.CtyDienThoai = grvDoanhNghiep.GetRowCellValue(i, colDienThoai).ToString();
                    obj.EmailKH = grvDoanhNghiep.GetRowCellValue(i, gridColumn1) != null ? grvDoanhNghiep.GetRowCellValue(i, gridColumn1).ToString() : "";
                    obj.CtyMaSoThue = grvDoanhNghiep.GetRowCellValue(i, colMST).ToString();
                    obj.CtyChucVuDD = grvDoanhNghiep.GetRowCellValue(i, ChucVu).ToString();
                    obj.CtyFax = grvDoanhNghiep.GetRowCellValue(i, colFax).ToString();
          
                    obj.SDTNguoiLH = grvDoanhNghiep.GetRowCellValue(i, colDienThoai).ToString();
                    obj.DienThoaiKH = grvDoanhNghiep.GetRowCellValue(i, colDienThoai).ToString();
                    obj.NguoiLH = grvDoanhNghiep.GetRowCellValue(i, NguoiLH).ToString();
                    obj.CtyNguoiDD = grvDoanhNghiep.GetRowCellValue(i, NguoiDaiDien).ToString();
                    if (grvDoanhNghiep.GetRowCellValue(i, colNgayDKKD) != null) obj.CtyNgayDKKD = grvDoanhNghiep.GetRowCellValue(i, colNgayDKKD).ToString();
                    if (grvDoanhNghiep.GetRowCellValue(i, colNoiDKKD) != null) obj.CtyNoiDKKD = grvDoanhNghiep.GetRowCellValue(i, colNoiDKKD).ToString();
                    obj.IsCaNhan = false;
                    if (grvDoanhNghiep.GetRowCellValue(i, colSoTaiKhoan) != null)
                    {
                        obj.CtySoTKNH = grvDoanhNghiep.GetRowCellValue(i, colSoTaiKhoan).ToString();
                        obj.CtyTenNH = grvDoanhNghiep.GetRowCellValue(i, colNganHang).ToString();
                    }
                    if (grvDoanhNghiep.GetRowCellValue(i, colKhuVuc) != null) obj.MaKV = (int)grvDoanhNghiep.GetRowCellValue(i, colKhuVuc);
                    obj.MaNV = objnhanvien.MaNV;
                    obj.MaTN = MaTN;

                    obj.DiaChiNhanThu = grvDoanhNghiep.GetRowCellValue(i, colDiaChiNhanThu).ToString();
                    obj.QuocTich = grvDoanhNghiep.GetRowCellValue(i, colQuocTich).ToString();
                    obj.Website = grvDoanhNghiep.GetRowCellValue(i, colWebsite).ToString();
                    obj.EmailKhachThue = grvDoanhNghiep.GetRowCellValue(i, gridColumn2).ToString();
                    obj.DiaPhan = grvDoanhNghiep.GetRowCellValue(i, gridColumn3).ToString();
                    obj.NganhNgheDoanhNghiep = grvDoanhNghiep.GetRowCellValue(i, colNganhNghe).ToString();

                    if (objNhomKH != null)
                    {
                        obj.MaNKH = objNhomKH.ID;
                    }
                    else
                        obj.MaNKH = null;

                    try
                    {
                        db.SubmitChanges();
                        var xoa = db.NgheNghiepKHs.Where(p => p.MaKH == obj.MaKH).ToList();
                        if (xoa.Count() > 0)
                        {
                            db.NgheNghiepKHs.DeleteAllOnSubmit(xoa);
                            db.SubmitChanges();
                        }
                        var chuoi = grvDoanhNghiep.GetRowCellValue(i, colNganhNghe).ToString();
                        if (chuoi != null)
                        {
                            var string1 = chuoi.Split(',').ToList();
                            foreach (var z in string1)
                            {
                                var xuly = z.Trim().ToString();
                                var check = db.NgheNghieps.SingleOrDefault(p => p.MaNN.ToString() == xuly);
                                if (check != null)
                                {
                                    var job = new NgheNghiepKH();
                                    job.MaNN = check.MaNN;
                                    job.MaKH = obj.MaKH;
                                    db.NgheNghiepKHs.InsertOnSubmit(job);
                                    db.SubmitChanges();
                                }
                            }
                        }

                    }
                    catch { }
                }
            }

            var wait = DialogBox.WaitingForm();

            wait.Close();
            wait.Dispose();

            DialogBox.Alert("Đã lưu");
            if (ltError.Count > 0)
            {
                gcDoanhNghiep.DataSource = ltError;
            }
            else
            {
                gcDoanhNghiep.DataSource = null;
                this.Close();
            }
        }

        private string GetNewMaKH()
        {
            string makh = "";
            db.khKhachHang_getNewMaKH(ref makh);
            return makh;
        }

        private void grvDoanhNghiep_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                if (db.tnKhachHangs.Where(p => p.KyHieu == grvDoanhNghiep.GetRowCellValue(e.RowHandle, colKyHieu).ToString().Trim()).Count() > 0)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
            }
            catch { }
        }

        private void btnXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvDoanhNghiep.DeleteSelectedRows();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcDoanhNghiep);
        }
    }

    class ImportItemDN
    {
        public string KyHieu { get; set; }
        public string MaPhu { get; set; }
        public string TenVT { get; set; }
        public string TenCty { get; set; }
        public string DiaChiCty { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string NguoiDaiDien { get; set; }
        public string ChucVu { get; set; }
        public string MaSoThue { get; set; }
        public string SoDKKD { get; set; }
        public DateTime? NgayDKKD { get; set; }
        public string NoiDKKD { get; set; }
        public string SoTaiKhoan { get; set; }
        public string NganHang { get; set; }
        public int? KhuVuc { get; set; }
        public string NhomKH { get; set; }
        public string TenNLH { get; set; }
        public string SDTNLH { get; set; }
        public string DiaChiNhanThu { get; set; }
        public string QuocTich { get; set; }
        public string Website { get; set; }
        public string NganhNgheDoanhNghiep { get; set; }
        public string EmailKhachThue { get; set; }
        public string DiaPhan { get; set; }
        public string Error { get; set; }
    }
}