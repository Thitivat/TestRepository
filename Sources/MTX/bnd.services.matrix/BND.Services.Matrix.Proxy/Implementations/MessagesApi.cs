using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Infrastructure.Proxies;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Entities.Extensions;
using BND.Services.Matrix.Entities.QueryStringModels;
using BND.Services.Matrix.Enums;
using BND.Services.Matrix.Proxy.Interfaces;

namespace BND.Services.Matrix.Proxy.Implementations
{
    /// <summary>
    /// The messages api.
    /// </summary>
    public class MessagesApi : IMessagesApi
    {
        /// <summary>
        /// The system accounts url.
        /// </summary>
        private const string MessagesUrl = "v1/messages";

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
        /// Initializes a new instance of the <see cref="MessagesApi"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public MessagesApi(IApiConfig config)
        {
            _config = config;
            _formatters = new List<MediaTypeFormatter>() { new Infrastructure.Proxies.Json.BaseJsonMediaTypeFormatter() }; 
        }

        /// <summary>
        /// The get messages.
        /// </summary>
        /// <param name="filters">
        /// The filters.
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> of <see cref="List{T}"/> where T is of type <see cref="Message"/>.
        /// </returns>
        public virtual async Task<List<Message>> GetMessages(MessageFilters filters, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, MessagesUrl));

                    var filter = new StringBuilder();

                    var filterCollection = filters == null ? null : filters.ToNameValueCollection();

                    if (filters != null)
                    {
                        filter.Append("?");

                        foreach (var key in filterCollection.AllKeys.Where(a => !string.IsNullOrWhiteSpace(filterCollection.Get(a))))
                        {
                            filter.AppendFormat("{0}={1}", key, filterCollection.Get(key));

                            if (key != filterCollection.AllKeys.Last())
                            {
                                filter.Append("&");
                            }
                        }
                    }

                    var response = await client.GetAsync(filter.ToString()).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var messages = response.Content.ReadAsAsync<List<Message>>(_formatters).Result;

                    if (messages == null)
                    {
                        throw new ProxyException(
                            new Error
                            {
                                Title = typeof(ProxyException).FullName,
                                Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                Message = "message items could not be read from the response content!",
                                Code = (int)EnumErrorCodes.ProxyLayerError
                            },
                            typeof(EnumErrorCodes));
                    }

                    return messages;
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
        /// Gets the message details.
        /// </summary>
        /// <param name="id"> The message id. </param>
        /// <param name="msgType"> The <see cref="EnumMessageTypes"/> </param>
        /// <param name="accessToken"> The access token. </param>
        /// <returns>
        /// The <see cref="MessageDetail"/>.
        /// </returns>
        public virtual async Task<MessageDetail> GetMessageDetails(int id, EnumMessageTypes msgType, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, MessagesUrl));

                    var response = await client.GetAsync(string.Concat(id.ToString(), "?messageType=", msgType.ToString())).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var message = response.Content.ReadAsAsync<MessageDetail>(_formatters).Result;

                    if (message == null)
                    {
                        throw new ProxyException(
                            new Error
                            {
                                Title = typeof(ProxyException).FullName,
                                Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                Message = "message item could not be read from the response content!",
                                Code = (int)EnumErrorCodes.ProxyLayerError
                            },
                            typeof(EnumErrorCodes));
                    }

                    return message;
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
