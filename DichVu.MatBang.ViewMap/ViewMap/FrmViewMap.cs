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
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraMap;

using System.Collections;

using System.Data.OleDb;
using System.IO;


namespace DichVu.MatBang.ViewMap.ViewMap
{
    public partial class FrmViewMap : DevExpress.XtraEditors.XtraForm
    {
        MapControl mapControl;
        DevExpress.XtraMap.VectorItemsLayer vectorItemsLayer;
        MapItemStorage storage = new MapItemStorage();
        public FrmViewMap()
        {
            InitializeComponent();
        }

        private void FrmViewMap_Load(object sender, EventArgs e)
        {
            LoadMap();
        }

        private void LoadMap()
        {
            // Create a map control, set its dock style and add it to the form.
            mapControl = new MapControl();
            mapControl.Dock = DockStyle.Fill;
            mapControl.Location = new System.Drawing.Point(0, 29);
            mapControl.MapEditor.ShowEditorPanel = true;
            mapControl.MapEditor.AllowSaveActions = true;
            
            mapControl.Name = "mapControl1";
            mapControl.Size = new System.Drawing.Size(1017, 511);
            mapControl.TabIndex = 0;
            mapControl.ZoomLevel = 2D;
            this.Controls.Add(mapControl);

            CreateLayerImage();
            LoadLayerShp();

            

            mapControl.MapItemClick += MapControl_MapItemClick;
            mapControl.MapItemDoubleClick += MapControl_MapItemDoubleClick;
            mapControl.MapEditor.MapItemCreating += MapEditor_MapItemCreating;
        }

        private void MapEditor_MapItemCreating(object sender, MapItemCreatingEventArgs e)
        {
            MapShape shape = e.Item as MapShape;
            if (shape != null)
            {
                shape.Fill = Color.Orange;
                storage.Items.Add(shape);
            }
            //vectorItemsLayer.Data = storage;
        }

        private void MapControl_MapItemDoubleClick(object sender, MapItemClickEventArgs e)
        {
            string nameAttribute = e.Item.Attributes["NAME"].Value.ToString();
            int category = (int) e.Item.Attributes["CATEGORY"].Value;
            //DichVu.MatBang.ViewMap.Data.DataItem dataItem = new Data.DataItem();
            //foreach (var attribute in e.Item.Attributes)
            //{
            //    var category = attribute.Name;
            //    var value = attribute.Value;

            //    switch (attribute.Name)
            //    {
            //        case "NAME": dataItem.NAME = attribute.Value.ToString(); break;
            //        case "CATEGORY": dataItem.CATEGORY = attribute.Value; break;
            //    }
            //}

            using(var frm = new DichVu.MatBang.ViewMap.Data.FrmShowItemDoubleCLickData() { name = nameAttribute, category = category }) { frm.ShowDialog(); nameAttribute = frm.name; category = frm.category; }

            //var statusAttributeIdx = item.Attributes.FindIndex(n => n.Name == "Status");
            //int tmpValue = Convert.ToInt32(item.Attributes[statusAttributeIdx].Value) + 1;
            //item.Attributes[statusAttributeIdx].Value = tmpValue.ToString();

            e.Item.Attributes["NAME"].Value = nameAttribute;
            e.Item.Attributes["CATEGORY"].Value = category;

            #region Ý tưởng

            // Mình làm đến đây là đã bắt được item ngay tại chổ click vào
            // Nói chung những thứ trong shapefile và dbf là đã có thể chủ động chỉnh sửa
            // Nhưng chưa thêm được item vào

            #endregion
        }

        private void MapControl_MapItemClick(object sender, MapItemClickEventArgs e)
        {
            if (e.MouseArgs.Button == MouseButtons.Right)
            {
                string nameAttribute = e.Item.Attributes["NAME"].Value.ToString();
                int category = (int)e.Item.Attributes["CATEGORY"].Value;

                using (var frm = new DichVu.MatBang.ViewMap.Data.FrmShowItemDoubleCLickData() { name = nameAttribute, category = category }) { frm.ShowDialog(); nameAttribute = frm.name; category = frm.category; }

                e.Item.Attributes["NAME"].Value = nameAttribute;
                e.Item.Attributes["CATEGORY"].Value = category;
            }

                if (e.MouseArgs.Button == MouseButtons.Left)
            {
                var hitInfo = mapControl.CalcHitInfo(e.MouseArgs.Location);
                //var point = mapControl.Layers[0].ScreenPointToGeoPoint(new MapPoint(hitInfo.HitPoint.X, hitInfo.HitPoint.Y));
                //if (_polyline == null)
                //{
                //    _itemsLayer = new VectorItemsLayer();
                //    MapItemStorage storage = new MapItemStorage();
                //    _polyline = new Style.Layer.CustomerVectorItemsLayer().CreateNewPolylineFirstPoint(point);
                //    storage.Items.Add(_polyline);
                //    _itemsLayer.Data = storage;
                //    mapControl1.Layers.Add(_itemsLayer);
                //}
                //else
                //{
                //    _polyline.Points.Insert(_polyline.Points.Count - 1, new GeoPoint(point.Latitude, point.Longitude));
                //}
            }
        }

        /// <summary>
        /// Khởi tạo 1 layer từ load image tiles from a local map data provider
        /// </summary>
        private void CreateLayerImage()
        {
            DevExpress.XtraMap.ImageLayer imageLayer = new DevExpress.XtraMap.ImageLayer();
            mapControl.Layers.Add(imageLayer);
            imageLayer.DataProvider = new LocalProvider();
        }

        #region Khởi tạo lớp layer image

        public class LocalProvider : MapDataProviderBase
        {

            readonly SphericalMercatorProjection projection = new SphericalMercatorProjection();

            public LocalProvider()
            {
                TileSource = new LocalTileSource(this);
            }

            public override ProjectionBase Projection
            {
                get
                {
                    return projection;
                }
            }

            protected override Size BaseSizeInPixels
            {
                get { return new Size(Convert.ToInt32(LocalTileSource.tileSize * 2), Convert.ToInt32(LocalTileSource.tileSize * 2)); }
            }

            public override MapSize GetMapSizeInPixels(double zoomLevel)
            {
                throw new NotImplementedException();
            }
        }

        public class LocalTileSource : MapTileSourceBase
        {
            public const int tileSize = 256;
            public const int maxZoomLevel = 2;
            string directoryPath;

            internal static double CalculateTotalImageSize(double zoomLevel)
            {
                if (zoomLevel < 1.0)
                    return zoomLevel * tileSize * 2;
                return Math.Pow(2.0, zoomLevel) * tileSize;
            }

            public LocalTileSource(ICacheOptionsProvider cacheOptionsProvider) :
                base((int)CalculateTotalImageSize(maxZoomLevel), (int)CalculateTotalImageSize(maxZoomLevel), tileSize, tileSize, cacheOptionsProvider)
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
                //directoryPath = dir.Parent.Parent.FullName;
                directoryPath = dir.FullName;
            }

            public override Uri GetTileByZoomLevel(int zoomLevel, int tilePositionX, int tilePositionY)
            {
                if (zoomLevel <= maxZoomLevel)
                {
                    //String.Format("http://127.0.0.1:8082/map/z{0}/{2}/{1}.png", zoomLevel, tilePositionX, tilePositionY)
                    Uri u = new Uri(string.Format("file://" + directoryPath + "\\openstreetmap.org\\Hybrid_{0}_{1}_{2}.png", zoomLevel, tilePositionX, tilePositionY));
                    return u;
                }
                return null;
            }
        }

        #endregion

        //public void LoadFromStream(Stream shpStream, Stream dbfStream);

        /// <summary>
        /// Load layer shp
        /// </summary>
        private void LoadLayerShp()
        {
            DevExpress.XtraMap.KeyColorColorizer keyColorColorizer = new KeyColorColorizer();
            DevExpress.XtraMap.AttributeItemKeyProvider attributeItemKeyProvider = new AttributeItemKeyProvider();
            DevExpress.XtraMap.CartesianSourceCoordinateSystem cartesianSourceCoordinateSystem = new CartesianSourceCoordinateSystem();
            DevExpress.XtraMap.CartesianMapCoordinateSystem cartesianMapCoordinateSystem = new CartesianMapCoordinateSystem();
            DevExpress.XtraMap.MapOverlay mapOverlay1 = new MapOverlay();
            DevExpress.XtraMap.MapOverlayTextItem mapOverlayTextItem1 = new MapOverlayTextItem();
            DevExpress.XtraMap.MapOverlay mapOverlay2 = new MapOverlay();
            DevExpress.XtraMap.MapOverlayTextItem mapOverlayTextItem2 = new MapOverlayTextItem();
            DevExpress.XtraMap.ColorListLegend colorListLegend = new ColorListLegend();
            vectorItemsLayer = new VectorItemsLayer();
            //DevExpress.XtraMap.SqlGeometryDataAdapter sqlGeometryDataAdapter = new SqlGeometryDataAdapter();
            DevExpress.XtraMap.ShapefileDataAdapter shapefileDataAdapter = new ShapefileDataAdapter();
            DevExpress.XtraEditors.ColorPickEdit colorPickEdit = new ColorPickEdit();

            //map.CenterPoint = new DevExpress.XtraMap.GeoPoint(-50D, -90D);
            //cartesianMapCoordinateSystem.MeasureUnit = DevExpress.XtraMap.MeasureUnit.Meter;
            //map.CoordinateSystem = cartesianMapCoordinateSystem;
            
            mapControl.Layers.Add(vectorItemsLayer);

            colorListLegend.Header = "Loại mặt bằng";
            colorListLegend.Layer = vectorItemsLayer;

            mapControl.Legends.Add(colorListLegend);
            

            mapOverlayTextItem1.Text = "Fill: ";
            mapOverlay1.Items.Add(mapOverlayTextItem1);
            mapOverlay1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            mapOverlay1.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
            mapOverlayTextItem2.Text = "Stroke:";
            mapOverlay2.Items.Add(mapOverlayTextItem2);
            mapOverlay2.Location = new System.Drawing.Point(600, 0);
            mapOverlay2.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            mapOverlay2.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
            //map.Overlays.Add(mapOverlay1);
            //map.Overlays.Add(mapOverlay2);
            

            attributeItemKeyProvider.AttributeName = "NAME";
            keyColorColorizer.ItemKeyProvider = attributeItemKeyProvider;
            keyColorColorizer.PredefinedColorSchema = PredefinedColorSchema.Palette;

            //sqlGeometryDataAdapter.ConnectionString = Library.Properties.Settings.Default.Building_dbConnectionString;
            //sqlGeometryDataAdapter.SqlText = "SELECT MaTT AS CATEGORY, MaSoMB AS NAME FROM mbMatBang";
            //sqlGeometryDataAdapter.SpatialDataMember = "CATEGORY";
            shapefileDataAdapter.FileUri = GetFileUri("1234.shp");

            //shapefileDataAdapter.SourceCoordinateSystem = cartesianSourceCoordinateSystem;

            // sau khi mình load data cho shape file, mình có thể tạo stogare k?
            // chổ này mình vẫn get data lần đầu tiên lên, đổ vào shape file.
            // sau khi đã có, thì từ vectoritemlayer, đổ vào storage
            // mỗi lần add item vào, thì add vào storage, cuối cùng trước khi lưu hoặc export, thì đổ ngược lại item vào vectoritem
            
            //vectorItemsLayer.Colorizer = keyColorColorizer;
            vectorItemsLayer.Colorizer = CreateColorizer();
            //vectorItemsLayer.Data = sqlGeometryDataAdapter;
            //vectorItemsLayer.ShapeTitlesPattern = "{NAME}";
            vectorItemsLayer.Data = shapefileDataAdapter;

            // Get item từ vectoer item layer to storage
            // Create a storage for map items and generate them.

            

            // Test thử với load từ data source
            //ListSourceDataAdapter dataAdapter = new ListSourceDataAdapter();
            //vectorItemsLayer.Data = dataAdapter;

            //dataAdapter.Mappings.Latitude = "Latitude";
            //dataAdapter.Mappings.Longitude = "Longitude";
            //dataAdapter.AttributeMappings.Add(new MapItemAttributeMapping("Name", "Name"));
            //dataAdapter.AttributeMappings.Add(new MapItemAttributeMapping("Year", "Year"));
            //dataAdapter.AttributeMappings.Add(new MapItemAttributeMapping("Description", "Desc"));


            // 
            // colorPickEdit1
            // 
            colorPickEdit.EditValue = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(80)))));
            colorPickEdit.Location = new System.Drawing.Point(532, 48);
            colorPickEdit.Name = "colorPickEdit1";
            colorPickEdit.Properties.AutomaticColor = System.Drawing.Color.Black;
            colorPickEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            colorPickEdit.Size = new System.Drawing.Size(72, 20);
            colorPickEdit.TabIndex = 1;

            //shapefileDataAdapter1.FileUri = GetFileUri("1234.shp");
            //vectorItemsLayer1.Data = shapefileDataAdapter1;

            
        }

        // Cái sơ đồ phân lô màu mè này có thể load từ table bảng màu lên
        private MapColorizer CreateColorizer()
        {
            Library.MasterDataContext db = new Library.MasterDataContext();
            var lmb = db.mbTrangThais;
            KeyColorColorizer colorizer = new KeyColorColorizer()
            {
                ItemKeyProvider = new AttributeItemKeyProvider() { AttributeName = "CATEGORY" },
                PredefinedColorSchema = PredefinedColorSchema.Palette
            };

            foreach(var item in lmb) colorizer.Keys.Add(new ColorizerKeyItem() { Key = item.MaTT, Name = item.TenTT });

            //colorizer.Keys.Add(new ColorizerKeyItem() { Key = 1, Name = "Restaurant" });
            //colorizer.Keys.Add(new ColorizerKeyItem() { Key = 2, Name = "Business room" });
            //colorizer.Keys.Add(new ColorizerKeyItem() { Key = 3, Name = "Bathroom" });
            //colorizer.Keys.Add(new ColorizerKeyItem() { Key = 4, Name = "Living room" });
            //colorizer.Keys.Add(new ColorizerKeyItem() { Key = 5, Name = "Other" });
            //colorizer.Keys.Add(new ColorizerKeyItem() { Key = 6, Name = "Phòng dịch vụ" }); //Service room
            //colorizer.Keys.Add(new ColorizerKeyItem() { Key = 7, Name = "Pool" });
            //colorizer.Keys.Add(new ColorizerKeyItem() { Key = 8, Name = "Phòng Gym" });
            //colorizer.Keys.Add(new ColorizerKeyItem() { Key = 9, Name = "Chưa biết" });

            return colorizer;
        }

        //    private MapColorizer CreateColorizer()
        //    {
        //        // Create a Choropleth colorizer 
        //        ChoroplethColorizer colorizer = new ChoroplethColorizer();

        //        // Specify colors for the colorizer. 
        //        colorizer.ColorItems.AddRange(new ColorizerColorItem[] {
        //    new ColorizerColorItem(Color.FromArgb(0x5F, 0x8B, 0x95)),
        //    new ColorizerColorItem(Color.FromArgb(0x79, 0x96, 0x89)),
        //    new ColorizerColorItem(Color.FromArgb(0xA2, 0xA8, 0x75)),
        //    new ColorizerColorItem(Color.FromArgb(0xCE, 0xBB, 0x5F)),
        //    new ColorizerColorItem(Color.FromArgb(0xF2, 0xCB, 0x4E)),
        //    new ColorizerColorItem(Color.FromArgb(0xF1, 0xC1, 0x49)),
        //    new ColorizerColorItem(Color.FromArgb(0xE5, 0xA8, 0x4D)),
        //    new ColorizerColorItem(Color.FromArgb(0xD6, 0x86, 0x4E)),
        //    new ColorizerColorItem(Color.FromArgb(0xC5, 0x64, 0x50)),
        //    new ColorizerColorItem(Color.FromArgb(0xBA, 0x4D, 0x51))
        //});

        //        // Specify range stops for the colorizer. 
        //        colorizer.RangeStops.AddRange(new double[] { 0, 1, 2, 3, 4,
        //    5, 6, 7, 8, 2500000, 15000000 });

        //        // Specify the attribute that provides data values for the colorizer. 
        //        colorizer.ValueProvider = new ShapeAttributeValueProvider() { AttributeName = "CATEGORY" };

        //        return colorizer;
        //    }

        public void CreateDbfFromTable()
        {
            Library.MasterDataContext db = new Library.MasterDataContext();
            var mb = (from p in db.mbMatBangs select new { p.MaTT, p.MaSoMB }).ToList();
        }

        public static Uri GetFileUri(string fileName)
        {
            return new Uri("file:\\\\" + GetRelativePath(fileName), UriKind.RelativeOrAbsolute);
        }
        public static string GetRelativePath(string name)
        {
            //name = "Data\\" + name;
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Export();
        }

        void Export()
        {
            foreach (MapItem mapItem in vectorItemsLayer.Data.Items)
            {
                storage.Items.Add(mapItem);
            }
            // MapItemStorage storage = new MapItemStorage();

            vectorItemsLayer.Data = storage;

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "SHP files|*.shp";
                dialog.CreatePrompt = true;
                dialog.OverwritePrompt = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var option = new DevExpress.XtraMap.ShpExportOptions();
                    option.ExportToDbf = true;

                    mapControl.MapEditor.ActiveLayer.ExportToShp(dialog.FileName, option);
                    vectorItemsLayer.ExportToShp(dialog.FileName, new DevExpress.XtraMap.ShpExportOptions() { ExportToDbf = true, ShapeType = DevExpress.XtraMap.ShapeType.Polygon });
                    XtraMessageBox.Show(ParentForm, string.Format("Items succesfully exported to {0} file", dialog.FileName), "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// View item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Load vector và dữ liệu từ shp

            System.Collections.Generic.List<DichVu.MatBang.ViewMap.Data.DataItem> dataItems = new List<Data.DataItem>();

            foreach (MapItem mapItem in vectorItemsLayer.Data.Items)
            {
                DichVu.MatBang.ViewMap.Data.DataItem dataItem = new Data.DataItem();

                MapShape mapShape = (MapShape)mapItem;
                if (mapShape.GetType() == typeof(MapPath))
                {
                    DevExpress.Map.CoordPoint coordPoint = ((MapPath)mapShape).Segments[0].Points[0];
                }

                foreach(var attribute in mapItem.Attributes)
                {
                    var category = attribute.Name;
                    var value = attribute.Value;

                    switch(attribute.Name)
                    {
                        case "NAME": dataItem.NAME = attribute.Value.ToString(); break;
                        case "CATEGORY": dataItem.CATEGORY = attribute.Value; break;
                    }
                }

                MapItemAttribute statusAttributeIdx = mapItem.Attributes.Find(n => n.Name == "CATEGORY");
                

                dataItems.Add(dataItem);
            }

            using(var frm = new DichVu.MatBang.ViewMap.Data.FrmViewData { DataItems = dataItems }) { frm.ShowDialog(); dataItems = frm.DataItems; }

            //var statusAttributeIdx = item.Attributes.FindIndex(n => n.Name == "Status");
            //int tmpValue = Convert.ToInt32(item.Attributes[statusAttributeIdx].Value) + 1;
            //item.Attributes[statusAttributeIdx].Value = tmpValue.ToString();
        }
    }
}