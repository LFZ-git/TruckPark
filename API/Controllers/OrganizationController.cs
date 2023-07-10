using BAL.Interface;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace API.Controllers
{
    public class OrganizationController : ApiController
    {
        private IOrganizationBAL _iOrganizationBAL;
        public OrganizationController()
        {
            _iOrganizationBAL = ServiceFactory.GetOrganizationInstance();
        }

        [HttpPost]
        public IHttpActionResult AddOrganization(OrganizationModel model)
        {
            return Ok(_iOrganizationBAL.AddOrganization(model));
        }

        [HttpPost]
        public IHttpActionResult UpdateOrganization(OrganizationModel model)
        {
            return Ok(_iOrganizationBAL.UpdateOrganization(model));
        }

        [HttpPost]
        public IHttpActionResult DeleteOrganization(OrganizationModel model, string strinhObj)
        {
            return Ok(_iOrganizationBAL.DeleteOrganization(model, strinhObj));
        }

        [HttpGet]
        public IHttpActionResult GetOrganizationList()
        {
            return Ok(_iOrganizationBAL.GetOrganizationList());
        }

        [HttpGet]
        public IHttpActionResult GetOrganizationDetails(int Id)
        {
            return Ok(_iOrganizationBAL.GetOrganizationDetails(Id));
        }

        [HttpGet]
        public IHttpActionResult GetOrganizationGroupList(int UserID)
        {
            return Ok(_iOrganizationBAL.GetOrganizationGroupList(UserID));
        }
    }
}