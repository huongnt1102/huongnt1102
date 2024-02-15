namespace LandsoftBuildingGeneral.BieuMau
{
    partial class frmCreateNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateNew));
            this.btnsave = new DevExpress.XtraEditors.SimpleButton();
            this.btnten = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gcNew = new DevExpress.XtraEditors.GroupControl();
            this.btncancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnGhiChu = new DevExpress.XtraEditors.ButtonEdit();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.btnten.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNew)).BeginInit();
            this.gcNew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnGhiChu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnsave
            // 
            this.btnsave.ImageOptions.ImageIndex = 0;
            this.btnsave.ImageOptions.ImageList = this.imageCollection1;
            this.btnsave.Location = new System.Drawing.Point(218, 80);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(75, 23);
            this.btnsave.TabIndex = 2;
            this.btnsave.Text = "Lưu lại";
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnten
            // 
            this.btnten.Location = new System.Drawing.Point(89, 28);
            this.btnten.Name = "btnten";
            this.btnten.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnten.Size = new System.Drawing.Size(285, 20);
            this.btnten.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Tên biểu mẫu:";
            // 
            // gcNew
            // 
            this.gcNew.Controls.Add(this.btncancel);
            this.gcNew.Controls.Add(this.btnsave);
            this.gcNew.Controls.Add(this.labelControl2);
            this.gcNew.Controls.Add(this.labelControl1);
            this.gcNew.Controls.Add(this.btnGhiChu);
            this.gcNew.Controls.Add(this.btnten);
            this.gcNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNew.Location = new System.Drawing.Point(0, 0);
            this.gcNew.Name = "gcNew";
            this.gcNew.Size = new System.Drawing.Size(386, 111);
            this.gcNew.TabIndex = 3;
            this.gcNew.Text = "Thông tin";
            // 
            // btncancel
            // 
            this.btncancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btncancel.ImageOptions.ImageIndex = 1;
            this.btncancel.ImageOptions.ImageList = this.imageCollection1;
            this.btncancel.Location = new System.Drawing.Point(299, 80);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 23);
            this.btncancel.TabIndex = 3;
            this.btncancel.Text = "Thoát";
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 57);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(39, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Ghi chú:";
            // 
            // btnGhiChu
            // 
            this.btnGhiChu.Location = new System.Drawing.Point(89, 54);
            this.btnGhiChu.Name = "btnGhiChu";
            this.btnGhiChu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnGhiChu.Size = new System.Drawing.Size(285, 20);
            this.btnGhiChu.TabIndex = 1;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            // 
            // frmCreateNew
            // 
            this.AcceptButton = this.btnsave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btncancel;
            this.ClientSize = new System.Drawing.Size(386, 111);
            this.Controls.Add(this.gcNew);
            this.Name = "frmCreateNew";
            this.ShowInTaskbar = false;
            this.Text = "Biễu mẫu";
            this.Load += new System.EventHandler(this.frmCreateNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnten.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNew)).EndInit();
            this.gcNew.ResumeLayout(false);
            this.gcNew.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnGhiChu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnsave;
        private DevExpress.XtraEditors.ButtonEdit btnten;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl gcNew;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ButtonEdit btnGhiChu;
        private DevExpress.XtraEditors.SimpleButton btncancel;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}