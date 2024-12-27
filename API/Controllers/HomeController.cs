using BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model.Models;
using Model.Models.UserDetail;
using Model.Models.Account;

namespace API.Controllers
{
    public class HomeController : ApiController
    {
        private IHomeBAL _iHomeBAL;
        HomeController()
        {
            _iHomeBAL = ServiceFactory.GetHomeInstance();
        }

        //[HttpPost]
        //public IHttpActionResult SaveRegistration(RegistrationModel model)
        //{
        //    return Ok(_iHomeBAL.SaveRegistrationBAL(model));
        //}

        [HttpPost]
        public IHttpActionResult CheckLogin(UserDetailModel model)
        {
            return Ok(_iHomeBAL.CheckLoginBAL(model));
        }

        [HttpPost]
        public IHttpActionResult ChangePassword(ChangePassword model)
        {
            return Ok(_iHomeBAL.ChangePassword(model));
        }

        [HttpGet]
        public IHttpActionResult Dashboard(int UserID, int RoleID, int? Id)
        {
            return Ok(_iHomeBAL.Dashboard(RoleID, UserID, Id));
        }

        [HttpPost]
        public IHttpActionResult CheckValidUser(ValididateUser_OnePortal model)
        {

            return Ok(_iHomeBAL.ValidUserBAL(model));
        }

    }
}
