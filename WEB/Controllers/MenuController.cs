using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Threading.Tasks;
using System.Net.Http;
using Model.Models.Menu;
using WEB.Controllers;

namespace WEB.Controllers
{
    public class MenuController : BaseController
    {

        [HttpGet]
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<ActionResult> GetMenu(int? parentId = 0)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Session["count"] = "0";
       
            List<M_Modules> menus = (List<M_Modules>)Session["Menu"];
            List<M_Modules> menus1 = menus.Where(m => m.Parentid == parentId).ToList();
            List<M_Modules> menus2 = new List<M_Modules>();
            foreach (var item in menus1)
            {

                menus2.Add(new M_Modules { Id = item.Id, ModuleName = item.ModuleName, ActionName = item.ActionName, ControllerName = item.ControllerName, Parentid = item.Parentid, DisplayOrder = item.DisplayOrder, IconImage = item.IconImage, IsActive = item.IsActive });
                List<M_Modules> obj = menus.Where(m => m.Parentid == item.Id).ToList();
                foreach (var item1 in obj)
                {
                    menus2.Add(new M_Modules { Id = item1.Id, ModuleName = item1.ModuleName, ActionName = item1.ActionName, ControllerName = item1.ControllerName, Parentid = item1.Parentid, DisplayOrder = item.DisplayOrder, IconImage = item.IconImage, IsActive = item.IsActive });
                }

            }
            return PartialView("~/Views/Shared/Menu.cshtml", menus2);
        }
    }
}