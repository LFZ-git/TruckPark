using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using DAL.Interface;
using Model.Models;
using Model.Models.Invoice;

namespace BAL.Concreate
{
    public class InvoiceBAL: IInvoiceBAL
    {
        private IInvoiceDAL _iInvoiceDAL;
        public InvoiceBAL()
        {
            _iInvoiceDAL = BALFactory.GetInvoiceInstance();
        }

        public ResponseInfo AddInvoice(AddProformaInvoice model)
        {
            return _iInvoiceDAL.AddInvoice(model);
        }
        public InvoiceDetailsAPIModel GetInvoiceDetailsBAL(InvoiceDetailsAPIModel model)
        {
            return _iInvoiceDAL.GetInvoiceDetailsDAL(model);
        }

        public List<Model.Models.ProformaInvoice> GetInvoiceList(int RoleId, int UDID)
        {
            return _iInvoiceDAL.GetInvoiceList(RoleId, UDID);
        }

        public Model.Models.ProformaInvoiceDetails GetInvoiceDetails(int Id)
        {
            return _iInvoiceDAL.GetInvoiceDetails(Id);
        }

        public ResponseInfo AddInvoicePdf(Model.Models.ProformaInvoice model)
        {
            return _iInvoiceDAL.AddInvoicePdf(model);
        }

        public List<Model.Models.ProformaInvoiceTruckDetails> GetInvoiceTruckDetails(int id)
        {
            return _iInvoiceDAL.GetInvoiceTruckDetails(id);
        }
    }
}
