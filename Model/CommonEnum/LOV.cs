using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.CommonEnum
{
    public static class LOV
    {
        public  enum LOVId
        {
            DepartmentType = 1,
            TransferType =10,
            MaterialType=21
        }

        public enum Role
        {
            LFZAdmin = 1,
            EnterpriseAdmin = 2,
            EnterpriseGroupAdmin = 3,
            LFZSecurity = 4
        }

        public enum SqlOperations
        {
            INSERT=1,
            UPDATE=2,
            SELECT=3,
            DELETE=4,            
        }

        public enum TruckOperations
        {
            CHECKOUT=1,
            CALLEDOUT = 2,
            CHECKIN=3
        }
    }
}