using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace Building.SMS
{
    public partial class FrmField : XtraForm
    {
        public FrmField()
        {
            InitializeComponent();
        }

        private void BieuThuc_Load()
        {
            using (var db = new MasterDataContext())
            {
                gcField.DataSource = (from field in db.SmsFields
                    join gr in db.SmsTyles on field.GroupId equals gr.Id
                    where field.GroupId == (int?) lookUpGroup.EditValue
                    select new
                    {
                        field.Field, gr.Name, FieldName = field.Name, field.Description, field.GroupSub,
                        field.Symbol
                    }).ToList();
                gvField.ExpandAllGroups();
            }
        }

        private void frmField_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                lookUpGroup.Properties.DataSource = db.SmsTyles;
            }
        }

        private void frmField_MouseDown(object sender, MouseEventArgs e)
        {
            Opacity = 1;
        }

        private void frmField_MouseClick(object sender, MouseEventArgs e)
        {
            Opacity = 1;
        }

        private void lookUpGroup_EditValueChanged(object sender, EventArgs e)
        {
            BieuThuc_Load();
        }

        private void gvField_DoubleClick(object sender, EventArgs e)
        {
            if (gvField.FocusedRowHandle < 0) return;
            var kyHieu = gvField.GetFocusedRowCellValue("Symbol") as string;
            if (kyHieu == null) return;
            var frm = (FrmDesign)Owner;
            frm.InsertText(kyHieu);
        }

        private void gvField_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvField.FocusedRowHandle >= 0)
            {
                lblTenBT.Text = gvField.GetFocusedRowCellValue("FieldName") as string;
                lblDienGiai.Text = gvField.GetFocusedRowCellValue("Description") as string;
            }
            else
            {
                lblTenBT.Text = "";
                lblDienGiai.Text = "";
            }
        }
    }
}