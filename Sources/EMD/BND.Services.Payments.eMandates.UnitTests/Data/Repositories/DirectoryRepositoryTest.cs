using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BND.Services.Payments.eMandates.Data.Repositories;
using BND.Services.Payments.eMandates.Data.Context;
using BND.Services.Payments.eMandates.UnitTests.Data.Context;
using BND.Services.Payments.eMandates.Domain.Interfaces;
using BND.Services.Payments.eMandates.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace BND.Services.Payments.eMandates.UnitTests.Data.Repositories
{
    [TestFixture]
    public class DirectoryRepositoryTest
    {
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            //_unitOfWork = new MockUnitOfWork(new MockDbContext(), false);
            DbConnection conn = new SqlConnection("data source=.;initial catalog=test_bnd_services_payments_emandate;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            _unitOfWork = new MockUnitOfWork(new MockDbContext(conn), false);
            
        }

        [Test]
        public void Get_Should_ReturnEmpty_InFirstTime()
        {
            IRepository<Directory> directoryRepo = _unitOfWork.GetRepository<Directory>();

            //ITransactionRepository repo = _unitOfWork.GetRepository<Transaction>();

            var directory = directoryRepo.Get();

            

            Assert.IsNotNull(directory);
            Assert.IsTrue(directory.Count() == 0);
        }
    }
}
