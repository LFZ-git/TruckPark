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
        LFZ_TruckPark_NewEntities entities = new LFZ_TruckPark_NewEntities();
        public ResponseInfoAPI CheckApiKey(string apiKey)
        {
            var result = (from key in entities.M_APIKey
                          where key.APIKey == apiKey
                          select new
                          {
                              APIKeyId = key.APIKeyId
                                      ,
                              APIKey = key.APIKey
                                      ,
                              PortalName = key.PortalName
                                      ,
                              IsActive = key.IsActive
                          }).FirstOrDefault();

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
            ObjectParameter outIsMaterialTypeMapFailed = new ObjectParameter("OutIsMaterialTypeMapFailed", typeof(bool));
            ObjectParameter outIsTransferTypeMapFailed = new ObjectParameter("OutIsTransferTypeMapFailed", typeof(bool));
            ObjectParameter outIsTruckCapacityMapFailed = new ObjectParameter("OutIsTruckCapacityMapFailed", typeof(bool));
            ObjectParameter outIsUserMapFailed = new ObjectParameter("OutIsUserMapFailed", typeof(bool));

            if (model.Driver == null)
            {
                model.Driver = new EcDriver()
                {
                    Phone = "",
                    FirstName = "",
                    LastName = ""
                };
            }


            model.EstimatedArrivalDate = model.EstimatedArrivalDate.Value.Add(model.EstimatedArrivalTime.TimeOfDay);

            var resp = IsTruckExists(model);

            if (!resp.IsSuccess) return resp;

            entities.Truck_CRUD_API(model.Id, model.Truck.Id, model.Truck.PlateNumber, model.Company.Id, model.Truck.Capacity.Id, model.EstimatedArrivalDate
                                    , null, model.Category.Id, model.User.FullName, model.User.Phone, model.Driver.FirstName + " " + model.Driver.LastName
                                    , model.Driver.Phone, model.Material.Id, model.User.Id, model.Terminal.Id, outId, outMssg, outIsSuccess, outIsMaterialTypeMapFailed, outIsTransferTypeMapFailed, outIsTruckCapacityMapFailed, outIsUserMapFailed);


            return new ResponseInfo()
            {
                IsSuccess = (bool)outIsSuccess.Value
                ,
                Msg = outMssg.Value.ToString()
                ,
                LongID = (long)outId.Value,
                IsUserMapFailed = (bool)outIsUserMapFailed.Value,
                IsMaterialTypeMapFailed = (bool)outIsMaterialTypeMapFailed.Value,
                IsTransferTypeMapFailed = (bool)outIsTransferTypeMapFailed.Value,
                IsTruckCapacityMapFailed = (bool)outIsTruckCapacityMapFailed.Value,
            };
        }

        ResponseInfo IsTruckExists(EcMainModel model)
        {
            ObjectParameter outIsSuccess = new ObjectParameter("OutIsSuccess", typeof(bool));
            ObjectParameter outMssg = new ObjectParameter("OutMssg", typeof(string));
            ObjectParameter outId = new ObjectParameter("OutId", typeof(long));
            ObjectParameter outIsTruckCapacityMapFailed = new ObjectParameter("OutIsTruckCapacityMapFailed", typeof(bool));
            ObjectParameter outIsUserMapFailed = new ObjectParameter("OutIsUserMapFailed", typeof(bool));

            entities.IsTruckExists(model.Truck.PlateNumber, model.Truck.Id, model.Company.Id, model.Truck.Capacity.Id, model.User.Id, outId, outMssg, outIsSuccess, outIsTruckCapacityMapFailed, outIsUserMapFailed);

            return new ResponseInfo()
            {
                IsSuccess = (bool)outIsSuccess.Value,
                Msg = outMssg.Value.ToString(),
                LongID = (long)outId.Value,
                IsUserMapFailed = (bool)outIsUserMapFailed.Value,
                IsTruckCapacityMapFailed = (bool)outIsTruckCapacityMapFailed.Value,
            };
        }

        public TruckDetailAPI GetTruckDetails(long truckDetailId)
        {
            var result = entities.Truck_API_G(truckDetailId).FirstOrDefault();

            return Mapping<TruckDetailAPI>(result);
        }
        /*public EcCheckOutModel GetDataForCheckoutAPI(long truckDetailId)
        {
            var result = entities.API_CheckOut_TruckDetail_G(truckDetailId).FirstOrDefault();

            return Mapping<EcCheckOutModel>(result);
        }*/
    }
}
