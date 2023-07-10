using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IMasterBAL
    {
        IList<M_TruckCapacity> GetTruckCapacity();
    }
}
