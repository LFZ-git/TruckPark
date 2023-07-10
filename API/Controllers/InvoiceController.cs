using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BAL.Interface;
using Model.Models;
using Model.Models.Invoice;

namespace API.Controllers
{
    public class InvoiceController : ApiController
    {
        private IInvoiceBAL _iInvoiceBAL;
        public InvoiceController()
        {
            _iInvoiceBAL = ServiceFactory.GetInvoiceInstance();
        }

        [HttpPost]
        public IHttpActionResult GenerateProformaInvoice(AddProformaInvoice model)
        {
            return Ok(_iInvoiceBAL.AddInvoice(model));
        }

        [HttpPost]
        public IHttpActionResult GetInvoiceDetails(InvoiceDetailsAPIModel model)
        {
            return Ok(_iInvoiceBAL.GetInvoiceDetailsBAL(model));
        }

        [HttpGet]
        public IHttpActionResult GetInvoiceList(int RoleID, int UserID)
        {
            return Ok(_iInvoiceBAL.GetInvoiceList(RoleID, UserID));
        }

        [HttpGet]
        public IHttpActionResult GetInvoiceDetails(int Id)
        {
            return Ok(_iInvoiceBAL.GetInvoiceDetails(Id));
        }

        [HttpPost]
        public IHttpActionResult AddInvoicePdf(ProformaInvoice model)
        {
            return Ok(_iInvoiceBAL.AddInvoicePdf(model));
        }

        [HttpGet]
        public IHttpActionResult GetProformaInvoiceTruckDetails(int Id)
        {
            return Ok(_iInvoiceBAL.GetInvoiceTruckDetails(Id));
        }
    }
}