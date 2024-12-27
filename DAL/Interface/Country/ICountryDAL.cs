using Model.Models.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ICountryDAL
    {
        List<M_CountryModel> GetCountry();
    }
}
