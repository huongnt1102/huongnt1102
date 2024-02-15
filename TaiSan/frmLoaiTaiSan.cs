using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using System.Data;

namespace TaiSan
{
    public partial class frmLoaiTaiSan : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        public frmLoaiTaiSan()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            db = new MasterDataContext();
            gcTaiSan.DataSource = db.tsLoaiTaiSans;
            lookDVT.DataSource = db.tsLoaiTaiSan_DVTs;
            lookLoai.DataSource = db.tsLoaiTaiSan_Types;
            lookThue.DataSource = db.tsLoaiTanSan_Thues;
            lookTN.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN});
            lookLTSCha.DataSource = db.tsLoaiTaiSans.Select(p => new { p.MaLTS, p.TenLTS});
        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvTaiSan.UpdateCurrentRow();
            try
            {
                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvTaiSan.AddNewRow();
            grvTaiSan.SetFocusedRowCellValue("MaTN", objnhanvien.MaTN);
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvTaiSan.DeleteSelectedRows();
        }

        private void itemChiTietTS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTaiSan.FocusedRowHandle <= 0)
            {
                DialogBox.Alert("Vui lòng chọn tài sản");
                return;
            }

            int MaTS = (int)grvTaiSan.GetFocusedRowCellValue("MaLTS");
            var ts = db.tsLoaiTaiSans.Single(p => p.MaLTS == MaTS);
            using (frmLoaiTaiSanChiTiet frm = new frmLoaiTaiSanChiTiet() { objlts = ts })
            {
                frm.ShowDialog();
            }
        }

        private void btnloai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmloaits frm = new frmloaits();
            frm.ShowDialog();
            LoadData();
        }

        private void btndvt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmdvt frm = new frmdvt();
            frm.ShowDialog();
            LoadData();
        }

        private void btnTiLeThue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTileThue frm = new frmTileThue();
            frm.ShowDialog();
            LoadData();
        }

        private void btnImportTaiSan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (Import.frmImport frm = new Import.frmImport() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void grvTaiSan_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var x = (tsLoaiTaiSan)grvTaiSan.GetFocusedRow();
            if (x == null) return;
            if (e.Column.FieldName == "ThueID")
            {
                x.tsLoaiTanSan_Thue = db.tsLoaiTanSan_Thues.SingleOrDefault(p => p.ThueID == (int)e.Value);
            }
            if (e.Column.FieldName == "TypeID")
            {
                x.tsLoaiTaiSan_Type = db.tsLoaiTaiSan_Types.SingleOrDefault(p => p.TypeID == (int)e.Value);
            }
            if (e.Column.FieldName == "MaDVT")
            {
                x.tsLoaiTaiSan_DVT = db.tsLoaiTaiSan_DVTs.SingleOrDefault(p => p.MaDVT == (int)e.Value);
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new System.Windows.Forms.SaveFileDialog();
            frm.Filter = "Excel|*.xls";
            frm.FileName = "Loai-tai-san";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                gcTaiSan.ExportToXls(frm.FileName);
                if (DialogBox.Question("Đã xử lý xong, bạn có muốn xem lại không?") == System.Windows.Forms.DialogResult.Yes)
                    System.Diagnostics.Process.Start(frm.FileName);
            }
        }     
    }
}