using BND.Services.Security.OTP.Repositories.Ef;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
//using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BND.Services.Security.OTP.RepositoriesTest
{
    [TestFixture]
    public class EfUnitOfWorkBaseTest
    {
        private MockDbContext _dbContext;

        [SetUp]
        public void Test_Initialize()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
            _dbContext = null;
            _dbContext = new MockDbContext();
        }

        #region [Constructor]

        [Test]
        public void Test_Constructor_Success()
        {
            //var actual = new MSTest.PrivateObject(new MockUnitOfWork(_dbContext, false));

            //Assert.IsNotNull(actual.GetField("Context"));
        }

        [Test]
        public void Test_Constructor_Exception_NullDbContext()
        {
            Assert.Throws<ArgumentNullException>(() => new MockUnitOfWork(null));
        }

        [Test]
        public void Test_Constructor_Exception_WrongConnectionString()
        {
            //Assert.Throws<ArgumentException>(() => new MockUnitOfWork(new MockDbContext(
            //    new SqlConnection("Data Source=.;Initial Catalog=FakeDb;Integrated Security=True;Pooling=False"))));
        }

        #endregion

        #region [GetRepository]

        [Test]
        public void Test_GetRepository_Success()
        {
            var actual = new MockUnitOfWork(_dbContext, false);
            Assert.IsInstanceOf<EfRepository<Poco1>>(actual.GetRepository<Poco1>());
        }

        [Test]
        public void Test_GetRepository_Success_AlreadyHave_Repository()
        {
            var actual = new MockUnitOfWork(_dbContext, false);

            // Mock _repositoriesContainer field.
            //var repoPoco1 = new MSTest.PrivateObject(new EfRepository<Poco1>(_dbContext));
            //Dictionary<string, dynamic> repositories = new Dictionary<string, dynamic>();
            //repositories.Add(typeof(Poco1).Name, repoPoco1);

            //Assert.IsInstanceOf<EfRepository<Poco1>>(actual.GetRepository<Poco1>());
        }

        [Test]
        public void Test_GetRepository_Exception_ObjectDisposed()
        {
            var actual = new MockUnitOfWork(_dbContext, false);
            actual.Dispose();
            Assert.Throws<ObjectDisposedException>(() => actual.GetRepository<Poco1>());
        }

        #endregion

        #region [Execute]
        [Test]
        public void Test_Execute_Success()
        {
            var actual = new MockUnitOfWork(_dbContext, false);
            actual.GetRepository<Poco1>().Insert(new Poco1 { Id = 99999, Name = "Test" });

            Assert.AreEqual(1, actual.Execute());
        }

        [Test]
        public void Test_Execute_Exception_ObjectDisposed()
        {
            var actual = new MockUnitOfWork(_dbContext, false);
            actual.Dispose();
            Assert.Throws<ObjectDisposedException>(() => actual.Execute());
        }

        #endregion

        #region [Dispose]
        [Test]
        public void Test_Dispose_Success()
        {
            //var actual = new MSTest.PrivateObject(new MockUnitOfWork(_dbContext, false));

            //// Mock _repositoriesContainer field.
            //var repoPoco1 = new MSTest.PrivateObject(new EfRepository<Poco1>(_dbContext));
            //Dictionary<string, dynamic> repositories = new Dictionary<string, dynamic>();
            //repositories.Add(typeof(Poco1).Name, repoPoco1);
            //actual.SetField("_repositoriesContainer", repositories);

            //actual.Invoke("Dispose");
            //actual.Invoke("Dispose");

            //Assert.IsNull(actual.GetField("Context"));
        }

        #endregion
    }
}
