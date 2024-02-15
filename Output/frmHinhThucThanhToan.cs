using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Native;
using Library;

namespace LandSoftBuilding.Fund.Output
{
    public partial class frmHinhThucThanhToan : DevExpress.XtraEditors.XtraForm
    {
        public byte MaTN { get; set; }
        MasterDataContext db = new MasterDataContext();
        public frmHinhThucThanhToan()
        {
            InitializeComponent();
        }

        private void frmHinhThucThanhToan_Load(object sender, EventArgs e)
        {
            gcHTTT.DataSource = db.pcPhanLoais;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            db.SubmitChanges();
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void repositoryItemTextEdit1_EditValueChanged(object sender, EventArgs e)
        {
            var KT = (TextEdit) sender;
            if (KT.Text != "")
            {
                grvHTTT.SetFocusedRowCellValue("MaTN",this.MaTN);
            }
        }

        private void gcHTTT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                {
                    grvHTTT.DeleteSelectedRows();
                    
                }
            }
        }
    }
}