using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Proxy.NET4.Interfaces;

using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Extensions.MonoHttp;

namespace BND.Services.Matrix.Proxy.NET4.Implementations
{
    public class CenterApi : ICenterApi
    {
        private const string Version = "v1";

        private readonly RestClient _client;

        private readonly JsonDeserializer _deserializer = new JsonDeserializer();

        /// <summary>
        /// Initializes a new instance of the <see cref="CenterApi"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public CenterApi(IApiConfig config)
        {
            _client = new RestClient(string.Format("{0}/{1}/", config.ServiceUrl, Version));

            _client.AddHandler("application/json", _deserializer);
        }

        /// <summary>
        /// Ping service
        /// </summary>
        /// <param name="accessToken">The access token</param>
        /// <returns>The specified <see cref="string"/> entity</returns>
        public string GetCurrentCenter(string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest("centers/current", Method.GET);
            var response = _client.Execute(request);

            if (string.IsNullOrWhiteSpace(response.Content))
            {
                throw new ProxyException(
                       new Error
                       {
                           Title = typeof(ProxyException).FullName,
                           Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                           StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                           StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                           Message = response.ErrorMessage,
                           Code = (int)EnumErrorCodes.ProxyLayerError
                       },
                       typeof(EnumErrorCodes));
            }

            var currentCenter = response.Content.Replace("\"", string.Empty);
            
            return currentCenter;
        }

        /// <summary>
        /// The throw if token empty.
        /// </summary>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <exception cref="ProxyException">
        /// </exception>
        private void ThrowIfTokenEmpty(string accessToken)
        {
            // Get calling method name
            var callingMethodName = new StackTrace().GetFrame(1).GetMethod().Name;

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ProxyException(
                    new Error
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", callingMethodName, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = "Please provide a valid access token",
                        Code = (int)EnumErrorCodes.ProxyLayerError
                    },
                    typeof(EnumErrorCodes));
            }
        }
    }
}
