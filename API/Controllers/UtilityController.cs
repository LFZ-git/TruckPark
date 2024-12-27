using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAL.Interface;
using Model.Models;

namespace API.Controllers
{
    public class UtilityController : ApiController
    {
        private IUtilityBAL _iUtlityBal;
        public UtilityController()
        {
            _iUtlityBal = ServiceFactory.GetUtilityInstance();
        }

        public IHttpActionResult LogError(ErrorLogModel logModel)
        {
            _iUtlityBal.LogError(logModel);
            ResponseInfo responseInfo = new ResponseInfo();
            responseInfo.IsSuccess = true;
            return Ok(responseInfo);
        }

        public IHttpActionResult LogActivity(ActivityModel logMode)
        {
            _iUtlityBal.ActivityLog(logMode);
            ResponseInfo responseInfo = new ResponseInfo();
            responseInfo.IsSuccess = true;
            return Ok(responseInfo);
        }
    }
}
