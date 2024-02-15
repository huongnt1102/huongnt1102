namespace Building.AppVime.Tower.Department
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
            this.btnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.glEmployee = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpDepartment = new DevExpress.XtraEditors.LookUpEdit();
            this.chkBQL = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsAdmin = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBQL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsAdmin.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Save_as_icon;
            this.btnAccept.Location = new System.Drawing.Point(285, 112);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(91, 23);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "Lưu && Đóng";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Delete_icon;
            this.btnCancel.Location = new System.Drawing.Point(381, 112);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.glEmployee);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.lookUpDepartment);
            this.panelControl1.Controls.Add(this.chkBQL);
            this.panelControl1.Controls.Add(this.chkIsAdmin);
            this.panelControl1.Location = new System.Drawing.Point(10, 11);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(446, 95);
            this.panelControl1.TabIndex = 6;
            // 
            // glEmployee
            // 
            this.glEmployee.Location = new System.Drawing.Point(93, 11);
            this.glEmployee.Name = "glEmployee";
            this.glEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glEmployee.Properties.DisplayMember = "Name";
            this.glEmployee.Properties.NullText = "";
            this.glEmployee.Properties.PopupFormMinSize = new System.Drawing.Size(386, 244);
            this.glEmployee.Properties.PopupView = this.gridLookUpEdit1View;
            this.glEmployee.Properties.ValueMember = "Id";
            this.glEmployee.Size = new System.Drawing.Size(342, 20);
            this.glEmployee.TabIndex = 29;
            this.glEmployee.EditValueChanged += new System.EventHandler(this.glEmployee_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn1,
            this.gridColumn2});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsBehavior.Editable = false;
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Họ và tên";
            this.gridColumn7.FieldName = "Name";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 128;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Điện thoại";
            this.gridColumn1.FieldName = "Phone";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 128;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tòa nhà";
            this.gridColumn2.FieldName = "Tower";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 110;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(55, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Phòng ban:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 14);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Nhân viên:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 66);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Tùy chọn:";
            // 
            // lookUpDepartment
            // 
            this.lookUpDepartment.Location = new System.Drawing.Point(93, 36);
            this.lookUpDepartment.Name = "lookUpDepartment";
            this.lookUpDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpDepartment.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenPB", "Name1")});
            this.lookUpDepartment.Properties.DisplayMember = "TenPB";
            this.lookUpDepartment.Properties.MaxLength = 1500;
            this.lookUpDepartment.Properties.NullText = "";
            this.lookUpDepartment.Properties.ShowHeader = false;
            this.lookUpDepartment.Properties.ValueMember = "MaPB";
            this.lookUpDepartment.Size = new System.Drawing.Size(342, 20);
            this.lookUpDepartment.TabIndex = 0;
            // 
            // chkBQL
            // 
            this.chkBQL.EditValue = null;
            this.chkBQL.Location = new System.Drawing.Point(309, 63);
            this.chkBQL.Name = "chkBQL";
            this.chkBQL.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkBQL.Properties.Caption = "Quản lý tòa nhà";
            this.chkBQL.Size = new System.Drawing.Size(125, 19);
            this.chkBQL.TabIndex = 1;
            // 
            // chkIsAdmin
            // 
            this.chkIsAdmin.EditValue = null;
            this.chkIsAdmin.Location = new System.Drawing.Point(91, 63);
            this.chkIsAdmin.Name = "chkIsAdmin";
            this.chkIsAdmin.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkIsAdmin.Properties.Caption = "Quản lý phòng";
            this.chkIsAdmin.Size = new System.Drawing.Size(125, 19);
            this.chkIsAdmin.TabIndex = 1;
            // 
            // frmEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 146);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng ký";
            this.Load += new System.EventHandler(this.frmEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBQL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsAdmin.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnAccept;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chkIsAdmin;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lookUpDepartment;
        private DevExpress.XtraEditors.GridLookUpEdit glEmployee;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.CheckEdit chkBQL;
    }
}