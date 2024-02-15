using System;
using System.Windows.Forms;
using Library;

namespace TaiSan.DatHang
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {

        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void chckChon_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lookDeXuat_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void txtVAT_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void chkDeXuat_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void grvTaiSan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
        }

        private void grvTaiSan_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
        }

        private void grvTaiSan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void grvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {
 
        }


        private void grvTaiSan_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void sLookNCC_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 1:
                    var frm = new KyThuat.KhachHang.frmEdit() { };
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                    }
                    break;
            }
        }
    }
}