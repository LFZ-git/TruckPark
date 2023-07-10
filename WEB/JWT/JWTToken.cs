using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using WEB.JWT.Managers;
using WEB.JWT.Models;

namespace WEB.JWT
{
    public class JWTToken
    {
        public string GetToken(string EmailId, int UDID)
        {
            IAuthContainerModel model = GetJWTContainerModel(EmailId, UDID);
            IAuthService authService = new JWTService(model.SecretKey);

            string token = authService.GenerateToken(model);

            if (!authService.IsTokenValid(token))
                throw new UnauthorizedAccessException();
            else
            {
                List<Claim> claims = authService.GetTokenClaims(token).ToList();

                //Console.WriteLine(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Name)).Value);
                //Console.WriteLine(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Email)).Value);
            }
            return token;
        }
        #region Private Methods
        private static JWTContainerModel GetJWTContainerModel(string email, int udid)
        {
            return new JWTContainerModel()
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(udid)),
                    new Claim(ClaimTypes.Email, email)
                }
            };
        }


        #endregion
    }
}