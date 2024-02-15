namespace ToaNha
{
    partial class frmSelect
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
            this.gcNhanVien = new DevExpress.XtraGrid.GridControl();
            this.gvNhanVien = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaSo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenNV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNhanVien)).BeginInit();
            this.SuspendLayout();
            // 
            // gcNhanVien
            // 
            this.gcNhanVien.Location = new System.Drawing.Point(12, 12);
            this.gcNhanVien.MainView = this.gvNhanVien;
            this.gcNhanVien.Name = "gcNhanVien";
            this.gcNhanVien.Size = new System.Drawing.Size(722, 343);
            this.gcNhanVien.TabIndex = 6;
            this.gcNhanVien.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNhanVien});
            // 
            // gvNhanVien
            // 
            this.gvNhanVien.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaSo,
            this.colTenNV,
            this.gridColumn5,
            this.gridColumn4,
            this.gridColumn6});
            this.gvNhanVien.GridControl = this.gcNhanVien;
            this.gvNhanVien.GroupPanelText = "Kéo một vài cột lên đây để xem theo nhóm";
            this.gvNhanVien.Name = "gvNhanVien";
            this.gvNhanVien.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvNhanVien.OptionsBehavior.Editable = false;
            this.gvNhanVien.OptionsSelection.MultiSelect = true;
            this.gvNhanVien.OptionsView.ColumnAutoWidth = false;
            this.gvNhanVien.OptionsView.ShowAutoFilterRow = true;
            // 
            // colMaSo
            // 
            this.colMaSo.Caption = "Mã số";
            this.colMaSo.FieldName = "MaSoNV";
            this.colMaSo.Name = "colMaSo";
            this.colMaSo.OptionsColumn.AllowEdit = false;
            this.colMaSo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colMaSo.Visible = true;
            this.colMaSo.VisibleIndex = 0;
            this.colMaSo.Width = 100;
            // 
            // colTenNV
            // 
            this.colTenNV.Caption = "Họ và tên";
            this.colTenNV.FieldName = "HoTenNV";
            this.colTenNV.Name = "colTenNV";
            this.colTenNV.OptionsColumn.AllowEdit = false;
            this.colTenNV.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colTenNV.Visible = true;
            this.colTenNV.VisibleIndex = 1;
            this.colTenNV.Width = 150;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Chức vụ";
            this.gridColumn5.FieldName = "TenCV";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 120;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Dự án";
            this.gridColumn6.FieldName = "TenTN";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 120;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Phòng ban";
            this.gridColumn4.FieldName = "TenPB";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 120;
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(659, 361);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Hủy";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSelect.Appearance.Options.UseFont = true;
            this.btnSelect.Location = new System.Drawing.Point(550, 361);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(103, 23);
            this.btnSelect.TabIndex = 7;
            this.btnSelect.Text = "Chọn && Đóng";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // frmSelect
            // 
            this.AcceptButton = this.btnSelect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(746, 396);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gcNhanVien);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn nhân viên";
            this.Load += new System.EventHandler(this.frmSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNhanVien)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcNhanVien;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNhanVien;
        private DevExpress.XtraGrid.Columns.GridColumn colMaSo;
        private DevExpress.XtraGrid.Columns.GridColumn colTenNV;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
    }
}