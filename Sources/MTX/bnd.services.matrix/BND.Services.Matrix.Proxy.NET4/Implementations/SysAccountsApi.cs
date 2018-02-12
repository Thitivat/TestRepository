using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;

using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Infrastructure.Proxies.NET4;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Proxy.NET4.Interfaces;
using BND.Services.Matrix.Proxy.NET4.Serializers;

using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

namespace BND.Services.Matrix.Proxy.NET4.Implementations
{
    /// <summary>
    /// The sys accounts api.
    /// </summary>
    public class SysAccountsApi : ISysAccountsApi
    {
        private const string Version = "v1";

        private readonly RestClient _client;

        private readonly JsonDeserializer _deserializer = new JsonDeserializer();

        private readonly RestSharpJsonNetSerializer _serializer = new RestSharpJsonNetSerializer();

        private readonly ProxyExceptionHandler _exceptionHandler = new ProxyExceptionHandler();

        /// <summary>
        /// Initializes a new instance of the <see cref="SysAccountsApi"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public SysAccountsApi(IApiConfig config)
        {
            _client = new RestClient(string.Format("{0}/{1}/", config.ServiceUrl, Version));

            _client.AddHandler("application/json", _deserializer);
        }

        /// <summary>
        /// Gets system accounts overviews.
        /// </summary>
        /// <param name="sysId"> The sys id. </param>
        /// <param name="valueDate"> The value date. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// A <see cref="List{T}"/> where T is <see cref="AccountOverview"/>.
        /// </returns>
        /// <exception cref="ProxyException">
        /// The proxy layer exception
        /// </exception>
        public virtual AccountOverview GetAccountOverview(string sysId, DateTime valueDate, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request =
                new RestRequest(
                    string.Format("sysAccounts/{0}/overview?valueDate={1}", sysId, valueDate.ToString("yyyy-MM-dd")),
                    Method.GET);

            var response = _client.Execute<AccountOverview>(request);

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
                        Message = "movement data could not be read from the response content!",
                        Code = (int)EnumErrorCodes.ProxyLayerError
                    },
                    typeof(EnumErrorCodes));
            }
            return response.Data;
        }

        /// <summary>
        /// Gets system accounts.
        /// </summary>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// A <see cref="List{T}"/> where T is <see cref="SystemAccount"/>..
        /// </returns>
        public virtual List<SystemAccount> GetSystemAccounts(string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest("sysAccounts", Method.GET);
            var response = _client.Execute<List<SystemAccount>>(request);

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
                        Message = "system accounts could not be read from the response content!",
                        Code = (int)EnumErrorCodes.ProxyLayerError
                    },
                    typeof(EnumErrorCodes));
            }
            return response.Data;
        }

        /// <summary>
        /// Gets movements.
        /// </summary>
        /// <param name="sysId">The system account id</param>
        /// <param name="startDate"> The start date. </param>
        /// <param name="endDate"> The end date. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// A <see cref="List{T}"/> where T is of type <see cref="MovementItem"/>
        /// </returns>
        public virtual List<MovementItem> GetMovements(string sysId, DateTime? startDate, DateTime? endDate, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest("sysAccounts/{sysId}/movements", Method.GET);
            request.AddUrlSegment("sysId", sysId);

            if (startDate != null)
            {
                request.AddQueryParameter("startDate", startDate.Value.ToString("yyyy-MM-dd"));
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
            return response.Data;
        }

        /// <summary>
        /// The sweep.
        /// </summary>
        /// <param name="sweep"> The sweep parameter. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// A decimal
        /// </returns>
        public decimal Sweep(Sweep sweep, string accessToken)
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

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest("sysAccounts/sweep", Method.POST) { RequestFormat = DataFormat.Json, JsonSerializer = _serializer };

            request.AddBody(sweep);

            var response = _client.Execute<decimal>(request);

            if (response.StatusCode != HttpStatusCode.Created)
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
        /// Gets system accounts balance.
        /// </summary>
        /// <param name="sysId"> The sys id. </param>
        /// <param name="valueDate"> The value date. </param>
        /// <param name="accessToken">The access token</param>
        /// <returns>
        /// The balance.
        /// </returns>
        public decimal GetBalanceOverview(string sysId, DateTime valueDate, string accessToken)
        {
            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest(
                string.Format("sysAccounts/{0}/balance?valueDate={1}", sysId, valueDate.ToString("yyyy-MM-dd")),
                Method.GET);

            var response = _client.Execute<decimal>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
            }

            if (response.Data == default(decimal))
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

            return response.Data;
        }

        /// <summary>
        /// Returns a payment.
        /// </summary>
        /// <param name="returnPayment">
        /// The return payment.
        /// </param>
        /// <param name="accessToken"> The access token. </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ReturnPayment(ReturnPayment returnPayment, string accessToken)
        {
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

            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest("sysAccounts/returnPayment", Method.POST) { RequestFormat = DataFormat.Json, JsonSerializer = _serializer };

            request.AddBody(returnPayment);

            var response = _client.Execute<bool>(request);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
            }

            return response.Data;
        }

        /// <summary>
        /// Create Outgoing Return bucket
        /// </summary>
        /// <param name="returnBucketItem">
        /// The return bucket item
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns></returns>
        public virtual string CreateOutgoingReturnBucket(ReturnBucketItem returnBucketItem, string accessToken)
        {
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

            ThrowIfTokenEmpty(accessToken);

            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");

            var request = new RestRequest("sysAccounts/CreateOutgoingReturnBucket", Method.POST) { RequestFormat = DataFormat.Json, JsonSerializer = _serializer };

            request.AddBody(returnBucketItem);

            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw _exceptionHandler.CreateNewExceptionFromResponse(response);
            }

            return response.Content;
        }

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
