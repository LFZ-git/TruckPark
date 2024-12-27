using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using DAL.Interface;
using Model.Models;

namespace BAL.Concreate
{
    public class MasterBAL: IMasterBAL
    {
        private IMasterDAL _iMasterDal;
        public MasterBAL()
        {
            _iMasterDal = BALFactory.GetMasterInstance();
        }

        public IList<M_TruckCapacity> GetTruckCapacity()
        {
            return _iMasterDal.GetTruckCapacity();
        }
    }
}
