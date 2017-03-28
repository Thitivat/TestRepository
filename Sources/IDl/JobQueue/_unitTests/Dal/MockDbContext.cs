using BND.Services.Payments.iDeal.JobQueue.Dal.Pocos;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.iDeal.JobQueue.Tests.Dal
{
    public class MockDbContext : DbContext
    {
        public virtual DbSet<p_JobList> JobList { get; set; }

        public MockDbContext()
            : base(Effort.DbConnectionFactory.CreateTransient(), true)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<p_JobList>()
                .ToTable("JobList", schemaName: "job");
        }
    }
}
