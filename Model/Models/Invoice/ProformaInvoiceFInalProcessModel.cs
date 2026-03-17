using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Invoice
{
    public class ProformaInvoiceFInalProcessModel
    {
        public string TruckIdList { get; set; }
        public int ProformaInvoiceId { get; set; }
        public bool IsSuccess { get; set; }
        public int UpdatedById { get; set; }
    }
}
