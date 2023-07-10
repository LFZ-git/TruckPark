using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.User;
using Model.Models.UserDetail;
using Model.Models.Account;

namespace BAL.Interface
{
    public interface IUsersBAL
    {
      //  UserDetailModel SaveUsersDetails(UserDetailModel users);

      UserInfo GetUserRole(string userId);


        UserDetailModel GetUserDetails(int id);
        UserDetailModel GetUserDetailsOne(ValididateUser_OnePortal Email);

    }
}
