using Model.Models;
using Model.Models.Role;
using Model.Models.UserDetail;
using WEB.APIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.Controllers;

namespace WEB.Controllers
{
    public class RoleController : BaseController
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
    }
}