using System;

namespace TaiSan.MuaHang
{
    public partial class frmTraHang : DevExpress.XtraEditors.XtraForm
    {

        public frmTraHang()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {

        }


        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void txtPMH_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lookTaiSanTra_EditValueChanged(object sender, EventArgs e)
        {
            grvTraHang.PostEditor();
        }

        private void grvTraHang_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

    }
}