using System.Web.Http;

namespace BND.Services.Payments.iDeal.IntegrationTests
{
    /// <summary>
    /// Class WebApiConfig for configure webapi.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers ane map routes for webapi.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
