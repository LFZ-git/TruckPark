using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ResponseInfoAPI
    {
        public int APIKeyId { get; set; }
        public string APIKey { get; set; }
        public string PayLoad { get; set; }
        public string SourceIP { get; set; }
        public string PortalName { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string ErrorMssg { get; set; }
        public string Event { get; set; }
        public string UserAgent { get; set; }
        public bool IsActive { get; set; }
    }
}
