using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using DAL.Interface;

namespace BAL.Concreate
{
    public class Menu : IMenu
    {
        IMenuDAL imenu;
        public Menu()
        {
            imenu = BALFactory.GetMenuObject();
        }
        public List<Model.Models.Menu.M_Modules> getMenu(string Role)
        {
            return imenu.getMenu(Role);
        }
    }
}
