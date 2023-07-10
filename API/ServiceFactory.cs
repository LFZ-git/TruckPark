using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using BAL;
using BAL.Concreate;
using BAL.Interface;

using BAL.Concreate.Country;
using BAL.Concreate.UserCreation;


namespace API
{
    public static class ServiceFactory
    {
        public static IUsersBAL GetBalObject()
        {
            return new UsersBAL();
        }
        public static IUsersDetailBAL GetUsersDetailInstance()
        {
            return new UsersDetailBAL();
        }
        public static IUtilityBAL GetUtilityInstance()
        {
            return new UtilityBAL();
        }
        public static IHomeBAL GetHomeInstance()
        {
            return new HomeBAL();
        }
        public static IMenu GetMenuObject()
        {
            return new Menu();
        }
      
        public static IListOfValueBAL GetListOfValueInstance()
        {
            return new ListOfValueBAL();
        }

        public static IRoleBAL GetRoleInstance()
        {
            return new RoleBAL();
        }
        public static ICountryBAL GetCountryInstance()
        {
            return new CountryBAL();
        }
        public static IUserCreationBAL GetUserCreationInstance()
        {
            return new UserCreationBAL();
        }
        public static IOrganizationBAL GetOrganizationInstance()
        {
            return new OrganizationBAL();
        }
        public static IMasterBAL GetMasterInstance()
        {
            return new MasterBAL();
        }
        public static ITruckBAL GetTruckInstance()
        {
            return new TruckBAL();
        }
        public static IInvoiceBAL GetInvoiceInstance()
        {
            return new InvoiceBAL();
        }
    }
}