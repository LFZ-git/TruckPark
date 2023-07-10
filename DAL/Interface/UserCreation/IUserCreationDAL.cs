using Model.Models;
using Model.Models.UserCreation;
using Model.Models.UserDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.UserCreation
{
    public interface IUserCreationDAL
    {
        ResponseInfo SaveUserDAL(UserCreationModel model);

        List<GetUserCreationModel> GetUserListDAL();

        GetUserEditDetailsModel GetUserDetailsDAL(int id);

        ResponseInfo DeleteUserDetailsDAL(int id);

    }
}
