using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model.Models.UserDetail
{
    public class UserDetailModel
    {

        public int UDID { get; set; }

        public int OrganizationID { get; set; }

        public string EmployeeName { get; set; }
 
        public string EmailId { get; set; }

        public string MobileNo { get; set; }

        public int DepartmentID { get; set; }

        public int ReportingToID { get; set; }

        public string Password { get; set; }

        public bool InitialPasswordReset { get; set; }

        public int RoleId { get; set; }
        public string SaltKey { get; set; }
        //public string saltKey { get; set; }

        //public string Role { get; set; }

        //public int DesginationId { get; set; }

        //public int HeadquarterId { get; set; }

        //public int CityId { get; set; }

        //public int ZoneId { get; set; }



    }
}
