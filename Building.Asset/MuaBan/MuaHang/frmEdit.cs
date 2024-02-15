using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace TaiSan.MuaHang
{
    public partial class frmEdit : XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }
        
        private void frmEdit_Load(object sender, EventArgs e)
        {

        }
        private void slookLoaiTS_EditValueChanged(object sender, EventArgs e)
        {
            grvTaiSan.PostEditor();
            grvTaiSan.UpdateCurrentRow();
        }
        private void chkDonHang_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void lookDatHang_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void sLookNCC_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grvTaiSan_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            
        }

        private void grvTaiSan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
        }

        private void grvTaiSan_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                
            }
            catch
            {
                return;
            }
        }

        private void grvTaiSan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void lookKhoHang_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void grvTaiSan_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void sLookNCC_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 1:
                    var frm = new KyThuat.KhachHang.frmEdit() {  };
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                       
                    }
                    break;
            }
        }
    }
}