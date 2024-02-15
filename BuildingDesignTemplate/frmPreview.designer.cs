namespace BuildingDesignTemplate
{
    partial class FrmPreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richEditControl1 = new DevExpress.XtraRichEdit.RichEditControl();
            this.SuspendLayout();
            // 
            // richEditControl1
            // 
            this.richEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditControl1.EnableToolTips = true;
            this.richEditControl1.Location = new System.Drawing.Point(0, 0);
            this.richEditControl1.Name = "richEditControl1";
            //this.richEditControl1.Options.
            //this.richEditControl1.Options.Behavior.Copy = DevExpress.XtraRichEdit.DocumentCapability.Hidden;
            //this.richEditControl1.Options.Behavior.CreateNew = DevExpress.XtraRichEdit.DocumentCapability.Hidden;
            //this.richEditControl1.Options.Behavior.Cut = DevExpress.XtraRichEdit.DocumentCapability.Hidden;
            //this.richEditControl1.Options.Behavior.Drag = DevExpress.XtraRichEdit.DocumentCapability.Hidden;
            //this.richEditControl1.Options.Behavior.Drop = DevExpress.XtraRichEdit.DocumentCapability.Hidden;
            //this.richEditControl1.Options.Behavior.Open = DevExpress.XtraRichEdit.DocumentCapability.Hidden;
            //this.richEditControl1.Options.Behavior.Paste = DevExpress.XtraRichEdit.DocumentCapability.Hidden;
            //this.richEditControl1.Options.Behavior.Printing = DevExpress.XtraRichEdit.DocumentCapability.Hidden;
            //this.richEditControl1.Options.Behavior.Save = DevExpress.XtraRichEdit.DocumentCapability.Hidden;
            //this.richEditControl1.Options.Behavior.SaveAs = DevExpress.XtraRichEdit.DocumentCapability.Hidden;
            //this.richEditControl1.Options.Behavior.ShowPopupMenu = DevExpress.XtraRichEdit.DocumentCapability.Hidden;
            //this.richEditControl1.Options.CopyPaste.MaintainDocumentSectionSettings = false;
            //this.richEditControl1.Options.Fields.UseCurrentCultureDateTimeFormat = false;
            //this.richEditControl1.Options.MailMerge.KeepLastParagraph = false;
            this.richEditControl1.ReadOnly = true;
            this.richEditControl1.Size = new System.Drawing.Size(1179, 562);
            this.richEditControl1.TabIndex = 3;
          //  this.richEditControl1.Views.SimpleView.Padding = new System.Windows.Forms.Padding(0);
            // 
            // frmPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 562);
            this.Controls.Add(this.richEditControl1);
            this.Name = "FrmPreview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem biểu mẫu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraRichEdit.RichEditControl richEditControl1;
    }
}