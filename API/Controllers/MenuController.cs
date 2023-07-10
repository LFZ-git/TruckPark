using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using API;
using API.Filter;
using BAL.Interface;

namespace API.Controllers
{
    [APIExceptionFilter]
    [Authorize]
    public class MenuController : ApiController
    {
        private IMenu _iMenu;
        public MenuController()
        {
            _iMenu = ServiceFactory.GetMenuObject();
        }
        public IHttpActionResult GetMenu()
        {
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            var claims = claimsIdentity.Claims.Select(x => new { type = x.Type, value = x.Value }).ToList();
            List<Model.Models.Menu.M_Modules> menuList = _iMenu.getMenu(claims[2].value);
            return Ok(menuList);
        }
    }
}
