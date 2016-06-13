using Microsoft.Owin;
using Owin;

using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using InternalSystem.Web.App_Start;

[assembly: OwinStartup(typeof(InternalSystem.Web.Startup))]
namespace InternalSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DependencyConfig.RegisterDependencies(app);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Admin/Account/Login")
            });
        }
    }
}
