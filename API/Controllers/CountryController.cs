using BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class CountryController : ApiController
    {
        private ICountryBAL _iCountry;

        public CountryController()
        {
            _iCountry = ServiceFactory.GetCountryInstance();
        }

        public IHttpActionResult GetCountry()
        {
            return Ok(_iCountry.GetCountry());
        }
    }
}
