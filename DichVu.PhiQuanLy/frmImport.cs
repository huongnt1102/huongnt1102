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

namespace DichVu.PhiQuanLy
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
            itemNgayNhap.EditValue = DateTime.Now;
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
                        STT = p["STT"].Cast<int>(),
                        MaMB = p["Mã mặt bằng"].Cast<int>(),
                        MaSoMB = p["Mã số mặt bằng"].ToString().Trim(),
                        MaKH = p["Mã khách hàng"].Cast<int>(),
                        MaSoKH = p["Mã số khách hàng"].ToString().Trim(),
                        KhachHang = p["Khách hàng"].ToString().Trim(),
                        SoTien = p["Số tiền"].Cast<decimal>(),
                        GhiChu = p["Ghi chú"].ToString().Trim()
                    }).ToList();
                    List<ImportItem> newlist = new List<ImportItem>();
                    foreach (var item in newds)
                    {
                        ImportItem import = new ImportItem()
                        {
                            STT = item.STT,
                            MaMB = item.MaMB,
                            MaSoMB = item.MaSoMB,
                            MaKH = item.MaKH,
                            MaSoKH = item.MaSoKH,
                            KhachHang = item.KhachHang,
                            SoTien = item.SoTien,
                            GhiChu = item.GhiChu
                        };
                        newlist.Add(import);
                    }
                    gcHopDong.DataSource = newlist;
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
            if (grvHopDong.DataSource == null)
            {
                DialogBox.Alert("Không có dữ liệu lưu");
                return;
            }
            var ngaynhap = (DateTime)itemNgayNhap.EditValue;
            var wait = DialogBox.WaitingForm();
            try
            {

                for (int i = 0; i < grvHopDong.RowCount; i++)
                {
                    db.cnLichSu_addDichVuV2(
                        (int)grvHopDong.GetRowCellValue(i, "MaMB"),
                        (int)grvHopDong.GetRowCellValue(i, "MaKH"),
                        ngaynhap,
                        (decimal)grvHopDong.GetRowCellValue(i, "SoTien"),
                        12, grvHopDong.GetRowCellValue(i, "GhiChu").ToString(), null);
                }
                wait.Close();
                wait.Dispose();
                DialogBox.Alert("Đã lưu");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
                DialogBox.Error("Vui lòng xem lại dữ liệu!");
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void btnXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvHopDong.DeleteSelectedRows();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcHopDong);
        }
    }

    class ImportItem
    {
        public int? STT { get; set; }
        public int? MaMB { get; set; }
        public string MaSoMB { get; set; }
        public int? MaKH { get; set; }
        public string MaSoKH { get; set; }
        public string KhachHang { get; set; }
        public decimal? SoTien { get; set; }
        public string GhiChu { get; set; }
        public string LoaiCap { get; set; }
    }
}