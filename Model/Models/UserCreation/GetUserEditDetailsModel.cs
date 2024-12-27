using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.UserCreation
{
    public class GetUserEditDetailsModel
    {
        public int UDID { get; set; }

        //public int OrganizationID { get; set; }

        [Required(ErrorMessage = "Enter Employee Name")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "select Role")]
        public int RoleId { get; set; }
    
        [Required(ErrorMessage = "Select Department")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Select Reporting To")]
        public int ReportingToID { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please Add Password")]
        public string Password { get; set; }

       
        public string MobileNo { get; set; }

        public string saltKey { get; set; }

    
    }
}
