using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.ECallUp.Request
{
    public class ECallupTruckStatusUpdateReqModel
    {
        [JsonProperty("plate_number")]
        public string PlateNumber { get; set; }
    
        [JsonProperty("status")]
        public string Status { get; set; }  
    }
}
