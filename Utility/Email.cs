using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class Email
    {
        public async void SendErrorMsg(string errorMsg, string action, string controller)
        {
            //Read from and fromname from config
            const String FROM = "sender@example.com";
            const String FROMNAME = "Sender Name";

            //Read to from config
            const String TO = "recipient@example.com";

            // Read from config
            const String SMTP_USERNAME = "smtp_username";

            // Read from config
            const String SMTP_PASSWORD = "smtp_password";

            // (Optional) the name of a configuration set to use for this message.
            // If you comment out this line, you also need to remove or comment out
            // the "X-SES-CONFIGURATION-SET" header below.
            // const String CONFIGSET = "ConfigSet";

            // If you're using in a region other than US West (Oregon), 
            // replace with the SES SMTP  
            // endpoint in the appropriate Region.
            const String HOST = "smtpout.secureserver.net";

            // The port you will connect to on the SES SMTP endpoint. We
            // are choosing port 587 because we will use STARTTLS to encrypt
            // the connection.
            const int PORT = 80;

            // The subject line of the email
            const String SUBJECT =
                " test (SMTP interface accessed using C#)";

            // The body of the email
            const String BODY =
                "<h1> Test</h1>" +
                "<p>This email was sent through the " +
                "<a href=''> SES</a> SMTP interface " +
                "using the .NET System.Net.Mail library.</p>";

            // Create and build a new MailMessage object
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM, FROMNAME);
            message.To.Add(new MailAddress(TO));
            message.Subject = SUBJECT;
            message.Body = BODY;
            // Comment or delete the next line if you are not using a configuration set
            //  message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

            // Create and configure a new SmtpClient
            SmtpClient client =
                new SmtpClient(HOST, PORT);
            // Pass SMTP credentials
            client.Credentials =
                new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
            // Enable SSL encryption
            client.EnableSsl = true;

            // Send the email. 
            try
            {
                client.Send(message);
            }
            catch (Exception)
            {
            }
        }

        public bool SendMail(string to, string subject, string body, [Optional] string Cc, [Optional] string Bcc)
        {
            bool f = false;
            try
            {
                //Reading info from config file
                string hostname = ConfigurationManager.AppSettings["MailHostName"];
                string UserName = ConfigurationManager.AppSettings["MailUserName"];
                string MailPassword = ConfigurationManager.AppSettings["MailPassword"];
                string SenderEmail = ConfigurationManager.AppSettings["SenderEmail"];
                int SenderPort = Convert.ToInt32(ConfigurationManager.AppSettings["SenderPort"]);

                int SendMailForLocal = Convert.ToInt32(ConfigurationManager.AppSettings["SendMailForLocal"]);

                if (SendMailForLocal == 1)
                {
                    to = ConfigurationManager.AppSettings["MailIdForLocal"];
                    if (Cc != null)
                        Cc = ConfigurationManager.AppSettings["MailIdForLocal"];
                    if (Bcc != null)
                        Bcc = ConfigurationManager.AppSettings["MailIdForLocal"];

                }



                //Code for Mail
                MailMessage mail = new MailMessage();
                mail.To.Add(to);
                mail.From = new MailAddress(SenderEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                if (Cc != null)
                    mail.CC.Add(Cc);
                if (Bcc != null)
                    mail.Bcc.Add(Bcc);
                //else
                //{
                //    mail.Bcc.Add(BccEmail);
                //}

                //Code for SMTP
                SmtpClient smtp = new SmtpClient();
                smtp.Host = hostname;
                smtp.Port = SenderPort;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(UserName, MailPassword); // Enter seders User name and password   
                smtp.EnableSsl = true;

                
                smtp.Send(mail);

                f = true;

            }
            catch (Exception ex)
            {
                f = false;
            }
            return f;
        }
    }
}
