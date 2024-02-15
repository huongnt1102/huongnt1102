using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace Building.PrintControls
{
    public partial class PrintForm : DevExpress.XtraEditors.XtraForm
    {
        public PrintForm()
        {
            InitializeComponent();
        }

        private void PrintForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.PrintControl.FilterForm != null)
            {
                this.PrintControl.FilterForm.Dispose();
                this.PrintControl.FilterForm = null;
            }
            this.PrintControl.Dispose();
            this.PrintControl = null;
        }
    }
}