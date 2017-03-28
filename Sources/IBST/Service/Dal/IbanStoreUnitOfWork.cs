
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using BND.Services.IbanStore.Repository.Ef;

namespace BND.Services.IbanStore.Service.Dal
{
    /// <summary>
    /// Class IbanStoreUnitOfWork is a unit of work class that extended from <see cref="Bndb.Common.Repositories.Ef.EfUnitOfWorkBase"/>
    /// abstract class for manipulating all data of sanction lists database following <a href="http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application">Repository and Unit of Work pattern</a>.
    /// </summary>
    /// <example>
    /// The following example demonstrates how to use the
    /// <see cref="Bndb.IBanStore.Dal.IbanStoreUnitOfWork"/> class
    /// to manipulate data in sanction lists database.
    /// <code>
    /// using System.Data.Entity;
    /// using Bndb.IBanStore.Dal;
    /// 
    /// class Test
    /// {
    ///     static void Main(string[] args)
    ///     {
    ///         // Creates instance of IbanStoreUnitOfWork with connection string.
    ///         var _ibanStoreUnitOfWork = new IbanStoreUnitOfWork(connectionString);
    /// 
    ///         // Creates repository from IbanStoreUnitOfWork.
    ///         var _entitiesRepo = _ibanStoreUnitOfWork.GetRepository&lt;p_Entity&gt;();
    /// 
    ///         // Gets all entity data from sanction lists database.
    ///         IEnumerable&lt;p_Entity&gt; entities = _entitiesRepo.Get();
    ///     }
    /// }
    /// </code>
    /// </example>
    public class IbanStoreUnitOfWork : EfUnitOfWorkBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IbanStoreUnitOfWork"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="checkDbExists">if set to <c>true</c> [check database exists].</param>
        public IbanStoreUnitOfWork(string connectionString, bool checkDbExists = true)
            : base(new IbanStoreDbContext(new SqlConnection(connectionString)),
                SqlProviderServices.Instance,
                checkDbExists)
        {
        }
    }
}
