using DAL;
using Model.Models;
using Model.Models.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface.Ext
{
    public interface IExtBAL
    {
        ResponseInfoAPI CheckApiKey(string apiKey);
        void ReceivedLog(ResponseInfoAPI model);
        void SendLog(ResponseInfoAPI model);
        ResponseInfo AddTruckParkData(EcMainModel model);
        TruckDetailAPI GetTruckDetails(long truckDetailId);
    }
}
