using BND.Services.IbanStore.Api.Helpers;
using BND.Services.IbanStore.Models;
using BND.Services.IbanStore.Service;
using System;
using System.IO;
using System.Net.Http;
using NUnit.Framework;

namespace BND.Services.IbanStore.ApiTest
{
    [TestFixture]
    public class ErrorHelperTest
    {
        [Test]
        public void Test_CreateErrorResponseMessage_Success()
        {
            ApiErrorModel expected = new ApiErrorModel
            {
                Message = "error",
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };

            HttpResponseMessage actualResponse = Errors.CreateErrorResponseMessage(new Exception(expected.Message));
            ApiErrorModel actual = actualResponse.Content.ReadAsAsync<ApiErrorModel>().Result;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ErrorCode, actual.ErrorCode);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [Test]
        public void Test_CreateErrorModel_Success_Unauthorized()
        {
            ApiErrorModel expected = new ApiErrorModel
            {
                Message = "error",
                StatusCode = System.Net.HttpStatusCode.Unauthorized
            };

            ApiErrorModel actual = Errors.CreateErrorModel(new UnauthorizedAccessException(expected.Message));

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ErrorCode, actual.ErrorCode);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [Test]
        public void Test_CreateErrorModel_Success_BadRequest()
        {
            ApiErrorModel expected = new ApiErrorModel
            {
                Message = "error",
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            ApiErrorModel actual = Errors.CreateErrorModel(new ArgumentException(expected.Message));

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ErrorCode, actual.ErrorCode);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [Test]
        public void Test_CreateErrorModel_Success_BadRequestWithErrorCode()
        {
            ApiErrorModel expected = new ApiErrorModel
            {
                ErrorCode = 99,
                Message = "error",
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            ApiErrorModel actual = Errors.CreateErrorModel(new IbanOperationException(expected.ErrorCode, expected.Message));

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ErrorCode, actual.ErrorCode);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }

        [Test]
        public void Test_CreateErrorModel_Success_NotFound()
        {
            ApiErrorModel expected = new ApiErrorModel
            {
                Message = "error",
                StatusCode = System.Net.HttpStatusCode.NotFound
            };

            ApiErrorModel actual = Errors.CreateErrorModel(new FileNotFoundException(expected.Message));

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ErrorCode, actual.ErrorCode);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.StatusCode, actual.StatusCode);
        }
    }
}
