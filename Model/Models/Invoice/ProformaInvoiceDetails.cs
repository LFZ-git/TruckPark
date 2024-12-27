using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ProformaInvoiceDetails
    {
        public long ProformaInvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationShortName { get; set; }
        public string OrgnaizationAddress { get; set; }
        public int? TotalTruckCount { get; set; }
        public decimal? TotalInvoiceAmount { get; set; }
        public DateTime Invoicedate { get; set; }
        public string InvoiceFilePath { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime Todate { get; set; }
        public decimal? TotalDiscount { get; set; }
    }

    public class ProformaInvoiceTruckDetails
    {
        public long ProformaInvoiceDetId { get; set; }
        public long ProformaInvoiceId { get; set; }
        public string TruckNo { get; set; }
        public int TruckCapacityId { get; set; }
        public DateTime CheckedInDate { get; set; }
        public DateTime CheckedOutDate { get; set; }
        public decimal InvoiceRate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public DateTime Invoicedate { get; set; }
        public decimal? Discount { get; set; }
    }
}
