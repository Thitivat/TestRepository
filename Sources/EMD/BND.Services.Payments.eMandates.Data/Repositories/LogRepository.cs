using BND.Services.Payments.eMandates.Domain.Interfaces;
using System;

namespace BND.Services.Payments.eMandates.Data.Repositories
{
    /// <summary>
    /// Class LogRepository.
    /// </summary>
    public class LogRepository : ILogRepository
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


        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="LogRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public LogRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion


        #region [Methods]
        /// <summary>
        /// Inserts the specified log.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <returns>p_TransactionStatusHistory.</returns>
        public int Insert(Models.Log log)
        {
            ValidateDisposed();

            IRepository<Models.Log> repository = _unitOfWork.GetRepository<Models.Log>();
            repository.Insert(log);
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

        #endregion
    }
}
