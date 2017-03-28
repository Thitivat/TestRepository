using BND.Services.IbanStore.Api;
using BND.Services.IbanStore.Api.Controllers;
using BND.Services.IbanStore.Api.Helpers;
using BND.Services.IbanStore.Api.Models;
using BND.Services.IbanStore.Service.Bll;
using BND.Services.IbanStore.Service.Bll.Interfaces;
using Castle.Core;
using Castle.MicroKernel.ComponentActivator;
using Castle.Windsor;
using Moq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using Model = BND.Services.IbanStore.Models;
using NUnit.Framework;
using System.Web;

namespace BND.Services.IbanStore.ApiTest
{
    [TestFixture]
    public class IbansControllerTest
    {
        private string _token;
        private IbansController _controller;
        private Mock<IIbanManager> _ibanManagerMock;
        private Mock<ISecurity> _securityMock;
        private Mock<ILogger> _loggerMock;
        private Model.Iban _expectedIban = new Model.Iban("NL", "BND", "538053100", "26")
        {
            Uid = "Uid",
            UidPrefix = "UidPrefix",
            ReservedTime = DateTime.Now
        };
        void Kernel_ComponentModelCreated(Castle.Core.ComponentModel model)
        {

            model.LifestyleType = LifestyleType.PerWebRequest;
        }
        [SetUp]
        public void Init()
        {
            var container = new WindsorContainer();
            container.Install(new ComponentInstaller());
            WindsorConfig._container = container;

            _token = @"token";
            _ibanManagerMock = new Mock<IIbanManager>();
            _securityMock = new Mock<ISecurity>();
            _securityMock.Setup(s => s.Authenticate(It.IsAny<CredentialsModel>())).Returns(true);


            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
            );
            string token = System.Web.HttpContext.Current.Request.Headers["Authorization"];
            string uidPrefix = System.Web.HttpContext.Current.Request.Headers["Uid-Prefix"];
            _securityMock.Setup(s => s.GetUserCredential(token, uidPrefix)).Returns(new CredentialsModel("HEADER") { Token = "MOCKED_TOKEN" });

            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // prepare controller and header
            _controller = new IbansController(_securityMock.Object);
            _controller.Request = new HttpRequestMessage { RequestUri = new Uri("http://localhost:50421/api/ibans") };
            _controller.Request.Headers.TryAddWithoutValidation("Token", _token);
            _controller.Request.Headers.TryAddWithoutValidation("Authorization", "User");
            _controller.Request.Headers.TryAddWithoutValidation("Uid-Prefix", "VO");
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, config);
            //var po = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(_controller);
            //po.SetField("_ibanManager", _ibanManagerMock.Object);
        }

        [Test]
        public void Test_Get_Success()
        {
            // create mockup and setup method
            //_ibanManagerMock.Setup(service => service.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(_expectedIban);

            //string uid = "UID";

            //// call Get method
            //var actionResult = _controller.Get(uid);
            //var actualModel = actionResult as OkNegotiatedContentResult<Model.Iban>;

            //Assert.IsNotNull(actualModel);
            //Assert.IsTrue(_expectedIban.Equals(actualModel.Content));
        }

        [Test]
        public void Test_Get_Fail_NullData()
        {
            // create mockup and setup method
            //_ibanManagerMock.Setup(service => service.Get(It.IsAny<string>())).Returns(() => null);

            //string prefixUid = "PREFIX_UID";
            //string uid = "UID";

            //// call Get method
            //Assert.Throws<FileNotFoundException>(() =>
            //{
            //    _controller.Get(uid);
            //});
        }

        [Test]
        public void Test_Reserve_Success()
        {
            // create mockup and setup method
            //_ibanManagerMock.Setup(service => service.Reserve(It.IsAny<string>(), It.IsAny<string>()))
            //                .Returns(_expectedIban);

            //// call ReserveNextAvailable method
            //var actionResult = _controller.ReserveNextAvailable("uid");
            //var actualModel = actionResult as ResponseMessageResult;

            //Assert.IsNotNull(actualModel);
            //Assert.AreEqual(HttpStatusCode.OK, actualModel.Response.StatusCode);
            //Assert.IsNotNull(actualModel.Response.Headers.Location);
        }

        [Test]
        public void Test_Assign_Success()
        {
            //// create mockup and setup method
            //_ibanManagerMock.Setup(service => service.Assign(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
            //                .Returns(_expectedIban);

            //var actionResult = _controller.Assign(0, "uid");
            //var actualModel = actionResult as ResponseMessageResult;
            //var actualBody = actualModel.Response.Content;

            //Assert.IsNotNull(actualModel);
            //Assert.AreEqual(HttpStatusCode.OK, actualModel.Response.StatusCode);
            //Assert.IsNotNull(actualModel.Response.Headers.GetValues("Location"));
            //Assert.IsNull(actualBody);
        }

        [Test]
        public void Test_Assign_Success_WithMultipleRequest()
        {
            // create mockup and setup method
            //_ibanManagerMock.Setup(service => service.Assign(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
            //                .Throws(new IbanAlreadyAssignedException(_expectedIban));

            //var actionResult = _controller.Assign(0, "uid");
            //var actualModel = actionResult as ResponseMessageResult;

            //Assert.IsNotNull(actualModel);
            //Assert.AreEqual(HttpStatusCode.NotModified, actualModel.Response.StatusCode);
            //Assert.IsNotNull(actualModel.Response.Headers.GetValues("Location"));
        }
    }
}
