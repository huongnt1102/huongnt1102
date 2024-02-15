namespace Document
{
    partial class frmLoaiTaiLieu
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
            this.gcLoaiTaiLieu = new DevExpress.XtraGrid.GridControl();
            this.grvLoaiTaiLieu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcLoaiTaiLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLoaiTaiLieu)).BeginInit();
            this.SuspendLayout();
            // 
            // gcLoaiTaiLieu
            // 
            this.gcLoaiTaiLieu.Location = new System.Drawing.Point(12, 12);
            this.gcLoaiTaiLieu.MainView = this.grvLoaiTaiLieu;
            this.gcLoaiTaiLieu.Name = "gcLoaiTaiLieu";
            this.gcLoaiTaiLieu.ShowOnlyPredefinedDetails = true;
            this.gcLoaiTaiLieu.Size = new System.Drawing.Size(538, 237);
            this.gcLoaiTaiLieu.TabIndex = 0;
            this.gcLoaiTaiLieu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvLoaiTaiLieu});
            // 
            // grvLoaiTaiLieu
            // 
            this.grvLoaiTaiLieu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn1});
            this.grvLoaiTaiLieu.GridControl = this.gcLoaiTaiLieu;
            this.grvLoaiTaiLieu.Name = "grvLoaiTaiLieu";
            this.grvLoaiTaiLieu.OptionsCustomization.AllowGroup = false;
            this.grvLoaiTaiLieu.OptionsSelection.MultiSelect = true;
            this.grvLoaiTaiLieu.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvLoaiTaiLieu.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "STT";
            this.gridColumn2.FieldName = "STT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 47;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên loại tài liệu";
            this.gridColumn1.FieldName = "TenLTL";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 470;
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(475, 255);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(376, 255);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(93, 23);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu && Đóng";
            // 
            // frmLoaiTaiLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 290);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.gcLoaiTaiLieu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmLoaiTaiLieu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Loại tài liệu";
            ((System.ComponentModel.ISupportInitialize)(this.gcLoaiTaiLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLoaiTaiLieu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcLoaiTaiLieu;
        private DevExpress.XtraGrid.Views.Grid.GridView grvLoaiTaiLieu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}