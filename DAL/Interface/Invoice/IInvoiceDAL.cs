using Model.Models;
using Model.Models.Invoice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IInvoiceDAL
    {
        ResponseInfo AddInvoice(AddProformaInvoice model);
        InvoiceDetailsAPIModel GetInvoiceDetailsDAL(InvoiceDetailsAPIModel model);

        List<Model.Models.ProformaInvoice> GetInvoiceList(int RoleId, int UDID);

        Model.Models.ProformaInvoiceDetails GetInvoiceDetails(int Id);

        ResponseInfo AddInvoicePdf(Model.Models.ProformaInvoice model);

        List<Model.Models.ProformaInvoiceTruckDetails> GetInvoiceTruckDetails(int id);
    }
}
