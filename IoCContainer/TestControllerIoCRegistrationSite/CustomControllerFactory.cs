using IoCContainer.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestControllerIoCRegistrationSite
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            var controller = IoCContainerSingletonService.Instance.Resolve(controllerType) as Controller;

            return controller;
        }
    }
}