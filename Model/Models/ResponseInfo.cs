using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ResponseInfo
    {
        public int? ID { get; set; }

        public string Status { get; set; }

        public string Msg { get; set; }

        public bool IsSuccess { get; set; }
    }
}
