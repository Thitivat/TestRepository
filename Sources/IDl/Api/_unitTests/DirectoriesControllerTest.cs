using BND.Services.Payments.iDeal.Api.Controllers;
using BND.Services.Payments.iDeal.Interfaces;
using BND.Services.Payments.iDeal.Models;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace BND.Services.Payments.iDeal.Api.Tests
{
    [TestFixture]
    public class DirectoriesControllerTest
    {
        public Mock<IiDealService> idealMock = new Mock<IiDealService>();
        public Mock<BND.Services.Payments.iDeal.Interfaces.ILogger> iLoggerMock = new Mock<BND.Services.Payments.iDeal.Interfaces.ILogger>();
        List<DirectoryModel> listDirectory = new List<DirectoryModel>()
            {
                new DirectoryModel
                {
                    CountryNames = "Country Name1",
                    Issuers = new List<IssuerModel>
                    {
                        new IssuerModel
                        {
                            IssuerID = "1",
                            IssuerName = "Issuer1"
                        },
                        new IssuerModel
                        {
                            IssuerID = "2",
                            IssuerName = "Issuer2"
                        }
                    }
                },
                new DirectoryModel
                {
                    CountryNames = "Country Name2",
                    Issuers = new List<IssuerModel>
                    {
                        new IssuerModel
                        {
                            IssuerID = "3",
                            IssuerName = "Issuer3"
                        },
                        new IssuerModel
                        {
                            IssuerID = "4",
                            IssuerName = "Issuer4"
                        }
                    }
                }
                
            };

        [Test]
        public void GetDirectory_Should_ReturnTrue_When_ListIsNotNull()
        {
            idealMock.Setup(service => service.GetDirectories()).Returns(listDirectory);
            var controller = new DirectoriesController(idealMock.Object, iLoggerMock.Object);
            var result = controller.Get();
            var actualModel = result as OkNegotiatedContentResult<IEnumerable<DirectoryModel>>;

            Assert.IsNotNull(actualModel);
            Assert.IsTrue(listDirectory.Equals(actualModel.Content));
        }

        [Test]
        public void GetDirectory_Should_ReturnAllIssuers_When_ListAll()
        {
            idealMock.Setup(service => service.GetDirectories()).Returns(listDirectory);
            var controller = new DirectoriesController(idealMock.Object, iLoggerMock.Object);
            var result = controller.Get();
            var actualModel = result as OkNegotiatedContentResult<IEnumerable<DirectoryModel>>;

            Assert.AreSame(listDirectory, actualModel.Content);
        }

        [Test]
        public void GetDirectory_Should_ReturnNull_When_ListIsNull()
        {
            Mock<IiDealService> idealMock = new Mock<IiDealService>();
            idealMock.Setup(service => service.GetDirectories()).Returns(() => null);
            var controller = new DirectoriesController(idealMock.Object, iLoggerMock.Object);
            var result = controller.Get();
            var actualModel = result as OkNegotiatedContentResult<IEnumerable<DirectoryModel>>;

            Assert.IsNull(actualModel.Content);
        }

        [Test]
        public void GetDirectory_Should_ReturnEmpty_When_ListIsEmpty()
        {
            Mock<IiDealService> idealMock = new Mock<IiDealService>();
            idealMock.Setup(service => service.GetDirectories()).Returns(new List<DirectoryModel>());
            var controller = new DirectoriesController(idealMock.Object, iLoggerMock.Object);
            var result = controller.Get();
            var actualModel = result as OkNegotiatedContentResult<IEnumerable<DirectoryModel>>;

            Assert.IsEmpty(actualModel.Content);
        }
    }
}
