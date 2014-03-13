using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestControllerIoCRegistrationSite.Startup))]
namespace TestControllerIoCRegistrationSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
