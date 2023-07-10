using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BAL.Interface
{
    public interface IMenu
    {
        List<Model.Models.Menu.M_Modules> getMenu(string role);
    }
}
