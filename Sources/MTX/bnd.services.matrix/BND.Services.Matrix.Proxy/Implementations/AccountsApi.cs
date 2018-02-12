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
using BND.Services.Matrix.Entities.QueryStringModels;
using BND.Services.Matrix.Proxy.Interfaces;

namespace BND.Services.Matrix.Proxy.Implementations
{
    /// <summary>
    /// The accounts api.
    /// </summary>
    public class AccountsApi : IAccountsApi
    {
        /// <summary>
        /// The version.
        /// </summary>
        private const string Version = "v1";

        /// <summary>
        /// The accounts_ url.
        /// </summary>
        private const string AccountsUrl = "v1/accounts";

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
        /// Initializes a new instance of the <see cref="AccountsApi"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public AccountsApi(IApiConfig config)
        {
            _config = config;
            _formatters = new List<MediaTypeFormatter>() { new Infrastructure.Proxies.Json.BaseJsonMediaTypeFormatter() };
        }

        /// <summary>
        /// Creates a savings account
        /// </summary>
        /// <param name="savingsFree"> The savings free. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The created account iban
        /// </returns>
        public virtual async Task<string> CreateSavingsAccount(SavingsFree savingsFree, string accessToken)
        {
            if (savingsFree == null)
            {
                throw new ProxyException(
                    new Error
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = "The provided SavingsFree entity is null!",
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
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, AccountsUrl));

                    var response = await client.PostAsJsonAsync("savings", savingsFree).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.Created)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    // Keep the code below in case we need to use it
                    /*string newAccountId;

                    IEnumerable<string> locationHeaders;
                    if (response.Headers.TryGetValues("Location", out locationHeaders))
                    {
                        newAccountId = locationHeaders.FirstOrDefault().Split('/').Last();
                    }
                    else
                    {
                        throw new ProxyException(
                            new Error
                            {
                                Title = typeof(ProxyException).FullName,
                                Source =
                                    string.Format(
                                        "An error occurred at method '{0}' of '{1}'!",
                                        MethodBase.GetCurrentMethod().Name,
                                        GetType().FullName),
                                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                Message = "Location header not found in response",
                                Code = (int)EnumErrorCodes.ProxyLayerError
                            },
                            typeof(EnumErrorCodes));
                    }

                    if (string.IsNullOrWhiteSpace(newAddressId))
                    {
                        throw new ProxyException(
                            new Error
                            {
                                Title = typeof(ProxyException).FullName,
                                Source =
                                    string.Format(
                                        "An error occurred at method '{0}' of '{1}'!",
                                        MethodBase.GetCurrentMethod().Name,
                                        GetType().FullName),
                                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                Message = "Location header in response does not contain a valid ID for the newly created object.",
                                Code = (int)EnumErrorCodes.ProxyLayerError
                            },
                            typeof(EnumErrorCodes));
                    }*/

                    // Returns the iban for now, maybe it should return the newly created id in the future
                    return savingsFree.Iban;
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
        /// Gets an interest rate.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="options"> The options. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// A <see cref="Task"/> of <see cref="List{T}"/> where T is of type <see cref="InterestRate"/>
        /// </returns>
        public virtual async Task<List<InterestRate>> GetInterestRate(string iban, InterestRateOptions options, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, AccountsUrl));

                    var filtersAndFields = new StringBuilder();

                    if (options != null)
                    {
                        if (options.FromDate != null)
                        {
                            filtersAndFields.AppendFormat("?fromDate={0}", options.FromDate.Value.ToString("yyyy-MM-dd"));
                        }

                        if (options.EndOverrideDate != null)
                        {
                            filtersAndFields.AppendFormat("&endOverrideDate={0}", options.EndOverrideDate.Value.ToString("yyyy-MM-dd"));
                        }
                    }

                    var response = await client.GetAsync(string.Format("{0}/interestrate{1}", iban, filtersAndFields)).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var interestRateList = response.Content.ReadAsAsync<List<InterestRate>>(_formatters).Result;

                    if (interestRateList == null)
                    {
                        throw new ProxyException(
                            new Error()
                            {
                                Title = typeof(ProxyException).FullName,
                                Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                Message = "InterestRate Items data could not be read from the response content!",
                                Code = (int)EnumErrorCodes.ProxyLayerError
                            },
                            typeof(EnumErrorCodes));
                    }

                    return interestRateList;
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
        /// Unblock savings accounts.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// A <see cref="Task"/>.
        /// </returns>
        public virtual async Task UnblockSavingAccounts(string iban, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, AccountsUrl));

                    var response = await client.PutAsync(string.Format("{0}/unblock", iban), null).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.NoContent)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }
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
        /// Gets the accrued interest.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="valueDate"> The value date. </param>
        /// <param name="calculateTaxAction"> The calculate tax action. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The accrued interest
        /// </returns>
        public virtual async Task<decimal> GetAccruedInterest(string iban, DateTime valueDate, bool calculateTaxAction, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, AccountsUrl));

                    var response = await client.GetAsync(string.Format("{0}/accruedinterest?valueDate={1}&calculateTaxAction={2}", iban, valueDate.ToString("yyyy-MM-dd"), calculateTaxAction)).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var accruedInterest = response.Content.ReadAsAsync<decimal>(_formatters).Result;

                    if (accruedInterest == default(decimal))
                    {
                        throw new ProxyException(
                            new Error
                            {
                                Title = typeof(ProxyException).FullName,
                                Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                Message = "accruedInterest data could not be read from the response content!",
                                Code = (int)EnumErrorCodes.ProxyLayerError
                            },
                            typeof(EnumErrorCodes));
                    }

                    return accruedInterest;
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
        /// Gets the balance overview.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="valueDate"> The value date. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The balance overview
        /// </returns>
        public virtual async Task<decimal> GetBalanceOverview(string iban, DateTime valueDate, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, AccountsUrl));

                    var response = await client.GetAsync(string.Format("{0}/balance?valueDate={1}", iban, valueDate.ToString("yyyy-MM-dd"))).ConfigureAwait(false);

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
        /// Gets the movements overview.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="startDate"> The start date. </param>
        /// <param name="endDate"> The end date. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// A <see cref="Task"/> of <see cref="List{T}"/> where T is of type <see cref="MovementItem"/>
        /// </returns>
        public virtual async Task<List<MovementItem>> GetMovements(string iban, DateTime? startDate, DateTime? endDate, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, AccountsUrl));

                    var filters = string.Empty;

                    if (startDate.HasValue)
                    {
                        filters = string.Format("?startDate={0}", startDate.Value.ToString("yyyy-MM-dd"));
                    }

                    if (endDate.HasValue)
                    {
                        filters = filters + string.Format("&endDate={0}", endDate.Value.ToString("yyyy-MM-dd"));
                    }

                    var response = await client.GetAsync(string.Format("{0}/movements{1}", iban, filters)).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var movementList = response.Content.ReadAsAsync<List<MovementItem>>(_formatters).Result;

                    if (movementList == null)
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

                    return movementList;
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
        /// Gets a saving account overview.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="valueDate"> The value date. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The account overview
        /// </returns>
        public virtual async Task<AccountOverview> GetAccountOverview(string iban, DateTime valueDate, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, AccountsUrl));

                    var response = await client.GetAsync(string.Format("{0}/overview?valueDate={1}", iban, valueDate.ToString("yyyy-MM-dd"))).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    var overview = response.Content.ReadAsAsync<AccountOverview>(_formatters).Result;

                    if (overview == default(AccountOverview))
                    {
                        throw new ProxyException(
                            new Error
                            {
                                Title = typeof(ProxyException).FullName,
                                Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                Message = "account overview data could not be read from the response content!",
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
        /// Block saving accounts.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task BlockSavingAccounts(string iban, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, AccountsUrl));

                    var response = await client.PutAsync(string.Format("{0}/block", iban), null).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.NoContent)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }
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
        /// Create outgoing payment.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="payment"> The payment. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The created outgoing payment id
        /// </returns>
        public virtual async Task<string> CreateOutgoingPayment(string iban, Payment payment, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            if (payment == null)
            {
                throw new ProxyException(
                    new Error
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = "The provided Payment entity is null!",
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
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, AccountsUrl));

                    var response = await client.PostAsJsonAsync(string.Format("{0}/PaymentsOut", iban), payment).ConfigureAwait(false);

                    if (response.StatusCode != HttpStatusCode.Created)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

                    string paymentBucketId;

                    IEnumerable<string> locationHeaders;
                    if (response.Headers.TryGetValues("Location", out locationHeaders))
                    {
                        paymentBucketId = locationHeaders.FirstOrDefault().Split('/').Last();
                    }
                    else
                    {
                        throw new ProxyException(
                            new Error
                            {
                                Title = typeof(ProxyException).FullName,
                                Source = string.Format("An error occurred at method '{0}' of '{1}'!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                                Message = "Location header not found in response",
                                Code = (int)EnumErrorCodes.ProxyLayerError
                            },
                            typeof(EnumErrorCodes));
                    }

                    if (string.IsNullOrWhiteSpace(paymentBucketId))
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

                    return paymentBucketId;
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
        /// The close account.
        /// </summary>
        /// <param name="iban">
        /// The iban.
        /// </param>
        /// <param name="closingPaymentItem">
        /// The closing payment item.
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<AccountCloseResultsItem> CloseAccount(string iban, ClosingPaymentItem closingPaymentItem, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.SetBearerToken(accessToken);
                    client.BaseAddress = new Uri(string.Format("{0}/{1}/", _config.ServiceUrl, AccountsUrl));

                    var response = await client.PutAsJsonAsync(string.Format("{0}/close", iban), closingPaymentItem).ConfigureAwait(false);
                    var result = response.Content.ReadAsAsync<AccountCloseResultsItem>(_formatters).Result;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw _exceptionHandler.CreateNewExceptionFromResponse(response);
                    }

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
        /// The throw if iban empty.
        /// </summary>
        /// <param name="iban">
        /// The iban.
        /// </param>
        private void ThrowIfIbanEmpty(string iban)
        {
            // Get calling method name
            var callingMethodName = new StackTrace().GetFrame(1).GetMethod().Name;

            if (string.IsNullOrWhiteSpace(iban))
            {
                throw new ProxyException(
                    new Error
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", callingMethodName, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = "Please provide a valid iban",
                        Code = (int)EnumErrorCodes.ProxyLayerError
                    },
                    typeof(EnumErrorCodes));
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
