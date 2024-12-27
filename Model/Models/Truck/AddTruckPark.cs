using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class AddTruckPark
    {
        public int TruckId { get; set; } = 0;
        public string TruckNo { get; set; }
        public int OwnedByOrganizationId { get; set; }
        public int TruckCapacityId { get; set; }
        public int CalledByOrganizationId { get; set; }
        public DateTime? ExpectedArrivalDate { get; set; }
        public DateTime? ExpectedDepatureDate { get; set; }
        public int LocalTransferTypeId { get; set; }
        public string TransportName { get; set; }

        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string TransportNo { get; set; }
        public string DriverName { get; set; }
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string DriverNo { get; set; }
        public int? MaterialTypeId { get; set; }
        public string MaterialGoods { get; set; }
        public DateTime? ActualArrivalDate { get; set; }
        public DateTime? ActualDepatureDate { get; set; }
        public bool IsForecasted { get; set; }
        public bool IsCheckedIn { get; set; }
        public bool IsCalledOut { get; set; }
        public int Createdby { get; set; }
        public DateTime Createddate { get; set; }
        public int? Modifiedby { get; set; }
        public DateTime? Modifieddate { get; set; }
        public bool IsActive { get; set; }
    }
}
