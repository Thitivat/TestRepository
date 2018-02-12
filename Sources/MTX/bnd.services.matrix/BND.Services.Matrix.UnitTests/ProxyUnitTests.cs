using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Proxy.Implementations;

using NUnit.Framework;

namespace BND.Services.Matrix.UnitTests
{
    /// <summary>
    /// The proxy unit tests.
    /// </summary>
    [TestFixture]
    public class ProxyUnitTests
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
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task CreateSavingsAccountAsync()
        {
            try
            {
                await
                    _proxy.CreateSavingsAccount(new SavingsFree() { Iban = "234", NominatedIban = "23423", ProductId = 987714 }).ConfigureAwait(
                        false);
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("Linked account could not be found."));
            }
        }

        /// <summary>
        /// The sweep.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task Sweep()
        {
            try
            {
                await
                    _proxy.Sweep(new Sweep()
                                     {
                                         ValueDate = DateTime.Now,
                                         Destination = "Default",
                                         Source = "Default",
                                         Description = string.Empty,
                                         Amount = 100M
                                     }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("Source and destination accounts are the same!"));
            }
        }

        /// <summary>
        /// The sweep.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task GetBalanceSystemAccounts()
        {
            try
            {
                await
                    _proxy.GetBalanceSystemAccounts(string.Empty, DateTime.Now).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("An account type has to be specified!"));
            }
        }

            /// <summary>
        /// The get accrued interest async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task GetAccruedInterestAsync()
        {
            try
            {
                await _proxy.GetAccruedInterest("NKasdf", DateTime.Now, true).ConfigureAwait(false);
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
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task GetInterestRateAsync()
        {
            try
            {
                await _proxy.GetInterestRate(
                    "NKLSDF",
                    new InterestRateOptions()
                        {
                            FromDate = DateTime.Now.AddMonths(-1),
                                                                    EndOverrideDate = DateTime.Now
                                                                }).ConfigureAwait(false);
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
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task GetalanceOverviewBAsync()
        {
            try
            {
                await _proxy.GetBalanceOverview("NLjdfh", DateTime.Now).ConfigureAwait(false);
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
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task GetTransactionsOverviewAsync()
        {
            try
            {
                await _proxy.GetTransactionsOverview("NLhga", DateTime.Now.AddMonths(-1), DateTime.Now).ConfigureAwait(false);
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
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task UnblockSavingsAccountAsync()
        {
            try
            {
                await _proxy.UnblockSavingAccounts("NLjfsdk").ConfigureAwait(false);
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
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task GetSavingsAccountOverview()
        {
            try
            {
                await _proxy.GetSavingAccountOverview("NLjdfh", DateTime.Now).ConfigureAwait(false);
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
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task BlockSavingsAccountAsync()
        {
            try
            {
                await _proxy.BlockSavingAccounts("NLjfsdk").ConfigureAwait(false);
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
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task CreateOutgoingPayment()
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
                await _proxy.CreateOutgoingPayment("NLjdfh", payment).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("Iban Not Consistent With Source Iban"));
            }
        }

        /// <summary>
        /// The get overviews.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task GetOverviews()
        {
            var type = "Default";

            try
            {
                await _proxy.GetSystemAccountOverview(type, new DateTime(2016, 06, 02)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var actualError = GetMostInnerException(ex);
                Assert.True(actualError.Message.Contains("The account number was not recognized."));
            }
        }

        /// <summary>
        /// The get System Accounts.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task GetSystemAccounts()
        {
            try
            {
                var sysAccounts = await _proxy.GetSystemAccounts().ConfigureAwait(false);

                Assert.IsNotNull(sysAccounts);
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
