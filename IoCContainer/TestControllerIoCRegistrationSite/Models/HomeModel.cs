using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestControllerIoCRegistrationSite.Models.Interfaces;

namespace TestControllerIoCRegistrationSite.Models
{
    public class HomeModel : IHomeModel
    {
        public string Message { get; set; }
    }
}