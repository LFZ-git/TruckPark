using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.UserCreation
{
    public class GetUserCreationModel
    {
        public int UDID { get; set; }

        public string EmployeeName { get; set; }

        public string EmailId { get; set; }

        public string MobileNo { get; set; }

        public string Department { get; set; }

        public string ReportingTo { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        //public int CreatedBy { get; set; }

        //public DateTime CreatedDate { get; set; }

        //public int UpdatedBy { get; set; }

        //public DateTime UpdatedDate { get; set; }

        //public bool IsActive { get; set; }
    }
}

