using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace BND.Services.Security.OTP.Api
{
    /// <summary>
    /// Class WebApiApplication is a root class for starting web api.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Application start.
        /// </summary>
        protected void Application_Start()
        {
            // Configs api.
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // Registers mapper configuration to api.
            MapperConfig.Register();
        }
    }
}
