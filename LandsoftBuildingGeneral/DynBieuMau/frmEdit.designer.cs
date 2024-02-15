namespace LandsoftBuildingGeneral.DynBieuMau
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ckbKhoa = new DevExpress.XtraEditors.CheckEdit();
            this.txtTenBM = new DevExpress.XtraEditors.ButtonEdit();
            this.lookLoaiBM = new DevExpress.XtraEditors.LookUpEdit();
            this.txtDienGiai = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckbKhoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenBM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoaiBM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ckbKhoa);
            this.panelControl1.Controls.Add(this.txtTenBM);
            this.panelControl1.Controls.Add(this.lookLoaiBM);
            this.panelControl1.Controls.Add(this.txtDienGiai);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(484, 152);
            this.panelControl1.TabIndex = 0;
            // 
            // ckbKhoa
            // 
            this.ckbKhoa.Location = new System.Drawing.Point(94, 123);
            this.ckbKhoa.Name = "ckbKhoa";
            this.ckbKhoa.Properties.Caption = "Khóa biểu mẫu";
            this.ckbKhoa.Size = new System.Drawing.Size(104, 19);
            this.ckbKhoa.TabIndex = 5;
            // 
            // txtTenBM
            // 
            this.txtTenBM.Location = new System.Drawing.Point(95, 13);
            this.txtTenBM.Name = "txtTenBM";
            this.txtTenBM.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Biểu mẫu", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.txtTenBM.Size = new System.Drawing.Size(373, 20);
            this.txtTenBM.TabIndex = 4;
            this.txtTenBM.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtTenBM_ButtonClick);
            // 
            // lookLoaiBM
            // 
            this.lookLoaiBM.Location = new System.Drawing.Point(95, 39);
            this.lookLoaiBM.Name = "lookLoaiBM";
            this.lookLoaiBM.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookLoaiBM.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLBM", "Name1")});
            this.lookLoaiBM.Properties.DisplayMember = "TenLBM";
            this.lookLoaiBM.Properties.NullText = "";
            this.lookLoaiBM.Properties.ShowHeader = false;
            this.lookLoaiBM.Properties.ValueMember = "MaLBM";
            this.lookLoaiBM.Size = new System.Drawing.Size(373, 20);
            this.lookLoaiBM.TabIndex = 3;
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Location = new System.Drawing.Point(94, 65);
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(374, 51);
            this.txtDienGiai.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Diễn giải:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(16, 42);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(69, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Loại biểu mẫu:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên biểu mẫu:";
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(319, 170);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(96, 26);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu && Đóng";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(421, 170);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 26);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 205);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Biểu mẫu";
            this.Load += new System.EventHandler(this.frmEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckbKhoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenBM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoaiBM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtDienGiai;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ButtonEdit txtTenBM;
        private DevExpress.XtraEditors.LookUpEdit lookLoaiBM;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit ckbKhoa;
    }
}