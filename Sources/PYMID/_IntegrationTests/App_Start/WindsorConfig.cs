using BND.Services.Payments.PaymentIdInfo.Proxy.NET4;
using BND.Services.Payments.PaymentIdInfo.Proxy.NET4.Interfaces;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace BND.Services.Payments.PaymentIdInfo.IntegrationTests
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
            string baseAddress = GetSetting<string>("baseAddress");
            string token = GetSetting<string>("token");

            container.Kernel.AddFacility<TypedFactoryFacility>();
            container.Register(
                // Registers all api controllers.
                Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest(),

                // Registers global config.
                Component.For<HttpConfiguration>()
                         .Instance(GlobalConfiguration.Configuration),

                // Registers all components what we want.

                Component.For<IPaymentIdInfoProxy>()
                         .ImplementedBy<PaymentIdInfoProxy>()
                         .DependsOn(Dependency.OnValue("baseAddress", baseAddress))
                         .DependsOn(Dependency.OnValue("token", token))
                         .LifestylePerWebRequest()
            );
        }

        private static T GetSetting<T>(string name)
        {
            string value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                throw new Exception(String.Format("Could not find setting '{0}',", name));
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}