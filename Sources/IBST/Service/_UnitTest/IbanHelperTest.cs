using BND.Services.IbanStore.Service.Bll;
using BND.Services.IbanStore.Service.Dal.Pocos;
using NUnit.Framework;

namespace BND.Services.IbanStore.ServiceTest
{
    [TestFixture]
    public class IbanHelperTest
    {
        [Test]
        public void Test_CheckIBanStatus_Available_Return_True()
        {
            p_EnumIbanStatus oldStatus = p_EnumIbanStatus.Available;
            p_EnumIbanStatus newStatus = p_EnumIbanStatus.Available;

            bool result = IbanHelper.CheckIBanStatus(oldStatus, newStatus);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_CheckIBanStatus_Available_Return_False()
        {
            p_EnumIbanStatus oldStatus = p_EnumIbanStatus.Assigned;
            p_EnumIbanStatus newStatus = p_EnumIbanStatus.Available;

            bool result = IbanHelper.CheckIBanStatus(oldStatus, newStatus);
            Assert.IsFalse(result);
        }

        [Test]
        public void Test_CheckIBanStatus_Assigned_Return_True()
        {
            p_EnumIbanStatus oldStatus = p_EnumIbanStatus.Available;
            p_EnumIbanStatus newStatus = p_EnumIbanStatus.Assigned;

            bool result = IbanHelper.CheckIBanStatus(oldStatus, newStatus);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_CheckIBanStatus_Assigned_Return_False()
        {
            p_EnumIbanStatus oldStatus = p_EnumIbanStatus.Assigned;
            p_EnumIbanStatus newStatus = p_EnumIbanStatus.Assigned;

            bool result = IbanHelper.CheckIBanStatus(oldStatus, newStatus);
            Assert.IsFalse(result);
        }

        [Test]
        public void Test_CheckIBanStatus_Canceled_Return_True()
        {
            p_EnumIbanStatus oldStatus = p_EnumIbanStatus.Assigned;
            p_EnumIbanStatus newStatus = p_EnumIbanStatus.Canceled;

            bool result = IbanHelper.CheckIBanStatus(oldStatus, newStatus);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_CheckIBanStatus_Canceled_Return_False()
        {
            p_EnumIbanStatus oldStatus = p_EnumIbanStatus.Canceled;
            p_EnumIbanStatus newStatus = p_EnumIbanStatus.Canceled;

            bool result = IbanHelper.CheckIBanStatus(oldStatus, newStatus);
            Assert.IsFalse(result);
        }

        [Test]
        public void Test_CheckIBanStatus_Active_Return_True()
        {
            p_EnumIbanStatus oldStatus = p_EnumIbanStatus.Assigned;
            p_EnumIbanStatus newStatus = p_EnumIbanStatus.Active;

            bool result = IbanHelper.CheckIBanStatus(oldStatus, newStatus);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_CheckIBanStatus_Active_Return_False()
        {
            p_EnumIbanStatus oldStatus = p_EnumIbanStatus.Active;
            p_EnumIbanStatus newStatus = p_EnumIbanStatus.Active;

            bool result = IbanHelper.CheckIBanStatus(oldStatus, newStatus);
            Assert.IsFalse(result);
        }

        [Test]
        public void TestCheckIBanStatus_Available_And_OldStatus_Is_Null_Return_True()
        {
            p_EnumIbanStatus? oldStatus = null;
            p_EnumIbanStatus newStatus = p_EnumIbanStatus.Available;

            bool result = IbanHelper.CheckIBanStatus(oldStatus, newStatus);
            Assert.IsTrue(result);
        }
    }
}
