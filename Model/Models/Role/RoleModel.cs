using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Role
{
    public class RoleModel
    {
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role name is required")]
        public string RoleName { get; set; }

       // public string RoleModuleIds { get; set; }

        //public int Createdby { get; set; }

        //public System.DateTime Createddate { get; set; }

        //public Nullable<int> Modifiedby { get; set; }

        //public Nullable<System.DateTime> Modifieddate { get; set; }

        //public bool IsActive { get; set; }
    }
}
