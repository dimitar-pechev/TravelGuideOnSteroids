using System.Web.Mvc;

namespace TravelGuide.Areas.Store.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}