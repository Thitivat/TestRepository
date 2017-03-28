using BND.Services.Security.OTP.ClientProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BND.Services.Security.OTP.ClientProxyTest
{
    [TestFixture]
    public class ResourceBaseTest
    {
        private string _apiKey = "apiKey";
        private string _accountId = "accountId";
        private string _baseAddress = "http://test.org";

        private Model1 _model = new Model1
        {
            Id = 20,
            Names = new string[] { "Fuck", "you" },
            Scores = new List<int> { 12, 548, 89 },
            Model = new Model2 { Id = 44, Name = "JJJJ" },
            Models = new Model3[]
            {
                new Model3 { Code = 22, BirthDate = DateTime.Now.AddYears(-10) },
                new Model3 { Code = 33, BirthDate = DateTime.Now.AddYears(-20) }
            },
            Structs = new List<Struct1>
            {
                new Struct1 { Key = "Key01", Value = "xxxxx" },
                new Struct1 { Key = "Key02", Value = 20d }
            },
            Dict = new Dictionary<string, Model4>
            {
                { "Dict1", new Model4 { Val = 45.856, Int = 456 } },
                { "Dict2", new Model4 { Val = 45.856, Int = 456 } },
                { "Dict3", new Model4 { Val = 45.856, Int = 456 } }
            }
        };

        [Test]
        public void GetHttpClient_Should_ReturnHttpClient_When_UseApiAddressWithoutSlash()
        {
            MockResourceBase resourceBase = new MockResourceBase("http://test.org", _apiKey, _accountId);
            HttpClient httpClient = resourceBase.GetHttpClientTest();

            Assert.AreEqual("http://test.org/api/", httpClient.BaseAddress.AbsoluteUri);
            Assert.AreEqual(_apiKey, httpClient.DefaultRequestHeaders.Authorization.Scheme);
            Assert.AreEqual(_accountId, httpClient.DefaultRequestHeaders.Where(x => x.Key == "Account-Id").First().Value.First());
        }

        [Test]
        public void GetHttpClient_Should_ReturnHttpClient_When_UseApiAddressWithSlash()
        {
            MockResourceBase resourceBase = new MockResourceBase("http://test.org/", _apiKey, _accountId);
            HttpClient httpClient = resourceBase.GetHttpClientTest();

            Assert.AreEqual("http://test.org/api/", httpClient.BaseAddress.AbsoluteUri);
            Assert.AreEqual(_apiKey, httpClient.DefaultRequestHeaders.Authorization.Scheme);
            Assert.AreEqual(_accountId, httpClient.DefaultRequestHeaders.Where(x => x.Key == "Account-Id").First().Value.First());
        }

        [Test]
        public void Test_MapModelToKeyValuePairs_Success()
        {
            MockClass cl = new MockClass(_baseAddress, _apiKey, _accountId);
            cl.MapModelTest(_model);
        }

        public class Model1
        {
            public int Id { get; set; }
            public string[] Names { get; set; }
            public List<int> Scores { get; set; }
            public Model2 Model { get; set; }
            public Model3[] Models { get; set; }
            public List<Struct1> Structs { get; set; }
            public Dictionary<string, Model4> Dict { get; set; }
        }

        public class Model2
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Model3
        {
            public int Code { get; set; }
            public DateTime BirthDate { get; set; }
        }

        public struct Struct1
        {
            public string Key { get; set; }
            public object Value { get; set; }
        }

        public class Model4
        {
            public double Val { get; set; }
            public int Int { get; set; }
        }

        public class MockResourceBase : ResourceBase
        {
            public MockResourceBase(string baseAddress, string apiKey, string accountId)
                : base(baseAddress, apiKey, accountId)
            { }

            public HttpClient GetHttpClientTest()
            {
                return base.GetHttpClient();
            }
        }
    }
}
