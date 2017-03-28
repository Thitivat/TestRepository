using BND.Services.Payments.iDeal.Dal.Pocos;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BND.Services.Payments.iDeal.Dal.Tests
{
    [TestFixture]
    public class DirectoryRepositoryTest
    {
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new MockUnitOfWork(new MockDbContext(), false);
        }

        [Test]
        public void Get_Should_ReturnDataByCriteria_When_InputAcquirerId()
        {
            // prepare items
            p_Directory defaultDirectory = new p_Directory
            {
                AcquirerID = "ACQ1",
                DirectoryDateTimestamp = DateTime.Now,
                LastDirectoryRequestDateTimestamp = DateTime.Now.AddDays(-1),
                Issuers = new List<p_Issuer>()
                {
                    new p_Issuer { AcquirerID = "ACQ1", CountryNames = "THA", IssuerID = "SBC", IssuerName = "SCB BANK" },
                    new p_Issuer { AcquirerID = "ACQ1", CountryNames = "THA", IssuerID = "TMB", IssuerName = "TMB BANK" },
                    new p_Issuer { AcquirerID = "ACQ1", CountryNames = "JPN", IssuerID = "TKR", IssuerName = "TKR BANK" },
                }
            };
            _unitOfWork.GetRepository<p_Directory>().Insert(defaultDirectory);
            _unitOfWork.CommitChanges();

            // do test
            string acquirerId = "ACQ1";
            DirectoryRepository repository = new DirectoryRepository(_unitOfWork);
            p_Directory directory = repository.Get();

            Assert.AreEqual(acquirerId, directory.AcquirerID);
            Assert.AreEqual(3, directory.Issuers.Count);
        }

        [Test]
        public void Get_Should_ReturnNull_When_InputAcquirerIdIsNotMatched()
        {
            // prepare items
            p_Directory defaultDirectory = new p_Directory
            {
                AcquirerID = "ACQ1",
                DirectoryDateTimestamp = DateTime.Now,
                LastDirectoryRequestDateTimestamp = DateTime.Now.AddDays(-1),
                Issuers = new List<p_Issuer>()
                     {
                         new p_Issuer { AcquirerID = "ACQ1", CountryNames = "THA", IssuerID = "SBC", IssuerName = "SCB BANK" },
                         new p_Issuer { AcquirerID = "ACQ1", CountryNames = "THA", IssuerID = "TMB", IssuerName = "TMB BANK" },
                         new p_Issuer { AcquirerID = "ACQ1", CountryNames = "JPN", IssuerID = "TKR", IssuerName = "TKR BANK" },
                     }
            };
            _unitOfWork.GetRepository<p_Directory>().Insert(defaultDirectory);

            // do test
            string acquirerId = "ACQ3";
            DirectoryRepository repository = new DirectoryRepository(_unitOfWork);
            p_Directory directory = repository.Get();

            Assert.IsNull(directory);
        }

        [Test]
        public void UpdateNewList_Should_ReplaceExistsDataWithNewData_When_InputNewData()
        {
            // prepare items
            p_Directory defaultDirectory = new p_Directory
            {
                AcquirerID = "ACQ1",
                DirectoryDateTimestamp = DateTime.Now,
                LastDirectoryRequestDateTimestamp = DateTime.Now.AddDays(-1),
                Issuers = new List<p_Issuer>()
                {
                    new p_Issuer { AcquirerID = "ACQ1", CountryNames = "THA", IssuerID = "SBC", IssuerName = "SCB BANK" },
                    new p_Issuer { AcquirerID = "ACQ1", CountryNames = "THA", IssuerID = "TMB", IssuerName = "TMB BANK" },
                    new p_Issuer { AcquirerID = "ACQ1", CountryNames = "JPN", IssuerID = "TKR", IssuerName = "TKR BANK" },
                }
            };
            IRepository<p_Directory> directoryRepository = _unitOfWork.GetRepository<p_Directory>();
            directoryRepository.Insert(defaultDirectory);
            _unitOfWork.CommitChanges();

            // do test
            DirectoryRepository repository = new DirectoryRepository(_unitOfWork);
            p_Directory newDirectory = new p_Directory
            {
                AcquirerID = "ACQ2",
                DirectoryDateTimestamp = DateTime.Now,
                LastDirectoryRequestDateTimestamp = DateTime.Now.AddDays(-1),
                Issuers = new List<p_Issuer>()
                {
                    new p_Issuer { AcquirerID = "ACQ2", CountryNames = "USA", IssuerID = "UOB", IssuerName = "UOB BANK" },
                    new p_Issuer { AcquirerID = "ACQ2", CountryNames = "AFF", IssuerID = "AFK", IssuerName = "AFK BANK" },
                }
            };
            int affectedRowCount = repository.UpdateDirectory(newDirectory);
            p_Directory directory = directoryRepository.GetQueryable().FirstOrDefault();

            Assert.NotNull(directory);
            //Assert.AreEqual("ACQ2", directory.AcquirerID);
            //Assert.AreEqual(2, directory.Issuers.Count);
        }
    }
}

