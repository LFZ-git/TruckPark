using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class AddProformaInvoice
    {
        public string InvoiceNo { get; set; }
        public int OrganizationId { get; set; }
        public int? TotalTruckCount { get; set; }
        public string TruckIdList { get; set; }
        public int createdBy { get; set; }
    }
}
