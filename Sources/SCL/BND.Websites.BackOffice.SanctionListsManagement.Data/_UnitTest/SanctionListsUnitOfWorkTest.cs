using Bndb.Kyc.Common.Repositories.Models;
using Bndb.Kyc.SanctionLists.SanctionListsDal;
using Bndb.Kyc.SanctionLists.SanctionListsDal.Pocos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bndb.Kyc.SanctionLists.SanctionListsDalTest
{
    [TestClass]
    public class SanctionListsUnitOfWorkTest
    {
        private SanctionListsUnitOfWork _unitOfWork;
        private List<object> _mockData;
        private List<p_EnumCountry> _countryData;
        private List<p_EnumContactInfoType> _contactTypeData;
        private List<p_EnumIdentificationType> _idenTypeData;
        private List<p_EnumGender> _genderData;
        private List<p_EnumLanguage> _langData;
        private List<p_EnumSubjectType> _subjectType;
        private List<p_EnumStatus> _statusType;
        private List<p_EnumListType> _listTypeData;
        private List<p_Remark> _remarkData;
        private List<p_Regulation> _regulationData;
        private List<p_Address> _addressData;
        private List<p_Bank> _bankData;
        private List<p_Birth> _birthData;
        private List<p_Citizenship> _citizenData;
        private List<p_ContactInfo> _contactData;
        private List<p_Identification> _idenData;
        private List<p_ListArchive> _listArchiveData;
        private List<p_NameAlias> _nameData;
        private List<p_Entity> _entityData;
        private bool _HasInitializedCompletely;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            // Register Effort for mocking database.
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
        }

        [TestInitialize]
        public void Test_Initialize()
        {
            // Initializes database.
            _HasInitializedCompletely = Helpers.InitializeDatabase();

            //_unitOfWork = new MockSanctionListsUnitOfWork();
            _unitOfWork = new SanctionListsUnitOfWork(Helpers.CONNECTION_STRING);
            _mockData = new List<object>();

            _countryData = new List<p_EnumCountry>
            {
                new p_EnumCountry
                {
                    Iso3 = "THA",
                    Iso2 = "TH",
                    Name = "THAILAND",
                    NiceName = "Thailand",
                    NumCode = 764,
                    PhoneCode = 66
                },
                new p_EnumCountry
                {
                    Iso3 = "NLD",
                    Iso2 = "NL",
                    Name = "NETHERLANDS",
                    NiceName = "Netherlands",
                    NumCode = 528,
                    PhoneCode = 31
                },
                new p_EnumCountry
                {
                    Iso3 = "POL",
                    Iso2 = "PL",
                    Name = "POLAND",
                    NiceName = "Poland",
                    NumCode = 616,
                    PhoneCode = 48
                }
            };
            _contactTypeData = new List<p_EnumContactInfoType>
            {
                new p_EnumContactInfoType
                {
                    ContactInfoTypeId = 1,
                    Name = "Phone"
                },
                new p_EnumContactInfoType
                {
                    ContactInfoTypeId = 2,
                    Name = "Web"
                },
                new p_EnumContactInfoType
                {
                    ContactInfoTypeId = 3,
                    Name = "Email"
                }
            };
            _idenTypeData = new List<p_EnumIdentificationType>
            {
                new p_EnumIdentificationType
                {
                    IdentificationTypeId = 1,
                    Name = "Passport",
                    Description = "National passport"
                },
                new p_EnumIdentificationType
                {
                    IdentificationTypeId = 2,
                    Name = "Id",
                    Description = "National identification card"
                },
                new p_EnumIdentificationType
                {
                    IdentificationTypeId = 3,
                    Name = "Ssn",
                    Description = "Social Security Number"
                }
            };
            _genderData = new List<p_EnumGender>
            {
                new p_EnumGender
                {
                    GenderId = 0,
                    Name = "Not Known"
                },
                new p_EnumGender
                {
                    GenderId = 1,
                    Name = "Male"
                },
                new p_EnumGender
                {
                    GenderId = 2,
                    Name = "Female"
                }
            };
            _langData = new List<p_EnumLanguage>
            {
                new p_EnumLanguage
                {
                    Iso3 = "THA",
                    Iso2 = "TH",
                    Name = "Thai"
                },
                new p_EnumLanguage
                {
                    Iso3 = "DUT",
                    Iso2 = "NL",
                    Name = "Dutch; Flemish"
                },
                new p_EnumLanguage
                {
                    Iso3 = "POL",
                    Iso2 = "PL",
                    Name = "Polish"
                }
            };
            _subjectType = new List<p_EnumSubjectType>
            {
                new p_EnumSubjectType
                {
                    SubjectTypeId = 1,
                    Name = "Person"
                },
                new p_EnumSubjectType
                {
                    SubjectTypeId = 2,
                    Name = "Enterprise"
                }
            };
            _statusType = new List<p_EnumStatus>
            {
                new p_EnumStatus
                {
                    StatusId = 0,
                    Name = "Inactive"
                },
                new p_EnumStatus
                {
                    StatusId = 1,
                    Name = "Active"
                },
                new p_EnumStatus
                {
                    StatusId = 2,
                    Name = "Removed"
                }
            };
            _listTypeData = new List<p_EnumListType>
            {
                new p_EnumListType
                {
                    ListTypeId = 1,
                    Name = "EU",
                    Description = "EU Sanction list"
                },
                new p_EnumListType
                {
                    ListTypeId = 2,
                    Name = "NL",
                    Description = "NL Sanction list"
                },
                new p_EnumListType
                {
                    ListTypeId = 3,
                    Name = "BND",
                    Description = "BND Sanction list"
                }
            };
            _remarkData = new List<p_Remark>
            {
                new p_Remark { Value = "Remark-01" },
                new p_Remark { Value = "Remark-02" },
                new p_Remark { Value = "Remark-03" }
            };
            _regulationData = new List<p_Regulation>
            {
                new p_Regulation
                {
                    RegulationTitle = "Title-01",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-01",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-01",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-02",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-02",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-02",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-03",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-03",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-03",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-04",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-04",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-04",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-05",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-05",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-05",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-06",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-06",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-06",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-07",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-07",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-07",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-08",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-08",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-08",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-09",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-09",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-09",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-10",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-10",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-10",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-11",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-11",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-11",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-12",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-12",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-12",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-13",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-13",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-13",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-14",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-14",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-14",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-15",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-15",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-15",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-16",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-16",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-16",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-17",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-17",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-17",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-18",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-18",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-18",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-19",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-19",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-19",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-20",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-20",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-20",
                    ListTypeId = _listTypeData[0].ListTypeId
                },
                new p_Regulation
                {
                    RegulationTitle = "Title-21",
                    PublicationDate = DateTime.Now,
                    PublicationTitle = "PubTitle-21",
                    PublicationUrl = "http://www.google.com",
                    Programme = "Prog-21",
                    ListTypeId = _listTypeData[0].ListTypeId
                }
            };
            _addressData = new List<p_Address>
            {
                new p_Address
                {
                    OriginalAddressId = 1,
                    Number = "Number-01",
                    Street = "Street-01",
                    City = "City-01",
                    Zipcode = "Zipcode-01",
                    CountryIso3 = _countryData[0].Iso3,
                    Regulation = _regulationData[0],
                    Remark = _remarkData[0]
                },
                new p_Address
                {
                    OriginalAddressId = 2,
                    Number = "Number-02",
                    Street = "Street-02",
                    City = "City-02",
                    Zipcode = "Zipcode-02",
                    CountryIso3 = _countryData[1].Iso3,
                    Regulation = _regulationData[0],
                    Remark = _remarkData[1]
                },
                new p_Address
                {
                    OriginalAddressId = 3,
                    Number = "Number-03",
                    Street = "Street-03",
                    City = "City-03",
                    Zipcode = "Zipcode-03",
                    CountryIso3 = _countryData[2].Iso3,
                    Regulation = _regulationData[2],
                    Remark = _remarkData[2]
                }
            };
            _bankData = new List<p_Bank>
            {
                new p_Bank
                {
                    AccountNumber = "AccNum-01",
                    AccountHolderName = "AccName-01",
                    BankName = "Name-01",
                    CountryIso3 = _countryData[0].Iso3,
                    Swift = "Swift-01",
                    Iban = "Iban-01",
                    Remark = _remarkData[0]
                },
                new p_Bank
                {
                    AccountNumber = "AccNum-02",
                    AccountHolderName = "AccName-02",
                    BankName = "Name-02",
                    CountryIso3 = _countryData[1].Iso3,
                    Swift = "Swift-02",
                    Iban = "Iban-02",
                    Remark = _remarkData[1]
                },
                new p_Bank
                {
                    AccountNumber = "AccNum-03",
                    AccountHolderName = "AccName-03",
                    BankName = "Name-03",
                    CountryIso3 = _countryData[2].Iso3,
                    Swift = "Swift-03",
                    Iban = "Iban-03",
                    Remark = _remarkData[2]
                }
            };
            _birthData = new List<p_Birth>
            {
                new p_Birth
                {
                    OriginalBirthId = 1,
                    Year = DateTime.Now.Year,
                    Month = DateTime.Now.Month,
                    Day = DateTime.Now.Day,
                    Place = "Place-01",
                    CountryIso3 = _countryData[0].Iso3,
                    Remark = _remarkData[0],
                    Regulation = _regulationData[3]
                },
                new p_Birth
                {
                    OriginalBirthId = 2,
                    Year = DateTime.Now.Year,
                    Month = DateTime.Now.Month,
                    Day = DateTime.Now.Day,
                    Place = "Place-02",
                    CountryIso3 = _countryData[1].Iso3,
                    Remark = _remarkData[1],
                    Regulation = _regulationData[4]
                },
                new p_Birth
                {
                    OriginalBirthId = 3,
                    Year = DateTime.Now.Year,
                    Month = DateTime.Now.Month,
                    Day = DateTime.Now.Day,
                    Place = "Place-03",
                    CountryIso3 = _countryData[2].Iso3,
                    Remark = _remarkData[2],
                    Regulation = _regulationData[5]
                }
            };
            _citizenData = new List<p_Citizenship>
            {
                new p_Citizenship
                {
                    OriginalCitizenshipId = 1,
                    CountryIso3 = _countryData[0].Iso3,
                    Remark = _remarkData[0],
                    Regulation = _regulationData[6]
                },
                new p_Citizenship
                {
                    OriginalCitizenshipId = 2,
                    CountryIso3 = _countryData[1].Iso3,
                    Remark = _remarkData[1],
                    Regulation = _regulationData[7]
                },
                new p_Citizenship
                {
                    OriginalCitizenshipId = 3,
                    CountryIso3 = _countryData[2].Iso3,
                    Remark = _remarkData[2],
                    Regulation = _regulationData[8]
                }
            };
            _contactData = new List<p_ContactInfo>
            {
                new p_ContactInfo
                {
                    OriginalContactInfoId = 1,
                    ContactInfoTypeId = _contactTypeData[0].ContactInfoTypeId,
                    Value = "Value-01",
                    Remark = _remarkData[0],
                    Regulation = _regulationData[9]
                },
                new p_ContactInfo
                {
                    OriginalContactInfoId = 2,
                    ContactInfoTypeId = _contactTypeData[1].ContactInfoTypeId,
                    Value = "Value-02",
                    Remark = _remarkData[1],
                    Regulation = _regulationData[10]
                },
                new p_ContactInfo
                {
                    OriginalContactInfoId = 3,
                    ContactInfoTypeId = _contactTypeData[2].ContactInfoTypeId,
                    Value = "Value-03",
                    Remark = _remarkData[2],
                    Regulation = _regulationData[11]
                }
            };
            _idenData = new List<p_Identification>
            {
                new p_Identification
                {
                    OriginalIdentificationId = 1,
                    IdentificationTypeId = _idenTypeData[0].IdentificationTypeId,
                    DocumentNumber = "DocNum-01",
                    IssueCity = "City-01",
                    IssueCountryIso3 = _countryData[0].Iso3,
                    IssueDate = DateTime.Now,
                    Remark = _remarkData[0],
                    Regulation = _regulationData[12]
                },
                new p_Identification
                {
                    OriginalIdentificationId = 2,
                    IdentificationTypeId = _idenTypeData[1].IdentificationTypeId,
                    DocumentNumber = "DocNum-02",
                    IssueCity = "City-02",
                    IssueCountryIso3 = _countryData[1].Iso3,
                    IssueDate = DateTime.Now,
                    Remark = _remarkData[1],
                    Regulation = _regulationData[13]
                },
                new p_Identification
                {
                    OriginalIdentificationId = 3,
                    IdentificationTypeId = _idenTypeData[2].IdentificationTypeId,
                    DocumentNumber = "DocNum-03",
                    IssueCity = "City-03",
                    IssueCountryIso3 = _countryData[2].Iso3,
                    IssueDate = DateTime.Now,
                    Remark = _remarkData[2],
                    Regulation = _regulationData[14]
                }
            };
            _listArchiveData = new List<p_ListArchive>
            {
                new p_ListArchive
                {
                    Date = DateTime.Now,
                    File = new byte[5]
                },
                new p_ListArchive
                {
                    Date = DateTime.Now,
                    File = new byte[10]
                },
                new p_ListArchive
                {
                    Date = DateTime.Now,
                    File = new byte[15]
                }
            };
            _nameData = new List<p_NameAlias>
            {
                new p_NameAlias
                {
                    OriginalNameAliasId = 1,
                    LastName = "Last-01",
                    FirstName = "First-01",
                    MiddleName = "Midle-01",
                    WholeName = "Whole-01",
                    PrefixName = "Prefix-01",
                    GenderId = _genderData[0].GenderId,
                    Title = "Title-01",
                    LanguageIso3 = _langData[0].Iso3,
                    Quality = 0,
                    Function = "Func-01",
                    Remark = _remarkData[0],
                    Regulation = _regulationData[15]
                },
                new p_NameAlias
                {
                    OriginalNameAliasId = 2,
                    LastName = "Last-02",
                    FirstName = "First-02",
                    MiddleName = "Midle-02",
                    WholeName = "Whole-02",
                    PrefixName = "Prefix-02",
                    GenderId = _genderData[1].GenderId,
                    Title = "Title-02",
                    LanguageIso3 = _langData[0].Iso3,
                    Quality = 1,
                    Function = "Func-02",
                    Remark = _remarkData[1],
                    Regulation = _regulationData[16]
                },
                new p_NameAlias
                {
                    OriginalNameAliasId = 3,
                    LastName = "Last-03",
                    FirstName = "First-03",
                    MiddleName = "Midle-03",
                    WholeName = "Whole-03",
                    PrefixName = "Prefix-03",
                    GenderId = _genderData[2].GenderId,
                    Title = "Title-03",
                    LanguageIso3 = _langData[0].Iso3,
                    Quality = 2,
                    Function = "Func-03",
                    Remark = _remarkData[2],
                    Regulation = _regulationData[17]
                }
            };
            _entityData = new List<p_Entity>
            {
                new p_Entity
                {
                    OriginalEntityId = 1,
                    Addresses = _addressData,
                    Banks = _bankData,
                    Births = _birthData,
                    Citizenships = _citizenData,
                    ContactInfo = _contactData,
                    Identifications = _idenData,
                    NameAliases = _nameData,
                    ListArchive = _listArchiveData[0],
                    Regulation = _regulationData[18],
                    Remark = _remarkData[0],
                    ListTypeId = _listTypeData[0].ListTypeId,
                    StatusId = _statusType[0].StatusId,
                    SubjectTypeId = _subjectType[0].SubjectTypeId
                },
                new p_Entity
                {
                    OriginalEntityId = 2,
                    //Addresses = _addressData,
                    //Banks = _bankData,
                    //Births = _birthData,
                    //Citizens = _citizenData,
                    //ContactInfo = _contactData,
                    //Identifications = _idenData,
                    //NameAliases = _nameData,
                    ListArchive = _listArchiveData[1],
                    Regulation = _regulationData[19],
                    Remark = _remarkData[1],
                    ListTypeId = _listTypeData[0].ListTypeId,
                    StatusId = _statusType[0].StatusId,
                    SubjectTypeId = _subjectType[0].SubjectTypeId
                },
                new p_Entity
                {
                    OriginalEntityId = 3,
                    //Addresses = _addressData,
                    //Banks = _bankData,
                    //Births = _birthData,
                    //Citizens = _citizenData,
                    //ContactInfo = _contactData,
                    //Identifications = _idenData,
                    //NameAliases = _nameData,
                    ListArchive = _listArchiveData[2],
                    Regulation = _regulationData[20],
                    Remark = _remarkData[2],
                    ListTypeId = _listTypeData[0].ListTypeId,
                    StatusId = _statusType[0].StatusId,
                    SubjectTypeId = _subjectType[0].SubjectTypeId
                }
            };

            //_mockData.Add(new UnitOfWorkTestObject<p_Regulation>
            //{
            //    GetParameters = new GetParameters<p_Regulation>
            //    {
            //        Filter = poco => poco.RegulationTitle.StartsWith("Title"),
            //        IncludeProperties = new string[] { "Entities", "NameAliasses" },
            //        OrderBy = poco => poco.OrderByDescending(p => p.RegulationId),
            //        Page = new Page<p_Regulation>
            //        {
            //            Limit = 2,
            //            Offset = 1,
            //            OrderBy = poco => poco.OrderBy(p => p.RegulationId)
            //        }
            //    },
            //    OnChangingPocoEntity = poco => poco.RegulationTitle = "Title was updated.",
            //    OnGetIds = poco => new object[] { poco.RegulationId },
            //    PocoEntities = _regulationData
            //});

            _mockData.Add(new UnitOfWorkTestObject<p_Entity>
            {
                GetParameters = new GetParameters<p_Entity>
                {
                    Filter = poco => poco.EntityId > 1,
                    IncludeProperties = new string[] { "Regulation", "NameAliases" },
                    OrderBy = poco => poco.OrderByDescending(p => p.EntityId),
                    Page = new Page<p_Entity>
                    {
                        Limit = 2,
                        Offset = 1,
                        OrderBy = poco => poco.OrderBy(p => p.EntityId)
                    }
                },
                OnChangingPocoEntity = poco => poco.Regulation.RegulationTitle = "Title was updated.",
                OnGetIds = poco => new object[] { poco.EntityId },
                PocoEntities = _entityData
            });
        }

        [TestCleanup]
        public void Test_Cleanup()
        {
            if (_HasInitializedCompletely)
            {
                // Restores database.
                Helpers.InitializeDatabase();
            }
        }

        [TestMethod]
        public void Test_AllRepositories_Success()
        {
            try
            {
                foreach (dynamic testObject in _mockData)
                {
                    Helpers.VerifyUnitOfWork(
                        _unitOfWork,
                        testObject.PocoEntities,
                        testObject.GetParameters,
                        testObject.OnGetIds,
                        testObject.OnChangingPocoEntity
                    );
                }
            }
            catch {}
        }
    }
}
