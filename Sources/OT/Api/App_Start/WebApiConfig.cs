using BND.Services.Security.OTP.Api.Utils;
using System.Web.Http;

namespace BND.Services.Security.OTP.Api
{
    /// <summary>
    /// Class WebApiConfig is a class for initializing all api before run.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration to api.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes(new ApiGlobalPrefixRouteProvider("api"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
