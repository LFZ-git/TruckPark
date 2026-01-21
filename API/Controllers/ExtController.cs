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
using Utility;

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
        public IHttpActionResult EcallUpExt()
        {
            try
            {
                string rawJson = Request.Content.ReadAsStringAsync().Result;

                FileLogger.Log(rawJson);

                EcMainModel model = JsonConvert.DeserializeObject<EcMainModel>(rawJson);

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

                IEnumerable<string> eventTypes;

                found = Request.Headers.TryGetValues("EVENT_TYPE", out eventTypes);

                string eventName = found ? eventTypes.FirstOrDefault() : null;

                if (eventName == null)
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, new ExtAPIBaseRespModel(false, "Invalid EVENT_TYPE Header"));
                }

                if (eventName == "ADD_TRUCK")
                {
                    model.StatusesHistory = JsonConvert.SerializeObject(model.Statuses);
                    var saveData = _iExtBAL.AddTruckParkData(model);

                    if(saveData == null || !saveData.IsSuccess)
                    {
                        return Content(System.Net.HttpStatusCode.BadRequest, new ExtAPIBaseRespModel(false, saveData.Msg));
                    }

                    TruckDetailAPI truckDetails = _iExtBAL.GetTruckDetails(saveData.LongID.Value);

                    return Content(System.Net.HttpStatusCode.OK, new ExtAPITruckDetailsRespModel(truckDetails));
                }
                else
                    return Content(System.Net.HttpStatusCode.BadRequest, new ExtAPIBaseRespModel(false, "Invalid EVENT_TYPE Header"));
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex);

                return Content(System.Net.HttpStatusCode.InternalServerError, new ExtAPIBaseRespModel(false, ExceptionUtility.ToJson(ex)));
            }
        }

        /*[HttpGet]
       public IHttpActionResult GetDataForCheckoutAPI(int id)
        {
            return Ok(_iExtBAL.GetDataForCheckoutAPI(Convert.ToInt64(id)));
        }*/
       
    }
}