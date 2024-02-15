namespace Library.Other
{
    partial class frmHuyen
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
            this.txtTenHuyen = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpTinh = new DevExpress.XtraEditors.LookUpEdit();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenHuyen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpTinh.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTenHuyen
            // 
            this.txtTenHuyen.Location = new System.Drawing.Point(126, 17);
            this.txtTenHuyen.Name = "txtTenHuyen";
            this.txtTenHuyen.Size = new System.Drawing.Size(185, 20);
            this.txtTenHuyen.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(48, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 13);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Tên huyện (*):";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(48, 46);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(64, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Tỉnh (TP) (*):";
            // 
            // lookUpTinh
            // 
            this.lookUpTinh.Location = new System.Drawing.Point(126, 43);
            this.lookUpTinh.Name = "lookUpTinh";
            this.lookUpTinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpTinh.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTinh", "Tên tỉnh")});
            this.lookUpTinh.Properties.DisplayMember = "TenTinh";
            this.lookUpTinh.Properties.NullText = "";
            this.lookUpTinh.Properties.ValueMember = "MaTinh";
            this.lookUpTinh.Size = new System.Drawing.Size(185, 20);
            this.lookUpTinh.TabIndex = 9;
            // 
            // btnDongY
            // 
            //this.btnDongY.Image = global::Library.Properties.Resources.OK;
            this.btnDongY.Location = new System.Drawing.Point(90, 87);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(87, 23);
            this.btnDongY.TabIndex = 2;
            this.btnDongY.Text = "Lưu && Đóng";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            //this.btnHuy.Image = global::Library.Properties.Resources.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(189, 87);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(79, 23);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "Hủy - ESC";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // Huyen_frm
            // 
            this.AcceptButton = this.btnDongY;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(359, 132);
            this.Controls.Add(this.lookUpTinh);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtTenHuyen);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Huyen_frm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Huyện (Quận)";
            this.Load += new System.EventHandler(this.Huong_frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTenHuyen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpTinh.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.TextEdit txtTenHuyen;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lookUpTinh;
    }
}