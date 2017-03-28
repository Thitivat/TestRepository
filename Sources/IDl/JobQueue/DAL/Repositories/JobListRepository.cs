using BND.Services.Payments.iDeal.JobQueue.Dal.Interfaces;
using BND.Services.Payments.iDeal.JobQueue.Dal.Pocos;

namespace BND.Services.Payments.iDeal.JobQueue.Dal.Repositories
{
    public class JobListRepository : IJobListRepository
    {
        #region [Fields]
        /// <summary>
        /// The unit of work
        /// </summary>
        private IUnitOfWork _unitOfWork;
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="JobListRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public JobListRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Inserts the specified job list.
        /// </summary>
        /// <param name="jobList">The job list.</param>
        /// <returns>total number of affected row(s)</returns>
        public int Insert(p_JobList jobList)
        {
            IRepository<p_JobList> repository = _unitOfWork.GetRepository<p_JobList>();
            repository.Insert(jobList);
            return _unitOfWork.CommitChanges();
        }
        #endregion
    }
}
