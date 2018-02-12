using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

using BND.Services.Matrix.Business.Implementations;
using BND.Services.Matrix.Business.Interfaces;
using BND.Services.Matrix.Web.Controllers;

using NUnit.Framework;

namespace BND.Services.Matrix.UnitTests
{
    [TestFixture]
    public class GetAccruedInterestUnitTest
    {
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
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            MatrixManager = null;
            AccountsController.Dispose();
        }

        /// <summary>
        /// The get overview balance.
        /// </summary>
        [Test]
        public void GetAccruedInterest()
        {
            var result = AccountsController.GetAccruedInterest("NL03BNDA0864284276", new DateTime(2016, 06, 02), true);
            
            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<decimal>), result);
        }
    }
}
