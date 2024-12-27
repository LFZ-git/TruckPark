using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using Model.Models;
using System.Data.SqlClient;
using Model.Models.Invoice;

namespace DAL.Concreate
{
    public class InvoiceDAL: BaseClassDAL, IInvoiceDAL
    {
        LFZ_TruckParkEntities entities = new LFZ_TruckParkEntities();

        public ResponseInfo AddInvoice(AddProformaInvoice model)
        {
            ResponseInfo respInfo = new ResponseInfo();
            System.Data.Entity.Core.Objects.ObjectParameter OutputParam = new System.Data.Entity.Core.Objects.ObjectParameter("OutParam", typeof(int));
            var result = entities.ProformaInvoice_C(model.createdBy, model.OrganizationId, model.TotalTruckCount, model.TruckIdList, OutputParam);

            respInfo.ID = Convert.ToInt32(OutputParam.Value);
            respInfo.Status = "";
            respInfo.IsSuccess = true;
            respInfo.Msg = OutputParam.Value.ToString();
            return respInfo;
        }
        public InvoiceDetailsAPIModel GetInvoiceDetailsDAL(InvoiceDetailsAPIModel model)
        {
            //InvoiceDetailsAPIModel model1 = new InvoiceDetailsAPIModel();
            //ResponseInfo respInfo = new ResponseInfo();
            var result = entities.InvoiceDetails_API_G(model.ProformaInvoiceId).FirstOrDefault();
            InvoiceDetailsAPIModel list = Mapping<InvoiceDetailsAPIModel>(result);
           // return list;
            //respInfo.ID = Convert.ToInt32(OutputParam.Value);
           // respInfo.Status = "";
           // respInfo.IsSuccess = true;
           // respInfo.Msg = OutputParam.Value.ToString();
            return list;
        }

        public List<Model.Models.ProformaInvoice> GetInvoiceList(int RoleId, int UDID)
        {
            var result = entities.ProformaInvoiceList_G(RoleId, UDID).ToList();
            List<Model.Models.ProformaInvoice> list = Mapping<List<Model.Models.ProformaInvoice>>(result);
            return list;
        }

        public Model.Models.ProformaInvoiceDetails GetInvoiceDetails(int Id)
        {
            var result = entities.ProformaInvoice_G(Id).FirstOrDefault();
            Model.Models.ProformaInvoiceDetails details = Mapping<Model.Models.ProformaInvoiceDetails>(result);
            return details;
        }

        public ResponseInfo AddInvoicePdf(Model.Models.ProformaInvoice model)
        {
            ResponseInfo respInfo = new ResponseInfo();
            System.Data.Entity.Core.Objects.ObjectParameter OutputParam = new System.Data.Entity.Core.Objects.ObjectParameter("OutError", typeof(string));
            var result = entities.ProfomaInvoice_U(Convert.ToInt32(model.ProformaInvoiceId), model.InvoiceFilePath);

            respInfo.Status = "";
            respInfo.IsSuccess = true;
            return respInfo;
        }

        public List<Model.Models.ProformaInvoiceTruckDetails> GetInvoiceTruckDetails(int id)
        {
            var result = entities.proformaInvoiceDetails_G(id).ToList();
            List<Model.Models.ProformaInvoiceTruckDetails> list = Mapping<List<Model.Models.ProformaInvoiceTruckDetails>>(result);
            return list;
        }
    }
}
