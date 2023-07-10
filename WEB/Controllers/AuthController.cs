using Model.Models;
using Model.Models.Account;
using Model.Models.UserDetail;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Utility;
using WEB.APIHelper;
using WEB.JWT;

namespace WEB.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult ValidUser(string Mail)
        {
            ValididateUser_OnePortal objSend2 = new ValididateUser_OnePortal();
            objSend2.EmailID = Mail;
            TokenModel objAPIOne = new TokenModel();
            objAPIOne = callAPI(Mail);
            string a = objAPIOne.JwtToken;
            Session["token"] = objAPIOne.LoginToken;
            ValididateUser_OnePortal obj = new ValididateUser_OnePortal();
            obj.EmailID = Mail;
            ValididateUser_OnePortal Info = WebAPIHelper.CallApi<ValididateUser_OnePortal>(HttpMethods.Post, "CheckValidUser", "Home", obj);
            JWTToken objToken = new JWTToken();
            string JWTTokens = objToken.GetToken(Info.EmailID, Info.Udid);
            HttpCookie cookie = new HttpCookie("Token", JWTTokens);
            HttpContext.Response.SetCookie(cookie);
            ValididateUser_OnePortal objSend = new ValididateUser_OnePortal();
            objSend.EmailID = cookie.Value;
            ResponseInfo objAPI = WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "CheckLFZUser", "Home", objSend);
            if (objAPI.IsSuccess == true)
            {
                ValididateUser_OnePortal Info1 = WebAPIHelper.CallApi<ValididateUser_OnePortal>(HttpMethods.Post, "CheckValidUser", "Home", obj);
                ViewBag.MailId = Mail;
                ViewBag.Token = objAPIOne.JwtToken;
                if (!string.IsNullOrEmpty(Info1.EmailID))
                {
                    UserDetailModel userdetail = WebAPIHelper.CallApi<UserDetailModel>(HttpMethods.Post, "GetUserDetailsOne", "Account", Info1);
                    Session["UserDetails"] = userdetail;
                    if (Session["count"] == null)
                        Session["count"] = "0";

                    return RedirectToAction("Dashboard", "Home", new { area = "" });

                }
                return View();
            }
            return RedirectToAction("LogOut", "Account");
        }

        public TokenModel callAPI(string Email)
        {
            string html = string.Empty;
            string url = ConfigurationManager.AppSettings["JWTOnePortal"] + Email + "";

            TokenModel deserializedProduct = new TokenModel();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                deserializedProduct = JsonConvert.DeserializeObject<TokenModel>(reader.ReadToEnd());
                html = reader.ReadToEnd();
            }

            return deserializedProduct;
        }
    }
}