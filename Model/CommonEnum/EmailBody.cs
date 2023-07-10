using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CommonEnum
{
    public static class EmailBody
    {
        public static string CheckIn = "Dear User,<br/><br/> Truck Park Update.     <br/><br/>Truck Number ([TruckNo])  for your enterprise ([CompanyShortName]) has checked IN at Truck Park on [ActualArrivalDate] <br/><br/><br/>Thank You,<br/>LFZ Truck Park Portal <br/> <a href='https://oneportal.lagosfreezone.com/'>https://oneportal.lagosfreezone.com/</a> ";
        public static string CheckOut = "Dear User,<br/><br/> Truck Park Update.     <br/><br/>Truck Number ([TruckNo])  for your enterprise ([CompanyShortName]) has checked OUT at Truck Park on [ActualArrivalDate] <br/><br/><br/>Thank You,<br/>LFZ Truck Park Portal <br/> <a href='https://oneportal.lagosfreezone.com/'>https://oneportal.lagosfreezone.com/</a>";
        //public static string Added = "Dear User,<br/><br/> Truck Park Update.     <br/><br/>Truck Number ([TruckNo])  of  enterprise ([CompanyShortName]) is Added at Truck Park on [ActualArrivalDate] <br/><br/><br/>Thank You,<br/>LFZ Truck Park Portal <br/> <a href='https://oneportal.lagosfreezone.com/'>https://oneportal.lagosfreezone.com/</a>";
    }

    public static class EmailSubject
    {
        public static string CheckIn = "Check In Update for truck number [TruckNo]";
        public static string CheckOut = "Check Out Update for truck number [TruckNo]";
        //public static string Added = "New Truck Details Added [TruckNo]";
    }
}
