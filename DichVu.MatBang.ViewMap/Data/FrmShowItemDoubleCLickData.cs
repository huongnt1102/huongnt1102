using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DichVu.MatBang.ViewMap.Data
{
    public partial class FrmShowItemDoubleCLickData : DevExpress.XtraEditors.XtraForm
    {
        public string name { get; set; }
        public int category { get; set; }

        public FrmShowItemDoubleCLickData()
        {
            InitializeComponent();
        }

        private void FrmShowItemDoubleCLickData_Load(object sender, EventArgs e)
        {
            spinEdit1.Value = category;
            textEdit1.Text = name;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            category = (int)spinEdit1.Value;
            name = textEdit1.Text;
            Close();
        }
    }
}