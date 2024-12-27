using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Model.Models.Home
{
    public class RegistrationModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Minium 5 chararcter Required and max 30")]
        [RegularExpression(@"^[a-zA-Z .]*$", ErrorMessage = "Use letters only please")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{6,9}$", ErrorMessage = "Password should have atleast one upper case, one lower case, one numeric, one special character in [!@#$%^&*] and min length is 6 and max 9.")]
        public string UserPwd { get; set; }

        [DataType(DataType.Password)]
        [Compare("UserPwd",ErrorMessage = "Password and Confirm Password do not match")]
        public string UserConfPwd { get; set; }

        public int RoleId { get; set; }

        public int PackageId { get; set; }

        [Required(ErrorMessage = "email id is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email_id { get; set; }

        [Required(ErrorMessage = "Mobile Number is Required!")]
        [RegularExpression(@"^[789]\d{9}$", ErrorMessage = "Wrong mobile Number")]
        public string UserMobile { get; set; }

        public DateTime ExpiryDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public bool IsActive { get; set; }
    }
}