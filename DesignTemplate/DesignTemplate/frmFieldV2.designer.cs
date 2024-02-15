namespace LandSoft.DuAn.BieuMau
{
    partial class frmFieldV2
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcField = new DevExpress.XtraGrid.GridControl();
            this.gvField = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookUpGroup = new DevExpress.XtraEditors.LookUpEdit();
            this.lblDienGiai = new DevExpress.XtraEditors.LabelControl();
            this.lblTenBT = new DevExpress.XtraEditors.LabelControl();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpGroup.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gcField);
            this.splitContainerControl1.Panel1.Controls.Add(this.lookUpGroup);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.lblDienGiai);
            this.splitContainerControl1.Panel2.Controls.Add(this.lblTenBT);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(286, 512);
            this.splitContainerControl1.SplitterPosition = 56;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gcField
            // 
            this.gcField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gcField.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcField.Location = new System.Drawing.Point(0, 27);
            this.gcField.MainView = this.gvField;
            this.gcField.Name = "gcField";
            this.gcField.Size = new System.Drawing.Size(286, 424);
            this.gcField.TabIndex = 3;
            this.gcField.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvField});
            // 
            // gvField
            // 
            this.gvField.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gvField.GridControl = this.gcField;
            this.gvField.GroupCount = 1;
            this.gvField.GroupFormat = "{1} {2}";
            this.gvField.Name = "gvField";
            this.gvField.OptionsView.ColumnAutoWidth = false;
            this.gvField.OptionsView.ShowAutoFilterRow = true;
            this.gvField.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn2, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvField.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvField_FocusedRowChanged);
            this.gvField.DoubleClick += new System.EventHandler(this.gvField_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên trường";
            this.gridColumn1.FieldName = "TenBT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 248;
            // 
            // lookUpGroup
            // 
            this.lookUpGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lookUpGroup.Location = new System.Drawing.Point(0, 1);
            this.lookUpGroup.Name = "lookUpGroup";
            this.lookUpGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpGroup.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name1")});
            this.lookUpGroup.Properties.DisplayMember = "Name";
            this.lookUpGroup.Properties.DropDownRows = 20;
            this.lookUpGroup.Properties.NullText = "[Chọn hạng mục]";
            this.lookUpGroup.Properties.ShowHeader = false;
            this.lookUpGroup.Properties.ValueMember = "ID";
            this.lookUpGroup.Size = new System.Drawing.Size(286, 20);
            this.lookUpGroup.TabIndex = 2;
            this.lookUpGroup.EditValueChanged += new System.EventHandler(this.lookUpGroup_EditValueChanged);
            // 
            // lblDienGiai
            // 
            this.lblDienGiai.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblDienGiai.Location = new System.Drawing.Point(6, 20);
            this.lblDienGiai.Name = "lblDienGiai";
            this.lblDienGiai.Size = new System.Drawing.Size(270, 13);
            this.lblDienGiai.TabIndex = 0;
            this.lblDienGiai.Text = "Diễn giải";
            // 
            // lblTenBT
            // 
            this.lblTenBT.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTenBT.Location = new System.Drawing.Point(6, 1);
            this.lblTenBT.Name = "lblTenBT";
            this.lblTenBT.Size = new System.Drawing.Size(53, 13);
            this.lblTenBT.TabIndex = 0;
            this.lblTenBT.Text = "Biểu thức";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Hạng mục";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // frmFieldV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 512);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainerControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(302, 550);
            this.Name = "frmFieldV2";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trường thông tin";
            this.Load += new System.EventHandler(this.frmField_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmField_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmField_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpGroup.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.LabelControl lblDienGiai;
        private DevExpress.XtraEditors.LabelControl lblTenBT;
        private DevExpress.XtraEditors.LookUpEdit lookUpGroup;
        private DevExpress.XtraGrid.GridControl gcField;
        private DevExpress.XtraGrid.Views.Grid.GridView gvField;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;

    }
}