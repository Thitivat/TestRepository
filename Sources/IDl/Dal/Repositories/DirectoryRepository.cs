using BND.Services.Payments.iDeal.Dal.Pocos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;

namespace BND.Services.Payments.iDeal.Dal
{
    /// <summary>
    /// Class DirectoryRepository performing directory table in database.
    /// </summary>
    public class DirectoryRepository : IDirectoryRepository
    {
        #region [Fieldds]

        /// <summary>
        /// The unit of work
        /// </summary>
        private IUnitOfWork _unitOfWork;
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
        /// Gets the directory.
        /// </summary>
        /// <returns>p_Directory.</returns>
        public p_Directory Get()
        {
            IRepository<p_Directory> repository = _unitOfWork.GetRepository<p_Directory>();
            return repository.Get().FirstOrDefault();
        }

        /// <summary>
        /// Updates the directory.
        /// </summary>
        /// <param name="newDirectory">The new directory.</param>
        /// <returns>Affected row count.</returns>
        public int UpdateDirectory(p_Directory newDirectory)
        {
            int affectedRowCount = 0;
            IRepository<p_Directory> directoryRepository = _unitOfWork.GetRepository<p_Directory>();
            IRepository<p_Issuer> issuerRepository = _unitOfWork.GetRepository<p_Issuer>();
            using (TransactionScope transactionScope = new TransactionScope())
            {
                // clear old directory and issuers
                var oldDirectory = directoryRepository.GetQueryable().FirstOrDefault();
                if (oldDirectory != null)
                {
                    issuerRepository.Delete(oldDirectory.Issuers);
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
    }
}
