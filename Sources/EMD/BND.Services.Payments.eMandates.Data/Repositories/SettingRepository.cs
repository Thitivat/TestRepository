using BND.Services.Payments.eMandates.Domain.Interfaces;
using System;

namespace BND.Services.Payments.eMandates.Data.Repositories
{
    /// <summary>
    /// Class SettingRepository.
    /// </summary>
    public class SettingRepository : ISettingRepository
    {
        /// <summary>
        /// The unit of work.
        /// </summary>
        private IUnitOfWork _unitOfWork;
        /// <summary>
        /// The _disposed flag.
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public SettingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the value by key.
        /// </summary>
        /// <param name="key">The key for retrieve data.</param>
        /// <returns>System.String.</returns>
        public string GetValueByKey(string key)
        {
            ValidateDisposed();

            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }

            IRepository<Models.Setting> repository = _unitOfWork.GetRepository<Models.Setting>();
            Models.Setting setting = repository.GetById(key);
            if (setting == null)
            {
                throw new ArgumentException("Key not found.", "key");
            }

            return repository.GetById(key).Value;
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
