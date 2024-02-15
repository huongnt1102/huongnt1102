namespace AnNinh
{
    partial class frmMapKeHoach
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
            this.gcMap = new DevExpress.XtraGrid.GridControl();
            this.grvMap = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNhomNV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookNhom = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.colKeHoach = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhom)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMap
            // 
            this.gcMap.Location = new System.Drawing.Point(12, 12);
            this.gcMap.MainView = this.grvMap;
            this.gcMap.Name = "gcMap";
            this.gcMap.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookNhom});
            this.gcMap.Size = new System.Drawing.Size(593, 157);
            this.gcMap.TabIndex = 0;
            this.gcMap.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMap});
            // 
            // grvMap
            // 
            this.grvMap.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNhomNV,
            this.colKeHoach});
            this.grvMap.GridControl = this.gcMap;
            this.grvMap.Name = "grvMap";
            this.grvMap.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvMap.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.grvMap.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvMap_InitNewRow);
            // 
            // colNhomNV
            // 
            this.colNhomNV.Caption = "Nhóm phân quyền";
            this.colNhomNV.ColumnEdit = this.lookNhom;
            this.colNhomNV.FieldName = "GroupID";
            this.colNhomNV.Name = "colNhomNV";
            this.colNhomNV.Visible = true;
            this.colNhomNV.VisibleIndex = 0;
            // 
            // lookNhom
            // 
            this.lookNhom.AutoHeight = false;
            this.lookNhom.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookNhom.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GroupName", "Tên nhóm"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DienGiai", "Diễn giải")});
            this.lookNhom.DisplayMember = "GroupName";
            this.lookNhom.Name = "lookNhom";
            this.lookNhom.NullText = "";
            this.lookNhom.ValueMember = "GroupID";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(530, 176);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(449, 176);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 30);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "Chấp nhận";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // colKeHoach
            // 
            this.colKeHoach.Caption = "MaKeHoach";
            this.colKeHoach.FieldName = "MaKeHoach";
            this.colKeHoach.Name = "colKeHoach";
            // 
            // frmMapKeHoach
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(617, 218);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gcMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmMapKeHoach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gán kế hoạch cho nhóm";
            this.Load += new System.EventHandler(this.frmMapKeHoach_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMap;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMap;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookNhom;
        private DevExpress.XtraGrid.Columns.GridColumn colNhomNV;
        private DevExpress.XtraGrid.Columns.GridColumn colKeHoach;
    }
}