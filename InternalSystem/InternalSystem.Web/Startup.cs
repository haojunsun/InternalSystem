using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InternalSystem.Web.Startup))]
namespace InternalSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
