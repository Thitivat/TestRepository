using BND.Services.Payments.eMandates.Domain.Interfaces;
using BND.Services.Payments.eMandates.Models;
using System;
using System.Linq;
using System.Transactions;

namespace BND.Services.Payments.eMandates.Data.Repositories
{
    /// <summary>
    /// Class DirectoryRepository.
    /// </summary>
    public class DirectoryRepository : IDirectoryRepository
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
        /// Initializes a new instance of the <see cref="DirectoryRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public DirectoryRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region [Methods]

        /// <summary>
        /// Gets the list of directory.
        /// </summary>
        /// <returns>p_Directory.</returns>
        public Directory Get()
        {
            ValidateDisposed();

            IRepository<Directory> repository = _unitOfWork.GetRepository<Directory>();
            return repository.Get().FirstOrDefault();
        }

        /// <summary>
        /// Updates the directory.
        /// </summary>
        /// <param name="newDirectory">The new directory.</param>
        /// <returns>System.Int32.</returns>
        public int UpdateDirectory(Directory newDirectory)
        {
            ValidateDisposed();

            int affectedRowCount = 0;
            IRepository<Directory> directoryRepository = _unitOfWork.GetRepository<Directory>();
            IRepository<DebtorBank> debtorRepository = _unitOfWork.GetRepository<DebtorBank>();
            using (TransactionScope transactionScope = new TransactionScope())
            {
                // clear old directory and debtor
                var oldDirectory = directoryRepository.GetQueryable().FirstOrDefault();
                if (oldDirectory != null)
                {
                    debtorRepository.Delete(oldDirectory.DebtorBanks);
                    directoryRepository.Delete(oldDirectory);
                    _unitOfWork.CommitChanges();
                }

                // insert new directory
                directoryRepository.Insert(newDirectory);
                affectedRowCount = _unitOfWork.CommitChanges();
                transactionScope.Complete();
            }
            return affectedRowCount;
        }

        #endregion
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
