using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class DashboardModel
    {
        public int CheckedInCount { get; set; }
        public int CheckedOutCount { get; set; }
        public int Forecasted { get; set; }
        public IList<StatusOfTruck> StatusOfTruckList { get; set; }
        public IList<MovementOfTruck> MovementOfTruckList { get; set; }

        public int Hours0to4 { get; set; }
        public int Hours4to8 { get; set; }
        public int Hours8to16 { get; set; }
        public int Hours16to24 { get; set; }
        public int Hours24to48 { get; set; }
        public int Hours48to72 { get; set; }
        public int Hours72More { get; set; }

    }

    public class StatusOfTruck
    {
        public string OrganizationShortName { get; set; }
        public string TotalTrucks { get; set; }
        public string FiveDayParkedTrucks { get; set; }
        public string TodayTrucks { get; set; }
        public string Capacity10 { get; set; }
        public string Capacity15 { get; set; }
        public string Capacity20 { get; set; }
        public string Capacity25 { get; set; }
        public string Capacity30 { get; set; }
        public string Capacity40 { get; set; }
        public string Capacity40nMore { get; set; }
    }

    public class MovementOfTruck
    {
        public string OrganizationName { get; set; }
        public string TimePeriod { get; set; }
        public int CheckInCount { get; set; }
        public int CheckOutCount { get; set; }
    }


}
