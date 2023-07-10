using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Utility
{
    public static class JsonSerializer
    {
        public static string Serialization(dynamic obj)
        {
            var json = new JavaScriptSerializer().Serialize(obj);
            return json;
        }

        public static T DeSerialization<T>(string json, T classObject) where T : class
        {

            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }
}
