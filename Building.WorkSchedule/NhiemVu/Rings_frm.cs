using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace Building.WorkSchedule.NhiemVu
{
    public partial class Rings_frm : DevExpress.XtraEditors.XtraForm
    {
        public string Rings = "";
        public bool IsUpdate = false;
        public Rings_frm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (gvRings.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn nhac chuông, xin cảm ơn.");
                return;
            }

            Rings = gvRings.GetFocusedRowCellValue("FileName").ToString();
            IsUpdate = true;
            this.Close();
        }

        private void btnRings_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            
        }

        private void Rings_frm_Shown(object sender, EventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                //gcRings.DataSource = db.Rings;
            }
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if (gvRings.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn nhac chuông, xin cảm ơn.");
                return;
            }

            MusicCls.FileName = Application.StartupPath + "\\Musics\\" + gvRings.GetFocusedRowCellValue("FileName").ToString();
            MusicCls.Play();
        }

        private void Rings_frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MusicCls.Close();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            MusicCls.Close();
        }
    }
}