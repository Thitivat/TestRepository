using BND.Services.Security.OTP.Api.Controllers;
using BND.Services.Security.OTP.Api.Utils;
//using MsTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace BND.Services.Security.OTP.ApiUnitTest
{
    [TestFixture]
    public class ChannelsControllerTest
    {
        [Test]
        public void Test_Get_Success()
        {
            List<string> expectedResult = new List<string> { "Channel1", "Channel2" };

            Mock<IApiManager> apiManager = new Mock<IApiManager>();
            apiManager.Setup(a => a.VerifyAccount());
            apiManager.Setup(a => a.GetChannelNames()).Returns(expectedResult);

            //MsTest.PrivateObject channelsController = new MsTest.PrivateObject(new ChannelsController(), new MsTest.PrivateType(typeof(ApiControllerBase)));
            //channelsController.SetField("ApiManager", apiManager.Object);

            //IHttpActionResult actionResult = ((ChannelsController)channelsController.Target).Get();
            //var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<string>>;

            //Assert.IsNotNull(contentResult);
            //Assert.IsTrue(contentResult.Content.SequenceEqual(expectedResult));
        }
    }
}
