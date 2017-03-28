using BND.Services.Security.OTP.Api.Utils;
//using MsTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BND.Services.Security.OTP.ApiUnitTest
{
    [TestFixture]
    public class ApiGlobalPrefixRouteProviderTest
    {
        [Test]
        public void Test_Ctor_Success()
        {
            string expectedPrefix = "api";
            //MsTest.PrivateObject prefixRouteProvider = new MsTest.PrivateObject(new ApiGlobalPrefixRouteProvider(expectedPrefix));

            //Assert.AreEqual(expectedPrefix, (string)prefixRouteProvider.GetField("_globalPrefix"));
        }

        [Test]
        public void Test_GetRoutePrefix_Success_WihtRoutePrefix()
        {
            string expectedPrefix = "api";
            Type expectedControllerType = typeof(MockApiController);
            HttpControllerDescriptor expectedControllerDesc =
                new HttpControllerDescriptor(new System.Web.Http.HttpConfiguration(),
                                             expectedControllerType.Name.TrimEnd("Controller".ToCharArray()),
                                             expectedControllerType);
            string expectedRoutePrefix =
                String.Format("{0}/{1}", expectedPrefix,
                              ((RoutePrefixAttribute)expectedControllerType.GetCustomAttributes(typeof(RoutePrefixAttribute), false)
                                                                           .First())
                              .Prefix);

            //MsTest.PrivateObject prefixRouteProvider = new MsTest.PrivateObject(new ApiGlobalPrefixRouteProvider(expectedPrefix));

            //Assert.AreEqual(expectedRoutePrefix, (string)prefixRouteProvider.Invoke("GetRoutePrefix", expectedControllerDesc));
        }

        [Test]
        public void Test_GetRoutePrefix_Success_WihtoutRoutePrefix()
        {
            string expectedPrefix = "api";
            Type expectedControllerType = typeof(ApiController);
            HttpControllerDescriptor expectedControllerDesc =
                new HttpControllerDescriptor(new System.Web.Http.HttpConfiguration(),
                                             expectedControllerType.Name.TrimEnd("Controller".ToCharArray()),
                                             expectedControllerType);
            string expectedRoutePrefix =
                String.Format("{0}/{1}", expectedPrefix, expectedControllerType.Name.TrimEnd("Controller".ToCharArray()));

            //MsTest.PrivateObject prefixRouteProvider = new MsTest.PrivateObject(new ApiGlobalPrefixRouteProvider(expectedPrefix));

            //Assert.AreEqual(expectedRoutePrefix, (string)prefixRouteProvider.Invoke("GetRoutePrefix", expectedControllerDesc));
        }
    }
}
