using BAL.Interface.Ext;
using DAL.Interface.Ext;
using Model.Models;
using Model.Models.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Concreate.Ext
{
    public class ExtBAL : IExtBAL
    {
        private IExtDAL _iExtDAL;

        public ExtBAL()
        {
            _iExtDAL = BALFactory.GetExtInstance(); 
        }

        public ResponseInfoAPI CheckApiKey(string apiKey)
        {
            return _iExtDAL.CheckApiKey(apiKey);
        }
        public void ReceivedLog(ResponseInfoAPI model)
        {
            _iExtDAL.ReceivedLog(model);
        }
        public void SendLog(ResponseInfoAPI model)
        {
            _iExtDAL.SendLog(model);
        }
        public ResponseInfo AddTruckParkData(EcMainModel model)
        {
            return _iExtDAL.AddTruckParkData(model);
        }
    }
}
