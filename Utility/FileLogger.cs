using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace Utility
{
    public static class FileLogger
    {
        public static void Log(string logText)
        {
            try
            {
                string logPath = ConfigurationManager.AppSettings["ExtErrorLog"] + DateTime.Now.ToString("dd-MM-yyyy") + "_log.txt";

                using (StreamWriter sw = File.AppendText(logPath))
                {
                    logText = "Date: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "\n" + logText;

                    Debug.WriteLine(logText);
                    sw.WriteLine(logText);
                    sw.WriteLine();

                    sw.Close();
                }
            }
            catch (Exception) { }
        }

        public static void LogException(Exception ex)
        {
            try
            {
                string logPath = ConfigurationManager.AppSettings["ExtErrorLog"] + DateTime.Now.ToString("dd-MM-yyyy") + "_exception_log.txt";

                using (StreamWriter sw = File.AppendText(logPath))
                {
                    string logText = "Date: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "\n" + ExceptionUtility.ToJson(ex);

                    Debug.WriteLine(logText);
                    sw.WriteLine(logText);
                    sw.WriteLine();

                    sw.Close();
                }
            }
            catch (Exception) { }
        }
/*
        public static void LogEmailException(Exception ex, HashSet<string> toList, string subject, HashSet<string> ccList = null, HashSet<string> bccList = null)
        {
            try
            {
                string logPath = ConfigurationManager.AppSettings["LogPath"] + DateTime.Now.ToString("dd-MM-yyyy") + "_email_exception_log.txt";

                using (StreamWriter sw = File.AppendText(logPath))
                {
                    string logText = "";

                    logText += "Date: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    logText += "\nTo: " + Newtonsoft.Json.Encode(toList);
                    logText += ccList != null ? "\nCC: " + Json.Encode(ccList) : "" + bccList != null ? "\nBCC: " + Json.Encode(bccList) : "";
                    logText += "\nSubject: " + subject;
                    logText += "\nError:\n" + ExceptionUtility.ToJson(ex);

                    Debug.WriteLine(logText);
                    sw.WriteLine(logText);
                    sw.WriteLine();

                    sw.Close();
                }
            }
            catch (Exception) { }
        }*/
    }
}