using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcCookiesVsWebBearer.Startup))]
namespace MvcCookiesVsWebBearer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
