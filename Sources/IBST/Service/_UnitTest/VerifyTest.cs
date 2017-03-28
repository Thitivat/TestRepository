using BND.Services.IbanStore.Service;
using BND.Services.IbanStore.Service.Bll;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace BND.Services.IbanStore.ServiceTest
{
    [TestFixture]
    public class VerifyTest
    {
        #region CheckBBan11Proof
        [Test]
        public void Test_CheckBBan11Proof_Success()
        {
            Assert.IsTrue(Verify.CheckBBan11Proof(110322746));
        }

        [Test]
        public void Test_CheckBBan11Proof_BbanDigitLessThanZero()
        {
            Assert.Throws<IbanOperationException>(() =>
            {
                Verify.CheckBBan11Proof(-1);
            });
        }

        [Test]
        public void Test_CheckBBan11Proof_BbanInvalidDigit()
        {
            Assert.Throws<IbanOperationException>(() =>
            {
                Verify.CheckBBan11Proof(514269);
            });
        }

        #endregion

        #region CreateHash

        [Test]
        public void Test_CreateHash_Success()
        {
            byte[] bban = Encoding.UTF8.GetBytes("asdfghjkl;ewrtyuiop[dcvbnm,./wedfrtghjk,l.;/wertyuiop[sadfghjkl;xcvbnm,./");
            string bbanHash = Verify.CreateHash(bban);
            Assert.IsNotNull(bbanHash);
        }

        #endregion

        #region CheckFileFormat

        [Test]
        public void Test_CheckFileFormat_Success()
        {
            byte[] bban = Encoding.UTF8.GetBytes("110322746 833102680 178114634 525843256 120235706 287763858 ".Replace(" ", Environment.NewLine));
            List<int> result = (List<int>)Verify.CheckFileFormat(bban);
            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Count);
        }

        [Test]
        public void Test_CheckFileFormat_Fail_InvalidFormat()
        {
            byte[] bban = Encoding.UTF8.GetBytes("110322746 833102680 178114634 525a43256 120235706 287763858 ".Replace(" ", Environment.NewLine));
            Assert.Throws<IbanOperationException>(() =>
            {
                List<int> result = (List<int>)Verify.CheckFileFormat(bban);
            });
        }

        [Test]
        public void Test_CheckFileFormat_Fail_NullByteArray()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                List<int> result = (List<int>)Verify.CheckFileFormat(null);
            });
        }

        [Test]
        public void Test_CheckFileFormat_Fail_EmptyByteArray()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                List<int> result = (List<int>)Verify.CheckFileFormat(new byte[0]);
            });
        }

        #endregion
    }
}
