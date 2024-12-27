using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concreate
{
    public class BaseClassDAL
    {
        public static T Mapping<T>(dynamic model) where T : class 
        {
            var config = new MapperConfiguration(cfg => { cfg.ValidateInlineMaps = false; });
            var mapper = config.CreateMapper();
            T st = mapper.Map<T>(model);
            return st;
        }
    }
}
