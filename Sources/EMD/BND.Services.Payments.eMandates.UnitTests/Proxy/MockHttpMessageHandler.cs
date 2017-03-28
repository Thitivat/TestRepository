using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.UnitTests.Proxy
{
    public class MockHttpMessageHandler : DelegatingHandler
    {
        private Uri _baseUri;
        private Dictionary<Uri, HttpResponseMessage> _mockResponses = new Dictionary<Uri, HttpResponseMessage>();

        public MockHttpMessageHandler(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        public void AddMockResponse<TExpectedData>(string relativeUri, HttpStatusCode expectedStatusCode, TExpectedData expectedResponseData)
        {
            HttpResponseMessage expectedResponse = new HttpResponseMessage();
            expectedResponse.StatusCode = expectedStatusCode;
            expectedResponse.Content = new StringContent(JsonConvert.SerializeObject(expectedResponseData), Encoding.UTF8, "application/json");

            Uri uri = new Uri(_baseUri, relativeUri);

            // Add the same key will replate the old one.
            if (_mockResponses.ContainsKey(uri))
            {
                _mockResponses.Remove(uri);
            }

            _mockResponses.Add(uri, expectedResponse);
        }

        public void AddMockResponseHeader<TExpectedData>(string relativeUri, HttpStatusCode expectedStatusCode, string expectedHeaderLocation, TExpectedData expectedResponseData)
        {
            HttpResponseMessage expectedResponse = new HttpResponseMessage();
            expectedResponse.StatusCode = expectedStatusCode;
            expectedResponse.Headers.Location = new Uri(_baseUri, expectedHeaderLocation);
            if (expectedResponseData != null)
            {
                expectedResponse.Content = new StringContent(JsonConvert.SerializeObject(expectedResponseData), Encoding.UTF8, "application/json");
            }

            _mockResponses.Add(new Uri(_baseUri, relativeUri), expectedResponse);
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_mockResponses.ContainsKey(request.RequestUri))
            {
                return _mockResponses[request.RequestUri];
            }
            else
            {
                throw new NotSupportedException(String.Format("[{0}]: Does not support '{1}' url.", this.GetType().Name, request.RequestUri));
            }
        }
    }
}
