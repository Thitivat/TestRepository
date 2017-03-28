using Effort;
using System.Data.Common;
using System.Data.Entity;

namespace BND.Services.Security.OTP.RepositoriesTest
{
    internal class MockDbContext : DbContext
    {
        public virtual DbSet<Poco1> Poco1s { get; set; }
        public virtual DbSet<Poco2> Poco2s { get; set; }

        public MockDbContext()
            : base(DbConnectionFactory.CreateTransient(), true)
        { }

        public MockDbContext(DbConnection connection)
            : base(connection, true)
        { }
    }
}
