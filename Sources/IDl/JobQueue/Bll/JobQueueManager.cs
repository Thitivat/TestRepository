using BND.Services.Payments.iDeal.JobQueue.Bll.Interfaces;
using BND.Services.Payments.iDeal.JobQueue.Dal.Interfaces;
using BND.Services.Payments.iDeal.JobQueue.Dal.Pocos;
using System;

namespace BND.Services.Payments.iDeal.JobQueue.Bll
{
    public class JobQueueManager : IJobQueueManager
    {
        #region [Fields]

        private IJobListRepository _jobListRepository;

        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="JobQueueManager"/> class.
        /// </summary>
        /// <param name="jobListRepository">The job list repository.</param>
        public JobQueueManager(IJobListRepository jobListRepository)
        {
            _jobListRepository = jobListRepository;
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Creates the job queue.
        /// </summary>
        /// <param name="transactionId">The transaction id.</param>
        /// <param name="entranceCode">The entrance code.</param>
        /// <param name="interval">The interval in minutes.</param>
        /// <returns>total number of affected row(s)</returns>
        public int CreateJobQueue(string transactionId, string entranceCode, int interval = 15)
        {
            if(String.IsNullOrEmpty(transactionId))
            {
                throw new ArgumentNullException("transactionId");
            }

            if(String.IsNullOrEmpty(entranceCode))
            {
                throw new ArgumentNullException("entranceCode");
            }

            var jobList = new p_JobList();

            jobList.JobClassName = "GetIdealStatus";
            jobList.Label = String.Format("GetIdealStatus: Entrancecode {0}, TrxID {1}", entranceCode, transactionId);
            jobList.JobParameters = String.Format("Entrancecode:{0},TrxID:{1}", entranceCode, transactionId);
            jobList.StartedBy = "iDealService";
            jobList.Inserted = DateTime.Now.AddMinutes(interval);
            jobList.JobPriorityID = 99;
            jobList.Parallel = false;
            jobList.SyncChainID = 3;
            jobList.JobStatusID = 0;

            return _jobListRepository.Insert(jobList);
        }
        #endregion
    }
}
