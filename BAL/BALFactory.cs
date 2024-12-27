using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using BAL.Concreate;
using DAL.Interface;
using DAL.Concreate;
using DAL;
using DAL.Interface.UserCreation;
using DAL.Concreate.UserCreation;
using DAL.Interface.Ext;
using DAL.Concreate.Ext;

namespace BAL
{
    public static class BALFactory
    {
        public static IUsersDAL GetUsersDAL()
        {
            return new UsersDAL();
        }

        public static IHomeDAL GetHomeInstance()
        {
            return new HomeDAL();
        }

        public static IUsersDetailDAL GetUserDetailInstance()
        {
            return new UsersDetailDAL();
        }

      
        public static IUtilityDAL GetUtilityInstance()
        {
            return new UtilityDAL();
        }

        public static IMenuDAL GetMenuObject()
        {
            return new MenuDAL();
        }

        public static IListOfValueDAL GetListOfValueInstance()
        {
            return new ListOfValueDAL();
        }
        public static IRoleDAL GetRoleInstance()
        {
            return new RoleDAL();
        }
        public static ICountryDAL GetCountryInstance()
        {
            return new CountryDAL();
        }
        public static IUserCreationDAL GetUserCreationDALInstance()
        {
            return new UserCreationDAL();
        }

        public static IOrganizationDAL GetOrganizationInstance()
        {
            return new OrganizationDAL();
        }

        public static IMasterDAL GetMasterInstance()
        {
            return new MasterDAL();
        }

        public static ITruckDAL GetTruckInstance()
        {
            return new TruckDAL();
        }
        public static IInvoiceDAL GetInvoiceInstance()
        {
            return new InvoiceDAL();
        }

        public static IExtDAL GetExtInstance()
        {
            return new ExtDAL();
        }

    }
}
