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
using System.Linq;
using LandSoftBuilding.Lease.Mau;

namespace LandSoftBuilding.Lease.Mau
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int TempID;
        public byte? MaTN;
        HDTTemplate objTemp;
        MasterDataContext db = new MasterDataContext();

        public frmEdit()
        {
            InitializeComponent();
        }


        public void InsertText(string text)
        {
        }

        private void frmTemplates_Load(object sender, EventArgs e)
        {
            objTemp = db.HDTTemplates.FirstOrDefault(p => p.ID == this.TempID);

            if (objTemp == null)
            {
                objTemp = new HDTTemplate();
                objTemp.MaNVN = Common.User.MaNV;
                objTemp.NgayNhap = DateTime.Now;
                objTemp.MaTN = this.MaTN;
                db.HDTTemplates.InsertOnSubmit(objTemp);
            }
            else
            {
                objTemp.MaNVS = Common.User.MaNV;
                objTemp.NgaySua = DateTime.Now;
            }

			txtTieuDe.Text = objTemp.TieuDe;
        }

        private void btnFields_Click(object sender, EventArgs e)
        {
            var frm = new frmField_AddNew();
            frm.Show(this);
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            objTemp.TieuDe = txtTieuDe.Text;
            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void txtTieuDe_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if(e.Button.Index == 0)
			{
				using (LandSoftBuilding.Lease.Mau.frmDesign frm = new LandSoftBuilding.Lease.Mau.frmDesign())
				{

					using (var db = new MasterDataContext())
					{
						frm.RtfText = objTemp.NoiDung;

						if (frm.ShowDialog() == DialogResult.OK)
							objTemp.NoiDung = frm.RtfText;
					}
				}
			}
		}
	}
}