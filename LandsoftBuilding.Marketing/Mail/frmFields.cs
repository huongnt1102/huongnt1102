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
using MSDN.Html.Editor;

namespace LandSoftBuilding.Marketing.Mail
{
    public partial class frmFields : DevExpress.XtraEditors.XtraForm
    {
        public HtmlEditorControl txtContent { get; set; }

        public frmFields()
        {
            InitializeComponent();
        }

        private void frmFields_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                lbField.DataSource = db.Fields.Where(p => p.CateID == 1);
            }
        }

        private void lbField_DoubleClick(object sender, EventArgs e)
        {
            txtContent.SelectedText = lbField.SelectedValue.ToString();
        }
    }
}