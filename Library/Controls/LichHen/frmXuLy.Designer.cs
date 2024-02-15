namespace Library.Controls.LichHen
{
    partial class frmXuLy
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
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.txtNoiDung = new DevExpress.XtraEditors.MemoEdit();
			this.lkTrangThai = new DevExpress.XtraEditors.LookUpEdit();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtNoiDung.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkTrangThai.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.btnCancel);
			this.layoutControl1.Controls.Add(this.btnSave);
			this.layoutControl1.Controls.Add(this.txtNoiDung);
			this.layoutControl1.Controls.Add(this.lkTrangThai);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(403, 261);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(299, 227);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(92, 22);
			this.btnCancel.StyleController = this.layoutControl1;
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Hủy";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(203, 227);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(92, 22);
			this.btnSave.StyleController = this.layoutControl1;
			this.btnSave.TabIndex = 6;
			this.btnSave.Text = "Lưu";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// txtNoiDung
			// 
			this.txtNoiDung.Location = new System.Drawing.Point(64, 36);
			this.txtNoiDung.Name = "txtNoiDung";
			this.txtNoiDung.Size = new System.Drawing.Size(327, 187);
			this.txtNoiDung.StyleController = this.layoutControl1;
			this.txtNoiDung.TabIndex = 5;
			// 
			// lkTrangThai
			// 
			this.lkTrangThai.Location = new System.Drawing.Point(64, 12);
			this.lkTrangThai.Name = "lkTrangThai";
			this.lkTrangThai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkTrangThai.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTrangThai", "Trạng thái")});
			this.lkTrangThai.Properties.DisplayMember = "TenTrangThai";
			this.lkTrangThai.Properties.NullText = "Chọn trạng thái";
			this.lkTrangThai.Properties.ValueMember = "ID";
			this.lkTrangThai.Size = new System.Drawing.Size(327, 20);
			this.lkTrangThai.StyleController = this.layoutControl1;
			this.lkTrangThai.TabIndex = 4;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem4});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(403, 261);
			this.layoutControlGroup1.Text = "layoutControlGroup1";
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.lkTrangThai;
			this.layoutControlItem1.CustomizationFormText = "Trạng thái";
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(383, 24);
			this.layoutControlItem1.Text = "Trạng thái";
			this.layoutControlItem1.TextSize = new System.Drawing.Size(49, 13);
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.txtNoiDung;
			this.layoutControlItem2.CustomizationFormText = "Ghi chú";
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(383, 191);
			this.layoutControlItem2.Text = "Ghi chú";
			this.layoutControlItem2.TextSize = new System.Drawing.Size(49, 13);
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.btnSave;
			this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
			this.layoutControlItem3.Location = new System.Drawing.Point(191, 215);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(96, 26);
			this.layoutControlItem3.Text = "layoutControlItem3";
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextToControlDistance = 0;
			this.layoutControlItem3.TextVisible = false;
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 215);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(191, 26);
			this.emptySpaceItem1.Text = "emptySpaceItem1";
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.btnCancel;
			this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
			this.layoutControlItem4.Location = new System.Drawing.Point(287, 215);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(96, 26);
			this.layoutControlItem4.Text = "layoutControlItem4";
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextToControlDistance = 0;
			this.layoutControlItem4.TextVisible = false;
			// 
			// frmXuLy
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(403, 261);
			this.Controls.Add(this.layoutControl1);
			this.Name = "frmXuLy";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Xử lý";
			this.Load += new System.EventHandler(this.frmXuLy_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtNoiDung.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkTrangThai.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.LookUpEdit lkTrangThai;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.MemoEdit txtNoiDung;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}