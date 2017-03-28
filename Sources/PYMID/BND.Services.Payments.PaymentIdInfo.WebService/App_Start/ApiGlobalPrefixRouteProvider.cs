using System;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace BND.Services.Payments.PaymentIdInfo.WebService
{
    /// <summary>
    /// Class ApiGlobalPrefixRouteProvider is a routing config provider to control any controller which use route attribute to go the same way.
    /// This class provides base route for any action of any controller, so you just set route after base route + controller name following:
    /// <br/>base route = "Api"<br/>
    /// controller name = "TestController"<br/>
    /// So if sets route attribute to be [Route("{id}/Foo")] on your action,
    /// uri will be "http:your-domain.com/Api/Test/{id}/Foo", you dont need to set whole uri path on Route attribute like
    /// [Route("Api/Test/{id}/Foo")]
    /// </summary>
    public class ApiGlobalPrefixRouteProvider : DefaultDirectRouteProvider
    {
        /// <summary>
        /// The global prefix route.
        /// </summary>
        private readonly string _globalPrefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiGlobalPrefixRouteProvider"/> class.
        /// </summary>
        /// <param name="globalPrefix">The global prefix.</param>
        public ApiGlobalPrefixRouteProvider(string globalPrefix)
        {
            _globalPrefix = globalPrefix;
        }

        /// <summary>
        /// Gets the route prefix from the provided controller.
        /// </summary>
        /// <param name="controllerDescriptor">The controller descriptor.</param>
        /// <returns>The route prefix or null.</returns>
        protected override string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
        {
            // Checks route prefix of each controller if any.
            var existingPrefix = base.GetRoutePrefix(controllerDescriptor);

            // Plus route prefix of each controller next to global route.
            return String.Format("{0}/{1}", _globalPrefix, (existingPrefix == null) ? controllerDescriptor.ControllerName : existingPrefix);
        }
    }
}