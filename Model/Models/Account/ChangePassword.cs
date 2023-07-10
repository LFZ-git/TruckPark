using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Account
{
    public class ChangePassword
    {
        public int UDID { get; set; }

        [Display(Name = "Old Password")]
        [Required(ErrorMessage = "Old password required.")]
        public string OldPassword { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password required.")]
        [CompareAttribute("Password", ErrorMessage = "Confirm Password and Password doesn't match.")]
        public string ConfirmPassword { get; set; }

        public string SaltKey { get; set; }
    }
}
