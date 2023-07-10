using Model.Models;
using Model.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IRoleBAL
    {
        List<RoleModel> GetRole();

    }
}
