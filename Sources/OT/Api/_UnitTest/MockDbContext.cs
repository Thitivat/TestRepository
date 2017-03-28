using Effort;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Security.OTP.ApiUnitTest
{
    internal class MockDbContext : DbContext
    {
        public virtual DbSet<Poco1> Poco1s { get; set; }

        public MockDbContext()
            : base(DbConnectionFactory.CreateTransient(), true)
        { }
    }

    public class Poco1
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
