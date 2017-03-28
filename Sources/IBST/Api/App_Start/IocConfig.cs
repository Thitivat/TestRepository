using BND.Services.IbanStore.Api.Controllers;
using BND.Services.IbanStore.Api.Helpers;
using BND.Services.IbanStore.Service.Bll;
using BND.Services.IbanStore.Service.Bll.Interfaces;
using BND.Services.IbanStore.Service.Dal;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace BND.Services.IbanStore.Api
{
    public class WindsorConfig : IHttpControllerActivator
    {
        public static IWindsorContainer _container;

        public WindsorConfig()
        {
            _container = new WindsorContainer();
            _container.Install(new ComponentInstaller());
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            IHttpController controller = (IHttpController)_container.Resolve(controllerType);

            request.RegisterForDispose(new Release(() => _container.Release(controller)));

            return controller;
        }

        private class Release : IDisposable
        {
            private readonly Action _release;

            public Release(Action release)
            {
                _release = release;
            }

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
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.AddFacility<TypedFactoryFacility>();
            container.Register(
                // Registers all api controllers.
                Classes.FromThisAssembly().BasedOn<ApiControllerBase>().LifestyleTransient(),

                // Registers global config.
                Component.For<HttpConfiguration>()
                         .Instance(GlobalConfiguration.Configuration),

                // Registers all components what we want.
                Component.For<IIbanManager>()
                         .ImplementedBy<IbanManager>()
                         .LifestylePerThread(),
                Component.For<ISecurity>()
                         .ImplementedBy<Security>()
                         .LifestyleSingleton(),
                Component.For<ILogger>()
                         .ImplementedBy<Logger>()
                         .LifestyleSingleton()

            );
        }
    }


}