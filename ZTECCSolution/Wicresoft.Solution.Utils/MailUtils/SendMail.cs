
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.Solution.Utils
{
    public class SendMail : IDisposable
    {
        SmtpClient client = new SmtpClient();

        public bool EnableSSL
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public NetworkCredential Certificates
        {
            get
            {
                NetworkCredential credetial = new NetworkCredential(UserName, Password);
                return credetial;
            }
        }
        public string From
        {
            get;
            set;
        }
        private string replyto;
        public string ReplyTo
        {
            get
            {
                return string.IsNullOrEmpty(replyto) ? From : replyto;
            }
            set
            {
                replyto = value;
            }
        }
        public string Subject
        {
            get;
            set;
        }
        public string Body
        {
            get;
            set;
        }

        public List<string> AttachmentPaths { get; set; }

        public string AttachmentPath { get; set; }

        public SendMail(string mailServer, int port, bool enableSSL, string userName, string pwd)
        {
            EnableSSL = enableSSL;
            UserName = userName;
            Password = pwd;

            InitSmtpClient(mailServer, port, Certificates);

        }
        public SendMail(string mailServer, int port)
        {
            client.Host = mailServer;
            client.Port = port;
        }

        /// <summary>
        /// Init smtp server by sharepoint
        /// </summary>
        /// <param name="host"></param>
        /// <param name="c"></param>
        protected void InitSmtpClient(string host, int port, ICredentialsByHost c)
        {
            client.Host = host;
            if (port > 0)
            {
                client.Port = port;
            }
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = EnableSSL;
            client.UseDefaultCredentials = false;
            client.Credentials = c;
        }

        public void Send(string to, string cc, string subject, string body)
        {
            Subject = subject;
            Body = body;
            List<string> toList = new List<string>();
            List<string> ccList = new List<string>();
            toList.Add(to);
            ccList.Add(cc);

            Send(toList, ccList);
        }
        public void Send(List<string> toList, List<string> ccList)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(From);
            foreach (string str in toList)
            {
                string str2 = this.ReplaceString(str);
                if (!string.IsNullOrEmpty(str2) && !message.To.Contains(new MailAddress(str2)))
                {
                    message.To.Add(str2);
                }
            }

            if (!string.IsNullOrEmpty(From) || !string.IsNullOrEmpty(ReplyTo))
                message.ReplyToList.Add(ReplyTo);
            foreach (string str3 in ccList)
            {
                string str2 = this.ReplaceString(str3);
                if (!string.IsNullOrEmpty(str2) && !message.CC.Contains(new MailAddress(str2)))
                {
                    message.CC.Add(this.Conver2MailAddress(str2));
                }
            }
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["MailBcc"]) && MailTest)
            {
                List<string> bccList = ConfigurationManager.AppSettings["MailBcc"].Split(';').ToList();
                foreach (string str4 in bccList)
                {
                    string str2 = this.ReplaceString(str4);
                    if (!string.IsNullOrEmpty(str2) && !message.Bcc.Contains(new MailAddress(str2)))
                    {
                        message.Bcc.Add(this.Conver2MailAddress(str2));
                    }
                }
            }

            message.IsBodyHtml = true;
            message.Subject = Subject;
            message.Body = Body;

            if (!String.IsNullOrWhiteSpace(AttachmentPath))
            {
                message.Attachments.Add(new Attachment(AttachmentPath));
            }
            if (AttachmentPaths != null && AttachmentPaths.Count() > 0)
            {
                foreach (string attachment in AttachmentPaths)
                {
                    message.Attachments.Add(new Attachment(attachment));
                }
            }

            if (this.MailToLog)
            {
                Log.Info(string.Format("\r\n TO:{0} \r\n CC:{1} \r\n Subject:{2}\r\n Body:{3} \r\n", message.To, message.CC, message.Subject, message.Body));
            }
            else if (!string.IsNullOrEmpty(this.client.Host))
            {
                this.client.Send(message);
            }
        }

        public void Send(List<string> toList, string cc)
        {
            List<string> ccList = new List<string>();
            toList.Add(cc);
            Send(toList, ccList);
        }

        public void Send(string to, List<string> ccList)
        {
            List<string> toList = new List<string>();
            toList.Add(to);
            Send(toList, ccList);
        }

        public void Send(string to, string cc)
        {
            List<string> toList = new List<string>();
            List<string> ccList = new List<string>();
            toList.Add(to);
            ccList.Add(cc);
            Send(toList, ccList);
        }

        public MailAddress Conver2MailAddress(string mailList)
        {
            return new MailAddress(mailList);

        }

        private string ReplaceString(string mail)
        {
            foreach (string str in this.ReplaceMail)
            {
                if (mail.ToLower().Trim() == str.Trim())
                {
                    return this.ReplaceToMail;
                }
            }
            return mail;
        }

        protected string[] ReplaceMail
        {
            get
            {
                string str = ConfigurationManager.AppSettings["ReplaceMail"];
                if (string.IsNullOrEmpty(str))
                {
                    return new string[0];
                }
                return str.ToLower().Split(new char[] { ';' });
            }
        }

        protected string ReplaceToMail
        {
            get
            {
                string str = ConfigurationManager.AppSettings["ReplaceToMail"];
                if (string.IsNullOrEmpty(str))
                {
                    return "";
                }
                return str.ToLower();
            }
        }
        protected bool MailToLog
        {
            get
            {
                string str = ConfigurationManager.AppSettings["MailToLog"];
                if (string.IsNullOrEmpty(str))
                {
                    return false;
                }
                return (str.ToLower() == "on");
            }
        }


        protected static bool MailTest
        {
            get
            {
                string status = System.Configuration.ConfigurationManager.AppSettings["MailTest"];
                if (!string.IsNullOrEmpty(status) && status.ToLower() == "on")
                    return true;
                else
                    return false;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.Collect();
        }

        #endregion
    }
}
