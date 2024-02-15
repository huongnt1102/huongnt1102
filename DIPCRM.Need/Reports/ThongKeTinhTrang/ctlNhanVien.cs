using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;

using DevExpress.XtraEditors;
using Library;

namespace DIPCRM.Need.Reports.ThongKeTinhTrang
{
    public partial class ctlNhanVien : DevExpress.XtraEditors.XtraUserControl
    {
        public DateTime? tuNgay { get; set; }
        public DateTime? denNgay { get; set; }
        public int Width { get; set; }
        public string strTinhTrang { get; set; }
        public string strNhanVien { get; set; }
        public short? MaChiNhanh { get; set; }

        public ctlNhanVien()
        {
            InitializeComponent();
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = this.Width;// / 2;

            //using (var db = new MasterDataContext())
            //{
            //    var arrTinhTrang = strTinhTrang.Split(',');
            //    var arrNhanVien = strNhanVien.Split(',');

            //    var list = (from p in db.ncTrangThais
            //                join nc in db.ncNhuCaus on p.MaTT equals nc.MaTT //into nc_p from nc in nc_p.DefaultIfEmpty()
            //                join nv in db.tnNhanViens on nc.MaNVN equals nv.MaNV //into nv_nc from nv in nv_nc.DefaultIfEmpty()
            //                //join ct in db.CongTies on nv.MaCT equals ct.ID
            //                where (SqlMethods.DateDiffDay(tuNgay, nc.NgayNhap) >= 0 & SqlMethods.DateDiffDay(nc.NgayNhap, denNgay) >= 0)
            //                & (arrTinhTrang.Contains(nc.MaTT.ToString()) == true | strTinhTrang == "")
            //                & (arrNhanVien.Contains(nc.MaNVN.ToString()) == true | strNhanVien == "")
            //                & nv.MaCT==MaChiNhanh
            //                select new
            //                {
            //                    TongSoLuong = 1,
            //                    NhanVien = nv.HoTen,
            //                    TinhTrang = p.TenTT,
            //                }).ToList();

            //    pivotGridControl.DataSource = list;
            //}
        }
    }
}
