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

namespace LandSoftBuilding.Marketing.SMS
{
    public partial class frmFields : DevExpress.XtraEditors.XtraForm
    {
        public MemoEdit txtContent { get; set; }

        public frmFields()
        {
            InitializeComponent();

           // Translate.Language.TranslateControl(this);
        }

        private void frmFields_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                lbField.DataSource = db.TruongTrons;
            }
        }

        private void lbField_DoubleClick(object sender, EventArgs e)
        {
            txtContent.Text = txtContent.Text.Insert(txtContent.SelectionStart, lbField.SelectedValue.ToString());
        }
    }
}