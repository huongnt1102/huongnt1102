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

namespace TaiSan.KhoHang.Import
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
            lookTaiSan.DataSource = db.tsLoaiTaiSans.Select(p=> new { p.MaLTS, p.KyHieu, p.TenLTS });
            lookTrangThai.DataSource = db.tsTrangThais.Select(p=> new { p.MaTT, p.TenTT});
            lookKhoHang.DataSource = db.KhoHangs.Select(p => new { p.ID, p.MaKho, p.TenKho });
        }


        private int? SearchTaiSan(string chuoi)
        {
            try
            {
                return db.tsLoaiTaiSans.FirstOrDefault(p => SqlMethods.Like(p.TenLTS, "%" + chuoi + "%")).MaLTS;
            }
            catch
            {
                return null;
            }
        }

        private int? SearchKhoHang(string chuoi)
        {
            try
            {
                return db.KhoHangs.FirstOrDefault(p => SqlMethods.Like(p.TenKho, "%" + chuoi.Trim() + "%")).ID;
            }
            catch
            {
                return null;
            }
        }

        private int? SearchTrangThai(string chuoi)
        {
            try
            {
                return db.tsTrangThais.FirstOrDefault(p => SqlMethods.Like(p.TenTT, "%" + chuoi + "%")).MaTT;
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
                        TaiSan = SearchTaiSan(p[0].ToString().Trim()),
                        SoLuong = p[1].Cast<int>(),
                        DonGia = p[2].Cast<decimal>(),
                        TrangThai = SearchTrangThai(p[3].ToString().Trim()),
                        NgayNhap = MyConvert(p[4]),
                        KhoHang = SearchKhoHang(p[5].ToString().Trim())
                    }).ToList();
                    List<ImportItem> newlist = new List<ImportItem>();
                    foreach (var item in newds)
                    {
                        ImportItem import = new ImportItem()
                        {
                            MaTS = item.TaiSan,
                            MaTT = item.TrangThai,
                            NgayNhap = item.NgayNhap,
                            SoLuong = item.SoLuong,
                            MaKhoHang = item.KhoHang,
                            DonGia = item.DonGia
                        };
                        newlist.Add(import);
                    }
                    gcMatBang.DataSource = newlist;
                }
                catch
                {
                    DialogBox.Alert("Mẫu excel dùng để import dữ liệu không đúng định dạng, vui lòng xem lại");
                }
                finally
                {
                    wait.Close();
                    wait.Dispose();
                }
            }
        }

        #region func
        private int? SearchLoaiTien(string chuoi)
        {
            try
            {
                return db.tnTyGias.FirstOrDefault(p => SqlMethods.Like(p.TenVT, "%" + chuoi + "%")).MaTG;
            }
            catch
            {
                return null;
            }
        }

        private int? SearchToaNha(string chuoi)
        {
            try
            {
                return db.tnToaNhas.FirstOrDefault(p => SqlMethods.Like(p.TenTN, "%" + chuoi + "%")).MaTN;
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
            }
            catch
            {
                return null;
            }
        }
        #endregion

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMatBang.DataSource == null)
            {
                DialogBox.Alert("Không có dữ liệu lưu");
                return;
            }

            List<Kho> listMB = new List<Kho>();
            for (int i = 0; i < grvMatBang.RowCount; i++)
            {
                Kho obj = new Kho();
                obj.DonGia = (decimal)grvMatBang.GetRowCellValue(i, colDonGia);
                obj.SoLuong = (int)grvMatBang.GetRowCellValue(i, colSoLuong);
                obj.NgayNhap = (DateTime)grvMatBang.GetRowCellValue(i, colNgayNhap);
                obj.MaTT = (int)grvMatBang.GetRowCellValue(i, colTrangThai);
                obj.MaTS = (int)grvMatBang.GetRowCellValue(i, colTaiSan);
                obj.MaNV = objnhanvien.MaNV;
                obj.MaKhoHang = (int)grvMatBang.GetRowCellValue(i, colKhoHang);
                listMB.Add(obj);
            }

            var wait = DialogBox.WaitingForm();

            try
            {
                db.Khos.InsertAllOnSubmit(listMB);
                db.SubmitChanges();

                wait.Close();
                wait.Dispose();

                DialogBox.Alert("Đã lưu");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch(Exception ex)
            {
                DialogBox.Error("Có lỗi xảy ra. Thông báo lỗi: " + ex.Message);
                wait.Close();
                wait.Dispose();
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }

        }

        private void grvMatBang_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
        }

        private void btnXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvMatBang.DeleteSelectedRows();
        }
    }

    class ImportItem
    {
        public int? MaTS { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public int? MaTT { get; set; }
        public DateTime? NgayNhap { get; set; }
        public int? MaNK { get; set; }
        public int? MaKhoHang { get; set; }
    }
}