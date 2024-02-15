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

namespace BuildingDesignTemplate
{
    public partial class frmFieldDefine : DevExpress.XtraEditors.XtraForm
    {
        public byte? GroupId { get; set; }

        MasterDataContext _db;
        
        public frmFieldDefine()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmFieldDefine_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _db = new MasterDataContext();
                glkGroup.DataSource = GroupId != null ? _db.rptGroups.Where(_ => _.ID == GroupId) : _db.rptGroups;
                gcField.DataSource = GroupId != null ? _db.template_Fields.Where(_ => _.GroupId == GroupId) : _db.template_Fields;
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
                _db.SubmitChanges();
                Library.DialogBox.Success();
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
            if (GroupId != null) grvField.SetFocusedRowCellValue("GroupId", GroupId);
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            grvField.DeleteSelectedRows();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new BuildingDesignTemplate.Import.FrmField())
            {
                frm.ShowDialog();
                LoadData();
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcField);
        }
    }
}