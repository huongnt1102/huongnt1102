using System.Linq;

namespace DichVu.MatBang.ViewMap.ViewMap
{
    public partial class FrmViewMap6 : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Mapcontrol
        /// </summary>
        private DevExpress.XtraMap.MapControl mapControl;
        private DevExpress.XtraMap.ImageLayer imageLayer;
        private DevExpress.XtraMap.VectorItemsLayer vectorItemsLayer;
        //private DevExpress.XtraMap.MapItemStorage mapItemStorage = new DevExpress.XtraMap.MapItemStorage();
        //DevExpress.XtraMap.ListSourceDataAdapter listSourceDataAdapter;

        public FrmViewMap6()
        {
            InitializeComponent();
        }

        private void FrmViewMap6_Load(object sender, System.EventArgs e)
        {
            // Tạo map control
            CreateMapControl();

            // Load layer image từ link ftp
            CreateLayerImage();

            LoadLayerData();
        }

        /// <summary>
        /// Tạo map control
        /// </summary>
        private void CreateMapControl()
        {
            mapControl = new DevExpress.XtraMap.MapControl();
            mapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            mapControl.Location = new System.Drawing.Point(0, 29); //(0, 29)
            mapControl.Name = "mapControl1";
            mapControl.Size = new System.Drawing.Size(1017, 511);
            mapControl.TabIndex = 0;
            mapControl.ZoomLevel = 2D;
            //DevExpress.XtraMap.CartesianMapCoordinateSystem cartesianMapCoordinateSystem1 = new DevExpress.XtraMap.CartesianMapCoordinateSystem();
            //this.mapControl.CenterPoint = new DevExpress.XtraMap.GeoPoint(-50D, -90D);
            //cartesianMapCoordinateSystem1.MeasureUnit = DevExpress.XtraMap.MeasureUnit.Meter;
            //this.mapControl.CoordinateSystem = cartesianMapCoordinateSystem1;

            mapControl.MapEditor.ShowEditorPanel = true;
            mapControl.MapEditor.AllowSaveActions = true;

            this.Controls.Add(mapControl);
        }

        #region Layer image

        /// <summary>
        /// Upload file vào ftp
        /// </summary>
        /// <param name="choose">Khai báo chọn img hay upload file, = true: img</param>
        /// <param name="img">Is img & Choose = true</param>
        /// <param name="shp">Is Shp & Choose = false</param>
        /// <param name="folder">img: "ImgUpload", shp: "ShpUpload", dbf: "DbfUpload"</param>
        /// <param name="buildingId"></param>
        /// <param name="stt">STT: 1: img, 2: shp, 3: dbf</param>
        private void UploadToFtp(bool choose, bool img, bool shp, string folder, byte buildingId, int stt)
        {
            try
            {
                var frm = new FTP.frmUploadFile() { IsChangeName = (choose == true) ? true : false };
                if (choose) frm.IsChangeName = true;
                if (frm.SelectFileUpload(choose, img, shp))
                {
                    frm.Folder = folder;
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Library.MasterDataContext masterDataContext = new Library.MasterDataContext();

                        Library.ViewMap viewMap;
                        viewMap = masterDataContext.ViewMaps.FirstOrDefault(_ => _.MaTn == buildingId & _.Stt == stt);
                        if (viewMap == null)
                        {
                            viewMap = new Library.ViewMap();
                            masterDataContext.ViewMaps.InsertOnSubmit(viewMap);
                        }

                        viewMap.LinkImage = frm.FileName;
                        viewMap.MaTn = buildingId; // tạm thời cứ mặc định
                        viewMap.Stt = stt;

                        masterDataContext.SubmitChanges();
                        masterDataContext.Dispose();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.ToString());
            }
        }

        public class LocalTileSource : DevExpress.XtraMap.MapTileSourceBase
        {
            public const int tileSize = 256; //256
            public const int maxZoomLevel = 1;
            private string directoryPath;

            internal static double CalculateTotalImageSize(double zoomLevel)
            {
                if (zoomLevel < 1.0) return zoomLevel * tileSize * 1;
                return System.Math.Pow(2.0, zoomLevel) * tileSize;
            }

            public LocalTileSource(DevExpress.XtraMap.ICacheOptionsProvider cacheOptionsProvider) : base((int)CalculateTotalImageSize(maxZoomLevel), (int)CalculateTotalImageSize(maxZoomLevel), tileSize, tileSize, cacheOptionsProvider)
            {
                //System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
                //directoryPath = directoryInfo.FullName;
                Library.MasterDataContext masterDataContext = new Library.MasterDataContext();
                var ftp = masterDataContext.tblConfigs.FirstOrDefault();
                //if (ftp == null) return null;
                Library.ViewMap viewMap = masterDataContext.ViewMaps.FirstOrDefault(_ => _.MaTn == 100 & _.Stt == 1);
                //if (viewMap == null) return null;
                if (viewMap != null & ftp != null) directoryPath = ftp.WebUrl + viewMap.LinkImage;
            }

            public override System.Uri GetTileByZoomLevel(int zoomLevel, int titlePositionX, int titlePositionY)
            {
                //Uri u = new Uri(string.Format("file://" + directoryPath + "\\openstreetmap.org\\Hybrid_{0}_{1}_{2}.png", zoomLevel, tilePositionX, tilePositionY));
                //String.Format("http://127.0.0.1:8082/map/z{0}/{2}/{1}.png", zoomLevel, tilePositionX, tilePositionY)
                //return u;
                //var ftp = db.tblConfigs.FirstOrDefault();
                //url = ftp.WebUrl + hinhAnh.HinhAnh;



                if (zoomLevel <= maxZoomLevel & titlePositionX == 1 & titlePositionY == 0 & directoryPath != null)
                {
                    System.Uri uri = new System.Uri(string.Format("{0}", directoryPath));
                    return uri;
                }
                return null;
            }
        }

        public class LocalProvider : DevExpress.XtraMap.MapDataProviderBase
        {
            private readonly DevExpress.XtraMap.SphericalMercatorProjection projection = new DevExpress.XtraMap.SphericalMercatorProjection();

            public LocalProvider() { TileSource = new LocalTileSource(this); }

            public override DevExpress.XtraMap.ProjectionBase Projection { get { return projection; } }

            protected override System.Drawing.Size BaseSizeInPixels
            {
                get { return new System.Drawing.Size(System.Convert.ToInt32(LocalTileSource.tileSize * 2), System.Convert.ToInt32(LocalTileSource.tileSize * 2)); }
            }

            public override DevExpress.XtraMap.MapSize GetMapSizeInPixels(double zoomLevel)
            {
                throw new System.NotImplementedException();
            }
        }

        private void CreateLayerImage()
        {
            if (imageLayer != null) mapControl.Layers.Remove(imageLayer);

            imageLayer = new DevExpress.XtraMap.ImageLayer();
            mapControl.Layers.Add(imageLayer);
            imageLayer.DataProvider = new LocalProvider();
        }

        #endregion

        private void itemChangeImage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Upload ảnh lên ftp, sau đó lấy ảnh từ ftp về, load layer image
            UploadToFtp(true, true, false, "ImgUpload", 100, 1);

            // Load lại layer image
            CreateLayerImage();
        }

        private void LoadLayerData()
        {
            // Create a layer to display vector items. 
            vectorItemsLayer = new DevExpress.XtraMap.VectorItemsLayer();
            mapControl.Layers.Add(vectorItemsLayer);

            // Create a storage for map items and generate them. 
            //mapItemStorage = new DevExpress.XtraMap.MapItemStorage();
            //DevExpress.XtraMap.MapItem[] capitals = GetCapitals();
            //mapItemStorage.Items.AddRange(capitals);
            //vectorItemsLayer.Data = mapItemStorage;

            // Thử đổ data bằng list source

            // Legend
            DevExpress.XtraMap.ColorListLegend colorListLegend = new DevExpress.XtraMap.ColorListLegend();
            colorListLegend.Header = "Loại mặt bằng";
            colorListLegend.Layer = vectorItemsLayer;
            mapControl.Legends.Add(colorListLegend);

            // Color
            vectorItemsLayer.Colorizer = GetMapColorizer();
        }

        private DevExpress.XtraMap.MapColorizer GetMapColorizer()
        {
            Library.MasterDataContext masterDataContext = new Library.MasterDataContext();
            var mbTrangThai = masterDataContext.mbTrangThais;
            DevExpress.XtraMap.KeyColorColorizer keyColorColorizer = new DevExpress.XtraMap.KeyColorColorizer();
            keyColorColorizer.ItemKeyProvider = new DevExpress.XtraMap.AttributeItemKeyProvider() { AttributeName = "CATEGORY" };
            keyColorColorizer.PredefinedColorSchema = DevExpress.XtraMap.PredefinedColorSchema.Palette;

            foreach (var item in mbTrangThai) keyColorColorizer.Keys.Add(new DevExpress.XtraMap.ColorizerKeyItem() { Key = item.MaTT, Name = item.TenTT });

            return keyColorColorizer;
        }

        // Create an array of callouts for 5 capital cities. 
        DevExpress.XtraMap.MapItem[] GetCapitals()
        {
            return new DevExpress.XtraMap.MapItem[] {
                new DevExpress.XtraMap.MapCallout() { Text = "London", Location = new DevExpress.XtraMap.GeoPoint(51.507222, -0.1275) },
                new DevExpress.XtraMap.MapCallout() { Text = "Rome", Location = new DevExpress.XtraMap.GeoPoint(41.9, 12.5) },
                new DevExpress.XtraMap.MapCallout() { Text = "Paris", Location = new DevExpress.XtraMap.GeoPoint(48.8567, 2.3508) },
                new DevExpress.XtraMap.MapCallout() { Text = "Berlin", Location = new DevExpress.XtraMap.GeoPoint(52.52, 13.38) },
                new DevExpress.XtraMap.MapCallout() { Text = "Madrid", Location = new DevExpress.XtraMap.GeoPoint(40.4, -3.68) }
            };
        }

        public class TestDataItem
        {
            public string Label { get; set; }
            public double Lat { get; set; }
            public double Lon { get; set; }
            public System.Drawing.Color Tag { get; set; }
        }

        public class TestData : System.Collections.Generic.List<TestDataItem>
        {
            static readonly TestData instance;

            static TestData()
            {
                instance = new TestData();
                instance.Add(new TestDataItem() { Lat = 14, Lon = 8, Label = "A", Tag = System.Drawing.Color.Yellow });
                instance.Add(new TestDataItem() { Lat = 4, Lon = -2, Label = "B", Tag = System.Drawing.Color.Purple });
                instance.Add(new TestDataItem() { Lat = -6, Lon = -12, Label = "C", Tag = System.Drawing.Color.Red });
            }

            public static TestData Instance { get { return instance; } }
        }
    }
}