using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using Model.Models;

namespace DAL.Concreate
{
    public class MasterDAL: BaseClassDAL, IMasterDAL
    {
        LFZ_TruckParkEntities entities = new LFZ_TruckParkEntities();

        public IList<Model.Models.M_TruckCapacity> GetTruckCapacity()
        {
            var result = entities.M_TruckCapacity.ToList();
            IList<Model.Models.M_TruckCapacity> list = Mapping<IList<Model.Models.M_TruckCapacity>>(result);
            return list;
        }
    }
}
