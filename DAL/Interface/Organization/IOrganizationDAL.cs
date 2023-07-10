using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IOrganizationDAL
    {
        ResponseInfo AddOrganization(OrganizationModel model);

        ResponseInfo UpdateOrganization(OrganizationModel model);

        ResponseInfo DeleteOrganization(OrganizationModel model, string IdList);

        IList<OrganizationModel> GetOrganizationList();

        OrganizationModel GetOrganizationDetails(int OrganizationId);

        IList<OrganizationModel> GetOrganizationGroupList(int UDID);
    }
}
