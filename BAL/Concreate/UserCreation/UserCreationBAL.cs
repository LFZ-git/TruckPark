
using BAL.Interface;
using DAL.Interface.UserCreation;
using Model.Models;
using Model.Models.UserCreation;
using Model.Models.UserDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Concreate.UserCreation
{
    public class UserCreationBAL : IUserCreationBAL
    {
        private IUserCreationDAL _iUserCreationDAL;

        public UserCreationBAL()
        {
            _iUserCreationDAL = BALFactory.GetUserCreationDALInstance();
        }

        public ResponseInfo SaveUserBAL(UserCreationModel model)
        {
            return _iUserCreationDAL.SaveUserDAL(model);
        }

        public List<GetUserCreationModel> GetUserListBAL()
        {
            return _iUserCreationDAL.GetUserListDAL();
        }
        public GetUserEditDetailsModel GetUserDetailsBAL(int id)
        {
            return _iUserCreationDAL.GetUserDetailsDAL(id);
        }

        public ResponseInfo DeleteUserDetailsBAL(int id)
        {
            return _iUserCreationDAL.DeleteUserDetailsDAL(id);
        }

    }
}
