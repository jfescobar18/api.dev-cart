using System.IO;
using System.Web.Mvc;
using api.dev_cart.Entity;

namespace api.dev_cart.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
