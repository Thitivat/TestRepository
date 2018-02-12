using System;
using System.Collections.Generic;
using System.Configuration;

using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Proxy.NET4.Implementations;

using NUnit.Framework;

namespace BND.Services.Matrix.UnitTests
{
    /// <summary>
    /// The proxy unit tests.
    /// </summary>
    [TestFixture]
    public class ProxyNET4UnitTests
    {
        /// <summary>
        /// The _proxy.
        /// </summary>
        private AccountsApi _proxy;

        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _proxy = new AccountsApi(new AccountsApiConfig(ConfigurationManager.AppSettings["BaseServiceUrl"]));
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _proxy = null;
        }

        /// <summary>
        /// The create savings account unittest.
        /// </summary>
        [Test]
        public void CreateSavingsAccount()
        {
            try
            {
                _proxy.CreateSavingsAccount(new SavingsFree() { Iban = "234", NominatedIban = "23423", ProductId = 987714 });
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("Linked account could not be found."));
            }
        }

        /// <summary>
        /// The get accrued interest async.
        /// </summary>
        [Test]
        public void GetAccruedInterest()
        {
            try
            {
                _proxy.GetAccruedInterest("NKasdf", DateTime.Now, true);
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("The account number was not recognized."));
            }
        }

        /// <summary>
        /// The get interest rate async.
        /// </summary>
        [Test]
        public void GetInterestRate()
        {
            try
            {
                _proxy.GetInterestRate(
                    "NKLSDF",
                    new InterestRateOptions()
                    {
                        FromDate = DateTime.Now.AddMonths(-1),
                        EndOverrideDate = DateTime.Now
                    });
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("The account number was not recognized."));
            }
        }

        /// <summary>
        /// The get overview balance async.
        /// </summary>
        [Test]
        public void GetOverviewBalance()
        {
            try
            {
                _proxy.GetOverviewBalance("NLjdfh", DateTime.Now);
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("The account number was not recognized."));
            }
        }

        /// <summary>
        /// The get transactions overview async.
        /// </summary>
        [Test]
        public void GetTransactionsOverview()
        {
            try
            {
                _proxy.GetTransactionsOverview("NLhga", DateTime.Now.AddMonths(-1), DateTime.Now);
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("The account number was not recognized."));
            }
        }

        /// <summary>
        /// The unblock savings account async.
        /// </summary>
        [Test]
        public void UnblockSavingsAccount()
        {
            try
            {
                _proxy.UnblockSavingAccounts("NLjfsdk");
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("The account number was not recognized."));
            }
        }

        /// <summary>
        /// The get savings account overview.
        /// </summary>
        [Test]
        public void GetSavingsAccountOverview()
        {
            try
            {
                _proxy.GetSavingAccountOverview("NLjdfh", DateTime.Now);
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("The account number was not recognized."));
            }
        }

        /// <summary>
        /// The block savings account async.
        /// </summary>
        [Test]
        public void BlockSavingsAccountAsync()
        {
            try
            {
                _proxy.BlockSavingAccounts("NLjfsdk");
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("The account number was not recognized."));
            }
        }

        /// <summary>
        /// The create outgoing payment.
        /// </summary> 
        [Test]
        public void CreateOutgoingPayment()
        {
            try
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
                            CountryCode = 10
                        },
                    CreditorDetails =
                        new DebtorCreditorDetails()
                        {
                            Name = "name",
                            Postcode = "30",
                            Street = "street",
                            City = "city",
                            CountryCode = 10
                        }
                };
                _proxy.CreateOutgoingPayment("NLjdfh", payment);
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("Iban Not Consistent With Source Iban"));
            }
        }

        /// <summary>
        /// The get movements.
        /// </summary> 
        [Test]
        public void GetMovements()
        {
            try
            {
                _proxy.GetMovements("Default", DateTime.Now, DateTime.Now.AddMilliseconds(50));
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("The account number was not recognized"));
            }
        }

        /// <summary>
        /// The sweep.
        /// </summary> 
        [Test]
        public void Sweep()
        {
            var sweep = new Sweep
                            {
                                Source = "Default",
                                Destination = "Error",
                                Amount = 434483,
                                ValueDate = DateTime.Now,
                                Description = "gbukwg"
                            };
            try
            {
                _proxy.Sweep(sweep);
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("The account number was not recognized")); 
            }
        }

        /// <summary>
        /// The get balance.
        /// </summary> 
        [Test]
        public void GetBalance()
        {
            try
            {
                _proxy.GetBalanceSystemAccounts("Nostro", DateTime.Now);
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("The account number was not recognized")); 
            }
        }

        /// <summary>
        /// The get overviews.
        /// </summary>
        [Test]
        public void GetOverviews()
        {
            var types = new List<string>();
            types.Add("Error");
            types.Add("Default");

            try
            {
                _proxy.GetSystemAccountsOverviews(types, new DateTime(2016, 06, 02));
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("The account number was not recognized."));
            }
        }

        /// <summary>
        /// The get most inner exception.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        private static Exception GetMostInnerException(Exception ex)
        {
            return ex.InnerException == null ? ex : GetMostInnerException(ex.InnerException);
        }
    }
}
