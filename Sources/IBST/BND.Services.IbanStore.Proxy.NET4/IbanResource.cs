using BND.Services.IbanStore.Proxy.NET4.Interfaces;
using BND.Services.IbanStore.Proxy.NET4.Models;
using BND.Services.Infrastructure.ErrorHandling;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace BND.Services.IbanStore.Proxy.NET4
{
    /// <summary>
    /// Class IbanResource.
    /// </summary>
    public class IbanResource : IIbanResource
    {
        #region [Fields]
        /// <summary>
        /// The url base address.
        /// </summary>
        private string _baseAddress;
        /// <summary>
        /// The api token.
        /// </summary>
        private string _token;
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="IbanResource"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        /// <param name="token">The token.</param>
        public IbanResource(string baseAddress, string token)
        {
            _baseAddress = baseAddress;
            _token = token;
        }
        #endregion

        #region [Protected Method]
        /// <summary>
        /// Creates the HTTP client.
        /// </summary>
        /// <param name="uidPrefix">The uid prefix.</param>
        /// <returns>HttpClient.</returns>
        /// <exception cref="System.ArgumentNullException">uidPrefix</exception>
        protected virtual HttpClient GetHttpClient(string uidPrefix)
        {
            if (string.IsNullOrEmpty(uidPrefix))
                throw new ArgumentNullException("uidPrefix");

            var client = new HttpClient();

            // Sets base url
            client.BaseAddress = new Uri(new Uri(_baseAddress.EndsWith("/")
                ? _baseAddress
                : _baseAddress + "/"),
                Properties.Resources.URL_RES_BASE);
            // Sets headers
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_token);
            client.DefaultRequestHeaders.TryAddWithoutValidation(Properties.Resources.HEADER_UID_PREFIX, uidPrefix);

            return client;
        }
        #endregion

        #region [Public Methods]
        public int ReserveNextAvailable(string uidPrefix, string uid)
        {
            if (String.IsNullOrEmpty(uidPrefix))
                throw new ArgumentNullException("uidPrefix");

            if (String.IsNullOrEmpty(uid))
                throw new ArgumentNullException("uid");

            try
            {
                using (var httpClient = GetHttpClient(uidPrefix))
                {
                    string requestUri = String.Format("{0}/{1}/{2}", Properties.Resources.URL_RES_IBAN
                        , "nextavailable"
                        , uid);

                    HttpResponseMessage responseMessage = httpClient.PutAsync(requestUri, null).Result;

                    if (responseMessage.IsSuccessStatusCode || responseMessage.StatusCode == HttpStatusCode.NotModified)
                    {
                        if (responseMessage.Headers.Location == null || string.IsNullOrEmpty(responseMessage.Headers.Location.ToString()))
                        {
                            throw new ProxyException(
                            new Error
                            {
                                Title = typeof(ProxyException).FullName,
                                Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                                StatusCode = ((int)responseMessage.StatusCode).ToString(CultureInfo.InvariantCulture),
                                StatusCodeDescription = responseMessage.StatusCode.ToString(),
                                Message = "Location header not found in response.",
                                Code = (int)EnumErrorCodes.DataLayerError
                            },
                            typeof(EnumErrorCodes));
                        }

                        string content = responseMessage.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<NextAvailableResponse>(content).IbanId;
                    }
                    else
                    {
                        throw new ProxyException(
                        new Error
                        {
                            Title = typeof(ProxyException).FullName,
                            Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                            StatusCode = ((int)responseMessage.StatusCode).ToString(CultureInfo.InvariantCulture),
                            StatusCodeDescription = responseMessage.StatusCode.ToString(),
                            Message = responseMessage.ReasonPhrase,
                            Code = (int)EnumErrorCodes.DataLayerError
                        },
                        typeof(EnumErrorCodes));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProxyException(
                    new Error
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = ex.Message,
                        Code = (int)EnumErrorCodes.ProxyLayerError
                    },
                    typeof(EnumErrorCodes));
            }
        }

        /// <summary>
        /// Assigns status of Iban
        /// </summary>
        /// <param name="uidPrefix">The uid prefix.</param>
        /// <param name="uid">The uid.</param>
        /// <param name="ibanId">The iban identifier.</param>
        /// <returns>Assigned IBAN Url</returns>
        /// <exception cref="System.Exception"></exception>
        /// <exception cref="BND.Services.Infrastructure.ErrorHandling.ProxyException"></exception>
        /// <exception cref="Error"></exception>
        public string Assign(string uidPrefix, string uid, int ibanId)
        {
            try
            {
                using (var httpClient = this.GetHttpClient(uidPrefix))
                {
                    // create url
                    string requestUri = String.Format(Properties.Resources.URL_RES_ASSIGN, ibanId, uid);

                    // call HttpPut for assign the status of Iban
                    HttpResponseMessage responseMessage = httpClient.PutAsync(requestUri, null).Result;

                    if (responseMessage.IsSuccessStatusCode || responseMessage.StatusCode == HttpStatusCode.NotModified)
                    {
                        return responseMessage.Headers.Location.ToString();
                    }
                    else
                    {
                        throw new Exception(responseMessage.ReasonPhrase);
                    }
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProxyException(
                    new Error
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = ex.Message,
                        Code = (int)EnumErrorCodes.ProxyLayerError
                    },
                    typeof(EnumErrorCodes));
            }
        }

        /// <summary>
        /// Gets the specified iban by iban parameter
        /// </summary>
        /// <param name="uidPrefix">The uid prefix.</param>
        /// <param name="uid">The uid.</param>
        /// <returns>Next available IBAN</returns>
        /// <exception cref="System.Exception"></exception>
        /// <exception cref="BND.Services.Infrastructure.ErrorHandling.ProxyException"></exception>
        /// <exception cref="Error"></exception>
        public NextAvailable Get(string uidPrefix, string uid)
        {
            try
            {
                using (HttpClient httpClient = GetHttpClient(uidPrefix))
                {
                    string requestUri = String.Format(Properties.Resources.URL_RES_GET, uid);
                    HttpResponseMessage responseMessage = httpClient.GetAsync(requestUri).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string content = responseMessage.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<NextAvailable>(content);
                    }
                    else
                    {
                        throw new Exception(responseMessage.ReasonPhrase);
                    }
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProxyException(
                    new Error
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = ex.Message,
                        Code = (int)EnumErrorCodes.ProxyLayerError
                    },
                    typeof(EnumErrorCodes));
            }
        }

        /// <summary>
        /// Reservs the and asssign.
        /// </summary>
        /// <param name="uidPrefix">The uid prefix.</param>
        /// <param name="uid">The uid.</param>
        /// <returns>Next available IBAN.</returns>
        public NextAvailable ReserveAndAssign(string uidPrefix, string uid)
        {
            int ibanId = ReserveNextAvailable(uidPrefix, uid);
            Assign(uidPrefix, uid, ibanId);
            return Get(uidPrefix, uid);
        }
        #endregion
    }
}
