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

namespace LandSoftBuilding.Lease.Mau
{
    public partial class frmField : DevExpress.XtraEditors.XtraForm
    {
        public frmField()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        void BieuThuc_Load()
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                treeField.DataSource = db.BmBieuThucHDTs
                    .Select(p => new
                    {
                        p.MaBT,
                        p.MaLBT,
                        p.KyHieu,
                        TenBT = p.TenBT + (p.KyHieu != null ? " (" + p.KyHieu + ")" : ""),
                        p.DienGiai,
                        p.ChinhSua
                    });
            }
        }

        private void frmField_Load(object sender, EventArgs e)
        {
            BieuThuc_Load();
        }


        private void treeField_DoubleClick(object sender, EventArgs e)
        {
            if ((int)treeField.FocusedNode["MaBT"] == 96)
            {
                var frm = new frmFieldDefine();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    BieuThuc_Load();
            }

            string kyHieu = treeField.FocusedNode["KyHieu"] as string;
            if (kyHieu != null)
            {
                var frm = (frmDesign)this.Owner;
                frm.InsertText(kyHieu);
            }
        }

        private void treeField_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            lblTenBT.Text = e.Node["TenBT"] as string;
            lblDienGiai.Text = e.Node["DienGiai"] as string;
        }
    }
}