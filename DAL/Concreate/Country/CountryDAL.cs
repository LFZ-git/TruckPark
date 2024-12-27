using DAL.Interface;
using Model.Models.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concreate
{
    public class CountryDAL: BaseClassDAL , ICountryDAL
    {
        //ICountryDAL _iCountryDAL;

        //LFTZ_InvestorPortalEntities entities = new LFTZ_InvestorPortalEntities();
        LFZ_TruckParkEntities entities = new LFZ_TruckParkEntities();

        public List<M_CountryModel> GetCountry()
        {
            var result = entities.M_Country_G().ToList();
            List<M_CountryModel> lstState = Mapping<List<M_CountryModel>>(result);
            return lstState;
        }
    }
}
