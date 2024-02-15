namespace Building.AppVime.Tower
{
    partial class frmSetupFunction
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
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookUpEditTower = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcFunctionChoice = new DevExpress.XtraGrid.GridControl();
            this.gvFunctionChoice = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btnPrevious = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcFunctionChoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFunctionChoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcFunction
            // 
            this.gcFunction.Location = new System.Drawing.Point(9, 11);
            this.gcFunction.MainView = this.gvFunction;
            this.gcFunction.Name = "gcFunction";
            this.gcFunction.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookUpEditTower});
            this.gcFunction.Size = new System.Drawing.Size(217, 370);
            this.gcFunction.TabIndex = 0;
            this.gcFunction.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFunction});
            // 
            // gvFunction
            // 
            this.gvFunction.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn1,
            this.gridColumn6});
            this.gvFunction.DetailHeight = 284;
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
            this.gridColumn7.MinWidth = 17;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Width = 64;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên chức năng";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.MinWidth = 17;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 214;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "FunctionId";
            this.gridColumn6.FieldName = "FunctionId";
            this.gridColumn6.MinWidth = 17;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Width = 64;
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
            // gcFunctionChoice
            // 
            this.gcFunctionChoice.Location = new System.Drawing.Point(270, 11);
            this.gcFunctionChoice.MainView = this.gvFunctionChoice;
            this.gcFunctionChoice.Name = "gcFunctionChoice";
            this.gcFunctionChoice.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1});
            this.gcFunctionChoice.Size = new System.Drawing.Size(237, 370);
            this.gcFunctionChoice.TabIndex = 1;
            this.gcFunctionChoice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFunctionChoice});
            // 
            // gvFunctionChoice
            // 
            this.gvFunctionChoice.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gvFunctionChoice.DetailHeight = 284;
            this.gvFunctionChoice.GridControl = this.gcFunctionChoice;
            this.gvFunctionChoice.Name = "gvFunctionChoice";
            this.gvFunctionChoice.OptionsDetail.EnableMasterViewMode = false;
            this.gvFunctionChoice.OptionsSelection.MultiSelect = true;
            this.gvFunctionChoice.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvFunctionChoice.OptionsView.ShowAutoFilterRow = true;
            this.gvFunctionChoice.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Id";
            this.gridColumn2.FieldName = "Id";
            this.gridColumn2.MinWidth = 17;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Width = 64;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tên chức năng";
            this.gridColumn3.FieldName = "Name";
            this.gridColumn3.MinWidth = 17;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 179;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "STT";
            this.gridColumn4.FieldName = "IndexNumber";
            this.gridColumn4.MinWidth = 17;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 55;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "FunctionId";
            this.gridColumn5.FieldName = "FunctionId";
            this.gridColumn5.MinWidth = 17;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Width = 64;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.DisplayMember = "TenTN";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            this.repositoryItemLookUpEdit1.ValueMember = "MaTN";
            // 
            // btnPrevious
            // 
            this.btnPrevious.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.btnPrevious.Appearance.Options.UseFont = true;
            this.btnPrevious.Location = new System.Drawing.Point(232, 133);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(32, 26);
            this.btnPrevious.TabIndex = 6;
            this.btnPrevious.Text = "«";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.btnNext.Appearance.Options.UseFont = true;
            this.btnNext.Location = new System.Drawing.Point(232, 166);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(32, 26);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "»";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Delete_icon;
            this.btnCancel.Location = new System.Drawing.Point(422, 387);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 26);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Save_as_icon;
            this.btnSave.Location = new System.Drawing.Point(333, 387);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 26);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Lưu && Đóng";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmSetupFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 423);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.gcFunction);
            this.Controls.Add(this.gcFunctionChoice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frmSetupFunction";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt chức năng";
            this.Load += new System.EventHandler(this.frmSetupFunction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcFunctionChoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFunctionChoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gcFunction;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFunction;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpEditTower;
        private DevExpress.XtraGrid.GridControl gcFunctionChoice;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFunctionChoice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.SimpleButton btnPrevious;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}