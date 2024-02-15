using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using Library;
using System.Data.Linq.SqlClient;
namespace LandSoftBuildingMain
{
    public partial class QuangForm : Form
    {
        private System.Timers.Timer scheduleServices;
        public QuangForm()
        { 
            InitializeComponent();
            scheduleServices = new System.Timers.Timer();
            scheduleServices.Interval = 10000;//60s;
            scheduleServices.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            scheduleServices.Start();
        }
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            UpdateTrangThai();
            //eventLogServices.WriteEntry("Ghi log nghiệp vụ");  
        }
        void UpdateTrangThai()
        {

            //MasterDataContext db = new MasterDataContext();

            //var ListDS = (from yc in db.tnycYeuCaus
            //              join ls in db.tnycLichSuCapNhats on yc.ID equals ls.MaYC
            //              where yc.MaTT == 3 & ls.MaTT == 3
            //                  & SqlMethods.DateDiffHour(ls.NgayCN, DateTime.Now) > 24
            //              select new
            //              {
            //                  yc.ID,
            //                  LSID = ls.ID
            //              }
            //                  ).ToList();
            //foreach (var i in ListDS)
            //{
            //    var objYC = db.tnycYeuCaus.SingleOrDefault(p => p.ID == i.ID);
            //    objYC.MaTT = 5;
            //    db.SubmitChanges();
            //}
        }
    }
}
