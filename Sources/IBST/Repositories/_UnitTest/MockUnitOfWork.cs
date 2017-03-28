using BND.Services.IbanStore.Repository.Ef;
using System.Data.Entity.SqlServer;

namespace BND.Services.IbanStore.RepositoryTest
{
    internal class MockUnitOfWork : EfUnitOfWorkBase
    {
        public MockUnitOfWork(MockDbContext dbContext, bool checkDbExists = true)
            : base(dbContext, SqlProviderServices.Instance, checkDbExists) { }


    }
}
