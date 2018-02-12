using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http.Results;

using BND.Services.Infrastructure.WebAPI.HttpActionResults;
using BND.Services.Matrix.Business.Implementations;
using BND.Services.Matrix.Business.Interfaces;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Entities.QueryStringModels;
using BND.Services.Matrix.Web.Controllers;

using NUnit.Framework;

namespace BND.Services.Matrix.UnitTests
{
    /// <summary>
    /// The create account savings unit tests.
    /// </summary>
    [TestFixture]
    public class AccountControllersUnitTests
    {
        /// <summary>
        /// The iban to test.
        /// </summary>
        private string ibanToTest;

        /// <summary>
        /// Gets or sets the matrix manager.
        /// </summary>
        protected IMatrixManager MatrixManager { get; set; }

        /// <summary>
        /// Gets or sets the accounts controller.
        /// </summary>
        protected AccountsController AccountsController { get; set; }

        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public virtual void Setup()
        {
            MatrixManager = new MatrixManager();
            AccountsController = new AccountsController(MatrixManager);
            ibanToTest = "NL03BNDA0864284276";
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            MatrixManager = null;
            AccountsController.Dispose();
            ibanToTest = null;
        }

        /// <summary>
        /// The open savings account.
        /// </summary>
        [Test]
        public void OpenSavingsAccount()
        {
            // Not completed, has to be amended in the future
            try
            {
                var newAccount = new SavingsFree()
                                     {
                                         DepartmentName = ConfigurationManager.AppSettings.Get("MatrixDepartmentName"),
                                         UnitName = ConfigurationManager.AppSettings.Get("MatrixUnitName"),
                                         Iban = ibanToTest,
                                         NominatedIban = ibanToTest
                                     };
                var response = AccountsController.CreateSavingsAccount(newAccount);

                Assert.IsInstanceOf(typeof(CreatedNegotiatedContentResult<string>), response);
            }
            catch (Exception)
            {
                // There already exists a service with the given external Id.
                Assert.Fail();
            }
        }

        /// <summary>
        /// The open savings account.
        /// </summary>
        [Test]
        public void GetInterestRate()
        {
            var options = new InterestRateOptions()
            {
                EndOverrideDate = new DateTime(2016, 1, 1),
                FromDate = new DateTime(2015, 1, 1)
            };
            var result = AccountsController.GetInterestRate(ibanToTest, options);

            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<List<Entities.InterestRate>>), result);
        }

        /// <summary>
        /// The get overview balance.
        /// </summary>
        [Test]
        public void GetBalanceOverview()
        {
            var result = AccountsController.GetBalanceOverview(ibanToTest, new DateTime(2016, 06, 02));

            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<decimal>), result);
        }

        /// <summary>
        /// The get overview transactions.
        /// </summary>
        [Test]
        public void GetTransactionsOverview()
        {
            var result = AccountsController.GetMovements(ibanToTest, new DateTime(2016, 01, 02));

            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<List<Entities.MovementItem>>), result);
        }

        /// <summary>
        /// The get overview balance.
        /// </summary>
        [Test]
        public void GetAccruedInterest()
        {
            var result = AccountsController.GetAccruedInterest(ibanToTest, new DateTime(2016, 06, 02), true);

            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<decimal>), result);
        }

        /// <summary>
        /// The unblock account.
        /// </summary>
        [Test]
        public void UnblockAccount()
        {
            // Not completed, has to be amended in the future
            try
            {
               var response = AccountsController.UnblockSavingAccounts(ibanToTest);

               Assert.IsInstanceOf(typeof(NoContentResult), response);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// The get saving accounts overview.
        /// </summary>
        [Test]
        public void GetSavingAccountsOverview()
        {
            var result = AccountsController.GetAccountOverview(ibanToTest, new DateTime(2016, 06, 02));

            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<AccountOverview>), result);
        }


        /// <summary>
        /// The block account.
        /// </summary>
        [Test]
        public void BlockAccount()
        {
            // Not completed, has to be amended in the future
            try
            {
                 var response = AccountsController.BlockSavingAccounts(ibanToTest);

                 Assert.IsInstanceOf(typeof(NoContentResult), response);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }


   /// <summary>
        /// The create outgoing payment.
        /// </summary>
        [Test]
        public void CreateOutgoingPayment()
        {
            var payment = new Payment()
            {
                SourceIBAN = "NL03BNDA0864284276",
                CounterpartyIBAN = "NL03BNDA0864284277",
                CounterpartyBIC = "blabla",
                Amount = 10,
                ValueDate = new DateTime(2016, 09, 09),
                Reference = "refer",
                DebtorDetails =
                    new DebtorCreditorDetails()
                    {
                        Name = "name",
                        Postcode = "30",
                        Street = "street",
                        City = "city",
                        CountryCode = "NL"
                    },
                CreditorDetails =
                    new DebtorCreditorDetails()
                    {
                        Name = "name",
                        Postcode = "30",
                        Street = "street",
                        City = "city",
                        CountryCode = "NL"
                    }
            };
            var result = AccountsController.CreateOutgoingPayment(ibanToTest, payment);

            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<string>), result);
        }
    }
}
