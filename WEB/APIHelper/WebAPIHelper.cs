using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace WEB.APIHelper
{
    public static class WebAPIHelper
    {
        public static string baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        public static T CallApi<T>(string Method, string Action, string Controller, dynamic obj = null, int? Id = -1, int? UserID = -1, int? RoleID = -1) where T : class

        {
            baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            if (Action == "CheckLFZUser")
            {
                baseUrl = ConfigurationManager.AppSettings["ApiBaseUrlOnePortal"];
            }
            HttpResponseMessage response = null;
            bool istokenReq = true;
            string url = string.Empty;
            if (Id != -1 && UserID != -1 && RoleID != -1)
                url = baseUrl + "api/" + Controller + "/" + Action + "?Id=" + Id + "&UserID=" + UserID + "&RoleID=" + RoleID;
            else if (Id != -1 && UserID != -1)
                url = baseUrl + "api/" + Controller + "/" + Action + "?Id=" + Id + "&UserID=" + UserID;
            else if (Id != -1 && RoleID != -1)
                url = baseUrl + "api/" + Controller + "/" + Action + "?Id=" + Id + "&RoleID=" + RoleID;
            else if (Id != -1)
                url = baseUrl + "api/" + Controller + "/" + Action + "?Id=" + Id;
            //else if (stringObj != "")
            //    url = baseUrl + "api/" + Controller + "/" + Action + "?strinhObj=" + stringObj;
            else
                url = baseUrl + "api/" + Controller + "/" + Action;
            if (Method == "POST")
            {
                if (Action == "SaveUser")
                    istokenReq = false;
                if (Action == "GetPassword")
                    istokenReq = false;

                var httpClient = GetHttpClient(istokenReq);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                string jsonWithLocalTimeZone = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });

                ser.MaxJsonLength = Int32.MaxValue;
                response = httpClient.PostAsync(url, new StringContent(
                      jsonWithLocalTimeZone, Encoding.UTF8, "application/json")).Result;
            }
            if (Method == "GET")
            {
                var httpClient = GetHttpClient(istokenReq);
                response = httpClient.GetAsync(url).Result;

            }
            if (response.IsSuccessStatusCode)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = Int32.MaxValue;
                T result = serializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
            else
            {
                throw new Exception(response.RequestMessage.RequestUri.ToString() + " Reason:- " + response.ReasonPhrase.ToString());
            }
        }
        public static HttpClient GetHttpClient(bool istokenReq)
        {
            var MyHttpClient = new HttpClient();
            if (istokenReq)
            {
                dynamic _token = HttpContext.Current.Session["token"];
                if (_token == null) throw new ArgumentNullException(nameof(_token));
                //return null;
               // throw new ArgumentNullException(nameof(_token));
                MyHttpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", _token));
                MyHttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            }
            return MyHttpClient;
        }

        //   public static T CallApi<T>(string Method, string Action, string Controller, List<T> obj = null, int? Id = -1) where T : class
        //   {
        //       HttpResponseMessage response = null;
        //       string url = string.Empty;
        //       if (Id != -1)
        //           url = baseUrl + "api/" + Controller + "/" + Action + "?Id=" + Id;
        //       else
        //           url = baseUrl + "api/" + Controller + "/" + Action;
        //       if (Method == "POST")
        //       {
        //           var httpClient = GetHttpClient();
        //           response = httpClient.PostAsync(url, new StringContent(
        //new JavaScriptSerializer().Serialize(obj), Encoding.UTF8, "application/json")).Result;
        //       }
        //       if (Method == "GET")
        //       {
        //           var httpClient = GetHttpClient();
        //           response = httpClient.GetAsync(url).Result;

        //       }
        //       if (response.IsSuccessStatusCode)
        //       {
        //           T result = (new JavaScriptSerializer()).Deserialize<T>(response.Content.ReadAsStringAsync().Result);
        //           return result;
        //       }
        //       else
        //       {
        //           throw new Exception("Problem while creating api request");
        //       }
        //   }
    }

}