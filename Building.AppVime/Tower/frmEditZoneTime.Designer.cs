namespace Building.AppVime.Tower
{
    partial class frmEditZoneTime
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
            this.btnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.spinMinuteTo = new DevExpress.XtraEditors.SpinEdit();
            this.spinMinuteFrom = new DevExpress.XtraEditors.SpinEdit();
            this.spinHourTo = new DevExpress.XtraEditors.SpinEdit();
            this.spinHourFrom = new DevExpress.XtraEditors.SpinEdit();
            this.spinNumberIndex = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMinuteTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMinuteFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHourTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHourFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNumberIndex.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Save_as_icon;
            this.btnAccept.Location = new System.Drawing.Point(168, 131);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(91, 23);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "Lưu && Đóng";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.Image = global::Building.AppVime.Properties.Resources.Delete_icon;
            this.btnCancel.Location = new System.Drawing.Point(265, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtName);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.spinMinuteTo);
            this.panelControl1.Controls.Add(this.spinMinuteFrom);
            this.panelControl1.Controls.Add(this.spinHourTo);
            this.panelControl1.Controls.Add(this.spinHourFrom);
            this.panelControl1.Controls.Add(this.spinNumberIndex);
            this.panelControl1.Location = new System.Drawing.Point(10, 11);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(329, 114);
            this.panelControl1.TabIndex = 6;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(94, 12);
            this.txtName.Name = "txtName";
            this.txtName.Properties.MaxLength = 49;
            this.txtName.Size = new System.Drawing.Size(223, 20);
            this.txtName.TabIndex = 8;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(15, 14);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(64, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Tên Suất (*):";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(227, 88);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(4, 13);
            this.labelControl6.TabIndex = 2;
            this.labelControl6.Text = ":";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(227, 63);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(4, 13);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = ":";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(15, 88);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(68, 13);
            this.labelControl5.TabIndex = 2;
            this.labelControl5.Text = "Đến thời gian:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 63);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(61, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Từ thời gian:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 39);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Số thứ tự:";
            // 
            // spinMinuteTo
            // 
            this.spinMinuteTo.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinMinuteTo.Location = new System.Drawing.Point(237, 85);
            this.spinMinuteTo.Name = "spinMinuteTo";
            this.spinMinuteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinMinuteTo.Properties.DisplayFormat.FormatString = "{0:N0} phút";
            this.spinMinuteTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinMinuteTo.Properties.EditFormat.FormatString = "{0:N0}";
            this.spinMinuteTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinMinuteTo.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinMinuteTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.spinMinuteTo.Properties.MaxValue = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.spinMinuteTo.Size = new System.Drawing.Size(80, 20);
            this.spinMinuteTo.TabIndex = 8;
            // 
            // spinMinuteFrom
            // 
            this.spinMinuteFrom.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinMinuteFrom.Location = new System.Drawing.Point(237, 61);
            this.spinMinuteFrom.Name = "spinMinuteFrom";
            this.spinMinuteFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinMinuteFrom.Properties.DisplayFormat.FormatString = "{0:N0} phút";
            this.spinMinuteFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinMinuteFrom.Properties.EditFormat.FormatString = "{0:N0}";
            this.spinMinuteFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinMinuteFrom.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinMinuteFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.spinMinuteFrom.Properties.MaxValue = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.spinMinuteFrom.Size = new System.Drawing.Size(80, 20);
            this.spinMinuteFrom.TabIndex = 8;
            // 
            // spinHourTo
            // 
            this.spinHourTo.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinHourTo.Location = new System.Drawing.Point(94, 85);
            this.spinHourTo.Name = "spinHourTo";
            this.spinHourTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinHourTo.Properties.DisplayFormat.FormatString = "{0:N0} giờ";
            this.spinHourTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinHourTo.Properties.EditFormat.FormatString = "{0:N0}";
            this.spinHourTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinHourTo.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinHourTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.spinHourTo.Properties.MaxValue = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.spinHourTo.Size = new System.Drawing.Size(128, 20);
            this.spinHourTo.TabIndex = 8;
            // 
            // spinHourFrom
            // 
            this.spinHourFrom.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinHourFrom.Location = new System.Drawing.Point(94, 61);
            this.spinHourFrom.Name = "spinHourFrom";
            this.spinHourFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinHourFrom.Properties.DisplayFormat.FormatString = "{0:N0} giờ";
            this.spinHourFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinHourFrom.Properties.EditFormat.FormatString = "{0:N0}";
            this.spinHourFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinHourFrom.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinHourFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.spinHourFrom.Properties.MaxValue = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.spinHourFrom.Properties.MinValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinHourFrom.Size = new System.Drawing.Size(128, 20);
            this.spinHourFrom.TabIndex = 8;
            // 
            // spinNumberIndex
            // 
            this.spinNumberIndex.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinNumberIndex.Location = new System.Drawing.Point(94, 37);
            this.spinNumberIndex.Name = "spinNumberIndex";
            this.spinNumberIndex.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinNumberIndex.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinNumberIndex.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.spinNumberIndex.Properties.MaxValue = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.spinNumberIndex.Size = new System.Drawing.Size(223, 20);
            this.spinNumberIndex.TabIndex = 8;
            // 
            // frmEditZoneTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 163);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditZoneTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt Suất";
            this.Load += new System.EventHandler(this.frmEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMinuteTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMinuteFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHourTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHourFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNumberIndex.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnAccept;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.SpinEdit spinNumberIndex;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit spinMinuteTo;
        private DevExpress.XtraEditors.SpinEdit spinMinuteFrom;
        private DevExpress.XtraEditors.SpinEdit spinHourTo;
        private DevExpress.XtraEditors.SpinEdit spinHourFrom;
    }
}