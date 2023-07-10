using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BAL.Interface;

namespace API.Controllers
{
    public class UsersDetailController : ApiController
    {
        private IUsersDetailBAL _iUsersDetail;
        public UsersDetailController()
        {
            _iUsersDetail = ServiceFactory.GetUsersDetailInstance();
        }

        public IHttpActionResult GetUserDetails()
        {
            return Ok(_iUsersDetail.GetUsersDetails());
        }
    }
}