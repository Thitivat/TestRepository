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
    /// The sys accounts api.
    /// </summary>
    public class SysAccountsApi : ISysAccountsApi
    {
        /// <summary>
        /// The system accounts url.
        /// </summary>
        private const string SystemAccountsUrl = "v1/sysAccounts";

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
        /// Initializes a new instance of the <see cref="SysAccountsApi"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public SysAccountsApi(IApiConfig config)
        {
            _config = config;
            _formatters = new List<MediaTypeFormatter>() { new Infrastructure.Proxies.Json.BaseJsonMediaTypeFormatter() };
        }

        /// <summary>
        /// The sweep.
        /// </summary>
        /// <param name="sweep"> The sweep parameter. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The <see cref="Task{Decimal}"/>.
        /// </returns>
        public async Task<decimal> Sweep(Sweep sweep, string accessToken)
        {
            if (sweep == null)
            {
                throw new ProxyException(
                    new Error
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = "The provided Sweep entity is null!",
                        Code = (int)EnumErrorCodes.ProxyLayerError
                    },
                    typeof(EnumErrorCodes));
            }

            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, SystemAccountsUrl));

                    var response = await client.PostAsJsonAsync("sweep", sweep).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.Created)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var resultDecimal = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (string.IsNullOrWhiteSpace(resultDecimal))
                    {
                        throw new ProxyException(
                            new Error
                            {
                                Title = typeof(ProxyException).FullName,
                                Source = string.Format("An error occurred at method '{0}' of '{1}'!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                Message = "Response content does not contain a valid decimal for the remaining balance.",
                                Code = (int)EnumErrorCodes.ProxyLayerError
                            },
                            typeof(EnumErrorCodes));
                    }

                    return Convert.ToDecimal(resultDecimal);
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
        /// Gets system accounts balance.
        /// </summary>
        /// <param name="sysId"> The sys id. </param>
        /// <param name="valueDate"> The value date. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The balance.
        /// </returns>
        public async Task<decimal> GetBalanceOverview(string sysId, DateTime valueDate, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, SystemAccountsUrl));

                    var response = await client.GetAsync(string.Format("{0}/balance?valueDate={1}", sysId, valueDate.ToString("yyyy-MM-dd"))).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var balance = response.Content.ReadAsAsync<decimal>(_formatters).Result;

                    if (balance == default(decimal))
                    {
                        throw new ProxyException(
                            new Error
                            {
                                Title = typeof(ProxyException).FullName,
                                Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                Message = "balance data could not be read from the response content!",
                                Code = (int)EnumErrorCodes.ProxyLayerError
                            },
                            typeof(EnumErrorCodes));
                    }

                    return balance;
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
        /// Gets system accounts overviews.
        /// </summary>
        /// <param name="sysId">
        /// The sys Id.
        /// </param>
        /// <param name="valueDate">
        /// The value date.
        /// </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The <see cref="AccountOverview"/>.
        /// </returns>
        public virtual async Task<AccountOverview> GetAccountOverview(string sysId, DateTime valueDate, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, SystemAccountsUrl));

                    var response = await client.GetAsync(string.Format("{0}/overview?valueDate={1}", sysId, valueDate.ToString("yyyy-MM-dd"))).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var overview = response.Content.ReadAsAsync<AccountOverview>(_formatters).Result;

                    if (overview == null)
                    {
                        throw new ProxyException(
                            new Error
                            {
                                Title = typeof(ProxyException).FullName,
                                Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                Message = "overview data could not be read from the response content!",
                                Code = (int)EnumErrorCodes.ProxyLayerError
                            },
                            typeof(EnumErrorCodes));
                    }

                    return overview;
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
        /// Gets system accounts.
        /// </summary>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The <see cref="Task"/> of <see cref="List{T}"/> where T is <see cref="SystemAccount"/>.
        /// </returns>
        public virtual async Task<List<SystemAccount>> GetSystemAccounts(string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, SystemAccountsUrl));

                    var response = await client.GetAsync(string.Empty).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var report = response.Content.ReadAsAsync<List<SystemAccount>>(_formatters).Result;

                    if (report == null)
                    {
                        throw new ProxyException(
                            new Error
                            {
                                Title = typeof(ProxyException).FullName,
                                Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                Message = "system accounts report could not be read from the response content!",
                                Code = (int)EnumErrorCodes.ProxyLayerError
                            },
                            typeof(EnumErrorCodes));
                    }

                    return report;
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
        /// Gets movements.
        /// </summary>
        /// <param name="sysId">
        /// The sys Id.
        /// </param>
        /// <param name="startDate">
        /// The start date.
        /// </param>
        /// <param name="endDate">
        /// The end date.
        /// </param>
        /// <param name="accessToken">
        /// The access token
        /// </param>
        /// <returns>
        /// A <see cref="Task{MovementOverviewItem}"/>
        /// </returns>
        /// <exception cref="ProxyException">
        /// The proxy layer exception
        /// </exception>
        public virtual async Task<List<MovementItem>> GetMovements(string sysId, DateTime? startDate, DateTime? endDate, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, SystemAccountsUrl));

                    string filters = string.Empty;

                    if (startDate != null)
                    {
                        filters = string.Format("?startDate={0}", startDate.Value.ToString("yyyy-MM-dd"));
                    }

                    if (endDate != null)
                    {
                        if (!string.IsNullOrWhiteSpace(filters))
                        {
                            filters = filters + "&";
                        }
                        else
                        {
                            filters = filters + "?";
                        }

                        filters = filters + string.Format("endDate={0}", endDate.Value.ToString("yyyy-MM-dd"));
                    }

                    var response = await client.GetAsync(string.Format("{0}/movements{1}", sysId, filters)).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var movementItems = response.Content.ReadAsAsync<List<MovementItem>>(_formatters).Result;

                    if (movementItems == null)
                    {
                        throw new ProxyException(
                            new Error
                            {
                                Title = typeof(ProxyException).FullName,
                                Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                Message = "movement data could not be read from the response content!",
                                Code = (int)EnumErrorCodes.ProxyLayerError
                            },
                            typeof(EnumErrorCodes));
                    }

                    return movementItems;
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
        /// Returns a payment.
        /// </summary>
        /// <param name="returnPayment">
        /// The return payment.
        /// </param>
        /// <param name="accessToken"> The access token. </param>
        /// <returns>
        /// The <see cref="Task"/> of <see cref="bool"/>.
        /// </returns>
        public async Task<bool> ReturnPayment(ReturnPayment returnPayment, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            if (returnPayment == null)
            {
                throw new ProxyException(
                    new Error
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = "The provided return payment entity is null!",
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
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, SystemAccountsUrl));

                    var response = await client.PostAsJsonAsync("returnPayment", returnPayment).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.Created)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var resultbool = await response.Content.ReadAsAsync<bool>().ConfigureAwait(false);

                    return resultbool;
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
        /// Create Outgoing Return bucket
        /// </summary>
        /// <param name="returnBucketItem">
        /// The return bucket item
        /// </param>
        /// <param name="accessToken">
        /// The access token
        /// </param>
        /// <returns></returns>
        public virtual async Task<string> CreateOutgoingReturnBucket(ReturnBucketItem returnBucketItem, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            if (returnBucketItem == null)
            {
                throw new ProxyException(
                    new Error
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = "The provided entity is null!",
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
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, SystemAccountsUrl));

                    var response = await client.PostAsJsonAsync("CreateOutgoingReturnBucket", returnBucketItem).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.Created)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var result = await response.Content.ReadAsAsync<string>().ConfigureAwait(false);

                    return result;
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
