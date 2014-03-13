using IoCContainer.Service.Services;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestControllerIoCRegistrationSite.Controllers;
using TestControllerIoCRegistrationSite.Models;
using TestControllerIoCRegistrationSite.Models.Interfaces;

namespace TestControllerIoCRegistrationSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterTypes();
            RegisterCustomControllerFactory();
        }

        private void RegisterTypes()
        {
            IoCContainerSingletonService.Instance
                .Register<HomeController, HomeController>()
                .Register<AccountController, AccountController>()
                .Register<IHomeModel, HomeModel>();
        }

        private void RegisterCustomControllerFactory()
        {
            var customControllerFactory = new CustomControllerFactory();

            ControllerBuilder.Current.SetControllerFactory(customControllerFactory);
        }
    }
}
