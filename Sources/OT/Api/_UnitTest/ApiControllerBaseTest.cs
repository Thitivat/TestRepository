using BND.Services.Security.OTP.Api.Controllers;
using BND.Services.Security.OTP.Api.Models;
using BND.Services.Security.OTP.Api.Utils;
using BND.Services.Security.OTP.Models;
using NUnit.Framework;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
//using MsTest = Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BND.Services.Security.OTP.ApiUnitTest
{
    [TestFixture]
    public class ApiControllerBaseTest
    {
        private static ApiSettings _apiSettings = new ApiSettings
        {
            AccountId = "5861F73F-CAD5-419D-96D4-56BD07211297",
            ApiKey = "1phM4nLk14tefH8cntFJfuINtH0w_POg1zdKO9EPiu28TYjwLH0mOWzvcFiD0h3pPvf9wlhxhYk5hA6Ur0BHg8InK91GwhfCbW4kQU_6KkbKTb1H9gkOqTnFZxY4lPyl",
            ClientIp = "127.0.0.1",
            ChannelPluginsPath = Path.GetTempPath(),
            ConnectionString = "Data Source=.;Integrated Security=True",
            OtpCodeLength = 6,
            OtpIdLength = 128,
            RefCodeLength = 6,
            UserAgent = "user agent"
        };
        private static string _channelPlugins = Path.Combine(Path.GetTempPath(), "ChannelPlugins");
        //private MsTest.PrivateObject _apiControllerMock = new MsTest.PrivateObject(new MockApiController(), new MsTest.PrivateType(typeof(ApiControllerBase)));

        [SetUp]
        public void Test_Initialize()
        {
            if (!Directory.Exists(_channelPlugins))
            {
                Directory.CreateDirectory(_channelPlugins);
            }
        }

        [TearDown]
        public void Test_Cleanup()
        {
            if (Directory.Exists(_channelPlugins))
            {
                Directory.Delete(_channelPlugins);
            }
        }

        [Test]
        public void Test_Dispose_Success()
        {
            //_apiControllerMock.SetField("ApiManager", new ApiManager(_apiSettings));

            //((MockApiController)_apiControllerMock.Target).Dispose();
            //((MockApiController)_apiControllerMock.Target).Dispose();

            //Assert.IsNull(_apiControllerMock.GetField("ApiManager"));
        }

        [Test]
        public void Test_Process_Success()
        {
            InitRequest();

            Func<IHttpActionResult> workMock = () => new OkResult(new MockApiController());
            //var actionResult = _apiControllerMock.Invoke("Process", workMock);

            //Assert.IsNotNull(actionResult);
            //Assert.IsTrue(actionResult is OkResult || actionResult is ErrorResult);
        }

        [Test]
        public void Test_Process_Success_ApiManagerNull()
        {
            bool expectedResult = true;

            Mock<IApiManager> apiManagerMock = new Mock<IApiManager>();
            apiManagerMock.Setup(a => a.VerifyAccount());

            //_apiControllerMock.SetField("ApiManager", apiManagerMock.Object);

            //Func<IHttpActionResult> workMock = () => new OkNegotiatedContentResult<bool>(expectedResult, new MockApiController());
            //OkNegotiatedContentResult<bool> actionResult = (OkNegotiatedContentResult<bool>)_apiControllerMock.Invoke("Process", workMock);

            //Assert.IsNotNull(actionResult);
            //Assert.AreEqual(expectedResult, actionResult.Content);
        }

        [Test]
        public void Test_Process_Fail()
        {
            InitRequest(false);
            Func<IHttpActionResult> workMock = () => new OkResult(new MockApiController());
            //ErrorResult actionResult = (ErrorResult)_apiControllerMock.Invoke("Process", workMock);

            //Assert.IsNotNull(actionResult);
        }

        [Test]
        public void Test_Error_Success_UnauthorizedAccessException()
        {
            InitRequest(false);
            string expectedErrorMessage = "unauthorized";
            UnauthorizedAccessException expectedException = new UnauthorizedAccessException(expectedErrorMessage);
            ErrorResult expectedResult =
                new ErrorResult(new HttpRequestMessage(),
                                new ApiErrorModel
                                {
                                    Messages = new ErrorMessageModel[]
                                    {
                                        new ErrorMessageModel { Message = expectedException.Message }
                                    },
                                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                                });

            //ErrorResult actionResult = (ErrorResult)_apiControllerMock.Invoke("Error", expectedException);

            //Assert.IsNotNull(actionResult);
            //Assert.IsNotNull(actionResult.Request);
            //Assert.IsNotNull(actionResult.Error);
            //Assert.AreEqual(expectedResult.Error.ErrorCode, actionResult.Error.ErrorCode);
            //Assert.AreEqual(expectedResult.Error.StatusCode, actionResult.Error.StatusCode);
            //Assert.IsNotNull(actionResult.Error.Messages);
            //Assert.IsTrue(actionResult.Error.Messages.SequenceEqual(expectedResult.Error.Messages, new MessageComparer()));
        }

        [Test]
        public void Test_Error_Success_ArgumentException()
        {
            InitRequest(false);
            string expectedParamName = "arg01";
            string expectedErrorMessage = "argument invalid";
            ArgumentException expectedException = new ArgumentException(expectedErrorMessage, expectedParamName);
            ErrorResult expectedResult =
                new ErrorResult(new HttpRequestMessage(),
                                new ApiErrorModel
                                {
                                    Messages = new ErrorMessageModel[]
                                    {
                                        new ErrorMessageModel
                                        {
                                            Key = expectedException.ParamName,
                                            Message = expectedException.Message
                                        }
                                    },
                                    StatusCode = System.Net.HttpStatusCode.BadRequest
                                });

            //ErrorResult actionResult = (ErrorResult)_apiControllerMock.Invoke("Error", expectedException);

            //Assert.IsNotNull(actionResult);
            //Assert.IsNotNull(actionResult.Request);
            //Assert.IsNotNull(actionResult.Error);
            //Assert.AreEqual(expectedResult.Error.ErrorCode, actionResult.Error.ErrorCode);
            //Assert.AreEqual(expectedResult.Error.StatusCode, actionResult.Error.StatusCode);
            //Assert.IsNotNull(actionResult.Error.Messages);
            //Assert.IsTrue(actionResult.Error.Messages.SequenceEqual(expectedResult.Error.Messages, new MessageComparer()));
        }

        [Test]
        public void Test_Error_Success_ChannelOperationException()
        {
            InitRequest(false);
            int expectedErrorCode = 69;
            string expectedErrorMessage = "channel cannot send";
            ChannelOperationException expectedException = new ChannelOperationException(expectedErrorCode, expectedErrorMessage);
            ErrorResult expectedResult =
                new ErrorResult(new HttpRequestMessage(),
                                new ApiErrorModel
                                {
                                    Messages = new ErrorMessageModel[]
                                    {
                                        new ErrorMessageModel { Message = expectedException.Message }
                                    },
                                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                                    ErrorCode = expectedErrorCode
                                });

            //ErrorResult actionResult = (ErrorResult)_apiControllerMock.Invoke("Error", expectedException);

            //Assert.IsNotNull(actionResult);
            //Assert.IsNotNull(actionResult.Request);
            //Assert.IsNotNull(actionResult.Error);
            //Assert.AreEqual(expectedResult.Error.ErrorCode, actionResult.Error.ErrorCode);
            //Assert.AreEqual(expectedResult.Error.StatusCode, actionResult.Error.StatusCode);
            //Assert.IsNotNull(actionResult.Error.Messages);
            //Assert.IsTrue(actionResult.Error.Messages.SequenceEqual(expectedResult.Error.Messages, new MessageComparer()));
        }

        [Test]
        public void Test_Error_Success_ChannelOperationException_ErrorCode4()
        {
            InitRequest(false);
            int expectedErrorCode = 4;
            string expectedErrorMessage = "channel cannot send";
            List<ErrorMessageModel> expectedErrorMessages = new List<ErrorMessageModel>
            {
                new ErrorMessageModel { Key = "key", Message = expectedErrorMessage, Type = 20 }
            };
            ChannelOperationException expectedException =
                new ChannelOperationException(expectedErrorCode, JsonConvert.SerializeObject(expectedErrorMessages));
            ErrorResult expectedResult =
                new ErrorResult(new HttpRequestMessage(),
                                new ApiErrorModel
                                {
                                    Messages = expectedErrorMessages.ToArray(),
                                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                                    ErrorCode = expectedErrorCode
                                });

            //ErrorResult actionResult = (ErrorResult)_apiControllerMock.Invoke("Error", expectedException);

            //Assert.IsNotNull(actionResult);
            //Assert.IsNotNull(actionResult.Request);
            //Assert.IsNotNull(actionResult.Error);
            //Assert.AreEqual(expectedResult.Error.ErrorCode, actionResult.Error.ErrorCode);
            //Assert.AreEqual(expectedResult.Error.StatusCode, actionResult.Error.StatusCode);
            //Assert.IsNotNull(actionResult.Error.Messages);
            //Assert.IsTrue(actionResult.Error.Messages.SequenceEqual(expectedResult.Error.Messages, new MessageComparer()));
        }

        [Test]
        public void Test_Error_Success_DbEntityValidationException()
        {
            InitRequest(false);
            string expectedParamName = "arg01";
            string expectedErrorMessage = "database failed";
            List<DbEntityValidationResult> expectedErrorMessages = new List<DbEntityValidationResult>
            {
                new DbEntityValidationResult(
                    new MockDbContext().Entry(new Poco1()),
                    new List<DbValidationError> { new DbValidationError(expectedParamName, expectedErrorMessage) })
            };
            DbEntityValidationException expectedException =
                new DbEntityValidationException(expectedErrorMessage, expectedErrorMessages);
            ErrorResult expectedResult =
                new ErrorResult(new HttpRequestMessage(),
                                new ApiErrorModel
                                {
                                    Messages = expectedErrorMessages.SelectMany(e => e.ValidationErrors)
                                                                    .Select(e => new ErrorMessageModel
                                                                    {
                                                                        Key = e.PropertyName,
                                                                        Message = e.ErrorMessage
                                                                    })
                                                                    .ToArray(),
                                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                                });

            //ErrorResult actionResult = (ErrorResult)_apiControllerMock.Invoke("Error", expectedException);

            //Assert.IsNotNull(actionResult);
            //Assert.IsNotNull(actionResult.Request);
            //Assert.IsNotNull(actionResult.Error);
            //Assert.AreEqual(expectedResult.Error.ErrorCode, actionResult.Error.ErrorCode);
            //Assert.AreEqual(expectedResult.Error.StatusCode, actionResult.Error.StatusCode);
            //Assert.IsNotNull(actionResult.Error.Messages);
            //Assert.IsTrue(actionResult.Error.Messages.SequenceEqual(expectedResult.Error.Messages, new MessageComparer()));
        }

        [Test]
        public void Test_Error_Success_AnotherException()
        {
            InitRequest(false);
            string expectedErrorMessage = "something went wrong";
            Exception expectedException = new Exception(expectedErrorMessage);
            ErrorResult expectedResult =
                new ErrorResult(new HttpRequestMessage(),
                                new ApiErrorModel
                                {
                                    Messages = new ErrorMessageModel[]
                                    {
                                        new ErrorMessageModel { Message = expectedException.Message }
                                    },
                                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                                });

            //ErrorResult actionResult = (ErrorResult)_apiControllerMock.Invoke("Error", expectedException);

            //Assert.IsNotNull(actionResult);
            //Assert.IsNotNull(actionResult.Request);
            //Assert.IsNotNull(actionResult.Error);
            //Assert.AreEqual(expectedResult.Error.ErrorCode, actionResult.Error.ErrorCode);
            //Assert.AreEqual(expectedResult.Error.StatusCode, actionResult.Error.StatusCode);
            //Assert.IsNotNull(actionResult.Error.Messages);
            //Assert.IsTrue(actionResult.Error.Messages.SequenceEqual(expectedResult.Error.Messages, new MessageComparer()));
        }

        [Test]
        public void Test_Prop_ApiKey_Exception_NoApiKey()
        {
            InitRequest();
            //((MockApiController)_apiControllerMock.Target).Request.Headers.Authorization = null;

            //Assert.Throws(Is.TypeOf<ArgumentException>(), delegate { _apiControllerMock.GetProperty("ApiKey"); });
        }

        [Test]
        public void Test_Prop_AccountId_Exception_NoAccountId()
        {
            InitRequest();
            //((MockApiController)_apiControllerMock.Target).Request.Headers.Remove("Account-Id");

            //Assert.Throws(Is.TypeOf<ArgumentException>(), delegate { _apiControllerMock.GetProperty("AccountId"); });
        }

        private void InitRequest(bool forSuccess = true)
        {
            HttpRequestMessage request = new HttpRequestMessage();

            if (forSuccess)
            {
                Mock<HttpRequestBase> httpRequestMock = new Mock<HttpRequestBase>();
                httpRequestMock.Setup(r => r.UserHostAddress).Returns("127.0.0.1");

                Mock<HttpContextBase> httpContextMock = new Mock<HttpContextBase>();
                httpContextMock.Setup(c => c.Request).Returns(httpRequestMock.Object);

                request.Headers.Authorization = new AuthenticationHeaderValue(_apiSettings.ApiKey);
                request.Headers.Add("Account-Id", _apiSettings.AccountId);
                request.Properties.Add("MS_HttpContext", httpContextMock.Object);
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("UnitTest", "1.0.0.0")));

                AppDomain.CurrentDomain.SetData("DataDirectory", _apiSettings.ChannelPluginsPath);
            }

            //((MockApiController)_apiControllerMock.Target).Request = request;
        }

        private class MessageComparer : IEqualityComparer<ErrorMessageModel>
        {
            public bool Equals(ErrorMessageModel x, ErrorMessageModel y)
            {
                return x.Key == y.Key && x.Message == y.Message && x.Type == y.Type;
            }

            public int GetHashCode(ErrorMessageModel obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
