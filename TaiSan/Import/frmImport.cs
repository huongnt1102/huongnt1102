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

namespace TaiSan.Import
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
            lookDVT.DataSource = db.tsLoaiTaiSan_DVTs.Select(p=> new { p.TenDVT, p.MaDVT});
            lookThue.DataSource = db.tsLoaiTanSan_Thues.Select(p=> new { p.ThueID, p.TiLe });
            lookLoai.DataSource = db.tsLoaiTaiSan_Types.Select(p => new { p.TypeID, p.TypeNam });
            lookLTSCha.DataSource = db.tsLoaiTaiSans.Select(p => new { p.MaLTS, p.TenLTS});
            lookTN.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN});
        }

        private int? SearchLoai(string chuoi)
        {
            try
            {
                return db.tsLoaiTaiSan_Types.FirstOrDefault(p => SqlMethods.Like(p.TypeNam, "%" + chuoi + "%")).TypeID;
            }
            catch
            {
                return null;
            }
        }

        private int? SearchDVT(string chuoi)
        {
            try
            {
                return db.tsLoaiTaiSan_DVTs.FirstOrDefault(p => SqlMethods.Like(p.TenDVT, "%" + chuoi + "%")).MaDVT;
            }
            catch
            {
                return null;
            }
        }

        private int? SearchTiLe(string chuoi)
        {
            try
            {
                return db.tsLoaiTanSan_Thues.FirstOrDefault(p => p.TiLe == Convert.ToDecimal(chuoi)).ThueID;
            }
            catch
            {
                return null;
            }
        }

        private byte? SearchToaNha(string chuoi)
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

        private int? SeachLoaiTSCha(string chuoi)
        {
            try
            {
                return db.tsLoaiTaiSans.FirstOrDefault(p => SqlMethods.Like(p.TenLTS, "%" + chuoi + "")).MaLTS;
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
                        MaTN = SearchToaNha(p[0].ToString().Trim()),
                        KyHieu = p[1].ToString().Trim(),
                        TenLTS = p[2].ToString().Trim(),
                        MaLTSCha = SeachLoaiTSCha(p[3].ToString().Trim()),
                        Loai = SearchLoai(p[4].ToString().Trim()),
                        DVT = SearchDVT(p[5].ToString().Trim()),
                        Thue = SearchTiLe(p[6].ToString().Trim()),
                        DacTinh = p[7].ToString().Trim()

                    }).ToList();
                    List<ImportItem> newlist = new List<ImportItem>();
                    foreach (var item in newds)
                    {
                        ImportItem import = new ImportItem()
                        {
                            KyHieu = item.KyHieu,
                            TenLTS = item.TenLTS,
                            LoaiTSCha = item.MaLTSCha,
                            DacTinh = item.DacTinh,
                            ToaNha = item.MaTN,
                            DVT = item.DVT,
                            Thue = item.Thue,
                            Loai = item.Loai,
                        };
                        newlist.Add(import);
                    }
                    gcTaiSan.DataSource = newlist;
                }
                catch
                {
                    DialogBox.Alert("Mẫu excel dùng để import dữ liệu không đúng định dạng, vui lòng xem lại");
                }

                wait.Close();
                wait.Dispose();
            }
        }
        
        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTaiSan.DataSource == null)
            {
                DialogBox.Alert("Không có dữ liệu lưu");
                return;
            }

            List<tsLoaiTaiSan> listLTS = new List<tsLoaiTaiSan>();
            for (int i = 0; i < grvTaiSan.RowCount; i++)
            {
                tsLoaiTaiSan obj = new tsLoaiTaiSan();
                obj.KyHieu = grvTaiSan.GetRowCellValue(i, colKyHieu).ToString();
                obj.TenLTS = grvTaiSan.GetRowCellValue(i, colTenLTS).ToString();
                obj.TiLeKhauHao = (decimal?)grvTaiSan.GetRowCellValue(i, colTiLeKhauHao);
                obj.TypeID = (int?)grvTaiSan.GetRowCellValue(i, colLoai);
                obj.ThueID = (int?)grvTaiSan.GetRowCellValue(i, colThue);
                obj.MaTN = (byte?)grvTaiSan.GetRowCellValue(i, "ToaNha");
                obj.DacTinh = grvTaiSan.GetRowCellValue(i, colDacTinh).ToString();
                obj.MaDVT = (int?)grvTaiSan.GetRowCellValue(i, colDVT);

                listLTS.Add(obj);
            }

            var wait = DialogBox.WaitingForm();
            try
            {
                db.tsLoaiTaiSans.InsertAllOnSubmit(listLTS);
                db.SubmitChanges();
                wait.Close();
                wait.Dispose();
                DialogBox.Alert("Đã lưu");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
                DialogBox.Error("Vui lòng xem lại dữ liệu. Lưu ý: Mã loại tài sản không được phép trùng");
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
                if (db.mbMatBangs.Where(p => p.MaSoMB == grvTaiSan.GetRowCellValue(e.RowHandle, colKyHieu).ToString().Trim()).Count() > 0)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
            }
            catch { }
        }

        private void btnXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvTaiSan.DeleteSelectedRows();
        }
    }

    class ImportItem
    {
        public string KyHieu { get; set; }
        public string TenLTS { get; set; }
        public string DacTinh { get; set; }
        public decimal TiLeKhauHao { get; set; }
        public int? LoaiTSCha { get; set; }
        public int? Loai { get; set; }
        public int? DVT { get; set; }
        public int? Thue { get; set; }
        public byte? ToaNha { get; set; }
    }
}