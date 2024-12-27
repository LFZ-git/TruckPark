using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using Model.Models.UserDetail;
using DAL.Interface;
using Model.Models.Account;

namespace BAL.Concreate
{
    public class UsersBAL : IUsersBAL
    {
        IUsersDAL iuserDal;
        public UsersBAL()
        {
            iuserDal = BALFactory.GetUsersDAL();
        }

       
        //public Model.Models.UserDetail.UserDetailModel SaveUsersDetails(Model.Models.UserDetail.UserDetailModel users)
        //{
        //    return iuserDal.SaveUsersDetails(users);
        //}

        public Model.User.UserInfo GetUserRole(string userid)
        {
            return iuserDal.GetUserRole(userid);
        }

        public Model.Models.UserDetail.UserDetailModel GetUserDetails(int id)
        {
            return iuserDal.GetUserDetails(id);
        }

        public Model.Models.UserDetail.UserDetailModel GetUserDetailsOne(ValididateUser_OnePortal model)
        {
            return iuserDal.GetUserDetailsOne(model);
        }
    }
}
