namespace Building.AppVime
{
    partial class frmVer
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.numAndroidVer = new DevExpress.XtraEditors.SpinEdit();
            this.numIOSVer = new DevExpress.XtraEditors.SpinEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.lbIOS = new System.Windows.Forms.Label();
            this.lbAndroid = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numAndroidVer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIOSVer.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(11, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Vesion IOS (*):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(11, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vesion Android (*):";
            // 
            // btnCancel
            // 
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(342, 114);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(254, 114);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 27);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Lưu && Đóng";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // numAndroidVer
            // 
            this.numAndroidVer.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numAndroidVer.Location = new System.Drawing.Point(116, 39);
            this.numAndroidVer.Name = "numAndroidVer";
            this.numAndroidVer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numAndroidVer.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numAndroidVer.Size = new System.Drawing.Size(186, 20);
            this.numAndroidVer.TabIndex = 66;
            // 
            // numIOSVer
            // 
            this.numIOSVer.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numIOSVer.Location = new System.Drawing.Point(116, 76);
            this.numIOSVer.Name = "numIOSVer";
            this.numIOSVer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numIOSVer.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numIOSVer.Size = new System.Drawing.Size(186, 20);
            this.numIOSVer.TabIndex = 67;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(311, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 68;
            this.label3.Text = "Phiên bản mới nhất";
            // 
            // lbIOS
            // 
            this.lbIOS.AutoSize = true;
            this.lbIOS.ForeColor = System.Drawing.Color.Red;
            this.lbIOS.Location = new System.Drawing.Point(308, 80);
            this.lbIOS.Name = "lbIOS";
            this.lbIOS.Size = new System.Drawing.Size(13, 13);
            this.lbIOS.TabIndex = 69;
            this.lbIOS.Text = "0";
            // 
            // lbAndroid
            // 
            this.lbAndroid.AutoSize = true;
            this.lbAndroid.ForeColor = System.Drawing.Color.Red;
            this.lbAndroid.Location = new System.Drawing.Point(308, 43);
            this.lbAndroid.Name = "lbAndroid";
            this.lbAndroid.Size = new System.Drawing.Size(13, 13);
            this.lbAndroid.TabIndex = 70;
            this.lbAndroid.Text = "0";
            // 
            // frmVer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 145);
            this.Controls.Add(this.lbAndroid);
            this.Controls.Add(this.lbIOS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numIOSVer);
            this.Controls.Add(this.numAndroidVer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CẬP NHẬT VERSION APP";
            this.Load += new System.EventHandler(this.frmVer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numAndroidVer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIOSVer.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private DevExpress.XtraEditors.SpinEdit numAndroidVer;
        private DevExpress.XtraEditors.SpinEdit numIOSVer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbIOS;
        private System.Windows.Forms.Label lbAndroid;
    }
}