namespace DichVu
{
    partial class ctlPhatSinh
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gcDichVu = new DevExpress.XtraGrid.GridControl();
            this.gvDichVu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController();
            ((System.ComponentModel.ISupportInitialize)(this.gcDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDichVu)).BeginInit();
            this.SuspendLayout();
            // 
            // gcDichVu
            // 
            this.gcDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDichVu.Location = new System.Drawing.Point(0, 0);
            this.gcDichVu.MainView = this.gvDichVu;
            this.gcDichVu.Name = "gcDichVu";
            this.gcDichVu.Size = new System.Drawing.Size(941, 229);
            this.gcDichVu.TabIndex = 0;
            this.gcDichVu.ToolTipController = this.toolTipController1;
            this.gcDichVu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDichVu});
            // 
            // gvDichVu
            // 
            this.gvDichVu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn9,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn10,
            this.gridColumn7,
            this.gridColumn8});
            this.gvDichVu.GridControl = this.gcDichVu;
            this.gvDichVu.Name = "gvDichVu";
            this.gvDichVu.OptionsView.ColumnAutoWidth = false;
            this.gvDichVu.OptionsView.ShowFooter = true;
            this.gvDichVu.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Thời gian";
            this.gridColumn1.DisplayFormat.FormatString = "{0:MM/yyyy}";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "NgayNhap";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mặt bằng";
            this.gridColumn2.FieldName = "MaSoMB";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 98;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Loại mặt bằng";
            this.gridColumn3.FieldName = "TenLMB";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 90;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Nợ trước";
            this.gridColumn9.DisplayFormat.FormatString = "{0:#,0.#}";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "NoTruoc";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
            this.gridColumn9.Width = 98;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Phải thu";
            this.gridColumn4.DisplayFormat.FormatString = "{0:#,0.#}";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "SoTien";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PQL", "{0:#,0.#}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 96;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Đã thu";
            this.gridColumn5.DisplayFormat.FormatString = "{0:#,0.#}";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "DaThu";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DaThu", "{0:#,0.#}")});
            this.gridColumn5.Width = 92;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Nợ nay";
            this.gridColumn6.DisplayFormat.FormatString = "{0:#,0.#}";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "ConLai";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ConLai", "{0:#,0.#}")});
            this.gridColumn6.Width = 93;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Tổng nợ";
            this.gridColumn10.DisplayFormat.FormatString = "{0:#,0.#}";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn10.FieldName = "TongNo";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            this.gridColumn10.Width = 102;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Tình trạng";
            this.gridColumn7.FieldName = "TrangThai";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Width = 98;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Diễn giải";
            this.gridColumn8.FieldName = "DienGiai";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 6;
            this.gridColumn8.Width = 197;
            // 
            // toolTipController1
            // 
            this.toolTipController1.AutoPopDelay = 50000;
            this.toolTipController1.InitialDelay = 10;
            this.toolTipController1.Rounded = true;
            this.toolTipController1.RoundRadius = 4;
            // 
            // ctlPhatSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcDichVu);
            this.Name = "ctlPhatSinh";
            this.Size = new System.Drawing.Size(941, 229);
            ((System.ComponentModel.ISupportInitialize)(this.gcDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDichVu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcDichVu;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDichVu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
    }
}
