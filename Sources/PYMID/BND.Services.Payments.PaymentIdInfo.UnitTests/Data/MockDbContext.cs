using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;

namespace BND.Services.Payments.PaymentIdInfo.Data.Test
{
    public partial class MockDbContext : DbContext
    {
        public virtual DbSet<Poco1> Poco1s { get; set; }
        public virtual DbSet<Poco2> Poco2s { get; set; }

        public MockDbContext()
            : base(Effort.DbConnectionFactory.CreateTransient(), true)
        {

        }

        public MockDbContext(DbConnection connection)
            : base(connection, true)
        { }
    }

    public class Poco1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Poco2> Poco2s { get; set; }

        public Poco1()
        {
            Poco2s = new HashSet<Poco2>();
        }
    }

    public class Poco2
    {
        [Key]
        public string Email { get; set; }

        public int Id { get; set; }
        [ForeignKey("Id")]
        public virtual Poco1 Poco1 { get; set; }
    }
}
