using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class EditTruckDetails
    {
        public long TruckDetailsId { get; set; }
        public long TruckId { get; set; }
        public int CalledByOrganizationId { get; set; }
        public int TruckCapacityId { get; set; }
        public Nullable<System.DateTime> ExpectedArrivalDate { get; set; }
        public Nullable<System.DateTime> ExpectedDepatureDate { get; set; }
        public int LocalTransferTypeId { get; set; }
        public string LocalTransferType { get; set; }
        public string TransportName { get; set; }
        public string TransportNo { get; set; }
        public string DriverName { get; set; }
        public string DriverNo { get; set; }
        public Nullable<int> MaterialTypeId { get; set; }
        public string MaterialType { get; set; }
        public string MaterialGoods { get; set; }
        public Nullable<System.DateTime> ActualArrivalDate { get; set; }
        public Nullable<System.DateTime> ActualDepatureDate { get; set; }
        public bool IsForecasted { get; set; }
        public bool IsCheckedIn { get; set; }
        public bool IsCalledOut { get; set; }
        public int Createdby { get; set; }
        public bool IsActive { get; set; }
        public string TruckNo { get; set; }
        public string TruckCapacity { get; set; }
        public string OwnedByOrganization { get; set; }
        public bool IsBilled { get; set; }
        public int RoleId { get; set; }
    }
}
