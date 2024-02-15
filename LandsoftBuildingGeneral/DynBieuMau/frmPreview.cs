using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandsoftBuildingGeneral.DynBieuMau
{
    public partial class frmPreview : DevExpress.XtraEditors.XtraForm
    {
        public string RtfText { get; set; }

        public frmPreview()
        {
            InitializeComponent();
            Library.TranslateLanguage.TranslateControl(this);
            this.Load += new EventHandler(frmPreview_Load);
        }

        private void frmPreview_Load(object sender, EventArgs e)
        {
            richEditControl1.RtfText = this.RtfText;
        }
    }
}