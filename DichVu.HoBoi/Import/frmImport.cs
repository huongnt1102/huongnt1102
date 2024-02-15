using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq;
using Library;
using System.Data.Linq.SqlClient;

namespace DichVu.HoBoi.Import
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

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
            lookNhanKhau.DataSource = db.tnNhanKhaus.Select(p => new { p.ID, p.HoTenNK }).ToList();
            lookLoaiThe.DataSource = db.dvhbLoaiThes.Select(p => new { p.ID, p.TenLT }).ToList();
            lookMatBang.DataSource = db.mbMatBangs.Select(p => new { p.MaMB, p.MaSoMB }).ToList();
        }

        private int? SearchMatBang(string chuoi)
        {
            try
            {
                return db.mbMatBangs.FirstOrDefault(p => SqlMethods.Like(p.MaSoMB, "%" + chuoi + "%")).MaMB;
            }
            catch
            {
                return null;
            }
        }

        private int? SearchNhanKhau(string chuoi)
        {
            try
            {
                return db.tnNhanKhaus.FirstOrDefault(p => SqlMethods.Like(p.HoTenNK, "%" + chuoi + "%")).ID;
            }
            catch
            {
                return null;
            }
        }

        private short? SearchLoaiThe(string chuoi)
        {
            try
            {
                return db.dvhbLoaiThes.FirstOrDefault(p => SqlMethods.Like(p.TenLT, "%" + chuoi + "%")).ID;
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
                //return DateTime.FromOADate(Convert.ToInt64(value));
            }
            catch
            {
                return null;
            }
        }

        private void btnChonTapTin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Excel file (*.xls)|*.xls";
            if (f.ShowDialog() == DialogResult.OK)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    var book = new LinqToExcel.ExcelQueryFactory(f.FileName);
                    var newds = book.Worksheet(0).Select(p => new
                    {
                        SoThe = p["Số thẻ"].ToString().Trim(),
                        NgayDangKy = MyConvert(p["Ngày đăng ký"]),
                        NgayHetHan = MyConvert(p["Ngày hết hạn"]),
                        MaLT = SearchLoaiThe(p["Loại thẻ"].ToString().Trim()),
                        MucPhi = p["Mức phí"].Cast<decimal>(),
                        MaMB = SearchMatBang(p["Mặt bằng"].ToString().Trim()),
                        MaNK = SearchNhanKhau(p["Nhân khẩu"].ToString().Trim()),
                        ChuThe = p["Chủ thẻ"].ToString().Trim(),
                        IsSuDung = p["Đang sử dụng"].Cast<bool>(),
                        IsTinhDuThang = p["IsTinhDuThang"].Cast<bool>(),
                        DienGiai = p["Diễn giải"].ToString().Trim(),
                    }).ToList();
                    List<ImportItem> newlist = new List<ImportItem>();
                    foreach (var item in newds)
                    {
                        ImportItem import = new ImportItem()
                        {
                            SoThe = item.SoThe,
                            NgayDangKy = item.NgayDangKy,
                            NgayHetHan = item.NgayHetHan,
                            MaLT = item.MaLT,
                            MucPhi = item.MucPhi,
                            MaMB = item.MaMB,
                            MaNK = item.MaNK,
                            ChuThe = item.ChuThe,
                            IsSuDung = item.IsSuDung,
                            IsTinhDuThang = item.IsTinhDuThang,
                            DienGiai = item.DienGiai,
                        };
                        newlist.Add(import);
                    }
                    gcHoBoi.DataSource = newlist;
                }
                catch
                {
                    DialogBox.Alert("Mẫu excel dùng để import dữ liệu không đúng định dạng hoặc dữ liệu không hợp lệ, vui lòng xem lại");
                }
                finally
                {
                    wait.Close();
                    wait.Dispose();
                }
            }
        }

        string LayNgay(string chuoi)
        {
            chuoi = chuoi != "" & chuoi.Length > 10 ? chuoi.Substring(0, chuoi[9] != ' ' ? 9 : 10) : chuoi;
            return chuoi;
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvHoBoi.DataSource == null)
            {
                DialogBox.Alert("Không có dữ liệu lưu");
                return;
            }

            List<dvhbHoBoi> listHB = new List<dvhbHoBoi>();
            for (int i = 0; i < grvHoBoi.RowCount; i++)
            {
                dvhbHoBoi objhb = new dvhbHoBoi();
                objhb.SoThe = grvHoBoi.GetRowCellValue(i, colSoThe).ToString();
                objhb.NgayDangKy = (DateTime)grvHoBoi.GetRowCellValue(i, colNgayDangKy);
                objhb.NgayHetHan = (DateTime)grvHoBoi.GetRowCellValue(i, colNgayHetHan);
                objhb.MaLT = (short?)grvHoBoi.GetRowCellValue(i, colLoaiThe);
                objhb.MucPhi = (decimal?)grvHoBoi.GetRowCellValue(i, colMucPhi);
                objhb.MaMB = (int?)grvHoBoi.GetRowCellValue(i, colMatBang);
                objhb.MaNK = (int?)grvHoBoi.GetRowCellValue(i, colNhanKhau);
                objhb.ChuThe = grvHoBoi.GetRowCellValue(i, colChuThe).ToString();
                objhb.IsSuDung = (bool?) grvHoBoi.GetRowCellValue(i, colIsSuDung);
                objhb.IsTinhDuThang = (bool?)grvHoBoi.GetRowCellValue(i, colIsTinhDuThang);
                objhb.DienGiai = grvHoBoi.GetRowCellValue(i, colDienGiai).ToString();
                objhb.MaNV = objnhanvien.MaNV;
                objhb.NgayTao = db.GetSystemDate();

                listHB.Add(objhb);
            }

            var wait = DialogBox.WaitingForm();

            db.dvhbHoBois.InsertAllOnSubmit(listHB);
            db.SubmitChanges();

            wait.Close();
            wait.Dispose();

            DialogBox.Alert("Đã lưu");
        }

        private void grvMatBang_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //try
            //{
            //    if (e.RowHandle < 0) return;
            //    if (db.tnNhanKhaus.Where(p => p.HoTenNK == grvHoBoi.GetRowCellValue(e.RowHandle, colSoThe).ToString().Trim()).Count() > 0)
            //    {
            //        e.Appearance.BackColor = Color.LightGreen;
            //    }
            //}
            //catch { }
        }

        private void btnXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvHoBoi.DeleteSelectedRows();
        }

        private void grvNhanKhau_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "MaNK":
                    if (grvHoBoi.GetFocusedRowCellValue("MaNK") == null) return;
                    var makh = db.tnNhanKhaus.Single(p => p.ID == (int)grvHoBoi.GetFocusedRowCellValue("MaNK")).HoTenNK;
                    grvHoBoi.SetFocusedRowCellValue(colChuThe, makh);
                    break;
                default:
                    break;
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcHoBoi);
        }
    }

    class ImportItem
    {
        public string SoThe { get; set; }
        public DateTime? NgayDangKy { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public short? MaLT { get; set; }
        public decimal? MucPhi { get; set; }
        public int? MaMB { get; set; }
        public int? MaNK { get; set; }
        public string ChuThe { get; set; }
        public bool? IsSuDung { get; set; }
        public bool? IsTinhDuThang { get; set; }
        public string DienGiai { get; set; }
    }
}