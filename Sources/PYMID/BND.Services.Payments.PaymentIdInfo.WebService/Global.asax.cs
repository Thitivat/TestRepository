using System;
using System.Web;
using System.Web.Http;

namespace BND.Services.Payments.PaymentIdInfo.WebService
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}