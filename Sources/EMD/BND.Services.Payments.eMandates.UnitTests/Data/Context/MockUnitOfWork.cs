using BND.Services.Payments.eMandates.Domain.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.UnitTests.Data.Context
{
    public class MockUnitOfWork : EfUnitOfWorkBase
    {
        public MockUnitOfWork(MockDbContext dbContext, bool checkDbExists = true)
            : base(dbContext, System.Data.Entity.SqlServer.SqlProviderServices.Instance, checkDbExists)
        { }
    }
}
