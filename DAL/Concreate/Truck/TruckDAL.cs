using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using Model.Models;
using WEB.Helper;
using System.Data;

namespace DAL.Concreate
{
    public class TruckDAL: BaseClassDAL, ITruckDAL
    {
        LFZ_TruckParkEntities entities = new LFZ_TruckParkEntities();

        public ResponseInfo AddTruck(AddTruckPark model)
        {
            ResponseInfo respInfo = new ResponseInfo();
            System.Data.Entity.Core.Objects.ObjectParameter OutputParam = new System.Data.Entity.Core.Objects.ObjectParameter("OutId", typeof(int));
            var result = entities.Truck_CRUD(model.TruckId,model.TruckNo, model.OwnedByOrganizationId, model.TruckCapacityId, model.Createdby, model.CalledByOrganizationId, model.ExpectedArrivalDate, model.ExpectedDepatureDate, model.LocalTransferTypeId, model.TransportName, model.TransportNo,
                                            model.DriverName, model.DriverNo, model.MaterialTypeId, model.MaterialGoods, model.ActualArrivalDate, model.ActualDepatureDate, model.IsForecasted, model.IsCheckedIn, model.IsCalledOut, OutputParam, null, (int)Model.CommonEnum.LOV.SqlOperations.INSERT);

            respInfo.Status = "";
            respInfo.IsSuccess = true;
            respInfo.Msg = "";
            respInfo.ID = Convert.ToInt32(OutputParam.Value);
            return respInfo;
        }

        public List<Model.Models.Truck> GetTruckInfo(string truckNo)
        {
            var result = entities.Truck_G(truckNo);
            List<Model.Models.Truck> objTruck = Mapping<List<Model.Models.Truck>>(result);
            return objTruck;
        }

        public List<TruckDetails> GetTruckList(int? RoleId, int? UserId, int?Id)
        {
            var result = entities.TruckList_G(RoleId, UserId, Id).ToList();
            List<TruckDetails> list = Mapping<List<TruckDetails>>(result);
            return list;
        }

        public ResponseInfo UpdateStatus(UpdateTruckStatus model)
        {
            ResponseInfo respInfo = new ResponseInfo();
            System.Data.Entity.Core.Objects.ObjectParameter OutputParam = new System.Data.Entity.Core.Objects.ObjectParameter("OutError", typeof(string));
            var result = entities.TruckStatus_U(model.TruckDetailsList, (byte)model.status, model.ModifiedBy, OutputParam);
            respInfo.Status = "";
            respInfo.IsSuccess = true;
            respInfo.Msg = OutputParam.Value.ToString();
            return respInfo;
        }

        public List<TruckDetails> GetTruckCheckedOutList(int? RoleId, int? UserId, int? Id)
        {
            var result = entities.TruckCheckedOutList_G(RoleId, UserId, Id).ToList();
            List<TruckDetails> list = Mapping<List<TruckDetails>>(result);
            return list;
        }

        public List<TruckDetails> GetTruckCheckedInList(int?RoleId, int? UserId, int? Id)
        {
            var result = entities.TruckCheckedInList_G(RoleId, UserId, Id).ToList();
            List<TruckDetails> list = Mapping<List<TruckDetails>>(result);
            return list;
        }

        public List<SendMailModel> GetDetailsForSendMail(string truckDetailsIds)
        {
            var result = entities.OrganizationMailId_G(truckDetailsIds).ToList();
            List<SendMailModel> list = Mapping<List<SendMailModel>>(result);
            return list;
        }

        public DataSet GetAdminReport()
        {
            DataSet dt = new DataSet();
            dt = SqlManager.ExecuteDataSet("Scheduler_Admin");
            return dt;
        }

        public List<TruckDetails> GetBillableTrucks()
        {
            var result = entities.BillableTrucks_G().ToList();
            List<TruckDetails> list = Mapping<List<TruckDetails>>(result);
            return list;
        }

        public EditTruckDetails GetTruckDetailsOnId(int Id)
        {
            var result = entities.TruckDetails_G(Id).FirstOrDefault();
            EditTruckDetails model = Mapping<EditTruckDetails>(result);
            return model;
        }

        public ResponseInfo UpdateTruck(EditTruckDetails model)
        {
            ResponseInfo respInfo = new ResponseInfo();
            System.Data.Entity.Core.Objects.ObjectParameter OutputParam = new System.Data.Entity.Core.Objects.ObjectParameter("OutId", typeof(int));
            var result = entities.Truck_CRUD((int)model.TruckId, model.TruckNo, model.CalledByOrganizationId, model.TruckCapacityId, model.Createdby, model.CalledByOrganizationId, model.ExpectedArrivalDate, model.ExpectedDepatureDate, model.LocalTransferTypeId, model.TransportName, model.TransportNo,
                                            model.DriverName, model.DriverNo, model.MaterialTypeId, model.MaterialGoods, model.ActualArrivalDate, model.ActualDepatureDate, model.IsForecasted, model.IsCheckedIn, model.IsCalledOut, OutputParam, (int)model.TruckDetailsId, (int)Model.CommonEnum.LOV.SqlOperations.UPDATE);

            respInfo.Status = "";
            respInfo.IsSuccess = true;
            respInfo.Msg = "";
            return respInfo;
        }

        public TruckStatus GetTruckDetails(string truckNo)
        {
            var result = entities.CheckTruckStatus_G(truckNo).FirstOrDefault();
            TruckStatus details = Mapping<TruckStatus>(result);
            return details;
        }


        public List<TruckDetails> GetTruckCalledOutList(int? RoleId, int? UserId, int? Id)
        {
            var result = entities.TruckCalledOutList_G(RoleId, UserId, Id).ToList();
            List<TruckDetails> list = Mapping<List<TruckDetails>>(result);
            return list;
        }

        public List<ViewTrucksEXT> ViewTruckExtListDAL()
        {
            var result = entities.TruckDetailsAPI_List_G().ToList();
            return Mapping<List<ViewTrucksEXT>>(result);
        }
        public List<TruckDetails> GetFullDumpCheckoutListDAL()
        {
            var result = entities.TruckCheckedOutList_G(-1, -1, -1).ToList();
            return Mapping<List<TruckDetails>>(result);
        }


        #region API
        public ResponseInfo SaveAPISentLog(ResponseInfoAPI model)
        {
            entities.API_SentLog(model.PayLoad, model.SourceIP);
            return new ResponseInfo() { IsSuccess = true };
        }
        #endregion
    }
}
