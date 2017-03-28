using BND.Services.Payments.iDeal.Api.Helpers;
using Castle.Windsor;
using Moq;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace BND.Services.Payments.iDeal.Api.Tests
{
    [TestFixture]
    public class SecurityConfigTest
    {
        [Test]
        public void OnActionExecuting_Should_ThrowArgumentNullException_When_AuthorizationIsNull()
        {
            Mock<IWindsorContainer> container = new Mock<IWindsorContainer>();
            container.Setup(c => c.Resolve<ISecurity>())
                     .Returns((ISecurity)null);
            WindsorConfig._container = container.Object;

            HttpControllerContext controllerContext = new HttpControllerContext();
            controllerContext.Request = new HttpRequestMessage();

            var actionContext = new HttpActionContext(controllerContext, new ReflectedHttpActionDescriptor());
            SecurityConfigAttribute securityValidation = new SecurityConfigAttribute();

            Assert.Throws<ArgumentNullException>(() => securityValidation.OnActionExecuting(actionContext));
        }

        [Test]
        public void OnActionExecuting_Should_DonNotThrowAnyException_When_AuthorizationIsNotNull()
        {
            Mock<ISecurity> security = new Mock<ISecurity>();
            security.Setup(s => s.ValidateToken(It.IsAny<string>()))
                    .Verifiable();
            Mock<IWindsorContainer> container = new Mock<IWindsorContainer>();
            container.Setup(c => c.Resolve<ISecurity>())
                     .Returns(security.Object);
            WindsorConfig._container = container.Object;

            HttpControllerContext controllerContext = new HttpControllerContext();
            controllerContext.Request = new HttpRequestMessage();

            var actionContext = new HttpActionContext(controllerContext, new ReflectedHttpActionDescriptor());
            actionContext.Request.Headers.Add("Authorization", "BND");

            SecurityConfigAttribute securityValidation = new SecurityConfigAttribute();

            Assert.DoesNotThrow(() => securityValidation.OnActionExecuting(actionContext));
        }
    }
}