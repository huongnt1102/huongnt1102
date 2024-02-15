namespace LandSoftBuilding.Fund.Input
{
    partial class frmHinhThucThanhToan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHinhThucThanhToan));
            this.gcHTTT = new DevExpress.XtraGrid.GridControl();
            this.grvHTTT = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.gcHTTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvHTTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcHTTT
            // 
            resources.ApplyResources(this.gcHTTT, "gcHTTT");
            this.gcHTTT.MainView = this.grvHTTT;
            this.gcHTTT.Name = "gcHTTT";
            this.gcHTTT.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gcHTTT.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvHTTT});
            this.gcHTTT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcHTTT_KeyDown);
            // 
            // grvHTTT
            // 
            this.grvHTTT.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.grvHTTT.GridControl = this.gcHTTT;
            this.grvHTTT.Name = "grvHTTT";
            this.grvHTTT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvHTTT.OptionsView.ShowDetailButtons = false;
            this.grvHTTT.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.ColumnEdit = this.repositoryItemTextEdit1;
            this.gridColumn1.FieldName = "TenPL";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // repositoryItemTextEdit1
            // 
            resources.ApplyResources(this.repositoryItemTextEdit1, "repositoryItemTextEdit1");
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.EditValueChanged += new System.EventHandler(this.repositoryItemTextEdit1_EditValueChanged);
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.FieldName = "MaTN";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnOK.Appearance.Font")));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnOK.ImageOptions.ImageIndex")));
            this.btnOK.ImageOptions.ImageList = this.imageCollection1;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnHuy.Appearance.Font")));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnHuy.ImageOptions.ImageIndex")));
            this.btnHuy.ImageOptions.ImageList = this.imageCollection1;
            resources.ApplyResources(this.btnHuy, "btnHuy");
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            // 
            // frmHinhThucThanhToan
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.gcHTTT);
            this.Name = "frmHinhThucThanhToan";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmHinhThucThanhToan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcHTTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvHTTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcHTTT;
        private DevExpress.XtraGrid.Views.Grid.GridView grvHTTT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}