using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using BAL.Concreate;
using BAL.Interface;
using Model.User;
using System;

namespace API.TokenManagment
{
    public class ADAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "MYDOMAIN"))
            //{
            //    // validate the credentials
            //    bool isValid = pc.ValidateCredentials(context.UserName, context.Password);

            //    if (!isValid)
            //    {
            //        context.SetError("invalid_grant", "The user name or password is incorrect.");
            //        return;
            //    }
            //}
            UsersBAL user = new UsersBAL();
            UserInfo userInfo = user.GetUserRole(context.UserName);

            if (userInfo == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim(ClaimTypes.Role, userInfo.Role));
            // identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userInfo.EmailId));
            identity.AddClaim(new Claim(ClaimTypes.Name, Convert.ToString(userInfo.Id)));
            identity.AddClaim(new Claim(ClaimTypes.Sid, Convert.ToString(userInfo.RoleId)));

            context.Validated(identity);
        }
    }
}