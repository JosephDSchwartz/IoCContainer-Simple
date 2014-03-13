using System.Web.Mvc;
using TestControllerIoCRegistrationSite.Models.Interfaces;

namespace TestControllerIoCRegistrationSite.Controllers
{
    public class HomeController : Controller
    {
        private IHomeModel homeModel;

        public HomeController(IHomeModel model)
        {
            homeModel = model;
            homeModel.Message = "The HomeModel was wired up using the Simple IoCContainer.";
        }

        public ActionResult Index()
        {
            ViewBag.Message = homeModel.Message;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}