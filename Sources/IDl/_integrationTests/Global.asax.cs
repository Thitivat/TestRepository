using AutoMapper;
using BND.Services.Payments.iDeal.IntegrationTests.ViewModels;
using BND.Services.Payments.iDeal.Models;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace BND.Services.Payments.iDeal.IntegrationTests
{
    /// <summary>
    /// Class WebApiApplication, the main application.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Configuration for everything before start application.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Mapper.Initialize(config =>
            {
                config.CreateMap<TransactionRequestViewModel, TransactionRequestModel>();
            });

        }
    }
}