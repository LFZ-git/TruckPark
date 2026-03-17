using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Invoice
{
    public class OrionReqModel
    {
        public string CustomerCode { get; set; }
        public string InvoiceAmount { get; set; }
        public string InvoiceReference { get; set; }
    }
}
