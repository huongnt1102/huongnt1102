using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

public class MailProviderCls
{
    #region Bien Can Dung
    public string SmtpServer { get; set; }
    public bool EnableSsl { get; set; }
    public string MailFrom { get; set; }
    public MailAddress MailAddressFrom { get; set; }
    public string PassWord { get; set; }
    public string MailTo { get; set; }
    public MailAddress MailAddressTo { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    #endregion

    #region Phuong thuc xu ly
    public void SendMail()
    {
        MailMessage objMail = new MailMessage(MailFrom, MailTo, Subject, Content);
        objMail.IsBodyHtml = true;
        objMail.BodyEncoding = System.Text.Encoding.UTF8;

        SmtpClient smtpMail = new SmtpClient(SmtpServer);
        smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpMail.EnableSsl = EnableSsl;
        smtpMail.Credentials = new NetworkCredential(MailFrom, PassWord);
        smtpMail.Send(objMail);
    }

    public void SendMailV2()
    {
        MailMessage objMail = new MailMessage(MailAddressFrom, MailAddressTo);
        objMail.Subject = Subject;
        objMail.Body = Content;
        objMail.IsBodyHtml = true;
        objMail.BodyEncoding = System.Text.Encoding.UTF8;

        SmtpClient smtpMail = new SmtpClient(SmtpServer);
        smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpMail.EnableSsl = EnableSsl;
        smtpMail.Credentials = new NetworkCredential(MailAddressFrom.Address, PassWord);
        smtpMail.Send(objMail);
    }

    public void SendMailAttachFile(string fileName)
    {
        MailMessage objMail = new MailMessage(MailFrom, MailTo, Subject, Content);
        objMail.IsBodyHtml = true;
        objMail.BodyEncoding = System.Text.Encoding.UTF8;

        Attachment attachFile = new Attachment(fileName);
        objMail.Attachments.Add(attachFile);

        SmtpClient smtpMail = new SmtpClient(SmtpServer);
        smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpMail.EnableSsl = EnableSsl;
        smtpMail.Credentials = new NetworkCredential(MailFrom, PassWord);
        smtpMail.Send(objMail);
    }

    public void SendMailAttachFile(System.IO.Stream stream, string name)
    {
        MailMessage objMail = new MailMessage(MailFrom, MailTo, Subject, Content);
        objMail.IsBodyHtml = true;
        objMail.BodyEncoding = System.Text.Encoding.UTF8;

        stream.Seek(0, System.IO.SeekOrigin.Begin);
        Attachment attachFile = new Attachment(stream, name, "application/pdf");  
        objMail.Attachments.Add(attachFile);

        SmtpClient smtpMail = new SmtpClient(SmtpServer);
        smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpMail.EnableSsl = EnableSsl;
        smtpMail.Credentials = new NetworkCredential(MailFrom, PassWord);
        smtpMail.Send(objMail);
    }

    public void SendMailAttachFileV2(System.IO.Stream stream, string name)
    {
        MailMessage objMail = new MailMessage(MailAddressFrom, MailAddressTo);
        objMail.Subject = Subject;
        objMail.Body = Content;
        objMail.IsBodyHtml = true;
        objMail.BodyEncoding = System.Text.Encoding.UTF8;

        stream.Seek(0, System.IO.SeekOrigin.Begin);
        Attachment attachFile = new Attachment(stream, name, "application/pdf");  
        objMail.Attachments.Add(attachFile);

        SmtpClient smtpMail = new SmtpClient(SmtpServer);
        smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpMail.EnableSsl = EnableSsl;
        smtpMail.Credentials = new NetworkCredential(MailAddressFrom.Address, PassWord);
        smtpMail.Send(objMail);
    }
    #endregion
}