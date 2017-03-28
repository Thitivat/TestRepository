using BND.Services.Security.OTP.Api.Utils;
using BND.Services.Security.OTP.Models;
using NUnit.Framework;
using System;
using System.Net.Http;

namespace BND.Services.Security.OTP.ApiUnitTest
{
    [TestFixture]
    public class ErrorResultTest
    {
        [Test]
        public void Test_Ctor_Success()
        {
            HttpRequestMessage expectedHttpRequest = new HttpRequestMessage();
            ApiErrorModel expectedErrorModel = new ApiErrorModel
            {
                ErrorCode = 2,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Messages = new ErrorMessageModel[]
                {
                    new ErrorMessageModel { Key = "key01", Message = "msg01", Type = 0 },
                    new ErrorMessageModel { Key = "key02", Message = "msg02", Type = 1 }
                }
            };

            ErrorResult actual = new ErrorResult(expectedHttpRequest, expectedErrorModel);

            Assert.AreEqual(expectedHttpRequest, actual.Request);
            Assert.AreEqual(expectedErrorModel, actual.Error);
        }

        [Test]
        public void Test_Ctor_Exception_RequestNull()
        {
            Assert.Throws(Is.TypeOf<ArgumentNullException>(), delegate { new ErrorResult(null, null); });
        }

        [Test]
        public void Test_Ctor_Exception_ErrorModelNull()
        {
            Assert.Throws(Is.TypeOf<ArgumentNullException>(), delegate { new ErrorResult(new HttpRequestMessage(), null); });
        }

        [Test]
        public void Test_Ctor_Exception_ErrorModelMessagesNull()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>(), delegate { new ErrorResult(new HttpRequestMessage(), new ApiErrorModel()); });
        }

        [Test]
        public void Test_Ctor_Exception_ErrorModelMessagesEmpty()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>(), delegate { new ErrorResult(new HttpRequestMessage(), new ApiErrorModel { Messages = new ErrorMessageModel[] { } }); });
        }

        [Test]
        public void Test_Ctor_Exception_ErrorModelStatusCodeOk()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>(), delegate
            {
                new ErrorResult(new HttpRequestMessage(),
                    new ApiErrorModel
                    {
                        Messages = new ErrorMessageModel[] { new ErrorMessageModel() },
                        StatusCode = System.Net.HttpStatusCode.OK
                    });
            });
        }

        [Test]
        public void Test_ExecuteAsync_Success()
        {
            HttpRequestMessage expectedHttpRequest = new HttpRequestMessage();
            ApiErrorModel expectedErrorModel = new ApiErrorModel
            {
                ErrorCode = 2,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Messages = new ErrorMessageModel[]
                {
                    new ErrorMessageModel { Key = "key01", Message = "msg01", Type = 0 },
                    new ErrorMessageModel { Key = "key02", Message = "msg02", Type = 1 }
                }
            };

            ErrorResult actual = new ErrorResult(expectedHttpRequest, expectedErrorModel);
            HttpResponseMessage actualResponse = actual.ExecuteAsync(new System.Threading.CancellationToken()).Result;

            Assert.AreEqual(expectedErrorModel.StatusCode, actualResponse.StatusCode);
            ApiErrorModel actualErrorModel;
            Assert.IsTrue(actualResponse.TryGetContentValue(out actualErrorModel));
            Assert.AreEqual(expectedErrorModel, actualErrorModel);
            Assert.AreEqual(expectedHttpRequest, actualResponse.RequestMessage);
        }
    }
}
