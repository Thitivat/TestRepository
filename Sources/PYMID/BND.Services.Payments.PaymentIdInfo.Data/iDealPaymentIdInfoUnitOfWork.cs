using BND.Services.Payments.PaymentIdInfo.Data.Ef;
using System.Data.SqlClient;

namespace BND.Services.Payments.PaymentIdInfo.Data
{
    /// <summary>
    /// Class iDealUnitOfWork is a unit of work class that extended from <see cref="BND.Services.Payments.iDeal.Dal.Ef.EfUnitOfWorkBase"/>
    /// abstract class for manipulating all data of sanction lists database following
    /// <a href="http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application">Repository and Unit of Work pattern</a>.
    /// </summary>
    /// <example>
    /// The following example demonstrates how to use the
    /// <see cref="BND.Services.Payments.iDeal.Dal.iDealUnitOfWork"/> class
    /// to manipulate data in sanction lists database.
    /// <code>
    /// using System.Data.Entity;
    /// using BND.Services.Payments.iDeal.Dal;
    /// 
    /// class Test
    /// {
    ///     static void Main(string[] args)
    ///     {
    ///         // Creates instance of iDealUnitOfWork with connection string.
    ///         var _iDealUnitOfWork = new iDealUnitOfWork(connectionString);
    /// 
    ///         // Creates repository from iDealUnitOfWork.
    ///         var _entitiesRepo = iDealUnitOfWork.GetRepository&lt;p_Entity&gt;();
    /// 
    ///         // Gets all entity data from sanction lists database.
    ///         IEnumerable&lt;p_Entity&gt; entities = _entitiesRepo.Get();
    ///     }
    /// }
    /// </code>
    /// </example>
    public class iDealPaymentIdInfoUnitOfWork : EfUnitOfWorkBase
    {

        /// Initializes a new instance of the <see cref="iDealUnitOfWork"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="checkDbExists">if set to <c>true</c> [check database exists].</param>
        public iDealPaymentIdInfoUnitOfWork(string connectionString, bool checkDbExists = true)
            : base(new iDealPaymentIdInfoDbContext(new SqlConnection(connectionString)),
                   System.Data.Entity.SqlServer.SqlProviderServices.Instance,
                   checkDbExists)
        { }
    }
}
