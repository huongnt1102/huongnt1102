using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraMap;
using DevExpress.Utils;
using System.IO;

namespace DichVu.MatBang.ViewMap.ViewMap
{
    public partial class FrmViewMap3 : DevExpress.XtraEditors.XtraForm
    {
        const int HotelsCount = 3;
        bool geoMapActivated = true;
        HotelPlansOverlayManager overlayManager;
        HotelRoomTooltipHelper tooltipHelper = new HotelRoomTooltipHelper();
        double? x, y;

        HotelPlansOverlayManager OverlayManager
        {
            get
            {
                if (overlayManager == null)
                    overlayManager = new HotelPlansOverlayManager();
                return overlayManager;
            }
        }

        public DevExpress.XtraMap.MapOverlay[] Overlays { get { return geoMapActivated ? OverlayUtils.GetBingOverlays() : OverlayManager.GetOverlays(); } }

        protected MiniMapAlignment MiniMapAlignment { get { return MiniMapAlignment.TopLeft; } }

        public MapControl MapControl { get { return mapControl1; } }


        public FrmViewMap3()
        {
            InitializeComponent();
            PrepareMap();
        }

        private void FrmViewMap3_Load(object sender, EventArgs e)
        {
            //PrepareMap();
            x = mapControl1.CenterPoint.GetX();
            y = mapControl1.CenterPoint.GetY();
        }

        void PrepareMap()
        {
            DemoUtils.SetBingMapDataProviderKey(BingMapDataProvider);
            PopulateItemStorage();
        }

        void PopulateItemStorage()
        {
            HotelsItemStorage.Items.Clear();
            HotelsItemStorage.Items.Add(CreateHotel(new GeoPoint(-21.1434, -175.154), "Geek Island Resort", "Hotel1", 0));
            HotelsItemStorage.Items.Add(CreateHotel(new GeoPoint(-21.1936528, -175.1552), "Nerd Hotel Tonga", "Hotel2", 1));
            HotelsItemStorage.Items.Add(CreateHotel(new GeoPoint(-21.1658, -175.1134), "The IT Paradise Hotel", "Hotel3", 2));
        }

        MapItem CreateHotel(GeoPoint location, string name, string path, int index)
        {
            MapCustomElement hotel = new MapCustomElement() { Location = location, Text = name, ImageIndex = index, TextAlignment = TextAlignment.TopCenter };
            hotel.Attributes.Add(new MapItemAttribute() { Name = "path", Value = path, Type = typeof(string) });
            hotel.Attributes.Add(new MapItemAttribute() { Name = "index", Value = index, Type = typeof(int) });
            return hotel;
        }

        private void HotelPlanLayer_DataLoaded(object sender, DataLoadedEventArgs e)
        {
            ResetMinMaxZoomLevel();
            mapControl1.ZoomToFitLayerItems(0.3);
            SetMinMaxZoomLevel();
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

        private void mapControl1_MapItemClick(object sender, MapItemClickEventArgs e)
        {
            ActivateCartesianMap(e.Item);
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

        void ChangeMiniMapState(bool isEnable)
        {
            //ChkShowMinimap.Enabled = isEnable;
            if (MapControl.MiniMap != null)
                MapControl.MiniMap.Visible = isEnable;
        }

        Uri GetFileUri(MapItem item)
        {
            return DemoUtils.GetFileUri(string.Format("Hotels\\{0}.shp", item.Attributes["path"].Value));
        }

        void SetElementsVisibility(bool isGeoMap)
        {
            HotelPlanLayer.Visible = !isGeoMap;
            HotelsLayer.Visible = isGeoMap;
            TilesLayer.Visible = isGeoMap;
        }

        void ResetOverlays()
        {
            MapControl.Overlays.Clear();
            //MapControl.CenterPoint.Offset(12.259328, 109.13429);
            //PopulateItemStorage();
            MapControl.Overlays.AddRange(Overlays);
        }

        private void mapControl1_DrawMapItem(object sender, DrawMapItemEventArgs e)
        {
            MapCustomElement el = (MapCustomElement)e.Item;
            if (e.IsHighlighted && (el.ImageIndex < HotelsCount))
                el.ImageIndex += HotelsCount;
            if (!e.IsHighlighted && (el.ImageIndex >= HotelsCount))
                el.ImageIndex -= HotelsCount;
        }

        private void mapControl1_MouseUp(object sender, MouseEventArgs e)
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

        void ActivateGeoMap()
        {
            this.geoMapActivated = true;
            ChangeMiniMapState(true);
            mapControl1.CoordinateSystem = new GeoMapCoordinateSystem();
            mapControl1.CenterPoint = new DevExpress.XtraMap.GeoPoint(12.259328D, 109.13429D);
            HotelsLayer.SelectedItems.Clear();
            SetElementsVisibility(true);
            ResetMinMaxZoomLevel();
            //mapControl1.ZoomToFitLayerItems(0.5);
            SetMinMaxZoomLevel();
            //mapControl1.ZoomLevel = 16;
            mapControl1.MapItemClick += mapControl1_MapItemClick;
            mapControl1.DrawMapItem += mapControl1_DrawMapItem;
            mapControl1.MouseUp -= mapControl1_MouseUp;
            ResetOverlays();
        }
    }

    public abstract class OverlayManagerBase : IDisposable
    {
        Dictionary<string, Font> fontsCollection;

        protected Dictionary<string, Font> FontsCollection { get { return fontsCollection; } }

        protected OverlayManagerBase()
        {
            this.fontsCollection = CreateFonts();
        }

        protected abstract Dictionary<string, Font> CreateFonts();

        #region IDisposable implementation
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                IEnumerable<string> keysCollection = new List<string>(this.fontsCollection.Keys);
                foreach (string key in keysCollection)
                {
                    if (fontsCollection[key] != null)
                    {
                        this.fontsCollection[key].Dispose();
                        this.fontsCollection[key] = null;
                    }
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~OverlayManagerBase()
        {
            Dispose(false);
        }
        #endregion
    }

    public class HotelPlansOverlayManager : OverlayManagerBase
    {
        MapOverlay overlay;
        MapOverlayImageItem backImage;
        MapOverlayTextItem hotelName;

        public MapOverlay Overlay { get { return overlay; } }
        public MapOverlayImageItem BackImage { get { return backImage; } }
        public MapOverlayTextItem HotelName { get { return hotelName; } }

        public HotelPlansOverlayManager()
        {
            CreateOverlay();
        }

        void CreateOverlay()
        {
            backImage = new MapOverlayImageItem() { Padding = new Padding(5), ImageUri = new Uri(DemoUtils.GetRelativePath("Images\\BackButton.png")) };
            hotelName = new MapOverlayTextItem() { Padding = new Padding(15) };
            hotelName.TextStyle.Font = FontsCollection["back_text"];
            overlay = new MapOverlay() { Alignment = ContentAlignment.TopLeft, Margin = new Padding(10, 10, 0, 0) };
            overlay.BackgroundStyle.Fill = Color.Transparent;
            overlay.Items.AddRange(new MapOverlayItemBase[] { backImage, hotelName });
        }

        protected override Dictionary<string, Font> CreateFonts()
        {
            Dictionary<string, Font> collection = new Dictionary<string, Font>();
            collection.Add("back_text", new Font(AppearanceObject.DefaultFont.FontFamily, 20, FontStyle.Bold));
            return collection;
        }

        public MapOverlay[] GetOverlays()
        {
            return new MapOverlay[] { overlay };
        }
    }

    public class DemoUtils
    {
        const string key = DevExpress.Map.Native.DXBingKeyVerifier.BingKeyWinMapMainDemo;

        internal static void SetBingMapDataProviderKey(BingMapDataProvider provider)
        {
            if (provider != null) provider.BingKey = key;
        }
        internal static void SetBingMapDataProviderKey(BingMapDataProviderBase provider)
        {
            if (provider != null) provider.BingKey = key;
        }

        public static string GetRelativePath(string name)
        {
            name = "Data\\" + name;
            string path = System.Windows.Forms.Application.StartupPath;
            string s = "\\";
            for (int i = 0; i <= 10; i++)
            {
                if (System.IO.File.Exists(path + s + name))
                    return (path + s + name);
                else
                    s += "..\\";
            }
            return string.Empty;
        }

        public static string GetRelativeDirectoryPath(string name)
        {
            name = "Data\\" + name;
            string path = System.Windows.Forms.Application.StartupPath;
            string s = "\\";
            for (int i = 0; i <= 10; i++)
            {
                if (System.IO.Directory.Exists(path + s + name))
                    return (path + s + name);
                else
                    s += "..\\";
            }
            return string.Empty;
        }

        public static Uri GetFileUri(string fileName)
        {
            return new Uri("file:\\\\" + GetRelativePath(fileName), UriKind.RelativeOrAbsolute);
        }
    }

    public class HotelRoomTooltipHelper
    {
        HotelImagesGenerator imageGenerator = new HotelImagesGenerator();
        SuperToolTip superToolTip = new SuperToolTip();
        ToolTipTitleItem titleItem = new ToolTipTitleItem();
        ToolTipItem contentItem = new ToolTipItem() { ImageToTextDistance = 0 };

        public HotelRoomTooltipHelper()
        {
            this.superToolTip.Items.Add(titleItem);
            this.superToolTip.Items.Add(contentItem);
        }

        string CalculateTitle(int category, string tooltip)
        {
            return category == 4 ? string.Format("Room: {0}", tooltip) : tooltip;
        }
        public SuperToolTip CalculateSuperTooltip(MapItem item, string tooltip)
        {
            if (item == null)
                return null;
            MapItemAttribute attr = item.Attributes["CATEGORY"];
            if (attr == null)
                return null;
            titleItem.Text = CalculateTitle((int)attr.Value, tooltip);
            attr = item.Attributes["IMAGE"];
            contentItem.Image = attr != null ? (Image)attr.Value : imageGenerator.GetItemImage(item);
            return superToolTip;
        }
        public void UpdateHotelIndex(int index)
        {
            imageGenerator.HotelIndex = index;
        }
    }

    public class HotelImagesGenerator
    {
        class PathsIndexPair
        {
            public string[] Paths { get; set; }
            public int Index { get; set; }
        }

        const int ImageWidth = 200;
        static readonly string[] Categories = new string[] { "Restaurant", "MeetingRoom", "Bathroom", "Bedroom", "Outofdoors", "ServiceRoom", "Pool", "Lobby" };

        int hotelIndex = 0;
        List<PathsIndexPair> filesWithIndices = new List<PathsIndexPair>();

        public int HotelIndex
        {
            get { return hotelIndex; }
            set
            {
                hotelIndex = value;
                UpdateIndices();
            }
        }

        public HotelImagesGenerator()
        {
            foreach (string category in Categories)
                filesWithIndices.Add(new PathsIndexPair() { Index = 0, Paths = GetAvailableFiles(category) });
        }
        void UpdateIndices()
        {
            filesWithIndices[0].Index = hotelIndex * 2;
            filesWithIndices[1].Index = 0;
            filesWithIndices[2].Index = hotelIndex * 4;
            filesWithIndices[6].Index = hotelIndex;
        }
        string[] GetAvailableFiles(string category)
        {
            string path = DemoUtils.GetRelativeDirectoryPath("\\Images\\Hotels\\");
            return Directory.GetFiles(path).Where(p => p.StartsWith(path + category)).ToArray();
        }
        Image GetImage(int category, string name, int roomCat)
        {
            if (category == 4)
                filesWithIndices[3].Index = roomCat;
            return GetCategoryImage(filesWithIndices[category - 1]);
        }
        Image GetCategoryImage(PathsIndexPair pathsWithIndex)
        {
            if (pathsWithIndex.Paths.Length == 0)
                return null;
            int index = pathsWithIndex.Index % pathsWithIndex.Paths.Length;
            pathsWithIndex.Index++;
            return new Bitmap(pathsWithIndex.Paths[index]);
        }
        Image ScaleImage(Image srcImg)
        {
            double ratio = (double)srcImg.Width / srcImg.Height;
            int newHeight = (int)((double)ImageWidth / ratio);
            Image resImg = new Bitmap(ImageWidth, newHeight);
            Graphics.FromImage(resImg).DrawImage(srcImg, 0, 0, ImageWidth, newHeight);
            return resImg;
        }
        public Image GetItemImage(MapItem item)
        {
            Image image = GetImage((int)item.Attributes["CATEGORY"].Value, item.Attributes["NAME"].Value.ToString(), (int)item.Attributes["ROOMCAT"].Value);
            if (image == null)
                return null;
            image = ScaleImage(image);
            item.Attributes.Add(new MapItemAttribute() { Name = "IMAGE", Value = image });
            return image;
        }
    }

    public static class OverlayUtils
    {
        static MapOverlay bingLogoOverlay;
        static MapOverlay bingCopyrightOverlay;
        static MapOverlay osmCopyrightOverlay;
        static MapOverlay medalsOverlay;

        public static MapOverlay BingLogoOverlay
        {
            get
            {
                if (bingLogoOverlay == null)
                    bingLogoOverlay = CreateBingLogoOverlay();
                return bingLogoOverlay;
            }
        }
        public static MapOverlay BingCopyrightOverlay
        {
            get
            {
                if (bingCopyrightOverlay == null)
                    bingCopyrightOverlay = CreateBingCopyrightOverlay();
                return bingCopyrightOverlay;
            }
        }
        public static MapOverlay OSMCopyrightOverlay
        {
            get
            {
                if (osmCopyrightOverlay == null)
                    osmCopyrightOverlay = CreateOSMCopyrightOverlay();
                return osmCopyrightOverlay;
            }
        }
        public static MapOverlay MedalsOverlay
        {
            get
            {
                if (medalsOverlay == null)
                    medalsOverlay = CreateMedalsOverlay();
                return medalsOverlay;
            }
        }

        static MapOverlay CreateBingLogoOverlay()
        {
            MapOverlay overlay = new MapOverlay() { Alignment = ContentAlignment.BottomLeft, Margin = new Padding(10, 0, 0, 10) };
            MapOverlayImageItem logoItem = new MapOverlayImageItem() { Padding = new Padding(), ImageUri = new Uri(DemoUtils.GetRelativePath("Images\\BingLogo.png")) };
            overlay.Items.Add(logoItem);
            return overlay;
        }
        static MapOverlay CreateBingCopyrightOverlay()
        {
            MapOverlay overlay = new MapOverlay() { Alignment = ContentAlignment.BottomRight, Margin = new Padding(0, 0, 10, 10) };
            MapOverlayTextItem copyrightItem = new MapOverlayTextItem() { Padding = new Padding(5), Text = "Copyright © 2018 Microsoft and its suppliers. All rights reserved." };
            overlay.Items.Add(copyrightItem);
            return overlay;
        }
        static MapOverlay CreateOSMCopyrightOverlay()
        {
            MapOverlay overlay = new MapOverlay() { Alignment = ContentAlignment.BottomRight, Margin = new Padding(0, 0, 10, 10) };
            MapOverlayTextItem copyrightItem = new MapOverlayTextItem() { Padding = new Padding(5), Text = "© OpenStreetMap contributors" };
            overlay.Items.Add(copyrightItem);
            return overlay;
        }
        static MapOverlay CreateMedalsOverlay()
        {
            MapOverlay overlay = new MapOverlay() { Alignment = ContentAlignment.TopCenter, Margin = new Padding(10, 10, 10, 10) };
            MapOverlayTextItem medalsItem = new MapOverlayTextItem() { Padding = new Padding(5), Text = "2016 Summer Olympics Medal Result" };
            medalsItem.TextStyle.Font = new Font(AppearanceObject.DefaultFont.FontFamily, 16, FontStyle.Regular);
            overlay.Items.Add(medalsItem);
            return overlay;
        }
        public static MapOverlay[] GetBingOverlays()
        {
            return new MapOverlay[] { BingLogoOverlay, BingCopyrightOverlay };
        }
        public static MapOverlay[] GetMedalsOverlay()
        {
            return new MapOverlay[] { MedalsOverlay };
        }
        public static MapOverlayItemBase GetClickedOverlayItem(MapHitInfo hitInfo)
        {
            if (hitInfo.InUIElement)
            {
                MapOverlayHitInfo overlayHitInfo = hitInfo.UiHitInfo as MapOverlayHitInfo;
                if (overlayHitInfo != null)
                    return overlayHitInfo.OverlayItem;
            }
            return null;
        }
    }
}