using BND.Services.IbanStore.Api.Models;
using BND.Services.IbanStore.Models;
using System;
using System.Net.Http;
using NUnit.Framework;

namespace BND.Services.IbanStore.ApiTest
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
                Message = "msg01"
            };

            ErrorResult actual = new ErrorResult(expectedHttpRequest, expectedErrorModel);

            Assert.AreEqual(expectedHttpRequest, actual.Request);
            Assert.AreEqual(expectedErrorModel, actual.Error);
        }

        [Test]
        public void Test_Ctor_Exception_RequestNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
          {
              new ErrorResult(null, null);
          });
        }

        [Test]
        public void Test_Ctor_Exception_ErrorModelNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
           {
               new ErrorResult(new HttpRequestMessage(), null);
           });
        }

        [Test]
        public void Test_Ctor_Exception_ErrorModelMessagesNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
           {
               new ErrorResult(new HttpRequestMessage(), new ApiErrorModel());
           });
        }

        [Test]
        public void Test_Ctor_Exception_ErrorModelMessagesEmpty()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ErrorResult(new HttpRequestMessage(), new ApiErrorModel { Message = { } });
            });
        }

        [Test]
        public void Test_Ctor_Exception_ErrorModelStatusCodeOk()
        {

            Assert.Throws<ArgumentException>(() =>
            {
                new ErrorResult(new HttpRequestMessage(),
                            new ApiErrorModel
                            {
                                Message = "msg01",
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
                Message = "msg01"
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
