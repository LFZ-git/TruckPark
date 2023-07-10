using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Invoice
{
    public class InvoiceDetailsAPIModel
    {
        public int ProformaInvoiceId { get; set; }
        public string CustomerCode { get; set; }
        public string InvoiceAmount  { get; set; }
        public string InvoiceReference { get; set; }
     
    }
}
