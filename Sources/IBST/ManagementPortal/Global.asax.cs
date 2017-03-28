using System.Web.Mvc;
using System.Web.Routing;

namespace BND.Services.IbanStore.ManagementPortal
{
    /// <summary>
    /// Class MvcApplication.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Register module for Application start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        
    }
}