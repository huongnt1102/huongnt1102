namespace Building.AppVime.ServiceExtension
{
    partial class frmProcess
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
            this.btnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblRequire2 = new DevExpress.XtraEditors.LabelControl();
            this.lblRequire = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtContents = new DevExpress.XtraEditors.MemoEdit();
            this.lookUpDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEmployee = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpStatus = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContents.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpStatus.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Save_as_icon;
            this.btnAccept.Location = new System.Drawing.Point(399, 236);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(91, 23);
            this.btnAccept.TabIndex = 5;
            this.btnAccept.Text = "Lưu && Đóng";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Delete_icon;
            this.btnCancel.Location = new System.Drawing.Point(496, 236);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblRequire2);
            this.panelControl1.Controls.Add(this.lblRequire);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtContents);
            this.panelControl1.Controls.Add(this.lookUpDepartment);
            this.panelControl1.Controls.Add(this.lookUpEmployee);
            this.panelControl1.Controls.Add(this.lookUpStatus);
            this.panelControl1.Location = new System.Drawing.Point(10, 11);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(561, 220);
            this.panelControl1.TabIndex = 3;
            // 
            // lblRequire2
            // 
            this.lblRequire2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblRequire2.Appearance.Options.UseForeColor = true;
            this.lblRequire2.Location = new System.Drawing.Point(360, 41);
            this.lblRequire2.Name = "lblRequire2";
            this.lblRequire2.Size = new System.Drawing.Size(14, 13);
            this.lblRequire2.TabIndex = 2;
            this.lblRequire2.Text = "(*)";
            // 
            // lblRequire
            // 
            this.lblRequire.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblRequire.Appearance.Options.UseForeColor = true;
            this.lblRequire.Location = new System.Drawing.Point(64, 41);
            this.lblRequire.Name = "lblRequire";
            this.lblRequire.Size = new System.Drawing.Size(14, 13);
            this.lblRequire.TabIndex = 2;
            this.lblRequire.Text = "(*)";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(9, 41);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(55, 13);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "Phòng ban:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(306, 41);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Nhân viên:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(9, 66);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Diễn giải:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Trạng thái:";
            // 
            // txtContents
            // 
            this.txtContents.EditValue = "Tiếp nhận";
            this.txtContents.Location = new System.Drawing.Point(82, 63);
            this.txtContents.Name = "txtContents";
            this.txtContents.Properties.MaxLength = 1500;
            this.txtContents.Size = new System.Drawing.Size(463, 141);
            this.txtContents.TabIndex = 1;
            // 
            // lookUpDepartment
            // 
            this.lookUpDepartment.Location = new System.Drawing.Point(82, 39);
            this.lookUpDepartment.Name = "lookUpDepartment";
            this.lookUpDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpDepartment.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name1")});
            this.lookUpDepartment.Properties.DisplayMember = "Name";
            this.lookUpDepartment.Properties.NullText = "";
            this.lookUpDepartment.Properties.ShowHeader = false;
            this.lookUpDepartment.Properties.ValueMember = "Id";
            this.lookUpDepartment.Size = new System.Drawing.Size(157, 20);
            this.lookUpDepartment.TabIndex = 0;
            this.lookUpDepartment.EditValueChanged += new System.EventHandler(this.lookUpDepartment_EditValueChanged);
            // 
            // lookUpEmployee
            // 
            this.lookUpEmployee.Location = new System.Drawing.Point(378, 39);
            this.lookUpEmployee.Name = "lookUpEmployee";
            this.lookUpEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEmployee.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name1")});
            this.lookUpEmployee.Properties.DisplayMember = "Name";
            this.lookUpEmployee.Properties.NullText = "";
            this.lookUpEmployee.Properties.ShowHeader = false;
            this.lookUpEmployee.Properties.ValueMember = "Id";
            this.lookUpEmployee.Size = new System.Drawing.Size(167, 20);
            this.lookUpEmployee.TabIndex = 0;
            // 
            // lookUpStatus
            // 
            this.lookUpStatus.Location = new System.Drawing.Point(82, 15);
            this.lookUpStatus.Name = "lookUpStatus";
            this.lookUpStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name1")});
            this.lookUpStatus.Properties.DisplayMember = "Name";
            this.lookUpStatus.Properties.NullText = "";
            this.lookUpStatus.Properties.ShowHeader = false;
            this.lookUpStatus.Properties.ValueMember = "Id";
            this.lookUpStatus.Size = new System.Drawing.Size(463, 20);
            this.lookUpStatus.TabIndex = 0;
            // 
            // frmProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 271);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProcess";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tiếp nhận";
            this.Load += new System.EventHandler(this.frmProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContents.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpStatus.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnAccept;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtContents;
        private DevExpress.XtraEditors.LookUpEdit lookUpStatus;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lookUpEmployee;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lookUpDepartment;
        private DevExpress.XtraEditors.LabelControl lblRequire2;
        private DevExpress.XtraEditors.LabelControl lblRequire;
    }
}