using System.Reflection;
using System.Web;
using System.Web.Http;

using BND.Services.Matrix.Web;

using log4net;
using log4net.Config;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BND.Services.Matrix.IoC.UnityWebApiActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(BND.Services.Matrix.IoC.UnityWebApiActivator), "Shutdown")]
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace BND.Services.Matrix.Web
{
    /// <summary>
    /// The web service application
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The application start event
        /// </summary>
        protected void Application_Start()
        {
            // Configure log4net
            XmlConfigurator.Configure();

            Logger.Info("Application starts...");

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
