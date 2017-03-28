using BND.Services.Payments.iDeal.iDealClients.Base;
using BND.Services.Payments.iDeal.Interfaces;
using BND.Services.Payments.iDeal.Models;
using Castle.Windsor;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Xml.Linq;

namespace BND.Services.Payments.iDeal.Api.Tests
{
    [TestFixture]
    public class ErrorHandlerConfigTest
    {
        Mock<ILogger> logger = new Mock<ILogger>();
        Mock<IWindsorContainer> container = new Mock<IWindsorContainer>();

        [Test]
        public void OnException_Should_ReturnUnauthorized_When_UnauthorizedAccessException()
        {
            container.Setup(c => c.Resolve<ILogger>()).Returns((ILogger)logger.Object);
            WindsorConfig._container = container.Object;

            //Defined controllerContext as HttpControllerContext for Request
            HttpControllerContext controllerContext = new HttpControllerContext();
            controllerContext.Request = new HttpRequestMessage();

            //Defined actionContext as HttpActionContext for ControllerContext
            var actionContext = new HttpActionContext();
            actionContext.ControllerContext = controllerContext;

            //Defined expectedException as UnauthorizedAccessException and set message as "Permission denied"
            var expectedException = new UnauthorizedAccessException("Permission denied");

            //Defined actionExecutedContext as HttpActionExecutedContext with 2 params HttpActionContext and Exception 
            HttpActionExecutedContext actionExecutedContext = new HttpActionExecutedContext(actionContext, expectedException);

            //Send actionExecutedContext to ErrorHandlerConfigAttribute
            ErrorHandlerConfigAttribute errorHandler = new ErrorHandlerConfigAttribute();
            errorHandler.OnException(actionExecutedContext);

            logger.Setup(l => l.Error(expectedException.InnerException, expectedException.Message));

            //Compare the Actual and Expected result and deserialize from JSon string to ApiErrorModel
            Assert.AreEqual(HttpStatusCode.Unauthorized, actionExecutedContext.Response.StatusCode);
            ApiErrorModel returnError =
                JsonConvert.DeserializeObject<ApiErrorModel>(
                    actionExecutedContext.Response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(returnError.Message, "Permission denied");
        }

        [Test]
        public void OnException_Should_ReturnForbidden_When_InvalidCredentialException()
        {
            container.Setup(c => c.Resolve<ILogger>()).Returns((ILogger)logger.Object);
            WindsorConfig._container = container.Object;

            HttpControllerContext controllerContext = new HttpControllerContext();
            controllerContext.Request = new HttpRequestMessage();

            var actionContext = new HttpActionContext();
            actionContext.ControllerContext = controllerContext;

            //Defined expectedException as InvalidCredentialException and set message as "Permission denied - InvalidCredential"
            var expectedException = new InvalidCredentialException("Permission denied - InvalidCredential");

            HttpActionExecutedContext actionExecutedContext = new HttpActionExecutedContext(actionContext, expectedException);

            ErrorHandlerConfigAttribute errorHandler = new ErrorHandlerConfigAttribute();
            errorHandler.OnException(actionExecutedContext);

            logger.Setup(l => l.Error(expectedException.InnerException, expectedException.Message));

            Assert.AreEqual(HttpStatusCode.Forbidden, actionExecutedContext.Response.StatusCode);
            ApiErrorModel returnError =
                JsonConvert.DeserializeObject<ApiErrorModel>(
                    actionExecutedContext.Response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(returnError.Message, "Permission denied - InvalidCredential");
        }

        [Test]
        public void OnException_Should_ReturnForbidden_When_InvalidOperationException()
        {
            container.Setup(c => c.Resolve<ILogger>()).Returns((ILogger)logger.Object);
            WindsorConfig._container = container.Object;

            HttpControllerContext controllerContext = new HttpControllerContext();
            controllerContext.Request = new HttpRequestMessage();

            var actionContext = new HttpActionContext();
            actionContext.ControllerContext = controllerContext;

            //Defined expectedException as InvalidOperationException and set message as "Permission denied - InvalidOperation"
            var expectedException = new InvalidOperationException("Permission denied - InvalidOperation");

            HttpActionExecutedContext actionExecutedContext = new HttpActionExecutedContext(actionContext, expectedException);

            ErrorHandlerConfigAttribute errorHandler = new ErrorHandlerConfigAttribute();
            errorHandler.OnException(actionExecutedContext);

            logger.Setup(l => l.Error(expectedException.InnerException, expectedException.Message));

            Assert.AreEqual(HttpStatusCode.Forbidden, actionExecutedContext.Response.StatusCode);
            ApiErrorModel returnError =
                JsonConvert.DeserializeObject<ApiErrorModel>(
                    actionExecutedContext.Response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(returnError.Message, "Permission denied - InvalidOperation");
        }

        [Test]
        public void OnException_Should_ReturnBadRequest_When_ArgumentException()
        {
            container.Setup(c => c.Resolve<ILogger>()).Returns((ILogger)logger.Object);
            WindsorConfig._container = container.Object;

            HttpControllerContext controllerContext = new HttpControllerContext();
            controllerContext.Request = new HttpRequestMessage();

            var actionContext = new HttpActionContext();
            actionContext.ControllerContext = controllerContext;

            //Defined expectedException as ArgumentException and set message as "Value is required"
            var expectedException = new ArgumentException("Value is required");

            HttpActionExecutedContext actionExecutedContext = new HttpActionExecutedContext(actionContext, expectedException);

            ErrorHandlerConfigAttribute errorHandler = new ErrorHandlerConfigAttribute();
            errorHandler.OnException(actionExecutedContext);

            logger.Setup(l => l.Error(expectedException.InnerException, expectedException.Message));

            Assert.AreEqual(HttpStatusCode.BadRequest, actionExecutedContext.Response.StatusCode);
            ApiErrorModel returnError =
                JsonConvert.DeserializeObject<ApiErrorModel>(
                    actionExecutedContext.Response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(returnError.Message, "Value is required");
        }

        [Test]
        public void OnException_Should_ReturnNotFound_When_ObjectNotFoundException()
        {
            container.Setup(c => c.Resolve<ILogger>()).Returns((ILogger)logger.Object);
            WindsorConfig._container = container.Object;

            HttpControllerContext controllerContext = new HttpControllerContext();
            controllerContext.Request = new HttpRequestMessage();

            var actionContext = new HttpActionContext();
            actionContext.ControllerContext = controllerContext;

            //Defined expectedException as ObjectNotFoundException and set message as "Object Not Found"
            var expectedException = new ObjectNotFoundException("Object Not Found");

            HttpActionExecutedContext actionExecutedContext = new HttpActionExecutedContext(actionContext, expectedException);

            ErrorHandlerConfigAttribute errorHandler = new ErrorHandlerConfigAttribute();
            errorHandler.OnException(actionExecutedContext);

            logger.Setup(l => l.Error(expectedException.InnerException, expectedException.Message));

            Assert.AreEqual(HttpStatusCode.NotFound, actionExecutedContext.Response.StatusCode);
            ApiErrorModel returnError =
                JsonConvert.DeserializeObject<ApiErrorModel>(
                    actionExecutedContext.Response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(returnError.Message, "Object Not Found");
        }

        [Test]
        public void OnException_Should_ReturnConflict_When_DataException()
        {
            container.Setup(c => c.Resolve<ILogger>()).Returns((ILogger)logger.Object);
            WindsorConfig._container = container.Object;

            HttpControllerContext controllerContext = new HttpControllerContext();
            controllerContext.Request = new HttpRequestMessage();

            var actionContext = new HttpActionContext();
            actionContext.ControllerContext = controllerContext;

            //Defined expectedException as DataException and set message as "Cannot access DB"
            var expectedException = new DataException("Cannot access DB");

            HttpActionExecutedContext actionExecutedContext = new HttpActionExecutedContext(actionContext, expectedException);

            ErrorHandlerConfigAttribute errorHandler = new ErrorHandlerConfigAttribute();
            errorHandler.OnException(actionExecutedContext);

            logger.Setup(l => l.Error(expectedException.InnerException, expectedException.Message));

            Assert.AreEqual(HttpStatusCode.Conflict, actionExecutedContext.Response.StatusCode);
            ApiErrorModel returnError =
                JsonConvert.DeserializeObject<ApiErrorModel>(
                    actionExecutedContext.Response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(returnError.Message, "Cannot access DB");
        }

        [Test]
        public void OnException_Should_ReturnInternalServerError_When_iDealOperationException()
        {
            container.Setup(c => c.Resolve<ILogger>()).Returns((ILogger)logger.Object);
            WindsorConfig._container = container.Object;

            HttpControllerContext controllerContext = new HttpControllerContext();
            controllerContext.Request = new HttpRequestMessage();

            var actionContext = new HttpActionContext();
            actionContext.ControllerContext = controllerContext;

            //Defined expectedException as iDealOperationException, errorcode and message follow ErrorMessages"
            var expectedException = new iDealOperationException("BND001",
              @"Unfortunately, it is not possible to pay using iDEAL at this time. Please try again later or use an alternative method of payment.");

            HttpActionExecutedContext actionExecutedContext = new HttpActionExecutedContext(actionContext, expectedException);

            ErrorHandlerConfigAttribute errorHandler = new ErrorHandlerConfigAttribute();
            errorHandler.OnException(actionExecutedContext);

            logger.Setup(l => l.Error(expectedException.InnerException, expectedException.Message));

            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
            ApiErrorModel returnError =
                JsonConvert.DeserializeObject<ApiErrorModel>(
                    actionExecutedContext.Response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(returnError.ErrorCode, "BND001");
            Assert.AreEqual(returnError.Message,
              @"Unfortunately, it is not possible to pay using iDEAL at this time. Please try again later or use an alternative method of payment.");
        }

        [Test]
        public void OnException_Should_ReturnInternalServerError_When_iDealException()
        {
            container.Setup(c => c.Resolve<ILogger>()).Returns((ILogger)logger.Object);
            WindsorConfig._container = container.Object;

            HttpControllerContext controllerContext = new HttpControllerContext();
            controllerContext.Request = new HttpRequestMessage();

            var actionContext = new HttpActionContext();
            actionContext.ControllerContext = controllerContext;

            XElement xElement = XElement.Parse(Properties.Resources.ErrorResponse);

            //Defined expectedException as iDealException, errorcode and message follow iDealException"
            var expectedException = new iDealException(xElement);

            HttpActionExecutedContext actionExecutedContext = new HttpActionExecutedContext(actionContext, expectedException);

            ErrorHandlerConfigAttribute errorHandler = new ErrorHandlerConfigAttribute();
            errorHandler.OnException(actionExecutedContext);

            logger.Setup(l => l.Error(expectedException.InnerException, expectedException.Message));

            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
            ApiErrorModel returnError =
                JsonConvert.DeserializeObject<ApiErrorModel>(
                    actionExecutedContext.Response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(returnError.ErrorCode, "SO1100");
            Assert.AreEqual(returnError.Message,
                "Code: SO1100, Message: Issuer not available, Detail: System generating error: Rabobank");
        }

        [Test]
        public void OnException_Should_ReturnInternalServerError_When_TheRestException()
        {
            container.Setup(c => c.Resolve<ILogger>()).Returns((ILogger)logger.Object);
            WindsorConfig._container = container.Object;

            HttpControllerContext controllerContext = new HttpControllerContext();
            controllerContext.Request = new HttpRequestMessage();

            var actionContext = new HttpActionContext();
            actionContext.ControllerContext = controllerContext;

            //Defined expectedException as Exception and message follow ErrorMessages
            var expectedException = new Exception(
              @"Unfortunately, it is not possible to pay using iDEAL at this time. Please try again later or use an alternative method of payment.");

            HttpActionExecutedContext actionExecutedContext = new HttpActionExecutedContext(actionContext, expectedException);

            ErrorHandlerConfigAttribute errorHandler = new ErrorHandlerConfigAttribute();
            errorHandler.OnException(actionExecutedContext);

            logger.Setup(l => l.Error(expectedException.InnerException, expectedException.Message));

            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
            ApiErrorModel returnError =
                JsonConvert.DeserializeObject<ApiErrorModel>(
                    actionExecutedContext.Response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(returnError.Message,
              @"Unfortunately, it is not possible to pay using iDEAL at this time. Please try again later or use an alternative method of payment.");
        }
    }
}