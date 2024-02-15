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
    public partial class frmFieldV2 : DevExpress.XtraEditors.XtraForm
    {
        public frmFieldV2()
        {
            InitializeComponent();
        }

        void BieuThuc_Load()
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                gcField.DataSource = (from bt in db.daBieuThucs
                                        join gr in db.fieldDetails on bt.MaBT equals gr.FieldID
                                        join lbt in db.daBieuThucs on bt.MaLBT equals lbt.MaBT
                                        where gr.GroupID == (int?)lookUpGroup.EditValue
                                        select new
                                        {
                                            bt.MaBT,
                                            bt.MaLBT,
                                            bt.KyHieu,
                                            TenBT = bt.TenBT + (bt.KyHieu != null ? " (" + bt.KyHieu + ")" : ""),
                                            bt.DienGiai,
                                            bt.ChinhSua,
                                            Name = lbt.TenBT
                                        }).ToList();
                gvField.ExpandAllGroups();
            }
        }

        private void frmField_Load(object sender, EventArgs e)
        {
            //BieuThuc_Load();
            using (var db = new MasterDataContext())
            {
                lookUpGroup.Properties.DataSource = db.fieldGroups;
            }

           // LandSoft.Translate.Language.TranslateControl(this);
        }

        private void frmField_MouseDown(object sender, MouseEventArgs e)
        {
            this.Opacity = 1;
        }

        private void frmField_MouseClick(object sender, MouseEventArgs e)
        {
            this.Opacity = 1;
        }

        private void lookUpGroup_EditValueChanged(object sender, EventArgs e)
        {
            BieuThuc_Load();
        }

        private void gvField_DoubleClick(object sender, EventArgs e)
        {
            if (gvField.FocusedRowHandle >= 0)
            {
                string kyHieu = gvField.GetFocusedRowCellValue("KyHieu") as string;
                if (kyHieu != null)
                {
                    var frm = (frmDesign)this.Owner;
                    frm.InsertText(kyHieu);
                }
            }
        }

        private void gvField_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvField.FocusedRowHandle >= 0)
            {
                lblTenBT.Text = gvField.GetFocusedRowCellValue("TenBT") as string;
                lblDienGiai.Text = gvField.GetFocusedRowCellValue("DienGiai") as string;
            }
            else
            {
                lblTenBT.Text = "";
                lblDienGiai.Text = "";
            }
        }
    }
}