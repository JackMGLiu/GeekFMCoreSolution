using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Geek.OAuthServer.Models
{
    public class GeekValidateToken : ISecurityTokenValidator
    {
        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; }

        public bool CanReadToken(string securityToken)
        {
            return true;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            validatedToken = null;

            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);

            if (securityToken == "geekToken")
            {
                identity.AddClaim(new Claim("name", "geekfm"));
                identity.AddClaim(new Claim("admin", "true"));
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"));
            }

            var principal = new ClaimsPrincipal(identity);

            return principal;
        }
    }
}
