using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DichVu.Quy
{
    public partial class frmCheckList : DevExpress.XtraEditors.XtraForm
    {
        public List<ItemData> listData;
        public frmCheckList()
        {
            InitializeComponent();
        }

        private void frmCheckList_Load(object sender, EventArgs e)
        {
            gcPhieuThu.DataSource = listData;
        }
    }
}