using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class OrganizationModel
    {
        public short OrganizationID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyShortName { get; set; }
        public DateTime? OrganizationCreatedDate { get; set; }
        public bool? IsActive { get; set; }
        public short OrganizationTypeId { get; set; }
        public string OrganizationType { get; set; }
    }
}
