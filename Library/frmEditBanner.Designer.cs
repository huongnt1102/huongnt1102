namespace Library
{
    partial class frmEditBanner
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
            this.components = new System.ComponentModel.Container();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.spinOrders = new DevExpress.XtraEditors.SpinEdit();
            this.checkFooter = new DevExpress.XtraEditors.CheckEdit();
            this.checkHeader = new DevExpress.XtraEditors.CheckEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.checkHienThi = new DevExpress.XtraEditors.CheckEdit();
            this.picBanner = new System.Windows.Forms.PictureBox();
            this.buttonEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.gcToaNha = new DevExpress.XtraGrid.GridControl();
            this.grvToaNha = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinOrders.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkFooter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkHeader.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkHienThi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBanner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnSave});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 1;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(667, 144);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSave)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Lưu";
            this.btnSave.Id = 0;
            this.btnSave.ImageOptions.SvgImage = global::Library.Properties.Resources.save;
            this.btnSave.Name = "btnSave";
            this.btnSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1101, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 346);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1101, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 322);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1101, 24);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 322);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 24);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl3);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl2);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl1);
            this.splitContainerControl1.Panel1.Controls.Add(this.spinOrders);
            this.splitContainerControl1.Panel1.Controls.Add(this.checkFooter);
            this.splitContainerControl1.Panel1.Controls.Add(this.checkHeader);
            this.splitContainerControl1.Panel1.Controls.Add(this.label2);
            this.splitContainerControl1.Panel1.Controls.Add(this.txtLink);
            this.splitContainerControl1.Panel1.Controls.Add(this.label1);
            this.splitContainerControl1.Panel1.Controls.Add(this.txtTitle);
            this.splitContainerControl1.Panel1.Controls.Add(this.checkHienThi);
            this.splitContainerControl1.Panel1.Controls.Add(this.picBanner);
            this.splitContainerControl1.Panel1.Controls.Add(this.buttonEdit1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gcToaNha);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1101, 322);
            this.splitContainerControl1.SplitterPosition = 656;
            this.splitContainerControl1.TabIndex = 50;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(454, 251);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(98, 16);
            this.labelControl2.TabIndex = 57;
            this.labelControl2.Text = "Size: 380 x 160";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(243, 76);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(33, 13);
            this.labelControl1.TabIndex = 56;
            this.labelControl1.Text = "Thứ tự";
            // 
            // spinOrders
            // 
            this.spinOrders.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinOrders.Location = new System.Drawing.Point(282, 73);
            this.spinOrders.MenuManager = this.barManager1;
            this.spinOrders.Name = "spinOrders";
            this.spinOrders.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinOrders.Size = new System.Drawing.Size(65, 20);
            this.spinOrders.TabIndex = 55;
            // 
            // checkFooter
            // 
            this.checkFooter.Location = new System.Drawing.Point(152, 75);
            this.checkFooter.MenuManager = this.barManager1;
            this.checkFooter.Name = "checkFooter";
            this.checkFooter.Properties.Caption = "Footer";
            this.checkFooter.Size = new System.Drawing.Size(75, 19);
            this.checkFooter.TabIndex = 54;
            // 
            // checkHeader
            // 
            this.checkHeader.Location = new System.Drawing.Point(71, 75);
            this.checkHeader.MenuManager = this.barManager1;
            this.checkHeader.Name = "checkHeader";
            this.checkHeader.Properties.Caption = "Header";
            this.checkHeader.Size = new System.Drawing.Size(75, 19);
            this.checkHeader.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Link:";
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(72, 48);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(371, 21);
            this.txtLink.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Tiêu đề:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(72, 21);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(371, 21);
            this.txtTitle.TabIndex = 49;
            // 
            // checkHienThi
            // 
            this.checkHienThi.Location = new System.Drawing.Point(19, 274);
            this.checkHienThi.MenuManager = this.barManager1;
            this.checkHienThi.Name = "checkHienThi";
            this.checkHienThi.Properties.Caption = "Hiển thị";
            this.checkHienThi.Size = new System.Drawing.Size(75, 19);
            this.checkHienThi.TabIndex = 48;
            // 
            // picBanner
            // 
            this.picBanner.BackColor = System.Drawing.Color.White;
            this.picBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBanner.Location = new System.Drawing.Point(19, 109);
            this.picBanner.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picBanner.Name = "picBanner";
            this.picBanner.Size = new System.Drawing.Size(597, 137);
            this.picBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBanner.TabIndex = 46;
            this.picBanner.TabStop = false;
            this.picBanner.DoubleClick += new System.EventHandler(this.picBanner_DoubleClick);
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.Location = new System.Drawing.Point(284, 160);
            this.buttonEdit1.MenuManager = this.barManager1;
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.ReadOnly = true;
            this.buttonEdit1.Size = new System.Drawing.Size(26, 20);
            this.buttonEdit1.TabIndex = 47;
            this.buttonEdit1.Visible = false;
            // 
            // gcToaNha
            // 
            this.gcToaNha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcToaNha.Location = new System.Drawing.Point(0, 0);
            this.gcToaNha.MainView = this.grvToaNha;
            this.gcToaNha.Name = "gcToaNha";
            this.gcToaNha.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcToaNha.ShowOnlyPredefinedDetails = true;
            this.gcToaNha.Size = new System.Drawing.Size(440, 322);
            this.gcToaNha.TabIndex = 3;
            this.gcToaNha.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvToaNha});
            // 
            // grvToaNha
            // 
            this.grvToaNha.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn1});
            this.grvToaNha.GridControl = this.gcToaNha;
            this.grvToaNha.IndicatorWidth = 40;
            this.grvToaNha.Name = "grvToaNha";
            this.grvToaNha.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Tên tòa nhà";
            this.gridColumn5.FieldName = "TenTN";
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
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "MaTN";
            this.gridColumn1.FieldName = "MaTN";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(19, 251);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(298, 17);
            this.labelControl3.TabIndex = 57;
            this.labelControl3.Text = "Click double vào ảnh để thêm hoặc sửa ảnh";
            // 
            // frmEditBanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 346);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmEditBanner";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CẬP NHẬT BANNER";
            this.Load += new System.EventHandler(this.frmEditBanner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinOrders.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkFooter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkHeader.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkHienThi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBanner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.CheckEdit checkFooter;
        private DevExpress.XtraEditors.CheckEdit checkHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitle;
        private DevExpress.XtraEditors.CheckEdit checkHienThi;
        private System.Windows.Forms.PictureBox picBanner;
        private DevExpress.XtraEditors.TextEdit buttonEdit1;
        private DevExpress.XtraGrid.GridControl gcToaNha;
        private DevExpress.XtraGrid.Views.Grid.GridView grvToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit spinOrders;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}