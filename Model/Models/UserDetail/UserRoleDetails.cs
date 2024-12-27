using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.UserDetail
{
    public class UserRoleDetails
    {
        public int UDID { get; set; }

        //public int OrganizationID { get; set; }

        public string EmployeeName { get; set; }

        public string EmailId { get; set; }

        public string MobileNo { get; set; }

        public int DepartmentID { get; set; }

        public int ReportingToID { get; set; }

        public string Password { get; set; }

        //public int InitialPasswordReset { get; set; }

        public int RoleId { get; set; }

        public string Role { get; set; }

        public string RoleIds { get; set; }

        //public int DesginationId { get; set; }

        //public int HeadquarterId { get; set; }

        //public int CityId { get; set; }

        //public int ZoneId { get; set; }
    }
}
