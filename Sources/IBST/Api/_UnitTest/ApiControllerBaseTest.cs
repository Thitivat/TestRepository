using BND.Services.IbanStore.Api.Controllers;
using BND.Services.IbanStore.Api.Helpers;
using BND.Services.IbanStore.Api.Models;
using BND.Services.IbanStore.Models;
using Moq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using BND.Services.IbanStore.Api;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace BND.Services.IbanStore.ApiTest
{
    [TestFixture]
    public class ApiControllerBaseTest
    {
        private string _authentication = "auth";
        private string _uidPrefixHeader = "Uid-Prefix";
        private string _uidPrefix = "unittest";
        //private Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject _apiControllerMock;
        private Mock<ISecurity> _securityMock;
        private Mock<ILogger> _loggerMock;

        [SetUp]
        public void Test_Initialize()
        {
            var container = new WindsorContainer();
            container.Install(new ComponentInstaller());
            WindsorConfig._container = container;
            HttpRequestMessage request = new HttpRequestMessage();
            request.Headers.Authorization = new AuthenticationHeaderValue(_authentication);
            request.Headers.Add(_uidPrefixHeader, _uidPrefix);
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("UnitTest", "1.0.0.0")));
            _securityMock = new Mock<ISecurity>();
            _loggerMock = new Mock<ILogger>();

            //_apiControllerMock = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(new MockApiController());
            //_apiControllerMock.SetField("_security", _securityMock.Object);

            //((MockApiController)_apiControllerMock.Target).Request = request;
        }

        [Test]
        public void Test_Prop_Credentials_Success()
        {
            CredentialsModel expected = new CredentialsModel(_authentication);
            //CredentialsModel actual = (CredentialsModel)_apiControllerMock.GetProperty("Credentials");

            //Assert.IsNotNull(actual);
            //Assert.AreEqual(expected.Token, actual.Token);
            //Assert.AreEqual(expected.Username, actual.Username);
        }

        [Test]
        public void Test_Prop_Credentials_Exception_AuthHeaderNull()
        {
            //((MockApiController)_apiControllerMock.Target).Request.Headers.Authorization = null;
            //Assert.Throws<ArgumentException>(() => { _apiControllerMock.GetProperty("Credentials"); });
        }

        //[Test]
        //public void Test_Prop_UidPrefix_Success()
        //{
        //    Assert.AreEqual(_uidPrefix, _apiControllerMock.GetProperty("UidPrefix"));
        //}

        //[Test]
        //public void Test_Prop_UidPrefix_Success_NoUidPrefix()
        //{
        //    ((MockApiController)_apiControllerMock.Target).Request.Headers.Remove(_uidPrefixHeader);

        //    Assert.IsNull(_apiControllerMock.GetProperty("UidPrefix"));
        //}

        //[Test]
        //public void Test_Initialize_Success()
        //{
        //    HttpControllerContext context = new HttpControllerContext();
        //    context.Request = ((MockApiController)_apiControllerMock.Target).Request;
        //    _securityMock.Setup(s => s.Authenticate(It.IsAny<CredentialsModel>())).Returns(true);
        //    _apiControllerMock.Invoke("Initialize", context);

        //    var actual = ((MockApiController)_apiControllerMock.Target).Request;
        //    Assert.AreEqual(actual, context.Request);
        //}

        //[Test]
        //public void Test_Initialize_Fail_Unauthorized()
        //{
        //    HttpControllerContext context = new HttpControllerContext();
        //    context.Request = ((MockApiController)_apiControllerMock.Target).Request;
        //    _securityMock.Setup(s => s.Authenticate(It.IsAny<CredentialsModel>())).Returns(false);

        //    try
        //    {
        //        _apiControllerMock.Invoke("Initialize", context);

        //        Assert.Fail();
        //    }
        //    catch (HttpResponseException ex)
        //    {
        //        ApiErrorModel errorModel = ex.Response.Content.ReadAsAsync<ApiErrorModel>().Result;

        //        Assert.IsNotNull(errorModel);
        //        Assert.AreEqual(System.Net.HttpStatusCode.Unauthorized, errorModel.StatusCode);
        //    }
        //}

        //[Test]
        //public void Test_Initialize_Exception_CannotInitialize()
        //{
        //    ((MockApiController)_apiControllerMock.Target).Request.Headers.Authorization = null;

        //    HttpControllerContext context = new HttpControllerContext();
        //    context.Request = ((MockApiController)_apiControllerMock.Target).Request;
        //    Assert.Throws<HttpResponseException>(() => { _apiControllerMock.Invoke("Initialize", context); });
        //}
    }
}
