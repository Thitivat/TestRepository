//using BND.Services.Payments.iDeal.Dal;
//using BND.Services.Payments.iDeal.Dal.Pocos;
//using BND.Services.Payments.iDeal.iDealClients.Base;
//using BND.Services.Payments.iDeal.iDealClients.Interfaces;
//using BND.Services.Payments.iDeal.Interfaces;
//using BND.Services.Payments.iDeal.Models;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Xml.Linq;
//using System.Linq;
//using Newtonsoft.Json;
//using BND.Services.Payments.iDeal.JobQueue.Bll.Interfaces;

//namespace BND.Services.Payments.iDeal.Bll.Tests
//{
//    [TestFixture]
//    public class iDealServiceTest
//    {
//        Mock<IDirectoryRepository> _directoryRepository;
//        Mock<ITransactionRepository> _transactionRepository;
//        Mock<ISettingRepository> _settingRepository;
//        Mock<IiDealClient> _iDealClient;
//        Mock<ILogger> _logger;
//        Mock<IJobQueueManager> _jobQueue;

//        IEnumerable<DirectoryModel> _oldDataExpected;
//        IEnumerable<DirectoryModel> _newDataExpected;

//        p_Directory _iDealDirectory;
//        p_Directory _dbDirectory;

//        string _xmlDirectoryResponse;
//        string _xmlStatusResponse;
//        string _xmlStatusOpenResponse;
//        string _xmlStatusFailResponse;
//        XElement _xmlErrorResponse;

//        iDealService _service;

//        const string MIN_EXPIRATION_PERIOD_KEY = "MinExpirationPeriodSecond";
//        const string MAX_EXPIRATION_PERIOD_KEY = "MaxExpirationPeriodSecond";

//        [SetUp]
//        public void SetUp()
//        {
//            _xmlDirectoryResponse = Properties.Resources.DirectoryResponse;
//            _xmlStatusResponse = Properties.Resources.StatusResponse;
//            _xmlStatusOpenResponse = Properties.Resources.StatusOpenResponse;
//            _xmlStatusFailResponse = Properties.Resources.StatusFailResponse;
//            _xmlErrorResponse = XElement.Parse(Properties.Resources.ErrorResponse);

//            _dbDirectory = new p_Directory()
//                            {
//                                Issuers = new List<p_Issuer> 
//                                {
//                                    new p_Issuer
//                                    { 
//                                        CountryNames = "Nederland",
//                                        IssuerID = "ABNANL2AXXX",
//                                        IssuerName = "ABN AMRO Bank"
//                                    },
//                                    new p_Issuer
//                                    {
//                                        CountryNames = "Nederland",
//                                        IssuerID = "ASNNNL21XXX",
//                                        IssuerName = "ASN Bank"
//                                    }
//                                },
//                                DirectoryDateTimestamp = DateTime.Now.AddDays(-10),
//                                LastDirectoryRequestDateTimestamp = DateTime.Now
//                            };

//            _iDealDirectory = new p_Directory()
//                                {
//                                    Issuers = new List<p_Issuer> 
//                                    {
//                                        new p_Issuer
//                                        { 
//                                            CountryNames = "Nederland",
//                                            IssuerID = "ABNANL2AXXX",
//                                            IssuerName = "ABN AMRO Bank"
//                                        },
//                                        new p_Issuer
//                                        {
//                                            CountryNames = "Nederland",
//                                            IssuerID = "ASNNNL21XXX",
//                                            IssuerName = "ASN Bank"
//                                        },
//                                        new p_Issuer
//                                        {
//                                            CountryNames = "Nederland",
//                                            IssuerID = "FRBKNL2LXXX",
//                                            IssuerName = "Friesland Bank"
//                                        },
//                                        new p_Issuer
//                                        {
//                                            CountryNames = "Nederland",
//                                            IssuerID = "INGBNL2AXXX",
//                                            IssuerName = "ING"
//                                        },
//                                        new p_Issuer
//                                        {
//                                            CountryNames = "Nederland",
//                                            IssuerID = "RABONL2UXXX",
//                                            IssuerName = "Rabobank"
//                                        },
//                                        new p_Issuer
//                                        {
//                                            CountryNames = "België/Belgique",
//                                            IssuerID = "KREDBE22XXX",
//                                            IssuerName = "KBC"
//                                        }
//                                    },
//                                    DirectoryDateTimestamp = DateTime.Now.AddDays(-10),
//                                    LastDirectoryRequestDateTimestamp = DateTime.Now
//                                };

//            _oldDataExpected = new List<DirectoryModel>()
//            {
//                new DirectoryModel()
//                {
//                    CountryNames = "Nederland"   ,
//                    Issuers = new List<IssuerModel>()
//                    {
//                        new IssuerModel() { IssuerID = "ABNANL2AXXX", IssuerName = "ABN AMRO Bank" },
//                        new IssuerModel() { IssuerID = "ASNNNL21XXX", IssuerName = "ASN Bank" },
//                    }
//                }
//            };

//            _newDataExpected = new List<DirectoryModel>()
//            {
//                new DirectoryModel()
//                {
//                    CountryNames = "Nederland",
//                    Issuers = new List<IssuerModel>()
//                    {
//                        new IssuerModel() { IssuerID = "ABNANL2AXXX", IssuerName = "ABN AMRO Bank" },
//                        new IssuerModel() { IssuerID = "ASNNNL21XXX", IssuerName = "ASN Bank" },
//                        new IssuerModel() { IssuerID = "FRBKNL2LXXX", IssuerName = "Friesland Bank" },
//                        new IssuerModel() { IssuerID = "INGBNL2AXXX", IssuerName = "ING" },
//                        new IssuerModel() { IssuerID = "RABONL2UXXX", IssuerName = "Rabobank" },
//                    }
//                },
//                new DirectoryModel()
//                {
//                    CountryNames = "België/Belgique",
//                    Issuers = new List<IssuerModel>()
//                    {
//                        new IssuerModel() { IssuerID = "KREDBE22XXX", IssuerName = "KBC" },
//                    }
//                },
//            };

//            _directoryRepository = new Mock<IDirectoryRepository>();
//            _directoryRepository
//                .Setup(x => x.UpdateDirectory(It.IsAny<p_Directory>()))
//                .Verifiable();

//            _transactionRepository = new Mock<ITransactionRepository>();
//            _transactionRepository
//                .Setup(t => t.Insert(It.IsAny<p_Transaction>()))
//                .Verifiable();

//            _settingRepository = new Mock<ISettingRepository>();
//            _settingRepository
//                .Setup(s => s.GetValueByKey(MIN_EXPIRATION_PERIOD_KEY))
//                .Returns(60.ToString());
//            _settingRepository
//                .Setup(s => s.GetValueByKey(MAX_EXPIRATION_PERIOD_KEY))
//                .Returns(3600.ToString());
//            _settingRepository
//                .Setup(s => s.GetValueByKey("DefaultExpirationPeriodSecond"))
//                .Returns(900.ToString());
//            _settingRepository
//                .Setup(s => s.GetValueByKey("MaxRetriesPerDays"))
//                .Returns(5.ToString());

//            _iDealClient = new Mock<IiDealClient>();

//            _logger = new Mock<ILogger>();
//            _logger.Setup(x => x.Info(It.IsAny<string>())).Verifiable();
//            _logger.Setup(x => x.Warning(It.IsAny<string>())).Verifiable();
//            _logger.Setup(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>())).Verifiable();

//            _jobQueue = new Mock<IJobQueueManager>();
//            _jobQueue.Setup(x => x.CreateJobQueue(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(1);

//            _service = new iDealService(
//                _directoryRepository.Object, _transactionRepository.Object, _settingRepository.Object, _iDealClient.Object, _logger.Object, _jobQueue.Object);
//        }

//        [Test]
//        public void GetDirectory_Should_ReturnOldData_When_DataFromTodayDoesExists()
//        {
//            _directoryRepository
//                .Setup(x => x.Get())
//                .Returns(_dbDirectory);

//            _directoryRepository.Setup(x => x.Get()).Returns(_dbDirectory);
//            IEnumerable<DirectoryModel> data = _service.GetDirectories();

//            Assert.AreEqual(_oldDataExpected.Count(), data.Count());
//            Assert.AreEqual(_oldDataExpected.FirstOrDefault().CountryNames, data.FirstOrDefault().CountryNames);
//            Assert.AreEqual(_oldDataExpected.FirstOrDefault().Issuers.Count(), data.FirstOrDefault().Issuers.Count());
//            Assert.AreEqual(_oldDataExpected.FirstOrDefault().Issuers.FirstOrDefault().IssuerName, data.FirstOrDefault().Issuers.FirstOrDefault().IssuerName);
//        }

//        [Test]
//        public void GetDirectory_Should_ReturnNewData_When_DataFromTodayDoesNotExist()
//        {
//            p_Directory emptyDir = new p_Directory();
//            _directoryRepository.SetupSequence(x => x.Get())
//                .Returns(emptyDir)
//                .Returns(_iDealDirectory);

//            var response = new iDealClients.Directory.DirectoryResponse(_xmlDirectoryResponse);
//            _iDealClient
//               .Setup(x => x.SendDirectoryRequest())
//               .Returns(response);

//            IEnumerable<DirectoryModel> data = _service.GetDirectories();
//            // compare 2 object cannot use another way(sequence equal, collectAssert).
//            Assert.AreEqual(JsonConvert.SerializeObject(data), JsonConvert.SerializeObject(data));
//            Assert.AreEqual(_newDataExpected.FirstOrDefault().CountryNames, data.FirstOrDefault().CountryNames);
//            Assert.AreEqual(_newDataExpected.FirstOrDefault().Issuers.Count(), data.FirstOrDefault().Issuers.Count());
//            Assert.AreEqual(_newDataExpected.FirstOrDefault().Issuers.FirstOrDefault().IssuerName, data.FirstOrDefault().Issuers.FirstOrDefault().IssuerName);
//        }

//        [Test]
//        public void GetDirectory_Should_ThrowiDealOperationException_When_DataFromToDayDoesNotExistAndiDealClientReturnNothing()
//        {
//            iDealClients.Directory.DirectoryResponse nullResponse = null;
//            _iDealClient
//               .Setup(x => x.SendDirectoryRequest())
//               .Returns(nullResponse);

//            p_Directory emptyDir = new p_Directory();
//            _directoryRepository
//                .Setup(x => x.Get())
//                .Returns(emptyDir);

//            Assert.Throws<iDealOperationException>(() =>
//            {
//                IEnumerable<DirectoryModel> data = _service.GetDirectories();
//            });
//        }

//        [Test]
//        public void GetDirectory_Should_ThrowiDealOperationException_When_DataFromToDayDoesNotExistAndiDealClientHasiDealException()
//        {
//            iDealException exception = new iDealException(_xmlErrorResponse);
//            _iDealClient
//               .Setup(x => x.SendDirectoryRequest())
//               .Throws(exception);

//            p_Directory emptyDir = new p_Directory();
//            _directoryRepository
//                .Setup(x => x.Get())
//                .Returns(emptyDir);

//            iDealOperationException thrownException = Assert.Throws<iDealOperationException>(() =>
//            {
//                IEnumerable<DirectoryModel> data = _service.GetDirectories();
//            });
//            Assert.AreEqual(exception.ErrorCode, thrownException.ErrorCode);
//        }

//        [Test]
//        public void GetDirectory_Should_ThrowiDealOperationException_When_DataFromToDayDoesNotExistAndiDealClientHasAnyException()
//        {
//            Exception exception = new Exception("Test Exception");
//            _iDealClient
//               .Setup(x => x.SendDirectoryRequest())
//               .Throws(exception);

//            p_Directory emptyDir = new p_Directory();
//            _directoryRepository
//                .Setup(x => x.Get())
//                .Returns(emptyDir);

//            iDealOperationException thrownException = Assert.Throws<iDealOperationException>(() =>
//            {
//                IEnumerable<DirectoryModel> data = _service.GetDirectories();
//            });
//            Assert.AreEqual(ErrorMessages.Error.Code, thrownException.ErrorCode);
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowArgumentNullException_When_transactionRequestParameterIsNull()
//        {
//            Assert.Throws<ArgumentNullException>(() => _service.CreateTransaction(null));
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowArgumentOutOfRangeException_When_transactionRequestAmountLessThanOrEqualsZero()
//        {
//            Assert.Throws<ArgumentOutOfRangeException>(() => _service.CreateTransaction(new TransactionRequestModel()));
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowArgumentNullException_When_transactionRequestBNDIBANParameterIsNull()
//        {
//            Assert.Throws<ArgumentNullException>(() => _service.CreateTransaction(new TransactionRequestModel { Amount = 20 }));
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowArgumentNullException_When_transactionRequestCurrencyParameterIsNull()
//        {
//            Assert.Throws<ArgumentNullException>(
//                () => _service.CreateTransaction(new TransactionRequestModel
//                {
//                    Amount = 20,
//                    BNDIBAN = "IBAN",
//                    Currency = String.Empty
//                })
//            );
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowArgumentException_When_transactionRequestCurrencyParameterIsWrongFormat()
//        {
//            Assert.Throws<ArgumentException>(
//                () => _service.CreateTransaction(new TransactionRequestModel
//                {
//                    Amount = 20,
//                    BNDIBAN = "IBAN",
//                    Currency = "currency"
//                })
//            );
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowArgumentNullException_When_transactionRequestCustomerIBANParameterIsNull()
//        {
//            Assert.Throws<ArgumentNullException>(
//                () => _service.CreateTransaction(new TransactionRequestModel
//                {
//                    Amount = 20,
//                    BNDIBAN = "IBAN"
//                })
//            );
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowArgumentOutOfRangeException_When_transactionRequestExpirationPeriodParameterLessThanMinValue()
//        {
//            int expectedMinValue = 60;
//            _settingRepository.Setup(s => s.GetValueByKey(MIN_EXPIRATION_PERIOD_KEY))
//                              .Returns(expectedMinValue.ToString());

//            Assert.Throws<ArgumentOutOfRangeException>(
//                () => _service.CreateTransaction(new TransactionRequestModel
//                {
//                    Amount = 20,
//                    BNDIBAN = "IBAN",
//                    CustomerIBAN = "IBAN",
//                    ExpirationPeriod = expectedMinValue - expectedMinValue - 20
//                })
//            );
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowArgumentOutOfRangeException_When_transactionRequestExpirationPeriodParameterGreaterThanMaxValue()
//        {
//            int expectedMaxValue = 3600;
//            _settingRepository.Setup(s => s.GetValueByKey(MAX_EXPIRATION_PERIOD_KEY))
//                              .Returns(expectedMaxValue.ToString());

//            Assert.Throws<ArgumentOutOfRangeException>(
//                () => _service.CreateTransaction(new TransactionRequestModel
//                {
//                    Amount = 20,
//                    BNDIBAN = "IBAN",
//                    CustomerIBAN = "IBAN",
//                    ExpirationPeriod = expectedMaxValue + 20
//                })
//            );
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowArgumentNullException_When_transactionRequestIssuerIDParameterIsNull()
//        {
//            Assert.Throws<ArgumentNullException>(
//                () => _service.CreateTransaction(new TransactionRequestModel
//                {
//                    Amount = 20,
//                    BNDIBAN = "IBAN",
//                    CustomerIBAN = "IBAN"
//                })
//            );
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowArgumentNullException_When_transactionRequestLanguageParameterIsNull()
//        {
//            Assert.Throws<ArgumentNullException>(
//                () => _service.CreateTransaction(new TransactionRequestModel
//                {
//                    Amount = 20,
//                    BNDIBAN = "IBAN",
//                    CustomerIBAN = "IBAN",
//                    IssuerID = "Issuer01",
//                    Language = String.Empty
//                })
//            );
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowArgumentException_When_transactionRequestLanguageParameterIsWrongFormat()
//        {
//            Assert.Throws<ArgumentException>(
//                () => _service.CreateTransaction(new TransactionRequestModel
//                {
//                    Amount = 20,
//                    BNDIBAN = "IBAN",
//                    CustomerIBAN = "IBAN",
//                    IssuerID = "Issuer01",
//                    Language = "Language"
//                })
//            );
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowArgumentNullException_When_transactionRequestMerchantReturnURLParameterIsNull()
//        {
//            Assert.Throws<ArgumentNullException>(
//                () => _service.CreateTransaction(new TransactionRequestModel
//                {
//                    Amount = 20,
//                    BNDIBAN = "IBAN",
//                    CustomerIBAN = "IBAN",
//                    IssuerID = "Issuer01"
//                })
//            );
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowArgumentNullException_When_transactionRequestPaymentTypeParameterIsNull()
//        {
//            Assert.Throws<ArgumentNullException>(
//                () => _service.CreateTransaction(new TransactionRequestModel
//                {
//                    Amount = 20,
//                    BNDIBAN = "IBAN",
//                    CustomerIBAN = "IBAN",
//                    IssuerID = "Issuer01",
//                    MerchantReturnURL = "http://www.tempuri.org"
//                })
//            );
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowArgumentNullException_When_transactionRequestPurchaseIDParameterIsNull()
//        {
//            Assert.Throws<ArgumentNullException>(
//                () => _service.CreateTransaction(new TransactionRequestModel
//                {
//                    Amount = 20,
//                    BNDIBAN = "IBAN",
//                    CustomerIBAN = "IBAN",
//                    IssuerID = "Issuer01",
//                    MerchantReturnURL = "http://www.tempuri.org",
//                    PaymentType = "Type01"
//                })
//            );
//        }

//        [Test]
//        public void CreateTransaction_Should_Succeed_When_HasBeenCalledWithDefaultExpirationPeriod()
//        {
//            var expectedResponse = new iDealClients.Transaction.TransactionResponse(Properties.Resources.TransactionResponse);
//            var expectedConfig = new Mock<IConfiguration>();
//            expectedConfig.Setup(c => c.MerchantId).Returns("123456");
//            expectedConfig.Setup(c => c.MerchantSubId).Returns(0);

//            _iDealClient.Setup(i => i.SendTransactionRequest(
//                            It.IsAny<string>(),
//                            It.IsAny<string>(),
//                            It.IsAny<string>(),
//                            It.IsAny<decimal>(),
//                            It.IsAny<TimeSpan>(),
//                            It.IsAny<string>(),
//                            It.IsAny<string>(),
//                            It.IsAny<string>(),
//                            It.IsAny<string>()))
//                        .Returns(expectedResponse);
//            _iDealClient.Setup(i => i.Configuration).Returns(expectedConfig.Object);

//            var actual = _service.CreateTransaction(new TransactionRequestModel
//            {
//                Amount = 20,
//                BNDIBAN = "IBAN",
//                CustomerIBAN = "IBAN",
//                IssuerID = "Issuer01",
//                MerchantReturnURL = "http://www.tempuri.org",
//                PaymentType = "Type01",
//                PurchaseID = "PID"
//            });

//            Assert.IsNotNull(actual);
//            Assert.IsNotNull(actual.EntranceCode);
//            Assert.AreEqual(expectedResponse.IssuerAuthenticationUrl, actual.IssuerAuthenticationURL.ToString());
//            Assert.AreEqual(expectedResponse.PurchaseId, actual.PurchaseID);
//            Assert.AreEqual(expectedResponse.TransactionId, actual.TransactionID);
//        }

//        [Test]
//        public void CreateTransaction_Should_Succeed_When_HasBeenCalled()
//        {
//            var expectedResponse = new iDealClients.Transaction.TransactionResponse(Properties.Resources.TransactionResponse);
//            var expectedConfig = new Mock<IConfiguration>();
//            expectedConfig.Setup(c => c.MerchantId).Returns("123456");
//            expectedConfig.Setup(c => c.MerchantSubId).Returns(0);

//            _iDealClient.Setup(i => i.SendTransactionRequest(
//                            It.IsAny<string>(),
//                            It.IsAny<string>(),
//                            It.IsAny<string>(),
//                            It.IsAny<decimal>(),
//                            It.IsAny<TimeSpan>(),
//                            It.IsAny<string>(),
//                            It.IsAny<string>(),
//                            It.IsAny<string>(),
//                            It.IsAny<string>()))
//                        .Returns(expectedResponse);
//            _iDealClient.Setup(i => i.Configuration).Returns(expectedConfig.Object);

//            var actual = _service.CreateTransaction(new TransactionRequestModel
//            {
//                Amount = 20,
//                BNDIBAN = "IBAN",
//                CustomerIBAN = "IBAN",
//                ExpirationPeriod = 100,
//                IssuerID = "Issuer01",
//                MerchantReturnURL = "http://www.tempuri.org",
//                PaymentType = "Type01",
//                PurchaseID = "PID"
//            });

//            Assert.IsNotNull(actual);
//            Assert.IsNotNull(actual.EntranceCode);
//            Assert.AreEqual(expectedResponse.IssuerAuthenticationUrl, actual.IssuerAuthenticationURL.ToString());
//            Assert.AreEqual(expectedResponse.PurchaseId, actual.PurchaseID);
//            Assert.AreEqual(expectedResponse.TransactionId, actual.TransactionID);
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowiDealOperationException_When_SomethingWentWrongFromOurComponent()
//        {
//            Assert.Throws<iDealOperationException>(
//                () => _service.CreateTransaction(new TransactionRequestModel
//                {
//                    Amount = 20,
//                    BNDIBAN = "IBAN",
//                    CustomerIBAN = "IBAN",
//                    ExpirationPeriod = 100,
//                    IssuerID = "Issuer01",
//                    MerchantReturnURL = "http://www.tempuri.org",
//                    PaymentType = "Type01",
//                    PurchaseID = "PID"
//                })
//            );
//        }

//        [Test]
//        public void CreateTransaction_Should_ThrowiDealOperationException_When_SomethingWentWrongFromiDealService()
//        {
//            _iDealClient.Setup(i => i.SendTransactionRequest(
//                            It.IsAny<string>(),
//                            It.IsAny<string>(),
//                            It.IsAny<string>(),
//                            It.IsAny<decimal>(),
//                            It.IsAny<TimeSpan>(),
//                            It.IsAny<string>(),
//                            It.IsAny<string>(),
//                            It.IsAny<string>(),
//                            It.IsAny<string>()))
//                        .Throws(new iDealException(_xmlErrorResponse));

//            Assert.Throws<iDealOperationException>(
//                () => _service.CreateTransaction(new TransactionRequestModel
//                {
//                    Amount = 20,
//                    BNDIBAN = "IBAN",
//                    CustomerIBAN = "IBAN",
//                    ExpirationPeriod = 100,
//                    IssuerID = "Issuer01",
//                    MerchantReturnURL = "http://www.tempuri.org",
//                    PaymentType = "Type01",
//                    PurchaseID = "PID"
//                })
//            );
//        }

//        [TestCase("entranceCode", "")]
//        [TestCase("", "transactionID")]
//        public void GetStatus_Should_ThrowArgumentNullException_When_ParamsIsNullOrEmpty(string entranceCode, string transactionID)
//        {
//            Assert.Throws<ArgumentNullException>(() => _service.GetStatus(entranceCode, transactionID));
//        }

//        [TestCase("entranceCode", "transactionID")]
//        public void GetStatus_Should_ThrowObjectNotFoundException_When_DataNotFound(string entranceCode, string transactionID)
//        {
//            Assert.Throws<ObjectNotFoundException>(() => _service.GetStatus(entranceCode, transactionID));
//        }

//        [TestCase("entranceCode", "transactionID")]
//        public void GetStatus_Should_ThrowInvalidOperationException_When_SystemWasFail(string entranceCode, string transactionID)
//        {
//            p_Transaction tx = new p_Transaction()
//            {
//                IsSystemFail = true
//            };
//            _transactionRepository
//                .Setup(x => x.GetTransactionWithLatestStatus(It.IsAny<String>(), It.IsAny<String>()))
//                .Returns(tx);
//            Assert.Throws<InvalidOperationException>(() => _service.GetStatus(entranceCode, transactionID));
//        }

//        [TestCase("entranceCode", "transactionID")]
//        public void GetStatus_Should_ReturnData_When_StatusAlreadyExist(string entranceCode, string transactionID)
//        {
//            p_Transaction tx = new p_Transaction()
//            {
//                IsSystemFail = false,
//                TransactionStatusHistories = new List<p_TransactionStatusHistory>
//                {
//                    new p_TransactionStatusHistory
//                    {
//                        Status = EnumQueryStatus.Success.ToString()
//                    }
//                }
//            };
//            _transactionRepository
//                .Setup(x => x.GetTransactionWithLatestStatus(It.IsAny<String>(), It.IsAny<String>()))
//                .Returns(tx);
//            Assert.AreEqual(EnumQueryStatus.Success.ToString(), _service.GetStatus(entranceCode, transactionID).ToString());
//        }

//        [TestCase("entranceCode", "transactionID")]
//        public void GetStatus_Should_ThrowInvalidOperationException_When_AttemptMoreThan5Times(string entranceCode, string transactionID)
//        {
//            p_Transaction tx = new p_Transaction()
//            {
//                IsSystemFail = false,
//                TransactionCreateDateTimestamp = DateTime.Now,
//                TodayAttempts = 5,
//                LatestAttemptsDateTimestamp = DateTime.Now,
//                TransactionStatusHistories = new List<p_TransactionStatusHistory>
//                {
//                    new p_TransactionStatusHistory
//                    {
//                        Status = EnumQueryStatus.Open.ToString()
//                    }
//                }
//            };
//            _transactionRepository
//                .Setup(x => x.GetTransactionWithLatestStatus(It.IsAny<String>(), It.IsAny<String>()))
//                .Returns(tx);

//            Assert.Throws<InvalidOperationException>(() => _service.GetStatus(entranceCode, transactionID));
//        }

//        [TestCase("entranceCode", "transactionID")]
//        public void GetStatus_Should_ThrowInvalidOperationException_When_AttemptRepeatLessThan60Seconds(string entranceCode, string transactionID)
//        {
//            p_Transaction tx = new p_Transaction()
//            {
//                IsSystemFail = false,
//                TransactionCreateDateTimestamp = DateTime.Now.AddDays(-6),
//                TodayAttempts = 4,
//                LatestAttemptsDateTimestamp = DateTime.Now,
//                TransactionStatusHistories = new List<p_TransactionStatusHistory>
//                {
//                    new p_TransactionStatusHistory
//                    {
//                        Status = EnumQueryStatus.Open.ToString(),
//                        StatusResponseDateTimeStamp = DateTime.Now
//                    }
//                }
//            };
//            _transactionRepository
//                .Setup(x => x.GetTransactionWithLatestStatus(It.IsAny<String>(), It.IsAny<String>()))
//                .Returns(tx);

//            Assert.Throws<InvalidOperationException>(() => _service.GetStatus(entranceCode, transactionID));
//        }

//        [TestCase("entranceCode", "transactionID")]
//        public void GetStatus_Should_ThrowiDealOperationException_When_AmountIsInvalid(string entranceCode, string transactionID)
//        {
//            p_Transaction tx = new p_Transaction()
//            {
//                IsSystemFail = false,
//                TransactionCreateDateTimestamp = DateTime.Now.AddDays(-6),
//                TodayAttempts = 4,
//                Amount = 7,
//                TransactionStatusHistories = new List<p_TransactionStatusHistory>
//                {
//                    new p_TransactionStatusHistory
//                    {
//                        Status = EnumQueryStatus.Open.ToString(),
//                        StatusResponseDateTimeStamp = DateTime.Now.AddSeconds(-100)
//                    }
//                }
//            };
//            _transactionRepository
//                .Setup(x => x.GetTransactionWithLatestStatus(It.IsAny<String>(), It.IsAny<String>()))
//                .Returns(tx);

//            var response = new iDealClients.Status.StatusResponse(_xmlStatusResponse);
//            _iDealClient
//               .Setup(x => x.SendStatusRequest(It.IsAny<String>()))
//               .Returns(response);
//            Assert.Throws<iDealOperationException>(() => _service.GetStatus(entranceCode, transactionID));
//        }

//        [TestCase("entranceCode", "transactionID")]
//        public void GetStatus_Should_ThrowiDealOperationException_When_TransactionExpired(string entranceCode, string transactionID)
//        {
//            p_Transaction tx = new p_Transaction()
//            {
//                IsSystemFail = false,
//                TransactionCreateDateTimestamp = DateTime.Now.AddHours(-1),
//                TodayAttempts = 4,
//                Amount = 1,
//                ExpirationSecondPeriod = 60,
//                TransactionStatusHistories = new List<p_TransactionStatusHistory>
//                {
//                    new p_TransactionStatusHistory
//                    {
//                        Status = EnumQueryStatus.Open.ToString(),
//                        StatusResponseDateTimeStamp = DateTime.Now.AddSeconds(-100)
//                    }
//                }
//            };
//            _transactionRepository
//                .Setup(x => x.GetTransactionWithLatestStatus(It.IsAny<String>(), It.IsAny<String>()))
//                .Returns(tx);

//            var response = new iDealClients.Status.StatusResponse(_xmlStatusOpenResponse);
//            _iDealClient
//               .Setup(x => x.SendStatusRequest(It.IsAny<String>()))
//               .Returns(response);

//            Assert.Throws<iDealOperationException>(() => _service.GetStatus(entranceCode, transactionID));
//        }

//        [TestCase("entranceCode", "transactionID")]
//        public void GetStatus_Should_ReturnSuccessStatus_When_SendRequestFinished(string entranceCode, string transactionID)
//        {
//            p_Transaction tx = new p_Transaction()
//            {
//                IsSystemFail = false,
//                TransactionCreateDateTimestamp = DateTime.Now.AddDays(-6),
//                TodayAttempts = 4,
//                Amount = 1,
//                TransactionStatusHistories = new List<p_TransactionStatusHistory>
//                {
//                    new p_TransactionStatusHistory
//                    {
//                        Status = EnumQueryStatus.Open.ToString(),
//                        StatusResponseDateTimeStamp = DateTime.Now.AddSeconds(-100)
//                    }
//                }
//            };
//            _transactionRepository
//                .Setup(x => x.GetTransactionWithLatestStatus(It.IsAny<String>(), It.IsAny<String>()))
//                .Returns(tx);

//            var response = new iDealClients.Status.StatusResponse(_xmlStatusResponse);
//            _iDealClient
//               .Setup(x => x.SendStatusRequest(It.IsAny<String>()))
//               .Returns(response);

//            Assert.AreEqual(EnumQueryStatus.Success, _service.GetStatus(entranceCode, transactionID));
//        }

//        [TestCase("entranceCode", "transactionID")]
//        public void GetStatus_Should_ReturnSuccessStatus_When_FirstRequest(string entranceCode, string transactionID)
//        {
//            p_Transaction tx = new p_Transaction()
//            {
//                IsSystemFail = false,
//                TransactionCreateDateTimestamp = DateTime.Now,
//                TodayAttempts = 0,
//                Amount = 1,
//            };
//            _transactionRepository
//                .Setup(x => x.GetTransactionWithLatestStatus(It.IsAny<String>(), It.IsAny<String>()))
//                .Returns(tx);

//            var response = new iDealClients.Status.StatusResponse(_xmlStatusResponse);
//            _iDealClient
//               .Setup(x => x.SendStatusRequest(It.IsAny<String>()))
//               .Returns(response);

//            Assert.AreEqual(EnumQueryStatus.Success, _service.GetStatus(entranceCode, transactionID));
//        }

//        [TestCase("entranceCode", "transactionID")]
//        public void GetStatus_Should_ReturnCancelledStatus_When_SendRequestFinished(string entranceCode, string transactionID)
//        {
//            p_Transaction tx = new p_Transaction()
//            {
//                IsSystemFail = false,
//                TransactionCreateDateTimestamp = DateTime.Now.AddDays(-6),
//                TodayAttempts = 4,
//                Amount = 1,
//                LatestAttemptsDateTimestamp = DateTime.Now.AddMinutes(-2),
//                TransactionStatusHistories = new List<p_TransactionStatusHistory>
//                {
//                    new p_TransactionStatusHistory
//                    {
//                        Status = EnumQueryStatus.Open.ToString(),
//                        StatusResponseDateTimeStamp = DateTime.Now.AddSeconds(-100)
//                    }
//                }
//            };
//            _transactionRepository
//                .Setup(x => x.GetTransactionWithLatestStatus(It.IsAny<String>(), It.IsAny<String>()))
//                .Returns(tx);

//            var response = new iDealClients.Status.StatusResponse(_xmlStatusFailResponse);
//            _iDealClient
//               .Setup(x => x.SendStatusRequest(It.IsAny<String>()))
//               .Returns(response);

//            Assert.AreEqual(EnumQueryStatus.Cancelled, _service.GetStatus(entranceCode, transactionID));
//        }
//    }
//}
