using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoft.DuAn.BieuMau
{
    public partial class frmPreview : DevExpress.XtraEditors.XtraForm
    {
        public string RtfText { get; set; }
        public int PaddingTop { get; set; }
        public int PaddingBottom { get; set; }
        public int PaddingLeft { get; set; }
        public int PaddingRight { get; set; }

        public frmPreview()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmPreview_Load);
        }

        private void frmPreview_Load(object sender, EventArgs e)
        {
            richEditControl1.RtfText = this.RtfText;
           // richEditControl1.Views.SimpleView.Padding = new Padding(this.PaddingLeft, this.PaddingTop, this.PaddingRight, this.PaddingBottom);
        }
    }
}