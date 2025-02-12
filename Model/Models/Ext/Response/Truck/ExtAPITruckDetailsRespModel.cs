using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Ext.Response.Truck
{
    public class ExtAPITruckDetailsRespModel
    {
        public ExtAPITruckDetailsRespModel(TruckDetailAPI truckDetails)
        {
            TruckDetails = truckDetails;
        }

        public ExtAPIRespModel Response { get; set; } = new ExtAPIRespModel();

        public TruckDetailAPI TruckDetails { get; set; }
    }
}
