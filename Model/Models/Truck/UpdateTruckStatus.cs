using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class UpdateTruckStatus
    {
        public string TruckDetailsList { get; set; }
        public int ModifiedBy { get; set; }
        public int RoleId { get; set; }
        public int status { get; set; }
    }
}
