using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraMap;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Types;

namespace DichVu.MatBang.ViewMap.ViewMap
{
    public partial class frmViewMap22 : DevExpress.XtraEditors.XtraForm
    {
        public frmViewMap22()
        {
            InitializeComponent();
        }

        private void frmViewMap_Load(object sender, EventArgs e)
        {

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
    }
}