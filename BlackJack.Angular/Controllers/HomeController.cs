using System.Web.Mvc;

namespace BlackJack.Angular.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return new FilePathResult("~/ClientApp/dist/ClientApp/index.html", "text/html");
        }
    }
}