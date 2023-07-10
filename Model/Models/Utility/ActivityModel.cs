using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ActivityModel
    {
        public long ActivityLog { get; set; }

        public string ActivityType { get; set; }

        public int UDID { get; set; }

        public DateTime Activitydate { get; set; }

        public int ModuleId { get; set; }

        public int RoleId { get; set; }

        public int ZoneId { get; set; }

        public int CityId { get; set; }

        public string Remarks { get; set; }
    }
}
