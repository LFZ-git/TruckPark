using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models.UserDetail;

namespace BAL.Interface
{
     public interface IUsersDetailBAL
    {
        List<UserDetailModel> GetUsersDetails();      

    }
}
