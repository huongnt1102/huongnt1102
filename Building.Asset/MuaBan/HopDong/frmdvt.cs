using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace TaiSan
{
    public partial class frmdvt : DevExpress.XtraEditors.XtraForm
    {

        public frmdvt()
        {
            InitializeComponent();

            TranslateLanguage.TranslateControl(this,barManager1);
        }

        void LoadData()
        {

        }

        private void frmXuatXu_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvtype.AddNewRow();
        }

        private void grvXuatXu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == DialogResult.No) return;
                grvtype.DeleteSelectedRows();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            grvtype.DeleteSelectedRows();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            DialogBox.Alert("Dữ liệu đã được lưu");

            LoadData();
            this.DialogResult=DialogResult.OK;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm=new Import.frmImportDonViTinh())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
    }
}