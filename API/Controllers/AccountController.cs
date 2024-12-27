using System.Web.Http;
using Model;
using BAL.Interface;
using API.Filter;
using Model.Models.UserDetail;
using System.Security.Claims;
using System.Linq;
using System;
using Model.Models.Account;

namespace API.Controllers
{
    public class AccountController : ApiController
    {
        private IUsersBAL iUserBal;
        public AccountController()
        {
            iUserBal = ServiceFactory.GetBalObject();
        }

        //[APIExceptionFilter]
        //[Authorize]
        //public IHttpActionResult SaveUsersDetails(UserDetailModel user)
        //{
        //    return Ok(iUserBal.SaveUsersDetails(user));
        //}

        [APIExceptionFilter]
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetUserDetails()
        {
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            var claims = claimsIdentity.Claims.Select(x => new { type = x.Type == "Name", value = x.Value }).ToList();
            return Ok(iUserBal.GetUserDetails(Convert.ToInt32(claims[0].value)));
        }

        [HttpPost]
        public IHttpActionResult GetUserDetailsOne(ValididateUser_OnePortal model)
        {
            return Ok(iUserBal.GetUserDetailsOne(model));
        }
    }
}
