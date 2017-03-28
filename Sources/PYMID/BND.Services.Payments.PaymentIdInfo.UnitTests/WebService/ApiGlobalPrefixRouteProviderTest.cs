using NUnit.Framework;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BND.Services.Payments.PaymentIdInfo.WebService.Test
{
    [RoutePrefix("TestPrefix")]
    public class MockApiController : ApiController
    {
        public MockApiController()
        {
        }

        [Route("TestGet")]
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }

    public class TestApiGlobalPrefixRouteProvider : ApiGlobalPrefixRouteProvider
    {
        public string GetRoute(HttpControllerDescriptor controllerDescriptor)
        {
            return base.GetRoutePrefix(controllerDescriptor);
        }

        public TestApiGlobalPrefixRouteProvider(string globalPrefix)
            : base(globalPrefix)
        {

        }
    }

    [TestFixture]
    public class ApiGlobalPrefixRouteProviderTest
    {
        [Test]
        public void GetRoutePrefix_Should_ReturnExpectedRoutePrefix_When_SuccessWithRoutePrefix()
        {
            string expectedPrefix = "testapi";
            Type expectedControllerType = typeof(MockApiController);
            HttpControllerDescriptor expectedControllerDescriptor = new HttpControllerDescriptor(
                new HttpConfiguration(), expectedControllerType.Name.TrimEnd("Controller".ToCharArray()),
                expectedControllerType);
            string expectedRoutePrefix = String.Format("{0}/{1}", expectedPrefix,
                ((RoutePrefixAttribute)
                    expectedControllerType.GetCustomAttributes(typeof(RoutePrefixAttribute), false).First()).Prefix);

            var routeProvider = new TestApiGlobalPrefixRouteProvider(expectedPrefix);

            Assert.AreEqual(expectedRoutePrefix, routeProvider.GetRoute(expectedControllerDescriptor));
        }

        [Test]
        public void GetRoutePrefix_Should_ReturnNull_When_SuccessWithoutRoutePrefix()
        {
            string expectedPrefix = "testapi";
            Type expectedControllerType = typeof(ApiController);
            HttpControllerDescriptor expectedControllerDescriptor = new HttpControllerDescriptor(
                new HttpConfiguration(), expectedControllerType.Name.TrimEnd("Controller".ToCharArray()),
                expectedControllerType);
            string expectedRoutePrefix = String.Format("{0}/{1}", expectedPrefix,
                expectedControllerType.Name.TrimEnd("Controller".ToCharArray()));
            var routeProvider = new TestApiGlobalPrefixRouteProvider(expectedPrefix);

            Assert.AreEqual(expectedRoutePrefix, routeProvider.GetRoute(expectedControllerDescriptor));
        }
    }

}
