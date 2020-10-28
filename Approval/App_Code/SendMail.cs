using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
namespace Approval
{
    public class SendMail
    {
        public string _from, _to, _subject, _body, _pass;
        public SendMail() { }
        public SendMail(string from, string pass, string to, string subject, string body)
        {
            this._to = to; this._from = from; this._pass = pass;
            this._subject = subject; this._body = body;
        }
        //public SendMail(string to, string pass, string subject, string body)
        //{
        //    this._to = to; this._pass = pass;
        //    this._subject = subject; this._body = body;
        //}
        public void SendM()
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_from);
                mail.To.Add(_to);
                mail.Subject = _subject;
                mail.Body = _body;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "mail.shared06.gdsvn.net";
                smtp.Port = 25;
                smtp.Credentials = new NetworkCredential(_from, _pass);
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                //throw;
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}