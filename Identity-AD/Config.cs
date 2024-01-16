//// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
//// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

//using IdentityServer4.Models;
//using System.Collections.Generic;

//namespace QuickstartIdentityServer
//{
//    using IdentityServer4;
//    using System;
//    using System.Security.Claims;

//    public class Config
//    {
//        public static IEnumerable<IdentityResource> GetIdentityResources()
//        {
//            return new List<IdentityResource>
//            {
//                new IdentityResources.OpenId(),
//                new IdentityResources.Profile(),
//                new IdentityResources.Email()
//            };
//        }

//        public static IEnumerable<ApiResource> GetApiResources()
//        {
//            return new List<ApiResource>
//            {
//                new ApiResource("dataEventRecordsApi","Data Event Records API",new List<string> { "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin", "dataEventRecords.user" })
//                {
//                    ApiSecrets =
//                    {
//                        new Secret("dataEventRecordsSecret".Sha256())
//                    },
//                    Scopes =
//                    {
//                        "dataEventRecordsScope",
//                    }
//                }
//            };
//        }

//        public static IEnumerable<ApiScope> GetApiScopes()
//        {
//            return new List<ApiScope>
//            {
//                new ApiScope
//                {
//                    Name = "dataEventRecordsScope",
//                    DisplayName = "Scope for the dataEventRecords ApiResource",
//                    UserClaims = { "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin", "dataEventRecords.user" }
//                }
//            };
//        }

//        // clients want to access resources (aka scopes)
//        public static IEnumerable<Client> GetClients()
//        {
//            return new List<Client>
//            {
//                new Client
//                {
//                    ClientId = "resourceownerclient",

//                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
//                    AccessTokenType = AccessTokenType.Jwt,
//                    AccessTokenLifetime = 3600,
//                    IdentityTokenLifetime = 3600,
//                    UpdateAccessTokenClaimsOnRefresh = true,
//                    SlidingRefreshTokenLifetime = 30,
//                    AllowOfflineAccess = true,
//                    RefreshTokenExpiration = TokenExpiration.Absolute,
//                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
//                    AlwaysSendClientClaims = true,
//                    Enabled = true,
//                    ClientSecrets=  new List<Secret> { new Secret("dataEventRecordsSecret".Sha256()) },
//                    AllowedScopes = {
//                        IdentityServerConstants.StandardScopes.OpenId, 
//                        IdentityServerConstants.StandardScopes.Profile,
//                        IdentityServerConstants.StandardScopes.Email,
//                        IdentityServerConstants.StandardScopes.OfflineAccess,
//                        "dataEventRecordsScope"
//                    }
//                }
//            };
//        }
//    }
//}

// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace CustomIdentityServer4
{
    public static class Config
    {



        public static IEnumerable<IdentityResource> IdentityResources =>
    new IdentityResource[] {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Email()
    };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[] { new ApiScope("api1", "My API") };

        public static IEnumerable<Client> Clients =>
            new Client[] {
                new Client {
                    ClientId = "client",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // secret for authentication
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                new Client {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:44305/signin-oidc" },
                    // where to redirect to after logout
                    PostLogoutRedirectUris =
                        { "https://localhost:44305/signout-callback-oidc" },
                    AllowedScopes =
                        new List<string> {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile
                        }
                },
                   new Client {
                    ClientId = "mvc3",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:44353/signin-oidc" },
                    // where to redirect to after logout
                    PostLogoutRedirectUris =
                        { "https://localhost:44353/signout-callback-oidc" },
                    AllowedScopes =
                        new List<string> {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile
                        }
                },
                new Client {
                    ClientId = "angular",
                    ClientName="angular",
                    RequireClientSecret=false,
                   // ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:4201/callback" },
                    // where to redirect to after logout
                    PostLogoutRedirectUris =
                        { "https://localhost:4201/signout-callback-oidc" },
                    AllowedScopes =
                        new List<string> {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.Phone,
                            IdentityServerConstants.StandardScopes.Email                        }
                }
            };

    }
}
