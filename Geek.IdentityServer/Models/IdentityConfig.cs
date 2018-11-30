using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geek.IdentityServer.Models
{
    public class IdentityConfig
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api","Geek Api")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client
            {
                ClientId = "mvc_implicit",
                ClientName = "MVC Client",
                AllowedGrantTypes = GrantTypes.Implicit,                //简化模式
                RequireConsent = false,     //Consent是授权页面，这里我们不进行授权

                RedirectUris = { "http://localhost:8800/signin-oidc" },
                PostLogoutRedirectUris = { "http://localhost:8800/signout-callback-oidc" },

                //授权后可以访问的用户信息（OpenId Connect Scope）与Api（OAuth2.0 Scope）
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email
                    //"api"
                },
                //允许返回Access Token
                AllowAccessTokensViaBrowser = true
            }
        };
    }
}
