using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Infrastructure.Proxies;
using BND.Services.MailMan.Entities;
using BND.Services.MailMan.Proxy.Interfaces;

using BaseJsonMediaTypeFormatter = BND.Services.Infrastructure.Proxies.Json.BaseJsonMediaTypeFormatter;

namespace BND.Services.MailMan.Proxy.Implementations
{
    /// <summary>
    /// The MailManApi <see langword="class"/> is the proxy to the MailMan service
    /// </summary>
    public class MailManApi : IMailManApi
    {
        /// <summary>
        /// The API version
        /// </summary>
        private const string Version = "v1";

        /// <summary>
        /// The <see cref="IMailManApiConfig"/>
        /// </summary>
        private readonly IMailManApiConfig _config;

        /// <summary>
        /// A <see cref="List{MediaTypeFormatter}"/>
        /// </summary>
        private readonly List<MediaTypeFormatter> _formatters;

        /// <summary>
        /// The _exceptionHandler
        /// </summary>
        public ProxyExceptionHandler _exceptionHandler = new ProxyExceptionHandler();

        /// <summary>
        /// Initializes a new instance of the <see cref="MailManApi"/> class.
        /// </summary>
        /// <param name="configuration"> The <see cref="IMailManApiConfig"/> configuration. </param>
        public MailManApi(IMailManApiConfig configuration)
        {
            _config = configuration;
            _formatters = new List<MediaTypeFormatter>() { new BaseJsonMediaTypeFormatter() };
        }

        /// <summary>
        /// Sends an mail asynchronously
        /// </summary>
        /// <param name="message"> The <see cref="Message"/> to sent </param>
        /// <param name="accessToken"> The access token. </param>
        /// <returns>
        /// The generated message id as a task of <see langword="int"/>
        /// </returns>
        public virtual async Task<int> SendAsync(Message message, string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ProxyException(
                    new Error()
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = "Please provide a valid access token",
                        Code = (int)EnumErrorCodes.ProxyLayerError
                    },
                    typeof(EnumErrorCodes));
            }

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);

                    var response =
                        await client.PostAsJsonAsync(new Uri(string.Format("{0}/{1}", _config.ServiceUrl, Version)), message).ConfigureAwait(false);


                if (response.StatusCode != HttpStatusCode.Created)
                {
                    throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                }


                    if (response.Headers.Location == null)
                    {
                        throw new ProxyException(
                            new Error
                                {
                                    Title = typeof(ProxyException).FullName,
                                    Source = string.Format("An error occurred at method '{0}' of '{1}'!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                    StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                    StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                    Message = "Location header not found in response.",
                                    Code = (int)EnumErrorCodes.ProxyLayerError
                                },
                            typeof(EnumErrorCodes));
                    }

                    var messageId = response.Headers.Location.ToString().Split('/').Last();

                    if (string.IsNullOrWhiteSpace(messageId))
                    {
                        throw new ProxyException(
                            new Error
                                {
                                    Title = typeof(ProxyException).FullName,
                                    Source = string.Format("An error occurred at method '{0}' of '{1}'!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                    StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                    StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                    Message = "Location header in response does not contain a valid ID for the newly created object.",
                                    Code = (int)EnumErrorCodes.ProxyLayerError
                                },
                            typeof(EnumErrorCodes));
                    }

                    return Convert.ToInt32(messageId);
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
        /// Gets the service info ping result
        /// </summary>
        /// <param name="accessToken">
        /// The access Token.
        /// </param>
        /// <returns>
        /// The specified <see cref="ServiceInfo"/> ServiceInfo
        /// </returns>
        public virtual async Task<ServiceInfo> Ping(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ProxyException(
                    new Error()
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = "Please provide a valid access token",
                        Code = (int)EnumErrorCodes.ProxyLayerError
                    },
                    typeof(EnumErrorCodes));
            }

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
                            new Error()
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



       

        // Old RestSharp way...
        /*/// <summary> Send mail. </summary>
        /// <param name="message"> The <see cref="Message"/>. </param>
        /// <returns> The generated message id<see cref="int"/>. </returns>
        public virtual int Send(Message message)
        //{
        //    var request = new RestRequest(Method.POST)
        //                      {
        //                            RequestFormat = DataFormat.Json,
        //                            JsonSerializer = _serializer
        //                      };
        //    request.AddBody(message);

        //    var response = _client.Execute(request);

        //    if (response.StatusCode != HttpStatusCode.Created)
        //    {
        //        throw NewExceptionFromResponse(response);
        //    }

        //    var location = response.Headers.FirstOrDefault(c => c.Name == "Location");

        //    if (location == null)
        //    {
        //        throw new ProxyException(
        //                new Error
        //                {
        //                    Title = typeof(ProxyException).FullName,
        //                    Source = string.Format("An error occurred at method '{0}' of '{1}'!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
        //                    StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
        //                    StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
        //                    Message = "Location header not found in response.",
        //                    Code = (int)EnumErrorCodes.ProxyMailManApiSendNoLocationHeader
        //                },
        //                typeof(EnumErrorCodes));
        //    }

        //    var messageId = location.Value.ToString().Split('/').Last();

        //    if (string.IsNullOrWhiteSpace(messageId))
        //    {
        //        throw new ProxyException(
        //               new Error
        //               {
        //                   Title = typeof(ProxyException).FullName,
        //                   Source = string.Format("An error occurred at method '{0}' of '{1}'!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
        //                   StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
        //                   StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
        //                   Message = "Location header in response does not contain a valid ID for the newly created object.",
        //                   Code = (int)EnumErrorCodes.ProxyMailManApiSendEmptyLocationHeader
        //               },
        //               typeof(EnumErrorCodes));
        //    }

        //    return Convert.ToInt32(messageId);
        //}*/

    }
}
