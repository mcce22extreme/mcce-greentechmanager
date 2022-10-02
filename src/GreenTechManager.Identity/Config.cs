using IdentityModel;
using IdentityServer4.Models;

namespace GreenTechManager.Identity
{
    internal static class Config
    {
        private const string SCOPE_WINDPARK = "windpark";
        private const string SCOPE_SOLARPARK = "solarpark";
        private const string SCOPE_OPERATOR = "operator";

        private static readonly string[] ClaimTypes = new []
        {
            JwtClaimTypes.Name,
            JwtClaimTypes.Role
        };

        public static IEnumerable<Client> Clients = new List<Client>
        {
            new Client
            {
                ClientId = "greentechclient",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequireClientSecret = false,
                AlwaysIncludeUserClaimsInIdToken = true,
                AlwaysSendClientClaims = true,
                ClientSecrets = new List<Secret> { new Secret("secret") },
                AllowedScopes =
                {
                    SCOPE_WINDPARK,
                    SCOPE_SOLARPARK,
                    SCOPE_OPERATOR
                }
            },
        };

        public static IEnumerable<ApiScope> Scopes = new List<ApiScope>
        {
            new ApiScope(SCOPE_WINDPARK, ClaimTypes),
            new ApiScope(SCOPE_SOLARPARK, ClaimTypes),
            new ApiScope(SCOPE_OPERATOR, ClaimTypes),
        };

        public static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiResource> Apis = new List<ApiResource>
        {
            new ApiResource(SCOPE_WINDPARK, "Windpark API", ClaimTypes)
            {
                Scopes = new []{ SCOPE_WINDPARK }
            },
            new ApiResource(SCOPE_SOLARPARK, "Solarpark API", ClaimTypes)
            {
                Scopes = new []{ SCOPE_SOLARPARK }
            },
        };

        //public static List<TestUser> Users = new List<TestUser>
        //{
        //    new TestUser
        //    {
        //        SubjectId = "19b1d441-3270-4f0e-a453-7bc9d6781258",
        //        Username = "usera",
        //        Password = "usera",
        //        Claims =
        //        {
        //            new Claim(JwtClaimTypes.Name, "usera"),
        //            new Claim(JwtClaimTypes.GivenName, "User")
        //        }
        //    },
        //    new TestUser
        //    {
        //        SubjectId = "18dda0a1-badf-4394-b08f-83217d195611",
        //        Username = "userb",
        //        Password = "userb",
        //        Claims =
        //        {
        //            new Claim(JwtClaimTypes.Name, "userb"),
        //            new Claim(JwtClaimTypes.GivenName, "User"),
        //            new Claim(JwtClaimTypes.FamilyName, "B")
        //        }
        //    }
        //};
    }
}
