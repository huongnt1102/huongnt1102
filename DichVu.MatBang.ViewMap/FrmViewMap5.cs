using System.Linq;

namespace DichVu.MatBang.ViewMap
{
    public partial class FrmViewMap5 : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Mapcontrol
        /// </summary>
        private DevExpress.XtraMap.MapControl mapControl;
        private DevExpress.XtraMap.ImageLayer imageLayer;
        private DevExpress.XtraMap.VectorItemsLayer vectorItemsLayer;
        DevExpress.XtraMap.ColorListLegend colorListLegend;
        private DevExpress.XtraMap.MapItemStorage mapItemStorage = new DevExpress.XtraMap.MapItemStorage();
        public bool IsOpened { get; set; }
        private DevExpress.Utils.ToolTipControlInfo _Info;

        public FrmViewMap5()
        {
            try
            {
                InitializeComponent();
            }
            catch (System.Exception ex) { Library.DialogBox.Error(ex.Message); }
            
        }

        private void FrmViewMap5_Load(object sender, System.EventArgs e)
        {
            try
            {
                Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);

                // Tạo map control
                CreateMapControl();

                // Load layer image từ link ftp
                CreateLayerImage();

                // Load layer shp
                LoadLayerShp();

                // event
                mapControl.MapEditor.MapItemCreating += MapEditor_MapItemCreating;
                mapControl.MapItemClick += MapControl_MapItemClick;
                mapControl.MouseMove += MapControl_MouseMove;
            }
            catch (System.Exception ex) { Library.DialogBox.Error(ex.Message); }
            
        }

        private void MapControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                DevExpress.XtraMap.MapHitInfo hitInfo = mapControl.CalcHitInfo(e.Location);
                if (hitInfo.InMapPath)
                {
                    if ((!IsOpened))
                    {
                        IsOpened = true;

                        var mapPath = hitInfo.MapPath;
                        var item = mapPath.Attributes["NAME"].Value.ToString();
                        Library.MasterDataContext masterDataContext = new Library.MasterDataContext();
                        var matBang = (from p in masterDataContext.mbMatBangs join tt in masterDataContext.mbTrangThais on p.MaTT equals tt.MaTT join tl in masterDataContext.mbTangLaus on p.MaTL equals tl.MaTL where p.MaSoMB.Trim().ToLower() == item.Trim().ToLower() select new { p.MaSoMB, tl.TenTL, tl.mbKhoiNha.TenKN, tt.TenTT }).FirstOrDefault();
                        if (matBang == null) return;

                        DevExpress.Utils.SuperToolTip sTooltip1 = new DevExpress.Utils.SuperToolTip();
                        // Create a tooltip item that represents a header.
                        //DevExpress.Utils.ToolTipTitleItem titleItem1 = new DevExpress.Utils.ToolTipTitleItem();
                        //titleItem1.Text = matBang.MaSoMB;
                        //// Create a tooltip item that represents the SuperTooltip's contents.
                        //DevExpress.Utils.ToolTipItem item1 = new DevExpress.Utils.ToolTipItem();
                        //item1.Text = "<b>Mặt bằng: </b>" + matBang.MaSoMB;
                        //item1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
                        //// Add the tooltip items to the SuperTooltip.

                        //DevExpress.Utils.ToolTipItem item2 = new DevExpress.Utils.ToolTipItem();
                        //item2.Text = "Tầng lầu: " + matBang.TenTL;
                        //item2.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;

                        //DevExpress.Utils.ToolTipItem item3 = new DevExpress.Utils.ToolTipItem();
                        //item3.Text = "Khối nhà: " + matBang.TenKN;
                        //item3.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;

                        //DevExpress.Utils.ToolTipItem item4 = new DevExpress.Utils.ToolTipItem();
                        //item4.Text = "Trạng thái: " + matBang.TenTT;
                        //item4.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
                        //// Add the tooltip items to the SuperTooltip.


                        //sTooltip1.Items.Add(titleItem1);
                        //sTooltip1.Items.Add(item1);
                        //sTooltip1.Items.Add(item2);
                        //sTooltip1.Items.Add(item3);
                        //sTooltip1.Items.Add(item4);

                        DevExpress.Utils.SuperToolTipSetupArgs args = new DevExpress.Utils.SuperToolTipSetupArgs();
                        args.Title.Text = matBang.MaSoMB;
                        args.Title.Appearance.BackColor = System.Drawing.Color.Blue;
                        args.Title.Appearance.Options.UseBackColor = true;
                        //args.Title.Font = new System.Drawing.Font("Times New Roman", 14);
                        args.Contents.Text = "<b>Mặt bằng: </b>" + matBang.MaSoMB + "</br><b>Tầng lầu: </b>" + matBang.TenTL + "</br><b>Khối nhà: </b>" + matBang.TenKN;
                        args.ShowFooterSeparator = true;
                        //args.Footer.Font = new System.Drawing.Font("Comic Sans MS", 8);
                        args.Footer.Text = "<b>Trạng thái: </b>" + matBang.TenTT;
                        sTooltip1.Setup(args);

                        _Info = new DevExpress.Utils.ToolTipControlInfo() { SuperTip = sTooltip1, ToolTipPosition = e.Location };
                        _Info.Object = System.Guid.NewGuid();
                        toolTipController1.ShowHint(_Info);
                    }
                }
                else
                {
                    toolTipController1.HideHint();
                    IsOpened = false;
                }
            }
            catch (System.Exception ex) { Library.DialogBox.Error("Lỗi ở mousemove " + ex.Message); }
        }

        private void MapControl_MapItemClick(object sender, DevExpress.XtraMap.MapItemClickEventArgs e)
        {
            try
            {
                if (e.MouseArgs.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    // Tạo thử thêm 1 đối tượng
                    string nameAttribute = e.Item.Attributes["NAME"].Value.ToString();
                    int category = (int)e.Item.Attributes["CATEGORY"].Value;

                    using (var frm = new DichVu.MatBang.ViewMap.Data.FrmShowItemDoubleCLickData() { name = nameAttribute, category = category }) { frm.ShowDialog(); nameAttribute = frm.name; category = frm.category; }
                    e.Item.Attributes["NAME"].Value = nameAttribute;
                    e.Item.Attributes["CATEGORY"].Value = category;
                }
            }
            catch(System.Exception ex) { Library.DialogBox.Error("Lỗi ở mapitemclick " + ex.Message); }
            
        }

        private void MapEditor_MapItemCreating(object sender, DevExpress.XtraMap.MapItemCreatingEventArgs e)
        {
            try
            {
                e.Item.Attributes.Add(new DevExpress.XtraMap.MapItemAttribute() { Name = "NAME", Type = typeof(int), Value = "" });
                e.Item.Attributes.Add(new DevExpress.XtraMap.MapItemAttribute() { Name = "CATEGORY", Type = typeof(int), Value = 1 });
                DevExpress.XtraMap.MapShape mapShape = e.Item as DevExpress.XtraMap.MapShape;
                if (mapShape != null)
                {
                    mapShape.Fill = System.Drawing.Color.Orange;

                    mapItemStorage.Items.Add(mapShape);
                }
            }
            catch (System.Exception ex) { Library.DialogBox.Error("Lỗi ở mapitemcreating " + ex.Message); }
          
        }

        /// <summary>
        /// Tạo map control
        /// </summary>
        private void CreateMapControl()
        {
            mapControl = new DevExpress.XtraMap.MapControl();
            mapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            //mapControl.Location = new System.Drawing.Point(0, 29); //(0, 29)
            mapControl.Name = "mapControl1";
            mapControl.Size = new System.Drawing.Size(1017, 511);
            mapControl.TabIndex = 0;
            mapControl.ZoomLevel = 2D;
            //DevExpress.XtraMap.CartesianMapCoordinateSystem cartesianMapCoordinateSystem1 = new DevExpress.XtraMap.CartesianMapCoordinateSystem();
            //this.mapControl.CenterPoint = new DevExpress.XtraMap.GeoPoint(-50D, -90D);
            //cartesianMapCoordinateSystem1.MeasureUnit = DevExpress.XtraMap.MeasureUnit.Meter;
            //this.mapControl.CoordinateSystem = cartesianMapCoordinateSystem1;

            mapControl.MapEditor.PanelOptions.ShowAddDotButton = false;
            mapControl.MapEditor.PanelOptions.ShowAddLineButton = false;
            mapControl.MapEditor.PanelOptions.ShowAddPolylineButton = false;
            mapControl.MapEditor.PanelOptions.ShowAddPushpinButton = false;
            mapControl.MapEditor.ShowEditorPanel = true;
            mapControl.MapEditor.AllowSaveActions = true;
            //mapControl.MapEditor.SetEditMode();

            this.Controls.Add(mapControl);
        }

        private void itemChangeImage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Upload ảnh lên ftp, sau đó lấy ảnh từ ftp về, load layer image
            UploadToFtp(true, true, false, "ImgUpload",110, 1);

            // Load lại layer image
            CreateLayerImage();
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
                var frm = new FTP.frmUploadFile() { IsChangeName = !choose };
                if (!choose) frm.IsChangeName = true;
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

            public LocalTileSource(DevExpress.XtraMap.ICacheOptionsProvider cacheOptionsProvider) : base((int)CalculateTotalImageSize(maxZoomLevel),(int)CalculateTotalImageSize(maxZoomLevel),tileSize,tileSize, cacheOptionsProvider)
            {
                //System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
                //directoryPath = directoryInfo.FullName;
                Library.MasterDataContext masterDataContext = new Library.MasterDataContext();
                var ftp = masterDataContext.tblConfigs.FirstOrDefault();
                //if (ftp == null) return null;
                Library.ViewMap viewMap = masterDataContext.ViewMaps.FirstOrDefault(_ => _.MaTn == 110 & _.Stt == 1);
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

                

                if (zoomLevel <= maxZoomLevel & titlePositionX == 1 & titlePositionY == 0 & directoryPath !=null)
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
            if(imageLayer !=null) mapControl.Layers.Remove(imageLayer);

            imageLayer = new DevExpress.XtraMap.ImageLayer();
            mapControl.Layers.Add(imageLayer);
            imageLayer.DataProvider = new LocalProvider();
        }

        #endregion

        private void itemUploadShp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UploadToFtp(false, false, true, "ShpUpload", 110, 2);
        }

        private void itemUploadDbf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UploadToFtp(false, false, true, "DbfUpload", 110, 3);
        }

        #region Layer Shp

        private void LoadLayerShp()
        {
            try
            {
                if (vectorItemsLayer != null) mapControl.Layers.Remove(vectorItemsLayer);

                vectorItemsLayer = new DevExpress.XtraMap.VectorItemsLayer();
                mapControl.Layers.Add(vectorItemsLayer);

                // Đổ data cho vectorItemsLayer
                DevExpress.XtraMap.ShapefileDataAdapter shapefileDataAdapter = new DevExpress.XtraMap.ShapefileDataAdapter();
                //DevExpress.XtraMap.CartesianSourceCoordinateSystem cartesianSourceCoordinateSystem1 = new DevExpress.XtraMap.CartesianSourceCoordinateSystem();
                //shapefileDataAdapter.SourceCoordinateSystem = cartesianSourceCoordinateSystem1;
                shapefileDataAdapter.FileUri = GetUriShp();
                
                vectorItemsLayer.Data = shapefileDataAdapter;

                //VectorFileLayer layer = (VectorFileLayer)this.mapControl1.Layers[0];
                //System.IO.Stream stream = DevExpress.Utils.ResourceStreamHelper.GetStream(path + s + fileName, System.Reflection.Assembly.GetExecutingAssembly());
                //System.IO.Stream dbfStream = DevExpress.Utils.ResourceStreamHelper.GetStream(path + s + fileName, System.Reflection.Assembly.GetExecutingAssembly());
                ////((ShapefileLoader)layer.FileLoader).LoadFromStream(stream, dbfStream);

                // Legend
                DevExpress.XtraMap.ColorListLegend colorListLegend = new DevExpress.XtraMap.ColorListLegend();
                colorListLegend.Header = "Loại mặt bằng";
                colorListLegend.Layer = vectorItemsLayer;
                mapControl.Legends.Add(colorListLegend);

                // Color
                vectorItemsLayer.Colorizer = GetMapColorizer();

                //shapefileDataAdapter.ItemsLoaded += ShapefileDataAdapter_ItemsLoaded;
                vectorItemsLayer.Colorizer = GetMapColorizer();
                mapControl.ZoomToFit(vectorItemsLayer.Data.Items);
                mapControl.ZoomOut();
            }
            catch (System.Exception ex) { Library.DialogBox.Error(ex.Message); }
        }

        private void ShapefileDataAdapter_ItemsLoaded(object sender, DevExpress.XtraMap.ItemsLoadedEventArgs e)
        {
            Library.MasterDataContext masterDataContext = new Library.MasterDataContext();
            var lTrangThaiMatBang = masterDataContext.mbMatBangs.Where(_ => _.MaTN == 110).Select(_ => new { _.MaSoMB, _.MaTT,_.MaMB }).ToList();

            foreach(var item in e.Items)
            {
                if (item.Attributes["NAME"] != null)
                {
                    var trangThai = lTrangThaiMatBang.FirstOrDefault(_ => _.MaSoMB.Trim() == item.Attributes["NAME"].Value.ToString().Trim());
                    if (trangThai != null)
                    {
                        if (item.Attributes["CATEGORY"] != null) item.Attributes.Remove((DevExpress.XtraMap.MapItemAttribute)item.Attributes["CATEGORY"]);
                        item.Attributes.Add(new DevExpress.XtraMap.MapItemAttribute() { Name = "CATEGORY", Type = typeof(int), Value = trangThai.MaTT });
                    }
                }
            }
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

        public System.Uri GetUriShp()
        {
            try
            {
                Library.MasterDataContext masterDataContext = new Library.MasterDataContext();
                var ftp = masterDataContext.tblConfigs.FirstOrDefault();

                // Download file dbf
                Library.ViewMap viewMap1 = masterDataContext.ViewMaps.FirstOrDefault(_ => _.MaTn == 110 & _.Stt == 3);
                if (viewMap1 != null & ftp != null)
                {
                    string strExtexsion = System.IO.Path.GetExtension(viewMap1.LinkImage).Trim();
                    if (strExtexsion == ".dbf") { string fileName = GetUriFileName(ftp.WebUrl, viewMap1.LinkImage); }
                    else
                    {
                        Library.DialogBox.Error("Sai định dạng file dbf - Tên file: "+ viewMap1.LinkImage);
                        return null;
                    }
                }

                // Download file Shp
                Library.ViewMap viewMap = masterDataContext.ViewMaps.FirstOrDefault(_ => _.MaTn == 110 & _.Stt == 2);
                if (viewMap != null & ftp != null)
                {
                    string strExtexsion = System.IO.Path.GetExtension(viewMap.LinkImage).Trim();
                    if (strExtexsion == ".shp") { string fileName = GetUriFileName(ftp.WebUrl, viewMap.LinkImage); return GetFileUri(fileName); }
                    else
                    {
                        Library.DialogBox.Error("Sai định dạng file shp - Tên file: " + viewMap.LinkImage);
                        return null;
                    }
                }
                return null;
            }
            catch { return null; }
        }

        private string GetUriFileName(string webUrl, string linkImage)
        {
            string path = System.Windows.Forms.Application.StartupPath;
            var fileName = linkImage.Substring(linkImage.LastIndexOf('/') + 1);
            var frm = new FTP.frmDownloadFile();
            frm.FileName = linkImage;
            frm.SavePath = path + "\\" + fileName;
            frm.IsFileMap = true;
            //if(frm.SaveAs())
            //if (!isShowing) this.Opacity = 0.0;
            //else this.Opacity = 1.0;
            frm.Opacity = 0.0;
            frm.Left = -16384;
            frm.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            frm.ShowDialog();
            return fileName;
        }

        public System.Uri GetFileUri(string fileName)
        {
            return new System.Uri("file:\\\\" + GetRelativePath(fileName), System.UriKind.RelativeOrAbsolute);
        }

        public string GetRelativePath(string name)
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

        #endregion

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            try
            {
                mapControl.Update();
                Library.MasterDataContext masterDataContext = new Library.MasterDataContext();
                var lTrangThaiMatBang = masterDataContext.mbMatBangs.Where(_ => _.MaTN == 110).Select(_ => new { _.MaSoMB, _.MaTT, _.MaMB }).ToList();

                foreach (DevExpress.XtraMap.MapItem mapItem in vectorItemsLayer.Data.Items) { 
                    if(mapItem.Attributes["NAME"]!=null)
                    {
                        var trangThai = lTrangThaiMatBang.FirstOrDefault(_ => _.MaSoMB.Trim() == mapItem.Attributes["NAME"].Value.ToString().Trim());
                        if (trangThai != null)
                        {
                            if (mapItem.Attributes["CATEGORY"] != null) mapItem.Attributes.Remove((DevExpress.XtraMap.MapItemAttribute)mapItem.Attributes["CATEGORY"]);
                            mapItem.Attributes.Add(new DevExpress.XtraMap.MapItemAttribute() { Name = "CATEGORY", Type = typeof(int), Value = trangThai.MaTT });
                        }
                    }
                    mapItemStorage.Items.Add(mapItem); }

                if (vectorItemsLayer != null) mapControl.Layers.Remove(vectorItemsLayer);

                vectorItemsLayer = new DevExpress.XtraMap.VectorItemsLayer();
                vectorItemsLayer.Data = mapItemStorage;
                mapControl.Layers.Add(vectorItemsLayer);

                mapItemStorage = new DevExpress.XtraMap.MapItemStorage();

                // Legend
                if(colorListLegend!=null) mapControl.Legends.Remove(colorListLegend);
                colorListLegend = new DevExpress.XtraMap.ColorListLegend();
                colorListLegend.Header = "Loại mặt bằng";
                colorListLegend.Layer = vectorItemsLayer;
                mapControl.Legends.Add(colorListLegend);

                // Color
                vectorItemsLayer.Colorizer = GetMapColorizer();

                var option = new DevExpress.XtraMap.ShpExportOptions();
                option.ExportToDbf = true;

                Library.ViewMap viewMap = masterDataContext.ViewMaps.FirstOrDefault(_ => _.MaTn == 110 & _.Stt == 2);
                if (viewMap == null) return;
                Library.ViewMap viewMapDbf = masterDataContext.ViewMaps.FirstOrDefault(_ => _.MaTn == 110 & _.Stt == 3);
                if (viewMap == null) return;

                string path = System.Windows.Forms.Application.StartupPath;
                string s = "\\";
                var fileName = viewMap.LinkImage.Substring(viewMap.LinkImage.LastIndexOf('/') + 1);
                mapControl.MapEditor.ActiveLayer.ExportToShp(path + s + fileName, option);
                vectorItemsLayer.ExportToShp(path + s + fileName, new DevExpress.XtraMap.ShpExportOptions() { ExportToDbf = true, ShapeType = DevExpress.XtraMap.ShapeType.Polygon });

                UpdateShpToFtp(path, s, fileName, "ShpUpload");

                // Update ftp dbf
                var fileNameDbf = viewMapDbf.LinkImage.Substring(viewMapDbf.LinkImage.LastIndexOf('/') + 1);
                UpdateShpToFtp(path, s, fileNameDbf, "DbfUpload");

                masterDataContext.Dispose();

                mapControl.Update();

                //DevExpress.XtraEditors.XtraMessageBox.Show(ParentForm, string.Format("Items succesfully save to {0} file", path + s + fileName), "Info",
                //    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch (System.Exception ex) { Library.DialogBox.Error(ex.Message); }
        }

        private void UpdateShpToFtp(string path, string s, string fileName, string folder)
        {
            var clientPathShp = path + s + fileName;

            using (var frm = new FTP.frmUploadFile())
            {
                frm.ClientPath = clientPathShp;
                frm.FileName = fileName;
                frm.Folder = folder;
                frm.Opacity = 0.0;
                frm.Left = -16384;
                frm.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                frm.IsChangeName = true;
                frm.ShowDialog();
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }
    }
}