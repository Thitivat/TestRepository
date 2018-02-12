using System;
using System.Collections.Generic;
using System.Web.Http.Results;

using BND.Services.Matrix.Business.Implementations;
using BND.Services.Matrix.Business.Interfaces;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Web.Controllers;

using NUnit.Framework;

namespace BND.Services.Matrix.UnitTests
{
    [TestFixture]
    public class SystemAccountsControllerUnitTests
    {
        /// <summary>
        /// Gets or sets the matrix manager.
        /// </summary>
        protected IMatrixManager MatrixManager { get; set; }

        /// <summary>
        /// Gets or sets the system accounts controller.
        /// </summary>
        protected SysAccountsController SysAccountsController { get; set; }

        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public virtual void Setup()
        {
            MatrixManager = new MatrixManager();
            SysAccountsController = new SysAccountsController(MatrixManager);
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            MatrixManager = null;
            SysAccountsController.Dispose();
        }

        /// <summary>
        /// The Sweep.
        /// </summary>
        [Test]
        public void Sweep()
        {
            var result =
                SysAccountsController.Sweep(
                    new Sweep()
                        {
                            Amount = 100M,
                            ValueDate = DateTime.Now,
                            Destination = "Default",
                            Source = "Error",
                            Description = "test"
                        });

            Assert.IsInstanceOf(typeof(CreatedNegotiatedContentResult<decimal>), result);
        }

        /// <summary>
        /// The test get movements.
        /// </summary>
        [Test]
        public void TestGetMovements()
        {
            var result = SysAccountsController.GetMovements("Default", DateTime.Now, DateTime.Now.AddDays(30));
            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<MovementOverviewItem>), result);
        }


        /// <summary>
        /// The GetSystemAccountsOverview.
        /// </summary>
        [Test]
        public void GetBalanceSystemAccounts()
        {
            var result =
                SysAccountsController.GetBalanceOverview("Nostro", DateTime.Now);

            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<decimal>), result);
        }

        /// <summary>
        /// The GetSystemAccountsOverview.
        /// </summary>
        [Test]
        public void GetSystemAccountsOverview()
        {
            var result =
                SysAccountsController.GetAccountOverview("Default", DateTime.Now);

            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<AccountOverview>), result);
        }
    }
}
