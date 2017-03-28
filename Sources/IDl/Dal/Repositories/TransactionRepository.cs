using BND.Services.Payments.iDeal.Dal.Enums;
using BND.Services.Payments.iDeal.Dal.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BND.Services.Payments.iDeal.Dal
{
    /// <summary>
    /// class TransactionRepository performing transaction table in database.
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        #region [Fields]
        /// <summary>
        /// The unit of work
        /// </summary>
        private IUnitOfWork _unitOfWork;
        #endregion

        #region [Constructor]
        /// <summary>
        /// Inserts the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        public TransactionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Inserts the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns>p_TransactionStatusHistory.</returns>
        public int Insert(p_Transaction transaction)
        {
            IRepository<p_Transaction> repository = _unitOfWork.GetRepository<p_Transaction>();
            repository.Insert(transaction);
            return _unitOfWork.CommitChanges();
        }

        /// <summary>
        /// Gets the specified transaction identifier with the latest status.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <returns>p_Transaction.</returns>
        public p_Transaction GetTransactionWithLatestStatus(string transactionId, string entranceCode)
        {
            IRepository<p_Transaction> repository = _unitOfWork.GetRepository<p_Transaction>();

            p_Transaction transaction = repository
                .GetQueryable(x => x.TransactionID == transactionId && x.EntranceCode == entranceCode)
                .FirstOrDefault();

            // TODO: Return "transaction" dont map it to the new poco resultTransaction. This causes a lot of confusion.
            // If this is really needed to hide some fields then hide them by precising ignore options in autoMapper
            if (transaction != null)
            {
                p_Transaction resultTransaction = new p_Transaction
                {
                    TransactionID = transaction.TransactionID,
                    Amount = transaction.Amount,
                    TransactionCreateDateTimestamp = transaction.TransactionCreateDateTimestamp,
                    ExpirationSecondPeriod = transaction.ExpirationSecondPeriod,
                    IsSystemFail = transaction.IsSystemFail,
                    TodayAttempts = transaction.TodayAttempts,
                    LatestAttemptsDateTimestamp = transaction.LatestAttemptsDateTimestamp,
                    BookingDate = transaction.BookingDate,
                    BookingGuid = transaction.BookingGuid,
                    BookingStatus = transaction.BookingStatus,
                    ConsumerIBAN = transaction.ConsumerIBAN,
                    BNDIBAN = transaction.BNDIBAN,
                    ConsumerName = transaction.ConsumerName,
                    Description = transaction.Description,
                    MovementId = transaction.MovementId
                };

                if (transaction.TransactionStatusHistories.Count > 0)
                {
                    var latestDate = transaction.TransactionStatusHistories.Max(x => x.StatusResponseDateTimeStamp);
                    resultTransaction.TransactionStatusHistories =
                        transaction.TransactionStatusHistories.Where(x => x.StatusResponseDateTimeStamp == latestDate).ToList();
                }

                return resultTransaction;
            }

            return null;
        }

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="consumeName">Consumer Name.</param>
        /// <param name="consumerIban">Consumer IBAN.</param>
        /// <param name="consumerBic">Consumer BIC.</param>
        /// <param name="isSystemFail">Is System Fail.</param>
        /// <param name="transactionStatusHistory">The transaction status history.</param>
        /// <returns>total number of affected row(s)</returns>
        public int UpdateStatus(string consumeName, string consumerIban, string consumerBic, bool isSystemFail,
            p_TransactionStatusHistory transactionStatusHistory)
        {
            IRepository<p_Transaction> transactionRepository = _unitOfWork.GetRepository<p_Transaction>();
            p_Transaction transaction = transactionRepository.GetById(transactionStatusHistory.TransactionID);
            transaction.ConsumerName = consumeName;
            transaction.ConsumerIBAN = consumerIban;
            transaction.ConsumerBIC = consumerBic;
            transaction.IsSystemFail = isSystemFail;

            IRepository<p_TransactionStatusHistory> transactionStatusHistoryRepository = _unitOfWork.GetRepository<p_TransactionStatusHistory>();
            transactionStatusHistoryRepository.Insert(transactionStatusHistory);

            return _unitOfWork.CommitChanges();
        }

        /// <summary>
        /// Update transaction attempts.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <param name="newAttempts">The new attempts that will be assigned to the transaction.</param>
        /// <returns>total number of affected row(s)</returns>
        public int UpdateTransactionAttempts(string transactionId, string entranceCode, int newAttempts)
        {
            IRepository<p_Transaction> repository = _unitOfWork.GetRepository<p_Transaction>();
            p_Transaction transaction = repository.Get(x => x.TransactionID == transactionId && x.EntranceCode == entranceCode).FirstOrDefault();
            if (transaction != null)
            {
                transaction.TodayAttempts = newAttempts;
                transaction.LatestAttemptsDateTimestamp = DateTime.Now;
                return _unitOfWork.CommitChanges(); 
            }

            return 0;
        }

        /// <summary>
        /// Updates the booking status.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <param name="newStatus">The new status.</param>
        /// <returns>p_Transaction.</returns>
        public p_Transaction UpdateBookingStatus(string transactionId, string entranceCode, EnumBookingStatus newStatus)
        {
            IRepository<p_Transaction> repository = _unitOfWork.GetRepository<p_Transaction>();
            p_Transaction transaction = repository.Get(x => x.TransactionID == transactionId && x.EntranceCode == entranceCode).FirstOrDefault();
            if (transaction != null)
            {
                transaction.BookingStatus = newStatus.ToString();                
                _unitOfWork.CommitChanges();
            }

            // return transaction that already updated status, if not found transaction will return null
            return transaction;
        }

        /// <summary>
        /// Updates the booking data after booking to matrix.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <param name="movementId">The movement identifier.</param>
        /// <param name="status">The status.</param>
        /// <param name="bookingDate">The booking date.</param>
        /// <returns>p_Transaction.</returns>
        public p_Transaction UpdateBookingData(string transactionId, string entranceCode, int movementId, EnumBookingStatus status, DateTime bookingDate)
        {
            IRepository<p_Transaction> repository = _unitOfWork.GetRepository<p_Transaction>();
            p_Transaction transaction = repository.Get(x => x.TransactionID == transactionId && x.EntranceCode == entranceCode).FirstOrDefault();
            if (transaction != null)
            {
                transaction.BookingStatus = status.ToString();
                transaction.BookingDate = bookingDate;
                transaction.MovementId = movementId;
                _unitOfWork.CommitChanges();
            }

            // return transaction that already updated, if not found transaction will return null
            return transaction;
        }

        #endregion


    }
}
