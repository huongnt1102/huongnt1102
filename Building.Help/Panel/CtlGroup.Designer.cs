namespace Building.Help.Panel
{
    partial class CtlGroup
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtlGroup));
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElementFooterMenu = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement4 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElementFooterBack = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.ctlTaiLieu1 = new Document.ctlTaiLieu();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElementFooterMenu,
            this.accordionControlElementFooterBack});
            this.accordionControl1.Images = this.imageCollection1;
            this.accordionControl1.Location = new System.Drawing.Point(0, 0);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.RootDisplayMode = DevExpress.XtraBars.Navigation.AccordionControlRootDisplayMode.Footer;
            this.accordionControl1.ShowFilterControl = DevExpress.XtraBars.Navigation.ShowFilterControl.Always;
            this.accordionControl1.Size = new System.Drawing.Size(250, 463);
            this.accordionControl1.TabIndex = 0;
            this.accordionControl1.Text = "accordionControl1";
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlElementFooterMenu
            // 
            this.accordionControlElementFooterMenu.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement4});
            this.accordionControlElementFooterMenu.Expanded = true;
            this.accordionControlElementFooterMenu.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("accordionControlElementFooterMenu.ImageOptions.SvgImage")));
            this.accordionControlElementFooterMenu.Name = "accordionControlElementFooterMenu";
            this.accordionControlElementFooterMenu.Text = "MENU";
            // 
            // accordionControlElement4
            // 
            this.accordionControlElement4.ImageOptions.ImageIndex = 0;
            this.accordionControlElement4.Name = "accordionControlElement4";
            this.accordionControlElement4.Text = "Element4";
            // 
            // accordionControlElementFooterBack
            // 
            this.accordionControlElementFooterBack.Expanded = true;
            this.accordionControlElementFooterBack.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("accordionControlElementFooterBack.ImageOptions.SvgImage")));
            this.accordionControlElementFooterBack.Name = "accordionControlElementFooterBack";
            this.accordionControlElementFooterBack.Text = "BACK";
            this.accordionControlElementFooterBack.Click += new System.EventHandler(this.accordionControlElementFooterBack_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "admin.png");
            this.imageCollection1.Images.SetKeyName(1, "app.png");
            this.imageCollection1.Images.SetKeyName(2, "bàn giao khách hàng.png");
            this.imageCollection1.Images.SetKeyName(3, "bàn giao nội bộ.png");
            this.imageCollection1.Images.SetKeyName(4, "báo cáo & thống kê.png");
            this.imageCollection1.Images.SetKeyName(5, "căn hộ.png");
            this.imageCollection1.Images.SetKeyName(6, "cơ hội.png");
            this.imageCollection1.Images.SetKeyName(7, "cư dân.png");
            this.imageCollection1.Images.SetKeyName(8, "CV.png");
            this.imageCollection1.Images.SetKeyName(9, "checklist.png");
            this.imageCollection1.Images.SetKeyName(10, "danh mục.png");
            this.imageCollection1.Images.SetKeyName(11, "dự án.png");
            this.imageCollection1.Images.SetKeyName(12, "giao diện.png");
            this.imageCollection1.Images.SetKeyName(13, "hệ thống.png");
            this.imageCollection1.Images.SetKeyName(14, "hóa đơn.png");
            this.imageCollection1.Images.SetKeyName(15, "hợp đồng phụ lục.png");
            this.imageCollection1.Images.SetKeyName(16, "hợp đồng.png");
            this.imageCollection1.Images.SetKeyName(17, "kết thúc.png");
            this.imageCollection1.Images.SetKeyName(18, "kinh phí dự trù.png");
            this.imageCollection1.Images.SetKeyName(19, "lễ tân.png");
            this.imageCollection1.Images.SetKeyName(20, "mẫu in, báo cáo.png");
            this.imageCollection1.Images.SetKeyName(21, "nhân viên.png");
            this.imageCollection1.Images.SetKeyName(22, "phương tiện.png");
            this.imageCollection1.Images.SetKeyName(23, "sổ quỹ.png");
            this.imageCollection1.Images.SetKeyName(24, "tài liệu.png");
            this.imageCollection1.Images.SetKeyName(25, "tiến trình.png");
            this.imageCollection1.Images.SetKeyName(26, "TS.png");
            this.imageCollection1.Images.SetKeyName(27, "TT.png");
            this.imageCollection1.Images.SetKeyName(28, "yêu cầu.png");
            // 
            // ctlTaiLieu1
            // 
            this.ctlTaiLieu1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlTaiLieu1.FormID = null;
            this.ctlTaiLieu1.LinkID = null;
            this.ctlTaiLieu1.Location = new System.Drawing.Point(250, 0);
            this.ctlTaiLieu1.MaNV = null;
            this.ctlTaiLieu1.Name = "ctlTaiLieu1";
            this.ctlTaiLieu1.Size = new System.Drawing.Size(539, 463);
            this.ctlTaiLieu1.TabIndex = 1;
            // 
            // CtlGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctlTaiLieu1);
            this.Controls.Add(this.accordionControl1);
            this.Name = "CtlGroup";
            this.Size = new System.Drawing.Size(789, 463);
            this.Load += new System.EventHandler(this.CtlGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElementFooterMenu;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement4;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElementFooterBack;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private Document.ctlTaiLieu ctlTaiLieu1;
    }
}
