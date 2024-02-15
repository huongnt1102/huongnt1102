using System;
using System.IO;
using System.Windows.Forms;
using Library;

namespace Building.SMS
{
    public partial class FrmDesign : DevExpress.XtraEditors.XtraForm
    {
        public string TextContents { get; set; }
        public MemoryStream stream { get; set; }

        private FrmField _frmField;
        public FrmDesign()
        {
            InitializeComponent();

            _frmField = new FrmField();

            itemLuu.ItemClick += itemLuu_ItemClick;
            itemDong.ItemClick += itemDong_ItemClick;
            Load += frmDesign_Load;
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
                // ignored
            }
        }

        private void frmDesign_Load(object sender, EventArgs e)
        {
            txtContent.Text = TextContents;

            // LandSoft.Translate.Language.TranslateControl(this, barManager1);        
            
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
            if (txtContent.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập nội dung");
                return;
            }

            try
            {
                TextContents = txtContent.Text;
                //stream = new MemoryStream();
                //txtContent.SaveDocument(stream, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                //byte[] buffer = new byte[stream.Length];
                //stream.Seek(0, SeekOrigin.Begin);                  // <- Add this line
                //stream.Read(buffer, 0, (int)stream.Length);
                stream = new System.IO.MemoryStream();

                txtContent.SaveDocument(stream, DevExpress.XtraRichEdit.DocumentFormat.OpenDocument);
            }
            catch { TextContents = ""; }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmDesign_Activated(object sender, EventArgs e)
        {
            try
            {
                _frmField.Opacity = .2;
            }
            catch
            {
                // ignored
            }
        }

        private void frmDesign_Deactivate(object sender, EventArgs e)
        {
            try
            {
                _frmField.Opacity = 1;
            }
            catch { }
        }

        private void frmDesign_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _frmField.Dispose();
            }
            catch { }
        }
    }
}