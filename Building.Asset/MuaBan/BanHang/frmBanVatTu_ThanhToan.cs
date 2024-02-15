using System;
using System.Windows.Forms;


namespace TaiSan.XuatKho
{
    public partial class frmBanVatTu_ThanhToan : DevExpress.XtraEditors.XtraForm
    {

        public frmBanVatTu_ThanhToan()
        {
            InitializeComponent();

        }

        private void frmBanVatTu_ThanhToan_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void LoadData()
        {
            
        }


        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}