using Model.Models;
using Model.Models.ListOfValue;
using Model.Models.Role;
using Model.Models.UserCreation;
using Model.Models.UserDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.APIHelper;
using WEB.Helper;

namespace WEB.Controllers
{
    public class UserCreationController : BaseController
    {

        private void GetMaters()
        {
            //For Role dropdown
            List<RoleModel> Roles = WebAPIHelper.CallApi<List<RoleModel>>(HttpMethods.Get, "GetRoleId", "Role");
            ViewBag.Roles = new SelectList(Roles, "RoleId", "RoleName");

            // For userName dropdown
            List<UserDetailModel> userdetail = WebAPIHelper.CallApi<List<UserDetailModel>>(HttpMethods.Get, "GetUserDetails", "UsersDetail");
            ViewBag.userdetail = new SelectList(userdetail, "UDID", "EmployeeName");

            int EventTypeId = (int)Model.CommonEnum.LOV.LOVId.DepartmentType;
            List<ListOfValueModel> Event = WebAPIHelper.CallApi<List<ListOfValueModel>>(HttpMethods.Get, "GetListOfValue", "ListOfValue", null, EventTypeId);
            ViewBag.events = new SelectList(Event, "LOVId", "LOVName");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            //UserCreationCommonModel objMainAddUser = new UserCreationCommonModel();

            //objMainAddUser.SaveUser = null;

            GetMaters();
            IList<OrganizationModel> list = WebAPIHelper.CallApi<IList<OrganizationModel>>(HttpMethods.Get, "GetOrganizationList", "Organization");
            ViewBag.Organizations = new SelectList(list, "OrganizationID", "CompanyName");
            return View();
        }

        [HttpPost]

        public ActionResult SaveUser(UserCreationModel objAddUser)
        {

            int userId = 0;
            if (Session != null)
            {
                UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];
                userId = userdetail.UDID;
                // objAddUser.CreatedBy = userId;
            }
            objAddUser.CreatedBy = userId;
            objAddUser.saltKey = helperClass.GeneratePassword(10); //Password Encrypt Here           
            objAddUser.Password = helperClass.EncodePassword(objAddUser.Password.ToString(), objAddUser.saltKey); //Password Encrypt Here

            ResponseInfo si = WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "SaveUser", "UserCreation", objAddUser);



            GetMaters();

            if (objAddUser.UDID == 0)
            {
                //TempData["Success"] = si.Msg;
                TempData["Message"] = "User Added Successfully.";
                ViewBag.AlertType = "success";
                ViewBag.AlertTitle = "Success";
                ModelState.Clear();
                //return View("Create", objAddUser);
                return RedirectToAction("Create");
                //return View("List", objAddUser);
            }
            else
            {
                ModelState.Clear();
                return View("Create", objAddUser);
            }


        }
        [HttpGet]
        public ActionResult List()
        {
            GetMaters();
            List<GetUserCreationModel> lstUserList = WebAPIHelper.CallApi<List<GetUserCreationModel>>(HttpMethods.Get, "GetUserList", "UserCreation");

            return View(lstUserList);

        }
        public ActionResult Edit(int UDID)
        {
           
            UserCreationModel GetUserEdit = WebAPIHelper.CallApi<UserCreationModel>(HttpMethods.Get, "GetUserDetails", "UserCreation", null, UDID);

            GetMaters();
            IList<OrganizationModel> list = WebAPIHelper.CallApi<IList<OrganizationModel>>(HttpMethods.Get, "GetOrganizationList", "Organization");
            ViewBag.Organizations = new SelectList(list, "OrganizationID", "CompanyName");

            OrganizationModel OrganizationModel = WebAPIHelper.CallApi<OrganizationModel>(HttpMethods.Get, "GetOrganizationDetails", "Organization", null, GetUserEdit.OrganizationID);
            ViewBag.orgname = "LFZ Admin";//OrganizationModel.CompanyName;

            return View("Edit", GetUserEdit);
        }
        [HttpPost]
        public ActionResult UpdateUser(UserCreationModel objAddUser)
        {
            int userId = 0;
            string saltkey = string.Empty;
            if (Session != null)
            {
                UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];
                userId = userdetail.UDID;
                saltkey = userdetail.SaltKey;
                // objAddUser.CreatedBy = userId;
            }
            objAddUser.CreatedBy = userId;
            if(objAddUser.Password != null)
            {
                objAddUser.saltKey = saltkey;
                objAddUser.Password = helperClass.EncodePassword(objAddUser.Password.ToString(), objAddUser.saltKey); //Password Encrypt Here
            }
            ResponseInfo si = WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "SaveUser", "UserCreation", objAddUser);



            GetMaters();

            //TempData["Success"] = si.Msg;
            //ModelState.Clear();

            TempData["Message"] = "User Updated Successfully.";
            ViewBag.AlertType = "success";
            ViewBag.AlertTitle = "Success";

            return RedirectToAction("List");

        }


        public ActionResult Delete(int UDID)
        {
            UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];

            var rs = WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "DeleteUserDetails", "UserCreation", null, UDID);

            TempData["Success"] = rs.Msg;

            return RedirectToAction("List");
        }
    }
}