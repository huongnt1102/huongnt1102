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

namespace DichVu.ChoThue.Import
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public int? TrangThaiMatBang { get; set; }

        public frmImport()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            lookKhachHang.DataSource = db.tnKhachHangs
                .Select(p => new
                {
                    p.MaKH,
                    KhachHang = p.IsCaNhan.Value ? string.Format("{0} {1}", p.HoKH, p.TenKH) : p.CtyTen
                });
            lookMatBang.DataSource = db.mbMatBangs;
            lookTrangThai.DataSource = db.thueTrangThais;
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

                    //var newds = book.Worksheet(0).Select(p => new
                    //{
                    //    KyHieu = p["Số hợp đồng"].ToString().Trim(),
                    //    NgayKy = p["Ngày ký"].ToString().Trim(),
                    //    MaKH = SearchKhachHang(p["Khách hàng"].ToString().Trim()),
                    //    TrangThai = SearchTrangThai(p["Trạng thái"].ToString().Trim()),
                    //    MaMB = SearchMatBang(p["Mặt bằng"].ToString().Trim()),
                    //    ThanhTien = p["Thành tiền"].ToString().Trim(),
                    //    ThueVAT = p["VAT"].ToString().Trim(),
                    //    TongThanhTien = p["Tổng tiền"].ToString().Trim()
                    //    //NgayThanhLy = p[12].ToString().Trim()
                    //}).ToList();
                    //List<ImportItem> newlist = new List<ImportItem>();
                    //foreach (var item in newds)
                    //{
                    //    ImportItem import = new ImportItem()
                    //    {
                    //        KyHieu = item.KyHieu.Length == 0 ? GetNewMaHD() : item.KyHieu,
                    //        NgayKy = Convert.ToDateTime(item.NgayKy),
                    //        MaKH = item.MaKH,
                    //        MaTT = item.TrangThai,
                    //        MaMB = item.MaMB,
                    //        ThanhTien = Convert.ToDecimal(item.ThanhTien),
                    //        ThueVAT = Convert.ToDecimal(item.ThueVAT),
                    //        TongThanhTien = Convert.ToDecimal(item.TongThanhTien)
                    //    };
                    //    newlist.Add(import);
                    //}
                    //gc.DataSource = newlist;
                }
                catch
                {
                    DialogBox.Alert("Mẫu excel dùng để import dữ liệu không đúng định dạng, vui lòng xem lại");
                }

                wait.Close();
                wait.Dispose();
            }
        }
        private DateTime? NgayThanhLy(string chuoi)
        {
            try
            {
                return Convert.ToDateTime(chuoi);
            }
            catch   
            {
                return null;
            }
        }

        private int? SearchKhachHang(string chuoi)
        {
            try
            {
                return db.tnKhachHangs.FirstOrDefault(p => SqlMethods.Like(p.KyHieu, "%" + chuoi + "%")).MaKH;
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
                return db.thueTrangThais.FirstOrDefault(p => SqlMethods.Like(p.TenTT, "%" + chuoi + "%")).MaTT;
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

        private string GetNewMaHD()
        {
            string MaHDNew = string.Empty;
            db.btHopDong_getNewMaHD(ref MaHDNew);
            return db.DinhDang(1, int.Parse(MaHDNew));
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.FocusedRowHandle < 0) return;
            gv.DeleteSelectedRows();
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.DataSource == null)
            {
                DialogBox.Alert("Không có dữ liệu lưu");
                return;
            }
            gv.UpdateCurrentRow();
            for (int i = 0; i < gv.RowCount; i++)
            {
                if (gv.GetRowCellValue(i, colKhachHang) == null)
                {
                    DialogBox.Alert("Vui lòng xem lại cột khách hàng");
                    return;
                }

                if (gv.GetRowCellValue(i, colMatBang) == null)
                {
                    DialogBox.Alert("Vui lòng xem lại cột mặt bằng");
                    return;
                }
                //if (grvHopDong.GetRowCellValue(i, colTrangThai) == null)
                //{
                //    DialogBox.Alert("Vui lòng xem lại cột trạng thái");
                //    return;
                //}
            }
            List<thueHopDong> listMB = new List<thueHopDong>();
            for (int i = 0; i < gv.RowCount; i++)
            {
                thueHopDong obj = new thueHopDong();
                obj.SoHD = gv.GetRowCellValue(i, colKyHieu).ToString();
                obj.ThanhTien = (decimal?)gv.GetRowCellValue(i, colTong);
                obj.DonGia = (decimal?)gv.GetRowCellValue(i, colThanhTien);
                obj.MaKH = (int?)gv.GetRowCellValue(i, colKhachHang);

                if (gv.GetRowCellValue(i, colTrangThai) == null)
                {
                    obj.ThoiHan = 12; // Ngan han = 1 nam
                    obj.DienGiai = "Thuê ngắn hạn 12 tháng, chưa phát sinh công nợ";
                }

                obj.MaTT = (int?)gv.GetRowCellValue(i, colTrangThai);
                obj.MaTG = 1;
                obj.NgayBG = (DateTime?)gv.GetRowCellValue(i, colNgayKy);
                obj.MaMB = (int?)gv.GetRowCellValue(i, colMatBang);
                obj.MaNV = objnhanvien.MaNV;
                
                var objmb = db.mbMatBangs.Single(p => p.MaMB == obj.MaMB);
                objmb.MaTT = TrangThaiMatBang;
                objmb.MaKH = (int)gv.GetRowCellValue(i, colKhachHang);
                
                listMB.Add(obj);
            }

            var wait = DialogBox.WaitingForm();

            db.thueHopDongs.InsertAllOnSubmit(listMB);
            db.SubmitChanges();

            wait.Close();
            wait.Dispose();

            DialogBox.Alert("Đã lưu");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

    }

    class ImportItem
    {
        public string KyHieu { get; set; }
        public DateTime? NgayKy { get; set; }
        public int? MaKH { get; set; }
        public int? MaMB { get; set; }
        public int? MaTT { get; set; }
        public decimal? ThanhTien { get; set; }
        public decimal? ThueVAT { get; set; }
        public decimal? TongThanhTien { get; set; }
        public DateTime? NgayThanhLy { get; set; }
    }
}