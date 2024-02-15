namespace DIPCRM.NhuCau
{
    partial class frmAddDoiThu
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnOrganization = new DevExpress.XtraEditors.ButtonEdit();
            this.txtNhuocDiem = new DevExpress.XtraEditors.MemoEdit();
            this.txtTenDoiTac = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblTen = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtUuDiem = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOrganization.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNhuocDiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDoiTac.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUuDiem.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnOrganization);
            this.panelControl1.Controls.Add(this.txtUuDiem);
            this.panelControl1.Controls.Add(this.txtNhuocDiem);
            this.panelControl1.Controls.Add(this.txtTenDoiTac);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.lblTen);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(511, 303);
            this.panelControl1.TabIndex = 0;
            // 
            // btnOrganization
            // 
            this.btnOrganization.Location = new System.Drawing.Point(17, 25);
            this.btnOrganization.Name = "btnOrganization";
            this.btnOrganization.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Chọn", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.btnOrganization.Properties.ReadOnly = true;
            this.btnOrganization.Size = new System.Drawing.Size(134, 20);
            this.btnOrganization.TabIndex = 0;
            this.btnOrganization.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnProduct_ButtonClick);
            // 
            // txtNhuocDiem
            // 
            this.txtNhuocDiem.Location = new System.Drawing.Point(17, 191);
            this.txtNhuocDiem.Name = "txtNhuocDiem";
            this.txtNhuocDiem.Size = new System.Drawing.Size(476, 96);
            this.txtNhuocDiem.TabIndex = 3;
            // 
            // txtTenDoiTac
            // 
            this.txtTenDoiTac.Location = new System.Drawing.Point(157, 25);
            this.txtTenDoiTac.Name = "txtTenDoiTac";
            this.txtTenDoiTac.Properties.ReadOnly = true;
            this.txtTenDoiTac.Size = new System.Drawing.Size(336, 20);
            this.txtTenDoiTac.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(17, 51);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(39, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Ưu điểm";
            // 
            // lblTen
            // 
            this.lblTen.Location = new System.Drawing.Point(157, 6);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(35, 13);
            this.lblTen.TabIndex = 7;
            this.lblTen.Text = "Đối thủ";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(17, 172);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(56, 13);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Nhược điểm";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 13);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "Ký hiệu (*)";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::DIPCRM.Need.Properties.Resources.Luu;
            this.btnSave.Location = new System.Drawing.Point(367, 321);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::DIPCRM.Need.Properties.Resources.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(448, 321);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtUuDiem
            // 
            this.txtUuDiem.Location = new System.Drawing.Point(17, 70);
            this.txtUuDiem.Name = "txtUuDiem";
            this.txtUuDiem.Size = new System.Drawing.Size(476, 96);
            this.txtUuDiem.TabIndex = 2;
            // 
            // frmAddDoiThu
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(535, 358);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddDoiThu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật <Đối thủ>";
            this.Load += new System.EventHandler(this.frmAddProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOrganization.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNhuocDiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDoiTac.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUuDiem.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.MemoEdit txtNhuocDiem;
        private DevExpress.XtraEditors.TextEdit txtTenDoiTac;
        private DevExpress.XtraEditors.LabelControl lblTen;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ButtonEdit btnOrganization;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.MemoEdit txtUuDiem;
    }
}