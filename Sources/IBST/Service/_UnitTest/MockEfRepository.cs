using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.IbanStore.ServiceTest
{
    internal class MockEfRepository<T> : BND.Services.IbanStore.Repository.Ef.EfRepository<T> where T : class
    {
        public MockEfRepository()
            : base(new MockDbContext())
        {

        }
    }
}
