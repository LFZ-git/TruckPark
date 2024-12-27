using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models.UserDetail;

namespace DAL.Interface
{
    public interface IUsersDetailDAL
    {
        List<UserDetailModel> GetUsersDetails();
       
    }
}
