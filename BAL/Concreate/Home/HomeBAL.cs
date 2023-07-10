using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using DAL.Interface;
using Model.Models;

using Model.Models.UserDetail;
using Model.Models.Account;
using System.Data;

namespace BAL.Concreate
{
    public class HomeBAL:IHomeBAL
    {
        private IHomeDAL _iHomeDAL;

        public HomeBAL()
        {
            _iHomeDAL = BALFactory.GetHomeInstance();
        }

        //public ResponseInfo SaveRegistrationBAL(RegistrationModel model)
        //{
        //    return _iHomeDAL.SaveRegistrationDAL(model);
        //}

        public ResponseInfo CheckLoginBAL(UserDetailModel model)
        {
            return _iHomeDAL.CheckLoginDAL(model);
        }

        public ResponseInfo ChangePassword(ChangePassword model)
        {
            return _iHomeDAL.ChangePassword(model);
        }

        public DashboardModel Dashboard(int RoleId, int UserID, int? orgId)
        {
            return _iHomeDAL.Dashboard(RoleId, UserID, orgId);
        }

        public ValididateUser_OnePortal ValidUserBAL(ValididateUser_OnePortal Email)
        {
            return _iHomeDAL.ValidUser(Email);
        }
    }
}
