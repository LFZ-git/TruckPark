using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Account
{
    public class TokenModel
    {
        public int udid { get; set; }
        public string JwtToken { get; set; }
        public string LoginToken { get; set; }
        public string EmailId { get; set; }

    }
}
