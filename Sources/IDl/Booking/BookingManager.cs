using BND.Services.Matrix.Entities;
using BND.Services.Payments.iDeal.Interfaces;
using System;
using BND.Services.Matrix.Proxy.NET4.Interfaces;
using BND.Services.Payments.iDeal.Dal;
using BND.Services.Payments.iDeal.Dal.Enums;
using BND.Services.Payments.iDeal.Dal.Pocos;
using BND.Services.Payments.iDeal.Models;

namespace BND.Services.Payments.iDeal.Booking
{
    /// <summary>
    /// Class BookingManager provide methods to communications with Matrix, .
    /// </summary>
    public class BookingManager : IBookingManager
    {
        #region [Fields]
        /// <summary>
        /// The transaction repository
        /// </summary>
        public ITransactionRepository TransactionRepository;

        /// <summary>
        /// The accounts API
        /// </summary>
        public IAccountsApi AccountsApi;
        /// <summary>
        /// the Logger .
        /// </summary>
        private ILogger _Logger;
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="BookingManager"/> class.
        /// </summary>
        public BookingManager(ITransactionRepository transactionRepository, IAccountsApi accountsApi, ILogger logger)
        {
            TransactionRepository = transactionRepository;
            AccountsApi = accountsApi;
            _Logger = logger;
        }
        #endregion

        #region [Public Methods]
        /// <summary>
        /// Books to matrix.
        /// </summary>
        /// <param name="bookingInfo">The booking information.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.NullReferenceException">Cannot find transaction: +transactionId</exception>
        public int BookToMatrix(BookingModel bookingInfo, string accessToken, string transactionId, string entranceCode)
        {
            // TODO concurrency issue is not solved. It should be solved by wrapper by accepting guid with transaction id

            // Set default value of movementId to -1. If this value is returned, outside shoudl know something went wrong.
            int movementId = -1;

            // Validate input
            Validate(bookingInfo, accessToken, transactionId, entranceCode);

            // Get transaction from DB
            p_Transaction transaction = TransactionRepository.GetTransactionWithLatestStatus(transactionId, entranceCode);

            // If cannot transaction, throw exception
            if (transaction == null)
            {
                // log for manually fixing
                _Logger.Warning(String.Format("Cannot find the transaction data from TransactionId:{0} and EntranceCode:{1}", transactionId, entranceCode));
                throw new NullReferenceException("Cannot find transaction: "+transactionId);
            }

            // Check status
            // if "booking" - TODO
            // if "booked" - return movementId
            // if "notbooked":
            if (transaction.BookingStatus == EnumBookingStatus.Booking.ToString())
            {
                // log for manually fixing
                _Logger.Warning(String.Format("Current booking attempt for transaction {0} could not be proceeed because this transaction has already status 'booking'.", transactionId));
            }
            else if (transaction.BookingStatus == EnumBookingStatus.Booked.ToString())
            {
                if (transaction.MovementId.HasValue)
                {
                    movementId = transaction.MovementId.Value;
                }
                else
                {
                    // log for manually fixing
                    _Logger.Warning(string.Format("Booking status is booked but the movementId is not set. TransactionId: {0}", transactionId));
                }
            }
            else if (transaction.BookingStatus == EnumBookingStatus.NotBooked.ToString())
            {
                // Set status to booking and save to DB
                // if failed throw exception

                TransactionRepository.UpdateBookingStatus(transactionId, entranceCode, EnumBookingStatus.Booking);

                // Call matrix to make a booking
                Payment payment = new Payment
                {
                    Amount = bookingInfo.Amount,
                    Clarification = transactionId,
                    CounterpartyIBAN = bookingInfo.BookToIban,
                    CounterpartyBIC = bookingInfo.BookToBic,
                    Reference  = bookingInfo.Description,
                    SourceIBAN = bookingInfo.BookFromIban,
                    ValueDate = bookingInfo.BookDate,
                    CreditorDetails = new PaymentCustomerItem
                    {
                        CustomerName = bookingInfo.Creditor.CustomerName,
                        Postcode = bookingInfo.Creditor.Postcode,
                        Street = bookingInfo.Creditor.Street,
                        City = bookingInfo.Creditor.City,
                        CountryCode = bookingInfo.Creditor.CountryCode
                    },
                    DebtorDetails = new PaymentCustomerItem
                    {
                        CustomerName = bookingInfo.Debtor.CustomerName,
                        Postcode = bookingInfo.Debtor.Postcode,
                        Street = bookingInfo.Debtor.Street,
                        City = bookingInfo.Debtor.City,
                        CountryCode = bookingInfo.Debtor.CountryCode
                    }
                };

                bool isBooked = false;

                try
                {
                    movementId = AccountsApi.CreateIncomingPayment(bookingInfo.BookToIban, payment, accessToken);

                    isBooked = true;

                    // Update booking data
                    TransactionRepository.UpdateBookingData(transactionId, entranceCode, movementId, EnumBookingStatus.Booked, bookingInfo.BookDate);

                    // log booking is success
                    _Logger.Info(String.Format("Booking to matrix have been successfully. TransactionId: {0}", transactionId));
                 }
                catch (Exception ex)
                {
                    // If failed, set status to "notbooked" and throw exception
                    if (!isBooked)
                    {
                        TransactionRepository.UpdateBookingStatus(transactionId, entranceCode, EnumBookingStatus.NotBooked);
                    }

                    // Check exception, use error code from exception if it is iDealException, otherwise use error code from error message library.
                    iDealOperationException exception = new iDealOperationException(ErrorMessages.Error.Code, String.Format("Booking for transaction {0} failed. Check inner exception for details.", transactionId), ex);

                    // log booking error
                    _Logger.Error(exception);

                    throw;
                }
            }
            else
            {
                iDealOperationException exception = new iDealOperationException(ErrorMessages.Error.Code, String.Format("Booking status cannot recognize \"{0}\".", transaction.BookingStatus));

                // log for manually fixing
                _Logger.Error(exception);
            }

            return movementId;
        }
        #endregion

        #region [Private Methods]
        /// <summary>
        /// Validates the specified booking information.
        /// </summary>
        /// <param name="bookingInfo">The booking information.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <exception cref="System.ArgumentException">
        /// Parameter cannot be null nor empty;accessToken
        /// or
        /// Parameter cannot be null nor empty;transactionId
        /// or
        /// Parameter cannot be null nor empty;entranceCode
        /// or
        /// Parameter cannot be null;bookingInfo
        /// or
        /// Parameter cannot be null nor empty;bookingInfo.BookFromIban
        /// or
        /// Parameter cannot be null nor empty;bookingInfo.BookToIban
        /// or
        /// Parameter cannot be null nor empty;bookingInfo.BookToBic
        /// or
        /// Parameter cannot be null nor empty;bookingInfo.TransactionId
        /// or
        /// Parameter value must be greater than zero;bookingInfo.Amount
        /// or
        /// BookDate value is invalid;bookingInfo.BookDate
        /// or
        /// Parameter cannot be null;bookingInfo.Creditor
        /// or
        /// Parameter cannot be null nor empty;bookingInfo.Creditor.CustomerName
        /// or
        /// Parameter cannot be null nor empty;bookingInfo.Creditor.CountryCode
        /// or
        /// Parameter cannot be null;bookingInfo.Debtor
        /// or
        /// Parameter cannot be null nor empty;bookingInfo.Debtor.CustomerName
        /// or
        /// Parameter cannot be null nor empty;bookingInfo.Debtor.CountryCode
        /// </exception>
        private void Validate(BookingModel bookingInfo, string accessToken, string transactionId, string entranceCode)
        {
            // AccessToken
            if (String.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentException("Parameter cannot be null nor empty", "accessToken");
            }

            // TransactionId
            if (String.IsNullOrWhiteSpace(transactionId))
            {
                throw new ArgumentException("Parameter cannot be null nor empty", "transactionId");
            }

            // EntranceCode
            if (String.IsNullOrWhiteSpace(entranceCode))
                if (String.IsNullOrWhiteSpace(entranceCode))
                {
                    throw new ArgumentException("Parameter cannot be null nor empty", "entranceCode");
                }

            // bookingInfo
            if (bookingInfo == null)
            {
                throw new ArgumentException("Parameter cannot be null", "bookingInfo");
            }

            // TODO reuse IBAN validator from BND.Services.Infrastructure nuget package
            // bookingInfo.BookFromIban
            if (String.IsNullOrWhiteSpace(bookingInfo.BookFromIban))
            {
                throw new ArgumentException("Parameter cannot be null nor empty", "bookingInfo.BookFromIban");
            }

            // TODO reuse IBAN validator from BND.Services.Infrastructure nuget package
            // bookingInfo.BookToIban
            if (String.IsNullOrWhiteSpace(bookingInfo.BookToIban))
            {
                throw new ArgumentException("Parameter cannot be null nor empty", "bookingInfo.BookToIban");
            }

            // bookingInfo.BookToBic
            if (String.IsNullOrWhiteSpace(bookingInfo.BookToBic))
            {
                throw new ArgumentException("Parameter cannot be null nor empty", "bookingInfo.BookToBic");
            }

            // bookingInfo.Amount
            if (bookingInfo.Amount <= 0)
            {
                throw new ArgumentException("Parameter value must be greater than zero", "bookingInfo.Amount");
            }

            // bookingInfo.BookDate
            if (bookingInfo.BookDate == DateTime.MinValue)
            {
                throw new ArgumentException("BookDate value is invalid", "bookingInfo.BookDate");
            }

            //bookingInfo.Creditor
            if (bookingInfo.Creditor == null)
            {
                throw new ArgumentException("Parameter cannot be null", "bookingInfo.Creditor");
            }

            //bookingInfo.Creditor.CustomerName
            if (String.IsNullOrWhiteSpace(bookingInfo.Creditor.CustomerName))
            {
                throw new ArgumentException("Parameter cannot be null nor empty", "bookingInfo.Creditor.CustomerName");
            }

            //bookingInfo.Creditor.CountryCode
            if (String.IsNullOrWhiteSpace(bookingInfo.Creditor.CountryCode))
            {
                throw new ArgumentException("Parameter cannot be null nor empty", "bookingInfo.Creditor.CountryCode");
            }

            //bookingInfo.Debtor
            if (bookingInfo.Debtor == null)
            {
                throw new ArgumentException("Parameter cannot be null", "bookingInfo.Debtor");
            }

            //bookingInfo.Debtor.CustomerName
            if (String.IsNullOrWhiteSpace(bookingInfo.Debtor.CustomerName))
            {
                throw new ArgumentException("Parameter cannot be null nor empty", "bookingInfo.Debtor.CustomerName");
            }

            //bookingInfo.Debtor.CountryCode
            if (String.IsNullOrWhiteSpace(bookingInfo.Debtor.CountryCode))
            {
                throw new ArgumentException("Parameter cannot be null nor empty", "bookingInfo.Debtor.CountryCode");
            }
        }

        #endregion [Private Methods]
    }

}
