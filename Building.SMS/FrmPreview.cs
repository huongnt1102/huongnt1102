using System;
using System.IO;
using DevExpress.XtraRichEdit.API.Native;

namespace Building.SMS
{
    public partial class FrmPreview : DevExpress.XtraEditors.XtraForm
    {
        public string RtfText { get; set; }
        //public int PaddingTop { get; set; }
        //public int PaddingBottom { get; set; }
        //public int PaddingLeft { get; set; }
        //public int PaddingRight { get; set; }
        public MemoryStream stream { get; set; }

        public FrmPreview()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmPreview_Load);
        }

        private void frmPreview_Load(object sender, EventArgs e)
        {
            richEditControl1.Text = this.Text;
           // richEditControl1.Views.SimpleView.Padding = new Padding(this.PaddingLeft, this.PaddingTop, this.PaddingRight, this.PaddingBottom);
           var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
           ctlRTF.RtfText = richEditControl1.RtfText;
           //ctlRTF.Document.ReplaceAll("[BuildingName]", "aaa", SearchOptions.None);

           richEditControl1.RtfText = ctlRTF.RtfText;
           //using (var stream = new MemoryStream())
           //{
           //    richEditControl1.SaveDocument(stream, DevExpress.XtraRichEdit.DocumentFormat.PlainText);
           //    byte[] buffer = new byte[stream.Length];
           //    stream.Seek(0, SeekOrigin.Begin);                  // <- Add this line
           //    stream.Read(buffer, 0, (int)stream.Length);
               
           //}

           stream = new MemoryStream();
           richEditControl1.SaveDocument(stream, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
           byte[] buffer = new byte[stream.Length];
           stream.Seek(0, SeekOrigin.Begin);                  // <- Add this line
           stream.Read(buffer, 0, (int)stream.Length);

        }
        
    }
}