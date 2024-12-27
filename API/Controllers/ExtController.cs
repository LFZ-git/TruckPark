using BAL.Interface.Ext;
using Model.Models;
using Model.Models.Ext;
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
                return Content(System.Net.HttpStatusCode.Unauthorized, new { mssg = "API Key is Null" });
            }

            var resp = _iExtBAL.CheckApiKey(apikey);
            if (resp == null || !resp.IsActive)
            {
                return Content(System.Net.HttpStatusCode.Unauthorized, new { mssg = "Invaild API Key" });
            }
            else
            {
                var reLog = new ResponseInfoAPI()
                {
                   APIKeyId = resp.APIKeyId
                 , PayLoad = JsonConvert.SerializeObject(model)
                 , SourceIP = Request.RequestUri.ToString()
                };
                _iExtBAL.ReceivedLog(reLog);
                var eventName = Request.Headers.GetValues("event-type").FirstOrDefault();

                if (eventName == "AddTruckPark")
                {
                    model.StatusesHistory = JsonConvert.SerializeObject(model.Statuses);
                    var saveData = _iExtBAL.AddTruckParkData(model);
                    return Content( System.Net.HttpStatusCode.OK, new { mssg = "Successfully Added." });
                }
                else
                    return Content(System.Net.HttpStatusCode.BadRequest, new { mssg = "The request could not be understood by the server due to malformed syntax or missing required parameters." });

            }

           
        }

    }
}