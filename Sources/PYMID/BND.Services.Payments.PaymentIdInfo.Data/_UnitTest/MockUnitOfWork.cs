using BND.Services.Payments.PaymentIdInfo.Dal.Ef;

namespace BND.Services.Payments.PaymentIdInfo.Dal.Test
{
    public class MockUnitOfWork : EfUnitOfWorkBase
    {
        public MockUnitOfWork(MockDbContext dbContext, bool checkDbExists = true)
            : base(dbContext, System.Data.Entity.SqlServer.SqlProviderServices.Instance, checkDbExists)
        { }
    }
}
