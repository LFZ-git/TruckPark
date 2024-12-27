using BAL.Interface;
using Model.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    public class RoleController : ApiController
    {
        private IRoleBAL _iRoleBAL;

        RoleController()
        {
            _iRoleBAL = ServiceFactory.GetRoleInstance();
        }

        [HttpGet]
        public IHttpActionResult GetRoleId()
        {
            return Ok(_iRoleBAL.GetRole());
        }
    }
}