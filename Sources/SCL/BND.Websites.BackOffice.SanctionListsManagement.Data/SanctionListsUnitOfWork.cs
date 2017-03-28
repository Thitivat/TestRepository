using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using BND.Websites.BackOffice.SanctionListsManagement.Domain.Ef;

namespace BND.Websites.BackOffice.SanctionListsManagement.Domain
{
    /// <summary>
    /// Class SanctionListsUnitOfWork is a unit of work class that extended from <see cref="EfUnitOfWorkBase"/>
    /// abstract class for manupulating all data of sanction lists database following <a href="http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application">Repository and Unit of Work pattern</a>.
    /// </summary>
    /// <example>
    /// The following example demonstrates how to use the
    /// <see cref="SanctionListsUnitOfWork"/> class
    /// to manipulate data in sanction lists database.
    /// 
    /// <code>
    /// using System.Data.Entity;
    /// using Bndb.Kyc.SanctionLists.SanctionListsDal;
    /// 
    /// class Test
    /// {
    ///     static void Main(string[] args)
    ///     {
    ///         // Creates instance of SanctionListsUnitOfWork with connection string.
    ///         var _sanctionListUnitOfWork = new SanctionListsUnitOfWork(connectionString);
    /// 
    ///         // Creates repository from SanctionListsUnitOfWork.
    ///         var _entitiesRepo = _sanctionListUnitOfWork.GetRepository&lt;p_Entity&gt;();
    /// 
    ///         // Gets all entity data from sanction lists database.
    ///         IEnumerable&lt;p_Entity&gt; entities = _entitiesRepo.Get();
    ///     }
    /// }
    /// </code>
    /// </example>
    public class SanctionListsUnitOfWork : EfUnitOfWorkBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SanctionListsUnitOfWork"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="checkDbExists">if set to <c>true</c> check database exists, it will throw exception if not exists.</param>
        public SanctionListsUnitOfWork(string connectionString, bool checkDbExists = true)
            : base(new SanctionListsDbContext(new SqlConnection(connectionString)),
                   SqlProviderServices.Instance,
                   checkDbExists)
        { }
    }
}