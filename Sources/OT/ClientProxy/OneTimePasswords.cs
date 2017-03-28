using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Security.OTP.ClientProxy.Interfaces;
using BND.Services.Security.OTP.Models;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace BND.Services.Security.OTP.ClientProxy
{
    /// <summary>
    /// Class OneTimePasswords is a resource of <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> api.
    /// It provides all actions of one time password resource and implements
    /// <seealso cref="BND.Services.Security.OTP.ClientProxy.Interfaces.IOneTimePasswords"/> interface.
    /// </summary>
    public class OneTimePasswords : ResourceBase, IOneTimePasswords
    {
        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="BND.Services.Security.OTP.ClientProxy.OneTimePasswords"/> class.
        /// </summary>
        /// <param name="baseAddress">The base otp api url.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="accountId">The account identifier.</param>
        public OneTimePasswords(string baseAddress, string apiKey, string accountId)
            : base(baseAddress, apiKey, accountId)
        { }

        #endregion


        #region [Methods]

        /// <summary>
        /// Creates new <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>The otp result, it will has value when otp code has been created and sent to receiver following channel address.</returns>
        public OtpResultModel NewCode(OtpRequestModel request)
        {
            // Checks required parameters.
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            if (String.IsNullOrEmpty(request.Suid))
            {
                throw new ArgumentException("request.Suid");
            }
            if (request.Channel == null)
            {
                throw new ArgumentException("request.Channel");
            }
            if (request.Channel.Type == null)
            {
                throw new ArgumentException("request.Channel.Type");
            }
            if (request.Channel.Sender == null)
            {
                throw new ArgumentException("request.Channel.Sender");
            }
            if (request.Channel.Address == null)
            {
                throw new ArgumentException("request.Channel.Address");
            }
            if (request.Channel.Message == null)
            {
                throw new ArgumentException("request.Channel.Message");
            }

            try
            {
                // Prepares all parameters before call to api via http client.
                OtpModel otpModel = new OtpModel()
                {
                    Suid = request.Suid,
                    Channel = request.Channel,
                    Payload = request.Payload
                };

                using (HttpClient httpClient = GetHttpClient())
                {
                    // Calls api with post method to creates new otp code following API document.
                    HttpResponseMessage result =
                        httpClient.PostAsync(
                        Properties.Resources.URL_RES_OTP_NEWCODE, 
                        MapModelToFormUrlEncodedContent(otpModel))
                        .Result;

                    string resultContent = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<OtpResultModel>(resultContent);
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

        /// <summary>
        /// Verifies the <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code.
        /// </summary>
        /// <param name="otpId">The otp identifier which you got from creating new otp code.</param>
        /// <param name="otpCode">The entered otp code.</param>
        /// <returns>The otp result, it will has value when entered otp code is valid.</returns>
        public OtpModel VerifyCode(string otpId, string otpCode)
        {
            // Checks required parameters.
            if (string.IsNullOrEmpty(otpId))
            {
                throw new ArgumentNullException("otpId");
            }
            if (string.IsNullOrEmpty(otpCode))
            {
                throw new ArgumentNullException("otpCode");
            }

            try
            {
                using (HttpClient httpClient = GetHttpClient())
                {
                    // Calls api with put method to verifies otp code following API document.
                    HttpResponseMessage result =
                        httpClient.PutAsync(String.Format("{0}/{1}/Verify", Properties.Resources.URL_RES_OTP_NEWCODE, otpId),
                        MapModelToRawContent(new { otpCode }))
                        .Result;


                    string resultContent = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<OtpModel>(resultContent);
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
