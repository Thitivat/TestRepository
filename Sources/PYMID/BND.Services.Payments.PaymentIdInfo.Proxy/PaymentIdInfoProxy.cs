using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Payments.PaymentIdInfo.Entities;
using BND.Services.Payments.PaymentIdInfo.Entities.Helpers;
using BND.Services.Payments.PaymentIdInfo.Proxy.Helpers;
using BND.Services.Payments.PaymentIdInfo.Proxy.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace BND.Services.Payments.PaymentIdInfo.Proxy
{
    /// <summary>
    /// Class PaymentIdInfoProxy.
    /// </summary>
    public class PaymentIdInfoProxy : ResourceBase, IPaymentIdInfoProxy
    {
        #region [Constructors]
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceBase" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address of api.</param>
        /// <param name="token">The token string that implement the authentication key .</param>
        public PaymentIdInfoProxy(string baseAddress, string token)
            : base(baseAddress, token)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceBase" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address of api.</param>
        /// <param name="token">The token string that implement the authentication key .</param>
        /// <param name="httpClient">The HTTP client.</param>
        public PaymentIdInfoProxy(string baseAddress, string token, HttpClient httpClient)
            : base(baseAddress, token, httpClient)
        { }
        #endregion

        #region [Methods]
        /// <summary>
        /// Gets the filter types.
        /// </summary>
        /// <returns>IEnumerable&lt;EnumFilterType&gt;.</returns>
        public IEnumerable<EnumFilterType> GetFilterTypes()
        {
            var url = UrlHelper.GenerateUrl(Properties.Resources.URL_RES_GET_FILTERTYPE, String.Empty);

            HttpResponseMessage result = base._httpClient.GetAsync(url).Result;

            return base.SetResult<IEnumerable<EnumFilterType>>(result);
        }

        /// <summary>
        /// Gets PaymentIdInfoModel by BND iban.
        /// </summary>
        /// <param name="bndIban">The BND iban.</param>
        /// <param name="filterTypes">The filter types.</param>
        /// <returns>IEnumerable&lt;PaymentIdInfoModel&gt;.</returns>
        /// <exception cref="ProxyException"></exception>
        /// <exception cref="Error"></exception>
        public IEnumerable<PaymentIdInfoModel> GetByBndIban(string bndIban, IEnumerable<EnumFilterType> filterTypes)
        {
            if (String.IsNullOrEmpty(bndIban))
            {
                throw new ProxyException(
                    new Error()
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = "bndIban could not be null or empty.",
                        Code = (int)EnumErrorCodes.ProxyLayerError
                    }, typeof(EnumErrorCodes)
                );
            }

            var filterString = EnumFilterTypeHelper.ConvertToString(filterTypes);
            var url = UrlHelper.GenerateUrl(String.Format(Properties.Resources.URL_RES_PAYMENTIDINFO_BY_BNDIBAN, bndIban), filterString);

            HttpResponseMessage result = base._httpClient.GetAsync(url).Result;

            return base.SetResult<IEnumerable<PaymentIdInfoModel>>(result);
        }

        /// <summary>
        /// Gets PaymentIdInfoModel by source iban.
        /// </summary>
        /// <param name="sourceIban">The source iban.</param>
        /// <param name="filterTypes">The filter types.</param>
        /// <returns>IEnumerable&lt;PaymentIdInfoModel&gt;.</returns>
        /// <exception cref="ProxyException"></exception>
        /// <exception cref="Error"></exception>
        public IEnumerable<PaymentIdInfoModel> GetBySourceIban(string sourceIban, IEnumerable<EnumFilterType> filterTypes)
        {
            if (String.IsNullOrEmpty(sourceIban))
            {
                throw new ProxyException(
                    new Error()
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = "sourceIban could not be null or empty.",
                        Code = (int)EnumErrorCodes.ProxyLayerError
                    }, typeof(EnumErrorCodes)
                );
            }

            var filterString = EnumFilterTypeHelper.ConvertToString(filterTypes);
            var url = UrlHelper.GenerateUrl(String.Format(Properties.Resources.URL_RES_PAYMENTIDINFO_BY_SOURCEIBAN, sourceIban), filterString);

            HttpResponseMessage result = base._httpClient.GetAsync(url).Result;

            return base.SetResult<IEnumerable<PaymentIdInfoModel>>(result);
        }

        /// <summary>
        /// Gets PaymentIdInfoModel by transaction identifier.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="filterTypes">The filter types.</param>
        /// <returns>IEnumerable&lt;PaymentIdInfoModel&gt;.</returns>
        /// <exception cref="ProxyException"></exception>
        /// <exception cref="Error"></exception>
        public IEnumerable<PaymentIdInfoModel> GetByTransactionId(string transactionId, IEnumerable<EnumFilterType> filterTypes)
        {
            if (String.IsNullOrEmpty(transactionId))
            {
                throw new ProxyException(
                    new Error()
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.ToString(),
                        Message = "transactionId could not be null or empty.",
                        Code = (int)EnumErrorCodes.ProxyLayerError
                    }, typeof(EnumErrorCodes)
                );
            }

            var filterString = EnumFilterTypeHelper.ConvertToString(filterTypes);
            var url = UrlHelper.GenerateUrl(String.Format(Properties.Resources.URL_RES_PAYMENTIDINFO_BY_TRANSACTION, transactionId), filterString);

            HttpResponseMessage result = base._httpClient.GetAsync(url).Result;

            return base.SetResult<IEnumerable<PaymentIdInfoModel>>(result);
        }
        #endregion
    }
}
