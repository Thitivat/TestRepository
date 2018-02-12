using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;

using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Infrastructure.Proxies.NET4;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Entities.QueryStringModels;
using BND.Services.Matrix.Proxy.NET4.Interfaces;
using BND.Services.Matrix.Proxy.NET4.Serializers;

using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

namespace BND.Services.Matrix.Proxy.NET4.Implementations
{
    /// <summary>
    /// The accounts api.
    /// </summary>
    public class AccountsApi : IAccountsApi
    {
        private const string Version = "v1";

        private readonly RestClient _client;

        private readonly JsonDeserializer _deserializer = new JsonDeserializer();

        private readonly RestSharpJsonNetSerializer _serializer = new RestSharpJsonNetSerializer();

        private readonly ProxyExceptionHandler _exceptionHandler = new ProxyExceptionHandler();

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsApi"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public AccountsApi(IApiConfig config)
        {
            _client = new RestClient(string.Format("{0}/{1}/", config.ServiceUrl, Version));

            _client.AddHandler("application/json", _deserializer);
        }

        /// <summary>
        /// Creates a savings account
        /// </summary>
        /// <param name="savingsFree"> The savings free. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The created account iban
        /// </returns>
        public virtual string CreateSavingsAccount(SavingsFree savingsFree, string accessToken)
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

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest("accounts/savings/", Method.POST) { RequestFormat = DataFormat.Json, JsonSerializer = _serializer };

            request.AddBody(savingsFree);

            var response = _client.Execute(request);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
            }

            var location = response.Headers.First(c => c.Name == "Location");

            if (location == null)
            {
                throw new ProxyException(
                    new Error
                        {
                            Title = typeof(ProxyException).FullName,
                            Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                            StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                            StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                            Message = "Location header not found in response.",
                            Code = (int)EnumErrorCodes.ProxyLayerError
                        },
                    typeof(EnumErrorCodes));
            }

            var newId = location.Value.ToString().Split('/').Last();

            return newId;
        }

        /// <summary>
        /// Gets an interest rate.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="options"> The options. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// A <see cref="List{T}"/> where T is of type <see cref="InterestRate"/>
        /// </returns>
        public virtual List<InterestRate> GetInterestRate(string iban, InterestRateOptions options, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest(string.Format("accounts/{0}/interestrate", iban), Method.GET);

            if (options != null)
            {
                if (options.FromDate != null)
                {
                    request.AddQueryParameter("fromDate", options.FromDate.Value.ToString("yyyy-MM-dd"));
                }
                if (options.EndOverrideDate != null)
                {
                    request.AddQueryParameter("endOverrideDate", options.EndOverrideDate.Value.ToString("yyyy-MM-dd"));
                }
            }

            var response = _client.Execute<List<InterestRate>>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
            }

            if (response.Data == null)
            {
                throw new ProxyException(
                    new Error()
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
            return response.Data;
        }

        /// <summary>
        /// Unblock savings accounts.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="accessToken">The access token</param>
        public virtual void UnblockSavingAccounts(string iban, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest(string.Format("accounts/{0}/unblock", iban), Method.PUT)
                              {
                                  RequestFormat = DataFormat.Json,
                                  JsonSerializer = _serializer
                              };

            var response = _client.Execute(request);
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
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
        public virtual decimal GetAccruedInterest(string iban, DateTime valueDate, bool calculateTaxAction, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request =
                new RestRequest(
                    string.Format(
                        "accounts/{0}/accruedinterest?valueDate={1}&calculateTaxAction={2}",
                        iban,
                        valueDate.ToString("yyyy-MM-dd"),
                        calculateTaxAction),
                    Method.GET);

            var response = _client.Execute<decimal>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
            }

            if (response.Data == default(decimal))
            {
                throw new ProxyException(
                    new Error()
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
            return response.Data;
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
        public virtual decimal GetBalanceOverview(string iban, DateTime valueDate, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest(string.Format("accounts/{0}/balance?valueDate={1}", iban, valueDate.ToString("yyyy-MM-dd")), Method.GET);

            var response = _client.Execute<decimal>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
            }

            if (response.Data == default(decimal))
            {
                throw new ProxyException(
                    new Error()
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
            return response.Data;
        }

        /// <summary>
        /// Gets the overview.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="startDate"> The start date. </param>
        /// <param name="endDate"> The end date. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// A <see cref="List{T}"/> where T is of type <see cref="MovementItem"/>
        /// </returns>
        public virtual List<MovementItem> GetMovements(string iban, DateTime? startDate, DateTime? endDate, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest(string.Format("accounts/{0}/movements", iban), Method.GET);

            if (startDate != null)
            {
                request.AddParameter("startDate", startDate.Value.ToString("yyyy-MM-dd"));
            }

            if (endDate != null)
            {
                request.AddQueryParameter("endDate", endDate.Value.ToString("yyyy-MM-dd"));
            }

            var response = _client.Execute<List<MovementItem>>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
            }

            if (response.Data == null)
            {
                throw new ProxyException(
                    new Error()
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
            return response.Data;
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
        public virtual AccountOverview GetAccountOverview(string iban, DateTime valueDate, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest(string.Format("accounts/{0}/overview?valueDate={1}", iban, valueDate.ToString("yyyy-MM-dd")), Method.GET);

            var response = _client.Execute<AccountOverview>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
            }

            if (response.Data == default(AccountOverview))
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
            return response.Data;
        }

        /// <summary>
        /// Block saving accounts.
        /// </summary>
        /// <param name="iban"> The iban. </param>
        /// <param name="accessToken">The access token</param>
        public virtual void BlockSavingAccounts(string iban, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest(string.Format("accounts/{0}/block", iban), Method.PUT)
                              {
                                  RequestFormat = DataFormat.Json,
                                  JsonSerializer = _serializer
                              };

            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
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
        public virtual string CreateOutgoingPayment(string iban, Payment payment, string accessToken)
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

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest(string.Format("accounts/{0}/PaymentsOut", iban), Method.POST)
                              {
                                  RequestFormat = DataFormat.Json,
                                  JsonSerializer = _serializer
                              };

            request.AddBody(payment);

            var response = _client.Execute<int>(request);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
            }

            string paymentBucketId;

            var locationHeaders = response.Headers.First(c => c.Name == "Location");

            if (locationHeaders != null)
            {
                paymentBucketId = locationHeaders.Value.ToString().Split('/').Last();
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

            if (string.IsNullOrWhiteSpace(paymentBucketId))
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
            }

            return paymentBucketId;
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
        /// The <see cref="AccountCloseResultsItem"/>.
        /// </returns>
        public virtual AccountCloseResultsItem CloseAccount(string iban, ClosingPaymentItem closingPaymentItem, string accessToken)
        {
            ThrowIfIbanEmpty(iban);
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest(string.Format("accounts/{0}/close", iban), Method.PUT)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = _serializer
            };

            request.AddBody(closingPaymentItem);

            var response = _client.Execute<AccountCloseResultsItem>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
            }

            return response.Data;
        }

        /// <summary>
        /// Throws if iban is empty.
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
        /// Throws if token is empty.
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

