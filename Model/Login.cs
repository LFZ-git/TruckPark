using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Login
    {

        public int Id { get; set; }

        [Display(Name ="Email ID")]
        [Required(ErrorMessage ="Email id required")]
        public string UserId { get; set; }

        [Display(Name ="Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
