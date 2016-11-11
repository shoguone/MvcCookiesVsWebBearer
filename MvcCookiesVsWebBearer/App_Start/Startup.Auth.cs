using IdentityServer3.AccessTokenValidation;
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
            app.MapWhen(
                ctx =>
                {
                    if (ctx != null && ctx.Request != null && ctx.Request.Headers != null)
                    {
                        if (ctx.Request.Headers.ContainsKey("Authorization"))
                        {
                            var authorizationHeader = ctx.Request.Headers["Authorization"];
                            if (authorizationHeader.StartsWith("Bearer "))
                                return true;
                        }
                    }
                    return false;
                },
                inner =>
                {
                    // accept access tokens from identityserver and require a scope of ...
                    inner.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                    {
                        Authority = identityServerUrl,

                        RequiredScopes = new[] { "openid", "profile", "roles" }
                    });

                    inner.UseWebApi(WebApiConfig.Register());
                });

            app.MapWhen(
                ctx =>
                {
                    if (ctx != null && ctx.Request != null)
                    {
                        if (ctx.Request.Cookies != null)
                        {
                            var enumerator = ctx.Request.Cookies.GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                if (enumerator.Current.Key.Equals(".AspNet.Cookies"))
                                    return true;
                            }
                        }
                        if (ctx.Request.Headers != null)
                        {
                            if (ctx.Request.Headers.ContainsKey("User-Agent"))
                                return true;
                        }
                    }
                    return false;
                },
                inner =>
                {
                    inner.UseCookieAuthentication(new CookieAuthenticationOptions
                    {
                        AuthenticationType = CookieAuthenticationDefaults.AuthenticationType
                    });

                    inner.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
                    {
                        Authority = identityServerUrl,
                        ClientId = clientId,
                        RedirectUri = redirectUrl,
                        ResponseType = "id_token",

                        SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType,

                        Scope = "openid profile roles",
                    });
                });
        }
    }
}