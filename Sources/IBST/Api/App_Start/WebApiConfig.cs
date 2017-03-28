using BND.Services.IbanStore.Api.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace BND.Services.IbanStore.Api
{
    /// <summary>
    /// Class WebApiConfig.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Services.Replace(typeof(IHttpControllerActivator), new WindsorConfig());
            config.Filters.Add(new ErrorHandlerConfigAttribute());

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
