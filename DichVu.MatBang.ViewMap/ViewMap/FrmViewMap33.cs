
using DevExpress.XtraMap;
using System.Windows.Forms;
using System;

namespace DichVu.MatBang.ViewMap.ViewMap
{
    public partial class FrmViewMap33 : DevExpress.XtraEditors.XtraForm
    {
        const int HotelsCount = 3;

        HotelPlansOverlayManager overlayManager;
        HotelRoomTooltipHelper tooltipHelper = new HotelRoomTooltipHelper();
        bool geoMapActivated = true;

        HotelPlansOverlayManager OverlayManager
        {
            get
            {
                if (overlayManager == null)
                    overlayManager = new HotelPlansOverlayManager();
                return overlayManager;
            }
        }
        protected MapOverlay[] Overlays { get { return geoMapActivated ? OverlayUtils.GetBingOverlays() : OverlayManager.GetOverlays(); } }
        protected MiniMapAlignment MiniMapAlignment { get { return MiniMapAlignment.TopLeft; } }
        public MapControl MapControl { get { return mapControl1; } }

        public FrmViewMap33()
        {
            InitializeComponent();
            
            PrepareMap();
        }

        void PrepareMap()
        {
            //DemoUtils.SetBingMapDataProviderKey(BingMapDataProvider);
            PopulateItemStorage();
        }

        void PopulateItemStorage()
        {
            HotelsItemStorage.Items.Clear();
            HotelsItemStorage.Items.Add(CreateHotel(new GeoPoint(12.2634, 109.154), "Geek Island Resort", "Hotel1", 0));
            HotelsItemStorage.Items.Add(CreateHotel(new GeoPoint(12.2036528,109.1552), "Nerd Hotel Tonga", "Hotel2", 1));
            HotelsItemStorage.Items.Add(CreateHotel(new GeoPoint(12.2458, 109.1134), "The IT Paradise Hotel", "Hotel3", 2));
        }

        MapItem CreateHotel(GeoPoint location, string name, string path, int index)
        {
            MapCustomElement hotel = new MapCustomElement() { Location = location, Text = name, ImageIndex = index, TextAlignment = TextAlignment.TopCenter };
            hotel.Attributes.Add(new MapItemAttribute() { Name = "path", Value = path, Type = typeof(string) });
            hotel.Attributes.Add(new MapItemAttribute() { Name = "index", Value = index, Type = typeof(int) });
            return hotel;
        }

        void ResetOverlays()
        {
            MapControl.Overlays.Clear();
            MapControl.Overlays.AddRange(Overlays);
        }
        void ActivateCartesianMap(MapItem item)
        {
            this.geoMapActivated = false;
            ChangeMiniMapState(false);
            mapControl1.CoordinateSystem = new CartesianMapCoordinateSystem();
            ShapefileDataAdapter data = new ShapefileDataAdapter()
            {
                SourceCoordinateSystem = new CartesianSourceCoordinateSystem(),
                FileUri = GetFileUri(item)
            };
            tooltipHelper.UpdateHotelIndex((int)item.Attributes["index"].Value);
            HotelPlanLayer.Data = data;
            OverlayManager.HotelName.Text = ((MapCustomElement)item).Text;
            SetElementsVisibility(false);
            mapControl1.MapItemClick -= mapControl1_MapItemClick;
            mapControl1.DrawMapItem -= mapControl1_DrawMapItem;
            mapControl1.MouseUp += mapControl1_MouseUp;
            ResetOverlays();
        }

        void ActivateGeoMap()
        {
            this.geoMapActivated = true;
            ChangeMiniMapState(true);
            mapControl1.CoordinateSystem = new GeoMapCoordinateSystem();
            HotelsLayer.SelectedItems.Clear();
            SetElementsVisibility(true);
            ResetMinMaxZoomLevel();
            mapControl1.ZoomToFitLayerItems(0.5);
            SetMinMaxZoomLevel();
            mapControl1.MapItemClick += mapControl1_MapItemClick;
            mapControl1.DrawMapItem += mapControl1_DrawMapItem;
            mapControl1.MouseUp -= mapControl1_MouseUp;
            ResetOverlays();
        }
        void ChangeMiniMapState(bool isEnable)
        {
            //ChkShowMinimap.Enabled = isEnable;
            if (MapControl.MiniMap != null)
                MapControl.MiniMap.Visible = isEnable;
        }
        void ResetMinMaxZoomLevel()
        {
            MapControl.MinZoomLevel = 1;
            MapControl.MaxZoomLevel = 20;
        }
        void SetMinMaxZoomLevel()
        {
            MapControl.MinZoomLevel = MapControl.ZoomLevel;
            MapControl.MaxZoomLevel = MapControl.MinZoomLevel + 2;
        }
        void SetElementsVisibility(bool isGeoMap)
        {
            HotelPlanLayer.Visible = !isGeoMap;
            HotelsLayer.Visible = isGeoMap;
            TilesLayer.Visible = isGeoMap;
        }
        Uri GetFileUri(MapItem item)
        {
            return DemoUtils.GetFileUri(string.Format("Hotels\\{0}.shp", item.Attributes["path"].Value));
        }

        void mapControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            MapHitInfo hitInfo = MapControl.CalcHitInfo(e.Location);
            MapOverlayItemBase clickedItem = OverlayUtils.GetClickedOverlayItem(hitInfo);
            if (clickedItem == null)
                return;
            if (clickedItem == OverlayManager.BackImage)
                ActivateGeoMap();
        }

        void OnDispose()
        {
            if (this.overlayManager != null)
            {
                this.overlayManager.Dispose();
                this.overlayManager = null;
            }
        }

        private void mapControl1_DrawMapItem(object sender, DrawMapItemEventArgs e)
        {
            MapCustomElement el = (MapCustomElement)e.Item;
            if (e.IsHighlighted && (el.ImageIndex < HotelsCount))
                el.ImageIndex += HotelsCount;
            if (!e.IsHighlighted && (el.ImageIndex >= HotelsCount))
                el.ImageIndex -= HotelsCount;
        }

        private void mapControl1_MapItemClick(object sender, MapItemClickEventArgs e)
        {
            ActivateCartesianMap(e.Item);
        }

        private void HotelPlanLayer_DataLoaded(object sender, DataLoadedEventArgs e)
        {
            ResetMinMaxZoomLevel();
            mapControl1.ZoomToFitLayerItems(0.3);
            SetMinMaxZoomLevel();
        }

        private void toolTipController1_BeforeShow(object sender, DevExpress.Utils.ToolTipControllerShowEventArgs e)
        {
            e.SuperTip = tooltipHelper.CalculateSuperTooltip(e.SelectedObject as MapItem, e.ToolTip);
        }
    }
}