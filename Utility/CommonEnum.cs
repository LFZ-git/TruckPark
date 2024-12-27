using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class CommonEnum
    {
        public enum QuaterType
        {
            CalendarYear = 1,
            FinancialYear = 2

        }

        public enum EmailType
        {
            Register = 1,
            Activation = 2,
            ForgotPwd = 3,
            OTP = 4

        }

        public enum LOVId
        {
            DepartmentType = 1,
            UploadFileType = 3,
            MonthName = 10,

            CaseType = 10,
            ReportedBy = 23,
            Others=27,
            UploadType = 19,
            Sketch = 20,
            Statement = 21,
            Photographs = 22,

            IncidentStatus = 28,
            Saved = 29,
            PendingforApproval = 30,
            Approved = 31,
            Rejected = 32,
            ResubmittedforApproval = 33,
            Closed = 34,
        }


        public enum Role
        {
            LFZAdmin = 1,
            EnterpriseAdmin = 2,
            EnterpriseGroupAdmin = 3,
            LFZSecurity=4
            //ViewOnlyLFZ =5,
        }

    }
}
