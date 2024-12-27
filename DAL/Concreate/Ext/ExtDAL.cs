using DAL.Interface.Ext;
using Model.Models;
using Model.Models.Ext;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concreate.Ext
{
    public class ExtDAL : BaseClassDAL, IExtDAL
    {
        LFZ_TruckParkEntities entities = new LFZ_TruckParkEntities();
        public ResponseInfoAPI CheckApiKey(string apiKey)
        {
            var result = (from key in entities.M_APIKey where key.APIKey == apiKey 
                          select new {  APIKeyId = key.APIKeyId
                                      , APIKey = key.APIKey
                                      , PortalName = key.PortalName
                                      , IsActive = key.IsActive }).FirstOrDefault();

            return Mapping<ResponseInfoAPI>(result);
        }

        public void ReceivedLog(ResponseInfoAPI model)
        {
            entities.API_ReceivedLog(model.APIKeyId, model.PayLoad, model.SourceIP);
        }

        public void SendLog(ResponseInfoAPI model)
        {
            entities.API_SentLog(model.PayLoad, model.SourceIP);
        }

        public ResponseInfo AddTruckData(EcMainModel model)
        {
            ObjectParameter outIsSuccess = new ObjectParameter("OutIsSuccess", typeof(bool));
            ObjectParameter outMssg = new ObjectParameter("OutMessage", typeof(string));

            model.EstimatedArrivalDate = model.EstimatedArrivalDate.Value.Add(model.EstimatedArrivalTime.TimeOfDay);

            entities.EcallUp_CU(model.Id, model.Company.Id, model.Company.Name, model.User.Id, model.Truck.Id, model.Driver.Id, model.Pregate.Id
                               , model.Pregate.Name, model.Park.Id, model.Park.Name, model.Park.Type, model.Terminal.Id, model.Terminal.Name
                               , model.Category.Id, model.Category.Name, model.Port.Id, model.Port.Name, model.Port.Type, model.StatusesHistory
                               , model.MaterialType, model.TransferType, model.Status, model.EstimatedArrivalDate, model.DepartureDate
                               , model.CreatedAt, model.UpdatedAt, outIsSuccess, outMssg);

            return new ResponseInfo() { IsSuccess = (bool)outIsSuccess.Value, Msg = outMssg.Value.ToString() };
        }
    }
}
