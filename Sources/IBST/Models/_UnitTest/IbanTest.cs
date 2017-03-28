using System;
using NUnit.Framework;
using BND.Services.IbanStore.Models;

namespace BND.Services.IbanStore.ModelsTest
{
    [TestFixture]
    public class IbanTest
    {
        private string _countryCode = "THA";
        private string _bankCode = "ABNBANK";
        /// <summary>
        /// The _bban code
        /// </summary>
        private string _bbanCode = "123456789";

        #region [Constructor]
        [Test]
        public void Test_Iban_Success()
        {
            // test first constructor.
            Iban iban = new Iban(_countryCode, _bankCode, _bbanCode);
            Assert.AreEqual(_countryCode, iban.CountryCode);
            Assert.AreEqual(_bankCode, iban.BankCode);
            Assert.AreEqual(_bbanCode, iban.Bban);

            string checkSum = iban.CheckSum;
            // test second constructor.
            Iban iban2 = new Iban(_countryCode, _bankCode, _bbanCode, checkSum);
            Assert.AreEqual(_countryCode, iban2.CountryCode);
            Assert.AreEqual(_bankCode, iban2.BankCode);
            Assert.AreEqual(_bbanCode, iban2.Bban);
            Assert.AreEqual(checkSum, iban2.CheckSum);
            Assert.AreEqual(String.Format("{0}{1}{2}0{3}", _countryCode, checkSum.ToString(), _bankCode, _bbanCode), iban2.Code);

            // test second constructor with checksum one digit.
            checkSum = "5";
            Iban iban3 = new Iban(_countryCode, _bankCode, _bbanCode, checkSum);
            Assert.AreEqual(_countryCode, iban3.CountryCode);
            Assert.AreEqual(_bankCode, iban3.BankCode);
            Assert.AreEqual(_bbanCode, iban3.Bban);
            Assert.AreEqual("0" + checkSum, iban3.CheckSum);
            Assert.AreEqual(String.Format("{0}{1}{2}0{3}", _countryCode, "0" + checkSum, _bankCode, _bbanCode), iban3.Code);

            // test second constructor with checksum two digits.
            // declare parameters
            string countryCode = "NL";
            string chkSum = "55";
            string bankCode = "BNDA";
            string bban = "713367733";
            Iban iban4 = new Iban(countryCode, bankCode, bban, chkSum);
            Assert.AreEqual(countryCode, iban4.CountryCode);
            Assert.AreEqual(bankCode, iban4.BankCode);
            Assert.AreEqual(bban, iban4.Bban);
            Assert.AreEqual(chkSum, iban4.CheckSum);
            Assert.AreEqual(String.Format("{0}{1}{2}0{3}", countryCode, chkSum, bankCode, bban), iban4.Code);
        }
        #endregion

        #region [IbanSplit]
        [Test]
        public void Test_IbanSplit_Success()
        {
            string ibanString = "NL55BNDA0713367733";
            Iban iban = new Iban(ibanString);

            // expected values
            string countryCode = "NL";
            string checkSum = "55";
            string bankCode = "BNDA";
            string bban = "713367733";

            Assert.AreEqual(countryCode, iban.CountryCode);
            Assert.AreEqual(checkSum, iban.CheckSum);
            Assert.AreEqual(bankCode, iban.BankCode);
            Assert.AreEqual(bban, iban.Bban);
        }
        #endregion

        #region [IbanValidate]
        [Test]
        public void Test_IbanValidate_Fail_Invalid_Iban_Country()
        {
            // parameter 
            string fakeIban = "0045BNDA0110322746";

            Assert.Throws<ArgumentException>(() => { new Iban(fakeIban); });
        }

        [Test]
        public void Test_IbanValidate_Fail_Invalid_Iban_Country_Length()
        {
            // parameter 
            string fakeIban = "NL45BNDA01103227460";
            
            Assert.Throws<ArgumentException>(() => { new Iban(fakeIban); });
        }


        [Test]
        public void Test_IbanValidate_Fail_Invalid_Iban_Structure()
        {
            // parameter 
            string fakeIban = "NL450NDA0110322746";
            
            Assert.Throws<ArgumentException>(() => { new Iban(fakeIban); });
        }

        [Test]
        public void Test_IbanValidate_Fail_Invalid_Iban_Format()
        {
            // parameter 
            string fakeIban = "NL45BNDA0110322746";

            Assert.Throws<ArgumentException>(() => { new Iban(fakeIban); });
        }
        #endregion
    }
}
