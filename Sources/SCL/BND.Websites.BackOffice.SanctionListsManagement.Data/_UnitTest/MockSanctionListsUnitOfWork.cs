using Bndb.Kyc.SanctionLists.SanctionListsDal;
using Effort;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bndb.Kyc.SanctionLists.SanctionListsDalTest
{
    internal class MockSanctionListsUnitOfWork : SanctionListsUnitOfWork
    {
        public MockSanctionListsUnitOfWork()
            : base("Data Source=.;Integrated Security=True;Pooling=False", false)
        {
            base._context = new DbContext(DbConnectionFactory.CreateTransient(), true);
        }
    }
}
