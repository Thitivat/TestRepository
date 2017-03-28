using BND.Services.IbanStore.Repository.Ef;
using BND.Services.IbanStore.Repository.Models;
using EntityFramework.Utilities;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace BND.Services.IbanStore.RepositoryTest
{
    [TestFixture]
    public class EfRepositoryTest
    {
        private MockDbContext _dbContext;
        private List<Poco1> _poco1sMockData;
        private List<Poco2> _poco2sMockData;

        [SetUp]
        public void Test_Initialize()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
            _dbContext = null;
            _dbContext = new MockDbContext();

            // Mocks data.
            _poco1sMockData = Helpers.MOCK_POCO1_DATA;
            _poco2sMockData = Helpers.MOCK_POCO2_DATA;

            _dbContext.Poco1s.AddRange(_poco1sMockData);
            _dbContext.Poco2s.AddRange(_poco2sMockData);
            _dbContext.SaveChanges();
        }

        //[Test]
        //public void Test_Constructor1_Success()
        //{
        //    var actual = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(new EfRepository<Poco1>(_dbContext));

        //    Assert.IsNotNull(actual.GetField("_context"));
        //    Assert.IsNotNull(actual.GetField("_dbSet"));
        //}

        #region [Get]
        [Test]
        public void Test_Get_Success_GetAll()
        {
            var actual = new EfRepository<Poco1>(_dbContext);

            Assert.AreEqual(_poco1sMockData.Count, actual.Get().Count());
        }

        [Test]
        public void Test_Get_Success_WithFilter()
        {
            int expectedId = 1;
            var actual = new EfRepository<Poco1>(_dbContext);
            var actualData = actual.Get(p1 => p1.Id == expectedId);

            Assert.IsNotNull(actualData);
            Assert.AreEqual(1, actualData.Count());
            Assert.AreEqual(_poco1sMockData[0].Id, actualData.First().Id);
            Assert.AreEqual(_poco1sMockData[0].Name, actualData.First().Name);
        }

        [Test]
        public void Test_Get_Success_WithPage()
        {
            var expectedPage = new Page<Poco1>
            {
                Limit = 2,
                Offset = 2,
                OrderBy = p1 => p1.OrderBy(p => p.Id)
            };
            var actual = new EfRepository<Poco1>(_dbContext);
            var actualData = actual.Get(page: expectedPage).ToList();

            Assert.IsNotNull(actualData);
            Assert.AreEqual(expectedPage.Limit, actualData.Count);
            int index = 0;
            for (int i = expectedPage.Offset.Value; i < expectedPage.Offset + expectedPage.Limit; i++)
            {
                Assert.AreEqual(_poco1sMockData[i].Id, actualData[index].Id);
                Assert.AreEqual(_poco1sMockData[i].Name, actualData[index].Name);
                index++;
            }
        }

        [Test]
        public void Test_Get_Success_WithOrder()
        {
            var expected = _poco1sMockData.OrderByDescending(p1 => p1.Id);
            var actual = new EfRepository<Poco1>(_dbContext);
            var actualData = actual.Get(orderBy: p1 => p1.OrderByDescending(p => p.Id)).ToList();

            Assert.IsNotNull(actualData);
            Assert.AreEqual(expected.Count(), actualData.Count);
            int index = 0;
            foreach (var poco1 in expected)
            {
                Assert.AreEqual(poco1.Id, actualData[index].Id);
                Assert.AreEqual(poco1.Name, actualData[index].Name);
                index++;
            }
        }

        [Test]
        public void Test_Get_Success_IncludeChildClass()
        {
            var actual = new EfRepository<Poco1>(_dbContext);
            var actualData = actual.Get(includeProperties: "Poco2s");

            Assert.IsNotNull(actualData);
            Assert.AreEqual(_poco1sMockData.Count, actualData.Count());
            foreach (var poco1 in actualData)
            {
                Assert.IsNotNull(poco1.Poco2s);
            }
        }

        [Test]
        public void Test_Get_Exception_NullOrderInPage()
        {
            var actual = new EfRepository<Poco1>(_dbContext);

            Assert.Throws<ArgumentNullException>(() => { actual.Get(page: new Page<Poco1>()); });
        }

        [Test]
        public void Test_Get_Exception_OffsetAndLimitNull()
        {
            var actual = new EfRepository<Poco1>(_dbContext);
            
            Assert.Throws<InvalidOperationException>(() =>
            { actual.Get(page: new Page<Poco1> { OrderBy = p1 => p1.OrderBy(p => p.Id) }); });
        }

        #endregion

        #region [GetById]

        [Test]
        public void Test_GetById_Success()
        {
            int expectedId = 1;
            var expected = _poco1sMockData.First(p1 => p1.Id == expectedId);
            var actual = new EfRepository<Poco1>(_dbContext);
            var actualData = actual.GetById(expectedId);

            Assert.IsNotNull(actualData);
            Assert.AreEqual(expected.Id, actualData.Id);
            Assert.AreEqual(expected.Name, actualData.Name);
        }

        #endregion

        #region [Insert]
        [Test]
        public void Test_Insert_Success_ByEntity()
        {
            var expected = new Poco1 { Id = 99, Name = "Test" };
            var actual = new EfRepository<Poco1>(_dbContext);
            actual.Insert(expected);
            actual.Insert(expected);
            _dbContext.SaveChanges();

            Assert.AreEqual(_poco1sMockData.Count + 1, actual.Get().Count());
            Assert.AreEqual(1, actual.Get(p1 => p1.Id == expected.Id).Count());
            Assert.AreEqual(expected.Id, actual.Get(p1 => p1.Id == expected.Id).First().Id);
            Assert.AreEqual(expected.Name, actual.Get(p1 => p1.Id == expected.Id).First().Name);
        }

        //[Test]
        //public void Test_Insert_Success_ByMultipleEntities()
        //{
        //    var expected = new List<Poco1>
        //    {
        //        new Poco1 { Id = 90, Name = "Test90" },
        //        new Poco1 { Id = 91, Name = "Test91" },
        //        new Poco1 { Id = 92, Name = "Test92" }
        //    };

        //    using (ShimsContext.Create())
        //    {
        //        var mock = new MockIEFBatchOperationBase<Poco1>();
        //        EntityFramework.Utilities.Fakes.ShimEFBatchOperation.ForOf2M0IDbSetOfM1<DbContext, Poco1>((a, b) => mock);

        //        var actual = new EfRepository<Poco1>(_dbContext);
        //        actual.Insert(expected);
        //        _dbContext.SaveChanges();

        //        Assert.AreEqual(expected.Count, mock.TotalOperatedRow);
        //    }

        //}


        #endregion

        #region [Update]

        [Test]
        public void Test_Update_Success()
        {
            int expectedId = 1;
            var expected = "TestUpdate";
            var actual = new EfRepository<Poco1>(_dbContext);
            var actualData = actual.GetById(expectedId);
            actualData.Name = expected;
            actual.Update(actualData);
            _dbContext.Entry(actualData).State = System.Data.Entity.EntityState.Detached;
            actual.Update(actualData);
            _dbContext.SaveChanges();

            Assert.AreEqual(expected, actual.GetById(expectedId).Name);
        }

        #endregion

        #region [Delete]
        [Test]
        public void Test_Delete_Success_ById()
        {
            Test_Initialize();

            int expectedId = 5;
            var actual = new EfRepository<Poco1>(_dbContext);
            actual.Delete(expectedId);
            _dbContext.SaveChanges();

            Assert.AreEqual(_poco1sMockData.Count - 1, actual.Get().Count());
            Assert.IsNull(actual.GetById(expectedId));
        }

        [Test]
        public void Test_Delete_Success_ByIdWhatCouldNotBeFound()
        {
            Test_Initialize();

            var actual = new EfRepository<Poco1>(_dbContext);
            actual.Delete(9999);
            _dbContext.SaveChanges();

            Assert.AreEqual(_poco1sMockData.Count, actual.Get().Count());
        }

        [Test]
        public void Test_Delete_Success_ByEntity()
        {
            Test_Initialize();

            int expectedId = 5;
            var actual = new EfRepository<Poco1>(_dbContext);
            var actualData = actual.GetById(expectedId);
            actual.Delete(actualData);
            _dbContext.Entry(actualData).State = System.Data.Entity.EntityState.Deleted;
            actual.Delete(actualData);
            _dbContext.SaveChanges();

            Assert.AreEqual(_poco1sMockData.Count - 1, actual.Get().Count());
            Assert.IsNull(actual.GetById(expectedId));
        }

        [Test]
        public void Test_Delete_Success_ByMultipleEntities()
        {
            Test_Initialize();

            var expectedPage = new Page<Poco1>
            {
                Limit = 3,
                Offset = 4,
                OrderBy = p1 => p1.OrderBy(p => p.Id)
            };
            var actual = new EfRepository<Poco1>(_dbContext);
            var actualData = actual.Get(page: expectedPage);
            actual.Delete(actualData);
            _dbContext.SaveChanges();

            Assert.AreEqual(_poco1sMockData.Count - expectedPage.Limit, actual.Get().Count());
        }

        #endregion

        #region [GetCount]
        [Test]
        public void Test_GetCount_Success_GetAll()
        {
            var actual = new EfRepository<Poco1>(_dbContext);

            Assert.AreEqual(_poco1sMockData.Count, actual.GetCount());
        }

        [Test]
        public void Test_GetCount_Success_WithFilter()
        {
            int expectedId = 1;
            var actual = new EfRepository<Poco1>(_dbContext);
            var actualData = actual.GetCount(p1 => p1.Id == expectedId);

            Assert.IsNotNull(actualData);
            Assert.AreEqual(1, actualData);
        }

        #endregion
    }
}
