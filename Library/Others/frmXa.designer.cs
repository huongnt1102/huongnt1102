namespace Library.Other
{
    partial class frmXa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmXa));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.txtTenXa = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpTinh = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpHuyen = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenXa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpTinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpHuyen.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            //this.btnHuy.Image = global::Library.Properties.Resources.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(188, 111);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(79, 23);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "Hủy - ESC";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDongY
            // 
            //this.btnDongY.Image = global::Library.Properties.Resources.OK;
            this.btnDongY.Location = new System.Drawing.Point(91, 111);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(90, 23);
            this.btnDongY.TabIndex = 2;
            this.btnDongY.Text = "Lưu && Đóng";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // txtTenXa
            // 
            this.txtTenXa.Location = new System.Drawing.Point(123, 12);
            this.txtTenXa.Name = "txtTenXa";
            this.txtTenXa.Size = new System.Drawing.Size(188, 20);
            this.txtTenXa.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(32, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(85, 13);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Tên xã (Phường):";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(32, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(47, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Tỉnh (TP):";
            // 
            // lookUpTinh
            // 
            this.lookUpTinh.Location = new System.Drawing.Point(123, 38);
            this.lookUpTinh.Name = "lookUpTinh";
            this.lookUpTinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("lookUpTinh.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.lookUpTinh.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTinh", "Tên tỉnh")});
            this.lookUpTinh.Properties.DisplayMember = "TenTinh";
            this.lookUpTinh.Properties.NullText = "";
            this.lookUpTinh.Properties.ValueMember = "MaTinh";
            this.lookUpTinh.Size = new System.Drawing.Size(188, 22);
            this.lookUpTinh.TabIndex = 9;
            this.lookUpTinh.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lookUpTinh_ButtonClick);
            this.lookUpTinh.EditValueChanged += new System.EventHandler(this.lookUpTinh_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(31, 67);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Huyện (Quận):";
            // 
            // lookUpHuyen
            // 
            this.lookUpHuyen.Location = new System.Drawing.Point(123, 64);
            this.lookUpHuyen.Name = "lookUpHuyen";
            this.lookUpHuyen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("lookUpHuyen.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.lookUpHuyen.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenHuyen", "Tên huyện")});
            this.lookUpHuyen.Properties.DisplayMember = "TenHuyen";
            this.lookUpHuyen.Properties.NullText = "";
            this.lookUpHuyen.Properties.ValueMember = "MaHuyen";
            this.lookUpHuyen.Size = new System.Drawing.Size(187, 22);
            this.lookUpHuyen.TabIndex = 9;
            this.lookUpHuyen.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lookUpHuyen_ButtonClick);
            // 
            // Xa_frm
            // 
            this.AcceptButton = this.btnDongY;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(359, 146);
            this.Controls.Add(this.lookUpHuyen);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lookUpTinh);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtTenXa);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Xa_frm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xã (Phường)";
            this.Load += new System.EventHandler(this.Huong_frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTenXa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpTinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpHuyen.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.TextEdit txtTenXa;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lookUpTinh;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lookUpHuyen;
    }
}