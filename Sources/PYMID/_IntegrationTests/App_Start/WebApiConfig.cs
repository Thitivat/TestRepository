using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace BND.Services.Payments.PaymentIdInfo.IntegrationTests
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IHttpControllerActivator), new WindsorConfig());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "GetEnumFilterType", id = RouteParameter.Optional }
            );

            config.EnableSystemDiagnosticsTracing();
        }
    }
}
