using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using Library;
using System.Linq;

namespace LandSoftBuilding.Lease.Mau
{
	public partial class frmDesign : DevExpress.XtraBars.Ribbon.RibbonForm
	{
		public string RtfText { get; set; }
        public int TempID;
        HDTTemplate objTemp;
        MasterDataContext db = new MasterDataContext();

        public frmDesign()
        {
            InitializeComponent();
            //TranslateLanguage.TranslateControl(this,barManager1);

            //itemLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemLuu_ItemClick);
            itemDong.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDong_ItemClick);
            this.Load += new EventHandler(frmDesign_Load);
        }

        public void InsertText(string text)
        {
            try
            {
                txtContent.Document.Cut();
                txtContent.Document.InsertText(txtContent.Document.CaretPosition, text);
            }
            catch {}
        }

        private void frmDesign_Load(object sender, EventArgs e)
        {
            txtContent.RtfText = this.RtfText;
        }

        private void itemField_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmField();
            frm.Show(this);
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtContent.RtfText.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập nội dung");
                return;
            }

            this.RtfText = txtContent.Document.RtfText;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}