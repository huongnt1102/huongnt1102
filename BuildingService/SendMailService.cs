using System;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using System.IO;
using System.Threading;

namespace BuildingService
{    
    partial class SendMailService : ServiceBase
    {
        System.Timers.Timer timerEmail;
        System.Timers.Timer timerCongNo;

        public SendMailService()
        {
            InitializeComponent();

            timerEmail = new System.Timers.Timer();
            timerEmail.Elapsed += new ElapsedEventHandler(timerEmail_Elapsed);
            timerEmail.Interval = 10000;
            timerEmail.Enabled = true;

            timerCongNo = new System.Timers.Timer();
            timerCongNo.Elapsed += new ElapsedEventHandler(timerCongNo_Elapsed);
            timerCongNo.Interval = 30000;
            timerCongNo.Enabled = true;
        }

        protected override void OnStart(string[] args)
        {
            WriteToLog(String.Format("========= {0} =========", DateTime.Now));
            WriteToLog("Service is starting.");

            timerEmail.Start();
            timerCongNo.Start();
        }

        protected override void OnStop()
        {
            base.OnStop();
            timerEmail.Stop();
            timerCongNo.Stop();
        }

        protected override void OnPause()
        {
            base.OnPause();
            timerEmail.Stop();
            timerCongNo.Stop();
        }

        protected override void OnContinue()
        {
            base.OnContinue();
            timerEmail.Start();
            timerCongNo.Start();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
            timerEmail.Stop();
            timerCongNo.Stop();
        }

        void timerEmail_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                SendMailNew();
                Thread.Sleep(5000);
                sendmaiNhacNo();
            }
            catch (Exception ex)
            {
                WriteToLog("Error: " + ex.Message);
            }
        }

        void timerCongNo_Elapsed(object sender, ElapsedEventArgs e)
        {
            //select top 3 mbMatBang
            //foreach
        }

        private void SendMailNew()
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                SendMailCls smcls = new SendMailCls();
                var objEmail = db.SendMailDetails.FirstOrDefault(p => p.TrangThai == 3 | p.TrangThai == 2);
                if (objEmail == null) return;
                if (objEmail.SendMail.SendMailAccount != null)
                {
                    BuildingService.Properties.Settings.Default.MailServer = objEmail.SendMail.SendMailAccount.Server;
                    BuildingService.Properties.Settings.Default.MailPass = objEmail.SendMail.SendMailAccount.Password;
                    BuildingService.Properties.Settings.Default.YourMail = objEmail.SendMail.SendMailAccount.DiaChi;
                    BuildingService.Properties.Settings.Default.Save();
                }

                smcls.Subject = objEmail.SendMail.TieuDe;
                smcls.MailTo = objEmail.tnKhachHang.EmailKH;
                smcls.Content = objEmail.SendMail.NoiDung;


                try
                {
                    smcls.SendMail();
                    objEmail.TrangThai = 1; // Gui thanh cong
                    WriteToLog(String.Format("Send Succeeded: FROM:: {0} :::: TO:: {1}", objEmail.SendMail.SendMailAccount.DiaChi, objEmail.tnKhachHang.EmailKH));
                }
                catch (Exception ex)
                {
                    objEmail.TrangThai = 2; // ko gui dc
                    WriteToLog("Error: " + ex.Message);
                }
                finally
                {
                    db.SubmitChanges();
                }
            }
        }

        private void sendmaiNhacNo()
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                SendMailCls smcls = new SendMailCls();
                var objnhacno = db.SendMailNhacNos.FirstOrDefault(p => p.TrangThai == 3 | p.TrangThai == 2);
                if (objnhacno == null) return;
                if (objnhacno.SendMailAccount != null)
                {
                    BuildingService.Properties.Settings.Default.MailServer = objnhacno.SendMailAccount.Server;
                    BuildingService.Properties.Settings.Default.MailPass = objnhacno.SendMailAccount.Password;
                    BuildingService.Properties.Settings.Default.YourMail = objnhacno.SendMailAccount.DiaChi;
                    BuildingService.Properties.Settings.Default.Save();
                }

                smcls.Subject = objnhacno.TieuDe;
                smcls.MailTo = objnhacno.tnKhachHang.EmailKH;
                smcls.Content = objnhacno.NoiDung;

                try
                {
                    MemoryStream stream = new MemoryStream();
                    byte[] FileBytesdl = null;
                    FileBytesdl = (byte[])objnhacno.FileDinhKem.ToArray();
                    stream.Write(FileBytesdl, 0, FileBytesdl.Length);
                    smcls.SendMailAttachFile(stream,"GuiKhachHang.pdf");

                    objnhacno.TrangThai = 1; // Gui thanh cong
                    WriteToLog(String.Format("Send Succeeded: FROM:: {0} :::: TO:: {1}", objnhacno.SendMailAccount.DiaChi, objnhacno.tnKhachHang.EmailKH));
                }
                catch (Exception ex)
                {
                    objnhacno.TrangThai = 2; // ko gui dc
                    WriteToLog("Error: " + ex.Message);
                }
                finally
                {
                    db.SubmitChanges();
                }
            }
            
        }

        private void WriteToLog(string TextToLog)
        {
            System.IO.StreamWriter sw = null;

            // Define log file path and name. 
            string CurrentLogFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Log.txt";

            sw = new System.IO.StreamWriter(CurrentLogFilePath, true);

            // Write data to log file. 
            sw.WriteLine(TextToLog);
            sw.Flush();
            sw.Close();
        }
    }
}
