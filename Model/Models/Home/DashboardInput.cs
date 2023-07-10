using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class DashboardInput
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
    }
}
