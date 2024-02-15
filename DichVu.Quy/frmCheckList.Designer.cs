namespace DichVu.Quy
{
    partial class frmCheckList
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
            this.gcPhieuThu = new DevExpress.XtraGrid.GridControl();
            this.grvPhieuThu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcPhieuThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPhieuThu)).BeginInit();
            this.SuspendLayout();
            // 
            // gcPhieuThu
            // 
            this.gcPhieuThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPhieuThu.Location = new System.Drawing.Point(0, 0);
            this.gcPhieuThu.MainView = this.grvPhieuThu;
            this.gcPhieuThu.Name = "gcPhieuThu";
            this.gcPhieuThu.Size = new System.Drawing.Size(764, 412);
            this.gcPhieuThu.TabIndex = 5;
            this.gcPhieuThu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvPhieuThu});
            // 
            // grvPhieuThu
            // 
            this.grvPhieuThu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.grvPhieuThu.GridControl = this.gcPhieuThu;
            this.grvPhieuThu.Name = "grvPhieuThu";
            this.grvPhieuThu.OptionsBehavior.Editable = false;
            this.grvPhieuThu.OptionsBehavior.ReadOnly = true;
            this.grvPhieuThu.OptionsSelection.MultiSelect = true;
            this.grvPhieuThu.OptionsView.ColumnAutoWidth = false;
            this.grvPhieuThu.OptionsView.ShowAutoFilterRow = true;
            this.grvPhieuThu.OptionsView.ShowFooter = true;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mặt bằng";
            this.gridColumn2.FieldName = "MaSoMB";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 149;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Khách hàng";
            this.gridColumn3.FieldName = "KhachHang";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 168;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Số tiền";
            this.gridColumn4.DisplayFormat.FormatString = "{0:#,0.#} VNÐ";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "SoTien";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SoTienThanhToan", "Tổng: {0:#,0.#}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 123;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Số đợt";
            this.gridColumn5.FieldName = "SoDot";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 49;
            // 
            // frmCheckList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 412);
            this.Controls.Add(this.gcPhieuThu);
            this.Name = "frmCheckList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Result";
            this.Load += new System.EventHandler(this.frmCheckList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcPhieuThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPhieuThu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcPhieuThu;
        private DevExpress.XtraGrid.Views.Grid.GridView grvPhieuThu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
    }
}