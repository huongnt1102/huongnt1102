using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DIP.SoftPhoneAPI
{
    public partial class MediaEditor : DevExpress.XtraEditors.XtraForm
    {
        public MediaEditor()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.enableContextMenu = false;
        }

        public string Url { get; set; }

        private void MediaEditor_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = this.Url;
        }

        private void MediaEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            axWindowsMediaPlayer1.close();
        }
    }
}