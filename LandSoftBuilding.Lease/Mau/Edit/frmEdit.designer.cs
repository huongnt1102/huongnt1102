namespace LandSoftBuilding.Lease.Mau.Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEdit));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.btnsave = new DevExpress.XtraEditors.SimpleButton();
            this.btnten = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gcNew = new DevExpress.XtraEditors.GroupControl();
            this.btncancel = new DevExpress.XtraEditors.SimpleButton();
            this.isCongNo = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.btnten.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNew)).BeginInit();
            this.gcNew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.isCongNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnsave
            // 
            this.btnsave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnsave.ImageOptions.Image")));
            this.btnsave.Location = new System.Drawing.Point(218, 54);
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
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Sửa mẫu", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnten.Size = new System.Drawing.Size(285, 20);
            this.btnten.TabIndex = 0;
            this.btnten.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnten_ButtonClick);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 31);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Tên biểu mẫu:";
            // 
            // gcNew
            // 
            this.gcNew.Controls.Add(this.isCongNo);
            this.gcNew.Controls.Add(this.btncancel);
            this.gcNew.Controls.Add(this.btnsave);
            this.gcNew.Controls.Add(this.labelControl1);
            this.gcNew.Controls.Add(this.btnten);
            this.gcNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNew.Location = new System.Drawing.Point(0, 0);
            this.gcNew.Name = "gcNew";
            this.gcNew.Size = new System.Drawing.Size(386, 82);
            this.gcNew.TabIndex = 3;
            this.gcNew.Text = "Thông tin";
            // 
            // btncancel
            // 
            this.btncancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btncancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btncancel.ImageOptions.Image")));
            this.btncancel.Location = new System.Drawing.Point(299, 54);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 23);
            this.btncancel.TabIndex = 3;
            this.btncancel.Text = "Thoát";
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // isCongNo
            // 
            this.isCongNo.Location = new System.Drawing.Point(89, 58);
            this.isCongNo.Name = "isCongNo";
            this.isCongNo.Properties.Caption = "In công nợ";
            this.isCongNo.Size = new System.Drawing.Size(75, 19);
            this.isCongNo.TabIndex = 5;
            // 
            // frmEdit
            // 
            this.AcceptButton = this.btnsave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btncancel;
            this.ClientSize = new System.Drawing.Size(386, 82);
            this.Controls.Add(this.gcNew);
            this.Name = "frmEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Biễu mẫu";
            this.Load += new System.EventHandler(this.frmCreateNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnten.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNew)).EndInit();
            this.gcNew.ResumeLayout(false);
            this.gcNew.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.isCongNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnsave;
        private DevExpress.XtraEditors.ButtonEdit btnten;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl gcNew;
        private DevExpress.XtraEditors.SimpleButton btncancel;
        private DevExpress.XtraEditors.CheckEdit isCongNo;
    }
}