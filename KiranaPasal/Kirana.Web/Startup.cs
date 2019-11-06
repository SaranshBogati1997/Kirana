using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kirana.Web.Startup))]
namespace Kirana.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
