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
using System.Linq.Expressions;

namespace TaiSan.Import
{
    public partial class frmImportGhiTang : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        public frmImportGhiTang()
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
            lookDVSD.DataSource = db.tsDonViSuDungs;
            lookLoaiTS.DataSource = db.tsLoaiTaiSans.Select(p => new { p.MaLTS, p.TenLTS});
            lookMatBang.DataSource = db.mbMatBangs.Select(p => new { p.MaMB, p.MaSoMB});
            LookNCC.DataSource = db.tnNhaCungCaps.Select(p => new { p.MaNCC, p.TenNCC});
            lookHeThong.DataSource = db.tsHeThongs;
        }

        private int? SearchHeThong(string chuoi)
        {
            try
            {
                return db.tsHeThongs.FirstOrDefault(p => SqlMethods.Like(p.TenHT, "%" + chuoi + "%")).ID;
            }
            catch
            {
                return null;
            }
        }

        private int? SearchNCC(string chuoi)
        {
            try
            {
                return db.tnNhaCungCaps.FirstOrDefault(p => SqlMethods.Like(p.TenNCC, "%" + chuoi + "%")).MaNCC;
            }
            catch
            {
                return null;
            }
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

        private int? SearchLoaiTS(string chuoi)
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

        private int? SearchDVSD(string chuoi)
        {
            try
            {
                return db.tsDonViSuDungs.FirstOrDefault(p => SqlMethods.Like(p.TenDV, "%" + chuoi + "%")).ID;
            }
            catch
            {
                return null;
            }
        }

        private DateTime? ConvertDateTime(LinqToExcel.Cell value)
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
                        MaTS = p[0].ToString().Trim(),
                        TenTS = p[1].ToString().Trim(),
                        LoaiTS = SearchLoaiTS(p[2].ToString().Trim()),
                        DonViSD = SearchDVSD(p[3].ToString().Trim()),
                        IsNoiBo = p[4].ToString().Trim() == "Yes" ? true : false,
                        MaMB = SearchMatBang(p[5].ToString().Trim().Substring(0, p[5].ToString().Trim().IndexOf(","))),
                        NhaSX = p[6].ToString().Trim(),
                        NamSX = p[7].ToString() != null ? Convert.ToInt32(p[7].ToString().Trim()) : 0,
                        QuocGiaSX = p[8].ToString().Trim(),
                        NhaCUngCap = SearchNCC(p[9].ToString().Trim()),
                        HanSuDung = p[10] != null ? Convert.ToDecimal((p[10].Value)) : 0M,
                        DVTHanSuDung = (p[11].ToString().Trim() == "Năm" ? true : false),
                        MaSoMatBang= p[5].ToString().Trim(),
                        SoGT= p[12].ToString().Trim(),
                        NgayGT = ConvertDateTime(p[13]),
                        MaHT=SearchHeThong(p[14].ToString().Trim())
                    }).ToList();
                    List<ImportGhiTang> newlist = new List<ImportGhiTang>();
                    foreach (var item in newds)
                    {
                        ImportGhiTang import = new ImportGhiTang()
                        {
                            MaTS=item.MaTS,
                            TenTS=item.TenTS,
                            LoaiTS=item.LoaiTS,
                            DonViSD=item.DonViSD,
                            IsNoiBo=item.IsNoiBo,
                            MaMB=item.MaMB,
                            NhaSX=item.NhaSX,
                            NamSX=item.NamSX,
                            QuocGiaSX=item.QuocGiaSX,
                            NhaCungCap=item.NhaCUngCap,
                            HanSuDung=item.HanSuDung,
                            DVTHanSuDung=item.DVTHanSuDung,
                            MaSoMatBang=item.MaSoMatBang,
                            SoGT=item.SoGT,
                            NgayGT=item.NgayGT,
                            MaHT=item.MaHT
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

            List<tsTaiSan> listLTS = new List<tsTaiSan>();
            for (int i = 0; i < grvTaiSan.RowCount; i++)
            {
                tsTaiSan obj = new tsTaiSan();
                obj.MaTS = grvTaiSan.GetRowCellValue(i, "MaTS").ToString();
                obj.TenTS = grvTaiSan.GetRowCellValue(i, "TenTS").ToString();
                obj.MaLTS = (int?)grvTaiSan.GetRowCellValue(i, "LoaiTS");
                obj.MaDVSD = (int?)grvTaiSan.GetRowCellValue(i, "DonViSD");
                obj.IsNoiBo = (bool?)grvTaiSan.GetRowCellValue(i, "IsNoiBo");
                obj.MaMB = (int?)grvTaiSan.GetRowCellValue(i, "MaMB");
                obj.NhaSanXuat = grvTaiSan.GetRowCellValue(i, "NhaSX").ToString();
                obj.NamSX = (int?)grvTaiSan.GetRowCellValue(i, "NamSX");
                obj.NuocSX = grvTaiSan.GetRowCellValue(i, "QuocGiaSX").ToString();
                obj.MaNCC = (int?)grvTaiSan.GetRowCellValue(i, "NhaCungCap");
                obj.ThoiGianSD = (decimal?)grvTaiSan.GetRowCellValue(i, "HanSuDung");
                obj.DVTTGSD = (bool?)grvTaiSan.GetRowCellValue(i, "DVTHanSuDung");
                obj.SoGT = grvTaiSan.GetRowCellValue(i, "SoGT").ToString();
                obj.NgayGT = (DateTime?)grvTaiSan.GetRowCellValue(i, "NgayGT");
                obj.MaHT = (int?)grvTaiSan.GetRowCellValue(i, "MaHT");
                listLTS.Add(obj);

                var list = grvTaiSan.GetRowCellValue(i, "MaSoMatBang").ToString().Trim().Split(',');
                foreach (var p in list)
                {
                    var objTSMB = new tsTaiSanMatBang();
                    objTSMB.MaMB = SearchMatBang(p);
                    //  objTSMB.MaTS = obj.ID;
                    listLTS[i].tsTaiSanMatBangs.Add(objTSMB);
                }
            }

            var wait = DialogBox.WaitingForm();
            try
            {
                db.tsTaiSans.InsertAllOnSubmit(listLTS);
                db.SubmitChanges();
                wait.Close();
                wait.Dispose();
                DialogBox.Alert("Đã lưu");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
                DialogBox.Error("Vui lòng xem lại dữ liệu. Lưu ý: Mã tài sản không được phép trùng");
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

    class ImportGhiTang
    {
        public string MaTS { get; set; }
        public string TenTS { get; set; }
        public int? LoaiTS { get; set; }
        public int? DonViSD { get; set; }
        public bool? IsNoiBo { get; set; }
        public int? MaMB { get; set; }
        public string NhaSX { get; set; }
        public int? NamSX { get; set; }
        public string QuocGiaSX { get; set; }
        public int? NhaCungCap { get; set; }
        public decimal? HanSuDung { get; set; }
        public bool? DVTHanSuDung { get; set; }
        public string MaSoMatBang { get; set; }
        public DateTime? NgayGT { get; set; }
        public string SoGT { get; set; }
        public int? MaHT { get; set; }
    }
}