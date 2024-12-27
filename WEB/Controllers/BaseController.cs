using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.Filters;
using WEB.APIHelper;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Model;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Configuration;

namespace WEB.Controllers
{

    [WEBException]
    [ActivityLoging]
    public class BaseController : Controller
    {
        private string baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"]; //"http://localhost:51219/";
        public async Task<T> CallApi<T>(string Method, string url) where T : class
        {
            HttpResponseMessage response = null;
            if (Method == "POST")
            {
                var json = JsonConvert.SerializeObject(typeof(T));
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                var httpClient = GetHttpClient();
                response = await httpClient.PostAsync(baseUrl + "api/" + url, stringContent);


            }
            if (Method == "GET")
            {
                var httpClient = GetHttpClient();
                response = httpClient.GetAsync(baseUrl + "api" + url).Result;
            }
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                JsonSerializerSettings config = new JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore };
                T result = JsonConvert.DeserializeObject<T>(json,config);
                return result;
            }
            else
            {
                throw new Exception("Problem while deserilization");
#pragma warning disable CS0162 // Unreachable code detected
                return null;
#pragma warning restore CS0162 // Unreachable code detected
            }
        }

        public HttpClient GetHttpClient()
        {
            var MyHttpClient = new HttpClient();
            dynamic _token = HttpContext.Session["token"];
            if (_token == null) throw new ArgumentNullException(nameof(_token));
            MyHttpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", _token));
            return MyHttpClient;
        }

        public async Task<string> GetToken(Login login)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                // We want the response to be JSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Build up the data to POST.
                List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("grant_type", "password"));
                postData.Add(new KeyValuePair<string, string>("username", login.UserId));
                postData.Add(new KeyValuePair<string, string>("password", login.Password));

                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);

                // Post to the Server and parse the response.
                HttpResponseMessage response = await client.PostAsync("Token", content);
                string jsonString = await response.Content.ReadAsStringAsync();
                object responseData = JsonConvert.DeserializeObject(jsonString);
                Session["token"] = ((dynamic)responseData).access_token;

                // return the Access Token.
                return ((dynamic)responseData).access_token;

            }
        }


        public async Task<T> JsonToObject<T>(HttpResponseMessage resp, T className) where T : class
        {
            string json = string.Empty;
            if (resp.IsSuccessStatusCode)
            {
                json = await resp.Content.ReadAsStringAsync();
            }
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}