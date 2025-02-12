using BAL.Interface;
using DAL.Interface;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Concreate
{
    public class TruckBAL: ITruckBAL
    {
        private ITruckDAL _iTruckDal;
        public TruckBAL()
        {
            _iTruckDal = BALFactory.GetTruckInstance();
        }

        public ResponseInfo AddTruck(AddTruckPark model)
        {
            return _iTruckDal.AddTruck(model);
        }

        public List<Model.Models.Truck> GetTruckInfo(string truckNo)
        {
            return _iTruckDal.GetTruckInfo(truckNo);
        }

        public List<TruckDetails> GetTruckList(int? RoleId, int? UserId, int? Id)
        {
            return _iTruckDal.GetTruckList(RoleId, UserId, Id);
        }

        public ResponseInfo UpdateStatus(UpdateTruckStatus model)
        {
            return _iTruckDal.UpdateStatus(model);
        }

        public List<TruckDetails> GetTruckCheckedOutList(int? RoleId, int? UserId, int? Id)
        {
            return _iTruckDal.GetTruckCheckedOutList(RoleId, UserId, Id);
        }

        public List<TruckDetails> GetTruckCheckedInList(int? RoleId, int? UserId, int? Id)
        {
            return _iTruckDal.GetTruckCheckedInList(RoleId, UserId, Id);
        }

        public List<SendMailModel> GetDetailsForSendMail(string truckDetailsIds)
        {
            return _iTruckDal.GetDetailsForSendMail(truckDetailsIds);
        }

        public DataSet GetAdminReport()
        {
            return _iTruckDal.GetAdminReport();
        }

        public List<TruckDetails> GetBillableTrucks()
        {
            return _iTruckDal.GetBillableTrucks();
        }

        public EditTruckDetails GetTruckDetailsId(int Id)
        {
            return _iTruckDal.GetTruckDetailsOnId(Id);
        }

        public ResponseInfo UpdateTruck(EditTruckDetails model)
        {
            return _iTruckDal.UpdateTruck(model);
        }

        public TruckStatus GetTruckDetails(string truckNo)
        {
            return _iTruckDal.GetTruckDetails(truckNo);
        }

        public List<TruckDetails> GetTruckCalledOutList(int? RoleId, int? UserId, int? Id)
        {
            return _iTruckDal.GetTruckCalledOutList(RoleId, UserId, Id);
        }

        public List<ViewTrucksEXT> ViewTruckExtListBAL()
        {
            return _iTruckDal.ViewTruckExtListDAL();
        }
    }
}
