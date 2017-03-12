using System.Web.Mvc;

namespace TravelGuide.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}