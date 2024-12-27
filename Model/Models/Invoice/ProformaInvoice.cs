using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ProformaInvoice
    {
        public long ProformaInvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public int? TotalTruckCount { get; set; }
        public decimal? TotalInvoiceAmount { get; set; }
        public DateTime Invoicedate { get; set; }
        public string InvoiceFilePath { get; set; }
    }
}
