using BND.Services.Security.OTP.Repositories.Ef;
using System.Data.Entity.SqlServer;

namespace BND.Services.Security.OTP.RepositoriesTest
{
    internal class MockUnitOfWork : EfUnitOfWorkBase
    {
        public MockUnitOfWork(MockDbContext dbContext, bool checkDbExists = true)
            : base(dbContext, SqlProviderServices.Instance, checkDbExists)  { }
    }
}
