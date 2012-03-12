using System.Web.Mvc;

namespace Web.Application.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new ViewResult();
        }
    }
}
