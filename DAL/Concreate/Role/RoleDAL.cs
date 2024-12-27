using DAL.Interface;
using Model.Models;
using Model.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concreate
{
    public class RoleDAL: BaseClassDAL, IRoleDAL
    {
        LFZ_TruckParkEntities entities = new LFZ_TruckParkEntities();

        public List<RoleModel> GetRole()
        {
            //var result = entities.M_Role.Where(m => m.IsActive == true).ToList();
            var result = entities.Role_G().ToList();
            List<RoleModel> lstRole = Mapping<List<RoleModel>>(result);
            return lstRole;
        }      

    }
}
