using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Interface;
using Model;
using Model.Models.Menu;

namespace DAL.Concreate
{
    public class MenuDAL : IMenuDAL
    {
        LFZ_TruckParkEntities entities = new LFZ_TruckParkEntities();
        public List<Model.Models.Menu.M_Modules> getMenu(string userId)
        {
            int roleId = Convert.ToInt32(userId);
            var LstModule = entities.GetMenusBasedRoleById_G(roleId).ToList();

            List<Model.Models.Menu.M_Modules> lstMenu = LstModule.Select(m => new Model.Models.Menu.M_Modules
            {
                Id = m.ModuleId,
                ModuleName = m.ModuleName,
                Parentid = Convert.ToInt32(m.Parentid == null ? default(int) : m.Parentid),
                ControllerName = m.ControllerName,
                ActionName = m.ActionName,
                DisplayOrder = (int)m.DisplayOrder,
                IconImage = m.IconImage,
                IsActive = m.IsActive,
                RoleIds = m.RoleIds
            }).ToList();
            return lstMenu;
        }
    }
    
}
