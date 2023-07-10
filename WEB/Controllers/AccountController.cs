using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WEB.APIHelper;
using Model;
using Model.Models.UserDetail;
using Utility;
using Model.Models;
using System.Configuration;
using System.IO;
using Model.Models.Menu;
using Model.Models.Account;
using WEB.Helper;

namespace WEB.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {

            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            //BaseConfig.Url =HttpContext.Request.Url.ToString();
            BaseConfig.Url = ConfigurationManager.AppSettings["BaseUrl"];
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                UserDetailModel users = new UserDetailModel();
                users.EmailId = login.UserId;
                users.Password = login.Password;
                string a = await GetToken(login);
                if (!string.IsNullOrEmpty(a))
                {
                    var Info = WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "CheckLogin", "Home", users);
                    if (Info.Status == "true")
                    {
                        UserDetailModel userdetail = WebAPIHelper.CallApi<UserDetailModel>(HttpMethods.Get, "GetUserDetails", "Account");
                        Session["UserDetails"] = userdetail;
                        Session["RoleId"] = userdetail.RoleId;
                          //List<M_Modules> s = await CallApi<List<M_Modules>>(HttpMethods.Get, "/Menu/GetMenu");
                          //Session["Menu"] = s;
                        if (Session["count"] == null)
                            Session["count"] = "0";
                        //if (userdetail.RoleId == 1)
                        //    return RedirectToAction("ModuleListing", "LoggedIn", new { area = "" });
                        //else
                        //{
                        //    return RedirectToAction("GetInActiveUser", "LoggedIn", new { area = "" });
                        //}
                        return RedirectToAction("Dashboard", "Home", new { area = "" });

                    }
                }
            }
            TempData["NotValid"] = "Please check username or password";
            ModelState.Clear();
            return View();
        }


        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.Clear();


            return RedirectToAction("Login", "Account");
        }

        public ActionResult ChangePassword()
        {
            BaseConfig.Url = ConfigurationManager.AppSettings["BaseUrl"];
            
            return View(new ChangePassword());

        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword cPassword)
        {
            string OldPassword = "";
            string OldPasswordgenerated = "";
            if (ModelState.IsValid)
            {
                if (Session != null)
                {
                    UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];
                    cPassword.UDID = userdetail.UDID;
                    string OldSaltKey = userdetail.SaltKey;
                    OldPassword = userdetail.Password;
                    OldPasswordgenerated = helperClass.EncodePassword(cPassword.OldPassword.ToString(), OldSaltKey); //Password Encrypt Here
                }
                cPassword.SaltKey = helperClass.GeneratePassword(10); //Password Encrypt Here           
                cPassword.Password = helperClass.EncodePassword(cPassword.Password.ToString(), cPassword.SaltKey); //Password Encrypt Here
                if (OldPassword == OldPasswordgenerated)
                {
                    ResponseInfo si = WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "ChangePassword", "Home", cPassword);
                    TempData["Success"] = "Password updated successfully";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Please enter valid old password.");
                    return View(cPassword);
                }

            }
            else
            {
                return View();
            }
        }
    }
}