using Model.Models;
using Model.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IRoleDAL
    {
        List<RoleModel> GetRole();

    }
}
