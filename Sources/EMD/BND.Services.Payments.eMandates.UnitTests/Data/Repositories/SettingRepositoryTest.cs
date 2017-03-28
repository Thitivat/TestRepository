using BND.Services.Payments.eMandates.Data.Repositories;
using BND.Services.Payments.eMandates.Domain.Interfaces;
using BND.Services.Payments.eMandates.Models;
using BND.Services.Payments.eMandates.UnitTests.Data.Context;
using NUnit.Framework;
using System;

namespace BND.Services.Payments.eMandates.UnitTests.Data.Repositories
{
    [TestFixture]
    public class SettingRepositoryTest
    {
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new MockUnitOfWork(new MockDbContext(), false);
        }

        [Test]
        public void GetValueByKey_Should_ReturnValue_When_KeyExisted()
        {
            ISettingRepository repository = new SettingRepository(_unitOfWork);
            string key = "keyTest";
            string value = "ValueTest";
            Setting expectedSetting = new Setting
            {
                Key = key,
                Value = value
            };

            _unitOfWork.GetRepository<Setting>().Insert(expectedSetting);
            Assert.AreEqual(value, repository.GetValueByKey(key));
        }

        [Test]
        [TestCase("", typeof(ArgumentNullException))]
        [TestCase("NotFoundKey", typeof(ArgumentException))]
        public void GetValueByKey_Should_ThrowsException_When_KeyIsInvalid(string key, Type exception)
        {
            ISettingRepository repository = new SettingRepository(_unitOfWork);
            Assert.Throws(exception, () => repository.GetValueByKey(key));
        }
    }

}
