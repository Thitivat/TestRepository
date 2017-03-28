using BND.Services.Payments.iDeal.Dal.Ef;
using NUnit.Framework;
using System;
using System.Data.Entity.Validation;
using System.Data.SqlClient;

namespace BND.Services.Payments.iDeal.Dal.Tests
{
    [TestFixture]
    public class EfUnitOfWorkBaseTest
    {
        private MockDbContext _dbContext;

        [SetUp]
        public void SetUpTest()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }

            _dbContext = new MockDbContext();
        }

        [Test]
        public void Ctor_Should_NotThrow_When_HasBeenCreated()
        {
            Assert.DoesNotThrow(() =>
            {
                var actual = new MockUnitOfWork(_dbContext, false);
            });
        }

        [Test]
        public void Ctor_Should_ThrowArgumentNullException_When_DbContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var actual = new MockUnitOfWork(null);
            });
        }

        [Test]
        public void Ctor_Should_ThrowArgumentException_When_DatabaseDoesNotExist()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var actual = new MockUnitOfWork(
                    new MockDbContext(new SqlConnection("Data Source=.;Initial Catalog=FakeDb;Integrated Security=True;Pooling=False")));
            });
        }

        [Test]
        public void GetRepository_Should_ReturnRepositoryOfPoco1_When_HasBeenCalledByUsingPoco1()
        {
            var actual = new MockUnitOfWork(_dbContext, false);

            // First time call will create new instance.
            Assert.IsInstanceOf(typeof(EfRepository<Poco1>), actual.GetRepository<Poco1>());
            // Second time call will re-use same instance.
            Assert.IsInstanceOf(typeof(EfRepository<Poco1>), actual.GetRepository<Poco1>());
        }

        [Test]
        public void GetRepository_Should_ThrowObjectDisposedException_When_InstanceHasBeenDisposed()
        {
            var actual = new MockUnitOfWork(_dbContext, false);
            actual.Dispose();

            Assert.Throws<ObjectDisposedException>(() =>
            {
                var actualRepo = actual.GetRepository<Poco1>();
            });
        }

        [Test]
        public void CommitChanges_Should_Has1AffectedRow_When_Change1RowInDb()
        {
            var actual = new MockUnitOfWork(_dbContext, false);
            actual.GetRepository<Poco1>().Insert(new Poco1 { Id = 99999, Name = "Test" });

            Assert.AreEqual(1, actual.CommitChanges());
        }

        [Test]
        public void CommitChanges_Should_StillHas1AffectedRow_When_PreviousCallHasAnError()
        {
            var actual = new MockUnitOfWork(_dbContext, false);
            actual.GetRepository<Poco2>().Insert(new Poco2());
           // actual.CommitChanges();

            Assert.Throws<DbEntityValidationException>(() => actual.CommitChanges());

            actual.GetRepository<Poco1>().Insert(new Poco1 { Id = 99999, Name = "Test" });

            Assert.AreEqual(1, actual.CommitChanges());
        }

        [Test]
        public void CommitChanges_Should_ThrowObjectDisposedException_When_InstanceHasBeenDisposed()
        {
            var actual = new MockUnitOfWork(_dbContext, false);
            actual.Dispose();

            Assert.Throws<ObjectDisposedException>(() =>
            {
                actual.CommitChanges();
            });
        }

        [Test]
        public void Dispose_Shoule_ClearContainerBeforeSetNull_When_HasAnyItem()
        {
            var actual = new MockUnitOfWork(_dbContext, false);
            var repo = actual.GetRepository<Poco2>();

            Assert.DoesNotThrow(() => actual.Dispose());
            Assert.DoesNotThrow(() => actual.Dispose());
        }
    }
}
