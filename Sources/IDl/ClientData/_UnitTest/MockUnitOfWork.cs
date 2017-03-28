
using BND.Services.Payments.iDeal.ClientData.Dal.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.iDeal.ClientData.Tests
{
    public class MockUnitOfWork : EfUnitOfWorkBase
    {
        public MockUnitOfWork(MockDbContext dbContext, bool checkDbExists = true)
            : base(dbContext, System.Data.Entity.SqlServer.SqlProviderServices.Instance, checkDbExists)
        { }
    }
}
