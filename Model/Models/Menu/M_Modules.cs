using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Menu
{
    public class M_Modules
    {
        public int Id { get; set; }

        public string ModuleName { get; set; }

        public int Parentid { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public int DisplayOrder { get; set; }

        public string IconImage { get; set; }

        public bool IsActive { get; set; }

        public string RoleIds { get; set; }

        //public int OrganizationId { get; set; }
    }
}
