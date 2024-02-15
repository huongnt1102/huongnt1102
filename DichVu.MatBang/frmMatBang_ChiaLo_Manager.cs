using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace MatBang
{
    public partial class frmMatBang_ChiaLo_Manager : DevExpress.XtraEditors.XtraForm
    {
        public frmMatBang_ChiaLo_Manager()
        {
            InitializeComponent();

            TranslateLanguage.TranslateControl(this);
        }

        private void frmMatBang_ChiaLo_Manager_Load(object sender, EventArgs e)
        {

        }
    }
}