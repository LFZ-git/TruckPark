using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BAL.Interface;

namespace API.Controllers
{
    public class MasterController : ApiController
    {
        private IMasterBAL _iMasterBal;

        public MasterController()
        {
            _iMasterBal = ServiceFactory.GetMasterInstance();
        }

        public IHttpActionResult GetTruckCapacity()
        {
            return Ok(_iMasterBal.GetTruckCapacity());
        }
    }
}