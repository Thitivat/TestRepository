using BND.Services.Payments.iDeal.ClientData.Dal.Interfaces;
using BND.Services.Payments.iDeal.ClientData.Dal.Pocos;
using BND.Services.Payments.iDeal.ClientData.Dal.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.iDeal.ClientData.Tests
{
    [TestFixture]
    public class ClientUserRepositoryTest
    {
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new MockUnitOfWork(new MockDbContext(), false);
        }

        //[Test]
        //public void RealTest_GetClientName()
        //{
        //    IUnitOfWork unitOfWork = new ClientDataUnitOfWork(
        //        "Data Source=192.168.7.59;Initial Catalog=dev_bnd;Persist Security Info=True;User ID=CBOUser;Password=DJKFH!489378djk$hFjkhsrw375", false);
        //    IClientUserRepository repo = new ClientUserRepository(unitOfWork);
        //    string result = repo.GetClientName("NL37BNDA4818136652");

        //    Assert.AreEqual(String.Empty, result);
        //}

        [Test]
        public void GetClientName_Should_ReturnData_When_FoundData()
        {
            // prepare database       
            var productRepository = _unitOfWork.GetRepository<p_Product>();
            productRepository.Insert(new p_Product
            {
                ProductGroup = "Product Group",
                ClientType = "Client Type",
                ProductFlowStatus = "Product Flow Status",
                ProductType = "ProductType",
                EncryptedPublicKey = "EncryptedPublicKey",
                ProductBankAccounts = new List<p_ProductBankAccount>
                {
                    new p_ProductBankAccount
                    {
                        MatrixIban = "NL77BNDA6370233804",
                        InsertedBy = "InsertedBy1",
                        UpdatedBy = "UpdatedBy1",
                    },
                    new p_ProductBankAccount
                    {
                        MatrixIban = "NL77BNDA6370233805",
                        InsertedBy = "InsertedBy2",
                        UpdatedBy = "UpdatedBy2",
                    },
                    new p_ProductBankAccount
                    {
                        MatrixIban = "NL77BNDA6370233806",
                        InsertedBy = "InsertedBy3",
                        UpdatedBy = "UpdatedBy3",
                    },
                },
                ProductClients = new List<p_ProductClient>
                {
                    new p_ProductClient
                    {
                        ClientUserType = "ClientUserType1",
                        ClientUser = new p_ClientUser
                        {
                            FirstName = "John",
                            LastNamePrefix = "Pre",
                            LastName = "Doe",
                            InsertedBy = "InsertedBy1",
                            UpdatedBy = "UpdatedBy1",
                        },
                    },
                    new p_ProductClient
                    {
                        ClientUserType = "ClientUserType2",
                        ClientUser = new p_ClientUser
                        {
                            FirstName = "Jane",
                            LastNamePrefix = "Pre",
                            LastName = "Doe",
                            InsertedBy = "InsertedBy2",
                            UpdatedBy = "UpdatedBy2",
                        },
                    },
                }
            });
            _unitOfWork.CommitChanges();

            // call method
            IClientUserRepository repo = new ClientUserRepository(_unitOfWork);
            string result = repo.GetClientName("NL77BNDA6370233804");

            Assert.AreEqual("John Pre Doe", result);
        }

        [Test]
        public void GetClientName_Should_ReturnEmpty_When_DataNotFounded()
        {
            IClientUserRepository repo = new ClientUserRepository(_unitOfWork);
            string result = repo.GetClientName("NL77BNDA6370233804");

            Assert.AreEqual(String.Empty, result);
        }
    }
}
