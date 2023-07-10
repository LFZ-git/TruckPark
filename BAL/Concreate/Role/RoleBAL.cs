using BAL.Interface;
using DAL.Interface;
using Model.Models;
using Model.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Concreate
{
    public class RoleBAL:IRoleBAL
    {
        private IRoleDAL _iRoleDAL;

        public RoleBAL()
        {
            _iRoleDAL = BALFactory.GetRoleInstance();
        }

        public List<RoleModel> GetRole()
        {
            return _iRoleDAL.GetRole();
        }

    }
}
