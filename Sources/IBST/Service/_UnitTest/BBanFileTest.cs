using BND.Services.IbanStore.Models;
using BND.Services.IbanStore.Repository.Ef;
using BND.Services.IbanStore.Repository.Interfaces;
using BND.Services.IbanStore.Repository.Models;
using BND.Services.IbanStore.Service;
using BND.Services.IbanStore.Service.Bll;
using BND.Services.IbanStore.Service.Bll.Interfaces;
using BND.Services.IbanStore.Service.Dal.Pocos;
using Moq;
using BND.Services.IbanStore.ServiceTest.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Linq;
using NUnit.Framework;

namespace BND.Services.IbanStore.ServiceTest
{
    [TestFixture]
    public class BBanFileTest
    {
        private string _connectionString = "Data Source=.;Integrated Security=True;Pooling=False;Connection Timeout=60";

        #region [Fields]

        private byte[] _csvFile = null;
        private p_Iban _iban = null;
        private p_IbanHistory _ibanHistory = null;
        private p_BbanFile _bbanfileUploading = null;
        private p_BbanFileHistory _bbanfileHistory = null;
        private p_BbanImport _bbanImport = null;
        private List<p_BbanImport> _listBBAN = null;
        private List<p_Iban> _listIBAN = null;
        private List<p_IbanHistory> _listIBANHistory = null;
        private List<p_BbanFile> _listBbanFile = null;
        private List<p_BbanFileHistory> _listBbanFileHistory = null;
        private string _token = "Token";
        private string _context = "Context";
        #endregion


        #region Initialize
        [SetUp]
        public void Init()
        {
            MapperConfig.CreateMapper();
            _csvFile = Encoding.UTF8.GetBytes(Resources.BBANFile_Sample);

            _bbanfileUploading = new p_BbanFile
            {
                BbanFileId = 1,
                Hash = "12345678i9rtyuikfvgbnhmj",
                RawFile = _csvFile,
                Name = "test upload",
                CurrentStatusHistoryId = 11
            };
            _listBbanFile = new List<p_BbanFile>
            {
                new p_BbanFile
                {
                    BbanFileId = 1,
                    Hash = "12345678i9rtyuikfvgbnhmj",
                    RawFile = _csvFile,
                    Name = "test upload"
                }
            };
            _bbanfileHistory = new p_BbanFileHistory
            {
                HistoryId = 1,
                BbanFileId = 1,
                BbanFileStatusId = p_EnumBbanFileStatus.Imported,
                ChangedBy = "admin",
                ChangedDate = DateTime.Now,
                Context = "client"
            };

            _bbanImport = new p_BbanImport
            {
                BbanImportId = 1,
                BbanFileId = 1,
                Bban = "110322746",
                IsImported = false
            };

            _listBBAN = new List<p_BbanImport>
            {
                new p_BbanImport
                {
                    BbanImportId = 1,
                    BbanFileId =1,
                    Bban = "110322746",
                    IsImported = false
                },
                new p_BbanImport
                {
                    BbanImportId = 2,
                    BbanFileId =1,
                    Bban = "833102680",
                    IsImported = false
                },
                new p_BbanImport
                {
                    BbanImportId = 3,
                    BbanFileId =1,
                    Bban = "178114634",
                    IsImported = false
                }
            };

            _iban = new p_Iban
            {
                BankCode = "NL",
                Bban = "123456789",
                BbanFileId = 1,
                CheckSum = "00",
                CountryCode = "BNDA",
                CurrentStatusHistoryId = 1,
                IbanId = 1,
                ReservedTime = DateTime.Now,
                Uid = "Uid",
                UidPrefix = "UidPrefix",
                BbanFile = new p_BbanFile()
                {
                    Name = "BBAN_FILE.csv",
                },
            };
            _ibanHistory = new p_IbanHistory
            {
                HistoryId = 1,
                IbanStatusId = p_EnumIbanStatus.Available
            };
            _listBbanFileHistory = new List<p_BbanFileHistory>
            {
                _bbanfileHistory
            };
            _listIBAN = new List<p_Iban>
            {
                _iban
            };
            _listIBANHistory = new List<p_IbanHistory>
            {
                _ibanHistory
            };

            AssemblyInit(null);
        }


        //[AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
        }
        #endregion


        #region Constructor

        [Test]
        public void Test_Constructor_Success()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
            //string token = privateObject.GetField("_username").ToString();
            //string context = privateObject.GetField("_context").ToString();
            //IEfUnitOfWork IEfUnitOfWork = privateObject.GetField("_unitOfWork") as IEfUnitOfWork;

            //Assert.AreEqual(token, _token);
            //Assert.AreEqual(context, _context);
            //Assert.IsNotNull(IEfUnitOfWork);
        }

        [Test]
        public void Test_Constructor_Fail_Token_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                IBbanFileManager bbanfile = new BbanFileManager("", _context, _connectionString, Resources.ConfigurationFile);
            });
        }
        #endregion


        #region Save

        [Test]
        public void Test_Save_Success()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(p => p.CommitChanges()).Returns(1);

            //_bbanfileUploading.CurrentStatusHistoryId = null;
            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), _bbanfileUploading, 1));
            //// setup BbanFileHistory repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo<p_BbanFileHistory>(new List<p_BbanFileHistory>(), _bbanfileHistory, 1));
            //// setup BbanFile repository
            //_listBBAN = new List<p_BbanImport>();
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo<p_BbanImport>(_listBBAN, _bbanImport, 0));

            //privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);

            //int bbanFileId = bbanfile.Save("bban_file_2016-01-01.csv", _csvFile);

            //Assert.AreEqual(0, bbanFileId);

        }

        [Test]
        public void Test_Save_Fail_File_Is_AlreadyUploaded()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(p => p.CommitChanges()).Returns(1);

            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(_listBbanFile, _bbanfileUploading, 1));
            //// setup BbanFileHistory repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo<p_BbanFileHistory>(new List<p_BbanFileHistory>(), _bbanfileHistory, 1));
            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo<p_BbanImport>(_listBBAN, _bbanImport, 3));

            //privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    bbanfile.Save("bban_file_2016-01-01.csv", _csvFile);
            //});
        }

        [Test]
        public void Test_Save_Fail_BbanSaveDuplicate()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(p => p.CommitChanges()).Returns(1);

            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), _bbanfileUploading, 1));
            //// setup BbanFileHistory repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo<p_BbanFileHistory>(new List<p_BbanFileHistory>(), _bbanfileHistory, 1));
            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo<p_BbanImport>(_listBBAN, _bbanImport, 0));

            //privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);

            //_csvFile = Encoding.UTF8.GetBytes("657757829\r\n657757829");
            //try
            //{
            //    bbanfile.Save("bban_file_2016-01-01.csv", _csvFile);
            //    Assert.Fail();
            //}
            //catch (IbanOperationException ex)
            //{
            //    Assert.AreEqual(MessageLibs.MSG_BBAN_FILE_DUPLICATE.Code, (ex as IbanOperationException).ErrorCode);
            //}
        }

        [Test]
        public void Test_Save_Fail_BbanDbDuplicate()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(p => p.CommitChanges()).Returns(1);

            //_bbanfileUploading.CurrentStatusHistoryId = null;
            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), _bbanfileUploading, 1));
            //// setup BbanFileHistory repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo<p_BbanFileHistory>(new List<p_BbanFileHistory>(), _bbanfileHistory, 1));
            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo<p_BbanImport>(_listBBAN, _bbanImport, 1));

            //privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);

            //try
            //{
            //    bbanfile.Save("bban_file_2016-01-01.csv", _csvFile);
            //    Assert.Fail();
            //}
            //catch (IbanOperationException ex)
            //{
            //    Assert.AreEqual(MessageLibs.MSG_ALREADY_EXIST_SYSTEM.Code, (ex as IbanOperationException).ErrorCode);
            //}
        }

        [Test]
        public void Test_Save_Fail_DbCannotUpdate()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(p => p.CommitChanges()).Throws(new DbUpdateException());

            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), _bbanfileUploading, 1));
            //// setup BbanFileHistory repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFileHistory>()).Throws(new DbUpdateException());
            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanImport>()).Throws(new DbUpdateException());

            //privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);

            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    bbanfile.Save("bban_file_2016-01-01.csv", _csvFile);
            //});
        }

        [Test]
        public void Test_Save_Fail_Execute()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(p => p.CommitChanges()).Throws(new Exception());

            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), _bbanfileUploading, 1));
            //// setup BbanFileHistory repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo<p_BbanFileHistory>(new List<p_BbanFileHistory>(), _bbanfileHistory, 1));
            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo<p_BbanImport>(_listBBAN, _bbanImport, 3));

            //privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);

            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    bbanfile.Save("bban_file_2016-01-01.csv", _csvFile);
            //});
        }

        [Test]
        public void Test_Save_Fail_FileName_IsNull()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //Assert.Throws<ArgumentNullException>(() =>
            //{
            //    int bbanFileId = bbanfile.Save("", _csvFile);
            //});
        }

        [Test]
        public void Test_Save_Fail_File_IsNull()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);


            //Assert.Throws<ArgumentNullException>(() =>
            //{
            //    _csvFile = null;
            //    int bbanFileId = bbanfile.Save("BBANFILE", _csvFile);
            //});
        }

        [Test]
        public void Test_Save_Fail_File_IsEmpty()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //Assert.Throws<ArgumentNullException>(() =>
            //{
            //    _csvFile = Encoding.UTF8.GetBytes("");
            //    int bbanFileId = bbanfile.Save("BBANFILE", _csvFile);
            //});
        }

        [Test]
        public void Test_Save_Fail_Disposed()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Test is already disposed.
            //bbanfile.Dispose();

            //Assert.Throws<ObjectDisposedException>(() =>
            //{
            //    int bbanFileId = bbanfile.Save("BBANFILE", _csvFile);
            //});
        }

        [Test]
        public void Test_Save_Fail_BBAN_Invalid_Zero()
        {
            Mock<IRepository<p_BbanFile>> mockRepoBbanFile = new Mock<IRepository<p_BbanFile>>();
            mockRepoBbanFile.Setup(p => p.GetQueryable(It.IsAny<Expression<Func<p_BbanFile, bool>>>(),
                                                       It.IsAny<Page<p_BbanFile>>(),
                                                       It.IsAny<Func<IQueryable<p_BbanFile>, IOrderedQueryable<p_BbanFile>>>(),
                                                       It.IsAny<string[]>()))
                            .Returns(new List<p_BbanFile>().AsQueryable());

            Mock<IEfUnitOfWork> uowMock = new Mock<IEfUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<p_BbanFile>()).Returns(mockRepoBbanFile.Object);

            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
            //privateObject.SetField("_unitOfWork", uowMock.Object);

            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    _csvFile = Encoding.UTF8.GetBytes("-1");
            //    int bbanFileId = bbanfile.Save("BBANFILE", _csvFile);
            //});
        }

        [Test]
        public void Test_Save_Fail_BBAN_Invalid_OnlyNumber()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //_csvFile = Encoding.UTF8.GetBytes("11032274A");
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    int bbanFileId = bbanfile.Save("BBANFILE", _csvFile);
            //});
        }

        [Test]
        public void Test_Save_Fail_BBAN_Invalid_RoundNumber()
        {
            Mock<IRepository<p_BbanFile>> mockRepoBbanFile = new Mock<IRepository<p_BbanFile>>();
            mockRepoBbanFile.Setup(p => p.GetQueryable(It.IsAny<Expression<Func<p_BbanFile, bool>>>(),
                                                       It.IsAny<Page<p_BbanFile>>(),
                                                       It.IsAny<Func<IQueryable<p_BbanFile>, IOrderedQueryable<p_BbanFile>>>(),
                                                       It.IsAny<string[]>()))
                            .Returns(new List<p_BbanFile>().AsQueryable());

            Mock<IEfUnitOfWork> uowMock = new Mock<IEfUnitOfWork>();
            uowMock.Setup(u => u.GetRepository<p_BbanFile>()).Returns(mockRepoBbanFile.Object);

            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);

            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
            //privateObject.SetField("_unitOfWork", uowMock.Object);

            //_csvFile = Encoding.UTF8.GetBytes("110322740");
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    int bbanFileId = bbanfile.Save("BBANFILE", _csvFile);
            //});
        }

        #endregion


        #region SendEmail
        [Test]
        public void Test_SendEmail_Success()
        {
            BbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);

            //var po = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
            //po.Invoke("SendEmail", "lek@kobkiat-it.com", "This is test message", "This is email subject");
        }

        [Test]
        public void Test_SendEmail_Fail_InvalidRecipient()
        {
            // load xml document.
            XDocument xdoc = XDocument.Parse(Resources.ConfigurationFile);

            BbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, xdoc.ToString());
            //var po = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    po.Invoke("SendEmail", "wrongEmail", "This is test message", "This is email subject");
            //});
        }

        [Test]
        public void Test_SendEmail_Fail_NullConfiguration()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                BbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, "");
            });
        }

        [Test]
        public void Test_SendEmail_Fail_InvalidSmtpConfiguration()
        {
            // load xml document.
            XDocument xdoc = XDocument.Parse(Resources.ConfigurationFile);
            // set EmailSmtpPort to null.
            var element = xdoc.Element("Setting").Element("EmailSetting").Element("EmailSmtpPort");
            element.Value = "";

            Assert.Throws<ArgumentNullException>(() =>
            {
                BbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, xdoc.ToString());
            });
        }

        [Test]
        public void Test_SendEmail_Fail_InvalidAccountConfiguration()
        {
            // load xml document.
            XDocument xdoc = XDocument.Parse(Resources.ConfigurationFile);
            // set EmailAccount to null.
            var element = xdoc.Element("Setting").Element("EmailSetting").Element("EmailAccount");
            element.Value = " ";

            Assert.Throws<ArgumentNullException>(() =>
            {
                BbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, xdoc.ToString());
            });
        }

        [Test]
        public void Test_SendEmail_Fail_InvalidPasswordConfiguration()
        {
            // load xml document.
            XDocument xdoc = XDocument.Parse(Resources.ConfigurationFile);
            // set EmailPassword to null.
            var element = xdoc.Element("Setting").Element("EmailSetting").Element("EmailPassword");
            element.Value = " ";

            Assert.Throws<ArgumentNullException>(() =>
            {
                BbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, xdoc.ToString());
            });
        }

        [Test]
        public void Test_SendEmail_Fail_InvalidComponentCodeConfiguration()
        {
            // load xml document.
            XDocument xdoc = XDocument.Parse(Resources.ConfigurationFile);
            // set MailManComponentCode to null.
            var element = xdoc.Element("Setting").Element("EmailSetting").Element("MailManComponentCode");
            element.Value = " ";

            Assert.Throws<ArgumentNullException>(() =>
            {
                BbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, xdoc.ToString());
            });
        }

        [Test]
        public void Test_SendEmail_Fail_InvalidEmailServerConfiguration()
        {
            // load xml document.
            XDocument xdoc = XDocument.Parse(Resources.ConfigurationFile);
            var element = xdoc.Element("Setting").Element("EmailSetting").Element("EmailServer");
            element.Value = " ";

            Assert.Throws<ArgumentNullException>(() =>
            {
                BbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, xdoc.ToString());
            });
        }

        #endregion


        #region ValidateEmailSetting

        [Test]
        public void Test_ValidateEmailSetting_Fail_EmptySetting()
        {
            // load xml document.
            XDocument xdoc = XDocument.Parse(Resources.ConfigurationFile);

            BbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, xdoc.ToString());
            //var po = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
            //// set mail setting to null.
            //po.SetField("_mailSetting", null);
            //// call for validate email setting.
            //Assert.Throws<ArgumentNullException>(() =>
            //{
            //    po.Invoke("ValidateEmailSetting");
            //});
        }

        #endregion


        #region ApproveBbanFile

        [Test]
        public void Test_Approve_Success()
        {
            // Creates BbanFile instance.
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock p_Iban repository
            //Mock<IRepository<p_BbanFile>> mockRepoBbanFile = new Mock<IRepository<p_BbanFile>>();
            //mockRepoBbanFile.SetupSequence(p => p.GetQueryable(It.IsAny<Expression<Func<p_BbanFile, bool>>>(),
            //                          It.IsAny<Page<p_BbanFile>>(),
            //                          It.IsAny<Func<IQueryable<p_BbanFile>, IOrderedQueryable<p_BbanFile>>>(),
            //                          It.IsAny<string[]>()))
            //                .Returns(_listBbanFile.AsParallel().AsQueryable());

            //_bbanfileUploading.CurrentStatusHistoryId = (int)p_EnumBbanFileStatus.WaitingForApproval;
            //mockRepoBbanFile.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //                .Returns(_bbanfileUploading)
            //                .Returns(_bbanfileUploading)
            //                .Returns(_bbanfileUploading);

            //// Mock p_IbanHistory repository
            //Mock<IRepository<p_BbanFileHistory>> mockRepoBbanHistory = new Mock<IRepository<p_BbanFileHistory>>();
            //mockRepoBbanHistory.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //                    .Returns(new p_BbanFileHistory
            //                    {
            //                        HistoryId = 1,
            //                        BbanFileStatusId = p_EnumBbanFileStatus.WaitingForApproval
            //                    })
            //                    .Returns(new p_BbanFileHistory
            //                    {
            //                        HistoryId = 1,
            //                        BbanFileStatusId = p_EnumBbanFileStatus.Approved
            //                    })
            //                    .Returns(new p_BbanFileHistory
            //                    {
            //                        HistoryId = 1,
            //                        BbanFileStatusId = p_EnumBbanFileStatus.Importing
            //                    });

            //Mock<IRepository<p_Iban>> mockRepoIban = new Mock<IRepository<p_Iban>>();
            //mockRepoIban.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //            .Returns(_iban)
            //            .Returns(_iban)
            //            .Returns(_iban);

            //mockRepoIban.SetupSequence(p => p.GetQueryable(It.IsAny<Expression<Func<p_Iban, bool>>>(),
            //                          It.IsAny<Page<p_Iban>>(),
            //                          It.IsAny<Func<IQueryable<p_Iban>, IOrderedQueryable<p_Iban>>>(),
            //                          It.IsAny<string[]>()))
            //                .Returns(_listIBAN.AsParallel().AsQueryable());

            //Mock<IRepository<p_IbanHistory>> mockRepoIbanHistory = new Mock<IRepository<p_IbanHistory>>();
            //mockRepoIbanHistory.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //                   .Returns(_ibanHistory)
            //                   .Returns(_ibanHistory)
            //                   .Returns(_ibanHistory);

            //// Mock p_IbanHistory repository
            //Mock<IRepository<p_BbanImport>> mockRepoBbanImport = new Mock<IRepository<p_BbanImport>>();
            //mockRepoBbanImport.SetupSequence(p => p.GetQueryable(It.IsAny<Expression<Func<p_BbanImport, bool>>>(),
            //                          It.IsAny<Page<p_BbanImport>>(),
            //                          It.IsAny<Func<IQueryable<p_BbanImport>, IOrderedQueryable<p_BbanImport>>>(),
            //                          It.IsAny<string[]>()))
            //                .Returns(_listBBAN.AsQueryable())
            //                .Returns(_listBBAN.AsQueryable());

            //mockRepoBbanImport.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //                .Returns(new p_BbanImport
            //                {
            //                    BbanFileId = 1,
            //                    BbanImportId = 1,
            //                    IsImported = false,
            //                });

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(m => m.CommitChanges()).Returns(1);
            //mockIEfUnitOfWork.Setup(m => m.GetRepository<p_BbanFile>()).Returns(mockRepoBbanFile.Object);
            //mockIEfUnitOfWork.Setup(m => m.GetRepository<p_BbanFileHistory>()).Returns(mockRepoBbanHistory.Object);
            //mockIEfUnitOfWork.Setup(m => m.GetRepository<p_BbanImport>()).Returns(mockRepoBbanImport.Object);
            //mockIEfUnitOfWork.Setup(m => m.GetRepository<p_Iban>()).Returns(mockRepoIban.Object);
            //mockIEfUnitOfWork.Setup(m => m.GetRepository<p_IbanHistory>()).Returns(mockRepoIbanHistory.Object);

            //privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);

            //// Calls method tp Approve BBAN file and specific id "3" that we alredy initialize.
            //bbanfile.Approve(3);
        }

        [Test]
        public void Test_Approve_Fail_Disposed()
        {
            // Creates BbanFile instance.
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);

            // Calls dispose to make test case.
            bbanfile.Dispose();

            // Calls method tp Approve BBAN file and specific id "3" that we alredy initialize.
            Assert.Throws<ObjectDisposedException>(() =>
            {
                bbanfile.Approve(3);
            });
        }

        [Test]
        public void Test_Approve_Fail_Invalid_ID()
        {
            // Creates BbanFile instance.
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);

            // Calls method tp Approve BBAN file.
            Assert.Throws<ArgumentException>(() =>
            {
                bbanfile.Approve(0);
            });
        }

        [Test]
        public void Test_Approve_Fail_Invalid_Approver()
        {
            // Creates BbanFile instance.
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock p_IbanHistory repository
            //Mock<IRepository<p_BbanFileHistory>> mockRepoBbanHistory = new Mock<IRepository<p_BbanFileHistory>>();
            //mockRepoBbanHistory.SetupSequence(p => p.GetQueryable(It.IsAny<Expression<Func<p_BbanFileHistory, bool>>>(),
            //                          It.IsAny<Page<p_BbanFileHistory>>(),
            //                          It.IsAny<Func<IQueryable<p_BbanFileHistory>, IOrderedQueryable<p_BbanFileHistory>>>(),
            //                          It.IsAny<string[]>()))
            //        .Returns(_listBbanFileHistory.AsParallel().AsQueryable());

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(m => m.CommitChanges()).Returns(1);
            //mockIEfUnitOfWork.Setup(m => m.GetRepository<p_BbanFileHistory>()).Returns(mockRepoBbanHistory.Object);

            //privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);
            //// Calls method tp Approve BBAN file.
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    bbanfile.Approve(1);
            //});
        }

        [Test]
        public void Test_Approve_Fail_BbanFileNotFound()
        {
            //Creates BbanFile instance.
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            _listBbanFileHistory = new List<p_BbanFileHistory>();
            //Mock p_IbanHistory repository
            Mock<IRepository<p_BbanFileHistory>> mockRepoBbanHistory = new Mock<IRepository<p_BbanFileHistory>>();
            mockRepoBbanHistory.SetupSequence(p => p.GetQueryable(It.IsAny<Expression<Func<p_BbanFileHistory, bool>>>(),
                                      It.IsAny<Page<p_BbanFileHistory>>(),
                                      It.IsAny<Func<IQueryable<p_BbanFileHistory>, IOrderedQueryable<p_BbanFileHistory>>>(),
                                      It.IsAny<string[]>()))
                               .Returns(_listBbanFileHistory.AsParallel().AsQueryable());

            //Mock unit of work
            Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            mockIEfUnitOfWork.Setup(m => m.CommitChanges()).Returns(1);
            mockIEfUnitOfWork.Setup(m => m.GetRepository<p_BbanFileHistory>()).Returns(mockRepoBbanHistory.Object);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
            //privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);
            ////Calls method tp Approve BBAN file.
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    bbanfile.Approve(1);
            //});
        }

        [Test]
        public void Test_Approve_Fail_BbanFileNotFound2()
        {
            // Creates BbanFile instance.
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock p_Iban repository
            //Mock<IRepository<p_BbanFile>> mockRepoBbanFile = new Mock<IRepository<p_BbanFile>>();
            //mockRepoBbanFile.SetupSequence(p => p.GetQueryable(It.IsAny<Expression<Func<p_BbanFile, bool>>>(),
            //                          It.IsAny<Page<p_BbanFile>>(),
            //                          It.IsAny<Func<IQueryable<p_BbanFile>, IOrderedQueryable<p_BbanFile>>>(),
            //                          It.IsAny<string[]>()))
            //                .Returns(_listBbanFile.AsParallel().AsQueryable());

            //_bbanfileUploading.CurrentStatusHistoryId = (int)p_EnumBbanFileStatus.WaitingForApproval;
            //mockRepoBbanFile.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //        .Returns(_bbanfileUploading)
            //        .Returns(_bbanfileUploading)
            //        .Returns(_bbanfileUploading);

            //// Mock p_IbanHistory repository
            //Mock<IRepository<p_BbanFileHistory>> mockRepoBbanHistory = new Mock<IRepository<p_BbanFileHistory>>();
            //mockRepoBbanHistory.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //                    .Returns(new p_BbanFileHistory
            //                    {
            //                        HistoryId = 1,
            //                        BbanFileStatusId = p_EnumBbanFileStatus.WaitingForApproval
            //                    })
            //                    .Returns(new p_BbanFileHistory
            //                    {
            //                        HistoryId = 1,
            //                        BbanFileStatusId = p_EnumBbanFileStatus.Approved
            //                    })
            //                    .Returns(new p_BbanFileHistory
            //                    {
            //                        HistoryId = 1,
            //                        BbanFileStatusId = p_EnumBbanFileStatus.Importing
            //                    });

            //Mock<IRepository<p_Iban>> mockRepoIban = new Mock<IRepository<p_Iban>>();
            //mockRepoIban.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //            .Returns(_iban)
            //            .Returns(_iban)
            //            .Returns(_iban);

            //Mock<IRepository<p_IbanHistory>> mockRepoIbanHistory = new Mock<IRepository<p_IbanHistory>>();
            //mockRepoIbanHistory.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //                   .Returns(_ibanHistory)
            //                   .Returns(_ibanHistory)
            //                   .Returns(_ibanHistory);

            //// Mock p_IbanHistory repository
            //Mock<IRepository<p_BbanImport>> mockRepoBbanImport = new Mock<IRepository<p_BbanImport>>();
            //mockRepoBbanImport.SetupSequence(p => p.GetQueryable(It.IsAny<Expression<Func<p_BbanImport, bool>>>(),
            //                          It.IsAny<Page<p_BbanImport>>(),
            //                          It.IsAny<Func<IQueryable<p_BbanImport>, IOrderedQueryable<p_BbanImport>>>(),
            //                          It.IsAny<string[]>()))
            //                .Returns(_listBBAN.AsQueryable())
            //                .Returns(new List<p_BbanImport>().AsQueryable());

            //mockRepoBbanImport.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //                .Returns(new p_BbanImport
            //                {
            //                    BbanFileId = 1,
            //                    BbanImportId = 1,
            //                    IsImported = false,
            //                });

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(m => m.CommitChanges()).Returns(1);
            //mockIEfUnitOfWork.Setup(m => m.GetRepository<p_BbanFile>()).Returns(mockRepoBbanFile.Object);
            //mockIEfUnitOfWork.Setup(m => m.GetRepository<p_BbanFileHistory>()).Returns(mockRepoBbanHistory.Object);
            //mockIEfUnitOfWork.Setup(m => m.GetRepository<p_BbanImport>()).Returns(mockRepoBbanImport.Object);
            //mockIEfUnitOfWork.Setup(m => m.GetRepository<p_Iban>()).Returns(mockRepoIban.Object);
            //mockIEfUnitOfWork.Setup(m => m.GetRepository<p_IbanHistory>()).Returns(mockRepoIbanHistory.Object);

            //privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);

            //try
            //{
            //    bbanfile.Approve(3);
            //    // if come to this line it's mean failed.
            //    Assert.Fail();
            //}
            //catch (IbanOperationException ex)
            //{
            //    Assert.IsNotNull(ex.InnerException);
            //    Assert.IsInstanceOf(typeof(InvalidOperationException), ex.InnerException);
            //}
        }

        [Test]
        public void Test_Approve_Fail_Execute()
        {
            // Creates BbanFile instance.
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var po = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //p_BbanFile pBbanFile = new p_BbanFile
            //{
            //    BbanFileId = 1,
            //    CurrentStatusHistoryId = 1,
            //    Hash = "12345678efrtgyu",
            //    Name = "file name",
            //    RawFile = new byte[10]
            //};
            //p_BbanFileHistory pBbanHistory = new p_BbanFileHistory
            //{
            //    HistoryId = 1,
            //    BbanFileId = 1,
            //    // Approve BBAN file the current should be WaitingForApproval.
            //    BbanFileStatusId = p_EnumBbanFileStatus.WaitingForApproval,
            //};

            //// Mock unit of work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //// Test in throw excpetion case 
            //mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Throws(new Exception());
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), pBbanFile, 1));
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo<p_BbanFileHistory>(new List<p_BbanFileHistory>(), pBbanHistory, 1));
            //po.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

            //// Calls method tp Approve BBAN file.
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    bbanfile.Approve(1);
            //});
        }

        [Test]
        public void Test_Approve_Fail_Execute_2()
        {
            // Creates BbanFile instance.
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var po = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //p_BbanFile pBbanFile = new p_BbanFile
            //{
            //    BbanFileId = 1,
            //    CurrentStatusHistoryId = 1,
            //    Hash = "12345678efrtgyu",
            //    Name = "file name",
            //    RawFile = new byte[10]
            //};
            //p_BbanFileHistory pBbanHistory = new p_BbanFileHistory
            //{
            //    HistoryId = 1,
            //    BbanFileId = 1,
            //    // Approve BBAN file the current should be WaitingForApproval.
            //    BbanFileStatusId = p_EnumBbanFileStatus.WaitingForApproval,
            //};

            //// Mock unit of work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //// Test in throw excpetion case IBanOperationException
            //mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Throws(new IbanOperationException(100));
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), pBbanFile, 1));
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo<p_BbanFileHistory>(new List<p_BbanFileHistory>(), pBbanHistory, 1));
            //po.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

            //// Calls method tp Approve BBAN file.
            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    bbanfile.Approve(1);
            //});
        }

        #endregion


        #region GetBbanFiles

        [Test]
        public void Test_GetBbanFiles_Success()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);

            //// Mock Bban repository
            //var mockEf = new MockEfRepository<p_BbanFile>();
            //var mockEfPrivateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(mockEf);

            //// Mock DbSet 
            //var mockDbSet = new MockDbSet<p_BbanFile>();
            //mockDbSet.Local = MockDataTest.GenerateBbanFiles();
            //mockEfPrivateObject.SetField("_dbSet", mockDbSet);

            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFile>()).Returns(mockEf);
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo(MockDataTest.GenerateBbanFilesHistory(), MockDataTest.GenerateBbanFilesHistory().FirstOrDefault(), MockDataTest.GenerateBbanFilesHistory().Count));
            //privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

            //int total;
            //IEnumerable<BbanFile> result = bbanfile.Get(0, 10, out total);
            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.FirstOrDefault().Name, MockDataTest.GenerateBbanFiles().FirstOrDefault().Name);

            //// with status.
            //result = bbanfile.Get(0, 10, out total, status: "Uploaded");
            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.FirstOrDefault().Name, MockDataTest.GenerateBbanFiles().FirstOrDefault().Name);
        }

        [Test]
        public void Test_GetBbanFiles_Success_WithStatus()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);

            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo(MockDataTest.GenerateBbanFiles(), MockDataTest.GenerateBbanFiles().FirstOrDefault(), MockDataTest.GenerateBbanFiles().Count));
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo(MockDataTest.GenerateBbanFilesHistory(), MockDataTest.GenerateBbanFilesHistory().FirstOrDefault(), MockDataTest.GenerateBbanFilesHistory().Count));
            //privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

            //int total;
            //IEnumerable<BbanFile> result = bbanfile.Get(0, 10, out total, 1, "Uploaded");
            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.FirstOrDefault().Name, MockDataTest.GenerateBbanFiles().FirstOrDefault().Name);

        }

        [Test]
        public void Test_GetBbanFiles_Success_WithIbanId()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);

            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo(MockDataTest.GenerateBbanFiles(), MockDataTest.GenerateBbanFiles().FirstOrDefault(), MockDataTest.GenerateBbanFiles().Count));
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo(MockDataTest.GenerateBbanFilesHistory(), MockDataTest.GenerateBbanFilesHistory().FirstOrDefault(), MockDataTest.GenerateBbanFilesHistory().Count));
            //privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

            //int total;

            //// with ibanId.
            //IEnumerable<BbanFile> result = bbanfile.Get(0, 10, out total, 1);
            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.FirstOrDefault().Name, MockDataTest.GenerateBbanFiles().FirstOrDefault().Name);
        }

        [Test]
        public void Test_GetBbanFiles_Fail_Crashed()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);

            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFile>()).Throws(new Exception());
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo(MockDataTest.GenerateBbanFilesHistory(), MockDataTest.GenerateBbanFilesHistory().FirstOrDefault(), MockDataTest.GenerateBbanFilesHistory().Count));
            //privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

            //int total;

            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    bbanfile.Get(0, 10, out total, 1);
            //});
        }

        [Test]
        public void Test_GetBbanFiles_Fail_InvalidStatus()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);

            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo(MockDataTest.GenerateBbanFiles(), MockDataTest.GenerateBbanFiles().FirstOrDefault(), MockDataTest.GenerateBbanFiles().Count));
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo(MockDataTest.GenerateBbanFilesHistory(), MockDataTest.GenerateBbanFilesHistory().FirstOrDefault(), MockDataTest.GenerateBbanFilesHistory().Count));
            //privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

            //Assert.Throws<ArgumentException>(() =>
            //{
            //    int total;
            //    bbanfile.Get(0, 10, out total, 1, status: "true");
            //});
        }

        [Test]
        public void Test_GetBbanFiles_Fail_InvalidStatus2()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);

            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo(MockDataTest.GenerateBbanFiles(), MockDataTest.GenerateBbanFiles().FirstOrDefault(), MockDataTest.GenerateBbanFiles().Count));
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo(MockDataTest.GenerateBbanFilesHistory(), MockDataTest.GenerateBbanFilesHistory().FirstOrDefault(), MockDataTest.GenerateBbanFilesHistory().Count));
            //privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

            //Assert.Throws<ArgumentException>(() =>
            //{
            //    int total;
            //    IEnumerable<BbanFile> result = bbanfile.Get(0, 10, out total, status: "InvalidStatus");
            //});
        }

        [Test]
        public void Test_GetBbanFiles_Fail_Disposed()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);

            // Calls to test this dispose case.
            bbanfile.Dispose();

            Assert.Throws<ObjectDisposedException>(() =>
            {
                int total;
                IEnumerable<BbanFile> result = bbanfile.Get(0, 10, out total);
            });
        }

        #endregion


        #region GetBbans
        [Test]
        public void Test_GetBbans_Success()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
            //int bbanFileId = 1;
            //int total;

            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(MockDataTest.GenerateBban().Count);

            //// Mock BbanImport repository
            //var mockEf = new MockEfRepository<p_BbanImport>();
            //var mockEfPrivateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(mockEf);
            //mockIEfUnitOfWork.Setup(m => m.GetRepository<p_BbanImport>()).Returns(mockEf);

            //// Mock DbSet
            //var mockDbSet = new MockDbSet<p_BbanImport>();
            //mockDbSet.Local = MockDataTest.GenerateBbanImport();
            //mockEfPrivateObject.SetField("_dbSet", mockDbSet);

            //privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

            //IEnumerable<Bban> result = bbanfile.GetBbans(0, 5, out total, bbanFileId);
            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.FirstOrDefault().BbanCode, MockDataTest.GenerateBban().FirstOrDefault().BbanCode);
        }

        [Test]
        public void Test_GetBbans_Fail_Disposed()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            // Calls to test this dispose case.
            bbanfile.Dispose();

            Assert.Throws<ObjectDisposedException>(() =>
            {
                int a = 1;
                int b;
                bbanfile.GetBbans(1, 1, out b, a);
            });
        }

        #endregion


        #region GetBbanFileStatus
        [Test]
        public void Test_GetBbanFileStatus_Success()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            Assert.IsNotNull(bbanfile.GetStatus());
        }

        [Test]
        public void Test_GetBbanFileStatus_Fail_Disposed()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            // Calls to test this dispose case.
            bbanfile.Dispose();

            Assert.Throws<ObjectDisposedException>(() =>
            {
                bbanfile.GetStatus();
            });
        }
        #endregion


        #region GetBbanFileHistory

        [Test]
        public void Test_GetBbanFileHistory_Success()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
            //int bbanFileId = 1;

            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(MockDataTest.GenerateBbanFilesHistory().Count);

            //// setup BbanFileHistory repository
            //mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo(MockDataTest.GenerateBbanFilesHistory(), MockDataTest.GenerateBbanFilesHistory().FirstOrDefault(), MockDataTest.GenerateBbanFilesHistory().Count));
            //privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

            //IEnumerable<BbanFileHistory> result = bbanfile.GetHistory(bbanFileId);
            //Assert.IsNotNull(result);
            //Assert.AreEqual(MockDataTest.GenerateBbanFilesHistory().Count, result.Count());
        }

        [Test]
        public void Test_GetBbanFileHistory_Fail_Disposed()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            // Calls to test this dispose case.
            bbanfile.Dispose();

            Assert.Throws<ObjectDisposedException>(() =>
            {
                bbanfile.GetHistory(1);
            });
        }
        #endregion


        #region GetIbans

        [Test]
        public void Test_GetIbans_Success()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //// Init parameter(s)
            //int bbanFileId = 1, total = 0;
            //int? offset = 0, limit = 5;

            //// Expected value(s) from mocked repositories
            //IQueryable<p_Iban> expectedIbanQueryable = new List<p_Iban>() { _iban }.AsQueryable();
            //int expectedIbanCount = expectedIbanQueryable.Count(); // this is the count of IbanList that has A HISTORY. So, it's will be the number of result of [GetQueryable]
            //p_IbanHistory expectedIbanHistory = _ibanHistory;

            //// Mock Iban repository
            //var mockEf = new MockEfRepository<p_Iban>();
            //var mockEfPrivateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(mockEf);
            //mockEfPrivateObject.SetField("_dbSet", new MockDbSet<p_Iban>());

            //// Mock IbanHistory repository
            //Mock<IRepository<p_IbanHistory>> mockRepoIbanHistory = new Mock<IRepository<p_IbanHistory>>();
            //mockRepoIbanHistory.Setup(p => p.GetById(It.IsAny<object>()))
            //                   .Returns(expectedIbanHistory);

            //// Mock unit of work
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(m => m.GetRepository<p_Iban>()).Returns(mockEf);
            //mockIEfUnitOfWork.Setup(m => m.GetRepository<p_IbanHistory>()).Returns(mockRepoIbanHistory.Object);
            //privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

            //IEnumerable<Iban> result = bbanfile.GetIbans(bbanFileId, offset, limit, out total);
            //Assert.IsNotNull(result);
        }

        [Test]
        public void Test_GetIbans_Fail_CannotRetrieveData()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
            //int bbanFileId = 1;
            //int total;
            //// Mock Unit of Work.
            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(MockDataTest.GenerateBban().Count);

            //// setup Iban repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_Iban>()).Returns(MockRepository.MockRepo<p_Iban>(_listIBAN, _iban, 1));
            //privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

            //Assert.Throws<IbanOperationException>(() =>
            //{
            //    bbanfile.GetIbans(bbanFileId, 0, 5, out total);
            //});
        }

        [Test]
        public void Test_GetIbans_Fail_Disposed()
        {
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
            //int bbanFileId = 1;
            //int total;
            //bbanfile.Dispose();
            //Assert.Throws<ObjectDisposedException>(() =>
            //{
            //    bbanfile.GetIbans(bbanFileId, 0, 5, out total);
            //});
        }

        #endregion


        #region VerifyBbanExist

        [Test]
        public void Test_VerifyBbanExist_Success()
        {
            // Creates BbanFile instance.
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            //var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

            //Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
            //mockIEfUnitOfWork.Setup(p => p.CommitChanges()).Returns(1);

            //// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), _bbanfileUploading, 1));
            //// Mock p_IbanHistory repository
            //Mock<IRepository<p_BbanFileHistory>> mockRepoBbanHistory = new Mock<IRepository<p_BbanFileHistory>>();
            //mockRepoBbanHistory.SetupSequence(p => p.GetById(It.IsAny<object>()))
            //                    .Returns(new p_BbanFileHistory
            //                    {
            //                        HistoryId = 1,
            //                        BbanFileStatusId = p_EnumBbanFileStatus.Uploaded
            //                    })
            //                    .Returns(new p_BbanFileHistory
            //                    {
            //                        HistoryId = 1,
            //                        BbanFileStatusId = p_EnumBbanFileStatus.Verifying
            //                    });
            ////// setup BbanFile repository
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo<p_BbanImport>(_listBBAN, _bbanImport, 1));
            //_listIBAN = new List<p_Iban>();
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_Iban>()).Returns(MockRepository.MockRepo<p_Iban>(_listIBAN, _iban, 1));
            //mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFileHistory>()).Returns(mockRepoBbanHistory.Object);
            //privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);
            //privateObject.Invoke("VerifyBbanExist", 1);
        }

        //[Test]
        //public void Test_VerifyBbanExist_Fail_DuplicateIban()
        //{
        //    // Creates BbanFile instance.
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockIEfUnitOfWork.Setup(p => p.CommitChanges()).Returns(1);

        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), _bbanfileUploading, 1));
        //    // Mock p_IbanHistory repository
        //    Mock<IRepository<p_BbanFileHistory>> mockRepoBbanHistory = new Mock<IRepository<p_BbanFileHistory>>();
        //    mockRepoBbanHistory.SetupSequence(p => p.GetById(It.IsAny<object>()))
        //                        .Returns(new p_BbanFileHistory
        //                        {
        //                            HistoryId = 1,
        //                            BbanFileStatusId = p_EnumBbanFileStatus.Uploaded
        //                        })
        //                        .Returns(new p_BbanFileHistory
        //                        {
        //                            HistoryId = 1,
        //                            BbanFileStatusId = p_EnumBbanFileStatus.Verifying
        //                        });
        //    //// setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo<p_BbanImport>(_listBBAN, _bbanImport, 1));
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_Iban>()).Returns(MockRepository.MockRepo<p_Iban>(_listIBAN, _iban, 1));
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFileHistory>()).Returns(mockRepoBbanHistory.Object);
        //    privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);

        //    Assert.Throws<IbanOperationException>(() =>
        //    {
        //        privateObject.Invoke("VerifyBbanExist", 1);
        //    });
        //}

        //[Test]
        //public void Test_VerifyBbanExist_Fail_Crashed()
        //{
        //    // Creates BbanFile instance.
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockIEfUnitOfWork.Setup(p => p.CommitChanges()).Returns(1);

        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), _bbanfileUploading, 1));
        //    // Mock p_IbanHistory repository
        //    Mock<IRepository<p_BbanFileHistory>> mockRepoBbanHistory = new Mock<IRepository<p_BbanFileHistory>>();
        //    mockRepoBbanHistory.SetupSequence(p => p.GetById(It.IsAny<object>()))
        //                        .Returns(new p_BbanFileHistory
        //                        {
        //                            HistoryId = 1,
        //                            BbanFileStatusId = p_EnumBbanFileStatus.Uploaded
        //                        })
        //                        .Throws(new Exception());
        //    //// setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo<p_BbanImport>(_listBBAN, _bbanImport, 1));
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_Iban>()).Returns(MockRepository.MockRepo<p_Iban>(_listIBAN, _iban, 1));
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFileHistory>()).Returns(mockRepoBbanHistory.Object);
        //    privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);

        //    Assert.Throws<IbanOperationException>(() =>
        //    {
        //        privateObject.Invoke("VerifyBbanExist", 1);
        //    });
        //}

        //[Test]
        //public void Test_VerifyBbanExist_Fail_Disposed()
        //{
        //    // Creates BbanFile instance.
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
        //    int bbanFileId = 1;

        //    // Calls to check dispose case.
        //    bbanfile.Dispose();

        //    Assert.Throws<ObjectDisposedException>(() =>
        //    {
        //        bbanfile.VerifyBbanExist(bbanFileId);
        //    });
        //}

        //[Test]
        //public void Test_VerifyBbanExist_Fail_BbanFileId_IsZero()
        //{
        //    // Creates BbanFile instance.
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
        //    int bbanFileId = 0;

        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //        bbanfile.VerifyBbanExist(bbanFileId);
        //    });
        //}

        //[Test]
        //public void Test_VerifyBbanExist_Fail_BBan_Exist_()
        //{
        //    // Creates BbanFile instance.
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
        //    int bbanFileId = 0;

        //    // Mock Unit of work.
        //    Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);

        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //        bbanfile.VerifyBbanExist(bbanFileId);
        //    });
        //}
        #endregion


        #region SendToApprove

        //[Test]
        //public void Test_SendToApprove_Success()
        //{
        //    // Creates BbanFile instance.
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    // Mock Unit of Work.
        //    Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);

        //    p_BbanFile pBbanFile = new p_BbanFile
        //    {
        //        BbanFileId = 1,
        //        // In case send to approve the current status should be Verified.
        //        CurrentStatusHistoryId = 1,
        //        Hash = "1234567ertgyhuj",
        //        Name = "file name",
        //        RawFile = new byte[10]
        //    };

        //    p_BbanFileHistory pBbanHistory = new p_BbanFileHistory
        //    {
        //        HistoryId = 1,
        //        BbanFileId = 1,
        //        // In case send to approve the current status should be Verified.
        //        BbanFileStatusId = p_EnumBbanFileStatus.Verified,
        //        ChangedBy = "admin",
        //        ChangedDate = DateTime.Now,
        //        Context = "client",
        //        Remark = "remark"
        //    };

        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo(MockDataTest.GenerateBbanFiles(), pBbanFile, MockDataTest.GenerateBbanFiles().Count));
        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo(MockDataTest.GenerateBbanFilesHistory(), pBbanHistory, MockDataTest.GenerateBbanFilesHistory().Count));
        //    privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

        //    bbanfile.SendToApprove(1, "lek@kobkiat-it.com", "This is send to approve message");
        //}

        //[Test]
        //public void Test_SendToApprove_Fail_BbanFileNotFound()
        //{
        //    // Creates BbanFile instance.
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    // Mock Unit of Work.
        //    Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);

        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo(new List<p_BbanFile>(), MockDataTest.GenerateBbanFiles().FirstOrDefault(), MockDataTest.GenerateBbanFiles().Count));
        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo(new List<p_BbanFileHistory>(), MockDataTest.GenerateBbanFilesHistory()[2], MockDataTest.GenerateBbanFilesHistory().Count));
        //    privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

        //    Assert.Throws<IbanOperationException>(() =>
        //    {
        //        bbanfile.SendToApprove(1, "lek@kobkiat-it.com", "This is send to approve message");
        //    });
        //}

        //[Test]
        //public void Test_SendToApprove_Fail_CannotFindReadyToApprove()
        //{
        //    // Creates BbanFile instance.
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    // Mock Unit of Work.
        //    Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);

        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo(new List<p_BbanFile>(), MockDataTest.GenerateBbanFiles().FirstOrDefault(), MockDataTest.GenerateBbanFiles().Count));
        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo(MockDataTest.GenerateBbanFilesHistory(), MockDataTest.GenerateBbanFilesHistory()[2], MockDataTest.GenerateBbanFilesHistory().Count));
        //    privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

        //    Assert.Throws<IbanOperationException>(() =>
        //    {
        //        bbanfile.SendToApprove(1, "lek@kobkiat-it.com", "This is send to approve message");
        //    });
        //}

        [Test]
        public void Test_SendToApprove_Fail_Disposed()
        {
            // Creates BbanFile instance.
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            bbanfile.Dispose();
            Assert.Throws<ObjectDisposedException>(() =>
            {
                bbanfile.SendToApprove(1, "lek@kobkiat-it.com", "This is send to approve message");
            });
        }

        [Test]
        public void Test_SendToApprove_Fail_InvalidId()
        {
            // Creates BbanFile instance.
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            Assert.Throws<ArgumentException>(() =>
            {
                bbanfile.SendToApprove(0, "lek@kobkiat-it.com", "This is send to approve message");
            });
        }

        [Test]
        public void Test_SendToApprove_Fail_InvalidEmailReceiver()
        {
            // Creates BbanFile instance.
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            Assert.Throws<ArgumentNullException>(() =>
            {
                bbanfile.SendToApprove(1, "", "This is send to approve message");
            });
        }

        [Test]
        public void Test_SendToApprove_Fail_InvalidMessage()
        {
            // Creates BbanFile instance.
            IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
            Assert.Throws<ArgumentNullException>(() =>
            {
                bbanfile.SendToApprove(1, "lek@kobkiat-it.com", "");
            });
        }
        #endregion


        #region DenyBbanFile
        //[Test]
        //public void Test_DenyBbanFile_Success()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
        //    int bbanfileId = 1;
        //    string remark = "remark for denie";

        //    // Mock BBAN file poco.
        //    p_BbanFile pBbanfile = new p_BbanFile()
        //    {
        //        BbanFileId = 1,
        //        CurrentStatusHistoryId = 1,
        //        Hash = "1234567890frtgyhjkl;",
        //        Name = "BBAN file name",
        //        RawFile = new byte[10]
        //    };
        //    p_BbanFileHistory pBbanHistory = new p_BbanFileHistory()
        //    {
        //        BbanFileId = 1,
        //        BbanFileStatusId = p_EnumBbanFileStatus.WaitingForApproval,
        //        ChangedBy = "admin",
        //        ChangedDate = DateTime.Now,
        //        Context = "Client",
        //        HistoryId = 1,
        //        Remark = "remark"
        //    };
        //    List<p_BbanImport> listBbanImport = new List<p_BbanImport> 
        //    {
        //        new p_BbanImport
        //        {
        //            BbanImportId=1,
        //            BbanFileId = 1,
        //            IsImported = true                    
        //        },
        //        new p_BbanImport
        //        {
        //            BbanImportId=2,
        //            BbanFileId = 1,
        //            IsImported = true                    
        //        }
        //    };
        //    // Mock IEfUnitOfWork.
        //    Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);
        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo(new List<p_BbanFile>(), pBbanfile, 1));
        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo(listBbanImport, _bbanImport, 2));
        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo(new List<p_BbanFileHistory>(), pBbanHistory, 1));

        //    // Inject mock to private field.
        //    privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

        //    // Calls method.
        //    bbanfile.Deny(bbanfileId, remark);
        //}

        //[Test]
        //public void Test_DenyBbanFile_Fail_Disposed()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
        //    int bbanfileId = 1;
        //    string remark = "remark for denie";

        //    bbanfile.Dispose();

        //    // Calls method.
        //    Assert.Throws<ObjectDisposedException>(() =>
        //    {
        //        bbanfile.Deny(bbanfileId, remark);
        //    });
        //}

        //[Test]
        //public void Test_DenyBbanFile_Fail_Remark_IsNull()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
        //    int bbanfileId = 1;
        //    string remark = "";

        //    // Calls method.
        //    Assert.Throws<ArgumentNullException>(() =>
        //    {
        //        bbanfile.Deny(bbanfileId, remark);
        //    });
        //}

        //[Test]
        //public void Test_DenyBbanFile_Fail_BbanNotFound()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
        //    int bbanfileId = 1;
        //    string remark = "remark for denie";

        //    // Mock IEfUnitOfWork.
        //    Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Returns(1);
        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo(new List<p_BbanFile>(), null, 1));
        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo(_listBBAN, _bbanImport, 2));
        //    _bbanfileHistory.BbanFileStatusId = p_EnumBbanFileStatus.WaitingForApproval;

        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo(new List<p_BbanFileHistory>(), _bbanfileHistory, 1));

        //    // Inject mock to private field.
        //    privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

        //    // Calls method.
        //    Assert.Throws<IbanOperationException>(() =>
        //    {
        //        bbanfile.Deny(bbanfileId, remark);
        //    });
        //}

        //[Test]
        //public void Test_DenyBbanFile_Fail_Id_IsZero()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
        //    int bbanfileId = 0;
        //    string remark = "remark for denie";

        //    // Calls method.
        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //        bbanfile.Deny(bbanfileId, remark);
        //    });
        //}

        //[Test]
        //public void Test_DenyBbanFile_Fail_ExecuteError()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
        //    int bbanfileId = 1;
        //    string remark = "remark for denie";

        //    // Mock BBAN file poco.
        //    p_BbanFile pBbanfile = new p_BbanFile()
        //    {
        //        BbanFileId = 1,
        //        CurrentStatusHistoryId = 1,
        //        Hash = "1234567890frtgyhjkl;",
        //        Name = "BBAN file name",
        //        RawFile = new byte[10]
        //    };
        //    p_BbanFileHistory pBbanHistory = new p_BbanFileHistory()
        //    {
        //        BbanFileId = 1,
        //        BbanFileStatusId = p_EnumBbanFileStatus.WaitingForApproval,
        //        ChangedBy = "admin",
        //        ChangedDate = DateTime.Now,
        //        Context = "Client",
        //        HistoryId = 1,
        //        Remark = "remark"
        //    };
        //    // Mock IEfUnitOfWork.
        //    Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();

        //    // Mock execute error
        //    mockIEfUnitOfWork.Setup(s => s.CommitChanges()).Throws(new Exception());
        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo(new List<p_BbanFile>(), pBbanfile, 1));
        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo(new List<p_BbanImport>(), _bbanImport, 1));
        //    mockIEfUnitOfWork.Setup(s => s.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo(new List<p_BbanFileHistory>(), pBbanHistory, 1));

        //    // Inject mock to private field.
        //    privateObject.SetField("_unitOfWork", mockIEfUnitOfWork.Object);

        //    // Calls method.
        //    Assert.Throws<IbanOperationException>(() =>
        //    {
        //        bbanfile.Deny(bbanfileId, remark);
        //    });
        //}
        #endregion


        #region CheckBbanFileStatus

        //[Test]
        //public void Test_CheckBbanFileStatus_Success_Uploaded()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    p_EnumBbanFileStatus? currentStatus = null;
        //    p_EnumBbanFileStatus newStatus = p_EnumBbanFileStatus.Uploaded;

        //    bool result = (bool)privateObject.Invoke("CheckBbanFileStatus", currentStatus, newStatus);

        //    Assert.IsTrue(result);
        //}
        //[Test]
        //public void Test_CheckBbanFileStatus_Fail_Uploaded()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    // this status is wrong.
        //    p_EnumBbanFileStatus? currentStatus = p_EnumBbanFileStatus.Uploaded;
        //    p_EnumBbanFileStatus newStatus = p_EnumBbanFileStatus.Uploaded;

        //    bool result = (bool)privateObject.Invoke("CheckBbanFileStatus", currentStatus, newStatus);

        //    Assert.IsFalse(result);
        //}
        //[Test]
        //public void Test_CheckBbanFileStatus_Success_UploadCanceled()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    p_EnumBbanFileStatus? currentStatus = p_EnumBbanFileStatus.Verified;
        //    p_EnumBbanFileStatus newStatus = p_EnumBbanFileStatus.UploadCanceled;

        //    bool result = (bool)privateObject.Invoke("CheckBbanFileStatus", currentStatus, newStatus);

        //    Assert.IsTrue(result);
        //}

        //[Test]
        //public void Test_CheckBbanFileStatus_Success_Verifying()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    p_EnumBbanFileStatus? currentStatus = p_EnumBbanFileStatus.Uploaded;
        //    p_EnumBbanFileStatus newStatus = p_EnumBbanFileStatus.Verifying;

        //    bool result = (bool)privateObject.Invoke("CheckBbanFileStatus", currentStatus, newStatus);

        //    Assert.IsTrue(result);
        //}
        //[Test]
        //public void Test_CheckBbanFileStatus_Success_Verified()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    p_EnumBbanFileStatus? currentStatus = p_EnumBbanFileStatus.Verifying;
        //    p_EnumBbanFileStatus newStatus = p_EnumBbanFileStatus.Verified;

        //    bool result = (bool)privateObject.Invoke("CheckBbanFileStatus", currentStatus, newStatus);

        //    Assert.IsTrue(result);
        //}
        //[Test]
        //public void Test_CheckBbanFileStatus_Success_VerifiedFailed()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    p_EnumBbanFileStatus? currentStatus = p_EnumBbanFileStatus.Verifying;
        //    p_EnumBbanFileStatus newStatus = p_EnumBbanFileStatus.VerifiedFailed;

        //    bool result = (bool)privateObject.Invoke("CheckBbanFileStatus", currentStatus, newStatus);

        //    Assert.IsTrue(result);
        //}
        //[Test]
        //public void Test_CheckBbanFileStatus_Success_WaitingForApproval()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    p_EnumBbanFileStatus? currentStatus = p_EnumBbanFileStatus.Verified;
        //    p_EnumBbanFileStatus newStatus = p_EnumBbanFileStatus.WaitingForApproval;

        //    bool result = (bool)privateObject.Invoke("CheckBbanFileStatus", currentStatus, newStatus);

        //    Assert.IsTrue(result);
        //}
        //[Test]
        //public void Test_CheckBbanFileStatus_Success_Approved()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    p_EnumBbanFileStatus? currentStatus = p_EnumBbanFileStatus.WaitingForApproval;
        //    p_EnumBbanFileStatus newStatus = p_EnumBbanFileStatus.Approved;

        //    bool result = (bool)privateObject.Invoke("CheckBbanFileStatus", currentStatus, newStatus);

        //    Assert.IsTrue(result);
        //}
        //[Test]
        //public void Test_CheckBbanFileStatus_Success_ApprovalDenied()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    p_EnumBbanFileStatus? currentStatus = p_EnumBbanFileStatus.WaitingForApproval;
        //    p_EnumBbanFileStatus newStatus = p_EnumBbanFileStatus.ApprovalDenied;

        //    bool result = (bool)privateObject.Invoke("CheckBbanFileStatus", currentStatus, newStatus);

        //    Assert.IsTrue(result);
        //}
        //[Test]
        //public void Test_CheckBbanFileStatus_Success_Importing()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    p_EnumBbanFileStatus? currentStatus = p_EnumBbanFileStatus.Approved;
        //    p_EnumBbanFileStatus newStatus = p_EnumBbanFileStatus.Importing;

        //    bool result = (bool)privateObject.Invoke("CheckBbanFileStatus", currentStatus, newStatus);

        //    Assert.IsTrue(result);
        //}
        //[Test]
        //public void Test_CheckBbanFileStatus_Success_Imported()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    p_EnumBbanFileStatus? currentStatus = p_EnumBbanFileStatus.Importing;
        //    p_EnumBbanFileStatus newStatus = p_EnumBbanFileStatus.Imported;

        //    bool result = (bool)privateObject.Invoke("CheckBbanFileStatus", currentStatus, newStatus);

        //    Assert.IsTrue(result);
        //}
        //[Test]
        //public void Test_CheckBbanFileStatus_Success_ImportedFailed()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    p_EnumBbanFileStatus? currentStatus = p_EnumBbanFileStatus.Importing;
        //    p_EnumBbanFileStatus newStatus = p_EnumBbanFileStatus.ImportFailed;

        //    bool result = (bool)privateObject.Invoke("CheckBbanFileStatus", currentStatus, newStatus);

        //    Assert.IsTrue(result);
        //}
        #endregion

        #region Cancel

        //[Test]
        //public void Test_Cancel_Success()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockIEfUnitOfWork.Setup(p => p.CommitChanges()).Returns(1);

        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), _bbanfileUploading, 1));
        //    // setup BbanFileHistory repository
        //    _bbanfileHistory.BbanFileStatusId = p_EnumBbanFileStatus.Verified;
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo<p_BbanFileHistory>(new List<p_BbanFileHistory>(), _bbanfileHistory, 1));
        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo<p_BbanImport>(_listBBAN, _bbanImport, 1));

        //    privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);

        //    // Calls method.
        //    bbanfile.Cancel(1);
        //}

        //[Test]
        //public void Test_Cancel_Fail_IbanNotFound()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockIEfUnitOfWork.Setup(p => p.CommitChanges()).Returns(1);

        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), null, 1));
        //    // setup BbanFileHistory repository
        //    _bbanfileHistory.BbanFileStatusId = p_EnumBbanFileStatus.Verified;
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo<p_BbanFileHistory>(new List<p_BbanFileHistory>(), _bbanfileHistory, 1));
        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo<p_BbanImport>(_listBBAN, _bbanImport, 1));

        //    privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);

        //    // Calls method.
        //    Assert.Throws<IbanOperationException>(() =>
        //    {
        //        bbanfile.Cancel(1);
        //    });
        //}

        //[Test]
        //public void Test_Cancel_Fail_InvalidIban()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
        //    // Calls method.
        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //        bbanfile.Cancel(0);
        //    });
        //}

        //[Test]
        //public void Test_Cancel_Fail_Disposed()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
        //    bbanfile.Dispose();
        //    // Calls method.
        //    Assert.Throws<ObjectDisposedException>(() =>
        //    {
        //        bbanfile.Cancel(1);
        //    });
        //}

        //[Test]
        //public void Test_Cancel_Fail_Crash()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);
        //    int bbanfileId = 1;

        //    // Calls method.
        //    Assert.Throws<IbanOperationException>(() =>
        //    {
        //        bbanfile.Cancel(bbanfileId);
        //    });
        //}

        //#endregion


        //#region Test_Private_CheckBbanFileStatus

        //[Test]
        //public void Test_Private_CheckBbanFileStatus_Fails()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", null, p_EnumBbanFileStatus.UploadCanceled));
        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", p_EnumBbanFileStatus.Verified, null));

        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", null, p_EnumBbanFileStatus.Verifying));
        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", p_EnumBbanFileStatus.Uploaded, null));

        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", null, p_EnumBbanFileStatus.Verified));
        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", p_EnumBbanFileStatus.Verifying, null));

        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", p_EnumBbanFileStatus.Verifying, null));
        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", null, p_EnumBbanFileStatus.VerifiedFailed));

        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", null, p_EnumBbanFileStatus.Verified));
        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", p_EnumBbanFileStatus.WaitingForApproval, null));

        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", null, p_EnumBbanFileStatus.WaitingForApproval));
        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", p_EnumBbanFileStatus.Approved, null));

        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", null, p_EnumBbanFileStatus.WaitingForApproval));
        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", p_EnumBbanFileStatus.ApprovalDenied, null));

        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", null, p_EnumBbanFileStatus.ApprovalDenied));
        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", p_EnumBbanFileStatus.WaitingForApproval, null));

        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", null, p_EnumBbanFileStatus.Importing));
        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", p_EnumBbanFileStatus.Approved, null));

        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", null, p_EnumBbanFileStatus.Approved));
        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", p_EnumBbanFileStatus.Importing, null));

        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", null, p_EnumBbanFileStatus.Imported));
        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", p_EnumBbanFileStatus.Importing, null));

        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", null, p_EnumBbanFileStatus.ImportFailed));
        //    Assert.IsFalse((bool)privateObject.Invoke("CheckBbanFileStatus", p_EnumBbanFileStatus.Importing, null));
        //}

        //#endregion


        //#region Test_Private_SetBBanFileStatus

        //[Test]
        //public void Test_Private_SetBBanFileStatus_Success_StatusApproved()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockIEfUnitOfWork.Setup(p => p.CommitChanges()).Returns(1);

        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), _bbanfileUploading, 1));
        //    // setup BbanFileHistory repository
        //    _bbanfileHistory.BbanFileStatusId = p_EnumBbanFileStatus.WaitingForApproval;
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo<p_BbanFileHistory>(new List<p_BbanFileHistory>(), _bbanfileHistory, 1));
        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo<p_BbanImport>(_listBBAN, _bbanImport, 1));

        //    // mock iban
        //    Mock<IRepository<p_Iban>> mockRepoIban = new Mock<IRepository<p_Iban>>();
        //    mockRepoIban.SetupSequence(p => p.GetById(It.IsAny<object>()))
        //                .Returns(_iban);
        //    mockIEfUnitOfWork.Setup(m => m.GetRepository<p_Iban>()).Returns(mockRepoIban.Object);

        //    privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);

        //    // case status Approved
        //    privateObject.Invoke("SetBBanFileStatus", 1, EnumBbanFileStatus.Approved, "");
        //}

        //[Test]
        //public void Test_Private_SetBBanFileStatus_Success_StatusImported()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockIEfUnitOfWork.Setup(p => p.CommitChanges()).Returns(1);

        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), _bbanfileUploading, 1));
        //    // setup BbanFileHistory repository
        //    _bbanfileHistory.BbanFileStatusId = p_EnumBbanFileStatus.Importing;
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo<p_BbanFileHistory>(new List<p_BbanFileHistory>(), _bbanfileHistory, 1));
        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo<p_BbanImport>(_listBBAN, _bbanImport, 1));

        //    // mock iban
        //    Mock<IRepository<p_Iban>> mockRepoIban = new Mock<IRepository<p_Iban>>();
        //    mockRepoIban.SetupSequence(p => p.GetById(It.IsAny<object>()))
        //                .Returns(_iban);
        //    mockIEfUnitOfWork.Setup(m => m.GetRepository<p_Iban>()).Returns(mockRepoIban.Object);

        //    privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);

        //    // case status importrd
        //    privateObject.Invoke("SetBBanFileStatus", 1, EnumBbanFileStatus.Imported, "my remark");
        //}

        //[Test]
        //public void Test_Private_SetBBanFileStatus_Fail_StatusInvalid()
        //{
        //    IBbanFileManager bbanfile = new BbanFileManager(_token, _context, _connectionString, Resources.ConfigurationFile);
        //    var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(bbanfile);

        //    Mock<IEfUnitOfWork> mockIEfUnitOfWork = new Mock<IEfUnitOfWork>();
        //    mockIEfUnitOfWork.Setup(p => p.CommitChanges()).Returns(1);

        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFile>()).Returns(MockRepository.MockRepo<p_BbanFile>(new List<p_BbanFile>(), _bbanfileUploading, 1));
        //    // setup BbanFileHistory repository
        //    _bbanfileHistory.BbanFileStatusId = p_EnumBbanFileStatus.Uploaded;
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanFileHistory>()).Returns(MockRepository.MockRepo<p_BbanFileHistory>(new List<p_BbanFileHistory>(), _bbanfileHistory, 1));
        //    // setup BbanFile repository
        //    mockIEfUnitOfWork.Setup(u => u.GetRepository<p_BbanImport>()).Returns(MockRepository.MockRepo<p_BbanImport>(_listBBAN, _bbanImport, 1));

        //    // mock iban
        //    Mock<IRepository<p_Iban>> mockRepoIban = new Mock<IRepository<p_Iban>>();
        //    mockRepoIban.SetupSequence(p => p.GetById(It.IsAny<object>()))
        //                .Returns(_iban);
        //    mockIEfUnitOfWork.Setup(m => m.GetRepository<p_Iban>()).Returns(mockRepoIban.Object);

        //    privateObject.SetFieldOrProperty("_unitOfWork", mockIEfUnitOfWork.Object);

        //    // case status Approved
        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //        privateObject.Invoke("SetBBanFileStatus", 1, EnumBbanFileStatus.Approved, "");
        //    });
        //}

        #endregion
    }
}
