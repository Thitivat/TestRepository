
using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using BND.Services.Payments.eMandates.Business.Exceptions;
using BND.Services.Payments.eMandates.Business.Implementations;
using BND.Services.Payments.eMandates.Business.Interfaces;
using BND.Services.Payments.eMandates.Business.Models;
using BND.Services.Payments.eMandates.Data.Repositories;
using BND.Services.Payments.eMandates.Domain.Interfaces;
using BND.Services.Payments.eMandates.Entities;
using BND.Services.Payments.eMandates.Models;
using eMandates.Merchant.Library;
using eMandates.Merchant.Library.Configuration;
using Moq;
using NUnit.Framework;
using BND.Services.Payments.eMandates.Data.Context;
using ILogger = BND.Services.Payments.eMandates.Business.Interfaces.ILogger;
using System.Linq;
using BND.Services.Payments.eMandates.UnitTests.Proxy;

namespace BND.Services.Payments.eMandates.UnitTests.Business
{
    [TestFixture]
    public class EMandatesManagerUnitTest
    {
        #region [CreateNewTransaction]
        [Test]
        public void CreateNewTransaction()
        {
            IConfiguration config = ConfigurationFactory.GetCoreCommunicator();
            CoreCommunicator cc = new CoreCommunicator(config);
            IEMandatesClient emc = new EMandatesClient(cc);

            Mock<ITransactionRepository> transRepMock = new Mock<ITransactionRepository>();
            Mock<IDirectoryRepository> dirRepMock = new Mock<IDirectoryRepository>();
            Mock<ISettingRepository> setingsRepMock = new Mock<ISettingRepository>();

            Mock<ILogger> logger = new Mock<ILogger>();

            EMandatesManager emm = new EMandatesManager(emc, dirRepMock.Object, transRepMock.Object, setingsRepMock.Object, logger.Object);


            NewTransactionModel ntm = new NewTransactionModel();

            ntm.DebtorBankId = "ABNANL2A";

            ntm.EMandateId = "4567890";

            ntm.Language = "nl";
            ntm.SequenceType = "Ooff";

            var response = emm.CreateTransaction(ntm);
        }

        [Test]
        public void CreateNewTransaction_validation_should_throw_exception_when_ntm_null()
        {
            IConfiguration config = ConfigurationFactory.GetCoreCommunicator();
            CoreCommunicator cc = new CoreCommunicator(config);
            IEMandatesClient emc = new EMandatesClient(cc);

            Mock<ITransactionRepository> transRepMock = new Mock<ITransactionRepository>();
            Mock<IDirectoryRepository> dirRepMock = new Mock<IDirectoryRepository>();
            Mock<ISettingRepository> setingsRepMock = new Mock<ISettingRepository>();

            Mock<ILogger> logger = new Mock<ILogger>();

            EMandatesManager emm = new EMandatesManager(emc, dirRepMock.Object, transRepMock.Object, setingsRepMock.Object, logger.Object);


            NewTransactionModel ntm = null;

            Assert.Throws<ValidationException>(() => emm.CreateTransaction(ntm));
        }

        // For non-optional parameters
        [TestCase("ABNANL2A","4567890","nl", "Ooff")]
        [TestCase("", "4567890", "nl", "Ooff")]
        [TestCase(" ", "4567890", "nl", "Ooff")]
        [TestCase(null, "4567890", "nl", "Ooff")]
        [TestCase("ABNANL2AA", "4567890", "nl", "Ooff")]
        [TestCase("ABNANLA", "4567890", "nl", "Ooff")]

        [TestCase("ABNANL2A", "", "nl", "Ooff")]
        [TestCase("ABNANL2A", " ", "nl", "Ooff")]
        [TestCase("ABNANL2A", null, "nl", "Ooff")]

        [TestCase("ABNANL2A", "4567890", "", "Ooff")]
        [TestCase("ABNANL2A", "4567890", " ", "Ooff")]
        [TestCase("ABNANL2A", "4567890", null, "Ooff")]

        [TestCase("ABNANL2A", "4567890", "nl", "")]
        [TestCase("ABNANL2A", "4567890", "nl", " ")]
        [TestCase("ABNANL2A", "4567890", "nl", null)]
        public void CreateNewTransaction_should_throw_exception_when_validation_fails(string debtorBankId, string eMandateId, string language, string sequenceType)
        {
            IConfiguration config = ConfigurationFactory.GetCoreCommunicator();
            CoreCommunicator cc = new CoreCommunicator(config);
            IEMandatesClient emc = new EMandatesClient(cc);

            Mock<ITransactionRepository> transRepMock = new Mock<ITransactionRepository>();
            Mock<IDirectoryRepository> dirRepMock = new Mock<IDirectoryRepository>();
            Mock<ISettingRepository> setingsRepMock = new Mock<ISettingRepository>();

            Mock<ILogger> logger = new Mock<ILogger>();

            EMandatesManager emm = new EMandatesManager(emc, dirRepMock.Object, transRepMock.Object, setingsRepMock.Object, logger.Object);


            NewTransactionModel ntm = new NewTransactionModel();

            ntm.DebtorBankId = debtorBankId;
            ntm.EMandateId = eMandateId;
            ntm.Language = language;
            ntm.SequenceType = sequenceType;

            Assert.Throws<ValidationException>(() => emm.CreateTransaction(ntm));
        }

        [Test]
        public void CreateNewTransaction_EntranceCode_check()
        {
            Mock<IEMandatesClient> emc = new Mock<IEMandatesClient>();

            emc.Setup(x => x.SendTransactionRequest(It.IsAny<NewMandateRequestModel>())).Returns(new NewMandateResponseModel());

            Mock<ITransactionRepository> transRepMock = new Mock<ITransactionRepository>();
            Mock<IDirectoryRepository> dirRepMock = new Mock<IDirectoryRepository>();
            Mock<ISettingRepository> setingsRepMock = new Mock<ISettingRepository>();

            Mock<ILogger> logger = new Mock<ILogger>();

            EMandatesManager emm = new EMandatesManager(emc.Object, dirRepMock.Object, transRepMock.Object, setingsRepMock.Object, logger.Object);


            NewTransactionModel ntm = new NewTransactionModel();

            ntm.DebtorBankId = "ABNANL2A";
            ntm.EMandateId = "4567890";
            ntm.Language = "nl";
            ntm.SequenceType = "Ooff";

            var response = emm.CreateTransaction(ntm);

            emc.Verify(x => x.SendTransactionRequest(It.Is<NewMandateRequestModel>(c => !String.IsNullOrWhiteSpace(c.EntranceCode))), Times.Once);
        }

        [Test]
        public void CreateNewTransaction_Values_from_eMandates_unchanged()
        {
            Mock<IEMandatesClient> emc = new Mock<IEMandatesClient>();

            NewMandateResponseModel nmrm = new NewMandateResponseModel();

            nmrm.IsError = false;
            nmrm.TransactionId = "123";
            nmrm.IssuerAuthenticationUrl = "asdfghjkl";
            nmrm.RawMessage = "qwertyuiop";
            nmrm.TransactionCreateDateTimestamp = DateTime.Now;

            emc.Setup(x => x.SendTransactionRequest(It.IsAny<NewMandateRequestModel>())).Returns(nmrm);

            Mock<ITransactionRepository> transRepMock = new Mock<ITransactionRepository>();
            Mock<IDirectoryRepository> dirRepMock = new Mock<IDirectoryRepository>();
            Mock<ISettingRepository> setingsRepMock = new Mock<ISettingRepository>();

            Mock<ILogger> logger = new Mock<ILogger>();

            EMandatesManager emm = new EMandatesManager(emc.Object, dirRepMock.Object, transRepMock.Object, setingsRepMock.Object, logger.Object);


            NewTransactionModel ntm = new NewTransactionModel();

            ntm.DebtorBankId = "ABNANL2A";
            ntm.EMandateId = "4567890";
            ntm.Language = "nl";
            ntm.SequenceType = "Ooff";

            var response = emm.CreateTransaction(ntm);

            Assert.AreEqual(nmrm.TransactionId, response.TransactionId);
            Assert.AreEqual(nmrm.IssuerAuthenticationUrl, response.IssuerAuthenticationUrl);
            Assert.AreEqual(nmrm.TransactionCreateDateTimestamp, response.TransactionRequestDateTimeStamp);
        }


        #endregion [CreateNewTransaction]

        #region [GetDirectory]
        [Test]
        public void GetDirectories_Should_WorkFine()
        {
            DateTime now = DateTime.Now;
            DirectoryResponseModel responseModel = MockDirectoryResponse();

            Mock<IEMandatesClient> emClient = new Mock<IEMandatesClient>();
            emClient.Setup(s => s.SendDirectoryRequest()).Returns(responseModel);

            Mock<ITransactionRepository> transaction = new Mock<ITransactionRepository>();

            Models.Directory expectedDirectory = null;
            Models.Directory expectedDirectoryDb = MockDirectoryDb(now);

            Mock<IDirectoryRepository> directory = new Mock<IDirectoryRepository>();
            
            directory.SetupSequence(s => s.Get())
                .Returns(expectedDirectory)
                .Returns(expectedDirectoryDb);

            directory.Setup(s => s.UpdateDirectory(It.IsAny<Directory>())).Returns(1);

            Mock<ISettingRepository> setting = new Mock<ISettingRepository>();
            setting.Setup(s => s.GetValueByKey("DayCheckPeriod")).Returns("7");

            Mock<ILogger> logger = new Mock<ILogger>();
            logger.Setup(s => s.Info(It.IsAny<string>()));
            logger.Setup(s => s.Warning(It.IsAny<string>()));
            logger.Setup(s => s.Error(It.IsAny<Exception>(), It.IsAny<string>()));

            EMandatesManager manager = new EMandatesManager(emClient.Object, directory.Object, transaction.Object, setting.Object, logger.Object);
            var debtorBankList = manager.GetDirectory();

            Assert.IsNotNull(debtorBankList);            
            Assert.AreEqual(expectedDirectoryDb.DebtorBanks.Count, debtorBankList.DebtorBanks.Count);
            Assert.AreEqual(expectedDirectoryDb.DebtorBanks.First().DebtorBankCountry, debtorBankList.DebtorBanks.First().DebtorBankCountry);
            Assert.AreEqual(expectedDirectoryDb.DebtorBanks.First().DebtorBankId, debtorBankList.DebtorBanks.First().DebtorBankId);
            Assert.AreEqual(expectedDirectoryDb.DebtorBanks.First().DebtorBankName, debtorBankList.DebtorBanks.First().DebtorBankName);

        }

        [Test]
        public void GetDirectories_Should_CallService_When_LastUpdateDate_IsOverThan_DayCheckPeriod()
        {
            DateTime now = DateTime.Now;
            DirectoryResponseModel responseModel = MockDirectoryResponse();

            Mock<IEMandatesClient> emClient = new Mock<IEMandatesClient>();
            emClient.Setup(s => s.SendDirectoryRequest()).Returns(responseModel);

            Mock<ITransactionRepository> transaction = new Mock<ITransactionRepository>();

            // mock is not up to date
            Models.Directory expectedDirectory = MockDirectoryDb(now.AddDays(-9));
            Models.Directory expectedDirectoryDb2 = MockDirectoryDb(now);            

            Mock<IDirectoryRepository> directory = new Mock<IDirectoryRepository>();

            directory.SetupSequence(s => s.Get())
               .Returns(expectedDirectory)
               .Returns(expectedDirectoryDb2);

            directory.Setup(s => s.UpdateDirectory(It.IsAny<Directory>())).Returns(1);

            Mock<ISettingRepository> setting = new Mock<ISettingRepository>();
            setting.Setup(s => s.GetValueByKey("DayCheckPeriod")).Returns("7");

            Mock<ILogger> logger = new Mock<ILogger>();
            logger.Setup(s => s.Info(It.IsAny<string>()));
            logger.Setup(s => s.Warning(It.IsAny<string>()));
            logger.Setup(s => s.Error(It.IsAny<Exception>(), It.IsAny<string>()));

            EMandatesManager manager = new EMandatesManager(emClient.Object, directory.Object, transaction.Object, setting.Object, logger.Object);
            var debtorBankList = manager.GetDirectory();

            // garuntee this method must call 1 time
            emClient.Verify(x => x.SendDirectoryRequest(), Times.Once);

            Assert.IsNotNull(debtorBankList);
            Assert.AreEqual(expectedDirectoryDb2.DebtorBanks.Count, debtorBankList.DebtorBanks.Count);
            Assert.AreEqual(expectedDirectoryDb2.DebtorBanks.First().DebtorBankCountry, debtorBankList.DebtorBanks.First().DebtorBankCountry);
            Assert.AreEqual(expectedDirectoryDb2.DebtorBanks.First().DebtorBankId, debtorBankList.DebtorBanks.First().DebtorBankId);
            Assert.AreEqual(expectedDirectoryDb2.DebtorBanks.First().DebtorBankName, debtorBankList.DebtorBanks.First().DebtorBankName);

        }

        [Test]
        public void GetDirectories_Should_Throw_Exception_When_ResponseIsError()
        {
            DateTime now = DateTime.Now;

            Mock<IEMandatesClient> emClient = new Mock<IEMandatesClient>();
            emClient.Setup(s => s.SendDirectoryRequest()).Returns(MockDirectoryResponseError());

            Mock<ITransactionRepository> transaction = new Mock<ITransactionRepository>();

            Models.Directory expectedDirectory = null;
            Models.Directory expectedDirectoryDb = MockDirectoryDb(now);

            Mock<IDirectoryRepository> directory = new Mock<IDirectoryRepository>();

            directory.SetupSequence(s => s.Get())
                .Returns(expectedDirectory)
                .Returns(expectedDirectoryDb);

            directory.Setup(s => s.UpdateDirectory(It.IsAny<Directory>())).Returns(1);

            Mock<ISettingRepository> setting = new Mock<ISettingRepository>();
            setting.Setup(s => s.GetValueByKey("DayCheckPeriod")).Returns("7");

            Mock<ILogger> logger = new Mock<ILogger>();
            logger.Setup(s => s.Info(It.IsAny<string>()));
            logger.Setup(s => s.Warning(It.IsAny<string>()));
            logger.Setup(s => s.Error(It.IsAny<Exception>(), It.IsAny<string>()));

            EMandatesManager manager = new EMandatesManager(emClient.Object, directory.Object, transaction.Object, setting.Object, logger.Object);

            Assert.Throws<eMandateOperationException>(() => manager.GetDirectory());
        }
        [Test]
        public void GetDirectories_Should_Throw_Exception_When_ResponseIsNull()
        {
            DateTime now = DateTime.Now;

            Mock<IEMandatesClient> emClient = new Mock<IEMandatesClient>();
            DirectoryResponseModel responseNull = null;
            emClient.Setup(s => s.SendDirectoryRequest()).Returns(responseNull);

            Mock<ITransactionRepository> transaction = new Mock<ITransactionRepository>();

            Models.Directory expectedDirectory = null;
            Models.Directory expectedDirectoryDb = MockDirectoryDb(now);

            Mock<IDirectoryRepository> directory = new Mock<IDirectoryRepository>();

            directory.SetupSequence(s => s.Get())
                .Returns(expectedDirectory)
                .Returns(expectedDirectoryDb);

            directory.Setup(s => s.UpdateDirectory(It.IsAny<Directory>())).Returns(1);

            Mock<ISettingRepository> setting = new Mock<ISettingRepository>();
            setting.Setup(s => s.GetValueByKey("DayCheckPeriod")).Returns("7");

            Mock<ILogger> logger = new Mock<ILogger>();
            logger.Setup(s => s.Info(It.IsAny<string>()));
            logger.Setup(s => s.Warning(It.IsAny<string>()));
            logger.Setup(s => s.Error(It.IsAny<Exception>(), It.IsAny<string>()));

            EMandatesManager manager = new EMandatesManager(emClient.Object, directory.Object, transaction.Object, setting.Object, logger.Object);

            Assert.Throws<eMandateOperationException>(() => manager.GetDirectory());
        }

        [Test]
        public void GetDirectories_Should_Throw_Exception_When_Cannot_UpdateTo_Database()
        {
            DateTime now = DateTime.Now;

            Mock<IEMandatesClient> emClient = new Mock<IEMandatesClient>();
            emClient.Setup(s => s.SendDirectoryRequest()).Returns(MockDirectoryResponse());

            Mock<ITransactionRepository> transaction = new Mock<ITransactionRepository>();

            Models.Directory expectedDirectory = null;
            Models.Directory expectedDirectoryDb = MockDirectoryDb(now);

            Mock<IDirectoryRepository> directory = new Mock<IDirectoryRepository>();

            directory.SetupSequence(s => s.Get())
                .Returns(expectedDirectory)
                .Returns(expectedDirectoryDb);

            // make case cannot update to database
            directory.Setup(s => s.UpdateDirectory(It.IsAny<Directory>())).Throws(new Exception());

            Mock<ISettingRepository> setting = new Mock<ISettingRepository>();
            setting.Setup(s => s.GetValueByKey("DayCheckPeriod")).Returns("7");

            Mock<ILogger> logger = new Mock<ILogger>();
            logger.Setup(s => s.Info(It.IsAny<string>()));
            logger.Setup(s => s.Warning(It.IsAny<string>()));
            logger.Setup(s => s.Error(It.IsAny<Exception>(), It.IsAny<string>()));

            EMandatesManager manager = new EMandatesManager(emClient.Object, directory.Object, transaction.Object, setting.Object, logger.Object);

            Assert.Throws<eMandateOperationException>(() => manager.GetDirectory());
        }
        private Models.Directory MockDirectoryDb(DateTime lastupdate)
        {
            Models.Directory expectedDirectoryDb = new Models.Directory
            {
                DirectoryDateTimestamp = DateTime.Now,
                DirectoryID = 1,
                LastDirectoryRequestDateTimestamp = lastupdate
            };
            expectedDirectoryDb.DebtorBanks.Add(new Models.DebtorBank
            {
                DebtorBankCountry = "Nederland",
                DebtorBankId = "ABNANL2A",
                DebtorBankName = "BVN Test - AAB"
            });
            return expectedDirectoryDb;
        }

        private DirectoryResponseModel MockDirectoryResponse()
        {
            return new DirectoryResponseModel
            {
                DebtorBanks = new System.Collections.Generic.List<DebtorBankModel>
                {
                    new DebtorBankModel
                    {
                        DebtorBankCountry = "Nederland",
                        DebtorBankId="ABNANL2A",
                        DebtorBankName="BVN Test - AAB"
                    }                    
                },
                DirectoryDateTimestamp = DateTime.Now,
                Error = null,
                IsError = false,
                RawMessage = "RAWMESSAGE"
            };
        }

        private DirectoryResponseModel MockDirectoryResponseError()
        {
            return new DirectoryResponseModel
            {
                DebtorBanks = null,
                DirectoryDateTimestamp = DateTime.Now,
                Error = new ErrorResponseModel
                {
                    ConsumerMessage = "ConsumerMessage",
                    ErrorCode= "1234",
                    ErrorDetails = "ErrorDetails",
                    ErrorMessage = "ErrorMessage",
                    SuggestedAction = "SuggestedAction"
                },
                IsError = true,
                RawMessage = "RAWMESSAGE"
            };
        }

        ////call real db and service
        //[Test]
        //public void GetDirectories()
        //{
        //    IConfiguration config = ConfigurationFactory.GetCoreCommunicator();
        //    CoreCommunicator cc = new CoreCommunicator(config);
        //    IEMandatesClient emc = new EMandatesClient(cc);

        //    string connection = "data source=.;initial catalog=test_bnd_services_payments_emandate;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

        //    IUnitOfWork unitOfWork = new EMandateUnitOfWork(connection);
        //    IDirectoryRepository directory = new DirectoryRepository(unitOfWork);
        //    ITransactionRepository transaction = new TransactionRepository(unitOfWork);
        //    ISettingRepository setting = new SettingRepository(unitOfWork);
        //    ILogRepository log = new LogRepository(unitOfWork);
        //    ILogger logger = new Logger(log, "Test");

        //    EMandatesManager emm = new EMandatesManager(emc, directory, transaction, setting, logger);
        //    var debtorBankList = emm.GetDirectory();

        //    Assert.IsNotNull(debtorBankList);
        //}
        #endregion [GetDirectory]

        #region [GetTransactionStatus]
        [Test]
        public void GetTransactionStatus()
        {
            IConfiguration config = ConfigurationFactory.GetCoreCommunicator();
            CoreCommunicator cc = new CoreCommunicator(config);
            IEMandatesClient emc = new EMandatesClient(cc);

            Mock<ITransactionRepository> transRepMock = new Mock<ITransactionRepository>();
            Mock<IDirectoryRepository> dirRepMock = new Mock<IDirectoryRepository>();
            Mock<ISettingRepository> setingsRepMock = new Mock<ISettingRepository>();
            Mock<eMandates.Business.Interfaces.ILogger> loggerMock = new Mock<eMandates.Business.Interfaces.ILogger>();

            EMandatesManager emm = new EMandatesManager(emc, dirRepMock.Object, transRepMock.Object, setingsRepMock.Object, loggerMock.Object);

            var response = emm.GetTransactionStatus("123456789");
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GetTransactionStatus_should_throw_exception_wrong_transactionId(string transactionId)
        {
            Mock<IEMandatesClient> emc = new Mock<IEMandatesClient>();

            Mock<ITransactionRepository> transRepMock = new Mock<ITransactionRepository>();
            Mock<IDirectoryRepository> dirRepMock = new Mock<IDirectoryRepository>();
            Mock<ISettingRepository> setingsRepMock = new Mock<ISettingRepository>();
            Mock<eMandates.Business.Interfaces.ILogger> loggerMock = new Mock<eMandates.Business.Interfaces.ILogger>();

            EMandatesManager emm = new EMandatesManager(emc.Object, dirRepMock.Object, transRepMock.Object, setingsRepMock.Object, loggerMock.Object);

            Assert.Throws<ValidationException>(() => emm.GetTransactionStatus(transactionId));
        }

        [Test]
        public void GetTransactionStatus_should_throw_exception_null_transaction()
        {
            Mock<IEMandatesClient> emc = new Mock<IEMandatesClient>();

            Transaction transaction = null;

            Mock<ITransactionRepository> transRepMock = new Mock<ITransactionRepository>();
            transRepMock.Setup(x => x.GetTransactionWithLatestStatus(It.IsAny<string>())).Returns(transaction);
            Mock<IDirectoryRepository> dirRepMock = new Mock<IDirectoryRepository>();
            Mock<ISettingRepository> setingsRepMock = new Mock<ISettingRepository>();
            Mock<eMandates.Business.Interfaces.ILogger> loggerMock = new Mock<eMandates.Business.Interfaces.ILogger>();

            EMandatesManager emm = new EMandatesManager(emc.Object, dirRepMock.Object, transRepMock.Object, setingsRepMock.Object, loggerMock.Object);

            Assert.Throws<Exception>(() => emm.GetTransactionStatus("123456789"));
        }

        [Test]
        public void GetTransactionStatus_should_throw_exception_cannot_parse_status()
        {
            Mock<IEMandatesClient> emc = new Mock<IEMandatesClient>();

            Transaction transaction = new Transaction();
            transaction.TransactionStatusHistories.Add(new TransactionStatusHistory(){Status = "ass"});

            Mock<ITransactionRepository> transRepMock = new Mock<ITransactionRepository>();
            transRepMock.Setup(x => x.GetTransactionWithLatestStatus(It.IsAny<string>())).Returns(transaction);
            Mock<IDirectoryRepository> dirRepMock = new Mock<IDirectoryRepository>();
            Mock<ISettingRepository> setingsRepMock = new Mock<ISettingRepository>();
            Mock<ILogger> loggerMock = new Mock<ILogger>();

            EMandatesManager emm = new EMandatesManager(emc.Object, dirRepMock.Object, transRepMock.Object, setingsRepMock.Object, loggerMock.Object);

            Assert.Throws<Exception>(() => emm.GetTransactionStatus("123456789"));
        }

        // todo test attempts valdiation
        //[Test]
        //public void GetTransactionStatus_should_throw_exception_invalid_attempts()
        //{
        //}

        [Test]
        public void GetTransactionStatus_should_throw_exception_SendRequest_error()
        {
            Mock<IEMandatesClient> emc = new Mock<IEMandatesClient>();

            StatusResponseModel srm = null;
            emc.Setup(x => x.SendStatusRequest("12345678")).Returns(srm);

            Transaction transaction = new Transaction();

            Mock<ITransactionRepository> transRepMock = new Mock<ITransactionRepository>();
            transRepMock.Setup(x => x.GetTransactionWithLatestStatus(It.IsAny<string>())).Returns(transaction);
            Mock<IDirectoryRepository> dirRepMock = new Mock<IDirectoryRepository>();
            Mock<ISettingRepository> setingsRepMock = new Mock<ISettingRepository>();
            Mock<ILogger> loggerMock = new Mock<ILogger>();

            EMandatesManager emm = new EMandatesManager(emc.Object, dirRepMock.Object, transRepMock.Object, setingsRepMock.Object, loggerMock.Object);

            Assert.Throws<Exception>(() => emm.GetTransactionStatus("123456789"));
        }
        #endregion [GetTransactionStatus]
    }
}
