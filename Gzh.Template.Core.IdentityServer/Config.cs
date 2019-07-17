using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Gzh.Template.Core.IdentityServer
{
    public class Config
    {
        // 1.配置资源
        public static IEnumerable<ApiResource> GetApiResource()
        {
            return new List<ApiResource>
            {
                new ApiResource ("api1", "my Api"),
                new ApiResource("api3","API"),
                new ApiResource("identityServerApi","identityServerApi,used to get token")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                // new IdentityResources.Phone(),
                //  new IdentityResources.Email(),
                //  new IdentityResources.Address()
            };
        }

        // 2.配置允许验证的client
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
              new Client () {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret ("secret".Sha256 ()) },
                AllowedScopes = { "api1" },
                RedirectUris = { "http:localhost:5001" }
              },
              new Client(){
                ClientId = "pwdclient",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedScopes = {"api1"},
                //RedirectUris = {"http:localhost:5001"}
              },
              new Client(){
                ClientId = "api3",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedScopes = {"api3"},
                //RedirectUris = {"http:localhost:5001"}
              },
              new Client(){
                ClientId = "mvc",
                ClientName = "MvcClient",
                //AllowedGrantTypes = GrantTypes.Implicit,
                AllowedGrantTypes = GrantTypes.Hybrid,

                ClientSecrets = {
                    new Secret("secret".Sha256())
                },

                // 登录成功回调处理地址，处理回调返回的数据
                RedirectUris = { "http://localhost:5002/signin-oidc" },
                // where to redirect to after logout
                PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                //ClientSecrets = {new Secret("secret".Sha256())},

                // AllowedScopes = new List<string>
                // {
                //     IdentityServerConstants.StandardScopes.OpenId,
                //     IdentityServerConstants.StandardScopes.Profile,
                //     // IdentityServerConstants.StandardScopes.Phone,
                //     //  IdentityServerConstants.StandardScopes.Email,
                //     //  IdentityServerConstants.StandardScopes.Address,
                //     // IdentityServerConstants.StandardScopes.OfflineAccess,

                // },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1"
                },
                AllowOfflineAccess = true  //允许刷新令牌

              },

              // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =           { "http://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:5003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                },
            
                //identityServer自身这个client
                new Client(){
                ClientId = "identityServerApi",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedScopes = {"identityServerApi"},
                //RedirectUris = {"http:localhost:5001"}
              },
    };
        }
    }
}
