namespace DichVu.HoaDon
{
    partial class frmSelectMonth
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
            this.cbmThang = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbmNam = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnCHon = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cbmThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbmNam.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cbmThang
            // 
            this.cbmThang.Location = new System.Drawing.Point(12, 12);
            this.cbmThang.Name = "cbmThang";
            this.cbmThang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbmThang.Properties.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cbmThang.Properties.NullText = "[Tháng]";
            this.cbmThang.Size = new System.Drawing.Size(79, 20);
            this.cbmThang.TabIndex = 0;
            // 
            // cbmNam
            // 
            this.cbmNam.Location = new System.Drawing.Point(97, 12);
            this.cbmNam.Name = "cbmNam";
            this.cbmNam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbmNam.Properties.Items.AddRange(new object[] {
            "2010",
            "2011",
            "2012",
            "2013",
            "2014",
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030",
            "2031",
            "2032",
            "2033",
            "2034",
            "2035",
            "2036",
            "2037",
            "2038",
            "2039",
            "2040",
            "2041",
            "2042",
            "2043",
            "2044",
            "2045",
            "2046",
            "2047",
            "2048",
            "2049",
            "2050"});
            this.cbmNam.Properties.NullText = "[Năm]";
            this.cbmNam.Size = new System.Drawing.Size(86, 20);
            this.cbmNam.TabIndex = 1;
            // 
            // btnCHon
            // 
            this.btnCHon.Location = new System.Drawing.Point(189, 9);
            this.btnCHon.Name = "btnCHon";
            this.btnCHon.Size = new System.Drawing.Size(48, 23);
            this.btnCHon.TabIndex = 2;
            this.btnCHon.Text = "Chọn";
            this.btnCHon.Click += new System.EventHandler(this.btnCHon_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(243, 9);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(48, 23);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmSelectMonth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 46);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnCHon);
            this.Controls.Add(this.cbmNam);
            this.Controls.Add(this.cbmThang);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectMonth";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn tháng thanh toán";
            this.Load += new System.EventHandler(this.frmSelectMonth_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbmThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbmNam.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cbmThang;
        private DevExpress.XtraEditors.ComboBoxEdit cbmNam;
        private DevExpress.XtraEditors.SimpleButton btnCHon;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
    }
}