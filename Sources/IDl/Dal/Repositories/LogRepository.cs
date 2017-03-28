using BND.Services.Payments.iDeal.Dal.Pocos;

namespace BND.Services.Payments.iDeal.Dal
{
    /// <summary>
    /// Class LogRepository performing log table in database.
    /// </summary>
    public class LogRepository : ILogRepository
    {
        #region [Fields]
        /// <summary>
        /// The unit of work
        /// </summary>
        private IUnitOfWork _unitOfWork;
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
        public int Insert(p_Log log)
        {
            IRepository<p_Log> repository = _unitOfWork.GetRepository<p_Log>();
            repository.Insert(log);
            return _unitOfWork.CommitChanges();
        }
        #endregion
    }
}
