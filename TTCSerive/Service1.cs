using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace TTCSerive
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

            MasterTTCDataContext db = new MasterTTCDataContext();
            db.CommandTimeout = 300000;//1000 = 1s
            var ListDS = (from yc in db.tnycYeuCaus
                          join ls in db.tnycLichSuCapNhats on yc.ID equals ls.MaYC
                          where yc.MaTT == 3 & ls.MaTT == 3
                              & SqlMethods.DateDiffHour(ls.NgayCN, db.GetLocalDate()) > 24
                          select new
                          {
                              yc.ID,
                              LSID = ls.ID
                          }
                              ).ToList();
            foreach (var i in ListDS)
            {
                var objYC = db.tnycYeuCaus.FirstOrDefault(p => p.ID == i.ID);
                objYC.MaTT = 5;
                objYC.Rating = 5;
                objYC.RatingComment = "Tôi rất hài lòng với chất lượng dịch vụ/ I am pleased with service.";
                objYC.RatingDate = DateTime.UtcNow.AddHours(7);
                db.SubmitChanges();
                var objLS = new tnycLichSuCapNhat();
                objLS.MaYC = i.ID;
                objLS.MaTT = 5;
                objLS.NoiDung = "Phản ánh này được đóng tự động (vì sau 24h không nhận được đánh giá hoặc yêu cầu khác từ Quý cư dân";
                objLS.NgayCN = DateTime.UtcNow.AddHours(7);
                objLS.MaNV = 6;
                db.tnycLichSuCapNhats.InsertOnSubmit(objLS);
                db.SubmitChanges();
            }
        }
    }
}
