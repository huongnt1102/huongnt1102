using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace BuildingDesignTemplate
{
    public partial class FrmField : XtraForm
    {
        public byte? GroupId { get; set; }

        private Library.MasterDataContext db = new Library.MasterDataContext();

        public FrmField()
        {
            InitializeComponent();
        }

        private void BieuThuc_Load()
        {
            gcField.DataSource = (from field in db.template_Fields
                join gr in db.rptGroups on field.GroupId equals gr.ID
                where field.GroupId == (byte?)lookUpGroup.EditValue
                select new
                {
                    field.Field,
                    gr.Name,
                    FieldName = field.Name,
                    field.Description,
                    field.GroupSub,
                    field.Symbol
                }).ToList();
            gvField.ExpandAllGroups();
        }

        private void frmField_Load(object sender, EventArgs e)
        {
            lookUpGroup.Properties.DataSource = db.rptGroups;
            if (GroupId != null) lookUpGroup.EditValue = GroupId;
        }

        private void frmField_MouseDown(object sender, MouseEventArgs e)
        {
            //Opacity = 1;
        }

        private void frmField_MouseClick(object sender, MouseEventArgs e)
        {
            //Opacity = 1;
        }

        private void lookUpGroup_EditValueChanged(object sender, EventArgs e)
        {
            BieuThuc_Load();
        }

        private void gvField_DoubleClick(object sender, EventArgs e)
        {
            if (gvField.FocusedRowHandle < 0) return;
            var kyHieu = gvField.GetFocusedRowCellValue("Field") as string;
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