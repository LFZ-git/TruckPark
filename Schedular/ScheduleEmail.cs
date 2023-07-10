using System.Collections.Generic;
using System.Data;
using Schedular.Helper;
using DAL;
using System.Data.SqlClient;
using System.Web.Configuration;
using Utility;

namespace Schedular
{
    public class ScheduleEmail
    {
        static void Main(string[] args)
        {
            DataSet MailReceiver = SqlManager.ExecuteDataSet("usp_AdminEnterpiseDetails_G");
            if (MailReceiver != null || MailReceiver.Tables.Count > 0)
            {
                SendMailToAdmin(MailReceiver.Tables[0]);
                SendMailToEnterprise(MailReceiver.Tables[1]);
            }
        }


        private static void SendMailToAdmin(DataTable dt)
        {
            Email email = new Email();
            string emailSubject = WebConfigurationManager.AppSettings["AdminEmailSubject"];
            //string receiver = dt.Rows[0]["EmailId"].ToString();
            string receiver = WebConfigurationManager.AppSettings["LFZHrEmail"];
            DataSet data = SqlManager.ExecuteDataSet("Scheduler_Admin");
            if (data != null)
            {
                if (data.Tables.Count > 0)
                {
                    string emailBody = EmailBodyHelper.GetAdminMailBody(data.Tables[0]);
                    email.SendMail(receiver, emailSubject, emailBody);
                }
            }
        }

        private static void SendMailToEnterprise(DataTable dt)
        {
            Email email = new Email();
            string emailSubject = WebConfigurationManager.AppSettings["EnterpriseEmailSubject"];
            if (dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    string receiver1 = dr["EmailId"].ToString();
                    List<SqlParameter> parameters = new List<SqlParameter>();

                    parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@OrganisationId",
                        SqlDbType = SqlDbType.VarChar,
                        Value = dr["OrganizationID"].ToString(),
                        Direction = System.Data.ParameterDirection.Input


                    });
                    DataSet data = SqlManager.ExecuteDataSet("Scheduler_Enterprise", parameters.ToArray());

                    if(data!=null)
                    {
                        if (data.Tables.Count > 0)
                        {
                            string emailBody = EmailBodyHelper.GetEnterpriseMailBody(data);
                            email.SendMail(receiver1, emailSubject, emailBody);
                        }
                    }
                }
            }
        }

    }
}
