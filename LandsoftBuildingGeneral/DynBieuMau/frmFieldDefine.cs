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

namespace LandsoftBuildingGeneral.DynBieuMau
{
    public partial class frmFieldDefine : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        
        public frmFieldDefine()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmFieldDefine_Load(object sender, EventArgs e)
        {
            try
            {
                db = new MasterDataContext();
                gcField.DataSource = db.BmBieuThucs;//.Where(p => p.MaLBT == 96);
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void grvField_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //grvField.SetFocusedRowCellValue("MaLBT", 96);
            grvField.SetFocusedRowCellValue("KyHieu", "[]");
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            grvField.DeleteSelectedRows();
        }
    }
}