using BND.Services.Payments.iDeal.Dal;
using BND.Services.Payments.iDeal.Dal.Pocos;
using BND.Services.Payments.iDeal.iDealClients.Base;
using Moq;
using NUnit.Framework;
using System;
using System.Xml.Linq;

namespace BND.Services.Payments.iDeal.Bll.Tests
{
    [TestFixture]
    public class LoggerTest
    {
        private const string APP_NAME = "APP_NAME";
        private iDealException _iDealException;

        [SetUp]
        public void SetUp()
        {
            XElement errorResponse = XElement.Parse(Properties.Resources.ErrorResponse);
            _iDealException = new iDealException(errorResponse);
        }

        #region Mocking Class
        public class MockLogger : Logger
        {
            public byte? GetPrivalTest(int logType)
            {
                return this.GetPrival(logType);
            }

            public p_Log GenerateLogTest(int severity, string procId, string msgId, string message)
            {
                return GenerateLog(severity, procId, msgId, message);
            }

            public p_Log GenerateLogTest(int severity, string procId, string msgId, Exception exception)
            {
                return GenerateLog(severity, procId, msgId, exception);
            }

            public MockLogger() : base(null, APP_NAME) { }
            public MockLogger(ILogRepository logRepository, string appName) : base(logRepository, appName) { }
        }
        #endregion

        #region GetPrival
        [Test]
        public void GetPrival_Should_Return134_When_LogTypeIsInfo()
        {
            MockLogger logger = new MockLogger();
            byte? actual = logger.GetPrivalTest((int)Logger.Severity.Informational);
            byte? expected = 134;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPrival_Should_Return132_When_LogTypeIsWarning()
        {
            MockLogger logger = new MockLogger();
            byte? actual = logger.GetPrivalTest((int)Logger.Severity.Warning);
            byte? expected = 132;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPrival_Should_Return131_When_LogTypeIsError()
        {
            MockLogger logger = new MockLogger();
            byte? actual = logger.GetPrivalTest((int)Logger.Severity.Error);
            byte? expected = 131;

            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region GenerateLog
        [Test]
        public void GenerateLog_Should_ReturnExpectedObject_When_InputData()
        {
            int severity = (int)Logger.Severity.Informational;
            string procId = "PROC_ID";
            string msgId = "MSG_ID";
            string message = "MESSAGE";

            MockLogger logger = new MockLogger();
            p_Log actual = logger.GenerateLogTest(severity, procId, msgId, message);
            p_Log expected = new p_Log()
            {
                Prival = (byte)134,
                Version = 1,
                Hostname = System.Environment.MachineName,
                AppName = APP_NAME,
                ProcId = procId,
                MsgId = msgId,
                Msg = message,
            };

            Assert.AreEqual(expected.Prival, actual.Prival);
            Assert.AreEqual(expected.Version, actual.Version);
            Assert.AreEqual(expected.Hostname, actual.Hostname);
            Assert.AreEqual(expected.AppName, actual.AppName);
            Assert.AreEqual(expected.ProcId, actual.ProcId);
            Assert.AreEqual(expected.MsgId, actual.MsgId);
            Assert.AreEqual(expected.Msg, actual.Msg);
        }

        [Test]
        public void GenerateLog_Should_ReturnMsgIdFromInputAndMessageFromException_When_InputAnyExceptionWithMsgId()
        {
            int severity = (int)Logger.Severity.Error;
            string procId = "PROC_ID";
            string msgId = "MSG_ID";
            string message = "MESSAGE";

            MockLogger logger = new MockLogger();
            Exception exception = new Exception(message);
            p_Log actual = logger.GenerateLogTest(severity, procId, msgId, exception);

            Assert.AreEqual(msgId, actual.MsgId);
            Assert.AreEqual(exception.Message, actual.Msg);
        }

        [Test]
        public void GenerateLog_Should_ReturnMsgIdAndMessageFromiDealException_When_InputiDealExceptionWithoutMsgId()
        {
            int severity = (int)Logger.Severity.Error;
            string procId = "PROC_ID";

            MockLogger logger = new MockLogger();
            p_Log actual = logger.GenerateLogTest(severity, procId, null, _iDealException);

            Assert.AreEqual(_iDealException.ErrorCode, actual.MsgId);
            Assert.AreEqual(_iDealException.Message, actual.Msg);
        }

        [Test]
        public void GenerateLog_Should_ReturnMsgIdFromInputAndMessageFromiDealException_When_InputiDealExceptionWithMsgId()
        {
            int severity = (int)Logger.Severity.Error;
            string procId = "PROC_ID";
            string msgId = "MSG_ID";

            MockLogger logger = new MockLogger();
            p_Log actual = logger.GenerateLogTest(severity, procId, msgId, _iDealException);

            Assert.AreEqual(msgId, actual.MsgId);
            Assert.AreEqual(_iDealException.Message, actual.Msg);
        }
        #endregion

        #region Info
        [Test]
        public void Info_Should_BeSaved_When_InputData()
        {
            Mock<ILogRepository> mockRep = new Mock<ILogRepository>();
            mockRep.Setup(x => x.Insert(It.IsAny<p_Log>())).Verifiable();
            Logger logger = new Logger(mockRep.Object, APP_NAME);

            logger.Info("TestMessage");
        }

        [Test]
        public void Info_Should_ThrowArgumentException_When_MissingMessage()
        {
            Mock<ILogRepository> mockRep = new Mock<ILogRepository>();
            mockRep.Setup(x => x.Insert(It.IsAny<p_Log>())).Verifiable();
            Logger logger = new Logger(mockRep.Object, APP_NAME);

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            {
                logger.Info(null);
            });

            Assert.AreEqual("message", ex.ParamName);
        }
        #endregion

        #region Warning
        [Test]
        public void Warning_Should_BeSaved_When_InputData()
        {
            Mock<ILogRepository> mockRep = new Mock<ILogRepository>();
            mockRep.Setup(x => x.Insert(It.IsAny<p_Log>())).Verifiable();
            Logger logger = new Logger(mockRep.Object, APP_NAME);

            logger.Warning("TestMessage");
        }

        [Test]
        public void Warning_Should_ThrowArgumentException_When_MissingMessage()
        {
            Mock<ILogRepository> mockRep = new Mock<ILogRepository>();
            mockRep.Setup(x => x.Insert(It.IsAny<p_Log>())).Verifiable();
            Logger logger = new Logger(mockRep.Object, APP_NAME);

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            {
                logger.Warning(null);
            });

            Assert.AreEqual("message", ex.ParamName);
        }
        #endregion

        #region Error
        [Test]
        public void Error_Should_BeSaved_When_InputAnyExceptionWithMsgId()
        {
            Mock<ILogRepository> mockRep = new Mock<ILogRepository>();
            mockRep.Setup(x => x.Insert(It.IsAny<p_Log>())).Verifiable();
            Logger logger = new Logger(mockRep.Object, APP_NAME);

            Exception exception = new Exception("This Exception is for Test");
            logger.Error(exception, "001");
        }

        [Test]
        public void Error_Should_ThrowArgumentException_When_InputAnyExceptionWithoutMsgId()
        {
            Mock<ILogRepository> mockRep = new Mock<ILogRepository>();
            mockRep.Setup(x => x.Insert(It.IsAny<p_Log>())).Verifiable();
            Logger logger = new Logger(mockRep.Object, APP_NAME);

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            {
                Exception exception = new Exception("This Exception is for Test");
                logger.Error(exception, String.Empty);
            });

            Assert.AreEqual("msgId", ex.ParamName);
        }

        [Test]
        public void Error_Should_BeSaved_When_InputiDealExceptionWithMsgId()
        {
            Mock<ILogRepository> mockRep = new Mock<ILogRepository>();
            mockRep.Setup(x => x.Insert(It.IsAny<p_Log>())).Verifiable();
            Logger logger = new Logger(mockRep.Object, APP_NAME);

            logger.Error(_iDealException, "001");
        }

        [Test]
        public void Error_Should_BeSaved_When_InputiDealExceptionWithoutMsgId()
        {
            Mock<ILogRepository> mockRep = new Mock<ILogRepository>();
            mockRep.Setup(x => x.Insert(It.IsAny<p_Log>())).Verifiable();
            Logger logger = new Logger(mockRep.Object, APP_NAME);

            logger.Error(_iDealException, String.Empty);
        }

        [Test]
        public void Error_Should_ThrowArgumentException_When_InputExceptionIsNull()
        {
            Mock<ILogRepository> mockRep = new Mock<ILogRepository>();
            mockRep.Setup(x => x.Insert(It.IsAny<p_Log>())).Verifiable();
            Logger logger = new Logger(mockRep.Object, APP_NAME);

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            {
                logger.Error(null, "001");
            });

            Assert.AreEqual("exception", ex.ParamName);
        }
        #endregion
    }
}
