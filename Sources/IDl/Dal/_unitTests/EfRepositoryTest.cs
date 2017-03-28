using BND.Services.Payments.iDeal.Dal.Ef;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BND.Services.Payments.iDeal.Dal.Tests
{
    [TestFixture]
    public class EfRepositoryTest
    {
        private MockDbContext _dbContext;
        private IEnumerable<Poco1> _poco1sMockData;
        private IEnumerable<Poco2> _poco2sMockData;

        [SetUp]
        public void SetUpTest()
        {
            _poco1sMockData = new List<Poco1>
            {
                new Poco1 { Id = 1, Name = "Poco1-01" },
                new Poco1 { Id = 2, Name = "Poco1-02" },
                new Poco1 { Id = 3, Name = "Poco1-03" },
                new Poco1 { Id = 4, Name = "Poco1-04" },
                new Poco1 { Id = 5, Name = "Poco1-05" },
                new Poco1 { Id = 6, Name = "Poco1-06" },
                new Poco1 { Id = 7, Name = "Poco1-07" },
                new Poco1 { Id = 8, Name = "Poco1-08" }
            };
            _poco2sMockData = new List<Poco2>
            {
                new Poco2 { Email = "Poco2.01@Poco1.com", Id = 1 },
                new Poco2 { Email = "Poco2.02@Poco1.com", Id = 1 },
                new Poco2 { Email = "Poco2.03@Poco1.com", Id = 2 },
                new Poco2 { Email = "Poco2.04@Poco1.com", Id = 2 },
                new Poco2 { Email = "Poco2.05@Poco1.com", Id = 3 },
                new Poco2 { Email = "Poco2.06@Poco1.com", Id = 4 },
                new Poco2 { Email = "Poco2.07@Poco1.com", Id = 4 }
            };

            if (_dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }

            _dbContext = new MockDbContext();
            _dbContext.Poco1s.AddRange(_poco1sMockData);
            _dbContext.Poco2s.AddRange(_poco2sMockData);
            _dbContext.SaveChanges();
        }

        [Test]
        public void Ctor_Should_NotThrow_When_HasBeenCreated()
        {
            Assert.DoesNotThrow(() =>
            {
                var actual = new EfRepository<Poco1>(_dbContext);
            });
        }

        [Test]
        public void Get_Should_RetrieveAllRecords_When_HasBeenCalledWithoutParameters()
        {
            var actualRepo = new EfRepository<Poco1>(_dbContext);
            var actual = actualRepo.Get();

            Assert.IsNotNull(actual);
            Assert.AreEqual(_poco1sMockData.Count(), actual.Count());
            Assert.IsTrue(actual.SequenceEqual(_poco1sMockData, new Poco1Comparer()));
        }

        [Test]
        public void Get_Should_RetrieveProperRecords_When_HasBeenCalledWithFilter()
        {
            var expectedName = _poco1sMockData.First().Name;
            var actualRepo = new EfRepository<Poco1>(_dbContext);
            var actual = actualRepo.Get(p => p.Name == expectedName).FirstOrDefault();

            Assert.IsNotNull(actual);
            Assert.AreEqual(_poco1sMockData.First().Id, actual.Id);
            Assert.AreEqual(_poco1sMockData.First().Name, actual.Name);
        }

        [Test]
        public void Get_Should_RetrieveSomeRecordsFollowingPaging_When_HasBeenCalledWithPage()
        {
            var expectedPage = new Page<Poco1>
            {
                Limit = 2,
                Offset = 2,
                OrderBy = p1 => p1.OrderBy(p => p.Id)
            };
            var actualRepo = new EfRepository<Poco1>(_dbContext);
            var actual = actualRepo.Get(page: expectedPage).ToList();

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedPage.Limit, actual.Count);

            int index = 0;
            for (int i = expectedPage.Offset.Value; i < expectedPage.Offset + expectedPage.Limit; i++)
            {
                Assert.AreEqual(_poco1sMockData.ToList()[i].Id, actual[index].Id);
                Assert.AreEqual(_poco1sMockData.ToList()[i].Name, actual[index].Name);
                index++;
            }
        }

        [Test]
        public void Get_Should_RetrieveOrderedRecords_When_HasBeenCalledWithOrder()
        {
            var expected = _poco1sMockData.OrderByDescending(p1 => p1.Id);
            var actualRepo = new EfRepository<Poco1>(_dbContext);
            var actual = actualRepo.Get(orderBy: p1 => p1.OrderByDescending(p => p.Id)).ToList();

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count(), actual.Count);
            Assert.IsTrue(actual.SequenceEqual(expected, new Poco1Comparer()));
        }

        [Test]
        public void Get_Should_RetrieveRecordsIncludingRelatedRecordsFromAnotherTable_When_HasBeenCalledWithIncludeProperties()
        {
            var actualRepo = new EfRepository<Poco1>(_dbContext);
            var actual = actualRepo.Get(includeProperties: "Poco2s");

            Assert.IsNotNull(actual);
            Assert.AreEqual(_poco1sMockData.Count(), actual.Count());
            foreach (var poco1 in actual)
            {
                Assert.IsNotNull(poco1.Poco2s);
            }
        }

        [Test]
        public void Get_Should_ThrowArgumentNullException_When_HasBeenCalledWithPageButOrderOfPageIsNull()
        {
            var actualRepo = new EfRepository<Poco1>(_dbContext);

            Assert.Throws<ArgumentNullException>(() => actualRepo.Get(page: new Page<Poco1>()));
        }

        [Test]
        public void Get_Should_ThrowInvalidOperationException_When_HasBeenCalledWithPageButOffsetAndOrLimitIsNull()
        {
            var actualRepo = new EfRepository<Poco1>(_dbContext);

            Assert.Throws<InvalidOperationException>(() => actualRepo.Get(page: new Page<Poco1> { OrderBy = p1 => p1.OrderBy(p => p.Id) }));
        }

        [Test]
        public void GetById_Should_Retrieve1RecordWithCorrectId_When_HasBeenCalled()
        {
            int expectedId = 1;
            var expected = _poco1sMockData.First(p1 => p1.Id == expectedId);
            var actualRepo = new EfRepository<Poco1>(_dbContext);
            var actual = actualRepo.GetById(expectedId);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [Test]
        public void Insert_Should_BeAdded_When_PocoHasBeenInsertedToDb()
        {
            var expected = new Poco1 { Id = 99, Name = "Test" };
            var actualRepo = new EfRepository<Poco1>(_dbContext);
            actualRepo.Insert(expected);
            actualRepo.Insert(expected);

            Assert.AreEqual(1, _dbContext.SaveChanges());

            var actual = actualRepo.GetById(expected.Id);

            Assert.IsNotNull(actual);
            Assert.AreEqual(_poco1sMockData.Count() + 1, actualRepo.Get().Count());
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [Test]
        public void Update_Should_BeChanged_When_PocoHasBeenUpdatedInDb()
        {
            int expectedId = 1;
            var expected = "TestUpdate";
            var actualRepo = new EfRepository<Poco1>(_dbContext);
            var actual = actualRepo.GetById(expectedId);
            actual.Name = expected;
            actualRepo.Update(actual);
            _dbContext.Entry(actual).State = System.Data.Entity.EntityState.Detached;
            actualRepo.Update(actual);

            Assert.AreEqual(1, _dbContext.SaveChanges());
            Assert.AreEqual(expected, actualRepo.GetById(expectedId).Name);
        }

        [Test]
        public void GetCount_Should_RetrieveAllRowAmountOfATableRepresentedByPoco_When_HasBeenCalled()
        {
            var actualRepo = new EfRepository<Poco1>(_dbContext);

            Assert.AreEqual(_poco1sMockData.Count(), actualRepo.GetCount());
        }

        [Test]
        public void GetCount_Should_RetrieveSomeRowAmountOfATableRepresentedByPoco_When_HasBeenCalledWithFilter()
        {
            int expectedId = 1;
            var actualRepo = new EfRepository<Poco1>(_dbContext);

            Assert.AreEqual(_poco1sMockData.Count(p1 => p1.Id == expectedId), actualRepo.GetCount(p1 => p1.Id == expectedId));
        }

        [Test]
        public void Delete_Should_BeRemoved_When_PocoHasBeenDeletedInDbById()
        {
            int expectedId = 5;
            var actualRepo = new EfRepository<Poco1>(_dbContext);
            actualRepo.Delete(expectedId);
            actualRepo.Delete(expectedId);

            Assert.AreEqual(1, _dbContext.SaveChanges());
            Assert.IsNull(actualRepo.GetById(expectedId));
        }

        [Test]
        public void Delete_Should_HasNoAffectedRow_When_PocoHasBeenDeletedInDbById()
        {
            int expectedId = 99999;
            var actualRepo = new EfRepository<Poco1>(_dbContext);
            actualRepo.Delete(expectedId);

            Assert.AreEqual(0, _dbContext.SaveChanges());
        }

        [Test]
        public void Delete_Should_BeRemoved_When_PocoHasBeenDeletedInDbByPoco()
        {
            int expectedId = 6;
            var actualRepo = new EfRepository<Poco1>(_dbContext);
            var actual = actualRepo.GetById(expectedId);
            actualRepo.Delete(actual);

            Assert.AreEqual(1, _dbContext.SaveChanges());
            Assert.IsNull(actualRepo.GetById(expectedId));
        }

        [Test]
        public void Delete_Should_BeRemoved_When_ListOfPocoesHaveBeenDeletedInDb()
        {
            var actualRepo = new EfRepository<Poco2>(_dbContext);
            actualRepo.Delete(actualRepo.Get());

            Assert.AreEqual(_poco2sMockData.Count(), _dbContext.SaveChanges());
            Assert.AreEqual(0, actualRepo.GetCount());
        }
    }

    public class Poco1Comparer : IEqualityComparer<Poco1>
    {
        public bool Equals(Poco1 x, Poco1 y)
        {
            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(Poco1 obj)
        {
            return obj.GetHashCode();
        }
    }
}
