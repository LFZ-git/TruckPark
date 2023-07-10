using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BAL.Interface;
using Model.Models;

namespace API.Controllers
{
    public class TruckController : ApiController
    {
        private ITruckBAL _iTruckBal;
        public TruckController()
        {
            _iTruckBal = ServiceFactory.GetTruckInstance();
        }

        [HttpPost]
        public IHttpActionResult AddTruck(AddTruckPark model)
        {
            return Ok(_iTruckBal.AddTruck(model));
        }

        [HttpPost]
        public IHttpActionResult GetTruckInfo(Truck model)
        {
            return Ok(_iTruckBal.GetTruckInfo(model.TruckNo));
        }

        [HttpGet]
        public IHttpActionResult GetTruckInfo()
        {
            string TruckNo = string.Empty;
            return Ok(_iTruckBal.GetTruckInfo(TruckNo));
        }

        [HttpGet]
        public IHttpActionResult GetTruckList(int RoleID, int UserID, int? Id)
        {
            return Ok(_iTruckBal.GetTruckList(RoleID, UserID, Id));
        }

        [HttpPost]
        public IHttpActionResult UpdateTruckStatus(UpdateTruckStatus model)
        {
            return Ok(_iTruckBal.UpdateStatus(model));
        }


        [HttpGet]
        public IHttpActionResult GetTruckCheckedOutList(int RoleID, int UserID, int? Id)
        {
            return Ok(_iTruckBal.GetTruckCheckedOutList(RoleID, UserID, Id));
        }

        [HttpGet]
        public IHttpActionResult GetTruckCheckedInList(int RoleID, int UserID, int? Id)
        {
            return Ok(_iTruckBal.GetTruckCheckedInList(RoleID, UserID, Id));
        }

        [HttpPost]
        public IHttpActionResult SendMailDetails(UpdateTruckStatus model)
        {
            return Ok(_iTruckBal.GetDetailsForSendMail(model.TruckDetailsList));
        }

        [HttpGet]
        public IHttpActionResult GetAdminReport()
        {
            return Ok(_iTruckBal.GetAdminReport());
        }

        [HttpGet]
        public IHttpActionResult GetBillableTrucks()
        {
            return Ok(_iTruckBal.GetBillableTrucks());
        }

        [HttpGet]
        public IHttpActionResult GetTruckDetailsOnId(int Id)
        {
            return Ok(_iTruckBal.GetTruckDetailsId(Id));
        }

        [HttpPost]
        public IHttpActionResult UpdateTruck(EditTruckDetails model)
        {
            return Ok(_iTruckBal.UpdateTruck(model));
        }

        [HttpPost]
        public IHttpActionResult GetTruckDetails(Truck model)
        {
            return Ok(_iTruckBal.GetTruckDetails(model.TruckNo));
        }

        [HttpGet]
        public IHttpActionResult GetCalledOutTruckList(int RoleID, int UserID, int? Id)
        {
            return Ok(_iTruckBal.GetTruckCalledOutList(RoleID, UserID, Id));
        }
    }
}