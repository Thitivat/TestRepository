using BND.Services.Payments.iDeal.Dal.Pocos;
using NUnit.Framework;
using System;

namespace BND.Services.Payments.iDeal.Dal.Tests
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
            p_Setting expectedSetting = new p_Setting
            {
                Key = key,
                Value = value
            };

            _unitOfWork.GetRepository<p_Setting>().Insert(expectedSetting);
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
