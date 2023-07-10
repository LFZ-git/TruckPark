using BAL.Interface;
using DAL.Interface;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Concreate
{
    public class OrganizationBAL: IOrganizationBAL
    {
        private IOrganizationDAL _iOrganizationDAL;

        public OrganizationBAL()
        {
            _iOrganizationDAL = BALFactory.GetOrganizationInstance();
        }

        public ResponseInfo AddOrganization(OrganizationModel model)
        {
            return _iOrganizationDAL.AddOrganization(model);
        }

        public ResponseInfo UpdateOrganization(OrganizationModel model)
        {
            return _iOrganizationDAL.UpdateOrganization(model);
        }

        public ResponseInfo DeleteOrganization(OrganizationModel model, string IdList)
        {
            return _iOrganizationDAL.DeleteOrganization(model, IdList);
        }

        public IList<OrganizationModel> GetOrganizationList()
        {
            return _iOrganizationDAL.GetOrganizationList();
        }

        public OrganizationModel GetOrganizationDetails(int OrganizationId)
        {
            return _iOrganizationDAL.GetOrganizationDetails(OrganizationId);
        }
        public IList<OrganizationModel> GetOrganizationGroupList(int UDID)
        {
            return _iOrganizationDAL.GetOrganizationGroupList(UDID);
        }
    }
}
