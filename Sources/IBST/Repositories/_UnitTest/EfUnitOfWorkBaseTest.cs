using BND.Services.IbanStore.Repository.Ef;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace BND.Services.IbanStore.RepositoryTest
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
            //var actual = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(new MockUnitOfWork(_dbContext, false));

            //Assert.IsNotNull(actual.GetField("_context"));
        }

        [Test]
        public void Test_Constructor_Exception_NullDbContext()
        {
            Assert.Throws<ArgumentNullException>(() => { new MockUnitOfWork(null); });

        }

        [Test]
        public void Test_Constructor_Exception_WrongConnectionString()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new MockUnitOfWork(new MockDbContext(
                    new SqlConnection("Data Source=.;Initial Catalog=FakeDb;Integrated Security=True;Pooling=False")));
            });
        }

        #endregion

        #region [GetRepository]

        [Test]
        public void Test_GetRepository_Success()
        {
            var actual = new MockUnitOfWork(_dbContext, false);

            Assert.IsInstanceOf(typeof(EfRepository<Poco1>), actual.GetRepository<Poco1>());
            
        }

        [Test]
        public void Test_GetRepository_Success_AlreadyHave_Repository()
        {
            var actual = new MockUnitOfWork(_dbContext, false);

            // Mock _repositoriesContainer field.
            //var repoPoco1 = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(new EfRepository<Poco1>(_dbContext));
            //Dictionary<string, dynamic> repositories = new Dictionary<string, dynamic>();
            //repositories.Add(typeof(Poco1).Name, repoPoco1);

            //Assert.IsInstanceOf(typeof(EfRepository<Poco1>), actual.GetRepository<Poco1>());
        }

        [Test]
        public void Test_GetRepository_Exception_ObjectDisposed()
        {
            var actual = new MockUnitOfWork(_dbContext, false);
            actual.Dispose();

            Assert.Throws<ObjectDisposedException>(() => { actual.GetRepository<Poco1>(); });
        }

        #endregion

        #region [Execute]
        [Test]
        public void Test_Execute_Success()
        {
            var actual = new MockUnitOfWork(_dbContext, false);
            actual.GetRepository<Poco1>().Insert(new Poco1 { Id = 99999, Name = "Test" });

            Assert.AreEqual(1, actual.CommitChanges());
        }

        [Test]
        public void Test_Execute_Exception_ObjectDisposed()
        {
            var actual = new MockUnitOfWork(_dbContext, false);
            actual.Dispose();

            Assert.Throws<ObjectDisposedException>(() => { actual.CommitChanges(); });
        }

        #endregion

        #region [ExecuteReader]

        [Test]
        public void Test_ExecuteReader_Success()
        {
            var actual = new MockUnitOfWork(_dbContext, false);

            List<Poco1> expectedPocos = new List<Poco1> 
            {
                new Poco1
                {
                    Id = 99,
                    Name = "Fake"
                }
            };

            //using (ShimsContext.Create())
            //{
            //    var dbRawSqlQueryMock = new System.Data.Entity.Infrastructure.Fakes.ShimDbRawSqlQuery<Poco1>();
            //    dbRawSqlQueryMock.GetEnumerator = () => expectedPocos.GetEnumerator();
            //    System.Data.Entity.Fakes.ShimDatabase.AllInstances.SqlQueryOf1StringObjectArray<Poco1>((db, sql, param) => dbRawSqlQueryMock.Instance);

            //    string sqlScript = "SELECT * FROM Poco1";
            //    Dictionary<string, object> parameters = null;

            //    var result = actual.ExecuteReader<Poco1>(sqlScript, parameters);

            //    Assert.IsNotNull(result);
            //    Assert.AreEqual(expectedPocos.Count(), result.Count());
            //    Assert.AreEqual(expectedPocos.First().Id, result.First().Id);
            //    Assert.AreEqual(expectedPocos.First().Name, result.First().Name);
            //}
        }

        #endregion

        #region [ExecuteNonQuery]

        [Test]
        public void Test_ExecuteNonQuery_Success()
        {
            var actual = new MockUnitOfWork(_dbContext, false);

            int expectedResult = 1;
            //using (ShimsContext.Create())
            //{
            //    System.Data.Entity.Fakes.ShimDatabase.AllInstances.ExecuteSqlCommandStringObjectArray = (db, sql, param) => 1;
                
            //    string sqlScript = "INSERT INTO Poco1(Id, Name)Values(99, '@Fake')";
            //    Dictionary<string, object> parameters = new Dictionary<string, object>();
            //    parameters.Add("Fake", "Fake Data");

            //    var result = actual.ExecuteNonQuery(sqlScript, parameters);

            //    Assert.IsNotNull(result);
            //    Assert.AreEqual(expectedResult, result);
            //}
        }

        #endregion

        #region [Dispose]
        [Test]
        public void Test_Dispose_Success()
        {
            //var actual = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(new MockUnitOfWork(_dbContext, false));

            //// Mock _repositoriesContainer field.
            //var repoPoco1 = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(new EfRepository<Poco1>(_dbContext));
            //Dictionary<string, dynamic> repositories = new Dictionary<string, dynamic>();
            //repositories.Add(typeof(Poco1).Name, repoPoco1);
            //actual.SetField("_repositoriesContainer", repositories);

            //actual.Invoke("Dispose");
            //actual.Invoke("Dispose");

            //Assert.IsNull(actual.GetField("_context"));
        }

        #endregion

        #region [DisposeAll]
        [Test]
        public void Test_DetachAll_Success()
        {
            //var actual = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(new MockUnitOfWork(_dbContext, false));

            //// prepare condition objects
            //var states = new System.Data.Entity.EntityState[]
            //{
            //    System.Data.Entity.EntityState.Added,
            //    System.Data.Entity.EntityState.Deleted,
            //    System.Data.Entity.EntityState.Detached,
            //    System.Data.Entity.EntityState.Modified,
            //    System.Data.Entity.EntityState.Unchanged,
            //};
            //for (int i = 0; i < states.Length; i++)
            //{
            //    var poco = new Poco1() { Id = i, Name = "poco: " + (i + 1).ToString() };
            //    _dbContext.Poco1s.Add(poco);
            //    _dbContext.Entry(poco).State = states[i];
            //}

            //// invoke DetachAll
            //try
            //{
            //    ((EfUnitOfWorkBase)actual.Target).CommitChanges();
            //}
            //catch (Exception) { }

            //// Do assert
            //Assert.AreEqual(1, _dbContext.ChangeTracker.Entries().Count());
        }
        #endregion
    }
}
