using IdentityServer4.Models;

namespace PasswordManager.MvcWebApp.Services
{
    public class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("api1", "My API")
        };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
            // Client configurations
            new Client
            {
                ClientId = "mvc",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                
                // Redirect URL of your MVC application
                RedirectUris = { "https://localhost:5002/signin-oidc" },

                // Post logout redirect URL of your MVC application
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                // AllowedScopes = { "openid", "profile", "api1" }, // Define the scopes you need
                AllowedScopes = { "openid", "profile" }, // Scopes for user authentication
            }
            };
    }
}
