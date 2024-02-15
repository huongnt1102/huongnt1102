using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using HtmlAgilityPack;
using Library;

namespace LandSoftBuilding.Marketing.Mail
{
    public class MailClient
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string Display { get; set; }
        public string Reply { get; set; }
        public string From { get; set; }
        public string Pass { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<Attachment> Attachs { get; set; }
        public string message { get; set; }
        public int? Susscess { get; set; }
        public int? Error { get; set; }
        public int? Status { get; set; }

        public MailClient()
        {
            this.Port = 25;
            //Port = 45;
        }

        public void Send()
        {
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            ServicePointManager.SecurityProtocol =
        SecurityProtocolType.Tls |
        SecurityProtocolType.Tls11 |
        SecurityProtocolType.Tls12;

            MailMessage objMail = new MailMessage();
            objMail.From = new MailAddress(From, Display);
            objMail.ReplyTo = new MailAddress(Reply, Display);
            objMail.To.Add(To);
            if (this.Cc != null) objMail.CC.Add(Cc);
            if (this.Bcc != null) objMail.Bcc.Add(Bcc);
            objMail.Subject = Subject;
            objMail.IsBodyHtml = true;

            #region AlternateView
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(this.Content);

            List<HtmlNode> imageNodes = null;
            try
            {
                imageNodes = (from HtmlNode n in doc.DocumentNode.SelectNodes("//img") where n.Attributes["src"].Value.IndexOf("http://") < 0 select n).ToList();
            }
            catch { }
            if (imageNodes != null && imageNodes.Count > 0)
            {
                for (int i = 0; i < imageNodes.Count; i++)
                {
                    var contentID = "cid:image" + i.ToString();
                    this.Content = this.Content.Replace(imageNodes[i].Attributes["src"].Value, contentID);
                }

                AlternateView alternateImage = AlternateView.CreateAlternateViewFromString(this.Content, System.Text.Encoding.UTF8, MediaTypeNames.Text.Html);
                for (int i = 0; i < imageNodes.Count; i++)
                {
                    var imgPath = imageNodes[i].Attributes["src"].Value.Trim().ToLower();
                    var strType = imgPath.Substring(imgPath.LastIndexOf('.') + 1, imgPath.Length - imgPath.LastIndexOf('.') - 1); //a.jpg
                    var imgType = MediaTypeNames.Image.Jpeg;
                    switch (strType)
                    {
                        case "gif": imgType = MediaTypeNames.Image.Gif; break;
                        case "tiff": imgType = MediaTypeNames.Image.Tiff; break;
                    }
                    var link = new LinkedResource(imgPath, imgType);
                    //var link = new LinkedResource(imgPath);
                    link.ContentId = "image" + i.ToString();
                    alternateImage.LinkedResources.Add(link);
                }

                objMail.AlternateViews.Add(alternateImage);
            }
            else
            {
                objMail.BodyEncoding = System.Text.Encoding.UTF8;
                objMail.Body = this.Content;
            }
            #endregion

            #region Attachs file
            if (this.Attachs != null)
            {
                foreach (var at in this.Attachs)
                {
                    objMail.Attachments.Add(at);
                }
            }
            #endregion

            var smtpMail = new SmtpClient
            {
                Host = this.SmtpServer,
                Port = this.Port,
                EnableSsl = this.EnableSsl,
                //EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(From.Trim(), Pass.Trim()),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            //MailMessage mail = new MailMessage();
            ////mail.To.Add(txtEmail.Text.Trim());
            //mail.To.Add("huongnt1102@gmail.com");
            ////mail.From = new MailAddress("tanthanh52xd52132137@gmail.com");user: cs.jgs@thanhthanhcong.com.vn
            //mail.From = new MailAddress("cs.jgs@thanhthanhcong.com.vn");
            //mail.Subject = "Confirmation of Registration on Job Junction.";
            //string Body = "Hi, this mail is to test sending mail using Gmail in ASP.NET";
            //mail.Body = Body;
            //mail.IsBodyHtml = true;
            ////SmtpClient smtpMail = new SmtpClient("smtp.gmail.com", 587);
            //// smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            ////SmtpClient smtpMail = new SmtpClient("mail.thanhthanhcong.com.vn", 587);
            //SmtpClient smtpMail = new SmtpClient("mail.thanhthanhcong.com.vn", 587);
            //smtpMail.UseDefaultCredentials = true;
            //smtpMail.EnableSsl = true;
            //smtpMail.Credentials = new System.Net.NetworkCredential("cs.jgs@thanhthanhcong.com.vn", "LLqsx3NGf5");
            // smtp.Port = 587;
            //Or your Smtp Email ID and Password
            //smtpMail.Send(mail);
            try
            {
                smtpMail.Send(objMail);
                //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                //    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                //    System.Security.Cryptography.X509Certificates.X509Chain chain,
                //    System.Net.Security.SslPolicyErrors sslPolicyErrors)
                //{
                //    return true;
                //};
                //smtpMail.Send(objMail);
                Susscess++;
                Status = 1;
            }
            catch (Exception ex)
            {
                //DialogBox.Error("Message not emailed: " + ex.ToString());
                message = ex.Message;
                Error++;
                Status = 2;
            }

            //var smtpMail = new SmtpClient();
            //smtpMail.Host = "smtp.gmail.com";
            //smtpMail.Port = 587;
            //smtpMail.UseDefaultCredentials = false;
            //smtpMail.EnableSsl = true;
            //NetworkCredential nc = new NetworkCredential(From.Trim(), Pass.Trim());
            //smtpMail.Credentials = nc;


        }
    }
}
