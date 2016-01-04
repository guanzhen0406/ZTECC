using System.Web.Mvc;
using System.Web.Routing;

namespace Wicresoft.Solution.Portal.Inner
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Sys_User", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}