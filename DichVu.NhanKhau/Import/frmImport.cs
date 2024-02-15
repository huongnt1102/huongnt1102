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

namespace DichVu.NhanKhau.Import
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public byte? MaTN { get; set; }
        public frmImport()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            lookTrangThai.DataSource = db.tnNhanKhauTrangThais.Select(p => new { p.MaTT, p.TenTrangThai });
            lookMatBang.DataSource = db.mbMatBangs.Select(p => new { p.MaMB, p.MaSoMB });
        }

        private int? SearchMatBang(string chuoi)
        {
            try
            {
                return db.mbMatBangs.FirstOrDefault(p => SqlMethods.Like(p.MaSoMB, "%" + chuoi + "%") & p.MaTN == this.MaTN).MaMB;
            }
            catch
            {
                DialogBox.Alert(chuoi);
                return null;
            }
        }

        private int? SearchTrangThai(string chuoi)
        {
            try
            {
                return db.tnNhanKhauTrangThais.FirstOrDefault(p => SqlMethods.Like(p.TenTrangThai, "%" + chuoi + "%")).MaTT;
            }
            catch
            {
                return null;
            }
        }

        private DateTime? MyConvert(LinqToExcel.Cell value)
        {
            try
            {
                return value.Cast<DateTime>();
                //  return DateTime.FromOADate(Convert.ToInt64(value));
            }
            catch
            {
                return null;
            }
        }

        int QH(string QH)
        {
            int trave = 0;
            MasterDataContext db = new MasterDataContext();
            var KT = db.tnQuanHes.FirstOrDefault(p => p.Name == QH);
            if (KT != null)
            {
                trave = KT.ID;
            }
            else
            {
                tnQuanHe insert = new tnQuanHe();
                insert.Name = QH;
                db.tnQuanHes.InsertOnSubmit(insert);
                db.SubmitChanges();
                var KT2 = db.tnQuanHes.FirstOrDefault(p => p.Name == QH);
                trave = KT2.ID;
            }
            return trave;
        }
        private void btnChonTapTin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Excel file (*.xls)|*.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //var wait = DialogBox.WaitingForm();
                try
                {
                    var book = new LinqToExcel.ExcelQueryFactory(f.FileName);

                    var newds = book.Worksheet(0).Select(p => new
                    {
                        MaMB = SearchMatBang(p["Mã mặt bằng"].ToString().Trim()),
                        HoTenNK = p["Họ tên"].ToString().Trim(),
                        DCLL = p["Địa chỉ"].ToString().Trim(),
                        GioiTinh = p["Giới tính"].Cast<bool>(),
                        NgaySinh = p["Ngày sinh"].ToString().Trim(),
                        CMND = p["CMND"].ToString().Trim(),
                        NgayCap = p["Ngày cấp"].ToString().Trim(),
                        NoiCap = p["Nơi cấp"].ToString().Trim(),
                        DienThoai = p["Điện thoại"].ToString().Trim(),
                        Email = p["Email"].ToString().Trim(),
                        DaDKTT = p["Đã DKTT"].Cast<bool>(),
                        NgayHetHanDKTT = p["Hết hạn DKTT"].ToString().Trim(),
                        TrangThai = SearchTrangThai(p["Trạng thái"].ToString().Trim()),
                        QuocTich = p["Quốc tịch"].ToString().Trim(),
                        NgayDKNK = p["Ngày DKNK"].ToString().Trim(),
                        DienGiai = p["Diễn giải"].ToString().Trim(),
                        QuanHe = p["Quan hệ"].ToString().Trim(),
                        //CardNumber = p[18].ToString().Trim(),
                    }).ToList();
                    var qh = db.tnQuanHes.ToList();
                    List<ImportItem> newlist = new List<ImportItem>();
                    foreach (var item in newds)
                    {
                        ImportItem import = new ImportItem()
                        {
                            MaMB = item.MaMB,
                            HoTenNK = item.HoTenNK,
                            DCTT = item.DCLL,
                            GioiTinh = item.GioiTinh,
                            NgaySinh = (item.NgaySinh),
                            CMND = item.CMND,
                            NgayCap = item.NgayCap,
                            NoiCap = item.NoiCap,
                            DienThoai = item.DienThoai,
                            Email = item.Email,
                            DaDKTT = item.DaDKTT,
                            NgayHetHanDKTT = item.NgayHetHanDKTT,
                            TrangThai = item.TrangThai,
                            QuocTich = item.QuocTich,
                            NgayDK = item.NgayDKNK,
                            DienGiai = item.DienGiai,
                            NhanVien = objnhanvien.MaNV,
                            MaKH = db.mbMatBangs.FirstOrDefault(p => p.MaMB == item.MaMB & p.MaTN == this.MaTN).MaKH,
                            QuanHe = QH(item.QuanHe),
                            // CardNumber = item.CardNumber,
                        };
                        newlist.Add(import);
                    }
                    gcNhanKhau.DataSource = newlist;

                }
                catch (Exception ex)
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                    DialogBox.Alert("Mẫu excel dùng để import dữ liệu không đúng định dạng hoặc dữ liệu không hợp lệ, vui lòng xem lại "+ex.Message);
                    

                }
                finally
                {
                    // wait.Close();
                    // wait.Dispose();
                }
            }
        }



        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNhanKhau.DataSource == null)
            {
                DialogBox.Alert("Không có dữ liệu lưu");
                return;
            }

            var ltNhanKhau = (List<ImportItem>)gcNhanKhau.DataSource;
            var ltError = new List<ImportItem>();
            List<tnNhanKhau> listMB = new List<tnNhanKhau>();
            foreach (var n in ltNhanKhau)
            {
                var KT =
                    db.tnNhanKhaus.FirstOrDefault(
                        p =>
                            p.MaMB == n.MaMB &
                            p.HoTenNK == n.HoTenNK);
                if (KT != null)
                {
                    // var loi= obj;
                    n.Error = "Trùng dữ liệu";
                    ltError.Add(n);

                }
                if ((int?)n.TrangThai == null)
                {
                    n.Error = "Trạng thái không tồn tại";
                    ltError.Add(n);
                }
                else
                {
                    tnNhanKhau obj = new tnNhanKhau();

                    obj.MaMB = n.MaMB;
                    obj.DienGiai = n.DienGiai;
                    obj.HoTenNK = n.HoTenNK;
                    obj.DCTT = n.DCTT;
                    obj.GioiTinh = n.GioiTinh;
                    obj.CMND = n.CMND;
                    try
                    {
                        if (n.NgaySinh != null)
                        {
                            var date = n.NgaySinh;
                            obj.NgaySinh = n.NgaySinh;
                        }


                        else
                        {
                            obj.NgaySinh = "";
                        }
                    }
                    catch { }
                    try
                    {
                        if (n.NgayCap != null)
                        {
                            var date = n.NgayCap;
                            obj.NgayCap = n.NgayCap;
                        }

                        else
                        {
                            obj.NgayCap = "";
                        }
                    }
                    catch
                    {
                    }
                    obj.NoiCap = n.NoiCap;
                    obj.DienThoai = n.DienThoai;
                    obj.Email = n.Email;
                    obj.DaDKTT = n.DaDKTT;

                    if (n.NgayHetHanDKTT != null)
                    {
                        var date = n.NgayHetHanDKTT;
                        obj.NgayHetHanDKTT = n.NgayHetHanDKTT;
                    }
                    else
                    {
                        obj.NgayHetHanDKTT = "";
                    }
                    obj.MaTT = n.TrangThai;
                    obj.QuocTich = n.QuocTich;
                    obj.QuanHeID = n.QuanHe;
                    // obj.CardNumber = n.CardNumber;
                    if (n.NgayDK != null)
                    {
                        var date = n.NgayDK;
                        obj.NgayDK = n.NgayDK;
                    }
                    else
                    {
                        obj.NgayDK = "";
                    }
                    // else obj.NgayDK = db.GetSystemDate();
                    obj.MaKH = n.MaKH;
                    obj.MaNV = objnhanvien.MaNV;

                    listMB.Add(obj);
                    db.tnNhanKhaus.InsertOnSubmit(obj);
                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {

                        n.Error = ex.Message;
                        ltError.Add(n);
                    }

                }

                //grvNhanKhau.SelectRow(i);
                //grvNhanKhau.DeleteSelectedRows();
            }

            var wait = DialogBox.WaitingForm();

            //db.tnNhanKhaus.InsertAllOnSubmit(listMB);
            try
            {
                //db.SubmitChanges();
                DialogBox.Alert("Đã lưu");
                wait.Close();
                if (ltError.Count() > 0)
                {
                    gcNhanKhau.DataSource = ltError;
                }
                else
                {
                    gcNhanKhau.DataSource = null;
                }

                //this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                if ("string or binary data would be truncated. the statement has been terminated".Contains(ex.Message) == true)
                    DialogBox.Alert("Vui lòng kiểm tra lại độ dài ký tự trong họ tên cư dân!");
            }


            // wait.Dispose();
        }

        private void grvMatBang_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //try
            //{
            //    if (e.RowHandle < 0) return;
            //    if (db.tnNhanKhaus.Where(p => p.HoTenNK == grvNhanKhau.GetRowCellValue(e.RowHandle, colHoTen).ToString().Trim()).Count() > 0)
            //    {
            //        e.Appearance.BackColor = Color.LightGreen;
            //    }
            //}
            //catch { }
        }

        private void btnXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvNhanKhau.DeleteSelectedRows();
        }

        private void grvNhanKhau_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "MaMB":
                    if (grvNhanKhau.GetFocusedRowCellValue("MaMB") == null) return;
                    var makh = db.mbMatBangs.Single(p => p.MaMB == (int)grvNhanKhau.GetFocusedRowCellValue("MaMB")).MaKH;
                    grvNhanKhau.SetFocusedRowCellValue(colKhachHang, makh);
                    break;
                default:
                    break;
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcNhanKhau);
        }
    }

    class ImportItem
    {
        public int? MaMB { get; set; }
        public int? MaKH { get; set; }
        public string HoTenNK { get; set; }
        public string DCTT { get; set; }
        public bool GioiTinh { get; set; }
        public string NgaySinh { get; set; }
        public string CMND { get; set; }
        public string NgayCap { get; set; }
        public string NoiCap { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public bool DaDKTT { get; set; }
        public string NgayHetHanDKTT { get; set; }
        public int? TrangThai { get; set; }
        public string QuocTich { get; set; }
        public string NgayDK { get; set; }
        public string DienGiai { get; set; }
        public string CardNumber { get; set; }
        public int? NhanVien { get; set; }
        public int? QuanHe { get; set; }
        public string Error { get; set; }
    }
}