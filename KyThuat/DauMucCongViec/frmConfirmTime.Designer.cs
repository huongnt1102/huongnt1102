namespace KyThuat.DauMucCongViec
{
    partial class frmConfirmTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfirmTime));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateTGXN = new DevExpress.XtraEditors.DateEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.timeGioXN = new DevExpress.XtraEditors.TimeEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDienGiai = new DevExpress.XtraEditors.MemoEdit();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.dateTGXN.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTGXN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeGioXN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.ImageIndex = 1;
            this.btnCancel.ImageOptions.ImageList = this.imageCollection1;
            this.btnCancel.Location = new System.Drawing.Point(267, 147);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(63, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Thời gian XN:";
            // 
            // dateTGXN
            // 
            this.dateTGXN.EditValue = null;
            this.dateTGXN.Location = new System.Drawing.Point(88, 14);
            this.dateTGXN.Name = "dateTGXN";
            this.dateTGXN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTGXN.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTGXN.Properties.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateTGXN.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTGXN.Properties.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateTGXN.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTGXN.Size = new System.Drawing.Size(107, 20);
            this.dateTGXN.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.ImageIndex = 0;
            this.btnSave.ImageOptions.ImageList = this.imageCollection1;
            this.btnSave.Location = new System.Drawing.Point(186, 147);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Lưu nhận";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.timeGioXN);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.dateTGXN);
            this.panelControl1.Controls.Add(this.txtDienGiai);
            this.panelControl1.Location = new System.Drawing.Point(7, 7);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(343, 134);
            this.panelControl1.TabIndex = 5;
            // 
            // timeGioXN
            // 
            this.timeGioXN.EditValue = new System.DateTime(2014, 4, 23, 0, 0, 0, 0);
            this.timeGioXN.Location = new System.Drawing.Point(234, 14);
            this.timeGioXN.Name = "timeGioXN";
            this.timeGioXN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeGioXN.Properties.DisplayFormat.FormatString = "{0:hh:mm tt}";
            this.timeGioXN.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.timeGioXN.Properties.EditFormat.FormatString = "{0:hh:mm tt}";
            this.timeGioXN.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.timeGioXN.Size = new System.Drawing.Size(100, 20);
            this.timeGioXN.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(203, 17);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(19, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Giờ:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 43);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Diễn giải:";
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Location = new System.Drawing.Point(88, 40);
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(247, 89);
            this.txtDienGiai.TabIndex = 3;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            // 
            // frmConfirmTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 174);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfirmTime";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thời gian khách hàng xác nhận thực hiện công việc";
            this.Load += new System.EventHandler(this.frmConfirmTime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dateTGXN.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTGXN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeGioXN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateTGXN;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoEdit txtDienGiai;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TimeEdit timeGioXN;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}