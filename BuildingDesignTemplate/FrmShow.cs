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
using System.IO;

namespace BuildingDesignTemplate
{
	public partial class FrmShow : DevExpress.XtraBars.Ribbon.RibbonForm
	{
        public string RtfText { get; set; }
        public int PaddingTop { get; set; }
        public int PaddingBottom { get; set; }
        public int PaddingLeft { get; set; }
        public int PaddingRight { get; set; }
        public DevExpress.XtraRichEdit.RichEditControl RichControl { get; set; }

        public FrmShow()
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
            txtContent.RtfText = RtfText;
        }

        private void itemField_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}