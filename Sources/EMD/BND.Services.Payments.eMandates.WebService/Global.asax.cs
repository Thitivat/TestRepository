using System.Web.Http;

namespace BND.Services.Payments.eMandates.WebService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Application start section.
        /// </summary>
        protected void Application_Start()
        {
            //UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
