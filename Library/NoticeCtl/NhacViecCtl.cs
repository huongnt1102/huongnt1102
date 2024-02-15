using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;
using System.Linq;

namespace Library.NoticeCtl
{
    public partial class NhacViecCtl : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        System.Timers.Timer mytimer;
        public NhacViecCtl()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void NhacViecCtl_Load(object sender, EventArgs e)
        {
            LoadData();
            
            //mytimer = new System.Timers.Timer();
            //mytimer.Elapsed += new System.Timers.ElapsedEventHandler(mytimer_Elapsed);
            //mytimer.Interval = 10000;
            //mytimer.Enabled = true;

            //mytimer.Start();
        }

        //void mytimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    LoadData();
        //}

        private void LoadData()
        {
            try
            {
                gcNhacViec.DataSource = db.tnNhacViec_Details
                        .Where(p => p.NguoiNhan == objnhanvien.MaNV & (p.DaDoc == false | p.DaDoc == null));
            }
            catch { }
        }

        private void grvNhacViec_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == ColDaDoc)
            {
                try
                {
                    db.SubmitChanges();
                }
                catch { }
            }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}
