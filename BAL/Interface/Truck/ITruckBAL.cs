using Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface ITruckBAL
    {
        ResponseInfo AddTruck(AddTruckPark model);

        List<Model.Models.Truck> GetTruckInfo(string truckNo);

        List<TruckDetails> GetTruckList(int? RoleId, int? UserId, int? Id);

        ResponseInfo UpdateStatus(UpdateTruckStatus model);

        List<TruckDetails> GetTruckCheckedOutList(int? RoleId, int? UserId, int? Id);

        List<TruckDetails> GetTruckCheckedInList(int? RoleId, int? UserId, int? Id);

        List<SendMailModel> GetDetailsForSendMail(string truckDetailsIds);

        DataSet GetAdminReport();

        List<TruckDetails> GetBillableTrucks();

        EditTruckDetails GetTruckDetailsId(int Id);

        ResponseInfo UpdateTruck(EditTruckDetails model);

        TruckStatus GetTruckDetails(string truckNo);

        List<TruckDetails> GetTruckCalledOutList(int? RoleId, int? UserId, int? Id);
        List<ViewTrucksEXT> ViewTruckExtListBAL();
        List<TruckDetails> GetFullDumpCheckoutListBAL();

        #region API
        ResponseInfo SaveAPISentLog(ResponseInfoAPI model);
        #endregion

    }
}
