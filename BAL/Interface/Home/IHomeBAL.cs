using Model.Models;
using Model.Models.Account;
using Model.Models.UserDetail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IHomeBAL
    {
       // ResponseInfo SaveRegistrationBAL(RegistrationModel model);

        ResponseInfo CheckLoginBAL(UserDetailModel model);

        ResponseInfo ChangePassword(ChangePassword model);

        DashboardModel Dashboard(int RoleId, int UserID, int? orgId);

        ValididateUser_OnePortal ValidUserBAL(ValididateUser_OnePortal Email);

    }
}
