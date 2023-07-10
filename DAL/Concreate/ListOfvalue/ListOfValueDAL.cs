using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using Model.Models.ListOfValue;

namespace DAL.Concreate
{
    public class ListOfValueDAL:BaseClassDAL, IListOfValueDAL
    {
        // IListOfValueDAL _iListOfvalueDAL;

        LFZ_TruckParkEntities entities = new LFZ_TruckParkEntities();
        public List<ListOfValueModel> GetListOfValue(int id)
        {
            var result = entities.M_LOV_G(id).ToList();
            List<ListOfValueModel> lstListOfvalue = Mapping<List<ListOfValueModel>>(result);
            return lstListOfvalue;
        }
    }
}
