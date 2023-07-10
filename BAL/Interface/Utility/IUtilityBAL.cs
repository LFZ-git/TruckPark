using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;

namespace BAL.Interface
{
    public interface IUtilityBAL
    {
        void LogError(ErrorLogModel logModel);

        void ActivityLog(ActivityModel activityModel);
    }
}
