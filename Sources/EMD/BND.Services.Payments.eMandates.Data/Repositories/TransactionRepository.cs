using BND.Services.Payments.eMandates.Domain.Interfaces;
using System;
using System.Linq;

namespace BND.Services.Payments.eMandates.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        #region [Fields]

        /// <summary>
        /// The unit of work.
        /// </summary>
        private IUnitOfWork _unitOfWork;
        /// <summary>
        /// The _disposed flag.
        /// </summary>
        private bool _disposed = false;
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public TransactionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        /// <summary>
        /// Inserts the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns>System.Int32.</returns>
        public int Insert(Models.Transaction transaction)
        {
            IRepository<Models.Transaction> repository = _unitOfWork.GetRepository<Models.Transaction>();
            repository.Insert(transaction);
            return _unitOfWork.CommitChanges();
        }

        /// <summary>
        /// Gets the transaction with latest status.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>Transaction.</returns>
        public Models.Transaction GetTransactionWithLatestStatus(string transactionId)
        {
            IRepository<Models.Transaction> repository = _unitOfWork.GetRepository<Models.Transaction>();

            Models.Transaction transaction = repository
                .GetQueryable(x => x.TransactionID == transactionId)
                .FirstOrDefault();

            if (transaction != null)
            {
                Models.Transaction resultTransaction = new Models.Transaction
                {
                    TransactionID = transaction.TransactionID,
                    DebtorBankID = transaction.DebtorBankID,
                    DebtorReference = transaction.DebtorReference,
                    EMandateID = transaction.EMandateID,
                    EMandateReason = transaction.EMandateReason,
                    EntranceCode = transaction.EntranceCode,
                    MaxAmount = transaction.MaxAmount,
                    TransactionCreateDateTimestamp = transaction.TransactionCreateDateTimestamp,
                    ExpirationPeriod = transaction.ExpirationPeriod,
                    IsSystemFail = transaction.IsSystemFail,
                    PurchaseID = transaction.PurchaseID,
                    SequenceType = transaction.SequenceType,
                    IssuerAuthenticationUrl = transaction.IssuerAuthenticationUrl,
                    MerchantReturnUrl = transaction.MerchantReturnUrl,
                    RawMessage = transaction.RawMessage
                };

                if (transaction.TransactionStatusHistories.Count > 0)
                {
                    var latestDate = transaction.TransactionStatusHistories.Max(x => x.StatusDateTimestamp);
                    resultTransaction.TransactionStatusHistories =
                        transaction.TransactionStatusHistories.Where(x => x.StatusDateTimestamp == latestDate).ToList();
                }

                return resultTransaction;
            }

            return null;
        }

        /// <summary>
        /// Updates the transaction attempts.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="newAttempts">The new attempts.</param>
        /// <returns>System.Int32.</returns>
        public int UpdateTransactionAttempts(string transactionId, int newAttempts)
        {
            IRepository<Models.Transaction> repository = _unitOfWork.GetRepository<Models.Transaction>();
            Models.Transaction transaction = repository.Get(x => x.TransactionID == transactionId).FirstOrDefault();
            if (transaction != null)
            {
                transaction.TodayAttempts = newAttempts;
                transaction.LatestAttemptsDateTimestamp = DateTime.Now;
                return _unitOfWork.CommitChanges();
            }

            return 0;
        }

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="isSystemFail">if set to <c>true</c> [is system fail].</param>
        /// <param name="transactionStatusHistory">The transaction status history.</param>
        /// <returns>System.Int32.</returns>
        public int UpdateStatus(bool isSystemFail, Models.TransactionStatusHistory transactionStatusHistory)
        {
            //TODO: add new table (eMandate) and save AcceptanceReport object to table

            IRepository<Models.Transaction> transactionRepository = _unitOfWork.GetRepository<Models.Transaction>();
            Models.Transaction transaction = transactionRepository.GetById(transactionStatusHistory.TransactionID);

            transaction.IsSystemFail = isSystemFail;

            IRepository<Models.TransactionStatusHistory> transactionStatusHistoryRepository = _unitOfWork.GetRepository<Models.TransactionStatusHistory>();
            transactionStatusHistoryRepository.Insert(transactionStatusHistory);

            return _unitOfWork.CommitChanges();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_unitOfWork != null)
                {
                    _unitOfWork = null;
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Validates is disposed.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException"></exception>
        private void ValidateDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}
