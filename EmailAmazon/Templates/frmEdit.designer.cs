namespace EmailAmazon.Templates
{
    partial class frmEdit
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.htmlContent = new MSDN.Html.Editor.HtmlEditorControl();
            this.txtTempName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.btnFields = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTempName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.htmlContent);
            this.panelControl1.Controls.Add(this.txtTempName);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(743, 405);
            this.panelControl1.TabIndex = 0;
            // 
            // htmlContent
            // 
            this.htmlContent.InnerText = null;
            this.htmlContent.Location = new System.Drawing.Point(14, 43);
            this.htmlContent.Name = "htmlContent";
            this.htmlContent.Size = new System.Drawing.Size(714, 348);
            this.htmlContent.TabIndex = 4;
            this.htmlContent.ToolbarDock = System.Windows.Forms.DockStyle.Top;
            // 
            // txtTempName
            // 
            this.txtTempName.Location = new System.Drawing.Point(82, 17);
            this.txtTempName.Name = "txtTempName";
            this.txtTempName.Size = new System.Drawing.Size(646, 20);
            this.txtTempName.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên mẫu (*):";
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(676, 423);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(79, 23);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy - ESC";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDongY
            // 
            this.btnDongY.Location = new System.Drawing.Point(579, 423);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(91, 23);
            this.btnDongY.TabIndex = 4;
            this.btnDongY.Text = "Lưu && Đóng";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // btnFields
            // 
            this.btnFields.Location = new System.Drawing.Point(482, 423);
            this.btnFields.Name = "btnFields";
            this.btnFields.Size = new System.Drawing.Size(91, 23);
            this.btnFields.TabIndex = 4;
            this.btnFields.Text = "Trường trộn";
            this.btnFields.Visible = false;
            this.btnFields.Click += new System.EventHandler(this.btnFields_Click);
            // 
            // frmEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 458);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnFields);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mẫu mail";
            this.Load += new System.EventHandler(this.frmTemplates_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTempName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtTempName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.SimpleButton btnFields;
        private MSDN.Html.Editor.HtmlEditorControl htmlContent;
    }
}