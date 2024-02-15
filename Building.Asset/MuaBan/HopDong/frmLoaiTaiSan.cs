using System;
using System.Windows.Forms;
using Library;
using System.Linq;

using DevExpress.XtraEditors;

using DevExpress.XtraGrid.Views.Grid;
using TaiSan.XuatKho;

namespace TaiSan
{
    public partial class frmLoaiTaiSan :XtraForm
    {

        public frmLoaiTaiSan()
        {
            InitializeComponent();

            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {

        }

        private void frmLoaiMatBang_Load(object sender, EventArgs e)
        {
            LoadData();

            
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvTaiSan.PostEditor();
            gvTaiSan.UpdateCurrentRow();
            
            try
            {




                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            gvTaiSan.AddNewRow();

            gvTaiSan.UpdateCurrentRow();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvTaiSan.DeleteSelectedRows();
        }

        private void itemChiTietTS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {




            using (frmLoaiTaiSanChiTiet frm = new frmLoaiTaiSanChiTiet() { })
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
            using (Import.frmImportTaiSan frm = new Import.frmImportTaiSan())
            {


                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void grvTaiSan_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

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

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void bbiQuyDoiDVT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDvtQuyDoi frm = new frmDvtQuyDoi();

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                LoadData();
        }




        private void grvTaiSan_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "TenDVT2")
            {

                using (var frm = new frmChonDonViTinhVatTu())
                {

                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {

                    }
                }
            }
            gvTaiSan.UpdateCurrentRow();
        }

        private void spinDonGiaBan_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grvTaiSan_InitNewRow(object sender, InitNewRowEventArgs e)
        {

        }

        private void grvTaiSan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }
    }
}