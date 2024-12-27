using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.UserDetail
{
    public class UserDetailSaveModel
    {

        public int UDID { get; set; }

      //  public int OrganizationID { get; set; }

        [Required(ErrorMessage = "Enter Your Name")]
        [StringLength(4, ErrorMessage = "Name should be less than or equal to four characters.")]
        public string EmployeeName { get; set; }

        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }

        //  public int RoleId { get; set; }
        [Required(ErrorMessage = "Please enter the Department")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Please enter Reporting To")]
        public int ReportingToID { get; set; }

        [Required(ErrorMessage = "Enter Your EmailID")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please Enter the Mobile Number")]
        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string MobileNo { get; set; }

        public int RoleId { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsActive { get; set; }

    }
}
