using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class M_TruckCapacity
    {
        public int TruckCapacityId { get; set; }
        public string TruckCapacity { get; set; }
        public int Createdby { get; set; }
        public DateTime Createddate { get; set; }
        public int? Modifiedby { get; set; }
        public DateTime? Modifieddate { get; set; }
        public bool IsActive { get; set; }
    }
}
