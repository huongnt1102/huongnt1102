namespace Building.AppVime.Tower
{
    partial class frmFunction
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
            this.gcFunction = new DevExpress.XtraGrid.GridControl();
            this.gvFunction = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookUpEditTower = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTower)).BeginInit();
            this.SuspendLayout();
            // 
            // gcFunction
            // 
            this.gcFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcFunction.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcFunction.Location = new System.Drawing.Point(0, 0);
            this.gcFunction.MainView = this.gvFunction;
            this.gcFunction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcFunction.Name = "gcFunction";
            this.gcFunction.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookUpEditTower});
            this.gcFunction.Size = new System.Drawing.Size(774, 452);
            this.gcFunction.TabIndex = 1;
            this.gcFunction.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFunction});
            // 
            // gvFunction
            // 
            this.gvFunction.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn1,
            this.gridColumn2});
            this.gvFunction.GridControl = this.gcFunction;
            this.gvFunction.Name = "gvFunction";
            this.gvFunction.OptionsDetail.EnableMasterViewMode = false;
            this.gvFunction.OptionsSelection.MultiSelect = true;
            this.gvFunction.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvFunction.OptionsView.ShowAutoFilterRow = true;
            this.gvFunction.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Id";
            this.gridColumn7.FieldName = "Id";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên chức năng";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 250;
            // 
            // lookUpEditTower
            // 
            this.lookUpEditTower.AutoHeight = false;
            this.lookUpEditTower.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditTower.DisplayMember = "TenTN";
            this.lookUpEditTower.Name = "lookUpEditTower";
            this.lookUpEditTower.NullText = "";
            this.lookUpEditTower.ValueMember = "MaTN";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Icon";
            this.gridColumn2.FieldName = "IconUrl";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // frmFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 452);
            this.Controls.Add(this.gcFunction);
            this.MaximizeBox = false;
            this.Name = "frmFunction";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chức năng";
            ((System.ComponentModel.ISupportInitialize)(this.gcFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTower)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcFunction;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFunction;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpEditTower;
    }
}