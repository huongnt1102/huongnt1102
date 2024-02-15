using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;

namespace AnNinh
{
    public partial class frmGhiChu : DevExpress.XtraEditors.XtraForm
    {
        public string NoiDung = string.Empty;
        public frmGhiChu()
        {
            InitializeComponent();
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            NoiDung = txtnoiDung.Text.Trim();
            this.Close();
        }

        private delegate void ChangeText();
        void ChangeTextLabel(string i)
        {
            labelControl1.Text = i.ToString();
        }

        private void frmGhiChu_Load(object sender, EventArgs e)
        {
            txtnoiDung.Text = NoiDung;
            //Thread th = new Thread(Update123);
            //th.Start();
        }
        private void Update123()
        {
            for (int i = 0; i < 10000; i++)
            {
                labelControl1.BeginInvoke(new ChangeText(() => ChangeTextLabel(i.ToString())));
                Thread.Sleep(2000);
            }
        }
    }
}