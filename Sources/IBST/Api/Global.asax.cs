using BND.Services.IbanStore.Service.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace BND.Services.IbanStore.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Create Mapper config.
            MapperConfig.CreateMapper();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
