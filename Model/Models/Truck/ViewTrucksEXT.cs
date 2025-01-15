using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ViewTrucksEXT
    {
        public long TruckDetailsId { get; set; }
        public string GUID { get; set; }
        public Nullable<long> TruckId { get; set; }
        public string TruckGUID { get; set; }
        public string TruckNo { get; set; }
        public Nullable<int> CalledByOrganizationId { get; set; }
        public string CalledByOrgGUID { get; set; }
        public string OwnedByOrganization { get; set; }
        public Nullable<int> TruckCapacityId { get; set; }
        public string TruckCapacity { get; set; }
        public Nullable<System.DateTime> ExpectedArrivalDate { get; set; }
        public Nullable<System.DateTime> ExpectedDepatureDate { get; set; }
        public Nullable<int> LocalTransferTypeId { get; set; }
        public string TransferType { get; set; }
        public string TransportName { get; set; }
        public string TransportNo { get; set; }
        public string DriverName { get; set; }
        public string DriverNo { get; set; }
        public Nullable<int> MaterialTypeId { get; set; }
        public string MaterialType { get; set; }
        public string MaterialGoods { get; set; }
        public Nullable<System.DateTime> ActualArrivalDate { get; set; }
        public Nullable<System.DateTime> ActualDepatureDate { get; set; }
        public Nullable<bool> IsForecasted { get; set; }
        public Nullable<bool> IsCheckedIn { get; set; }
        public Nullable<bool> IsCalledOut { get; set; }
        public Nullable<bool> IsBilled { get; set; }
    }
}
