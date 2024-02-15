namespace DichVu.MatBang.ViewMap.ViewMap
{
    partial class frmViewMap22
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
            DevExpress.XtraMap.CartesianMapCoordinateSystem cartesianMapCoordinateSystem1 = new DevExpress.XtraMap.CartesianMapCoordinateSystem();
            DevExpress.XtraMap.ColorListLegend colorListLegend1 = new DevExpress.XtraMap.ColorListLegend();
            DevExpress.XtraMap.MapOverlay mapOverlay1 = new DevExpress.XtraMap.MapOverlay();
            DevExpress.XtraMap.MapOverlayTextItem mapOverlayTextItem1 = new DevExpress.XtraMap.MapOverlayTextItem();
            DevExpress.XtraMap.MapOverlay mapOverlay2 = new DevExpress.XtraMap.MapOverlay();
            DevExpress.XtraMap.MapOverlayTextItem mapOverlayTextItem2 = new DevExpress.XtraMap.MapOverlayTextItem();
            DevExpress.XtraMap.CartesianSourceCoordinateSystem cartesianSourceCoordinateSystem1 = new DevExpress.XtraMap.CartesianSourceCoordinateSystem();
            DevExpress.XtraMap.KeyColorColorizer keyColorColorizer1 = new DevExpress.XtraMap.KeyColorColorizer();
            DevExpress.XtraMap.AttributeItemKeyProvider attributeItemKeyProvider1 = new DevExpress.XtraMap.AttributeItemKeyProvider();
            this.mapControl1 = new DevExpress.XtraMap.MapControl();
            this.shapefileDataAdapter1 = new DevExpress.XtraMap.ShapefileDataAdapter();
            this.vectorItemsLayer1 = new DevExpress.XtraMap.VectorItemsLayer();
            this.colorPickEdit1 = new DevExpress.XtraEditors.ColorPickEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // mapControl1
            // 
            this.mapControl1.CenterPoint = new DevExpress.XtraMap.GeoPoint(-50D, -90D);
            cartesianMapCoordinateSystem1.MeasureUnit = DevExpress.XtraMap.MeasureUnit.Meter;
            this.mapControl1.CoordinateSystem = cartesianMapCoordinateSystem1;
            this.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl1.Layers.Add(this.vectorItemsLayer1);
            colorListLegend1.Header = "Phòng";
            colorListLegend1.Layer = this.vectorItemsLayer1;
            this.mapControl1.Legends.Add(colorListLegend1);
            this.mapControl1.Location = new System.Drawing.Point(0, 29);
            this.mapControl1.MapEditor.ShowEditorPanel = true;
            this.mapControl1.Name = "mapControl1";
            mapOverlayTextItem1.Text = "Fill:";
            mapOverlay1.Items.Add(mapOverlayTextItem1);
            mapOverlay1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            mapOverlay1.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
            mapOverlayTextItem2.Text = "Stroke:";
            mapOverlay2.Items.Add(mapOverlayTextItem2);
            mapOverlay2.Location = new System.Drawing.Point(600, 0);
            mapOverlay2.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            mapOverlay2.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this.mapControl1.Overlays.Add(mapOverlay1);
            this.mapControl1.Overlays.Add(mapOverlay2);
            this.mapControl1.Size = new System.Drawing.Size(1017, 511);
            this.mapControl1.TabIndex = 0;
            this.mapControl1.ZoomLevel = 2D;
            this.shapefileDataAdapter1.FileUri = new System.Uri("C:\\Users\\Public\\Documents\\DevExpress Demos 18.2\\Components\\Data\\Hotels\\Hotel1.shp" +
        "", System.UriKind.Absolute);
            this.shapefileDataAdapter1.SourceCoordinateSystem = cartesianSourceCoordinateSystem1;
            attributeItemKeyProvider1.AttributeName = "CATEGORY";
            keyColorColorizer1.ItemKeyProvider = attributeItemKeyProvider1;
            keyColorColorizer1.PredefinedColorSchema = DevExpress.XtraMap.PredefinedColorSchema.Palette;
            this.vectorItemsLayer1.Colorizer = keyColorColorizer1;
            this.vectorItemsLayer1.Data = this.shapefileDataAdapter1;
            // 
            // colorPickEdit1
            // 
            this.colorPickEdit1.EditValue = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(80)))));
            this.colorPickEdit1.Location = new System.Drawing.Point(532, 48);
            this.colorPickEdit1.Name = "colorPickEdit1";
            this.colorPickEdit1.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.colorPickEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorPickEdit1.Size = new System.Drawing.Size(72, 20);
            this.colorPickEdit1.TabIndex = 1;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1});
            this.barManager1.MaxItemId = 1;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1017, 29);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 540);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1017, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 511);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1017, 29);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 511);
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Lưu";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // frmViewMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 540);
            this.Controls.Add(this.colorPickEdit1);
            this.Controls.Add(this.mapControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmViewMap";
            this.Text = "frmViewMap";
            this.Load += new System.EventHandler(this.frmViewMap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraMap.MapControl mapControl1;
        private DevExpress.XtraMap.VectorItemsLayer vectorItemsLayer1;
        private DevExpress.XtraMap.ShapefileDataAdapter shapefileDataAdapter1;
        private DevExpress.XtraEditors.ColorPickEdit colorPickEdit1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    }
}