using NUnit.Framework;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace BND.Services.Payments.iDeal.Api.Tests
{
    [TestFixture]
    public class ModelStateValidationConfigTest
    {
        [Test]
        public void OnActionExecuting_Should_ThrowArgumentException_When_Invalid()
        {
            HttpControllerContext controllerContext = new HttpControllerContext();
            controllerContext.Request = new HttpRequestMessage();

            var actionContext = new HttpActionContext();
            actionContext.ModelState.AddModelError("PurchaseID", "Value is required");

            ModelStateValidationConfigAttribute modelStateValidation = new ModelStateValidationConfigAttribute();
            actionContext.ActionArguments.Add("PurchaseID", String.Empty);

            Assert.Throws<ArgumentException>(() => modelStateValidation.OnActionExecuting(actionContext));
        }

        [Test]
        public void OnActionExecuting_Should_DoNotThrowAnyException_When_Valid()
        {
            HttpControllerContext controllerContext = new HttpControllerContext();
            controllerContext.Request = new HttpRequestMessage();

            var actionContext = new HttpActionContext();
            actionContext.ControllerContext = controllerContext;

            ModelStateValidationConfigAttribute modelStateValidation = new ModelStateValidationConfigAttribute();

            Assert.DoesNotThrow(() => modelStateValidation.OnActionExecuting(actionContext));
        }
    }
}