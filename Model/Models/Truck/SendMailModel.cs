using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class SendMailModel
    {
        public string EmailId { get; set; }
        public string TruckNo { get; set; }
        public string CompanyShortName { get; set; }
        public DateTime ActualArrivalDate { get; set; }
        public DateTime ActualDepartureDate { get; set; }
        public string GUID { get; set; }
    }
}
