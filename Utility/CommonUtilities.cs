using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.UI;

namespace Utility
{
    public static class CommonUtilities
    {

        // public const string SMSAPI_Success = "SUCCESS";


        public static DateTime DateNow(string Timezone)
        {
            // TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, CommonUtilities.INDIAN_ZONE)
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime custDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
            return custDateTime;
        }

        public static bool SendMail(string to, string subject, string body, [Optional]  string Cc, [Optional]  string Bcc)
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
                }

                //string SenderEmail = "info@test-zaksof.in";

                //string BccEmail = "ansari@pharmasquire.in,support@zaksof.in,rucha@pharmasquire.in,zak4e1@gmai.com";

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

        [Obsolete]
        public static bool SendPDFEmail(string to, string subject, StringBuilder sb, [Optional]  string Cc, [Optional]  string Bcc)
        {
            bool f = false;
            try
            {
                string hostname = ConfigurationManager.AppSettings["MailHostName"];
                string UserName = ConfigurationManager.AppSettings["MailUserName"];
                string MailPassword = ConfigurationManager.AppSettings["MailPassword"];
                string SenderEmail = ConfigurationManager.AppSettings["SenderEmail"];
                int SenderPort = Convert.ToInt32(ConfigurationManager.AppSettings["SenderPort"]);

                int SendMailForLocal = Convert.ToInt32(ConfigurationManager.AppSettings["SendMailForLocal"]);

                if (SendMailForLocal == 1)
                {
                    to = ConfigurationManager.AppSettings["MailIdForLocal"];
                }

                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        StringReader sr = new StringReader(sb.ToString());

                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                            pdfDoc.Open();
                            htmlparser.Parse(sr);
                            pdfDoc.Close();
                            byte[] bytes = memoryStream.ToArray();
                            memoryStream.Close();

                            MailMessage mm = new MailMessage();
                            mm.To.Add(to);
                            mm.From = new MailAddress(SenderEmail);
                            mm.Subject = subject;
                            mm.Body = "Investor Profile";
                            mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "InvestorProfile.pdf"));
                            mm.IsBodyHtml = true;

                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = hostname;
                            smtp.Port = 587;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential(UserName, MailPassword); // Enter seders User name and password   
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.EnableSsl = true;
                            smtp.Send(mm);
                            f = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                f = false;
            }
            return f;
        }

    }
}
