using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Infrastructure.Proxies;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Proxy.Interfaces;

namespace BND.Services.Matrix.Proxy.Implementations
{
    /// <summary>
    /// The CoreApi interface.
    /// </summary>
    public class CoreApi : ICoreApi
    {
        /// <summary>
        /// The version.
        /// </summary>
        private const string Version = "v1";

        /// <summary>
        /// The _formatters.
        /// </summary>
        private readonly List<MediaTypeFormatter> _formatters;

        /// <summary>
        /// The _config.
        /// </summary>
        private readonly IApiConfig _config;

        /// <summary>
        /// The _exception handler.
        /// </summary>
        private readonly ProxyExceptionHandler _exceptionHandler = new ProxyExceptionHandler();

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreApi"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public CoreApi(IApiConfig config)
        {
            _config = config;
            _formatters = new List<MediaTypeFormatter>() { new Infrastructure.Proxies.Json.BaseJsonMediaTypeFormatter() };
        }

        /// <summary>
        /// Ping service
        /// </summary>
        /// <param name="accessToken">The access token</param>
        /// <returns>The specified <see cref="ServiceInfo"/> entity</returns>
        public virtual async Task<ServiceInfo> Ping(string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);

                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, Version));

                    var response = await client.GetAsync("ping").ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var serviceInfo = response.Content.ReadAsAsync<ServiceInfo>(_formatters).Result;

                    if (serviceInfo == null)
                    {
                        throw new ProxyException(
                            new Error
                            {
                                Title = typeof(ProxyException).FullName,
                                Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                Message = "The service info could not be read from the response content!",
                                Code = (int)EnumErrorCodes.ProxyLayerError
                            },
                            typeof(EnumErrorCodes));
                    }

                    return serviceInfo;
                }
                catch (HttpRequestException re)
                {
                    throw new ProxyException(
                        new Error()
                        {
                            Title = typeof(ProxyException).FullName,
                            Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                            StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                            StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                            Message = "An HttpRequestException occured, please see inner error for details.",
                            Code = (int)EnumErrorCodes.ProxyLayerError
                        },
                        re,
                        typeof(EnumErrorCodes));
                }
            }
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
