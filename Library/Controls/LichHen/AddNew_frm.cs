using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Data.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace Library.Controls.LichHen
{
    public partial class AddNew_frm : DevExpress.XtraEditors.XtraForm
    {
        public byte? MaTN;

        public int? MaLH;

        public int? MaNC;

        public int? MaKH;

        public int? MaCD;

        public bool IsView = false;

        public AddNew_frm()
        {
            InitializeComponent();
            
        }

        private void AddNew_frm_Load(object sender, EventArgs e)
        {
            if (IsView)
             layoutSaveNSend.Visibility = layoutSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            
            ctlEditLichHen1.MaTN = this.MaTN;
            ctlEditLichHen1.MaNC = this.MaNC;
            ctlEditLichHen1.MaKH = this.MaKH;
            ctlEditLichHen1.LoadData(this.MaLH);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ctlEditLichHen1.Save(false))
                DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void itemSaveNSend_Click(object sender, EventArgs e)
        {
            if (ctlEditLichHen1.Save(true))
                DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}