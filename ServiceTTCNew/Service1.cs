using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.ServiceProcess;
using System.Data.Linq.SqlClient;
using System.IO;
namespace ServiceTTCNew
{

    public partial class Service1 : ServiceBase
    {
        private System.Diagnostics.EventLog eventLogServices;
        private System.Timers.Timer scheduleServices;
        public Service1()
        {
            InitializeComponent();
            // <strong>Ghi lại hoạt động của Services bằng EventLog</strong>
            eventLogServices = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("TTCSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "DTTCSource", "TTCLog");
            }
            eventLogServices.Source = "TTCSource";
            eventLogServices.Log = "TTCLog";

            // <strong>Xử lý nghiệp vụ mang tính tuần hoàn bằng cách sử dụng 1 Timer.</strong>
            scheduleServices = new System.Timers.Timer();
            scheduleServices.Interval = 60000;
            scheduleServices.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            scheduleServices.Start();
        }
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            UpdateTrangThai();
            eventLogServices.WriteEntry("Ghi log nghiệp vụ");
        }
        protected override void OnStart(string[] args)
        {
            scheduleServices.Stop();
            eventLogServices.WriteEntry("Started");
            scheduleServices.Start();
        }

        protected override void OnStop()
        {
            scheduleServices.Stop();
            eventLogServices.WriteEntry("Stopped");
        }
       
        void UpdateTrangThai()
        {

            DataClasses1DataContext db = new DataClasses1DataContext();

            var ListDS = (from yc in db.tnycYeuCaus
                          join ls in db.tnycLichSuCapNhats on yc.ID equals ls.MaYC
                          where yc.MaTT == 3 & ls.MaTT == 3
                              & SqlMethods.DateDiffHour(ls.NgayCN, db.GetSystemDate()) > 24
                          select new
                          {
                              yc.ID,
                              LSID = ls.ID
                          }
                              ).ToList();
        }
    }
}
