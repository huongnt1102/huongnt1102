using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using System.Linq;
using System.Data.Linq.SqlClient;
using Library;


namespace DIPCRM.Need.Reports
{
    public partial class ctlMucDoXuLyCongViec : DevExpress.XtraEditors.XtraUserControl
    {
        Thread thread;
        delegate void Updateprocess();

        void LoadData()
        {
            try
            {
                KyBaoCao objKBC = new KyBaoCao();
                objKBC.Index = cmbKyBaoCao.SelectedIndex;
                objKBC.SetToDate();

                var maNV = (int?)lkNhanVien.EditValue;

                using (var db = new MasterDataContext())
                {
                    var ltResult = (from nc in db.ncNhuCaus
                                    join tt in db.ncTrangThais on nc.MaTT equals tt.MaTT
                                    where SqlMethods.DateDiffDay(dateTuNgay.DateTime, nc.NgayNhap) >= 0 
                                        & SqlMethods.DateDiffDay(nc.NgayNhap, dateDenNgay.DateTime) >= 0
                                        & (nc.MaNVQL == maNV | maNV == null)
                                    group nc.MaTT by new { tt.MaTT, tt.TenTT } into gr
                                    select new { gr.Key.TenTT, SoLuong = gr.Count() }).ToList();

                    var series1 = chartControl1.Series[0];
                    series1.Points.Clear();
                    foreach (var p in ltResult)
                    {
                        var point = new SeriesPoint();
                        point.Argument = p.TenTT;
                        point.Tag = p.TenTT;
                        point.Values = new double[] { Convert.ToDouble(p.SoLuong) };
                        series1.Points.Add(point);
                    }
                }
            }
            catch { }
        }

        void Process()
        {
            this.BeginInvoke(new Updateprocess(LoadData));
        }

        void BaoCaoLoad()
        {
            thread = new Thread(new ThreadStart(Process));
            thread.IsBackground = true;
            thread.Start();
        }

        public ctlMucDoXuLyCongViec()
        {
            InitializeComponent();
            //

            this.Load += new EventHandler(ctlMucDoXuLyCongViec_Load);
            cmbKyBaoCao.SelectedIndexChanged += new EventHandler(cmbKyBaoCao_SelectedIndexChanged);
            dateTuNgay.EditValueChanged += new EventHandler(dateTuNgay_EditValueChanged);
            dateDenNgay.EditValueChanged += new EventHandler(dateDenNgay_EditValueChanged);
            lkNhanVien.EditValueChanged += new EventHandler(lkNhanVien_EditValueChanged);
            lkNhanVien.KeyUp += new KeyEventHandler(lkNhanVien_KeyUp);
            chartControl1.ObjectHotTracked+=new HotTrackEventHandler(chartControl1_ObjectHotTracked);
        }

        void lkNhanVien_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) lkNhanVien.EditValue = null;
        }

        void lkNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            BaoCaoLoad();
        }

        void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            BaoCaoLoad();
        }

        void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            BaoCaoLoad();
        }

        void cmbKyBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = cmbKyBaoCao.SelectedIndex;
            objKBC.SetToDate();

            dateTuNgay.EditValue = objKBC.DateFrom;
            dateDenNgay.EditValue = objKBC.DateTo;
        }

        void chartControl1_ObjectHotTracked(object sender, HotTrackEventArgs e)
        {
            SeriesPoint point = e.AdditionalObject as SeriesPoint;

            if (point != null)
            {
                var view = chartControl1.Series[0].View as PieSeriesView;
                view.ExplodedPoints.Clear();
                view.ExplodedPoints.Add(point);

                string s = string.Format("{0}: {1:#,0}", point.Tag, point.Values[0]);
                toolTipController1.ShowHint(s);
            }
            else
                toolTipController1.HideHint();
        }

        void ctlMucDoXuLyCongViec_Load(object sender, EventArgs e)
        {
            try
            {
                KyBaoCao objKBC = new KyBaoCao();
                objKBC.Initialize(cmbKyBaoCao);

                using (var db = new MasterDataContext())
                {
                    //lkNhanVien.Properties.DataSource = db.Select(p => new { p.MaNV, MaSo = p.MaSoNV, HoTen = p.HoTenNV }).ToList();
                }

                this.Update();
            }
            catch { }
        }
    }
}
