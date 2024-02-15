namespace Library.Other
{
    partial class frmTinhHuyenXa
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
            this.lookHuyen = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lookTinh = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lookXa = new DevExpress.XtraEditors.LookUpEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lookHuyen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookXa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lookHuyen
            // 
            this.lookHuyen.Location = new System.Drawing.Point(142, 26);
            this.lookHuyen.Name = "lookHuyen";
            this.lookHuyen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookHuyen.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenHuyen", "Tên huyện")});
            this.lookHuyen.Properties.DisplayMember = "TenHuyen";
            this.lookHuyen.Properties.NullText = "";
            this.lookHuyen.Properties.ValueMember = "MaHuyen";
            this.lookHuyen.Size = new System.Drawing.Size(120, 20);
            this.lookHuyen.TabIndex = 12;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(142, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 13);
            this.labelControl3.TabIndex = 11;
            this.labelControl3.Text = "Huyện (Quận):";
            // 
            // lookTinh
            // 
            this.lookTinh.Location = new System.Drawing.Point(16, 26);
            this.lookTinh.Name = "lookTinh";
            this.lookTinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTinh.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTinh", "Tên tỉnh")});
            this.lookTinh.Properties.DisplayMember = "TenTinh";
            this.lookTinh.Properties.NullText = "";
            this.lookTinh.Properties.ValueMember = "MaTinh";
            this.lookTinh.Size = new System.Drawing.Size(120, 20);
            this.lookTinh.TabIndex = 13;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(47, 13);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "Tỉnh (TP):";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(268, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 13);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "Xã (Phường):";
            // 
            // lookXa
            // 
            this.lookXa.Location = new System.Drawing.Point(268, 26);
            this.lookXa.Name = "lookXa";
            this.lookXa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookXa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenXa", "Tên xã")});
            this.lookXa.Properties.DisplayMember = "TenXa";
            this.lookXa.Properties.NullText = "";
            this.lookXa.Properties.ValueMember = "MaXa";
            this.lookXa.Size = new System.Drawing.Size(120, 20);
            this.lookXa.TabIndex = 12;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lookXa);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.lookTinh);
            this.panelControl1.Controls.Add(this.lookHuyen);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(405, 59);
            this.panelControl1.TabIndex = 14;
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            //this.btnHuy.Image = global::Library.Properties.Resources.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(338, 77);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(79, 23);
            this.btnHuy.TabIndex = 16;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDongY
            // 
            //this.btnDongY.Image = global::Library.Properties.Resources.OK;
            this.btnDongY.Location = new System.Drawing.Point(236, 77);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(95, 23);
            this.btnDongY.TabIndex = 15;
            this.btnDongY.Text = "Chọn && Đóng";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // frmTinhHuyenXa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 110);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTinhHuyenXa";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn vị trí";
            ((System.ComponentModel.ISupportInitialize)(this.lookHuyen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookXa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit lookHuyen;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lookTinh;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookXa;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
    }
}