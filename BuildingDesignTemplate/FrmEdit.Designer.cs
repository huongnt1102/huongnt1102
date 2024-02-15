namespace BuildingDesignTemplate
{
    partial class FrmEdit
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.glkFormGroup = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnImageLogo = new DevExpress.XtraEditors.ButtonEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.spinTop = new DevExpress.XtraEditors.SpinEdit();
            this.spinBottom = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.spinRight = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.spinLeft = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpAction = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.chkLogoDIP = new DevExpress.XtraEditors.CheckEdit();
            this.chkBarcode = new DevExpress.XtraEditors.CheckEdit();
            this.chkLogoCty = new DevExpress.XtraEditors.CheckEdit();
            this.ckbIsUseApartment = new DevExpress.XtraEditors.CheckEdit();
            this.txtFormName = new DevExpress.XtraEditors.ButtonEdit();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnReview = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkFormGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImageLogo.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinTop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinBottom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLeft.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpAction.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLogoDIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBarcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLogoCty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsUseApartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.glkFormGroup);
            this.panelControl1.Controls.Add(this.btnImageLogo);
            this.panelControl1.Controls.Add(this.groupBox1);
            this.panelControl1.Controls.Add(this.lookUpAction);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.chkLogoDIP);
            this.panelControl1.Controls.Add(this.chkBarcode);
            this.panelControl1.Controls.Add(this.chkLogoCty);
            this.panelControl1.Controls.Add(this.ckbIsUseApartment);
            this.panelControl1.Controls.Add(this.txtFormName);
            this.panelControl1.Controls.Add(this.txtDescription);
            this.panelControl1.Controls.Add(this.labelControl10);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(516, 298);
            this.panelControl1.TabIndex = 0;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            // 
            // glkFormGroup
            // 
            this.glkFormGroup.Location = new System.Drawing.Point(95, 39);
            this.glkFormGroup.Name = "glkFormGroup";
            this.glkFormGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkFormGroup.Properties.DisplayMember = "Name";
            this.glkFormGroup.Properties.NullText = "";
            this.glkFormGroup.Properties.PopupView = this.gridLookUpEdit1View;
            this.glkFormGroup.Properties.ValueMember = "ID";
            this.glkFormGroup.Size = new System.Drawing.Size(408, 20);
            this.glkFormGroup.TabIndex = 11;
            this.glkFormGroup.EditValueChanged += new System.EventHandler(this.GlkFormGroup_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Nhóm biểu mẫu";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // btnImageLogo
            // 
            this.btnImageLogo.Location = new System.Drawing.Point(221, 186);
            this.btnImageLogo.Name = "btnImageLogo";
            this.btnImageLogo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnImageLogo.Properties.ReadOnly = true;
            this.btnImageLogo.Size = new System.Drawing.Size(282, 20);
            this.btnImageLogo.TabIndex = 10;
            this.btnImageLogo.Visible = false;
            this.btnImageLogo.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnImageLogo_ButtonClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.spinTop);
            this.groupBox1.Controls.Add(this.spinBottom);
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.spinRight);
            this.groupBox1.Controls.Add(this.labelControl7);
            this.groupBox1.Controls.Add(this.spinLeft);
            this.groupBox1.Controls.Add(this.labelControl6);
            this.groupBox1.Controls.Add(this.labelControl8);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(16, 220);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(487, 70);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Canh lề trang";
            // 
            // spinTop
            // 
            this.spinTop.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinTop.Location = new System.Drawing.Point(92, 43);
            this.spinTop.Name = "spinTop";
            this.spinTop.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinTop.Properties.DisplayFormat.FormatString = "n0";
            this.spinTop.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTop.Properties.EditFormat.FormatString = "n0";
            this.spinTop.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTop.Properties.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spinTop.Size = new System.Drawing.Size(100, 20);
            this.spinTop.TabIndex = 10;
            // 
            // spinBottom
            // 
            this.spinBottom.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinBottom.Location = new System.Drawing.Point(378, 43);
            this.spinBottom.Name = "spinBottom";
            this.spinBottom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinBottom.Properties.DisplayFormat.FormatString = "n0";
            this.spinBottom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinBottom.Properties.EditFormat.FormatString = "n0";
            this.spinBottom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinBottom.Properties.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spinBottom.Size = new System.Drawing.Size(100, 20);
            this.spinBottom.TabIndex = 10;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(15, 20);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(71, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Bên trái (Left):";
            // 
            // spinRight
            // 
            this.spinRight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinRight.Location = new System.Drawing.Point(378, 17);
            this.spinRight.Name = "spinRight";
            this.spinRight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinRight.Properties.DisplayFormat.FormatString = "n0";
            this.spinRight.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinRight.Properties.EditFormat.FormatString = "n0";
            this.spinRight.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinRight.Properties.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spinRight.Size = new System.Drawing.Size(100, 20);
            this.spinRight.TabIndex = 10;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(15, 46);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(74, 13);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "Bên trên (Top):";
            // 
            // spinLeft
            // 
            this.spinLeft.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinLeft.Location = new System.Drawing.Point(92, 17);
            this.spinLeft.Name = "spinLeft";
            this.spinLeft.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinLeft.Properties.DisplayFormat.FormatString = "n0";
            this.spinLeft.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinLeft.Properties.EditFormat.FormatString = "n0";
            this.spinLeft.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinLeft.Properties.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spinLeft.Size = new System.Drawing.Size(100, 20);
            this.spinLeft.TabIndex = 10;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(278, 20);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(81, 13);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "Bên phải (Right):";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(278, 46);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(91, 13);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "Bên dưới (Bottom):";
            // 
            // lookUpAction
            // 
            this.lookUpAction.Location = new System.Drawing.Point(94, 67);
            this.lookUpAction.Name = "lookUpAction";
            this.lookUpAction.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpAction.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name1")});
            this.lookUpAction.Properties.DisplayMember = "Name";
            this.lookUpAction.Properties.NullText = "";
            this.lookUpAction.Properties.ShowHeader = false;
            this.lookUpAction.Properties.ValueMember = "Id";
            this.lookUpAction.Size = new System.Drawing.Size(409, 20);
            this.lookUpAction.TabIndex = 7;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(16, 70);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(46, 13);
            this.labelControl9.TabIndex = 6;
            this.labelControl9.Text = "Thao tác:";
            // 
            // chkLogoDIP
            // 
            this.chkLogoDIP.Location = new System.Drawing.Point(389, 161);
            this.chkLogoDIP.Name = "chkLogoDIP";
            this.chkLogoDIP.Properties.Caption = "Sử dụng logo DIP";
            this.chkLogoDIP.Size = new System.Drawing.Size(114, 19);
            this.chkLogoDIP.TabIndex = 5;
            this.chkLogoDIP.Visible = false;
            // 
            // chkBarcode
            // 
            this.chkBarcode.Location = new System.Drawing.Point(221, 161);
            this.chkBarcode.Name = "chkBarcode";
            this.chkBarcode.Properties.Caption = "Sử dụng Barcode";
            this.chkBarcode.Size = new System.Drawing.Size(114, 19);
            this.chkBarcode.TabIndex = 5;
            this.chkBarcode.Visible = false;
            // 
            // chkLogoCty
            // 
            this.chkLogoCty.Location = new System.Drawing.Point(92, 186);
            this.chkLogoCty.Name = "chkLogoCty";
            this.chkLogoCty.Properties.Caption = "Sử dụng logo cty";
            this.chkLogoCty.Size = new System.Drawing.Size(116, 19);
            this.chkLogoCty.TabIndex = 5;
            this.chkLogoCty.Visible = false;
            // 
            // ckbIsUseApartment
            // 
            this.ckbIsUseApartment.Location = new System.Drawing.Point(92, 161);
            this.ckbIsUseApartment.Name = "ckbIsUseApartment";
            this.ckbIsUseApartment.Properties.Caption = "In riêng từng mặt bằng";
            this.ckbIsUseApartment.Size = new System.Drawing.Size(132, 19);
            this.ckbIsUseApartment.TabIndex = 5;
            // 
            // txtFormName
            // 
            this.txtFormName.Location = new System.Drawing.Point(95, 13);
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Biểu mẫu", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.txtFormName.Size = new System.Drawing.Size(408, 20);
            this.txtFormName.TabIndex = 4;
            this.txtFormName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtTenBM_ButtonClick);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(94, 93);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(409, 62);
            this.txtDescription.TabIndex = 2;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(18, 163);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(48, 13);
            this.labelControl10.TabIndex = 0;
            this.labelControl10.Text = "Tùy chọn:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 96);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Diễn giải:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(16, 42);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(69, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Loại biểu mẫu:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên biểu mẫu:";
            // 
            // btnReview
            // 
            this.btnReview.Location = new System.Drawing.Point(249, 319);
            this.btnReview.Name = "btnReview";
            this.btnReview.Size = new System.Drawing.Size(96, 23);
            this.btnReview.TabIndex = 1;
            this.btnReview.Text = "Xem trước";
            this.btnReview.Click += new System.EventHandler(this.btnReview_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(351, 319);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(96, 23);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu && Đóng";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(453, 319);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FrmEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 350);
            this.Controls.Add(this.btnReview);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Biểu mẫu";
            this.Load += new System.EventHandler(this.frmEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkFormGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImageLogo.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinTop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinBottom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLeft.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpAction.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLogoDIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBarcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLogoCty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsUseApartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtDescription;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ButtonEdit txtFormName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit ckbIsUseApartment;
        private DevExpress.XtraEditors.LookUpEdit lookUpAction;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.SpinEdit spinBottom;
        private DevExpress.XtraEditors.SpinEdit spinRight;
        private DevExpress.XtraEditors.SpinEdit spinTop;
        private DevExpress.XtraEditors.SpinEdit spinLeft;
        private DevExpress.XtraEditors.CheckEdit chkBarcode;
        private DevExpress.XtraEditors.CheckEdit chkLogoCty;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.SimpleButton btnReview;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.CheckEdit chkLogoDIP;
        private DevExpress.XtraEditors.ButtonEdit btnImageLogo;
        private DevExpress.XtraEditors.GridLookUpEdit glkFormGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}