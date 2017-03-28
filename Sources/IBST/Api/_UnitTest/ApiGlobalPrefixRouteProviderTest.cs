using BND.Services.IbanStore.Api;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using NUnit.Framework;

namespace BND.Services.IbanStore.ApiTest
{
    [TestFixture]
    public class ApiGlobalPrefixRouteProviderTest
    {
        [Test]
        public void Test_Ctor_Success()
        {
            string expectedPrefix = "api";
            //var prefixRouteProvider = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(new ApiGlobalPrefixRouteProvider(expectedPrefix));

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

            //var prefixRouteProvider = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(new ApiGlobalPrefixRouteProvider(expectedPrefix));

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

            //var prefixRouteProvider = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(new ApiGlobalPrefixRouteProvider(expectedPrefix));

            //Assert.AreEqual(expectedRoutePrefix, (string)prefixRouteProvider.Invoke("GetRoutePrefix", expectedControllerDesc));
        }
    }
}
