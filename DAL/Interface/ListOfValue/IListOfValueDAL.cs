using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models.ListOfValue;

namespace DAL.Interface
{
    public interface IListOfValueDAL
    {
        List<ListOfValueModel> GetListOfValue(int id);
    }
}
