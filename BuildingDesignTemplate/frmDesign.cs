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
	public partial class FrmDesign : DevExpress.XtraBars.Ribbon.RibbonForm
	{
        public string RtfText { get; set; }
        public MemoryStream stream { get; set; }
        public byte? GroupId { get; set; }

        private FrmField _frmField;

        public FrmDesign()
        {
            InitializeComponent();
            //TranslateLanguage.TranslateControl(this,barManager1);
            _frmField = new FrmField();

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
            if (GroupId != null) _frmField.GroupId = GroupId;
        }

        private void itemField_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (!_frmField.Visible)
                    _frmField.Show(this);
                else
                    _frmField.Opacity = 1;
            }
            catch
            {
                // ignored
            }
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