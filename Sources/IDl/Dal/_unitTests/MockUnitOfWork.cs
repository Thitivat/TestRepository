using BND.Services.Payments.iDeal.Dal.Ef;

namespace BND.Services.Payments.iDeal.Dal.Tests
{
    public class MockUnitOfWork : EfUnitOfWorkBase
    {
        public MockUnitOfWork(MockDbContext dbContext, bool checkDbExists = true)
            : base(dbContext, System.Data.Entity.SqlServer.SqlProviderServices.Instance, checkDbExists)
        { }
    }
}
