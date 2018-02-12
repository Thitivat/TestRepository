using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;

using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Infrastructure.Proxies.NET4;
using BND.Services.Infrastructure.Proxies.NET4.Extensions;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Entities.Extensions;
using BND.Services.Matrix.Entities.QueryStringModels;
using BND.Services.Matrix.Proxy.NET4.Interfaces;
using BND.Services.Matrix.Proxy.NET4.Serializers;

using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

namespace BND.Services.Matrix.Proxy.NET4.Implementations
{
    /// <summary>
    /// The buckets api.
    /// </summary>
    public class BucketsApi : IBucketsApi
    {
        private const string Version = "v1";

        private readonly RestClient _client;

        private readonly JsonDeserializer _deserializer = new JsonDeserializer();

        private readonly RestSharpJsonNetSerializer _serializer = new RestSharpJsonNetSerializer();

        private readonly ProxyExceptionHandler _exceptionHandler = new ProxyExceptionHandler();

        /// <summary>
        /// Initializes a new instance of the <see cref="BucketsApi"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public BucketsApi(IApiConfig config)
        {
            _client = new RestClient(string.Format("{0}/{1}/", config.ServiceUrl, Version));

            _client.AddHandler("application/json", _deserializer);
        }

        /// <summary>
        /// The get bucket.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The <see cref="BucketItem"/>.
        /// </returns>
        public virtual BucketItem GetBucket(string id, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest("buckets/{id}", Method.GET);
            request.AddUrlSegment("id", id);

            var response = _client.Execute<BucketItem>(request);

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
                        Message = "bucket item could not be read from the response content!",
                        Code = (int)EnumErrorCodes.ProxyLayerError
                    },
                    typeof(EnumErrorCodes));
            }
            return response.Data;
        }

        /// <summary>
        /// The get buckets.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="field">
        /// The field
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The <see cref="List{BucketItem}"/>.
        /// </returns>
        public virtual List<BucketItem> GetBuckets(BucketItemFilters filter, BucketExtraFields field, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest("buckets", Method.GET);

            if (filter != null)
            {
                var filterCollection = filter.ToNameValueCollection();

                foreach (var key in filterCollection.AllKeys.Where(a => !string.IsNullOrWhiteSpace(filterCollection.Get(a))))
                {
                    request.AddQueryParameter(key, filterCollection.Get(key));
                }
            }

            if (field != null)
            {
                var fields = field.Fields.ToValues().ToList();

                request.AddQueryParameter("fields", string.Join(",", fields));
            }

            var response = _client.Execute<List<BucketItem>>(request);

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
                        Message = "bucket items could not be read from the response content!",
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
