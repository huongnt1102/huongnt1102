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

namespace LandSoftBuilding.Marketing.Mail.Templates
{
    public partial class frmCategory : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmCategory()
        {
            InitializeComponent();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            gcCategory.DataSource = db.mailCategories;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                grvCategory.RefreshData();

                db.SubmitChanges();

                DialogBox.Success();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var rows = grvCategory.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Loại mẫu email]. Xin cám ơn!");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            try
            {
                foreach (var i in rows)
                {
                    var obj = db.mailCategories.Single(p => p.CateID == (short)grvCategory.GetRowCellValue(i, "CateID"));
                    db.mailCategories.DeleteOnSubmit(obj);
                }

                db.SubmitChanges();
                grvCategory.DeleteSelectedRows();
            }
            catch (Exception ex) 
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}