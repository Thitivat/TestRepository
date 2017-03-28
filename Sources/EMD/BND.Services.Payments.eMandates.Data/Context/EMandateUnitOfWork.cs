using BND.Services.Payments.eMandates.Domain.Ef;
using System.Data.SqlClient;

namespace BND.Services.Payments.eMandates.Data.Context
{
    public class EMandateUnitOfWork : EfUnitOfWorkBase
    {

        /// Initializes a new instance of the <see cref="EMandateUnitOfWork"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="checkDbExists">if set to <c>true</c> [check database exists].</param>
        public EMandateUnitOfWork(string connectionString, bool checkDbExists = true)
            : base(new EMandateDbContext(new SqlConnection(connectionString)),
                   System.Data.Entity.SqlServer.SqlProviderServices.Instance,
                   checkDbExists)
        { }
    }
}
