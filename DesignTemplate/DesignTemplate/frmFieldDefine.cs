using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using LandSoft.Library;

namespace LandSoft.DuAn.BieuMau
{
    public partial class frmFieldDefine : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public frmFieldDefine()
        {
            InitializeComponent();
        }

        private void frmFieldDefine_Load(object sender, EventArgs e)
        {
            try
            {
                db = new MasterDataContext();
                gcField.DataSource = db.daBieuThucs.Where(p => p.MaLBT == 96);
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }

           // LandSoft.Translate.Language.TranslateControl(this, barManager1);
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
                this.Close();
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
            grvField.SetFocusedRowCellValue("MaLBT", 96);
            grvField.SetFocusedRowCellValue("KyHieu", "[]");
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question() == DialogResult.No) return;

            grvField.DeleteSelectedRows();
        }
    }
}