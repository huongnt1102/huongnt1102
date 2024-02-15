using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Windows.Forms;

namespace LandSoftBuilding.Lease.Mau.Edit
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        HDTTemplate objTemp;
        public int ID;
        MasterDataContext db = new MasterDataContext();

        public frmEdit()
        {
            InitializeComponent();
            //MailCommon.cmd = new APISoapClient();
            //MailCommon.cmd.Open();
            TranslateLanguage.TranslateControl(this);
        }
        public int MaBM { get; set; }

        private void btnsave_Click(object sender, EventArgs e)
        {
            objTemp.TieuDe = btnten.Text;
            objTemp.MaNVS = Common.User.MaNV;
            objTemp.NgaySua = DateTime.Now;
            if (isCongNo.Checked == true)
                objTemp.IsCongNo = true;
            else
                objTemp.IsCongNo = false;
            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCreateNew_Load(object sender, EventArgs e)
        {
            objTemp = db.HDTTemplates.Single(p => p.ID == this.ID);

            isCongNo.Checked = objTemp.IsCongNo == true ? true : false;
            btnten.Text = objTemp.TieuDe;
        }

        private void btnten_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                using (var frm = new frmDesign())
                {
                    frm.RtfText = objTemp.NoiDung;
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                        objTemp.NoiDung = frm.RtfText;
                }
            }
        }
    }
}