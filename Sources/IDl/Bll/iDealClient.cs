using BND.Services.Payments.iDeal.iDealClients.Base;
using BND.Services.Payments.iDeal.iDealClients.Directory;
using BND.Services.Payments.iDeal.iDealClients.Interfaces;
using BND.Services.Payments.iDeal.iDealClients.Models;
using BND.Services.Payments.iDeal.iDealClients.Status;
using BND.Services.Payments.iDeal.iDealClients.Transaction;
using BND.Services.Payments.iDeal.Interfaces;
using System;
using SYSConfig = System.Configuration;

namespace BND.Services.Payments.iDeal
{
    /// <summary>
    /// Class iDealClient that provide methods to request a list of bank, create transaction or check status of transaction with iDeal
    /// </summary>
    public class iDealClient : IiDealClient
    {
        #region [Fields]
        /// <summary>
        /// The _configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The _signature provider
        /// </summary>
        private readonly ISignatureProvider _signatureProvider;

        /// <summary>
        /// The _i deal HTTP request
        /// </summary>
        private readonly IiDealHttpRequest _iDealHttpRequest;

        /// <summary>
        /// The _i deal HTTP response handler
        /// </summary>
        private readonly IiDealHttpResponseHandler _iDealHttpResponseHandler;
        #endregion


        #region [Property]
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration
        {
            get { return _configuration; }
        }
        #endregion


        #region [Constructors]
        /// <summary>
        /// Initializes a new instance of the <see cref="iDealClient" /> class.
        /// </summary>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="subId">The sub identifier.</param>
        public iDealClient(string merchantId, int subId) :
            this(new DefaultConfiguration((ConfigurationSectionHandler)SYSConfig.ConfigurationManager.GetSection("iDeal"), merchantId, subId)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="iDealClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public iDealClient(IConfiguration configuration) :
            this(configuration,
                new SignatureProvider(configuration.AcceptantCertificate),
                new iDealHttpRequest(),
                new iDealHttpResponseHandler()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="iDealClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="signatureProvider">The signature provider.</param>
        /// <param name="iDealHttpRequest">The i deal HTTP request.</param>
        /// <param name="iDealHttpResponseHandler">The i deal HTTP response handler.</param>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Merchant Id is not set
        /// or
        /// Merchant Id cannot contain more as 9 characters
        /// or
        /// SubId must contain a value ranging from 0 to 6
        /// or
        /// Url of acquirer is not set
        /// or
        /// Acceptant's certificate is not set
        /// or
        /// Acceptant's certificate does not contain private key
        /// or
        /// Acquirer's certificate is not set</exception>
        public iDealClient(IConfiguration configuration, ISignatureProvider signatureProvider, IiDealHttpRequest iDealHttpRequest,
                           IiDealHttpResponseHandler iDealHttpResponseHandler)
        {
            // Configuration guard clauses
            if (String.IsNullOrWhiteSpace(configuration.MerchantId))
            {
                throw new SYSConfig.ConfigurationErrorsException("Merchant Id is not set");
            }
            if (configuration.MerchantId.Length > 9)
            {
                throw new SYSConfig.ConfigurationErrorsException("Merchant Id cannot contain more as 9 characters");
            }
            if (configuration.MerchantSubId < 0 || configuration.MerchantSubId > 6)
            {
                throw new SYSConfig.ConfigurationErrorsException("SubId must contain a value ranging from 0 to 6");
            }
            if (String.IsNullOrWhiteSpace(configuration.AcquirerUrl))
            {
                throw new SYSConfig.ConfigurationErrorsException("Url of acquirer is not set");
            }
            if (configuration.AcceptantCertificate == null)
            {
                throw new SYSConfig.ConfigurationErrorsException("Acceptant's certificate is not set");
            }
            if (!configuration.AcceptantCertificate.HasPrivateKey)
            {
                throw new SYSConfig.ConfigurationErrorsException("Acceptant's certificate does not contain private key");
            }

            _configuration = configuration;
            _signatureProvider = signatureProvider;
            _iDealHttpRequest = iDealHttpRequest;
            _iDealHttpResponseHandler = iDealHttpResponseHandler;

        }
        #endregion


        #region [Method]
        /// <summary>
        /// Sends the directory request.
        /// </summary>
        /// <returns>DirectoryResponse.</returns>
        public DirectoryResponse SendDirectoryRequest()
        {
            // Set up dependencies for http request
            iDealRequestBase directoryRequest = new DirectoryRequest(_configuration.MerchantId, _configuration.MerchantSubId);

            // execute request
            return (DirectoryResponse)_iDealHttpRequest.SendRequest(directoryRequest, _signatureProvider, _configuration.AcquirerUrl,
                                                                    _iDealHttpResponseHandler);
        }

        /// <summary>
        /// Sends the transaction request.
        /// </summary>
        /// <param name="issuerCode">The issuer code.</param>
        /// <param name="merchantReturnUrl">The merchant return URL.</param>
        /// <param name="purchaseId">The purchase identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="expirationPeriod">The expiration period.</param>
        /// <param name="description">The description.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <param name="language">The language.</param>
        /// <param name="currency">The currency.</param>
        /// <returns>TransactionResponse.</returns>
        public TransactionResponse SendTransactionRequest(string issuerCode, string merchantReturnUrl, string purchaseId, decimal amount,
                                                          TimeSpan? expirationPeriod, string description, string entranceCode, string language,
                                                          string currency)
        {
            // Set up dependencies for http request
            var transactionRequest = new TransactionRequest(_configuration.MerchantId, _configuration.MerchantSubId, issuerCode,
                                                            merchantReturnUrl, purchaseId, amount, expirationPeriod, description, entranceCode,
                                                            language, currency);

            // Execute http request
            var result = (TransactionResponse)_iDealHttpRequest.SendRequest(transactionRequest, _signatureProvider, _configuration.AcquirerUrl,
                                                                      _iDealHttpResponseHandler);

            result.TransactionRequestDateTimeStamp = transactionRequest.CreateDateTimeStamp;
            return result;
        }

        /// <summary>
        /// Retrieve status of transaction (Success, Cancelled Failure, Expired, Open)
        /// </summary>
        /// <param name="transactionId">Unique identifier of transaction obtained when the transaction was issued</param>
        /// <returns>StatusResponse.</returns>
        public StatusResponse SendStatusRequest(string transactionId)
        {
            // Set up dependencies for http request
            var statusRequest = new StatusRequest(_configuration.MerchantId, _configuration.MerchantSubId, transactionId);

            // Execute http request
            var result = _iDealHttpRequest.SendRequest(statusRequest, _signatureProvider, _configuration.AcquirerUrl, _iDealHttpResponseHandler);
            if (result != null)
            {
                return (StatusResponse)result;
            }
            else { return null; }
        }
        #endregion
    }
}
