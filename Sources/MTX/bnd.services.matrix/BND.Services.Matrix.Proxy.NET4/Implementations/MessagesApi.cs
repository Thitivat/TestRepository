using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;

using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Infrastructure.Proxies.NET4;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Entities.Extensions;
using BND.Services.Matrix.Entities.QueryStringModels;
using BND.Services.Matrix.Enums;
using BND.Services.Matrix.Proxy.NET4.Interfaces;
using BND.Services.Matrix.Proxy.NET4.Serializers;

using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

namespace BND.Services.Matrix.Proxy.NET4.Implementations
{
    /// <summary>
    /// The messages api.
    /// </summary>
    public class MessagesApi : IMessagesApi
    {
        private const string Version = "v1";

        private readonly RestClient _client;

        private readonly JsonDeserializer _deserializer = new JsonDeserializer();

        private readonly RestSharpJsonNetSerializer _serializer = new RestSharpJsonNetSerializer();

        private readonly ProxyExceptionHandler _exceptionHandler = new ProxyExceptionHandler();

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesApi"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public MessagesApi(IApiConfig config)
        {
            _client = new RestClient(string.Format("{0}/{1}/", config.ServiceUrl, Version));

            _client.AddHandler("application/json", _deserializer);
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
        /// A <see cref="List{T}"/> where T is of type <see cref="Message"/>
        /// </returns>
        public virtual List<Message> GetMessages(MessageFilters filters, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest("messages", Method.GET);

            if (filters != null)
            {
                var filterCollection = filters.ToNameValueCollection();

                foreach (var key in filterCollection.AllKeys.Where(a => !string.IsNullOrWhiteSpace(filterCollection.Get(a))))
                {
                    request.AddQueryParameter(key, filterCollection.Get(key));
                }
            }

            var response = _client.Execute<List<Message>>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
            }

            if (response.Data == null)
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
            return response.Data;
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
        public virtual MessageDetail GetMessageDetails(int id, EnumMessageTypes msgType, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest("messages/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
            request.AddQueryParameter("messageType", msgType.ToString());

            var response = _client.Execute<MessageDetail>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
            }

            if (response.Data == null)
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
            return response.Data;
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
