using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraMap;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Types;

namespace DichVu.MatBang.ViewMap.ViewMap
{
    public partial class frmViewMap2 : DevExpress.XtraEditors.XtraForm
    {
        public frmViewMap2()
        {
            InitializeComponent();
        }

        private void frmViewMap_Load(object sender, EventArgs e)
        {
            //Init();
        }

        void Init()
        {
            //var sqlItem = SqlGeography.STGeomFromText(new SqlChars(new SqlString("MULTILINESTRING((59.92695 50.9863, 59.9269 50.9858, 59.9272 50.9857, 59.9272 50.9863, 59.92695 50.9863), (59.92695 50.9863, 59.9289 50.9858, 59.9272 50.9897, 59.9272 50.9863, 59.92695 50.9863))")), 4326);


            //var t = new SqlGeometryItemStorage();


            var adapter = new SqlGeometryItemStorage();

            foreach (Data sqlGeography in GetData())
            {
                adapter.Items.Add(sqlGeography.Geo1Item); // Well-Known Text and SRID values   
            }

            vectorItemsLayer1.Data = adapter;
            mapControl1.ZoomToFitLayerItems(new LayerBase[] { vectorItemsLayer1 });
        }


        List<Data> GetData()
        {
            List<Data> list = new List<Data>();
            list.Add(new Data(0, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.92478447098253 50.978582123054345, 59.922150353562351 50.977989061529918, 59.922396820689386 50.977157234976175, 59.925092554891329 50.977881232161842, 59.92478447098253 50.978582123054345))	")), 4326)));
            list.Add(new Data(1, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.929138332130478 50.985412723831516, 59.92828345853048 50.984983468151519, 59.929556674530481 50.983379215991505, 59.930240573410487 50.983801196151511, 59.929138332130478 50.985412723831516))	")), 4326)));
            list.Add(new Data(2, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.925837439844365 50.988409995099822)	")), 4326)));
            list.Add(new Data(3, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.915322207520767 50.987352534723591)	")), 4326)));
            list.Add(new Data(4, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.923489068852909 50.9782236330091, 59.922213657289262 50.977900933553713, 59.92242382053189 50.977219495160966, 59.923670685002946 50.977539496780921, 59.923489068852909 50.9782236330091))	")), 4326)));
            list.Add(new Data(5, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.930799991368417 50.980311294789615, 59.930198843690583 50.980634225360355, 59.929639218110168 50.979882540911582, 59.930186246065681 50.979477025634687, 59.930799991368417 50.980311294789615))	")), 4326)));
            list.Add(new Data(6, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.924221814717285 50.986971636416044)	")), 4326)));
            list.Add(new Data(7, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.92695 50.9863, 59.9269 50.9858, 59.9272 50.9857, 59.9272 50.9863, 59.92695 50.9863))	")), 4326)));
            list.Add(new Data(8, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.918740042350827 50.983387721348372, 59.917683562617057 50.983560192756272, 59.917409422918482 50.981577248936574, 59.918475544426705 50.981405208702661, 59.918740042350827 50.983387721348372))	")), 4326)));
            list.Add(new Data(9, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.930162178120817 50.979175058762245)	")), 4326)));
            list.Add(new Data(10, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.93016 50.98362, 59.93028 50.9835, 59.93052 50.98369, 59.93036 50.98387, 59.93016 50.98362))	")), 4326)));
            list.Add(new Data(11, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.927535725475444 50.9835297818152)	")), 4326)));
            list.Add(new Data(12, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.929575238559849 51.017881446058617, 59.926643116342987 51.0182802848056, 59.92597692415022 51.017373036227291, 59.927624873258651 51.016877774926087, 59.92818587721046 51.017460693094762, 59.929321033644193 51.016987346010424, 59.929575238559849 51.017881446058617))	")), 4326)));
            list.Add(new Data(13, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.91849035633787 50.983416505858223, 59.917861614776676 50.983510148218407, 59.917723380816412 50.982662907816788, 59.918396713977693 50.982569265456611, 59.91849035633787 50.983416505858223))	")), 4326)));
            list.Add(new Data(14, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.918556911112915 50.982435991064953, 59.917746191639104 50.982559254322176, 59.917578294859666 50.98159104956077, 59.91840257168473 50.981463381896518, 59.918556911112915 50.982435991064953))	")), 4326)));
            list.Add(new Data(15, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.932059841314988 50.996689713631888, 59.93068043977042 50.996168428164466, 59.930900430391418 50.994757564420091, 59.93252498834746 50.995093778123938, 59.932059841314988 50.996689713631888))	")), 4326)));
            list.Add(new Data(16, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.927862952189869 50.989747128051391)	")), 4326)));
            list.Add(new Data(17, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.928124989128676 50.986699147455006)	")), 4326)));
            list.Add(new Data(18, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.927930585387834 50.979549859706296)	")), 4326)));
            list.Add(new Data(19, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.923751461828168 50.988418167627515)	")), 4326)));
            list.Add(new Data(20, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.93029094838154 50.978564542707062)	")), 4326)));
            list.Add(new Data(21, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.915322207520767 50.9873525347236)	")), 4326)));
            list.Add(new Data(22, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.929531346302682 50.984526283826462, 59.928985920935524 50.984192445886215, 59.929564259902428 50.983440135034961, 59.930119089155227 50.983806886574953, 59.929531346302682 50.984526283826462))	")), 4326)));
            list.Add(new Data(23, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.926679289604216 51.017662895116068)	")), 4326)));
            list.Add(new Data(24, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.924217753461484 50.986971636416044)	")), 4326)));
            list.Add(new Data(25, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.912747633812295 50.9832059523981)	")), 4326)));
            list.Add(new Data(26, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.92951612564358 50.9798525814742, 59.928833856982223 50.979223370723091, 59.929098486742284 50.978684660140125, 59.930126132904888 50.979314947955757, 59.92951612564358 50.9798525814742))	")), 4326)));
            list.Add(new Data(27, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.924773791744848 50.978428108681577, 59.923747435565836 50.978156895390747, 59.923887941329461 50.9776044803531, 59.925039687128006 50.977906953730574, 59.924773791744848 50.978428108681577))	")), 4326)));
            list.Add(new Data(28, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.9297 50.98, 59.9295 50.9797, 59.9304 50.9795, 59.9305 50.9798, 59.9297 50.98))	")), 4326)));
            list.Add(new Data(29, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.933861867349272 50.997883440223745)	")), 4326)));
            list.Add(new Data(30, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.928967357410485 50.985300862711512, 59.92834901501049 50.9849888479915, 59.928909153250487 50.9842495500715, 59.929440266210484 50.984591499511509, 59.928967357410485 50.985300862711512))	")), 4326)));
            list.Add(new Data(31, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.924957272223345 50.986526477000893)	")), 4326)));
            list.Add(new Data(32, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.912469514370436 50.9821233365566)	")), 4326)));
            list.Add(new Data(33, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.930568454809887 50.9829486943165, 59.93030837845096 50.982757787201969, 59.93041351570244 50.982611148403855, 59.930744605818944 50.982684006674617, 59.930568454809887 50.9829486943165))	")), 4326)));
            list.Add(new Data(34, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.929313887303046 50.979642813982466, 59.928765723176433 50.979201941766753, 59.9290593480043 50.978570395744832, 59.930091490377556 50.979193445580052, 59.930458242018005 50.979564642694939, 59.930940132301338 50.9803125472927, 59.930159167814359 50.980737687723476, 59.929313887303046 50.979642813982466))	")), 4326)));
            list.Add(new Data(35, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POLYGON ((59.927 50.9847, 59.9272 50.9841, 59.9277 50.9843, 59.92735 50.98476, 59.927 50.9847))	")), 4326)));
            list.Add(new Data(36, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.9129559590216 50.984413351867495)	")), 4326)));
            list.Add(new Data(37, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.913644383951841 50.985576336825275)	")), 4326)));
            list.Add(new Data(38, SqlGeography.STGeomFromText(new SqlChars(new SqlString("	POINT (59.912300316230912 50.981179251691849)	")), 4326)));
            return list;
        }

        public class Data
        {
            public Data(int id, SqlGeography geo)
            {
                Id = id;
                Geo1 = geo;
            }
            public object Tag { get; set; }
            public int Id { get; set; }

            public SqlGeography Geo1 { get; set; }

            public SqlGeometryItem Geo1Item { get => new SqlGeometryItem(Geo1.ToString(), 4326); set { } }


            /// <summary>
            /// optional
            /// </summary>
            public SqlGeography Geo2 { get; set; }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Export();
        }

        void Export()
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "SHP files|*.shp";
                dialog.CreatePrompt = true;
                dialog.OverwritePrompt = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var option = new DevExpress.XtraMap.ShpExportOptions();
                    option.ExportToDbf = true;

                    mapControl1.MapEditor.ActiveLayer.ExportToShp(dialog.FileName, option);
                    vectorItemsLayer1.ExportToShp(dialog.FileName, new DevExpress.XtraMap.ShpExportOptions() { ExportToDbf = true, ShapeType = DevExpress.XtraMap.ShapeType.Polygon });
                    XtraMessageBox.Show(ParentForm, string.Format("Items succesfully exported to {0} file", dialog.FileName), "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}