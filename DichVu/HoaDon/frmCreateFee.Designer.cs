namespace DichVu.HoaDon
{
    partial class frmCreateFee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateFee));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gcService = new DevExpress.XtraGrid.GridControl();
            this.gvService = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.chkCheckAll = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpToaNha = new DevExpress.XtraEditors.LookUpEdit();
            this.dateSyn = new DevExpress.XtraEditors.DateEdit();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcService)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvService)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheckAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToaNha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSyn.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSyn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.gcService);
            this.panelControl1.Location = new System.Drawing.Point(11, 36);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(475, 265);
            this.panelControl1.TabIndex = 0;
            // 
            // gcService
            // 
            this.gcService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcService.Location = new System.Drawing.Point(0, 0);
            this.gcService.MainView = this.gvService;
            this.gcService.Name = "gcService";
            this.gcService.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkCheck});
            this.gcService.Size = new System.Drawing.Size(475, 265);
            this.gcService.TabIndex = 0;
            this.gcService.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvService});
            // 
            // gvService
            // 
            this.gvService.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gvService.GridControl = this.gcService;
            this.gvService.Name = "gvService";
            this.gvService.OptionsView.ColumnAutoWidth = false;
            this.gvService.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Dịch vụ";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 380;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Check";
            this.gridColumn2.ColumnEdit = this.chkCheck;
            this.gridColumn2.FieldName = "IsCheck";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.OptionsColumn.AllowSize = false;
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 20;
            // 
            // chkCheck
            // 
            this.chkCheck.AutoHeight = false;
            this.chkCheck.Name = "chkCheck";
            this.chkCheck.EditValueChanged += new System.EventHandler(this.chkCheck_EditValueChanged);
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tình trạng";
            this.gridColumn3.FieldName = "No";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.ImageIndex = 1;
            this.btnCancel.ImageOptions.ImageList = this.imageCollection1;
            this.btnCancel.Location = new System.Drawing.Point(406, 307);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 26);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Thoát";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.ImageOptions.ImageIndex = 0;
            this.btnOK.ImageOptions.ImageList = this.imageCollection1;
            this.btnOK.Location = new System.Drawing.Point(306, 307);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 26);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "Thực hiện";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkCheckAll
            // 
            this.chkCheckAll.Location = new System.Drawing.Point(27, 305);
            this.chkCheckAll.Name = "chkCheckAll";
            this.chkCheckAll.Properties.Caption = "Chọn/(Bỏ chọn) tất cả";
            this.chkCheckAll.Size = new System.Drawing.Size(140, 19);
            this.chkCheckAll.TabIndex = 13;
            this.chkCheckAll.CheckedChanged += new System.EventHandler(this.chkCheckAll_CheckedChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(47, 13);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "Thời gian:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(224, 13);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(33, 13);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "Dự án:";
            // 
            // lookUpToaNha
            // 
            this.lookUpToaNha.Location = new System.Drawing.Point(273, 10);
            this.lookUpToaNha.Name = "lookUpToaNha";
            this.lookUpToaNha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpToaNha.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Name1")});
            this.lookUpToaNha.Properties.DisplayMember = "TenTN";
            this.lookUpToaNha.Properties.NullText = "[Chọn Dự án]";
            this.lookUpToaNha.Properties.ShowHeader = false;
            this.lookUpToaNha.Properties.ValueMember = "MaTN";
            this.lookUpToaNha.Size = new System.Drawing.Size(213, 20);
            this.lookUpToaNha.TabIndex = 17;
            // 
            // dateSyn
            // 
            this.dateSyn.EditValue = null;
            this.dateSyn.Location = new System.Drawing.Point(67, 10);
            this.dateSyn.Name = "dateSyn";
            this.dateSyn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateSyn.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateSyn.Size = new System.Drawing.Size(112, 20);
            this.dateSyn.TabIndex = 1;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_save.png");  
            this.imageCollection1.Images.SetKeyName(1, "icons8_refresh1.png");  
            // 
            // frmCreateFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 345);
            this.Controls.Add(this.dateSyn);
            this.Controls.Add(this.lookUpToaNha);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.chkCheckAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreateFee";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo công nợ hàng tháng";
            this.Load += new System.EventHandler(this.frmCreateFee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcService)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvService)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheckAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToaNha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSyn.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSyn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gcService;
        private DevExpress.XtraGrid.Views.Grid.GridView gvService;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkCheck;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.CheckEdit chkCheckAll;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lookUpToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.DateEdit dateSyn;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}