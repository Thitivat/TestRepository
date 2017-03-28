using Bndb.Kyc.SanctionLists.Entities;
using Bndb.Kyc.SanctionLists.SanctionListsBll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bndb.Kyc.SanctionLists.SanctionListsBllTest
{
    [TestClass]
    public class SanctionListsBllTest
    {
        #region[field]

        private const string _connectionString = @"Data Source=.;Initial Catalog=Bndb.Kyc.SanctionLists.SanctionListsDb;Integrated Security=True";

        private static Bndb.Kyc.SanctionLists.SanctionListsBll.SanctionListsBll _sactionListBll;

        private static NameAlias _nameAlias = new NameAlias
        {
            EntityId = 1541,
            FirstName = "",
            Function = "",
            Gender = new Gender(),
            Language = new Language(),
            LastName = "",
            MiddleName = "",
            NameAliasId = 441,
            OriginalNameAliasId = 4811,
            PrefixName = "Sir",
            Quality = 0,
            Regulation = new Regulation
            {
                Programme = "program",
                PublicationDate = DateTime.UtcNow,
                PublicationTitle = "Title",
                PublicationUrl = "url",
                RegulationDate = DateTime.UtcNow,
                RegulationId = 22,
                RegulationTitle = "title",
                Remark = new Remark
                {
                    RemarkId = 1,
                    Value = "testRemark"
                },
                ListType = new ListType
                {
                    Description = "Simple ListType",
                    ListTypeId = 47101,
                    Name = "Abnormal"
                }
            },
            Remark = new Remark { RemarkId = 11, Value = "" },
            Title = "",
            WholeName = ""
        };

        private static Entity _entity = new Entity
        {
            Addresses = new List<Address>
                {
                    new Address
                    {
                        AddressId = 2,
                        City = "Bangkok",
                        Country = new Country
                        {
                            Iso2 = "123",
                            Iso3 = "123",
                            Name = "name",
                            NiceName = "nicename",
                            NumCode = null,
                            PhoneCode = null
                        },
                        Number = "10110",
                        OriginalAddressId = 12345,
                        Regulation = new Regulation
                        {
                            Programme = "program",
                            PublicationDate = DateTime.UtcNow,
                            PublicationTitle = "Title",
                            PublicationUrl = "url",
                            RegulationDate = DateTime.UtcNow,
                            RegulationId = 22,
                            RegulationTitle = "title",
                            Remark = new Remark
                            {
                                RemarkId = 1,
                                Value = "testRemark"
                            }
                        },
                        Remark = new Remark
                        {
                            RemarkId =2,
                            Value = "12345"
                        },
                        Street = "",
                        Zipcode = ""
                    }
                },
            Banks = new List<Bank>
                {
                    new Bank
                    {
                        BankId = 1234,
                        BankName = "BankName",
                        Iban = "iban",
                        Remark = new Remark
                        {
                            RemarkId = 1,
                            Value = "testRemark"
                        },
                        AccountNumber = "123443234567",
                        AccountHolderName = "Holdername"
                    }
                },
            Births = new List<Birth>
                {
                    new Birth
                    {
                        BirthId = 123,
                        Country = new Country
                        {
                            Iso2 = "123",
                            Iso3 = "123",
                            Name = "name",
                            NiceName = "nicename",
                            NumCode = null,
                            PhoneCode = null
                        },
                        Day = 23,
                        Month = 2,
                        Year = 1969,
                        EntityId = 23,
                        OriginalBirthId = 32,
                        Place = "",
                        Regulation = new Regulation
                        {
                            Programme = "program",
                            PublicationDate = DateTime.UtcNow,
                            PublicationTitle = "Title",
                            PublicationUrl = "url",
                            RegulationDate = DateTime.UtcNow,
                            RegulationId = 22,
                            RegulationTitle = "title",
                            Remark = new Remark
                            {
                                RemarkId = 1,
                                Value = "testRemark"
                            }
                        },
                        Remark = new Remark
                        {
                            RemarkId = 1,
                            Value = "testRemark"
                        }
                    }
                },
            Citizenships = new List<Citizenship>
                {
                    new Citizenship
                    {
                        CitizenshipId = 2314,
                        Country = new Country
                        {
                            Iso2 = "123",
                            Iso3 = "123",
                            Name = "name",
                            NiceName = "nicename",
                            NumCode = null,
                            PhoneCode = null
                        },
                        EntityId = 23,
                        OriginalCitizenshipId = 45,
                        Regulation = new Regulation
                        {
                            Programme = "program",
                            PublicationDate = DateTime.UtcNow,
                            PublicationTitle = "Title",
                            PublicationUrl = "url",
                            RegulationDate = DateTime.UtcNow,
                            RegulationId = 22,
                            RegulationTitle = "title",
                            Remark = new Remark
                            {
                                RemarkId = 1,
                                Value = "testRemark"
                            }
                        },
                        Remark = new Remark
                        {
                            RemarkId = 1,
                            Value = "testRemark"
                        }
                    }
                },
            ContactInfo = new List<ContactInfo>
                {
                    new ContactInfo
                    {
                        ContactInfoId = 43,
                        ContactInfoType = new ContactInfoType
                        {
                            ContactInfoTypeId = 53,
                            Name = "name"
                        },
                        EntityId = 23,
                        OriginalContactInfoId = 4,
                        Regulation = new Regulation
                        {
                            Programme = "program",
                            PublicationDate = DateTime.UtcNow,
                            PublicationTitle = "Title",
                            PublicationUrl = "url",
                            RegulationDate = DateTime.UtcNow,
                            RegulationId = 22,
                            RegulationTitle = "title",
                            Remark = new Remark
                            {
                                RemarkId = 1,
                                Value = "testRemark"
                            }
                        },
                        Remark = new Remark
                        {
                            RemarkId = 1,
                            Value = "testRemark"
                        },
                        Value = "value"
                    }
                },
            EntityId = 232,
            Identifications = new List<Identification>
                {
                    new Identification 
                    {
                        IssueCountry = new Country
                        {
                            Iso2 = "123",
                            Iso3 = "123",
                            Name = "name",
                            NiceName = "nicename",
                            NumCode = null,
                            PhoneCode = null
                        },
                        DocumentNumber = "1234",
                        EntityId = 234,
                        IdentificationId = 42,
                        IdentificationType = new IdentificationType
                        {
                            IdentificationTypeId = 1,
                            Name = "name",
                            Description = "description"
                        },
                        IssueCity = "city",
                        IssueDate = null,
                        OriginalIdentificationId = 342,
                        Regulation = new Regulation
                        {
                            Programme = "program",
                            PublicationDate = DateTime.UtcNow,
                            PublicationTitle = "Title",
                            PublicationUrl = "url",
                            RegulationDate = DateTime.UtcNow,
                            RegulationId = 22,
                            RegulationTitle = "title",
                            Remark = new Remark
                            {
                                RemarkId = 1,
                                Value = "testRemark"
                            }
                        },
                        Remark = new Remark
                        {
                            RemarkId = 1,
                            Value = "testRemark"
                        }
                    }
                },
            ListArchiveId = 1,
            ListType = new ListType
            {
                Name = "",
                ListTypeId = 1,
                Description = "description"
            },
            NameAliases = new List<NameAlias>
                {
                    new NameAlias
                    {
                        FirstName = "name",
                        LastName = "lastName",
                        MiddleName = "middleName",
                        EntityId = 42,
                        Function = "function",
                        Gender = new Gender
                        {
                            GenderId = 1,
                            Name = "Gender"
                        },
                        Language = new Language
                        {
                            Iso2 = "iso",
                            Iso3 = "iso",
                            Name = "name"
                        },
                        NameAliasId = 3,
                        OriginalNameAliasId = 123,
                        PrefixName = "Mr",
                        Quality = 2,
                        Title = "Title",
                        Regulation = new Regulation
                        {
                            Programme = "program",
                            PublicationDate = DateTime.UtcNow,
                            PublicationTitle = "Title",
                            PublicationUrl = "url",
                            RegulationDate = DateTime.UtcNow,
                            RegulationId = 22,
                            RegulationTitle = "title",
                            Remark = new Remark
                            {
                                RemarkId = 1,
                                Value = "testRemark"
                            }
                        },
                        Remark = new Remark
                        {
                            RemarkId = 1,
                            Value = "testRemark"
                        }
                    }
                },
            OriginalEntityId = 3124,
            Regulation = new Regulation
            {
                Programme = "program",
                PublicationDate = DateTime.UtcNow,
                PublicationTitle = "Title",
                PublicationUrl = "url",
                RegulationDate = DateTime.UtcNow,
                RegulationId = 22,
                RegulationTitle = "title",
                Remark = new Remark
                {
                    RemarkId = 1,
                    Value = "testRemark"
                }
            },
            Remark = new Remark
            {
                RemarkId = 1,
                Value = "testRemark"
            },
            Status = new Status
            {
                StatusId = 2,
                Name = "name"
            },
            SubjectType = new SubjectType
            {
                SubjectTypeId = 2,
                Name = "name"
            },
        };

        private static Regulation _regulation = new Regulation
        {
            Programme = "program",
            PublicationDate = DateTime.UtcNow,
            PublicationTitle = "Title",
            PublicationUrl = "url",
            RegulationDate = DateTime.UtcNow,
            RegulationId = 22,
            RegulationTitle = "title",
            Remark = new Remark
            {
                RemarkId = 1,
                Value = "testRemark"
            },
            ListType = new ListType
            {
                Description = "Simple ListType",
                ListTypeId = 47101,
                Name = "Abnormal"
            }
        };

        private bool _initialCompleted;

        #endregion

        [TestInitialize]
        public void Test_Initialize()
        {
            //_initialCompleted = Helpers.GenerateBillingDb();
            _sactionListBll = new Bndb.Kyc.SanctionLists.SanctionListsBll.SanctionListsBll(_connectionString);
            _initialCompleted = Helpers.InitializeDatabase();
        }

        /// <summary>
        /// Cleanup method for clean database.
        /// </summary>
        [TestCleanup]
        public void Test_Cleanup()
        {
            if (_initialCompleted)
            {
                //revert database
                Helpers.InitializeDatabase();
            }
        }

        [TestMethod]
        public void Test_ContactInfoAdd_Success()
        {
            ContactInfo contactInfo = new ContactInfo
            {
                ContactInfoId = 11451,
                ContactInfoType = new ContactInfoType { ContactInfoTypeId = 114, Name = "Serial" },
                EntityId = 4814,
                OriginalContactInfoId = 441,
                Regulation = new Regulation
                {
                    ListType = new ListType
                    {
                        Description = "Simple Test",
                        ListTypeId = 47101,
                        Name = "Abnormal"
                    },
                    Programme = "InPrison",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "InPrison",
                    PublicationUrl = "InPrison",
                    RegulationDate = DateTime.Now,
                    RegulationId = 4474,
                    RegulationTitle = "",
                    Remark = new Remark { RemarkId = 1, Value = "Regulation Remark " }
                },
                Remark = new Remark { RemarkId = 1, Value = "ContactInfo Remark" },
                Value = "Test"
            };

            _sactionListBll.ContactInfoAdd(contactInfo);
        }

        [TestMethod]
        public void Test_ContactInfoTypesGet_Success()
        {
            ContactInfoType[] contactInfoTypes = _sactionListBll.ContactInfoTypesGet();

            Assert.IsNotNull(contactInfoTypes);
            Assert.IsInstanceOfType(contactInfoTypes, typeof(ContactInfoType[]));
            Assert.AreEqual(6, contactInfoTypes.Length);
        }

        [TestMethod]
        public void Test_CountriesGet_Success()
        {
            Country[] countries = _sactionListBll.CountriesGet();

            Assert.IsNotNull(countries);
            Assert.IsInstanceOfType(countries, typeof(Country[]));
            Assert.AreEqual(239, countries.Length);
        }

        [TestMethod]
        public void Test_GenderGet_Success()
        {
            Gender[] genders = _sactionListBll.GendersGet();

            Assert.IsNotNull(genders);
            Assert.IsInstanceOfType(genders, typeof(Gender[]));
            Assert.AreEqual(4, genders.Length);
        }

        [TestMethod]
        public void Test_IdentificationTypeGet_Success()
        {
            IdentificationType[] identificationTypes = _sactionListBll.IdentificationTypesGet();

            Assert.IsNotNull(identificationTypes);
            Assert.IsInstanceOfType(identificationTypes, typeof(IdentificationType[]));
            Assert.AreEqual(7, identificationTypes.Length);
        }

        [TestMethod]
        public void Test_LanguagesGet_Success()
        {
            Language[] languages = _sactionListBll.LanguagesGet();

            Assert.IsNotNull(languages);
            Assert.IsInstanceOfType(languages, typeof(Language[]));
            Assert.AreEqual(184, languages.Length);
        }

        [TestMethod]
        public void Test_StatusesGet_Success()
        {
            Status[] statuses = _sactionListBll.StatusesGet();

            Assert.IsNotNull(statuses);
            Assert.IsInstanceOfType(statuses, typeof(Status[]));
            Assert.AreEqual(3, statuses.Length);

        }

        [TestMethod]
        public void Test_SubjectTypesGet_Success()
        {
            SubjectType[] subjectTypes = _sactionListBll.SubjectTypesGet();

            Assert.IsNotNull(subjectTypes);
            Assert.IsInstanceOfType(subjectTypes, typeof(SubjectType[]));
            Assert.AreEqual(2, subjectTypes.Length);
        }

        [TestMethod]
        public void Test_Regulations_Success()
        {

            Assert.IsInstanceOfType(_sactionListBll.RegulationsAdd(_regulation), typeof(Regulation));

            Regulation[] regulations = _sactionListBll.RegulationsGet(1);
            Assert.IsInstanceOfType(regulations, typeof(Regulation[]));

            Regulation regulation = _sactionListBll.RegulationsGetByPublicationTitle(_regulation.PublicationTitle, _regulation.ListType.ListTypeId);
            Assert.IsNotNull(regulation);
            Assert.IsInstanceOfType(regulation, typeof(Regulation));

        }

        [TestMethod]
        public void Test_UpdateGet_Success()
        {
            Assert.IsInstanceOfType(_sactionListBll.UpdatesGet(1), typeof(Update[]));
        }

        [TestMethod]
        public void Test_EntitiesAdd_Success()
        {
            Assert.IsInstanceOfType(_sactionListBll.EntitiesAdd(_entity), typeof(Entity));
        }

        [TestMethod]
        public void Test_NameAliasesAdd_Success()
        {
            _sactionListBll.NameAliasesAdd(_nameAlias);
        }

        [TestMethod]
        public void Test_BirthAdd_Success()
        {
            _sactionListBll.BirthsAdd(_entity.Births.FirstOrDefault());
        }
        [TestMethod]
        public void Test_EntityGet_Success()
        {
            Entities.SearchEntity searchEntity = new Entities.SearchEntity();
            searchEntity.LastName = "J";
            //searchEntity.FirstName = "Khalifa";
            //searchEntity.BirthDate = new DateTime(1964, 06, 29);
            ListType listtype = new ListType();
            listtype.ListTypeId = 1;
            listtype.Name = "";
            searchEntity.SanctionListType = listtype;
            Entity[] entities = _sactionListBll.EntitiesGet(searchEntity, 0, 10);
            int entities1 = entities.Count();
        }
        [TestMethod]
        public void Test_EntitiesCount_Success()
        {
            Entities.SearchEntity searchEntity = new Entities.SearchEntity();
            searchEntity.LastName = "Hussein Al-Tikriti";
            //searchEntity.FirstName = "Khalifa";
            //searchEntity.BirthDate = new DateTime(1964, 06, 29);
            ListType listtype = new ListType();
            listtype.ListTypeId = 1;
            listtype.Name = "";
            searchEntity.SanctionListType = listtype;
            int entities = _sactionListBll.EntitiesCount(searchEntity);

        }



        [TestMethod]
        public void Test_EntitiesGetCondition_Success()
        {
            ISanctionListsBll bll = new SanctionListsBll.SanctionListsBll(_connectionString);
            var results = bll.EntitiesGet(new SearchEntity()
            {
                FirstName = "c j",
                LastName = null,
                SanctionListType = new ListType() { Name = "", ListTypeId = 1, Description = "" },
            }).ToList();

            var a = "";
        }
    }
}
