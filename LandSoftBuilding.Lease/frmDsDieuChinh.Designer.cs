namespace LandSoftBuilding.Lease
{
    partial class frmDsDieuChinh
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
            this.gcChiTiet = new DevExpress.XtraGrid.GridControl();
            this.gvChiTiet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn114 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcChiTiet
            // 
            this.gcChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcChiTiet.Location = new System.Drawing.Point(0, 0);
            this.gcChiTiet.MainView = this.gvChiTiet;
            this.gcChiTiet.Name = "gcChiTiet";
            this.gcChiTiet.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1});
            this.gcChiTiet.Size = new System.Drawing.Size(790, 328);
            this.gcChiTiet.TabIndex = 3;
            this.gcChiTiet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvChiTiet});
            // 
            // gvChiTiet
            // 
            this.gvChiTiet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn114,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn3,
            this.gridColumn5});
            this.gvChiTiet.GridControl = this.gcChiTiet;
            this.gvChiTiet.GroupPanelText = "Kéo cột lên đây để xem theo nhóm";
            this.gvChiTiet.Name = "gvChiTiet";
            this.gvChiTiet.OptionsBehavior.Editable = false;
            this.gvChiTiet.OptionsDetail.EnableMasterViewMode = false;
            this.gvChiTiet.OptionsSelection.MultiSelect = true;
            this.gvChiTiet.OptionsView.ColumnAutoWidth = false;
            this.gvChiTiet.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn114
            // 
            this.gridColumn114.Caption = "Ngày điều chỉnh";
            this.gridColumn114.DisplayFormat.FormatString = "{0: HH:mm - dd/MM/yyyy}";
            this.gridColumn114.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn114.FieldName = "NgayDC";
            this.gridColumn114.Name = "gridColumn114";
            this.gridColumn114.OptionsColumn.ReadOnly = true;
            this.gridColumn114.Visible = true;
            this.gridColumn114.VisibleIndex = 0;
            this.gridColumn114.Width = 116;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Giá thuê cũ";
            this.gridColumn1.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "GiaThueCu";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 120;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "TLĐC Giá thuê";
            this.gridColumn2.DisplayFormat.FormatString = "p0";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "TyLeDCGT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 93;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Giá dịch vụ cũ";
            this.gridColumn4.DisplayFormat.FormatString = "{0:#,0.##}";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "GiaDVCu";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 89;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "TLĐC Giá dịch vụ";
            this.gridColumn3.DisplayFormat.FormatString = "p0";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "TyLeDCGDV";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            this.gridColumn3.Width = 99;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Diễn giải";
            this.gridColumn5.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.gridColumn5.FieldName = "DienGiai";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 265;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            this.repositoryItemMemoExEdit1.ShowIcon = false;
            // 
            // frmDsDieuChinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 328);
            this.Controls.Add(this.gcChiTiet);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDsDieuChinh";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lịch sử điều chỉnh giá";
            this.Load += new System.EventHandler(this.frmDsDieuChinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcChiTiet;
        private DevExpress.XtraGrid.Views.Grid.GridView gvChiTiet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn114;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
    }
}