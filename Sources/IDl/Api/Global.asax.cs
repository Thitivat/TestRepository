using System.Net;
using System.Web.Http;

namespace BND.Services.Payments.iDeal.Api
{
    /// <summary>
    /// Class WebApiApplication.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

    }
}
