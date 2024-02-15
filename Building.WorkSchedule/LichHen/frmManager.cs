using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace Building.WorkSchedule.LichHen
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNV;
        public frmManager()
        {
            InitializeComponent();
        }

        //public void LoadData()
        //{
        //    var db = new MasterDataContext();
        //    try
        //    {
        //        gcLich.DataSource = (from lh in db.LichHens
        //            join nv in db.NhanViens on lh.MaNV equals nv.MaNV
        //            where
        //                lh.MaKH == this.MaKH | lh.MaNC == this.MaNC | lh.MaNV == this.MaNV | lh.MaNVu == this.MaNVU |
        //                lh.MaCDICH == this.MaCD
        //            orderby lh.NgayBD descending
        //            select new
        //            {
        //                lh.MaLH,
        //                lh.TieuDe,
        //                lh.NgayBD,
        //                lh.NgayKT,
        //                lh.DiaDiem,
        //                lh.DienGiai,
        //                nv.HoTen
        //            })
        //            .AsEnumerable()
        //            .Select((p, index) => new
        //            {
        //                STT = index + 1,
        //                p.MaLH,
        //                p.TieuDe,
        //                p.NgayBD,
        //                p.NgayKT,
        //                p.DiaDiem,
        //                p.DienGiai,
        //                HoTenNV = p.HoTen
        //            }).ToList();
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        db.Dispose();
        //    }
        //}

        private void frmManager_Load(object sender, EventArgs e)
        {
            var ctl = new SchedulerList_ctl();
            ctl.objNV = objNV;
            ctl.Dock = DockStyle.Fill;
            //ctl.lo
            this.Controls.Clear();
            this.Controls.Add(ctl);
        }
    }
}