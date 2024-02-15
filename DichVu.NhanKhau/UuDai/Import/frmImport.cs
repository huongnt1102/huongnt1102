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

namespace DichVu.NhanKhau.UuDai.Import
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
            lookMatBang.DataSource = db.mbMatBangs.Select(p => new { p.MaMB, p.MaSoMB });
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

        private int? SearchNhanKhau(string chuoi, int? MaMB)
        {
            try
            {
                return db.tnNhanKhaus.FirstOrDefault(p =>p.MaMB == MaMB && SqlMethods.Like(p.HoTenNK, "%" + chuoi + "%")).ID;
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
                        MaNK = SearchNhanKhau(p[2].ToString().Trim(), SearchMatBang(p[3].ToString().Trim())),
                        HoTenNK = p[2].ToString().Trim(),
                        MaMB = SearchMatBang(p[3].ToString().Trim()),
                        TuNgay = MyConvert(p[4]),
                        DenNgay = MyConvert(p[5]),
                        DienGiai = p[6].ToString().Trim(),
                    }).ToList();
                    List<ImportItem> newlist = new List<ImportItem>();
                    foreach (var item in newds)
                    {
                        ImportItem import = new ImportItem()
                        {
                            MaMB = item.MaMB,
                            MaNK  = item.MaNK,
                            HoTenNK = item.HoTenNK,
                            TuNgay = item.TuNgay,
                            DenNgay = item.DenNgay,
                            DienGiai = item.DienGiai,
                        };
                        newlist.Add(import);
                    }
                    gcUuDai.DataSource = newlist;
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
            if (grvUuDai.DataSource == null)
            {
                DialogBox.Alert("Không có dữ liệu lưu");
                return;
            }

            var wait = DialogBox.WaitingForm();
            List<tnnkDangKyUuDai> listMB = new List<tnnkDangKyUuDai>();
            for (int i = 0; i < grvUuDai.RowCount; i++)
            {
                tnnkDangKyUuDai obj = new tnnkDangKyUuDai();
                obj.MaNK = Convert.ToInt32(grvUuDai.GetRowCellValue(i, colMaNK));
                obj.DienGiai = grvUuDai.GetRowCellValue(i, colDienGiai).ToString();
                obj.TuNgay = (DateTime)grvUuDai.GetRowCellValue(i, colTuNgay);
                obj.DenNgay = (DateTime)grvUuDai.GetRowCellValue(i, colDenNgay);
                obj.MaNV = objnhanvien.MaNV;
                obj.IsDuyet = true;
                obj.NgayTao = db.GetSystemDate();
                listMB.Add(obj);
            }
            try
            {
                db.tnnkDangKyUuDais.InsertAllOnSubmit(listMB);
                db.SubmitChanges();
                DialogBox.Alert("Đã lưu");
            }
            catch
            {
                DialogBox.Error("Lỗi dữ liệu, vui lòng kiểm tra lại");
            }
            finally
            {

                wait.Close();
                wait.Dispose();
            }
        }

        private void grvMatBang_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                if (db.tnNhanKhaus.Where(p => p.HoTenNK == grvUuDai.GetRowCellValue(e.RowHandle, colNhanKhau).ToString().Trim()).Count() > 0)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
            }
            catch { }
        }

        private void btnXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvUuDai.DeleteSelectedRows();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }

    class ImportItem
    {
        public int? MaNK { get; set; }
        public int? MaMB { get; set; }
        public string HoTenNK { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string DienGiai { get; set; }
    }
}