using BND.Services.IbanStore.Repository.Interfaces;
using BND.Services.IbanStore.Repository.Models;
using BND.Services.IbanStore.Service;
using BND.Services.IbanStore.Service.Bll;
using BND.Services.IbanStore.Service.Dal.Pocos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Model = BND.Services.IbanStore.Models;
using NUnit.Framework;

namespace BND.Services.IbanStore.ServiceTest
{
    [TestFixture]
    public class IbanManagerTest
    {
        #region [Fields]
        private string _connectionString = "Data Source=.;Integrated Security=True;Pooling=False;Connection Timeout=60";
        //private string _connectionString = "data source=.;initial catalog=Bndb.IBanStore.Db.Test;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

        private string _username = "Username";
        private string _context = "Context";
        #endregion

        #region Initialize
        [SetUp]
        public void Init()
        {
            MapperConfig.CreateMapper();
        }
        #endregion

        #region [Constructor]
        [Test]
        public void Test_Constructor_Success()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);
            //string user = privateObject.GetField("_username").ToString();
            //string context = privateObject.GetField("_context").ToString();
            //IEfUnitOfWork unitOfWork = privateObject.GetField("_unitOfWork") as IEfUnitOfWork;

            //Assert.AreEqual(user, _username);
            //Assert.AreEqual(context, _context);
            //Assert.IsNotNull(unitOfWork);
        }

        [Test]
        public void Test_Constructor_Fail_User_IsNull()
        {
            // Creates constructor with user 
            Assert.Throws<ArgumentNullException>(() =>
            {
                IbanManager iban = new IbanManager(null, _context, _connectionString);
            });
            // After call constructor will throw an error.
        }
        #endregion

        #region [Get List]
        [Test]
        public void Test_Get_List_Success()
        {
            int total = 0;
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //string status = "Available";
            //string ibanNumber = "NL45BNDA0110322746";

            //// expected data
            //List<p_Iban> ibanList = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        IbanId = 1,
            //        BbanFileId = 1,
            //        BankCode = "BNDA",
            //        Bban ="110322746",
            //        CheckSum ="45",
            //        CountryCode ="NL",
            //        CurrentStatusHistoryId = 1,       
            //        Uid = "uid",
            //        UidPrefix = "uid-prefix",
            //        IbanHistory = new List<p_IbanHistory>
            //        {
            //            new p_IbanHistory
            //            {
            //                Context = "context",
            //                HistoryId = 1,
            //                IbanId = 1,
            //                IbanStatusId = p_EnumIbanStatus.Available
            //            }
            //        },
            //        BbanFile =  new p_BbanFile
            //        { 
            //            BbanFileId =1, 
            //            Name = "file name" 
            //        }
            //    }
            //};
            //List<p_IbanHistory> ibanHistoryList = new List<p_IbanHistory>()
            //{
            //     new p_IbanHistory
            //        {
            //            Context = "context",
            //            HistoryId = 1,
            //            IbanId = 1,
            //            IbanStatusId = p_EnumIbanStatus.Available
            //        }
            //};

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(p => p.GetRepository<p_Iban>()).Returns(
            //        MockRepository.MockRepo<p_Iban>(ibanList, new p_Iban(), 1));
            //mockUnitOfWork.Setup(p => p.GetRepository<p_IbanHistory>()).Returns(
            //        MockRepository.MockRepo<p_IbanHistory>(ibanHistoryList, ibanHistoryList.First(), 1));

            //// Set field
            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls Get method
            //var result = iban.Get(0, 100, out total, status, ibanNumber);

            //Assert.IsNotNull(result);
            //Assert.AreEqual(ibanList.Count(), result.Count());
            //Assert.AreEqual(ibanList[0].BankCode, result.ToList()[0].BankCode);
            //Assert.AreEqual(ibanList[0].Bban, result.ToList()[0].Bban);
            //Assert.AreEqual(ibanList[0].BbanFile.Name, result.ToList()[0].BbanFileName);
            //Assert.AreEqual(ibanList[0].CheckSum, result.ToList()[0].CheckSum);
            //Assert.AreEqual(ibanNumber, result.ToList()[0].Code);
            //Assert.AreEqual(ibanList[0].CountryCode, result.ToList()[0].CountryCode);
            //Assert.AreEqual(ibanList[0].IbanId, result.ToList()[0].IbanId);
            //Assert.AreEqual(ibanList[0].Uid, result.ToList()[0].Uid);
            //Assert.AreEqual(ibanList[0].UidPrefix, result.ToList()[0].UidPrefix);
            //Assert.AreEqual(ibanHistoryList[0].Context, result.ToList()[0].CurrentIbanHistory.Context);
        }

        [Test]
        public void Test_Get_List_Success_OffsetLimit_IsNull()
        {
            int total = 0;
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //// Declare parameters
            //string status = "Available";
            //string ibanNumber = "NL45BNDA0110322746";
            //int? offset = null;
            //int? limit = null;

            //// expected data
            //List<p_Iban> ibanList = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        IbanId = 1,
            //        BbanFileId = 1,
            //        BankCode = "BNDA",
            //        Bban ="110322746",
            //        CheckSum ="45",
            //        CountryCode ="NL",
            //        CurrentStatusHistoryId = 1,       
            //        Uid = "uid",
            //        UidPrefix = "uid-prefix",
            //        IbanHistory = new List<p_IbanHistory>
            //        {
            //            new p_IbanHistory
            //            {
            //                Context = "context",
            //                HistoryId = 1,
            //                IbanId = 1,
            //                IbanStatusId = p_EnumIbanStatus.Available
            //            }
            //        },
            //        BbanFile =  new p_BbanFile
            //        { 
            //            BbanFileId =1, 
            //            Name = "file name" 
            //        }
            //    }
            //};
            //List<p_IbanHistory> ibanHistoryList = new List<p_IbanHistory>()
            //{
            //     new p_IbanHistory
            //        {
            //            Context = "context",
            //            HistoryId = 1,
            //            IbanId = 1,
            //            IbanStatusId = p_EnumIbanStatus.Available
            //        }
            //};

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(p => p.GetRepository<p_Iban>()).Returns(
            //        MockRepository.MockRepo<p_Iban>(ibanList, new p_Iban(), 1));
            //mockUnitOfWork.Setup(p => p.GetRepository<p_IbanHistory>()).Returns(
            //        MockRepository.MockRepo<p_IbanHistory>(ibanHistoryList, ibanHistoryList.First(), 1));

            //// Set field
            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls Get method
            //var result = iban.Get(offset, limit, out total, status, ibanNumber);

            //Assert.IsNotNull(result);
            //Assert.AreEqual(ibanList.Count(), result.Count());
            //Assert.AreEqual(ibanList[0].BankCode, result.ToList()[0].BankCode);
            //Assert.AreEqual(ibanList[0].Bban, result.ToList()[0].Bban);
            //Assert.AreEqual(ibanList[0].BbanFile.Name, result.ToList()[0].BbanFileName);
            //Assert.AreEqual(ibanList[0].CheckSum, result.ToList()[0].CheckSum);
            //Assert.AreEqual(ibanNumber, result.ToList()[0].Code);
            //Assert.AreEqual(ibanList[0].CountryCode, result.ToList()[0].CountryCode);
            //Assert.AreEqual(ibanList[0].IbanId, result.ToList()[0].IbanId);
            //Assert.AreEqual(ibanList[0].Uid, result.ToList()[0].Uid);
            //Assert.AreEqual(ibanList[0].UidPrefix, result.ToList()[0].UidPrefix);
            //Assert.AreEqual(ibanHistoryList[0].Context, result.ToList()[0].CurrentIbanHistory.Context);
        }

        [Test]
        public void Test_Get_List_Fail_Disposed()
        {
            int total = 0;
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //string status = "Available";
            //string ibanNumber = "NL45BNDA0110322746";

            //// Calls for test case.
            //iban.Dispose();

            //// Calls Get method
            //Assert.Throws<ObjectDisposedException>(() =>
            //{
            //    var result = iban.Get(0, 100, out total, status, ibanNumber);
            //});
        }

        [Test]
        public void Test_Get_List_Fail_Wrong_Status()
        {
            int total = 0;
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //string status = "WRONG_STATUS";
            //string ibanNumber = "NL45BNDA0110322746";

            //// expected data
            //List<p_Iban> ibanList = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        IbanId = 1,
            //        BbanFileId = 1,
            //        BankCode = "BNDA",
            //        Bban ="110322746",
            //        CheckSum ="45",
            //        CountryCode ="NL",
            //        CurrentStatusHistoryId = 1,
            //        IbanHistory = new List<p_IbanHistory>
            //        {
            //            new p_IbanHistory
            //            {
            //                Context = "context",
            //                HistoryId = 1,
            //                IbanId = 1,
            //                IbanStatusId = p_EnumIbanStatus.Available
            //            }
            //        },
            //        BbanFile =  new p_BbanFile
            //        { 
            //            BbanFileId =1, 
            //            Name = "file name" 
            //        }
            //    }
            //};

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(p => p.GetRepository<p_Iban>()).Returns(
            //        MockRepository.MockRepo<p_Iban>(ibanList, new p_Iban(), 1));

            //// Set field
            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls Get method
            //Assert.Throws<ArgumentException>(() =>
            //{
            //    var result = iban.Get(0, 100, out total, status, ibanNumber);
            //});
        }

        [Test]
        public void Test_Get_List_Fail_Wrong_Iban_Length()
        {
            int total = 0;
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //string status = "Available";
            //string ibanNumber = "NL45BNDA0110322746_WorngLength";

            //// expected data
            //List<p_Iban> ibanList = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        IbanId = 1,
            //        BbanFileId = 1,
            //        BankCode = "BNDA",
            //        Bban ="110322746",
            //        CheckSum ="45",
            //        CountryCode ="NL",
            //        CurrentStatusHistoryId = 1,
            //        IbanHistory = new List<p_IbanHistory>
            //        {
            //            new p_IbanHistory
            //            {
            //                Context = "context",
            //                HistoryId = 1,
            //                IbanId = 1,
            //                IbanStatusId = p_EnumIbanStatus.Available
            //            }
            //        },
            //        BbanFile =  new p_BbanFile
            //        { 
            //            BbanFileId =1, 
            //            Name = "file name" 
            //        }
            //    }
            //};

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(p => p.GetRepository<p_Iban>()).Returns(
            //        MockRepository.MockRepo<p_Iban>(ibanList, new p_Iban(), 1));

            //// Set field
            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls Get method
            //Assert.Throws<ArgumentException>(() =>
            //{
            //    var result = iban.Get(0, 100, out total, status, ibanNumber);
            //});
        }

        #endregion

        #region [Reserve]

        [Test]
        public void Test_Reserve_Success()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //string uid = "123456789";
            //string uidPrefix = "TEST";

            //// expected list
            //List<p_Iban> listIban_empty = new List<p_Iban>();
            //List<p_Iban> listIban_hasData = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        BankCode = "NL",
            //        Bban = "123456789",
            //        BbanFileId = 1,
            //        CheckSum = "00",
            //        CountryCode="BNDA",
            //        CurrentStatusHistoryId = 1,
            //        IbanId =1,
            //        ReservedTime = DateTime.Now,
            //        Uid = uid,
            //        UidPrefix = uidPrefix
            //    }
            //};

            //// Mock p_Iban repository
            //Mock<IRepository<p_Iban>> mockRepoIban = new Mock<IRepository<p_Iban>>();
            //mockRepoIban.SetupSequence(p => p.GetQueryable(It.IsAny<Expression<Func<p_Iban, bool>>>(),
            //                          It.IsAny<Page<p_Iban>>(),
            //                          It.IsAny<Func<IQueryable<p_Iban>, IOrderedQueryable<p_Iban>>>(),
            //                          It.IsAny<string[]>()))
            //        .Returns(listIban_empty.AsQueryable())
            //        .Returns(listIban_empty.AsQueryable())
            //        .Returns(listIban_empty.AsQueryable())
            //        .Returns(listIban_hasData.AsQueryable());

            //mockRepoIban.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //        .Returns(listIban_hasData.FirstOrDefault());

            //// Mock p_IbanHistory repository
            //Mock<IRepository<p_IbanHistory>> mockRepoIbanHistory = new Mock<IRepository<p_IbanHistory>>();
            //mockRepoIbanHistory.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //        .Returns(new p_IbanHistory
            //        {
            //            HistoryId = 1,
            //            IbanStatusId = p_EnumIbanStatus.Available
            //        });

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(m => m.CommitChanges()).Returns(1);
            //mockUnitOfWork.Setup(m => m.GetRepository<p_Iban>()).Returns(mockRepoIban.Object);
            //mockUnitOfWork.Setup(m => m.GetRepository<p_IbanHistory>()).Returns(mockRepoIbanHistory.Object);

            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls Reserved method
            //var result = iban.Reserve(uid, uidPrefix);
        }

        [Test]
        public void Test_Reserve_Success_Call_Again_After_3_Minutes()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //string uid = "123456789";
            //string uidPrefix = "TEST";

            //// expected list
            //List<p_Iban> listIban_empty = new List<p_Iban>();
            //List<p_Iban> listIban_hasData = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        BankCode = "NL",
            //        Bban = "123456789",
            //        BbanFileId = 1,
            //        CheckSum = "00",
            //        CountryCode="BNDA",
            //        CurrentStatusHistoryId = 1,
            //        IbanId =1,
            //        ReservedTime = DateTime.Now,
            //        Uid = uid,
            //        UidPrefix = uidPrefix
            //    }
            //};

            //List<p_IbanHistory> listIbanHistory_empty = new List<p_IbanHistory>();
            //List<p_IbanHistory> listIbanHistory_hasData = new List<p_IbanHistory>()
            //{
            //    new p_IbanHistory() { HistoryId  = 1 },
            //    new p_IbanHistory() { HistoryId  = 2 },
            //};

            //// Mock p_Iban repository
            //Mock<IRepository<p_Iban>> mockRepoIban = new Mock<IRepository<p_Iban>>();
            //mockRepoIban.SetupSequence(p => p.GetQueryable(It.IsAny<Expression<Func<p_Iban, bool>>>(),
            //                          It.IsAny<Page<p_Iban>>(),
            //                          It.IsAny<Func<IQueryable<p_Iban>, IOrderedQueryable<p_Iban>>>(),
            //                          It.IsAny<string[]>()))
            //        .Returns(listIban_hasData.AsQueryable())
            //        .Returns(listIban_empty.AsQueryable())
            //        .Returns(listIban_empty.AsQueryable())
            //        .Returns(listIban_hasData.AsQueryable());

            //mockRepoIban.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //        .Returns(listIban_hasData.FirstOrDefault());

            //// Mock p_IbanHistory repository
            //Mock<IRepository<p_IbanHistory>> mockRepoIbanHistory = new Mock<IRepository<p_IbanHistory>>();
            //mockRepoIbanHistory.SetupSequence(x => x.GetQueryable(It.IsAny<Expression<Func<p_IbanHistory, bool>>>(),
            //                                    It.IsAny<Page<p_IbanHistory>>(),
            //                                    It.IsAny<Func<IQueryable<p_IbanHistory>, IOrderedQueryable<p_IbanHistory>>>(),
            //                                    It.IsAny<string[]>()))
            //                .Returns(listIbanHistory_hasData.AsQueryable())
            //                .Returns(listIbanHistory_empty.AsQueryable());
            //mockRepoIbanHistory.Setup(x => x.Update(It.IsAny<p_IbanHistory>()));
            //mockRepoIbanHistory.Setup(p => p.GetById(It.IsAny<object>())).Returns(listIbanHistory_hasData.FirstOrDefault());


            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(m => m.CommitChanges()).Returns(1);
            //mockUnitOfWork.Setup(m => m.GetRepository<p_Iban>()).Returns(mockRepoIban.Object);
            //mockUnitOfWork.Setup(m => m.GetRepository<p_IbanHistory>()).Returns(mockRepoIbanHistory.Object);

            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls Reserved method
            //var result = iban.Reserve(uid, uidPrefix);
        }

        [Test]
        public void Test_Reserve_Success_Second_Time()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //string uid = "123456789";
            //string uidPrefix = "TEST";

            //// expected list
            //List<p_Iban> listIban_empty = new List<p_Iban>();
            //List<p_Iban> listIban_hasData = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        BankCode = "NL",
            //        Bban = "123456789",
            //        BbanFileId = 1,
            //        CheckSum = "00",
            //        CountryCode="BNDA",
            //        CurrentStatusHistoryId = 1,
            //        IbanId =1,
            //        ReservedTime = DateTime.Now,
            //        Uid = uid,
            //        UidPrefix = uidPrefix
            //    }
            //};

            //// Mock p_Iban repository
            //Mock<IRepository<p_Iban>> mockRepoIban = new Mock<IRepository<p_Iban>>();
            //mockRepoIban.SetupSequence(p => p.GetQueryable(It.IsAny<Expression<Func<p_Iban, bool>>>(),
            //                          It.IsAny<Page<p_Iban>>(),
            //                          It.IsAny<Func<IQueryable<p_Iban>, IOrderedQueryable<p_Iban>>>(),
            //                          It.IsAny<string[]>()))
            //        .Returns(listIban_empty.AsQueryable())
            //        .Returns(listIban_hasData.AsQueryable())
            //        .Returns(listIban_empty.AsQueryable())
            //        .Returns(listIban_hasData.AsQueryable());

            //mockRepoIban.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //        .Returns(listIban_hasData.FirstOrDefault());

            //// Mock p_IbanHistory repository
            //Mock<IRepository<p_IbanHistory>> mockRepoIbanHistory = new Mock<IRepository<p_IbanHistory>>();
            //mockRepoIbanHistory.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //        .Returns(new p_IbanHistory
            //        {
            //            HistoryId = 1,
            //            IbanStatusId = p_EnumIbanStatus.Available
            //        });

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(m => m.CommitChanges()).Returns(1);
            //mockUnitOfWork.Setup(m => m.GetRepository<p_Iban>()).Returns(mockRepoIban.Object);
            //mockUnitOfWork.Setup(m => m.GetRepository<p_IbanHistory>()).Returns(mockRepoIbanHistory.Object);

            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls Reserved method
            //var result = iban.Reserve(uid, uidPrefix);
        }

        [Test]
        public void Test_Reserve_Fail_Disposed()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            string uid = "";
            string uidPrefix = "TEST";

            // Calls for test case.
            iban.Dispose();

            // Calls Reserved method
            Assert.Throws<ObjectDisposedException>(() =>
            {
                var ibanModel = iban.Reserve(uid, uidPrefix);
            });
        }

        [Test]
        public void Test_Reserve_Fail_UID_IsNull()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            string uid = "";
            string uidPrefix = "TEST";

            // Calls Reserved method
            Assert.Throws<ArgumentNullException>(() =>
            {
                var ibanModel = iban.Reserve(uid, uidPrefix);
            });
        }

        [Test]
        public void Test_Reserve_Fail_UidPrefix_IsNull()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            string uid = "2345678";
            string uidPrefix = "";

            // Calls Reserved method
            Assert.Throws<ArgumentNullException>(() =>
            {
                var ibanModel = iban.Reserve(uid, uidPrefix);
            });
        }

        [Test]
        public void Test_Reserve_Thow_Already_Assigned()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //string uid = "2345678";
            //string uidPrefix = "TEST";

            //// expected list
            //List<p_Iban> listIban_empty = new List<p_Iban>();
            //List<p_Iban> listIban_hasData = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        BankCode = "NL",
            //        Bban = "123456789",
            //        BbanFileId = 1,
            //        CheckSum = "00",
            //        CountryCode="BNDA",
            //        CurrentStatusHistoryId = 1,
            //        IbanId =1,
            //        ReservedTime = DateTime.Now,
            //        Uid = uid,
            //        UidPrefix = uidPrefix
            //    }
            //};

            //// Mock p_Iban repository
            //Mock<IRepository<p_Iban>> mockRepoIban = new Mock<IRepository<p_Iban>>();
            //mockRepoIban.SetupSequence(p => p.GetQueryable(It.IsAny<Expression<Func<p_Iban, bool>>>(),
            //                          It.IsAny<Page<p_Iban>>(),
            //                          It.IsAny<Func<IQueryable<p_Iban>, IOrderedQueryable<p_Iban>>>(),
            //                          It.IsAny<string[]>()))
            //        .Returns(listIban_empty.AsQueryable())
            //        .Returns(listIban_empty.AsQueryable())
            //        .Returns(listIban_empty.AsQueryable())
            //        .Returns(listIban_hasData.AsQueryable());

            //// Mock UnitOfWork & Repository
            //Mock<IEfUnitOfWork> mockUnitTest = new Mock<IEfUnitOfWork>();
            //mockUnitTest.Setup(s => s.CommitChanges()).Returns(1);
            //mockUnitTest.Setup(s => s.GetRepository<p_Iban>()).Returns(mockRepoIban.Object);

            //// Set field
            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitTest.Object);

            //// Calls Reserved method
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    var ibanModel = iban.Reserve(uid, uidPrefix);
            //});
        }

        [Test]
        public void Test_Reserve_Fail_NO_IBAN()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //string uid = "2345678";
            //string uidPrefix = "TEST";

            //// Mock UnitOfWork & Repository
            //Mock<IEfUnitOfWork> mockUnitTest = new Mock<IEfUnitOfWork>();
            //mockUnitTest.Setup(s => s.CommitChanges()).Returns(1);
            //mockUnitTest.Setup(s => s.GetRepository<p_Iban>())
            //            .Returns(MockRepository.MockRepo<p_Iban>(new List<p_Iban>(), new p_Iban(), 1));

            //// Set field
            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitTest.Object);

            //// Calls Reserved method
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    var ibanModel = iban.Reserve(uid, uidPrefix);
            //});
        }

        [Test]
        public void Test_Reserve_Fail_Exeucte_Error()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //string uid = "123456789";
            //string uidPrefix = "TEST";

            //// expected list
            //List<p_Iban> listIban_empty = new List<p_Iban>();
            //List<p_Iban> listIban_hasData = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        BankCode = "NL",
            //        Bban = "123456789",
            //        BbanFileId = 1,
            //        CheckSum = "00",
            //        CountryCode="BNDA",
            //        CurrentStatusHistoryId = 1,
            //        IbanId =1,
            //        ReservedTime = DateTime.Now,
            //        Uid = uid,
            //        UidPrefix = uidPrefix
            //    }
            //};

            //// Mock p_Iban repository
            //Mock<IRepository<p_Iban>> mockRepoIban = new Mock<IRepository<p_Iban>>();
            //mockRepoIban.SetupSequence(p => p.GetQueryable(It.IsAny<Expression<Func<p_Iban, bool>>>(),
            //                          It.IsAny<Page<p_Iban>>(),
            //                          It.IsAny<Func<IQueryable<p_Iban>, IOrderedQueryable<p_Iban>>>(),
            //                          It.IsAny<string[]>()))
            //        .Returns(listIban_empty.AsQueryable())
            //        .Returns(listIban_empty.AsQueryable())
            //        .Returns(listIban_hasData.AsQueryable());

            //mockRepoIban.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //        .Returns(listIban_hasData.FirstOrDefault());

            //// Mock p_IbanHistory repository
            //Mock<IRepository<p_IbanHistory>> mockRepoIbanHistory = new Mock<IRepository<p_IbanHistory>>();
            //mockRepoIbanHistory.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //        .Returns(new p_IbanHistory
            //        {
            //            HistoryId = 1,
            //            IbanStatusId = p_EnumIbanStatus.Available
            //        });

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(m => m.CommitChanges()).Throws(new InvalidOperationException());
            //mockUnitOfWork.Setup(m => m.GetRepository<p_Iban>()).Returns(mockRepoIban.Object);
            //mockUnitOfWork.Setup(m => m.GetRepository<p_IbanHistory>()).Returns(mockRepoIbanHistory.Object);

            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls Reserved method
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    var result = iban.Reserve(uid, uidPrefix);
            //});
        }

        #endregion

        #region [Assign]
        [Test]
        public void Test_Assign_Success()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //int ibanId = 1;
            //string uid = "123456789";
            //string uidPrefix = "TEST";

            //// expected data
            //List<p_Iban> ibanListEmpty = new List<p_Iban>();
            //List<p_Iban> ibanList = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        IbanId = 1,
            //        BbanFileId = 1,
            //        BankCode = "BNDA",
            //        Bban ="110322746",
            //        CheckSum ="45",
            //        CountryCode ="NL",
            //        CurrentStatusHistoryId = 1,
            //        ReservedTime = DateTime.Now,
            //        Uid = uid,
            //        UidPrefix = uidPrefix,
            //        IbanHistory = new List<p_IbanHistory>
            //        {
            //            new p_IbanHistory
            //            {
            //                Context = "context",
            //                HistoryId = 1,
            //                IbanId = 1,
            //                IbanStatusId = p_EnumIbanStatus.Available
            //            }
            //        },
            //        BbanFile =  new p_BbanFile
            //        { 
            //            BbanFileId =1, 
            //            Name = "file name" 
            //        }
            //    }
            //};
            //List<p_IbanHistory> ibanHistoryList = new List<p_IbanHistory>()
            //{
            //     new p_IbanHistory
            //        {
            //            Context = "context",
            //            HistoryId = 1,
            //            IbanId = 1,
            //            IbanStatusId = p_EnumIbanStatus.Available
            //        }
            //};
            //List<p_BbanFile> bbanfileList = new List<p_BbanFile>
            //{
            //    new p_BbanFile
            //        { 
            //            BbanFileId =1, 
            //            Name = "file name" 
            //        }
            //};

            //// Mock Repository
            //Mock<IRepository<p_Iban>> mockIbanReposirory = new Mock<IRepository<p_Iban>>();
            //mockIbanReposirory.SetupSequence(s => s.GetQueryable(It.IsAny<Expression<Func<p_Iban, bool>>>(),
            //                          It.IsAny<Page<p_Iban>>(),
            //                          It.IsAny<Func<IQueryable<p_Iban>, IOrderedQueryable<p_Iban>>>(),
            //                          It.IsAny<string[]>()))
            //                 .Returns(ibanListEmpty.AsQueryable())
            //                 .Returns(ibanList.AsQueryable());
            //mockIbanReposirory.Setup(s => s.GetById(It.IsAny<object[]>()))
            //                 .Returns(ibanList.First());
            //Mock<IRepository<p_IbanHistory>> mockIbanHistoryReposirory = new Mock<IRepository<p_IbanHistory>>();
            //mockIbanHistoryReposirory.Setup(s => s.GetById(It.IsAny<object[]>()))
            //                 .Returns(ibanHistoryList.First());

            //// Mock Unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);
            //mockUnitOfWork.Setup(s => s.GetRepository<p_Iban>()).Returns(mockIbanReposirory.Object);
            //mockUnitOfWork.Setup(s => s.GetRepository<p_IbanHistory>()).Returns(mockIbanHistoryReposirory.Object);

            //// Sets private field
            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls method
            //Model.Iban model = iban.Assign(ibanId, uid, uidPrefix);

            //Assert.IsNotNull(model);
        }

        [Test]
        public void Test_Assign_Fail_Disposed()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //int ibanId = 1;
            //string uid = "123456789";
            //string uidPrefix = "TEST";

            //// Calls for test case.
            //iban.Dispose();

            //// Calls method
            //Assert.Throws<ObjectDisposedException>(() =>
            //{
            //    Model.Iban ibanModel = iban.Assign(ibanId, uid, uidPrefix);
            //});

        }

        [Test]
        public void Test_Assign_Fail_IbanId_LessThanOrEqualZero()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //int ibanId = 0;
            //string uid = "123456789";
            //string uidPrefix = "TEST";

            //// Calls method
            //Assert.Throws<ArgumentException>(() =>
            //{
            //    Model.Iban ibanModel = iban.Assign(ibanId, uid, uidPrefix);
            //});

        }

        [Test]
        public void Test_Assign_Fail_Uid_IsEmpty()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //int ibanId = 1;
            //string uid = "";
            //string uidPrefix = "TEST";

            //// Calls method
            //Assert.Throws<ArgumentNullException>(() =>
            //{
            //    Model.Iban ibanModel = iban.Assign(ibanId, uid, uidPrefix);
            //});

        }

        [Test]
        public void Test_Assign_Fail_UidPrefix_IsEmpty()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //int ibanId = 1;
            //string uid = "123456789";
            //string uidPrefix = "";

            //// Calls method
            //Assert.Throws<ArgumentNullException>(() =>
            //{
            //    Model.Iban ibanModel = iban.Assign(ibanId, uid, uidPrefix);
            //});

        }

        [Test]
        public void Test_Assign_Fail_Already_Assign()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //// Declare parameters
            //int ibanId = 1;
            //string uid = "uid";
            //string uidPrefix = "uit-prefix";

            //// Create expected data.
            //List<p_Iban> ibanList = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        IbanId = 1,
            //        BbanFileId = 1,
            //        BankCode = "BNDA",
            //        Bban ="110322746",
            //        CheckSum ="45",
            //        CountryCode ="NL",
            //        CurrentStatusHistoryId = 2,       
            //        Uid = "uid",
            //        UidPrefix = "uid-prefix",
            //        IbanHistory = new List<p_IbanHistory>
            //        {
            //            new p_IbanHistory
            //            {
            //                Context = "context",
            //                HistoryId = 2,
            //                IbanId = 1,
            //                IbanStatusId = p_EnumIbanStatus.Assigned
            //            }
            //        },
            //        BbanFile =  new p_BbanFile
            //        { 
            //            BbanFileId =1, 
            //            Name = "file name" 
            //        }
            //    }
            //};

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);
            //mockUnitOfWork.Setup(s => s.GetRepository<p_Iban>())
            //              .Returns(MockRepository.MockRepo<p_Iban>(ibanList, ibanList.First(), 1));

            //// Injected to ibanmanager class
            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls method
            //Assert.Throws<IbanAlreadyAssignedException>(() =>
            //{
            //    Model.Iban ibanModel = iban.Assign(ibanId, uid, uidPrefix);
            //});
        }

        [Test]
        public void Test_Assign_Fail_IBAN_NotFound()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //// Declare parameters
            //int ibanId = 1;
            //string uid = "uid";
            //string uidPrefix = "uit-prefix";

            //// Create expected data.
            //List<p_Iban> ibanListEmpty = new List<p_Iban>();
            //List<p_Iban> ibanList = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        IbanId = 1,
            //        BbanFileId = 1,
            //        BankCode = "BNDA",
            //        Bban ="110322746",
            //        CheckSum ="45",
            //        CountryCode ="NL",
            //        CurrentStatusHistoryId = 2,       
            //        Uid = "uid",
            //        UidPrefix = "uid-prefix",
            //        ReservedTime = DateTime.Now,
            //        IbanHistory = new List<p_IbanHistory>
            //        {
            //            new p_IbanHistory
            //            {
            //                Context = "context",
            //                HistoryId = 2,
            //                IbanId = 1,
            //                IbanStatusId = p_EnumIbanStatus.Assigned
            //            }
            //        },
            //        BbanFile =  new p_BbanFile
            //        { 
            //            BbanFileId =1, 
            //            Name = "file name" 
            //        }
            //    }
            //};

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);
            //mockUnitOfWork.SetupSequence(s => s.GetRepository<p_Iban>())
            //              .Returns(MockRepository.MockRepo<p_Iban>(ibanListEmpty, ibanListEmpty.FirstOrDefault(), 1))
            //              .Returns(MockRepository.MockRepo<p_Iban>(ibanListEmpty, ibanListEmpty.FirstOrDefault(), 1));

            //// Injected to ibanmanager class
            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls method
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    Model.Iban ibanModel = iban.Assign(ibanId, uid, uidPrefix);
            //});
        }

        [Test]
        public void Test_Assign_Fail_ReservedTime_HasNoValue()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //// Declare parameters
            //int ibanId = 1;
            //string uid = "uid";
            //string uidPrefix = "uit-prefix";

            //// Create expected data.
            //List<p_Iban> ibanListEmpty = new List<p_Iban>();
            //List<p_Iban> ibanList = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        IbanId = 1,
            //        BbanFileId = 1,
            //        BankCode = "BNDA",
            //        Bban ="110322746",
            //        CheckSum ="45",
            //        CountryCode ="NL",
            //        CurrentStatusHistoryId = 2,       
            //        Uid = "uid",
            //        UidPrefix = "uid-prefix",
            //        ReservedTime = null,
            //        IbanHistory = new List<p_IbanHistory>
            //        {
            //            new p_IbanHistory
            //            {
            //                Context = "context",
            //                HistoryId = 2,
            //                IbanId = 1,
            //                IbanStatusId = p_EnumIbanStatus.Assigned
            //            }
            //        },
            //        BbanFile =  new p_BbanFile
            //        { 
            //            BbanFileId =1, 
            //            Name = "file name" 
            //        }
            //    }
            //};

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);
            //mockUnitOfWork.SetupSequence(s => s.GetRepository<p_Iban>())
            //              .Returns(MockRepository.MockRepo<p_Iban>(ibanListEmpty, ibanListEmpty.FirstOrDefault(), 1))
            //              .Returns(MockRepository.MockRepo<p_Iban>(ibanList, ibanList.FirstOrDefault(), 1));

            //// Injected to ibanmanager class
            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls method
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    Model.Iban ibanModel = iban.Assign(ibanId, uid, uidPrefix);
            //});
        }

        [Test]
        public void Test_Assign_Fail_ReservedTime_MoreThanSetting()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //// Declare parameters
            //int ibanId = 1;
            //string uid = "uid";
            //string uidPrefix = "uit-prefix";

            //// Create expected data.
            //List<p_Iban> ibanListEmpty = new List<p_Iban>();
            //List<p_Iban> ibanList = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        IbanId = 1,
            //        BbanFileId = 1,
            //        BankCode = "BNDA",
            //        Bban ="110322746",
            //        CheckSum ="45",
            //        CountryCode ="NL",
            //        CurrentStatusHistoryId = 2,       
            //        Uid = "uid",
            //        UidPrefix = "uid-prefix",
            //        ReservedTime = DateTime.Now.AddMinutes(-5),
            //        IbanHistory = new List<p_IbanHistory>
            //        {
            //            new p_IbanHistory
            //            {
            //                Context = "context",
            //                HistoryId = 2,
            //                IbanId = 1,
            //                IbanStatusId = p_EnumIbanStatus.Assigned
            //            }
            //        },
            //        BbanFile =  new p_BbanFile
            //        { 
            //            BbanFileId =1, 
            //            Name = "file name" 
            //        }
            //    }
            //};

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);
            //mockUnitOfWork.SetupSequence(s => s.GetRepository<p_Iban>())
            //              .Returns(MockRepository.MockRepo<p_Iban>(ibanListEmpty, ibanListEmpty.FirstOrDefault(), 1))
            //              .Returns(MockRepository.MockRepo<p_Iban>(ibanList, ibanList.FirstOrDefault(), 1));

            //// Injected to ibanmanager class
            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls method
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    Model.Iban ibanModel = iban.Assign(ibanId, uid, uidPrefix);
            //});
        }
        #endregion

        #region [Get by uid uid-prefix]
        [Test]
        public void Test_Get_By_Uid_And_UidPrefix_Success()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //// Declare parameters
            //string uid = "UID";
            //string uidPrefix = "PREFIX";

            //// expected data
            //List<p_Iban> ibanList = new List<p_Iban> 
            //{
            //    new p_Iban
            //    {
            //        IbanId = 1,
            //        BbanFileId = 1,
            //        BankCode = "BNDA",
            //        Bban ="110322746",
            //        CheckSum ="45",
            //        CountryCode ="NL",
            //        CurrentStatusHistoryId = 1,       
            //        Uid = "uid",
            //        UidPrefix = "uid-prefix",
            //        IbanHistory = new List<p_IbanHistory>
            //        {
            //            new p_IbanHistory
            //            {
            //                Context = "context",
            //                HistoryId = 1,
            //                IbanId = 1,
            //                IbanStatusId = p_EnumIbanStatus.Available
            //            }
            //        },
            //        BbanFile =  new p_BbanFile
            //        { 
            //            BbanFileId =1, 
            //            Name = "file name" 
            //        }
            //    }
            //};
            //List<p_IbanHistory> ibanHistoryList = new List<p_IbanHistory>()
            //{
            //     new p_IbanHistory
            //        {
            //            Context = "context",
            //            HistoryId = 1,
            //            IbanId = 1,
            //            IbanStatusId = p_EnumIbanStatus.Available
            //        }
            //};

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(p => p.GetRepository<p_Iban>()).Returns(
            //        MockRepository.MockRepo<p_Iban>(ibanList, new p_Iban(), 1));
            //mockUnitOfWork.Setup(p => p.GetRepository<p_IbanHistory>()).Returns(
            //        MockRepository.MockRepo<p_IbanHistory>(ibanHistoryList, ibanHistoryList.First(), 1));

            //// Set field
            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls Get method
            //Model.Iban model = iban.Get(uid, uidPrefix);
        }

        [Test]
        public void Test_Get_By_Uid_And_UidPrefix_Fail_Uid_Is_Null()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //// Declare parameters
            //string uid = null;
            //string uidPrefix = "PREFIX";

            //// Calls Get method
            //Assert.Throws<ArgumentNullException>(() =>
            //{
            //    Model.Iban model = iban.Get(uid, uidPrefix);
            //});
        }

        [Test]
        public void Test_Get_By_Uid_And_UidPrefix_Fail_UidPrefix_Is_Null()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //// Declare parameters
            //string uid = "UID";
            //string uidPrefix = null;

            //// Calls Get method
            //Assert.Throws<ArgumentNullException>(() =>
            //{
            //    Model.Iban model = iban.Get(uid, uidPrefix);
            //});
        }

        [Test]
        public void Test_Get_By_Uid_And_UidPrefix_Fail_IbanPoco_Count_Is_Zero()
        {
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            //var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

            //// Declare parameters
            //string uid = "UID";
            //string uidPrefix = "PREFIX";

            //// expected data
            //List<p_Iban> ibanList = new List<p_Iban>();

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockUnitOfWork.Setup(p => p.GetRepository<p_Iban>()).Returns(
            //        MockRepository.MockRepo<p_Iban>(ibanList, new p_Iban(), 1));

            //// Set field
            //pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

            //// Calls Get method
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    Model.Iban model = iban.Get(uid, uidPrefix);
            //});
        }

        #endregion

        #region [Get by id]
        [Test]
        public void Test_GetById_Exception_ArgumentNull()
        {
            int id = 0;
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            Assert.Throws<ArgumentNullException>(() =>
            {
                iban.Get(id);
            });
        }

        //[Test]
        //public void Test_GetById_Exception_IbanOperation()
        //{
        //    int id = 1;
        //    IbanManager iban = new IbanManager(_username, _context, _connectionString);
        //    var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);
        //    List<p_Iban> expectModel = new List<p_Iban>();

        //    Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockUnitOfWork.Setup(r => r.GetRepository<p_Iban>())
        //        .Returns(MockRepository.MockRepo<p_Iban>(expectModel, new p_Iban(), 1));

        //    pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

        //    Assert.Throws<IbanOperationException>(() =>
        //    {
        //        var result = ((IbanManager)pObject.Target).Get(id);
        //    });
        //}

        //[Test]
        //public void Test_GetById_Success()
        //{
        //    int id = 1;
        //    IbanManager iban = new IbanManager(_username, _context, _connectionString);
        //    var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

        //    List<p_Iban> expectsModel = new List<p_Iban>
        //    {
        //        new p_Iban 
        //        {
        //            IbanId = 1,
        //            BbanFileId = 1,
        //            BankCode = "ABNA",
        //            Bban = "0417164300",
        //            CheckSum ="91",
        //            CountryCode = "NL",
        //            CurrentStatusHistoryId = 1,
        //            IbanHistory = new List<p_IbanHistory>
        //            {
        //                new p_IbanHistory
        //                {
        //                    Context = "context",
        //                    HistoryId = 1,
        //                    IbanId = 1,
        //                    IbanStatusId = p_EnumIbanStatus.Available
        //                }
        //            },
        //            BbanFile =  new p_BbanFile
        //            { 
        //                BbanFileId =1, 
        //                Name = "file name" 
        //            }
        //        }
        //    };

        //    List<p_IbanHistory> ibanHistories = new List<p_IbanHistory>()
        //    {
        //        new p_IbanHistory
        //        {
        //            HistoryId=1,
        //            IbanId=1,
        //            Remark = "remark",
        //            IbanStatusId = p_EnumIbanStatus.Available,
        //            Context="context"
        //        }
        //    };

        //    List<p_BbanFile> bbanFiles = new List<p_BbanFile>
        //    {
        //        new p_BbanFile
        //            { 
        //                BbanFileId =1, 
        //                Name = "file name" 
        //            }
        //    };

        //    // Mock unit of work
        //    Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockUnitOfWork.Setup(r => r.GetRepository<p_Iban>())
        //        .Returns(MockRepository.MockRepo<p_Iban>(expectsModel, new p_Iban(), 1));
        //    mockUnitOfWork.Setup(p => p.GetRepository<p_IbanHistory>()).Returns(
        //            MockRepository.MockRepo<p_IbanHistory>(ibanHistories, new p_IbanHistory(), 1));
        //    mockUnitOfWork.Setup(p => p.GetRepository<p_BbanFile>()).Returns(
        //            MockRepository.MockRepo<p_BbanFile>(bbanFiles, new p_BbanFile(), 1));

        //    // Set field
        //    pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

        //    var result = ((IbanManager)pObject.Target).Get(id);

        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(expectsModel.First().BankCode == result.BankCode
        //               && expectsModel.First().Bban == result.Bban
        //               && expectsModel.First().CountryCode == result.CountryCode);

        //}
        #endregion

        #region [Get by iban]
        //[Test]
        //public void Test_GetByIban_Success()
        //{
        //    string iban = @"NL91ABNA0417164300";
        //    IbanManager ibanManager = new IbanManager(_username, _context, _connectionString);
        //    var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(ibanManager);

        //    List<p_Iban> expectedModel = new List<p_Iban>
        //    {
        //        new p_Iban
        //        {
        //            IbanId = 1,
        //            BbanFileId = 1,
        //            BankCode = "ABNA",
        //            Bban = "0417164300",
        //            CheckSum ="91",
        //            CountryCode = "NL",
        //            CurrentStatusHistoryId = 1,
        //            IbanHistory = new List<p_IbanHistory>
        //            {
        //                new p_IbanHistory
        //                {
        //                    Context = "context",
        //                    HistoryId = 1,
        //                    IbanId = 1,
        //                    IbanStatusId = p_EnumIbanStatus.Available
        //                }
        //            },
        //            BbanFile =  new p_BbanFile
        //            { 
        //                BbanFileId =1, 
        //                Name = "file name" 
        //            }
        //        }
        //    };

        //    List<p_IbanHistory> ibanHistories = new List<p_IbanHistory>()
        //    {
        //        new p_IbanHistory
        //        {
        //            HistoryId=1,
        //            IbanId=1,
        //            Remark = "remark",
        //            IbanStatusId = p_EnumIbanStatus.Available,
        //            Context="context"
        //        }
        //    };

        //    List<p_BbanFile> bbanFiles = new List<p_BbanFile>
        //    {
        //        new p_BbanFile
        //            { 
        //                BbanFileId =1, 
        //                Name = "file name" 
        //            }
        //    };

        //    // Mock unit of work
        //    Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockUnitOfWork.Setup(r => r.GetRepository<p_Iban>())
        //        .Returns(MockRepository.MockRepo<p_Iban>(expectedModel, new p_Iban(), 1));
        //    mockUnitOfWork.Setup(p => p.GetRepository<p_IbanHistory>()).Returns(
        //            MockRepository.MockRepo<p_IbanHistory>(ibanHistories, new p_IbanHistory(), 1));
        //    mockUnitOfWork.Setup(p => p.GetRepository<p_BbanFile>()).Returns(
        //            MockRepository.MockRepo<p_BbanFile>(bbanFiles, new p_BbanFile(), 1));

        //    // Set field
        //    pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

        //    // Calls Get method
        //    var result = ((IbanManager)pObject.Target).Get(iban);

        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(expectedModel.First().BankCode == result.BankCode
        //               && expectedModel.First().Bban == result.Bban
        //               && expectedModel.First().CountryCode == result.CountryCode);
        //}

        [Test]
        public void Test_GetByIban_Fail()
        {
            string iban = string.Empty;
            IbanManager ibanManager = new IbanManager(_username, _context, _connectionString);
            Assert.Throws<ArgumentNullException>(() =>
            {
                ibanManager.Get(iban);
            });
        }
        #endregion

        #region [GetHistory]
        //[Test]
        //public void Test_GetHistory_Success()
        //{
        //    IbanManager iban = new IbanManager(_username, _context, _connectionString);
        //    var pObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(iban);

        //    List<p_IbanHistory> ibanHistories = new List<p_IbanHistory>()
        //    {
        //        new p_IbanHistory
        //        {
        //            HistoryId=1,
        //            IbanId=1,
        //            Remark = "remark",
        //            IbanStatusId = p_EnumIbanStatus.Available,
        //            Context="context"
        //        }
        //    };

        //    // Mock unit of work
        //    Mock<IEfUnitOfWork> mockUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockUnitOfWork.Setup(p => p.GetRepository<p_IbanHistory>())
        //        .Returns(MockRepository.MockRepo<p_IbanHistory>(ibanHistories, new p_IbanHistory(), 1));

        //    // Set field
        //    pObject.SetFieldOrProperty("_unitOfWork", mockUnitOfWork.Object);

        //    // Calls Get method
        //    var result = ((IbanManager)pObject.Target).GetHistory(1);

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(ibanHistories.Count(), result.Count());
        //}

        [Test]
        public void Test_GetHistory_Exception_ObjectDisposed()
        {
            int id = 1;
            IbanManager iban = new IbanManager(_username, _context, _connectionString);
            // Calls for test case.
            iban.Dispose();
            // Calls Get method
            Assert.Throws<ObjectDisposedException>(() =>
            {
                var result = iban.GetHistory(id);
            });
        }
        #endregion

        #region [Disposed]
        #endregion
    }
}
