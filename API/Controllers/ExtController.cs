using BAL.Interface.Ext;
using Model.Models;
using Model.Models.Ext;
using Model.Models.Ext.Response;
using Model.Models.Ext.Response.Truck;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{

    public class ExtController : ApiController
    {

        private IExtBAL _iExtBAL;

        public ExtController()
        {
            _iExtBAL = ServiceFactory.GetExtInstance();
        }

        [HttpPost]
        public IHttpActionResult EcallUpExt([FromBody]EcMainModel model)
        {
            var apikey = Request.Headers.GetValues("EXT_API_KEY").FirstOrDefault();

            if (apikey == null)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, new ExtAPIBaseRespModel(false, "Invalid EXT_API_KEY Header"));
            }

            var resp = _iExtBAL.CheckApiKey(apikey);
            if (resp == null || !resp.IsActive)
            {
                return Content(System.Net.HttpStatusCode.Unauthorized, new ExtAPIBaseRespModel(false, "Invalid EXT_API_KEY"));
            }

            var reLog = new ResponseInfoAPI()
            {
                APIKeyId = resp.APIKeyId
                 ,
                PayLoad = JsonConvert.SerializeObject(model)
                 ,
                SourceIP = Request.RequestUri.ToString()
            };

            _iExtBAL.ReceivedLog(reLog);

            var eventName = Request.Headers.GetValues("EVENT_TYPE").FirstOrDefault();

            if (eventName == "AddTruckPark")
            {
                model.StatusesHistory = JsonConvert.SerializeObject(model.Statuses);
                var saveData = _iExtBAL.AddTruckParkData(model);

                TruckDetailAPI truckDetails = _iExtBAL.GetTruckDetails(saveData.LongID.Value);

                return Content(System.Net.HttpStatusCode.OK, new ExtAPITruckDetailsRespModel(truckDetails));
            }
            else
                return Content(System.Net.HttpStatusCode.BadRequest, new ExtAPIBaseRespModel(false, "Invalid EVENT_TYPE Header"));
        }

    }
}