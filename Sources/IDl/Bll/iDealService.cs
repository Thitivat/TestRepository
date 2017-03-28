using BND.Services.Payments.iDeal.Dal;
using BND.Services.Payments.iDeal.Dal.Enums;
using BND.Services.Payments.iDeal.Dal.Pocos;
using BND.Services.Payments.iDeal.iDealClients.Base;
using BND.Services.Payments.iDeal.iDealClients.Directory;
using BND.Services.Payments.iDeal.iDealClients.Status;
using BND.Services.Payments.iDeal.iDealClients.Transaction;
using BND.Services.Payments.iDeal.Interfaces;
using BND.Services.Payments.iDeal.JobQueue.Bll.Interfaces;
using BND.Services.Payments.iDeal.Models;
using BND.Services.Payments.iDeal.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using IssuerModel = BND.Services.Payments.iDeal.Models.IssuerModel;

namespace BND.Services.Payments.iDeal
{
    /// <summary>
    /// Class iDealService is the core business class of iDeal, it provides all necessary functionalities to access iDeal service.
    /// </summary>
    public class iDealService : IiDealService
    {
        #region [Constant]
        /// <summary>
        /// The default_ queue_ interval 
        /// </summary>
        private const int DEFAULT_QUEUE_INTERVAL = 15;       
        #endregion

        #region [Fields]
        /// <summary>
        /// Directory Repostiroy.
        /// </summary>
        private IDirectoryRepository _directoryRepository;
        /// <summary>
        /// The Transaction repository.
        /// </summary>
        private ITransactionRepository _transactionRepository;
        /// <summary>
        /// The Setting repository.
        /// </summary>
        private ISettingRepository _settingRepository;
        /// <summary>
        /// The IDealClient call iDeal service.
        /// </summary>
        private IiDealClient _idealClient;
        /// <summary>
        /// the Logger .
        /// </summary>
        private ILogger _Logger;
        /// <summary>
        /// The job queue manager
        /// </summary>
        private IJobQueueManager _jobQueueManager;
        /// <summary>
        /// The booking manager.
        /// </summary>
        private IBookingManager _bookingManager;
        /// <summary>
        /// The client data provider.
        /// </summary>
        private IClientDataProvider _clientDataProvider;
        /// <summary>
        /// The BND BIC.
        /// </summary>
        private string _bndBic ;
        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="iDealService" /> class.
        /// </summary>
        /// <param name="directoryRepository">The directory repository.</param>
        /// <param name="transactionRepository">The transaction repository.</param>
        /// <param name="settingRepository">The setting repository.</param>
        /// <param name="iDealClient">The i deal client.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="jobQueueManager">The job queue manager.</param>
        /// <param name="bookingManager">The booking manager.</param>
        /// <param name="clientDataProvider"></param>
        /// <param name="bndBic"></param>
        public iDealService(
            IDirectoryRepository directoryRepository,
            ITransactionRepository transactionRepository,
            ISettingRepository settingRepository,
            IiDealClient iDealClient,            
            ILogger logger,
            IJobQueueManager jobQueueManager,
            IBookingManager bookingManager,
            IClientDataProvider clientDataProvider,
            string bndBic)
        {
            _directoryRepository = directoryRepository;
            _transactionRepository = transactionRepository;
            _settingRepository = settingRepository;
            _idealClient = iDealClient;
            _Logger = logger;
            _jobQueueManager = jobQueueManager;
            _bookingManager = bookingManager;
            _clientDataProvider = clientDataProvider;
            _bndBic = bndBic;
        }
        #endregion

        #region [Public Methods]
        /// <summary>
        /// Gets the issueing bank list that stored in database the data will updated one time per day from iDeal,
        /// that will execute at first request of the day.
        /// </summary>
        /// <returns>List of directories which retrieve from iDeal.</returns>
        /// <exception cref="iDealOperationException">Directory could not be found.</exception>
        public IEnumerable<DirectoryModel> GetDirectories()
        {
            // Get current date issuer list  from database 
            p_Directory directories = _directoryRepository.Get();

            // if no data or last update is not today will get from iDEAL
            if (directories == null || directories.LastDirectoryRequestDateTimestamp.Date != DateTime.Now.Date)
            {
                try
                {
                    DirectoryResponse idealDirectories = _idealClient.SendDirectoryRequest();
                    if (idealDirectories == null || !idealDirectories.Issuers.Any())
                        throw new iDealOperationException("Directory could not be found.");

                    if (directories == null ||
                        directories.DirectoryDateTimestamp.CompareTo(idealDirectories.DirectoryDateTimeStampLocalTime) <= 0)
                    {
                        // Creates Directory poco object from iDeal response.
                        p_Directory iDealDirectories = new p_Directory
                        {
                            AcquirerID = idealDirectories.AcquirerId.ToString(),
                            DirectoryDateTimestamp = idealDirectories.DirectoryDateTimeStampLocalTime,
                            LastDirectoryRequestDateTimestamp = DateTime.Now,
                            Issuers = idealDirectories.Issuers.Select(s => new p_Issuer()
                            {
                                AcquirerID = idealDirectories.AcquirerId.ToString(),
                                CountryNames = s.CountryNames,
                                IssuerID = s.IssuerID,
                                IssuerName = s.IssuerName
                            }).ToList()
                        };

                        //insert int database and delete old record
                        _directoryRepository.UpdateDirectory(iDealDirectories);

                        _Logger.Info("Directories have been updated successfully.");
                        directories = _directoryRepository.Get();
                    }
                }
                catch (Exception ex)
                {
                    // Check exception, use error code from exception if it is iDealException, otherwise use error code from error message library.
                    iDealOperationException exception = (ex is iDealException)
                                                        ? new iDealOperationException(((iDealException)ex).ErrorCode, ex.Message)
                                                        : new iDealOperationException(ErrorMessages.Error.Code, ErrorMessages.Error.Message, ex);
                    // Logs what error is.
                    _Logger.Error(exception);

                    // Throws exception as iDealOperationException only.
                    throw exception;
                }
            }

            // Create the directory model from poco object.
            List<DirectoryModel> result = directories.Issuers.GroupBy(s => s.CountryNames).Select(x => new DirectoryModel()
            {
                CountryNames = x.Key,
                Issuers = x.ToList().Select(y => new IssuerModel()
                {
                    IssuerID = y.IssuerID,
                    IssuerName = y.IssuerName
                }).ToList()
            }).ToList();

            return result;

        }

        /// <summary>
        /// Creates a transaction to request a payment to iDeal service for transfering money from customer to BND bank account.
        /// </summary>
        /// <param name="transactionRequest">The transaction request.</param>
        /// <returns>TransactionResponseModel.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// transactionRequest
        /// or
        /// transactionRequest.BNDIBAN
        /// or
        /// transactionRequest.Currency
        /// or
        /// transactionRequest.CustomerIBAN
        /// or
        /// transactionRequest.IssuerID
        /// or
        /// transactionRequest.Language
        /// or
        /// transactionRequest.MerchantReturnURL
        /// or
        /// transactionRequest.PaymentType
        /// or
        /// transactionRequest.PurchaseID
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// transactionRequest.Amount;The value could not be less than or equals zero.
        /// or
        /// transactionRequest.ExpirationPeriod
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The value is wrong format, It has to be ALPHA-3 currency code following ISO 4217.;transactionRequest.Currency
        /// or
        /// The value is wrong format, It has to be ALPHA-2 language code following ISO 639-2.;transactionRequest.Language
        /// </exception>
        public TransactionResponseModel CreateTransaction(TransactionRequestModel transactionRequest)
        {
            // Validates all required parameters.
            if (transactionRequest == null)
            {
                throw new ArgumentNullException("transactionRequest");
            }
            if (transactionRequest.Amount <= 0m)
            {
                throw new ArgumentOutOfRangeException("transactionRequest.Amount", "The value could not be less than or equals zero.");
            }
            if (String.IsNullOrWhiteSpace(transactionRequest.BNDIBAN))
            {
                throw new ArgumentNullException("transactionRequest.BNDIBAN");
            }
            if (String.IsNullOrWhiteSpace(transactionRequest.Currency))
            {
                throw new ArgumentNullException("transactionRequest.Currency");
            }
            if (!Regex.IsMatch(transactionRequest.Currency, "^[a-zA-Z]{3}$"))
            {
                throw new ArgumentException("The value is wrong format, It has to be ALPHA-3 currency code following ISO 4217.",
                                            "transactionRequest.Currency");
            }
            if (String.IsNullOrWhiteSpace(transactionRequest.CustomerIBAN))
            {
                throw new ArgumentNullException("transactionRequest.CustomerIBAN");
            }
            int minExpiration = Convert.ToInt32(_settingRepository.GetValueByKey("MinExpirationPeriodSecond"));
            int maxExpiration = Convert.ToInt32(_settingRepository.GetValueByKey("MaxExpirationPeriodSecond"));
            if (transactionRequest.ExpirationPeriod != default(int) &&
                (transactionRequest.ExpirationPeriod < minExpiration || transactionRequest.ExpirationPeriod > maxExpiration))
            {
                throw new ArgumentOutOfRangeException("transactionRequest.ExpirationPeriod",
                                                      String.Format("The value must between {0}-{1} seconds.", minExpiration, maxExpiration));
            }
            if (String.IsNullOrWhiteSpace(transactionRequest.IssuerID))
            {
                throw new ArgumentNullException("transactionRequest.IssuerID");
            }
            if (String.IsNullOrWhiteSpace(transactionRequest.Language))
            {
                throw new ArgumentNullException("transactionRequest.Language");
            }
            if (!Regex.IsMatch(transactionRequest.Language, "^[a-zA-Z]{2}$"))
            {
                throw new ArgumentException("The value is wrong format, It has to be ALPHA-2 language code following ISO 639-2.",
                                            "transactionRequest.Language");
            }
            if (transactionRequest.MerchantReturnURL == null)
            {
                throw new ArgumentNullException("transactionRequest.MerchantReturnURL");
            }
            if (String.IsNullOrWhiteSpace(transactionRequest.PaymentType))
            {
                throw new ArgumentNullException("transactionRequest.PaymentType");
            }
            if (String.IsNullOrWhiteSpace(transactionRequest.PurchaseID))
            {
                throw new ArgumentNullException("transactionRequest.PurchaseID");
            }

            try
            {
                // Generates new entrance code.
                string newEntranceCode = EntranceCode.GenerateCode();
                int expirationPeriod = (transactionRequest.ExpirationPeriod == default(int))
                                       ? Convert.ToInt32(_settingRepository.GetValueByKey("DefaultExpirationPeriodSecond"))
                                       : transactionRequest.ExpirationPeriod;
                // Calls to real iDeal service.
                TransactionResponse response = _idealClient.SendTransactionRequest(
                    transactionRequest.IssuerID,
                    transactionRequest.MerchantReturnURL.ToString(),
                    transactionRequest.PurchaseID,
                    transactionRequest.Amount,
                    TimeSpan.FromSeconds(expirationPeriod),
                    transactionRequest.Description,
                    newEntranceCode,
                    transactionRequest.Language.ToLower(), // iDeal service allows only lower case.
                    transactionRequest.Currency
                );
                // Insert to our database.
                _transactionRepository.Insert(new p_Transaction
                {
                    AcquirerID = response.AcquirerId.ToString(),
                    Amount = transactionRequest.Amount,
                    BNDIBAN = transactionRequest.BNDIBAN,
                    Currency = transactionRequest.Currency,
                    Description = transactionRequest.Description,
                    EntranceCode = newEntranceCode,
                    ExpectedCustomerIBAN = transactionRequest.CustomerIBAN,
                    ExpirationSecondPeriod = expirationPeriod,
                    IssuerAuthenticationURL = response.IssuerAuthenticationUrl,
                    IssuerID = transactionRequest.IssuerID,
                    Language = transactionRequest.Language,
                    MerchantID = _idealClient.Configuration.MerchantId,
                    MerchantReturnURL = transactionRequest.MerchantReturnURL.ToString(),
                    PaymentType = transactionRequest.PaymentType,
                    PurchaseID = transactionRequest.PurchaseID,
                    SubID = _idealClient.Configuration.MerchantSubId,
                    TransactionCreateDateTimestamp = response.CreatedDate,
                    TransactionID = response.TransactionId,
                    TransactionRequestDateTimestamp = response.TransactionRequestDateTimeStamp,
                    TransactionResponseDateTimestamp = response.CreateDateTimeStamp,
                    BookingStatus = EnumBookingStatus.NotBooked.ToString()

                });

                // Calculate the job interval to passing to queue.
                int jobInterval = CalculateJobInterval(_settingRepository.GetValueByKey("MaxExpirationPeriodSecond"));
                // Add to JobQueue
                _jobQueueManager.CreateJobQueue(response.TransactionId, newEntranceCode, jobInterval);

                // Logs.
                _Logger.Info("Transaction request has been sent successfully.");

                // Returns object.
                return new TransactionResponseModel
                {
                    EntranceCode = newEntranceCode,
                    IssuerAuthenticationURL = new Uri(response.IssuerAuthenticationUrl),
                    PurchaseID = response.PurchaseId,
                    TransactionID = response.TransactionId
                };
            }
            catch (Exception ex)
            {
                // Check exception, use error code from exception if it is iDealException, otherwise use error code from error message library.
                iDealOperationException exception = (ex is iDealException)
                                                    ? new iDealOperationException(((iDealException)ex).ErrorCode, ex.Message)
                                                    : new iDealOperationException(ErrorMessages.Error.Code, ErrorMessages.Error.Message, ex);
                // Logs what error is.
                _Logger.Error(exception);

                // Throws exception as iDealOperationException only.
                throw exception;
            }
        }

        /// <summary>
        /// Gets the status of specific Transaction to check what going on in processing of payment.
        /// </summary>
        /// <param name="entranceCode">The entranceCode this got from transaction response after called CreateTransaction</param>
        /// <param name="transactionId">The transactionID this got from transaction response after called CreateTransaction</param>
        /// <returns>EnumQueryStatus.</returns>
        /// <exception cref="System.ArgumentNullException">entranceCode
        /// or
        /// transactionID</exception>
        /// <exception cref="System.Data.ObjectNotFoundException">Transaction could not be found.</exception>
        /// <exception cref="System.InvalidOperationException">
        /// This transaction is invalid, please contact administrator.
        /// or
        /// Too many request, please try again later.
        /// </exception>
        /// <exception cref="iDealOperationException"></exception>
        public EnumQueryStatus GetStatus(string entranceCode, string transactionId)
        {
            // initialize parameters
            EnumQueryStatus resultStatus = EnumQueryStatus.Open;
            bool isSystemFail = false;
            string message = "";

            // Checks required parameters
            ValidateGetStatusParameters(entranceCode, transactionId);

            // Gets transaction by using entrance code and transaction id.
            p_Transaction transaction = _transactionRepository.GetTransactionWithLatestStatus(transactionId, entranceCode);

            // Check if transaction data is not found will throw ObjectNotfoundException.
            if (transaction == null)
            {
                throw new ObjectNotFoundException("Transaction could not be found.");
            }

            // If the transaction is not valid, the request to iDeal should not be made.
            if (transaction.IsSystemFail)
            {
                throw new InvalidOperationException("This transaction is invalid, please contact administrator.");
            }        

            // if used to call will check iDeal policy.
            if (transaction.TransactionStatusHistories.Any() &&
                transaction.TransactionStatusHistories.First().Status != EnumQueryStatus.Open.ToString())
            {
                _Logger.Info("Status request success. TransactionId: "+transactionId);
                
                // Set result as current status
                resultStatus = (EnumQueryStatus)Enum.Parse(typeof(EnumQueryStatus), transaction.TransactionStatusHistories.First().Status);
            }
            else
            {
                // get transaction from iDeal

                ValidateTransactionAttempt(transaction);

                try
                {
                    // Gets status request from ideal service.
                    StatusResponse status = _idealClient.SendStatusRequest(transactionId);

                    message = "Status request success. TransactionId: " + transactionId;

                    // If transaction expired in open state, mark as error.
                    if (status.Status == EnumQueryStatus.Open.ToString() &&
                        transaction.TransactionCreateDateTimestamp.AddSeconds(transaction.ExpirationSecondPeriod) < DateTime.Now)
                    {
                        isSystemFail = true;

                        message = String.Format(
                            "Transaction status is invalid.(expected status is {0} but it was {1}, please contact administrator. TransactionId: {2}",
                            EnumQueryStatus.Expired.ToString(), status.Status, transactionId);
                    }
                    // If amount is not valid, mark as error.
                    else if (status.Status == EnumQueryStatus.Success.ToString() && status.Amount != transaction.Amount)
                    {
                        isSystemFail = true;

                        message = String.Format(
                            "The transaction amount is invalid.(expected amount is {0} but it was {1}), please contact administrator. TransactionId: {2}",
                            transaction.Amount, status.Amount, transactionId);
                    }

                    p_TransactionStatusHistory txHistory = new p_TransactionStatusHistory
                    {
                        TransactionID = status.TransactionId,
                        Status = status.Status,
                        StatusDateTimeStamp = status.StatusDate,
                        StatusRequestDateTimeStamp = status.CreateDateTimeStamp,
                        StatusResponseDateTimeStamp = DateTime.Now
                    };

                    // update transaction data.
                    _transactionRepository.UpdateStatus(status.ConsumerName, status.ConsumerIBAN, status.ConsumerBIC, isSystemFail, txHistory);

                    if (isSystemFail)
                    {
                        throw new iDealOperationException(ErrorMessages.Error.Code, message);
                    }

                    _Logger.Info(message);

                    resultStatus = (EnumQueryStatus)Enum.Parse(typeof(EnumQueryStatus), status.Status);
                }
                finally
                {
                    _transactionRepository.UpdateTransactionAttempts(transactionId, entranceCode,
                        transaction.LatestAttemptsDateTimestamp.GetValueOrDefault().Date < DateTime.Now.Date
                        ? 1
                        : transaction.TodayAttempts++);
                }
            }

            // If status is Success, then call booking method
            // This ensures booking is done.
            if (resultStatus == EnumQueryStatus.Success)
            {
                try
                {
                    transaction = _transactionRepository.GetTransactionWithLatestStatus(transactionId, entranceCode);

                    // Check if transaction data is not found will throw ObjectNotfoundException.
                    if (transaction == null)
                    {
                        throw new ObjectNotFoundException("Booking couldn't be made because transaction could not be found.");
                    }

                    BookingModel bookingModel = new BookingModel()
                    {
                        Description = transaction.Description,
                        BookFromIban = transaction.ConsumerIBAN,
                        BookToIban = transaction.BNDIBAN,
                        BookToBic = _bndBic,
                        Amount = transaction.Amount,
                        BookDate = DateTime.Now,
                        Debtor = new Contract()
                        {
                            CustomerName = transaction.ConsumerName,
                            CountryCode = GetCountryCodeFromIban(transaction.ConsumerIBAN)
                        },
                        Creditor = new Contract()
                        {
                            CustomerName = _clientDataProvider.GetClientNameByIban(transaction.BNDIBAN),
                            CountryCode = GetCountryCodeFromIban(transaction.BNDIBAN)
                        }
                    };

                    int movementId = _bookingManager.BookToMatrix(bookingModel, GetToken(), transactionId, entranceCode);

                    if (movementId < 0)
                    {
                        resultStatus = EnumQueryStatus.IdealSuccessBookingFailed;
                        _Logger.Warning(string.Format("Booking attempt for transaction: {0} returned wrong movementId: {1}", transactionId, movementId));
                    } 
                    else
                    {
                        _Logger.Info(string.Format("Booking attempt for transaction: {0} was successfully.", transactionId));
                    }

                }
                catch (Exception ex)
                {
                    resultStatus = EnumQueryStatus.IdealSuccessBookingFailed;

                    // Check exception, use error code from exception if it is iDealException, otherwise use error code from error message library.
                    iDealOperationException exception = (ex is iDealException)
                                                        ? new iDealOperationException(((iDealException)ex).ErrorCode, ex.Message)
                                                        : new iDealOperationException(ErrorMessages.Error.Code, string.Format("Booking attempt for transaction: {0} returned error.", transactionId), ex);

                    _Logger.Error(exception);
                }
            }

            return resultStatus;
        }

        #endregion

        #region [Private Methods]

        /// <summary>
        ///  Calculate job queue interval from setting table and convert to minute then return.
        /// </summary>
        /// <param name="expiration">The expiration.</param>
        /// <returns>System.Int32.</returns>
        private int CalculateJobInterval(string expiration)
        {
            if(String.IsNullOrEmpty(expiration))
            {
                return DEFAULT_QUEUE_INTERVAL;
            }

            // return by plus 1 minute more expire.
            return (Convert.ToInt32(expiration) / 60) + 1;
        }

        /// <summary>
        /// Validates the transaction attempt. It will check if request for transaction status is aloowed.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <exception cref="System.InvalidOperationException">
        /// Too many request, please try again later.
        /// </exception>
        private void ValidateTransactionAttempt(p_Transaction transaction)
        {
            int maxRetriesPerDays = Convert.ToInt32(_settingRepository.GetValueByKey("MaxRetriesPerDays"));
            // check if system was attempt for request more than 5 times system will not allow to continue.
            if (transaction.TodayAttempts >= maxRetriesPerDays &&
                transaction.LatestAttemptsDateTimestamp.GetValueOrDefault().Date == DateTime.Now.Date)
            {
                throw new InvalidOperationException(
                    String.Format("Transaction has reached maximum call {0} times per day, please try again tomorrow.",
                        maxRetriesPerDays));
            }

            int minExpirationPeriodSecond = Convert.ToInt32(_settingRepository.GetValueByKey("MinExpirationPeriodSecond"));
            // check if last request is not passed 60 seconds will not allow to continue.
            if (transaction.LatestAttemptsDateTimestamp.GetValueOrDefault().AddSeconds(minExpirationPeriodSecond) > DateTime.Now)
            {
                throw new InvalidOperationException("Too many request, please try again later.");
            }
        }

        /// <summary>
        /// Validates parameters of GetStatus method.
        /// <param name="entranceCode">The entrance code.</param>
        /// <param name="transactionID">The transaction identifier.</param>
        /// <exception cref="System.ArgumentNullException">
        /// entranceCode
        /// or
        /// transactionID
        /// </exception>
        private static void ValidateGetStatusParameters(string entranceCode, string transactionID)
        {
            if (String.IsNullOrEmpty(entranceCode))
            {
                throw new ArgumentNullException("entranceCode");
            }
            if (String.IsNullOrEmpty(transactionID))
            {
                throw new ArgumentNullException("transactionID");
            }
        }

        /// <summary>
        /// Returns token to be used while calling matrix.
        /// </summary>
        /// <returns>System.String.</returns>
        private string GetToken()
        {
            // TODO this is just mock. It have to be real functionality when identification service is ready
            return "FakeToken";
        }

        /// <summary>
        /// Will parse IBAN and wil lreturn country code.
        /// </summary>
        /// <param name="iban">The iban.</param>
        /// <returns>System.String.</returns>
        private string GetCountryCodeFromIban(string iban)
        {
            return iban.Substring(0, 2);
        }
        #endregion
    }
}
