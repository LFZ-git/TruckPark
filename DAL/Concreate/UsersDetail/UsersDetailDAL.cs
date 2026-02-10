using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using Model.Models.UserDetail;

namespace DAL.Concreate
{
    public class UsersDetailDAL:BaseClassDAL, IUsersDetailDAL
    {

        LFZ_TruckPark_NewEntities entities = new LFZ_TruckPark_NewEntities();

        public List<UserDetailModel> GetUsersDetails()
        {
            //  var result = entities.M_UserDetails_G().ToList();
            // List<UserDetailModel> lstUsersDetail = Mapping<List<UserDetailModel>>(result);
            //// List<UserDetailModel> lstUsersDetail = new List<UserDetailModel>();
            // return lstUsersDetail;
            var result = entities.M_UserDetails_G().ToList();
            List<UserDetailModel> lstUsersDetail = Mapping<List<UserDetailModel>>(result);
            return lstUsersDetail;
        }
    }
}
