using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.APIHelper;
using Model;
using System.Threading.Tasks;
using System.Net.Http;
using Utility;
using Model.Models.UserDetail;
using Model.Models;
using System.Web.UI;
using WEB.Helper;
using System.Data;
using System.Globalization;

namespace WEB.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {

            return View();

        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            int organizationId = 0;
            UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];
            DashboardModel data = WebAPIHelper.CallApi<DashboardModel>(HttpMethods.Get, "Dashboard", "Home", null, userdetail.OrganizationID, userdetail.UDID, userdetail.RoleId);
            ViewBag.Hours0to4 = data.Hours0to4;
            ViewBag.Hours4to8 = data.Hours4to8;
            ViewBag.Hours8to16 = data.Hours8to16;
            ViewBag.Hours16to24 = data.Hours16to24;
            ViewBag.Hours24to48 = data.Hours24to48;
            ViewBag.Hours48to72 = data.Hours48to72;
            ViewBag.Hours72More = data.Hours72More;

            return View(data);
        }

        //[HttpPost]
        //public ActionResult LoadDashboard(string startDate, string endDate)
        //{

        //}
    }
}