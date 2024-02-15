using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Library.HeThongCls
{
    public class SendMailCls
    {
        #region Bien Can Dung
        private string _smtpServer;
        private string _MailFrom;
        private string _PWD;
        private string _MailTo;
        private string _Subject;
        private string _Content;
        #endregion

        #region Thuoc tin
        private string smtpServer
        {
            get { return Library.Properties.Settings.Default.MailServer; }
            set { _smtpServer = value; }
        }

        private string MailFrom
        {
            get { return Library.Properties.Settings.Default.YourMail; }
        }

        private string PWD
        {
            get { return Commoncls.DecodeString(Library.Properties.Settings.Default.MailPass); }
        }

        public string MailTo
        {
            get { return _MailTo; }
            set { _MailTo = value; }
        }

        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        public string ErrorText { get; set; }
        #endregion

        #region Phuong thuc xu ly
        public void SendMail()
        {
            try
            {
                MailMessage objMail = new MailMessage(MailFrom, MailTo, Subject, Content);
                objMail.IsBodyHtml = true;
                objMail.BodyEncoding = System.Text.Encoding.UTF8;

                SmtpClient smtpMail = new SmtpClient(smtpServer);
                smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpMail.EnableSsl = true;
                smtpMail.Credentials = new NetworkCredential(MailFrom, PWD);
                smtpMail.Send(objMail);
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
            }
        }

        public void SendMailAttachFile(string fileName)
        {
            try
            {
                MailMessage objMail = new MailMessage(MailFrom, MailTo, Subject, Content);
                objMail.IsBodyHtml = true;
                objMail.BodyEncoding = System.Text.Encoding.UTF8;

                Attachment attachFile = new Attachment(fileName);
                objMail.Attachments.Add(attachFile);

                SmtpClient smtpMail = new SmtpClient(smtpServer);
                smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpMail.EnableSsl = true;
                smtpMail.Credentials = new NetworkCredential(MailFrom, PWD);
                smtpMail.Send(objMail);
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
            }
        }

        public void SendMailAttachFile(System.IO.Stream stream, string name)
        {
            MailMessage objMail = new MailMessage(MailFrom, MailTo, Subject, Content);
            objMail.IsBodyHtml = true;
            objMail.BodyEncoding = System.Text.Encoding.UTF8;

            stream.Seek(0, System.IO.SeekOrigin.Begin);
            Attachment attachFile = new Attachment(stream, name, "application/pdf");  
            objMail.Attachments.Add(attachFile);

            SmtpClient smtpMail = new SmtpClient(smtpServer);
            smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpMail.EnableSsl = true;
            smtpMail.Credentials = new NetworkCredential(MailFrom, PWD);
            smtpMail.Send(objMail);
        }
        #endregion
    }
}
