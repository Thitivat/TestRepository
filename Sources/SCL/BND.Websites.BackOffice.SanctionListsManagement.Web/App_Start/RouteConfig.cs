using System.Web.Mvc;
using System.Web.Routing;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            // Default route
            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );

            // Default route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
