using BAL.Interface;
using DAL.Interface;
using Model.Models.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Concreate.Country
{
    public class CountryBAL : ICountryBAL
    {
        private ICountryDAL _iCountryDAL;

        public CountryBAL()
        {
            _iCountryDAL = BALFactory.GetCountryInstance();
        }

        public List<M_CountryModel> GetCountry()
        {
            return _iCountryDAL.GetCountry();
        }
    }
}

