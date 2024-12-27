using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models.UserDetail;
using Model.Models.Account;

namespace DAL.Interface
{
    public interface IUsersDAL
    {
       // Model.Models.UserDetail.UserDetailModel SaveUsersDetails(Model.Models.UserDetail.UserDetailModel users);

       Model.User.UserInfo GetUserRole(string userId);


        Model.Models.UserDetail.UserDetailModel GetUserDetails(int id);
        Model.Models.UserDetail.UserDetailModel GetUserDetailsOne(ValididateUser_OnePortal Model);

    }
}
