using Model.Models;
using Model.Models.UserCreation;
using Model.Models.UserDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IUserCreationBAL

    {
        ResponseInfo SaveUserBAL(UserCreationModel model);

        List<GetUserCreationModel> GetUserListBAL();

        GetUserEditDetailsModel GetUserDetailsBAL(int id);

        ResponseInfo DeleteUserDetailsBAL(int id);
    }
}
