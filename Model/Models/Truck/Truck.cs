using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Truck
    {
        public long TruckId { get; set; }
        public string TruckNo { get; set; }
        public int? OwnedByOrganizationId { get; set; }
        public int? TruckCapacityId { get; set; }
        public bool IsActive { get; set; }
    }

    public class TruckStatus
    {
        public string TruckNo { get; set; }
        public int TruckCapacityId { get; set; }
        public int OwnedByOrganizationId { get; set; }
        public bool IsActive { get; set; }
        public bool IsForecasted { get; set; }
        public bool IsCheckedIn { get; set; }
    }
}
