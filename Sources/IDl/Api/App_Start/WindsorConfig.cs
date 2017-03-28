using System;
using System.Configuration;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using BND.Services.Matrix.Proxy.NET4.Implementations;
using BND.Services.Matrix.Proxy.NET4.Interfaces;
using BND.Services.Payments.iDeal.Api.Helpers;
using BND.Services.Payments.iDeal.Booking;
using BND.Services.Payments.iDeal.ClientData;
using BND.Services.Payments.iDeal.ClientData.Dal.Interfaces;
using BND.Services.Payments.iDeal.ClientData.Dal.Repositories;
using BND.Services.Payments.iDeal.Dal;
using BND.Services.Payments.iDeal.Interfaces;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using BND.Services.Payments.iDeal.JobQueue.Dal.Interfaces;
using BND.Services.Payments.iDeal.JobQueue.Dal;
using BND.Services.Payments.iDeal.JobQueue.Dal.Repositories;
using BND.Services.Payments.iDeal.JobQueue.Bll.Interfaces;
using BND.Services.Payments.iDeal.JobQueue.Bll;
using IUnitOfWork = BND.Services.Payments.iDeal.ClientData.Dal.Interfaces.IUnitOfWork;


namespace BND.Services.Payments.iDeal.Api
{
    /// <summary>
    /// Class WindsorConfig.
    /// </summary>
    public class WindsorConfig : IHttpControllerActivator
    {
        /// <summary>
        /// The _container install Ioc
        /// </summary>
        public static IWindsorContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorConfig"/> class.
        /// </summary>
        public WindsorConfig()
        {
            _container = new WindsorContainer();
            _container.Install(new ComponentInstaller());
        }


        /// <summary>
        /// register apicontroller
        /// </summary>
        /// <param name="request">The message request.</param>
        /// <param name="controllerDescriptor">The HTTP controller descriptor.</param>
        /// <param name="controllerType">The type of the controller.</param>
        /// <returns>An <see cref="T:System.Web.Http.Controllers.IHttpController" /> object.</returns>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            IHttpController controller = (IHttpController)_container.Resolve(controllerType);

            request.RegisterForDispose(new Release(() => _container.Release(controller)));

            return controller;
        }
        /// <summary>
        /// Class Release.
        /// </summary>
        private class Release : IDisposable
        {
            /// <summary>
            /// The _release
            /// </summary>
            private readonly Action _release;

            /// <summary>
            /// Initializes a new instance of the <see cref="Release"/> class.
            /// </summary>
            /// <param name="release">The release.</param>
            public Release(Action release)
            {
                _release = release;
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                _release();
            }
        }
    }

    /// <summary>
    /// Class ComponentInstaller.
    /// </summary>
    public class ComponentInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.AddFacility<TypedFactoryFacility>();
            
            container.Register(
                // Registers all api controllers.
                Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest(),

                // Registers global config.
                Component.For<HttpConfiguration>()
                         .Instance(GlobalConfiguration.Configuration),

                // Registers all components what we want.
                Component.For<IiDealService>()
                         .ImplementedBy<iDealService>()
                         .DependsOn(Dependency.OnValue("bndBic", ConfigurationManager.AppSettings["BndBic"]))
                         .LifestylePerWebRequest(),
                Component.For<ISecurity>()
                         .ImplementedBy<Security>()
                         .LifestylePerWebRequest(),
                Component.For<ILogger>()
                         .ImplementedBy<Logger>().DependsOn(Dependency.OnValue("appName", ConfigurationManager.AppSettings["AppName"]))
                         .LifestylePerWebRequest(),

                Component.For<BND.Services.Payments.iDeal.Dal.IUnitOfWork>()
                         .ImplementedBy<iDealUnitOfWork>()
                         .DependsOn(Dependency.OnValue("connectionString", ConfigurationManager.AppSettings["ConnectionString"]))
                         .LifestylePerWebRequest(),
                Component.For<ILogRepository>()
                         .ImplementedBy<LogRepository>()
                         .LifestylePerWebRequest(),
                Component.For<IDirectoryRepository>()
                         .ImplementedBy<DirectoryRepository>()
                         .LifestylePerWebRequest(),
                Component.For<ITransactionRepository>()
                         .ImplementedBy<TransactionRepository>()
                         .LifestylePerWebRequest(),
                Component.For<ISettingRepository>()
                         .ImplementedBy<SettingRepository>()
                         .LifestylePerWebRequest(),

                // register components for jobqueue.
                Component.For<BND.Services.Payments.iDeal.JobQueue.Dal.Interfaces.IUnitOfWork>()
                         .ImplementedBy<JobQueueUnitOfWork>()
                         .DependsOn(Dependency.OnValue("connectionString", ConfigurationManager.AppSettings["JobQueueConnectionString"]))
                         .LifestylePerWebRequest(),
                Component.For<IJobListRepository>()
                         .ImplementedBy<JobListRepository>()
                         .LifestylePerWebRequest(),
                Component.For<IJobQueueManager>()
                         .ImplementedBy<JobQueueManager>()
                         .LifestylePerWebRequest(),

                Component.For<IiDealClient>()
                         .ImplementedBy<iDealClient>().DependsOn(Dependency.OnValue("merchantId", ConfigurationManager.AppSettings["MerchantId"]),
                                                                 Dependency.OnValue("subId", Int32.Parse(ConfigurationManager.AppSettings["SubId"])))
                         .LifestylePerWebRequest(),
                
                
                // AccountsApi for matrix
                Component.For<IApiConfig>()
                         .ImplementedBy<ApiConfig>()
                         .DependsOn(Dependency.OnValue("serviceUrl", ConfigurationManager.AppSettings["MatrixServiceUrl"]))
                         .LifestylePerWebRequest(),

                Component.For<IAccountsApi>()
                         .ImplementedBy<AccountsApi>()
                         .LifestylePerWebRequest(),

                Component.For<IUnitOfWork>()
                         .ImplementedBy<ClientDataUnitOfWork>()
                         .DependsOn(Dependency.OnValue("connectionString", ConfigurationManager.AppSettings["CboConnectionString"]))
                         .LifestylePerWebRequest(),

                Component.For<IClientUserRepository>()
                         .ImplementedBy<ClientUserRepository>()
                         .LifestylePerWebRequest(),

                Component.For<IClientDataProvider>()
                         .ImplementedBy<ClientDataProvider>()
                         .DependsOn(Dependency.OnValue("connectionString", ConfigurationManager.AppSettings["CboConnectionString"]))
                         .LifestylePerWebRequest(),

                // Booking Manager
                Component.For<IBookingManager>()
                         .ImplementedBy<BookingManager>()
                         .LifestylePerWebRequest()
            );
        }
    }
}
