using BND.Services.Security.OTP.Repositories.Ef;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;

namespace BND.Services.Security.OTP.Dal
{
    public class OtpUnitOfWork : EfUnitOfWorkBase
    {
        /// <summary>
        /// Initializes a new instance of the <see>
        ///         <cref>SanctionListsUnitOfWork</cref>
        ///     </see>
        ///     class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="checkDbExists">if set to <c>true</c> check database exists, it will throw exception if not exists.</param>
        /// <param name="lazyLoad">if set to <c>true</c> EF will not load related entities, you should manual load that by eager loading.
        /// by default EF set this flag is <c>false</c>.</param>
        public OtpUnitOfWork(string connectionString, bool checkDbExists = true, bool lazyLoad = false)
            : base(new OtpContext(new SqlConnection(connectionString)), 
                   SqlProviderServices.Instance,
                   checkDbExists,
                   lazyLoad)
        { }
    }
}
