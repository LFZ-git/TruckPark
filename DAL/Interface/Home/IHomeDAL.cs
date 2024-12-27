using Model.Models;
using Model.Models.Account;
using Model.Models.UserDetail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IHomeDAL
    {
        //ResponseInfo SaveRegistrationDAL (RegistrationModel model);

        ResponseInfo CheckLoginDAL(UserDetailModel model);
        ResponseInfo ChangePassword(ChangePassword model);

        DashboardModel Dashboard(int RoleId, int UserID, int? orgId);
        ValididateUser_OnePortal ValidUser(ValididateUser_OnePortal Email);

    }
}
