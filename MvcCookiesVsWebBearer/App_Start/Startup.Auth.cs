using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

namespace MvcCookiesVsWebBearer
{
    public partial class Startup
    {
        public const string identityServerUrl = "https://localhost:44333/core";
        public const string redirectUrl = "http://localhost:62874/";
        public const string clientId = "mvc";

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = identityServerUrl,
                ClientId = clientId,
                RedirectUri = redirectUrl,
                ResponseType = "id_token",

                SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType,

                Scope = "openid profile roles",
            });
        }
    }
}