using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedular.Helper
{
    public class EmailBodyHelper
    {
        public static string GetAdminMailBody(DataTable dt)
        {
            string emailBody = string.Empty;
            try
            {
                string messageBody = "<font>Please find the below last 12 hours activity of trucks at LFZ Truck Park: </font><br><br>";
                if (dt.Rows.Count == 0) return messageBody;
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdStartColSpan2 = "<td colspan=\"2\" style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdStartColSpan3 = "<td colspan=\"3\" style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdStartRowSpan = "<td  rowspan=\"2\" style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStartRowSpan + "Srno" + htmlTdEnd;
                messageBody += htmlTdStartRowSpan + "Company Name" + htmlTdEnd;
                messageBody += htmlTdStartColSpan2 + "Movement of Trucks (Last 12 Hours)" + htmlTdEnd;
                messageBody += htmlTdStartColSpan3 + "Status of Truck Parked" + htmlTdEnd;
                messageBody += htmlTdStartColSpan3 + "Import/Export" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Check In" + htmlTdEnd;
                messageBody += htmlTdStart + "Check Out" + htmlTdEnd;
                messageBody += htmlTdStart + "Total No. Of Trucks" + htmlTdEnd;
                messageBody += htmlTdStart + "Parks For More Than 5 Days" + htmlTdEnd;
                messageBody += htmlTdStart + "Trucks Parked Today" + htmlTdEnd;
                messageBody += htmlTdStart + "Import" + htmlTdEnd;
                messageBody += htmlTdStart + "Export" + htmlTdEnd;
                messageBody += htmlTdStart + "Both" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                //Loop all the rows from grid vew and added to html td  
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    int j = i + 1;

                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + j.ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + dt.Rows[i]["OrgShortName"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + dt.Rows[i]["CheckInCount"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + dt.Rows[i]["CheckOutCount"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + dt.Rows[i]["TotalTrucks"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + dt.Rows[i]["FiveDaysParkedTrucks"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + dt.Rows[i]["TodayTrucks"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + dt.Rows[i]["import"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + dt.Rows[i]["export"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + dt.Rows[i]["both"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd;
                messageBody = messageBody + "<br><br><font>Regards,</font><br><font>LFZ Support Team</font><br><br><br><br><font>This is system generated report mailer. Do not reply on this</font>";
                emailBody = messageBody;
            }
            catch (Exception ex)
            {

            }
            return emailBody;
        }

        public static string GetEnterpriseMailBody(DataSet dt)
        {
            string emailBody = string.Empty;
            try
            {
                string messageBody = "<font>Please find the below last 12 hours activity of your Enterprise Truck moments at LFZ Truck Park: </font><br><br>";

                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdStartColSpan2 = "<td colspan=\"2\" style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdStartColSpan3 = "<td colspan=\"3\" style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdStartRowSpan = "<td  rowspan=\"2\" style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStartRowSpan + "Company Name" + htmlTdEnd;
                messageBody += htmlTdStartColSpan2 + "Movement of Trucks (Last 12 Hours)" + htmlTdEnd;
                messageBody += htmlTdStartColSpan3 + "Status of Truck Parked" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                messageBody += htmlHeaderRowStart;
                //messageBody += htmlTdStart + "Company Name" + htmlTdEnd;
                messageBody += htmlTdStart + "Check In" + htmlTdEnd;
                messageBody += htmlTdStart + "Check Out" + htmlTdEnd;
                messageBody += htmlTdStart + "Total No. Of Trucks" + htmlTdEnd;
                messageBody += htmlTdStart + "Parks For More Than 5 Days" + htmlTdEnd;
                messageBody += htmlTdStart + "Trucks Parked Today" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                //Loop all the rows from grid vew and added to html td  
                for (int i = 0; i <= dt.Tables[0].Rows.Count - 1; i++)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + dt.Tables[0].Rows[i]["OrgShortName"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + dt.Tables[0].Rows[i]["CheckInCount"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + dt.Tables[0].Rows[i]["CheckOutCount"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + dt.Tables[0].Rows[i]["TotalTrucks"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + dt.Tables[0].Rows[i]["FiveDaysParkedTrucks"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + dt.Tables[0].Rows[i]["TodayTrucks"].ToString() + htmlTdEnd;
                    messageBody = messageBody + htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd;
                emailBody += messageBody;

                if (dt.Tables[1].Rows.Count > 0)
                {
                    string messageBody_2 = "<br/><br/><font>The following are the records: </font><br><br>";

                    string htmlTableStart_2 = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                    string htmlTableEnd_2 = "</table>";
                    string htmlHeaderRowStart_2 = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
                    string htmlHeaderRowEnd_2 = "</tr>";
                    string htmlTrStart_2 = "<tr style=\"color:#555555;\">";
                    string htmlTrEnd_2 = "</tr>";
                    string htmlTdStart_2 = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                    string htmlTdEnd_2 = "</td>";
                    messageBody_2 += htmlTableStart_2;
                    messageBody_2 += htmlHeaderRowStart_2;
                    messageBody_2 += htmlTdStart_2 + "Truck No" + htmlTdEnd_2;
                    messageBody_2 += htmlTdStart_2 + "Actual Arrival Date" + htmlTdEnd_2;
                    messageBody_2 += htmlTdStart_2 + "Driver Name" + htmlTdEnd_2;
                    messageBody_2 += htmlTdStart_2 + "Driver No" + htmlTdEnd_2;

                    messageBody_2 += htmlHeaderRowEnd_2;
                    //Loop all the rows from grid vew and added to html td  
                    for (int i = 0; i <= dt.Tables[1].Rows.Count - 1; i++)
                    {
                        messageBody_2 = messageBody_2 + htmlTrStart_2;
                        messageBody_2 = messageBody_2 + htmlTdStart_2 + dt.Tables[1].Rows[i]["TruckNo"].ToString() + htmlTdEnd_2;
                        messageBody_2 = messageBody_2 + htmlTdStart_2 + dt.Tables[1].Rows[i]["ActualArrivalDate"].ToString() + htmlTdEnd_2;
                        messageBody_2 = messageBody_2 + htmlTdStart_2 + dt.Tables[1].Rows[i]["DriverName"].ToString() + htmlTdEnd_2;
                        messageBody_2 = messageBody_2 + htmlTdStart_2 + dt.Tables[1].Rows[i]["DriverNo"].ToString() + htmlTdEnd_2;

                        messageBody_2 = messageBody_2 + htmlTrEnd_2;
                    }
                    messageBody_2 = messageBody_2 + htmlTableEnd_2;
                    emailBody += messageBody_2;

                }
                emailBody = emailBody + "<br><br><font>Please highlight any discrepancies within 24 hours or will be considered as deemed acceptance. </font><br><font>Regards,</font><br><font>LFZ Support Team</font><br><br><br><br><font>This is system generated report mailer. Do not reply on this</font>";
            }
            catch (Exception ex)
            {

            }
            return emailBody;
        }
    }
}
