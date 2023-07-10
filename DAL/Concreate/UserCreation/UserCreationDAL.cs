using DAL.Interface.UserCreation;
using Model.Models;
using Model.Models.UserCreation;
using Model.Models.UserDetail;
using Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concreate.UserCreation
{
    public class UserCreationDAL : BaseClassDAL,IUserCreationDAL
    {
        LFZ_TruckParkEntities entities = new LFZ_TruckParkEntities();
        UserInfo user = new UserInfo();

        public ResponseInfo SaveUserDAL(UserCreationModel model)
        {
            ResponseInfo respInfo = new ResponseInfo();

            if (model.UDID == 0)
            {
                System.Data.Entity.Core.Objects.ObjectParameter OutputParam = new System.Data.Entity.Core.Objects.ObjectParameter("OutError", typeof(string));

                var result = entities.UserCreation_CRUD(model.UDID, model.OrganizationID, model.EmployeeName, model.Password,model.RoleId, model.ReportingToID, model.DepartmentID, model.EmailId,model.MobileNo,model.CreatedBy,model.IsActive, 1, OutputParam,model.saltKey);

                respInfo.ID = model.UDID;
                respInfo.Status = "";
                respInfo.IsSuccess = true;
                respInfo.Msg = OutputParam.Value.ToString();
            }
            else
            {
                System.Data.Entity.Core.Objects.ObjectParameter OutputParam = new System.Data.Entity.Core.Objects.ObjectParameter("OutError", typeof(string));

                var result = entities.UserCreation_CRUD(model.UDID, model.OrganizationID, model.EmployeeName, model.Password, model.RoleId, model.ReportingToID, model.DepartmentID, model.EmailId, model.MobileNo, model.CreatedBy, model.IsActive, 2, OutputParam, model.saltKey);

                respInfo.ID = model.UDID;
                respInfo.Status = "";
                respInfo.IsSuccess = true;
                respInfo.Msg = OutputParam.Value.ToString();

            }

            return respInfo;

        }
        public List<GetUserCreationModel> GetUserListDAL()
        {
            var result = entities.UserCreationList_G().ToList();
            List<GetUserCreationModel> lstUserList = Mapping<List<GetUserCreationModel>>(result);
            return lstUserList;
        }
        public GetUserEditDetailsModel GetUserDetailsDAL(int id)
        {
         
            var result = entities.UserCreation_G(id).FirstOrDefault();
            GetUserEditDetailsModel lstUserList = Mapping<GetUserEditDetailsModel>(result);
            return lstUserList;
        }

        public ResponseInfo DeleteUserDetailsDAL(int id)
        {
            System.Data.Entity.Core.Objects.ObjectParameter OutputParam = new System.Data.Entity.Core.Objects.ObjectParameter("OutError", typeof(string));
            ResponseInfo respInfo = new ResponseInfo();
            var res = entities.UserCreation_D(id, OutputParam);

            respInfo.IsSuccess = true;
            respInfo.Msg = (string)OutputParam.Value;
            return respInfo;
        }
    }

}
