using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraPrinting;
//using Sunland.Class;

namespace LandSoft.DuAn.BieuMau
{
    public partial class frmDesign : DevExpress.XtraEditors.XtraForm
    {
        public string RtfText { get; set; }

        frmFieldV2 frmField;
        public frmDesign()
        {
            InitializeComponent();

            frmField = new frmFieldV2();

            itemLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemLuu_ItemClick);
            itemDong.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDong_ItemClick);
            this.Load += new EventHandler(frmDesign_Load);
        }

        public void InsertText(string text)
        {
            try
            {
                //txtContent.Document
                //txtContent.Document.Cut();
                //txtContent.Document.InsertText(txtContent.Document.CaretPosition, text);
            }
            catch
            {
            }
        }

        private void frmDesign_Load(object sender, EventArgs e)
        {
            txtContent.RtfText = this.RtfText;

           // LandSoft.Translate.Language.TranslateControl(this, barManager1);          
        }

        private void itemField_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (!frmField.Visible)
                    frmField.Show(this);
                else
                    frmField.Opacity = 1;
            }
            catch { }
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtContent.RtfText.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập nội dung");
                return;
            }

            try
            {
                this.RtfText = txtContent.RtfText;
            }
            catch { this.RtfText = ""; }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void frmDesign_Activated(object sender, EventArgs e)
        {
            try
            {
                frmField.Opacity = .2;
            }
            catch { }
        }

        private void frmDesign_Deactivate(object sender, EventArgs e)
        {
            try
            {
                frmField.Opacity = 1;
            }
            catch { }
        }

        private void frmDesign_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmField.Dispose();
            }
            catch { }
        }
    }
}