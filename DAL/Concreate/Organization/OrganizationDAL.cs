using DAL.Interface;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concreate
{
    public class OrganizationDAL: BaseClassDAL, IOrganizationDAL
    {
        LFZ_TruckParkEntities entities = new LFZ_TruckParkEntities();

        public ResponseInfo AddOrganization(OrganizationModel model)
        {
            ResponseInfo respInfo = new ResponseInfo();
            System.Data.Entity.Core.Objects.ObjectParameter OutputParam = new System.Data.Entity.Core.Objects.ObjectParameter("OutError", typeof(string));
            var result = entities.Organization_CRUD(1, model.CompanyName, model.CompanyShortName, model.OrganizationTypeId, model.OrganizationID, "", OutputParam);

            respInfo.ID = model.OrganizationID;
            respInfo.Status = "";
            respInfo.IsSuccess = true;
            respInfo.Msg = OutputParam.Value.ToString();

            return respInfo;
        }

        public ResponseInfo UpdateOrganization(OrganizationModel model)
        {
            ResponseInfo respInfo = new ResponseInfo();
            System.Data.Entity.Core.Objects.ObjectParameter OutputParam = new System.Data.Entity.Core.Objects.ObjectParameter("OutError", typeof(string));
            var result = entities.Organization_CRUD(2, model.CompanyName, model.CompanyShortName, model.OrganizationTypeId, model.OrganizationID, "", OutputParam);

            respInfo.ID = model.OrganizationID;
            respInfo.Status = "";
            respInfo.IsSuccess = true;
            respInfo.Msg = OutputParam.Value.ToString();

            return respInfo;
        }

        public ResponseInfo DeleteOrganization(OrganizationModel model, string IdList)
        {
            ResponseInfo respInfo = new ResponseInfo();
            System.Data.Entity.Core.Objects.ObjectParameter OutputParam = new System.Data.Entity.Core.Objects.ObjectParameter("OutError", typeof(string));
            var result = entities.Organization_CRUD(3, model.CompanyName, model.CompanyShortName, model.OrganizationTypeId, model.OrganizationID, IdList, OutputParam);

            respInfo.ID = model.OrganizationID;
            respInfo.Status = "";
            respInfo.IsSuccess = true;
            respInfo.Msg = OutputParam.Value.ToString();

            return respInfo;
        }

        public IList<OrganizationModel> GetOrganizationList()
        {
            var result = entities.Organization_G(null).ToList();
            IList<OrganizationModel> list = Mapping<IList<OrganizationModel>>(result);
            return list;
        }

        public OrganizationModel GetOrganizationDetails(int OrganizationId)
        {
            var result = entities.Organization_G(OrganizationId).FirstOrDefault();
            OrganizationModel model = Mapping<OrganizationModel>(result);
            return model;
        }

        public IList<OrganizationModel> GetOrganizationGroupList(int UDID)
        {
            var result = entities.OrganizationEntToEnt_G(UDID).ToList();
            IList<OrganizationModel> list = Mapping<IList<OrganizationModel>>(result);
            return list;
        }
    }
}
