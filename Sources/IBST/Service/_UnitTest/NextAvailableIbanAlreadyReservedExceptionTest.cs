using BND.Services.IbanStore.Models;
using BND.Services.IbanStore.Service.Bll;
using System;
using NUnit.Framework;

namespace BND.Services.IbanStore.ServiceTest
{
    [TestFixture]
    public class NextAvailableIbanAlreadyReservedExceptionTest
    {
        [Test]
        public void Test_Constructor_Case1()
        {
            // declare Iban object
            Iban iban = new Iban("NL","BNDA","110322746")
            {
                IbanId = 1,
                BbanFileName ="FileName",
                CurrentIbanHistory = new IbanHistory 
                {
                    Id = 1,
                    Context = "context",
                    ChangedBy ="user",
                    ChangedDate = DateTime.Now,
                    Remark="remark",
                    Status = "Available"
                }
            };
            NextAvailableIbanAlreadyReservedException exception = new NextAvailableIbanAlreadyReservedException(iban);
            Assert.IsNotNull(exception);
            Assert.IsNull(exception.InnerException);
            Assert.IsNotNull(exception.ReservedIban);
            Assert.AreEqual(iban.BankCode, exception.ReservedIban.BankCode);
            Assert.AreEqual(iban.Bban, exception.ReservedIban.Bban);
            Assert.AreEqual(iban.BbanFileName, exception.ReservedIban.BbanFileName);
            Assert.AreEqual(iban.CheckSum, exception.ReservedIban.CheckSum);
            Assert.AreEqual(iban.Code, exception.ReservedIban.Code);
            Assert.AreEqual(iban.CountryCode, exception.ReservedIban.CountryCode);
            Assert.AreEqual(iban.CurrentIbanHistory, exception.ReservedIban.CurrentIbanHistory);
            Assert.AreEqual(iban.IbanId, exception.ReservedIban.IbanId);
            Assert.AreEqual(iban.ReservedTime, exception.ReservedIban.ReservedTime);
            Assert.AreEqual(iban.Uid, exception.ReservedIban.Uid);
            Assert.AreEqual(iban.UidPrefix, exception.ReservedIban.UidPrefix);
        }

        [Test]
        public void Test_Constructor_Case2()
        {
            // declare Iban object
            Iban iban = new Iban("NL", "BNDA", "110322746")
            {
                IbanId = 1,
                BbanFileName = "FileName",
                CurrentIbanHistory = new IbanHistory
                {
                    Id = 1,
                    Context = "context",
                    ChangedBy = "user",
                    ChangedDate = DateTime.Now,
                    Remark = "remark",
                    Status = "Available"
                }
            };
            string errorMessage = "This is an error message";

            NextAvailableIbanAlreadyReservedException exception = new NextAvailableIbanAlreadyReservedException(iban, errorMessage);
            Assert.IsNotNull(exception);
            Assert.IsNull(exception.InnerException);
            Assert.IsNotNull(exception.ReservedIban);
            Assert.AreEqual(iban.BankCode, exception.ReservedIban.BankCode);
            Assert.AreEqual(iban.Bban, exception.ReservedIban.Bban);
            Assert.AreEqual(iban.BbanFileName, exception.ReservedIban.BbanFileName);
            Assert.AreEqual(iban.CheckSum, exception.ReservedIban.CheckSum);
            Assert.AreEqual(iban.Code, exception.ReservedIban.Code);
            Assert.AreEqual(iban.CountryCode, exception.ReservedIban.CountryCode);
            Assert.AreEqual(iban.CurrentIbanHistory, exception.ReservedIban.CurrentIbanHistory);
            Assert.AreEqual(iban.IbanId, exception.ReservedIban.IbanId);
            Assert.AreEqual(iban.ReservedTime, exception.ReservedIban.ReservedTime);
            Assert.AreEqual(iban.Uid, exception.ReservedIban.Uid);
            Assert.AreEqual(iban.UidPrefix, exception.ReservedIban.UidPrefix);
            Assert.AreEqual(errorMessage, exception.Message);
        }

        [Test]
        public void Test_Constructor_Case3()
        {
            // declare Iban object
            Iban iban = new Iban("NL", "BNDA", "110322746")
            {
                IbanId = 1,
                BbanFileName = "FileName",
                CurrentIbanHistory = new IbanHistory
                {
                    Id = 1,
                    Context = "context",
                    ChangedBy = "user",
                    ChangedDate = DateTime.Now,
                    Remark = "remark",
                    Status = "Available"
                }
            };
            string errorMessage = "This is an error message";
            ArgumentException expectedEx = new ArgumentException();

            NextAvailableIbanAlreadyReservedException exception = new NextAvailableIbanAlreadyReservedException(iban, errorMessage, expectedEx);
            Assert.IsNotNull(exception);
            Assert.IsNotNull(exception.ReservedIban);
            Assert.AreEqual(iban.BankCode, exception.ReservedIban.BankCode);
            Assert.AreEqual(iban.Bban, exception.ReservedIban.Bban);
            Assert.AreEqual(iban.BbanFileName, exception.ReservedIban.BbanFileName);
            Assert.AreEqual(iban.CheckSum, exception.ReservedIban.CheckSum);
            Assert.AreEqual(iban.Code, exception.ReservedIban.Code);
            Assert.AreEqual(iban.CountryCode, exception.ReservedIban.CountryCode);
            Assert.AreEqual(iban.CurrentIbanHistory, exception.ReservedIban.CurrentIbanHistory);
            Assert.AreEqual(iban.IbanId, exception.ReservedIban.IbanId);
            Assert.AreEqual(iban.ReservedTime, exception.ReservedIban.ReservedTime);
            Assert.AreEqual(iban.Uid, exception.ReservedIban.Uid);
            Assert.AreEqual(iban.UidPrefix, exception.ReservedIban.UidPrefix);
            Assert.AreEqual(errorMessage, exception.Message);
            Assert.IsTrue(exception.InnerException is ArgumentException);
        }
    }
}
