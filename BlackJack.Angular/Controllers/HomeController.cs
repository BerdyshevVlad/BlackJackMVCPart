using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlackJack.Angular.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return new FilePathResult("~/ClientApp/dist/ClientApp/index.html", "text/html");
            //return View();
        }
    }
}