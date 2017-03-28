using Castle.Windsor;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace BND.Services.Payments.eMandates.IoC
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
}