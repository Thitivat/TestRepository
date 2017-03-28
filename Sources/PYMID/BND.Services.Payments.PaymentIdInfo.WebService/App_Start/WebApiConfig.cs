using BND.Services.Payments.PaymentIdInfo.WebService.App_Start;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace BND.Services.Payments.PaymentIdInfo.WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Services.Replace(typeof(IHttpControllerActivator), new WindsorConfig());
            config.MapHttpAttributeRoutes(new ApiGlobalPrefixRouteProvider("api"));
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            FilterConfig.Register(config.Filters);
        }        
    }
}
