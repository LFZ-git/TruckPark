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
        public IHttpActionResult EcallUpExt([FromBody] EcMainModel model)
        {
            try
            {
                IEnumerable<string> apiKeys;

                bool found = Request.Headers.TryGetValues("API_KEY", out apiKeys);

                string apikey = found ? apiKeys.FirstOrDefault() : null;

                if (apikey == null)
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, new ExtAPIBaseRespModel(false, "Invalid API_KEY Header"));
                }

                var resp = _iExtBAL.CheckApiKey(apikey);

                if (resp == null || !resp.IsActive)
                {
                    return Content(System.Net.HttpStatusCode.Unauthorized, new ExtAPIBaseRespModel(false, "Invalid API_KEY"));
                }

                var reLog = new ResponseInfoAPI()
                {
                    APIKeyId = resp.APIKeyId,
                    PayLoad = JsonConvert.SerializeObject(model),
                    SourceIP = Request.RequestUri.ToString()
                };

                _iExtBAL.ReceivedLog(reLog);

                var eventName = Request.Headers.GetValues("EVENT_TYPE").FirstOrDefault();

                if (eventName == "ADD_TRUCK")
                {
                    model.StatusesHistory = JsonConvert.SerializeObject(model.Statuses);
                    var saveData = _iExtBAL.AddTruckParkData(model);

                    TruckDetailAPI truckDetails = _iExtBAL.GetTruckDetails(saveData.LongID.Value);

                    return Content(System.Net.HttpStatusCode.OK, new ExtAPITruckDetailsRespModel(truckDetails));
                }
                else
                    return Content(System.Net.HttpStatusCode.BadRequest, new ExtAPIBaseRespModel(false, "Invalid EVENT_TYPE Header"));
            }
            catch (Exception ex)
            {
                return Content(System.Net.HttpStatusCode.InternalServerError, new ExtAPIBaseRespModel(false, ex.ToString()));
            }
        }

        /*[HttpGet]
       public IHttpActionResult GetDataForCheckoutAPI(int id)
        {
            return Ok(_iExtBAL.GetDataForCheckoutAPI(Convert.ToInt64(id)));
        }*/
       
    }
}