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
using BND.Services.Infrastructure.Proxies.Extensions;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Entities.Extensions;
using BND.Services.Matrix.Entities.QueryStringModels;
using BND.Services.Matrix.Proxy.Interfaces;

namespace BND.Services.Matrix.Proxy.Implementations
{
    /// <summary>
    /// The buckets api.
    /// </summary>
    public class BucketsApi : IBucketsApi
    {
        /// <summary>
        /// The system accounts url.
        /// </summary>
        private const string BucketsUrl = "v1/buckets";

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
        /// Initializes a new instance of the <see cref="BucketsApi"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public BucketsApi(IApiConfig config)
        {
            _config = config;
            _formatters = new List<MediaTypeFormatter>() { new Infrastructure.Proxies.Json.BaseJsonMediaTypeFormatter() }; 
        }

        /// <summary>
        /// The get buckets.
        /// </summary>
        /// <param name="filter"> The filter.</param>
        /// <param name="field">The fields</param>
        /// <param name="accessToken"> The access token. </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<List<BucketItem>> GetBuckets(BucketItemFilters filter, BucketExtraFields field, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, BucketsUrl));

                    var filtersAndFields = new StringBuilder();

                    var filterCollection = filter == null ? null : filter.ToNameValueCollection();

                    if (filter != null)
                    {
                        filtersAndFields.Append("?");

                        foreach (var key in filterCollection.AllKeys.Where(a => !string.IsNullOrWhiteSpace(filterCollection.Get(a))))
                        {
                            filtersAndFields.AppendFormat("{0}={1}", key, filterCollection.Get(key));

                            if (key != filterCollection.AllKeys.Last())
                            {
                                filtersAndFields.Append("&");
                            }
                        }
                    }

                    if (field != null)
                    {
                        filtersAndFields.Append("&fields=");

                        var fields = field.Fields.ToValues().ToList();

                        foreach (var f in fields)
                        {
                            filtersAndFields.AppendFormat(f == fields.Last() ? "{0}" : "{0},", f);
                        }
                    }

                    var response = await client.GetAsync(filtersAndFields.ToString()).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var buckets = response.Content.ReadAsAsync<List<BucketItem>>(_formatters).Result;

                    if (buckets == null)
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

                    return buckets;
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
        /// The get bucket.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<BucketItem> GetBucket(string id, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, BucketsUrl));

                    var response = await client.GetAsync(id).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var bucket = response.Content.ReadAsAsync<BucketItem>(_formatters).Result;

                    if (bucket == null)
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

                    return bucket;
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
