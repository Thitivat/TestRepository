using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Security.OTP.ClientProxy.Interfaces;
using BND.Services.Security.OTP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace BND.Services.Security.OTP.ClientProxy
{
    /// <summary>
    /// Class Channels is a resource of <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> api.
    /// It provides all actions of channel resource and implements
    /// <seealso cref="BND.Services.Security.OTP.ClientProxy.Interfaces.IChannels"/> interface.
    /// </summary>
    public class Channels : ResourceBase, IChannels
    {
        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceBase" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address of api.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="accountId">The account identifier.</param>
        public Channels(string baseAddress, string apiKey, string accountId)
            : base(baseAddress, apiKey, accountId)
        { }

        #endregion


        #region [Methods]
        /// <summary>
        /// Gets all channel type names.
        /// </summary>
        /// <returns>The collection of channel type names, it will has value when has retrieved all channel type names.</returns>
        /// <exception cref="BND.Services.Infrastructure.ErrorHandling.ProxyException">
        /// </exception>
        /// <exception cref="Error">
        /// </exception>
        public IList<string> GetAllChannelTypeNames()
        {
            try
            {
                using (HttpClient httpClient = GetHttpClient())
                {
                    // Calls api with get method to gets all channel type names.
                    HttpResponseMessage result = httpClient.GetAsync(Properties.Resources.URL_RES_CHANNEL).Result;

                    string resultContent = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<IList<string>>(resultContent);
                    }
                    else
                    {
                        ApiErrorModel error = JsonConvert.DeserializeObject<ApiErrorModel>(resultContent);

                        throw new ProxyException(
                         new Error
                         {
                             Title = typeof(ProxyException).FullName,
                             Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                             StatusCode = ((int)result.StatusCode).ToString(CultureInfo.InvariantCulture),
                             StatusCodeDescription = (result.StatusCode).ToString(),
                             Message = String.Join(Environment.NewLine, error.Messages.Select(x => x.ToString())),
                             Code = (int)EnumErrorCodes.DataLayerError
                         },
                         typeof(EnumErrorCodes));
                    }
                }
            }
            catch (ProxyException)
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
        #endregion
    }
}
