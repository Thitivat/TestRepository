using Effort;
using System.Data.Common;
using System.Data.Entity;

namespace BND.Services.IbanStore.ServiceTest
{
    internal class MockDbContext : DbContext
    {
        private void InitMock()
        {

        }

        public MockDbContext()
            : base(DbConnectionFactory.CreateTransient(), false)
        { InitMock(); }

        public MockDbContext(DbConnection connection)
            : base(connection, true)
        { InitMock(); }
    }
}
