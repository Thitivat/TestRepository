using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using BND.Services.Infrastructure.Common.Json;
using BND.Services.Infrastructure.WebAPI.FilterAttributes;
using BND.Services.Infrastructure.WebAPI.MessageHandlers;
using BND.Services.Infrastructure.WebAPI.Security;
using BND.Services.Matrix.Web.BinderProviders;
using BND.Services.Matrix.Web.Filters;

using WebApiThrottle;

namespace BND.Services.Matrix.Web
{
    /// <summary>
    /// WebAPI configuration
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the <paramref name="config"/>
        /// </summary>
        /// <param name="config">The <see cref="HttpConfiguration"/></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var settings = new BaseJsonSerializerSettings(Convert.ToInt32(ConfigurationManager.AppSettings[Constants.DefaultDecimalPrecision]));

            // Remove the JSON formatter and the XML formatter
            config.Formatters.Remove(config.Formatters.JsonFormatter);
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Add our own JSON formatter
            config.Formatters.Add(new BaseJsonMediaTypeFormatter(settings));

            // Global registration of action filter attributes
            config.Filters.Add(new BaseExceptionFilterAttribute());
            config.Filters.Add(new QueryStringValuesNotEmptyFilterAttribute());
            config.Filters.Add(new ModelValidationFilterAttribute());

            config.Filters.Add(new QueryStringModelValidKeysFilterAttribute());

            config.Filters.Add(
                new BNDWebApiAuthorizeAttribute(
                    null /*new List<Claim>() { new Claim("role", "Customer") }*/,
                    bool.Parse(ConfigurationManager.AppSettings.Get(Constants.AuthSecurityEnabled))));

            config.MessageHandlers.Add(new RequestAndResponseLoggingHandler(settings));

            // http://www.stefanprodan.com/2013/12/asp-net-web-api-throttling-handler/
            // configuration in web config
            config.MessageHandlers.Add(new ThrottlingHandler()
            {
                Policy = ThrottlePolicy.FromStore(new PolicyConfigurationProvider()),
                Repository = new CacheRepository()
            });


            // Global registration of model binder providers
            //config.Services.Insert(typeof(ModelBinderProvider), 0, new MessageFiltersModelBinderProvider());

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}
