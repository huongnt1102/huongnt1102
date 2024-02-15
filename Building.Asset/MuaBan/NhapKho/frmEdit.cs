using System;

using System.Windows.Forms;

using Library;

namespace TaiSan.NhapKho
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {


        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this,barManager1);

        }

        
        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkChon_EditValueChanged(object sender, EventArgs e)
        {
            gvTaiSan.PostEditor();
        }

        private void spinSLNhap_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grvTaiSan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {

        }

        private void grvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void grvTaiSan_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            
        }

        private void grvTaiSan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }




        private void chkChonMuaHang_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void repTaiSan_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void glkMuaHang_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}