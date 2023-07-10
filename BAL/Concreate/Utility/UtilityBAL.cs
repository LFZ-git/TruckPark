using BAL.Interface;
using DAL.Interface;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Concreate
{
    public class UtilityBAL : IUtilityBAL
    {
        private IUtilityDAL _iUtilityDAL;
        public UtilityBAL()
        {
            _iUtilityDAL = BALFactory.GetUtilityInstance();
        }

        public void LogError(ErrorLogModel logModel)
        {
            _iUtilityDAL.LogError(logModel);
        }

        public void ActivityLog(ActivityModel activityModel)
        {
            _iUtilityDAL.ActivityLog(activityModel);
        }
    }
}
