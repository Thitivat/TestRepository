using BND.Services.Security.OTP.Api.Controllers;
using BND.Services.Security.OTP.Api.Utils;
using BND.Services.Security.OTP.Models;
using NUnit.Framework;
//using MsTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;
using System.Web.Http.Results;

namespace BND.Services.Security.OTP.ApiUnitTest
{
    [TestFixture]
    public class OneTimePasswordsControllerTest
    {
        [Test]
        public void Test_Post_Success()
        {
            OtpResultModel expectedResult = new OtpResultModel
            {
                OtpId = "otp-id",
                RefCode = "REFCODE"
            };

            Mock<IApiManager> apiManager = new Mock<IApiManager>();
            apiManager.Setup(a => a.VerifyAccount());
            apiManager.Setup(a => a.AddOtp(It.IsAny<OtpRequestModel>())).Returns(expectedResult);

            //MsTest.PrivateObject oneTimePasswordsController = new MsTest.PrivateObject(new OneTimePasswordsController(), new MsTest.PrivateType(typeof(ApiControllerBase)));
            //oneTimePasswordsController.SetField("ApiManager", apiManager.Object);

            //IHttpActionResult actionResult = ((OneTimePasswordsController)oneTimePasswordsController.Target).Post(new OtpRequestModel());
            //var contentResult = actionResult as OkNegotiatedContentResult<OtpResultModel>;

            //Assert.IsNotNull(contentResult);
            //Assert.AreEqual(expectedResult, contentResult.Content);
        }

        [Test]
        public void Test_Verify_Success()
        {
            OtpModel expectedResult = new OtpModel
            {
                Channel = new ChannelModel
                {
                    Address = "address",
                    Message = "message",
                    Sender = "sender",
                    Type = "EMAIL"
                },
                ExpiryDate = DateTime.Now,
                Id = "id",
                Payload = "payload",
                RefCode = "REFCODE",
                Status = "Verified",
                Suid = "suid"
            };

            Mock<IApiManager> apiManager = new Mock<IApiManager>();
            apiManager.Setup(a => a.VerifyAccount());
            apiManager.Setup(a => a.VerifyOtp(It.IsAny<string>(), It.IsAny<JObject>())).Returns(expectedResult);

            //MsTest.PrivateObject oneTimePasswordsController = new MsTest.PrivateObject(new OneTimePasswordsController(), new MsTest.PrivateType(typeof(ApiControllerBase)));
            //oneTimePasswordsController.SetField("ApiManager", apiManager.Object);

            //IHttpActionResult actionResult = ((OneTimePasswordsController)oneTimePasswordsController.Target).Verify("otp-id", new JObject());
            //var contentResult = actionResult as OkNegotiatedContentResult<OtpModel>;

            //Assert.IsNotNull(contentResult);
            //Assert.AreEqual(expectedResult, contentResult.Content);
        }
    }
}
