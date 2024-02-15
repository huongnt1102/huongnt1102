namespace DichVu.MatBang.ViewMap.ViewMap
{
    partial class FrmViewMap3
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
            DevExpress.XtraMap.KeyColorColorizer keyColorColorizer1 = new DevExpress.XtraMap.KeyColorColorizer();
            DevExpress.XtraMap.AttributeItemKeyProvider attributeItemKeyProvider1 = new DevExpress.XtraMap.AttributeItemKeyProvider();
            DevExpress.XtraMap.ColorListLegend colorListLegend1 = new DevExpress.XtraMap.ColorListLegend();
            DevExpress.XtraMap.MiniMap miniMap1 = new DevExpress.XtraMap.MiniMap();
            DevExpress.XtraMap.FixedMiniMapBehavior fixedMiniMapBehavior1 = new DevExpress.XtraMap.FixedMiniMapBehavior();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmViewMap3));
            DevExpress.XtraMap.KeyColorColorizer keyColorColorizer2 = new DevExpress.XtraMap.KeyColorColorizer();
            this.HotelsLayer = new DevExpress.XtraMap.VectorItemsLayer();
            this.HotelsItemStorage = new DevExpress.XtraMap.MapItemStorage();
            this.miniMapImageTilesLayer1 = new DevExpress.XtraMap.MiniMapImageTilesLayer();
            this.bingMapDataProvider1 = new DevExpress.XtraMap.BingMapDataProvider();
            this.miniMapVectorItemsLayer1 = new DevExpress.XtraMap.MiniMapVectorItemsLayer();
            this.mapItemStorage1 = new DevExpress.XtraMap.MapItemStorage();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.mapControl1 = new DevExpress.XtraMap.MapControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.TilesLayer = new DevExpress.XtraMap.ImageLayer();
            this.BingMapDataProvider = new DevExpress.XtraMap.BingMapDataProvider();
            this.HotelPlanLayer = new DevExpress.XtraMap.VectorItemsLayer();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            attributeItemKeyProvider1.AttributeName = "CATEGORY";
            keyColorColorizer1.ItemKeyProvider = attributeItemKeyProvider1;
            keyColorColorizer1.PredefinedColorSchema = DevExpress.XtraMap.PredefinedColorSchema.Palette;
            this.HotelsLayer.Colorizer = keyColorColorizer1;
            this.HotelsLayer.Data = this.HotelsItemStorage;
            this.miniMapImageTilesLayer1.DataProvider = this.bingMapDataProvider1;
            this.bingMapDataProvider1.BingKey = "YOUR BING MAPS KEY";
            this.miniMapVectorItemsLayer1.Data = this.mapItemStorage1;
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
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Nạp";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(813, 29);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 495);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(813, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 466);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(813, 29);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 466);
            // 
            // mapControl1
            // 
            this.mapControl1.CenterPoint = new DevExpress.XtraMap.GeoPoint(-21.1685D, -175.1343D);
            this.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl1.ImageList = this.imageCollection1;
            this.mapControl1.Layers.Add(this.TilesLayer);
            this.mapControl1.Layers.Add(this.HotelPlanLayer);
            this.mapControl1.Layers.Add(this.HotelsLayer);
            colorListLegend1.Layer = this.HotelsLayer;
            this.mapControl1.Legends.Add(colorListLegend1);
            this.mapControl1.Location = new System.Drawing.Point(0, 29);
            this.mapControl1.MapEditor.ShowEditorPanel = true;
            this.mapControl1.MaxZoomLevel = 15D;
            miniMap1.Alignment = DevExpress.XtraMap.MiniMapAlignment.BottomRight;
            fixedMiniMapBehavior1.CenterPoint = new DevExpress.XtraMap.GeoPoint(12.259328D, 109.13429D);
            miniMap1.Behavior = fixedMiniMapBehavior1;
            miniMap1.Layers.Add(this.miniMapImageTilesLayer1);
            miniMap1.Layers.Add(this.miniMapVectorItemsLayer1);
            this.mapControl1.MiniMap = miniMap1;
            this.mapControl1.MinZoomLevel = 13D;
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.Size = new System.Drawing.Size(813, 466);
            this.mapControl1.TabIndex = 4;
            this.mapControl1.ZoomLevel = 13D;
            this.mapControl1.DrawMapItem += new DevExpress.XtraMap.DrawMapItemEventHandler(this.mapControl1_DrawMapItem);
            this.mapControl1.MapItemClick += new DevExpress.XtraMap.MapItemClickEventHandler(this.mapControl1_MapItemClick);
            this.mapControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapControl1_MouseUp);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(83, 93);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Hotel_1.png");
            this.imageCollection1.Images.SetKeyName(1, "Hotel_2.png");
            this.imageCollection1.Images.SetKeyName(2, "Hotel_3.png");
            this.TilesLayer.DataProvider = this.BingMapDataProvider;
            this.TilesLayer.Name = "TilesLayer";
            this.BingMapDataProvider.BingKey = "YOUR BING MAPS KEY";
            this.HotelPlanLayer.Colorizer = keyColorColorizer2;
            this.HotelPlanLayer.ToolTipPattern = "{NAME}";
            this.HotelPlanLayer.DataLoaded += new DevExpress.XtraMap.DataLoadedEventHandler(this.HotelPlanLayer_DataLoaded);
            // 
            // FrmViewMap3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 495);
            this.Controls.Add(this.mapControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmViewMap3";
            this.Text = "FrmViewMap3";
            this.Load += new System.EventHandler(this.FrmViewMap3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraMap.MapControl mapControl1;
        private DevExpress.XtraMap.ImageLayer TilesLayer;
        private DevExpress.XtraMap.VectorItemsLayer HotelPlanLayer;
        private DevExpress.XtraMap.VectorItemsLayer HotelsLayer;
        private DevExpress.XtraMap.MapItemStorage HotelsItemStorage;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraMap.MiniMapImageTilesLayer miniMapImageTilesLayer1;
        private DevExpress.XtraMap.BingMapDataProvider bingMapDataProvider1;
        private DevExpress.XtraMap.MiniMapVectorItemsLayer miniMapVectorItemsLayer1;
        private DevExpress.XtraMap.MapItemStorage mapItemStorage1;
        private DevExpress.XtraMap.BingMapDataProvider BingMapDataProvider;
    }
}