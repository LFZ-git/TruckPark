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

        public ResponseInfo AddTruckParkData(EcMainModel model)
        {
            ObjectParameter outIsSuccess = new ObjectParameter("OutIsSuccess", typeof(bool));
            ObjectParameter outMssg = new ObjectParameter("OutMssg", typeof(string));
            ObjectParameter outId = new ObjectParameter("OutId", typeof(long));

            model.EstimatedArrivalDate = model.EstimatedArrivalDate.Value.Add(model.EstimatedArrivalTime.TimeOfDay);

            IsTruckExists(model);

            entities.Truck_CRUD_API(model.Id, model.Truck.Id, model.Truck.PlateNumber, model.Company.Id, model.Truck.Capacity.Id, model.EstimatedArrivalDate
                                    , null, model.Category.Id, model.User.FullName, model.User.Phone, model.Driver.FirstName + " " + model.Driver.LastName
                                    , model.Driver.Phone, model.Material.Id, model.User.Id, model.Terminal.Id, outId, outMssg, outIsSuccess);


            return new ResponseInfo() 
            { 
                IsSuccess = (bool)outIsSuccess.Value
                , Msg = outMssg.Value.ToString()
                , LongID = (long)outId.Value };
        }

        ResponseInfo IsTruckExists(EcMainModel model)
        {
            ObjectParameter outMssg = new ObjectParameter("OutMssg", typeof(string));
            entities.IsTruckExists(model.Truck.PlateNumber, model.Truck.Id, model.Company.Id, model.Truck.Capacity.Id, model.User.Id, outMssg);

            return new ResponseInfo() { Msg = outMssg.Value.ToString() };
        }

        public TruckDetailAPI GetTruckDetails(long truckDetailId)
        {
            var result = (from t in entities.TruckDetails where t.TruckDetailsId == truckDetailId 
                          select new {
                              TruckDetailsId = t.TruckDetailsId
                            , TruckGUID = t.TruckGUID
                            , GUID  = t.GUID
                            , CalledByOrgGUID = t.CalledByOrgGUID
                            , TruckCapacity = t.TruckCapacityId
                            , TransferType = t.TransferType
                            , MaterialType = t.MaterialType
                            , TransportName = t.TransportName
                            , TransportNo = t.TransportNo
                            , DriverName = t.DriverName
                            , DriverNo = t.DriverNo
                            , ExpectedArrivalDate = t.ExpectedArrivalDate
                            
                          }).FirstOrDefault();

            return Mapping<TruckDetailAPI>(result);
        }
        /*public EcCheckOutModel GetDataForCheckoutAPI(long truckDetailId)
        {
            var result = entities.API_CheckOut_TruckDetail_G(truckDetailId).FirstOrDefault();

            return Mapping<EcCheckOutModel>(result);
        }*/
    }
}
