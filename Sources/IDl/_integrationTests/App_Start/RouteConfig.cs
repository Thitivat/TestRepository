using System.Web.Mvc;
using System.Web.Routing;

namespace BND.Services.Payments.iDeal.IntegrationTests
{
    /// <summary>
    /// Class RouteConfig for configure website.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers ane map routes for website.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}