
namespace BND.Services.Payments.iDeal.JobQueue.Bll.Interfaces
{
    /// <summary>
    /// Interface IJobQueueManager
    /// </summary>
    public interface IJobQueueManager
    {
        /// <summary>
        /// Creates the job queue.
        /// </summary>
        /// <param name="jobQueue">The job queue.</param>
        /// <returns>total number of affected row(s)</returns>
        int CreateJobQueue(string transactionId, string entranceCode, int interval);
    }
}
