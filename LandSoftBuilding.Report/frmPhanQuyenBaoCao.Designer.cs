namespace LandSoftBuilding.Report
{
    partial class frmPhanQuyenBaoCao
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
            this.gcToaNha = new DevExpress.XtraGrid.GridControl();
            this.grvToaNha = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBaoCao = new DevExpress.XtraGrid.GridControl();
            this.grvBaoCao = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcToaNha
            // 
            this.gcToaNha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcToaNha.Location = new System.Drawing.Point(0, 0);
            this.gcToaNha.MainView = this.grvToaNha;
            this.gcToaNha.Name = "gcToaNha";
            this.gcToaNha.ShowOnlyPredefinedDetails = true;
            this.gcToaNha.Size = new System.Drawing.Size(674, 499);
            this.gcToaNha.TabIndex = 1;
            this.gcToaNha.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvToaNha});
            // 
            // grvToaNha
            // 
            this.grvToaNha.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.grvToaNha.GridControl = this.gcToaNha;
            this.grvToaNha.IndicatorWidth = 40;
            this.grvToaNha.Name = "grvToaNha";
            this.grvToaNha.OptionsBehavior.Editable = false;
            this.grvToaNha.OptionsBehavior.ReadOnly = true;
            this.grvToaNha.OptionsView.ShowAutoFilterRow = true;
            this.grvToaNha.OptionsView.ShowFooter = true;
            this.grvToaNha.OptionsView.ShowGroupPanel = false;
            this.grvToaNha.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.grvToaNha_CustomDrawRowIndicator);
            this.grvToaNha.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvToaNha_FocusedRowChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên VT";
            this.gridColumn1.FieldName = "TenVT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "TenVT", "{0:#,0.##} dòng")});
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên Dự án";
            this.gridColumn2.FieldName = "TenTN";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Địa chỉ";
            this.gridColumn3.FieldName = "DiaChi";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Điện thoại";
            this.gridColumn4.FieldName = "DienThoai";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gcBaoCao
            // 
            this.gcBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBaoCao.Location = new System.Drawing.Point(0, 0);
            this.gcBaoCao.MainView = this.grvBaoCao;
            this.gcBaoCao.Name = "gcBaoCao";
            this.gcBaoCao.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcBaoCao.ShowOnlyPredefinedDetails = true;
            this.gcBaoCao.Size = new System.Drawing.Size(360, 499);
            this.gcBaoCao.TabIndex = 2;
            this.gcBaoCao.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvBaoCao});
            // 
            // grvBaoCao
            // 
            this.grvBaoCao.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn6});
            this.grvBaoCao.GridControl = this.gcBaoCao;
            this.grvBaoCao.IndicatorWidth = 40;
            this.grvBaoCao.Name = "grvBaoCao";
            this.grvBaoCao.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.grvBaoCao_CustomDrawRowIndicator);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Tên báo cáo";
            this.gridColumn5.FieldName = "Name";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 1011;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Chọn";
            this.gridColumn6.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn6.FieldName = "Duyet";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            this.gridColumn6.Width = 163;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEdit1_CheckedChanged);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gcToaNha);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gcBaoCao);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1039, 499);
            this.splitContainerControl1.SplitterPosition = 674;
            this.splitContainerControl1.TabIndex = 3;
            // 
            // frmPhanQuyenBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 499);
            this.Controls.Add(this.splitContainerControl1);
            this.MaximizeBox = false;
            this.Name = "frmPhanQuyenBaoCao";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phân quyền báo cáo";
            this.Load += new System.EventHandler(this.frmPhanQuyenBaoCao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcToaNha;
        private DevExpress.XtraGrid.Views.Grid.GridView grvToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.GridControl gcBaoCao;
        private DevExpress.XtraGrid.Views.Grid.GridView grvBaoCao;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
    }
}