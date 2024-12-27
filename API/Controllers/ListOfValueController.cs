using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API;
using BAL.Interface;

namespace API.Controllers
{
    public class ListOfValueController : ApiController
    {
        private IListOfValueBAL _iListOfValueDetail;
        public ListOfValueController()
        {
            _iListOfValueDetail = ServiceFactory.GetListOfValueInstance();
        }

        public IHttpActionResult GetListOfValue(int id)
        {
            return Ok(_iListOfValueDetail.GetListOFValueDetail(id));
        }
    }
}
