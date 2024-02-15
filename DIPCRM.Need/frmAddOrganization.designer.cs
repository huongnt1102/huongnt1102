namespace DIPCRM.NhuCau
{
    partial class frmAddOrganization
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtVaiTro = new DevExpress.XtraEditors.TextEdit();
            this.btnOrganization = new DevExpress.XtraEditors.ButtonEdit();
            this.txtDienGiai = new DevExpress.XtraEditors.MemoEdit();
            this.txtTenDoiTac = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblTen = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVaiTro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOrganization.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDoiTac.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtVaiTro);
            this.panelControl1.Controls.Add(this.btnOrganization);
            this.panelControl1.Controls.Add(this.txtDienGiai);
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
            // txtVaiTro
            // 
            this.txtVaiTro.Location = new System.Drawing.Point(17, 70);
            this.txtVaiTro.Name = "txtVaiTro";
            this.txtVaiTro.Size = new System.Drawing.Size(476, 20);
            this.txtVaiTro.TabIndex = 2;
            // 
            // btnOrganization
            // 
            this.btnOrganization.Location = new System.Drawing.Point(17, 25);
            this.btnOrganization.Name = "btnOrganization";
            this.btnOrganization.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Chọn", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.btnOrganization.Properties.ReadOnly = true;
            this.btnOrganization.Size = new System.Drawing.Size(134, 20);
            this.btnOrganization.TabIndex = 0;
            this.btnOrganization.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnProduct_ButtonClick);
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Location = new System.Drawing.Point(17, 115);
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(476, 172);
            this.txtDienGiai.TabIndex = 3;
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
            this.labelControl3.Size = new System.Drawing.Size(86, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Vai trò của đối tác";
            // 
            // lblTen
            // 
            this.lblTen.Location = new System.Drawing.Point(157, 6);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(34, 13);
            this.lblTen.TabIndex = 7;
            this.lblTen.Text = "Đối tác";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(17, 96);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(40, 13);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Diễn giải";
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
            // frmAddOrganization
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
            this.Name = "frmAddOrganization";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật <Đối tác>";
            this.Load += new System.EventHandler(this.frmAddProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVaiTro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOrganization.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDoiTac.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.MemoEdit txtDienGiai;
        private DevExpress.XtraEditors.TextEdit txtTenDoiTac;
        private DevExpress.XtraEditors.LabelControl lblTen;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ButtonEdit btnOrganization;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtVaiTro;
    }
}