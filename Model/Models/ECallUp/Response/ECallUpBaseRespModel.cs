using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.ECallUp.Response
{
    public class ECallUpBaseRespModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("code")]
        public int StatusCode { get; set; }
    }
}
