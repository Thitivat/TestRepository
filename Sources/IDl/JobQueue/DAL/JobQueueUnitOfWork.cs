using BND.Services.Payments.iDeal.JobQueue.Dal.Ef;
using System.Data.SqlClient;

namespace BND.Services.Payments.iDeal.JobQueue.Dal
{
    /// <summary>
    /// Class JobQueueUnitOfWork is a unit of work class that extended from <see cref="BND.Services.Payments.iDeal.JobQueue.Dal.Ef.EfUnitOfWorkBase"/>
    /// abstract class for manipulating all data of sanction lists database following
    /// <a href="http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application">Repository and Unit of Work pattern</a>.
    /// </summary>
    /// <example>
    /// The following example demonstrates how to use the
    /// <see cref="BND.Services.Payments.iDeal.JobQueue.Dal.JobQueueUnitOfWork"/> class
    /// to manipulate data in sanction lists database.
    /// <code>
    /// using System.Data.Entity;
    /// using BND.Services.Payments.iDeal.JobQueue.Dal;
    /// 
    /// class Test
    /// {
    ///     static void Main(string[] args)
    ///     {
    ///         // Creates instance of JobQueueUnitOfWork with connection string.
    ///         var _jobQueueUnitOfWork = new JobQueueUnitOfWork(connectionString);
    /// 
    ///         // Creates repository from JobQueueUnitOfWork.
    ///         var _entitiesRepo = JobQueueUnitOfWork.GetRepository&lt;p_Entity&gt;();
    /// 
    ///         // Gets all entity data from sanction lists database.
    ///         IEnumerable&lt;p_Entity&gt; entities = _entitiesRepo.Get();
    ///     }
    /// }
    /// </code>
    /// </example>
    public class JobQueueUnitOfWork : EfUnitOfWorkBase
    {

        /// Initializes a new instance of the <see cref="JobQueueUnitOfWork"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="checkDbExists">if set to <c>true</c> [check database exists].</param>
        public JobQueueUnitOfWork(string connectionString, bool checkDbExists = true)
            : base(new JobQueueDbContext(new SqlConnection(connectionString)),
                   System.Data.Entity.SqlServer.SqlProviderServices.Instance,
                   checkDbExists)
        { }
    }
}
