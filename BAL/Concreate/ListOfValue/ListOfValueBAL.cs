using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using DAL.Interface;
using Model.Models.ListOfValue;

namespace BAL.Concreate
{
    public class ListOfValueBAL : IListOfValueBAL
    {
        private IListOfValueDAL _iListOfValue;

        public ListOfValueBAL()
        {
            _iListOfValue = BALFactory.GetListOfValueInstance();
        }

        public List<ListOfValueModel> GetListOFValueDetail(int id)
        {
            return _iListOfValue.GetListOfValue(id);
        }
    }
}
