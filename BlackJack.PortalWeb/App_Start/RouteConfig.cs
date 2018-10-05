using System.Web.Mvc;
using System.Web.Routing;

namespace BlackJack.PortalWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BlackJack.PortalWeb.Controllers" }
            );
        }
    }
}
