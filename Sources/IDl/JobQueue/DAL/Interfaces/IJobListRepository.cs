using BND.Services.Payments.iDeal.JobQueue.Dal.Pocos;

namespace BND.Services.Payments.iDeal.JobQueue.Dal.Interfaces
{
    public interface IJobListRepository
    {
        /// <summary>
        /// Inserts the specified job list.
        /// </summary>
        /// <param name="jobList">The job list.</param>
        /// <returns>total number of affected row(s)</returns>
        int Insert(p_JobList jobList);
    }
}
