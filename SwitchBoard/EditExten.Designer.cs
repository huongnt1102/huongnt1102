namespace DIP.SwitchBoard
{
    partial class EditExten
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
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtExtenName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtPass = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDisplay = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.spPort = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lkStaff = new DevExpress.XtraEditors.LookUpEdit();
            this.btClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtExtenName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDisplay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkStaff.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(175, 139);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(98, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu && Đóng";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtExtenName
            // 
            this.txtExtenName.Location = new System.Drawing.Point(93, 12);
            this.txtExtenName.Name = "txtExtenName";
            this.txtExtenName.Size = new System.Drawing.Size(261, 20);
            this.txtExtenName.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(74, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Tên máy nhánh";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(93, 38);
            this.txtPass.Name = "txtPass";
            this.txtPass.Properties.UseSystemPasswordChar = true;
            this.txtPass.Size = new System.Drawing.Size(261, 20);
            this.txtPass.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Mật khẩu";
            // 
            // txtDisplay
            // 
            this.txtDisplay.Location = new System.Drawing.Point(93, 87);
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Size = new System.Drawing.Size(261, 20);
            this.txtDisplay.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 90);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(56, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Tên hiển thị";
            // 
            // spPort
            // 
            this.spPort.EditValue = new decimal(new int[] {
            5060,
            0,
            0,
            0});
            this.spPort.Location = new System.Drawing.Point(93, 64);
            this.spPort.Name = "spPort";
            this.spPort.Properties.Appearance.Options.UseTextOptions = true;
            this.spPort.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.spPort.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spPort.Properties.IsFloatValue = false;
            this.spPort.Properties.Mask.EditMask = "N00";
            this.spPort.Size = new System.Drawing.Size(261, 20);
            this.spPort.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(13, 67);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(25, 13);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "Cổng";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(13, 116);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 13);
            this.labelControl5.TabIndex = 2;
            this.labelControl5.Text = "Nhân viên";
            // 
            // lkStaff
            // 
            this.lkStaff.Location = new System.Drawing.Point(93, 113);
            this.lkStaff.Name = "lkStaff";
            this.lkStaff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkStaff.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HoTenNV", "Name1")});
            this.lkStaff.Properties.DisplayMember = "HoTenNV";
            this.lkStaff.Properties.NullText = "";
            this.lkStaff.Properties.ShowHeader = false;
            this.lkStaff.Properties.ValueMember = "MaNV";
            this.lkStaff.Size = new System.Drawing.Size(262, 20);
            this.lkStaff.TabIndex = 4;
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(279, 139);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 0;
            this.btClose.Text = "Hủy";
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // EditExten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 174);
            this.Controls.Add(this.lkStaff);
            this.Controls.Add(this.spPort);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtExtenName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "EditExten";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Máy nhánh";
            this.Load += new System.EventHandler(this.EditExten_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtExtenName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDisplay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkStaff.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtExtenName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtPass;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtDisplay;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SpinEdit spPort;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LookUpEdit lkStaff;
        private DevExpress.XtraEditors.SimpleButton btClose;
    }
}