namespace LandSoftBuilding.Marketing.SMS
{
    partial class frmTemplates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTemplates));
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.btnFields = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtContents = new DevExpress.XtraEditors.MemoEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.txtContents.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageOptions.ImageIndex = 2;
            this.btnHuy.ImageOptions.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(347, 285);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(79, 23);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy - ESC";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDongY
            // 
            this.btnDongY.ImageOptions.ImageIndex = 1;
            this.btnDongY.ImageOptions.ImageList = this.imageCollection1;
            this.btnDongY.Location = new System.Drawing.Point(250, 285);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(91, 23);
            this.btnDongY.TabIndex = 4;
            this.btnDongY.Text = "Lưu && Đóng";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // btnFields
            // 
            this.btnFields.ImageOptions.ImageIndex = 0;
            this.btnFields.ImageOptions.ImageList = this.imageCollection1;
            this.btnFields.Location = new System.Drawing.Point(153, 285);
            this.btnFields.Name = "btnFields";
            this.btnFields.Size = new System.Drawing.Size(91, 23);
            this.btnFields.TabIndex = 4;
            this.btnFields.Text = "Trường trộn";
            this.btnFields.Click += new System.EventHandler(this.btnFields_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 17);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(63, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Nội dung (*):";
            // 
            // txtContents
            // 
            this.txtContents.Location = new System.Drawing.Point(14, 36);
            this.txtContents.Name = "txtContents";
            this.txtContents.Size = new System.Drawing.Size(383, 211);
            this.txtContents.TabIndex = 2;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtContents);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(414, 267);
            this.panelControl1.TabIndex = 0;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "list2.png");
            this.imageCollection1.Images.SetKeyName(1, "Save1.png");
            this.imageCollection1.Images.SetKeyName(2, "Cancel1.png");
            // 
            // frmTemplates
            // 
            this.AcceptButton = this.btnDongY;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(438, 318);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnFields);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTemplates";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mẫu SMS";
            this.Load += new System.EventHandler(this.frmTemplates_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtContents.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.SimpleButton btnFields;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoEdit txtContents;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}