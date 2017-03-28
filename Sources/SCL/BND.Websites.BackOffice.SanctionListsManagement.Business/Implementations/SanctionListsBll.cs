using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BND.Websites.BackOffice.SanctionListsManagement.Business.Interfaces;
using BND.Websites.BackOffice.SanctionListsManagement.Domain;
using BND.Websites.BackOffice.SanctionListsManagement.Domain.Interfaces;
using BND.Websites.BackOffice.SanctionListsManagement.Entities;
using BND.Websites.BackOffice.SanctionListsManagement.Models;

namespace BND.Websites.BackOffice.SanctionListsManagement.Business.Implementations
{
    /// <summary>
    /// Business logic of SanctionLists.
    /// </summary>
    public class SanctionListsBll : ISanctionListsBll
    {
        #region [FIELDS]

        /// <summary>
        /// A unit of work of SanctionLists for manipulating all data of sanction lists.
        /// </summary>
        private SanctionListsUnitOfWork _sanctionListUnitOfWork;

        /// <summary>
        /// Repository for manipulating data of EnumContactInfoTypes table.
        /// </summary>
        private IRepository<p_EnumContactInfoType> _contactInfoTypeRepo;

        /// <summary>
        /// Repository for manipulating data of EnumCountries table.
        /// </summary>
        private IRepository<p_EnumCountry> _countryRepo;

        /// <summary>
        /// Repository for manipulating data of EnumGenders table.
        /// </summary>
        private IRepository<p_EnumGender> _genderRepo;

        /// <summary>
        /// Repository for manipulating data of EnumIdentificationTypes table.
        /// </summary>
        private IRepository<p_EnumIdentificationType> _identificationTypeRepo;

        /// <summary>
        /// Repository for manipulating data of EnumLanguages table.
        /// </summary>
        private IRepository<p_EnumLanguage> _langaugeRepo;

        /// <summary>
        /// Repository for manipulating data of EnumListTypes table.
        /// </summary>
        private IRepository<p_EnumListType> _listTypeRepo;

        /// <summary>
        /// Repository for manipulating data of EnumStatuses table.
        /// </summary>
        private IRepository<p_EnumStatus> _statusRepo;

        /// <summary>
        /// Repository for manipulating data of EnumSubjectTypes table.
        /// </summary>
        private IRepository<p_EnumSubjectType> _subjectTypeRepo;

        /// <summary>
        /// Repository for manipulating data of Updates table.
        /// </summary>
        private IRepository<p_Update> _updateRepo;

        /// <summary>
        /// Repository for manipulating data of Logs table.
        /// </summary>
        private IRepository<p_Log> _logRepo;

        /// <summary>
        /// Repository for manipulating data of Settings table.
        /// </summary>
        private IRepository<p_Setting> _settingRepo;

        /// <summary>
        /// Repository for manipulating data of Remarks table.
        /// </summary>
        private IRepository<p_Remark> _remarkRepo;

        /// <summary>
        /// Repository for manipulating data of Regulations table.
        /// </summary>
        private IRepository<p_Regulation> _regulationRepo;

        /// <summary>
        /// Repository for manipulating data of NameAliases table.
        /// </summary>
        private IRepository<p_NameAlias> _nameAliasRepo;

        /// <summary>
        /// Repository for manipulating data of Addresses table.
        /// </summary>
        private IRepository<p_Address> _addressRepo;

        /// <summary>
        /// Repository for manipulating data of Births table.
        /// </summary>
        private IRepository<p_Birth> _birthRepo;

        /// <summary>
        /// Repository for manipulating data of Banks table.
        /// </summary>
        private IRepository<p_Bank> _bankRepo;

        /// <summary>
        /// Repository for manipulating data of Citizenships table.
        /// </summary>
        private IRepository<p_Citizenship> _citizenshipRepo;

        /// <summary>
        /// Repository for manipulating data of ContactInfo table.
        /// </summary>
        private IRepository<p_ContactInfo> _contactInfoRepo;

        /// <summary>
        /// Repository for manipulating data of Identifications table.
        /// </summary>
        private IRepository<p_Identification> _identificationRepo;

        /// <summary>
        /// Repository for manipulating data of Entities table.
        /// </summary>
        private IRepository<p_Entity> _entityRepo;

        /// <summary>
        /// Repository for manipulating data of ListArchive table.
        /// </summary>
        private IRepository<p_ListArchive> _listArchiveRepo;

        /// <summary>
        /// Collection of p_Regulation object for reuse the Regulation object.
        /// </summary>
        private List<p_Regulation> _relationRegulation;

        #endregion


        #region [CONSTRUCTOR]

        /// <summary>
        /// Class constructor. Initializes private fields.
        /// </summary>
        /// <param name="connectionString">Database connection string.</param>
        public SanctionListsBll(string connectionString)
        {
            _sanctionListUnitOfWork = new SanctionListsUnitOfWork(connectionString);
            _contactInfoTypeRepo = _sanctionListUnitOfWork.GetRepository<p_EnumContactInfoType>();
            _countryRepo = _sanctionListUnitOfWork.GetRepository<p_EnumCountry>();
            _genderRepo = _sanctionListUnitOfWork.GetRepository<p_EnumGender>();
            _identificationTypeRepo = _sanctionListUnitOfWork.GetRepository<p_EnumIdentificationType>();
            _langaugeRepo = _sanctionListUnitOfWork.GetRepository<p_EnumLanguage>();
            _listTypeRepo = _sanctionListUnitOfWork.GetRepository<p_EnumListType>();
            _statusRepo = _sanctionListUnitOfWork.GetRepository<p_EnumStatus>();
            _subjectTypeRepo = _sanctionListUnitOfWork.GetRepository<p_EnumSubjectType>();
            _updateRepo = _sanctionListUnitOfWork.GetRepository<p_Update>();
            _logRepo = _sanctionListUnitOfWork.GetRepository<p_Log>();
            _settingRepo = _sanctionListUnitOfWork.GetRepository<p_Setting>();
            _remarkRepo = _sanctionListUnitOfWork.GetRepository<p_Remark>();
            _regulationRepo = _sanctionListUnitOfWork.GetRepository<p_Regulation>();
            _nameAliasRepo = _sanctionListUnitOfWork.GetRepository<p_NameAlias>();
            _addressRepo = _sanctionListUnitOfWork.GetRepository<p_Address>();
            _birthRepo = _sanctionListUnitOfWork.GetRepository<p_Birth>();
            _bankRepo = _sanctionListUnitOfWork.GetRepository<p_Bank>();
            _citizenshipRepo = _sanctionListUnitOfWork.GetRepository<p_Citizenship>();
            _contactInfoRepo = _sanctionListUnitOfWork.GetRepository<p_ContactInfo>();
            _identificationRepo = _sanctionListUnitOfWork.GetRepository<p_Identification>();
            _entityRepo = _sanctionListUnitOfWork.GetRepository<p_Entity>();
            _listArchiveRepo = _sanctionListUnitOfWork.GetRepository<p_ListArchive>();
        }

        #endregion


        #region [Enums]

        /// <summary>
        /// Gets list of ContractInfoType object.
        /// </summary>
        /// <returns>ContactInfoType[].</returns>
        public ContactInfoType[] ContactInfoTypesGet()
        {
            IEnumerable<p_EnumContactInfoType> contactInfoTypes = _contactInfoTypeRepo.Get();

            return contactInfoTypes.Select(c => MapContactInfoType(c)).ToArray();
        }

        /// <summary>
        /// Gets list of Country object.
        /// </summary>
        /// <returns>Country[].</returns>
        public Country[] CountriesGet()
        {
            IEnumerable<p_EnumCountry> countries = _countryRepo.Get();

            return countries.Select(c => MapCountry(c)).ToArray();
        }

        /// <summary>
        /// Gets list of Gender object.
        /// </summary>
        /// <returns>Gender[].</returns>
        public Gender[] GendersGet()
        {
            IEnumerable<p_EnumGender> genders = _genderRepo.Get();

            return genders.Select(g => MapGender(g)).ToArray();
        }

        /// <summary>
        /// Gets list of IdentificationType object.
        /// </summary>
        /// <returns>IdentificationType[].</returns>
        public IdentificationType[] IdentificationTypesGet()
        {
            IEnumerable<p_EnumIdentificationType> identificationTypes = _identificationTypeRepo.Get();

            return identificationTypes.Select(i => MapIdentificationType(i)).ToArray();
        }

        /// <summary>
        /// Gets list of Language object.
        /// </summary>
        /// <returns>Language[].</returns>
        public Language[] LanguagesGet()
        {
            IEnumerable<p_EnumLanguage> languages = _langaugeRepo.Get();

            return languages.Select(l => MapLanguage(l)).ToArray();
        }

        /// <summary>
        /// Gets list of ListType object.
        /// </summary>
        /// <returns>ListType[].</returns>
        public ListType[] ListTypesGet()
        {
            IEnumerable<p_EnumListType> listTypes = _listTypeRepo.Get();

            return listTypes.Select(l => MapListType(l)).ToArray();
        }

        /// <summary>
        /// Returns list type object by id.
        /// </summary>
        /// <param name="listTypeId">Id of the ListType</param>
        /// <returns>ListTypes</returns>
        public ListType ListTypesGetById(int listTypeId)
        {
            return MapListType(_listTypeRepo.GetById(listTypeId));
        }

        /// <summary>
        /// Gets list of Status object.
        /// </summary>
        /// <returns>Status[].</returns>
        public Status[] StatusesGet()
        {
            IEnumerable<p_EnumStatus> statuses = _statusRepo.Get();

            return statuses.Select(s => MapStatus(s)).ToArray();
        }

        /// <summary>
        /// Gets list of SubjectType object.
        /// </summary>
        /// <returns>SubjectType[].</returns>
        public SubjectType[] SubjectTypesGet()
        {
            IEnumerable<p_EnumSubjectType> subjectTypes = _subjectTypeRepo.Get();

            return subjectTypes.Select(s => MapSubjectType(s)).ToArray();
        }

        #endregion


        #region [Update]

        /// <summary>
        /// Gets information about Update object by passing the ListTypeId.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <returns>Gets <see cref="Update" /> entities.</returns>
        public Update UpdatesGet(int listTypeId)
        {
            p_Update update = _updateRepo.Get(u => u.ListTypeId == listTypeId).SingleOrDefault();

            return (update == null)
                   ? null
                   : new Update
                     {
                         ListArchiveId = update.ListArchiveId,
                         ListType = MapListType(update.EnumListType),
                         PublicDate = update.PublicDate,
                         UpdatedDate = update.UpdatedDate,
                         UpdateId = update.UpdateId,
                         Username = update.Username
                     };
        }

        /// <summary>
        /// Updates information about Update object, if object doesn't exist create a new one.
        /// </summary>
        /// <param name="update">The update object.</param>
        /// <returns>Sets <see cref="Update" /> entities.</returns>
        public Update UpdatesSet(Update update)
        {
            p_Update pUpdate = _updateRepo.Get(u => u.ListTypeId == update.ListType.ListTypeId).SingleOrDefault();
            bool exists = pUpdate != null;

            pUpdate = pUpdate ?? new p_Update();
            pUpdate.ListArchiveId = update.ListArchiveId;
            pUpdate.ListTypeId = update.ListType.ListTypeId;
            pUpdate.PublicDate = update.PublicDate;
            pUpdate.UpdatedDate = DateTime.UtcNow;
            pUpdate.Username = update.Username;

            if (exists)
            {
                _sanctionListUnitOfWork.GetRepository<p_Update>().Update(pUpdate);
            }
            else
            {
                _sanctionListUnitOfWork.GetRepository<p_Update>().Insert(pUpdate);
            }

            _sanctionListUnitOfWork.Execute();

            update.UpdateId = pUpdate.UpdateId;
            update.ListArchiveId = pUpdate.ListArchiveId;

            return update;
        }

        #endregion


        #region [Log]

        /// <summary>
        /// Gets list of Log object by passing listTypeId if null will be return all of Log, and can get data by
        /// specific the offset of page and limitation to show in the page
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <param name="offset">The offset or page number.</param>
        /// <param name="limit">The limitation of number of record per page.</param>
        /// <returns>Log[].</returns>
        public Log[] LogsGet(int? listTypeId = null, int? offset = null, int? limit = null)
        {
            Page<p_Log> page = null;
            if (offset.HasValue || limit.HasValue)
            {
                page = new Page<p_Log>
                {
                    Limit = limit,
                    Offset = offset,
                    OrderBy = l => l.OrderBy(o => o.LogId)
                };
            }

            IEnumerable<p_Log> logs = (listTypeId.HasValue)
                                      ? _logRepo.Get(l => l.ListTypeId == listTypeId.Value, page)
                                      : _logRepo.Get(page: page);

            return logs.Select(l => new Log
            {
                ActionType = new ActionType
                {
                    ActionTypeId = l.EnumActionType.ActionTypeId,
                    Description = l.EnumActionType.Description,
                    Name = l.EnumActionType.Name
                },
                Description = l.Description,
                ListType = MapListType(l.EnumListType),
                LogDate = l.LogDate,
                LogId = l.LogId,
                Username = l.Username
            }).ToArray();
        }

        /// <summary>
        /// Adds Log object.
        /// </summary>
        /// <param name="log">The log object.</param>
        /// <returns>Log.</returns>
        public Log LogsAdd(Log log)
        {
            p_Log pLog = new p_Log
            {
                ActionTypeId = log.ActionType.ActionTypeId,
                Description = log.Description,
                ListTypeId = (log.ListType == null) ? (int?)null : log.ListType.ListTypeId,
                LogDate = DateTime.UtcNow,
                Username = log.Username
            };

            _logRepo.Insert(pLog);
            _sanctionListUnitOfWork.Execute();

            log.LogId = pLog.LogId;

            return log;
        }

        /// <summary>
        /// Gets record count of Los object.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <returns>System.Int32.</returns>
        public int LogsCount(int? listTypeId)
        {
            if (listTypeId != null)
            {
                return _logRepo.GetCount(e => e.ListTypeId == listTypeId);
            }
            return _logRepo.GetCount();
        }

        #endregion


        #region [Setting]

        /// <summary>
        /// Get information about Setting object by passing listTypeId and key name of Setting.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <param name="key">The key name of setting.</param>
        /// <returns>Setting.</returns>
        public Setting SettingsGet(int listTypeId, string key)
        {
            p_Setting setting = _settingRepo.Get(s => s.ListTypeId == listTypeId && s.Key == key).SingleOrDefault();

            return (setting == null)
                   ? null
                   : new Setting
                   {
                       Key = setting.Key,
                       ListType = MapListType(setting.EnumListType),
                       SettingId = setting.SettingId,
                       Value = setting.Value
                   };
        }

        #endregion


        #region [Regulation]

        /// <summary>
        /// Gets list of Regulation object by passing listTypeId , and get data by specific the offset of page 
        /// and limitation to show in the page
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <param name="offset">The offset or page number.</param>
        /// <param name="limit">The limitation of number of record per page.</param>
        /// <returns>Regulation[].</returns>
        public Regulation[] RegulationsGet(int listTypeId, int? offset = null, int? limit = null)
        {
            Page<p_Regulation> page = null;
            if (offset.HasValue || limit.HasValue)
            {
                page = new Page<p_Regulation>
                {
                    Limit = limit,
                    Offset = offset,
                    OrderBy = l => l.OrderBy(o => o.RegulationId)
                };
            }

            IEnumerable<p_Regulation> regulations = _regulationRepo.Get(l => l.ListTypeId == listTypeId, page);

            return regulations.Select(r => MapRegulation(r)).ToArray();

        }

        /// <summary>
        /// Gets record count of regulations object.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <returns>System.Int32.</returns>
        public int RegulationsCount(int? listTypeId)
        {
            if (listTypeId != null)
            {
                return _regulationRepo.GetCount(e => e.ListTypeId == listTypeId);
            }
            return _regulationRepo.GetCount();
        }

        /// <summary>
        /// Gets information about Regulation by PublicationTitle.
        /// </summary>
        /// <param name="publicationTitle">The publication title.</param>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <returns>Regulation.</returns>
        public Regulation RegulationsGetByPublicationTitle(string publicationTitle, int listTypeId)
        {
            p_Regulation pRegulation = _regulationRepo.Get(r => r.PublicationTitle == publicationTitle &&
                                                                r.ListTypeId == listTypeId)
                                                      .SingleOrDefault();

            return (pRegulation == null) ? null : MapRegulation(pRegulation);
        }

        /// <summary>
        /// Adds Regulation object.
        /// </summary>
        /// <param name="regulation">The regulation object.</param>
        /// <returns>Regulation.</returns>
        public Regulation RegulationsAdd(Regulation regulation)
        {
            p_Remark pRemark = null;
            if (regulation.Remark != null)
            {
                pRemark = new p_Remark();
                pRemark.Value = regulation.Remark.Value;
            }

            p_Regulation pRegulation = new p_Regulation();
            pRegulation.Programme = regulation.Programme;
            pRegulation.RegulationTitle = regulation.RegulationTitle;
            pRegulation.RegulationDate = regulation.RegulationDate;
            pRegulation.PublicationTitle = regulation.PublicationTitle;
            pRegulation.PublicationDate = regulation.PublicationDate;
            pRegulation.PublicationUrl = regulation.PublicationUrl;
            pRegulation.ListTypeId = regulation.ListType.ListTypeId;
            pRegulation.Remark = pRemark;

            _regulationRepo.Insert(pRegulation);
            _sanctionListUnitOfWork.Execute();

            regulation.RegulationId = pRegulation.RegulationId;

            return regulation;
        }

        #endregion


        #region [NameAlias]

        /// <summary>
        /// Returns <see cref="NameAlias" /> objects by entity id.
        /// </summary>
        /// <param name="entityId">Id if the <see cref="Entity" /> that is related to nameAliases.</param>
        /// <returns>Array of<see cref="NameAlias" />.</returns>
        public NameAlias[] NameAliasesGet(int entityId)
        {
            IEnumerable<p_NameAlias> nameAliases = _nameAliasRepo.Get(n => n.EntityId == entityId);

            return nameAliases.Select(n => MapNameAlias(n)).ToArray();
        }

        /// <summary>
        /// Returns <see cref="NameAlias" /> object by id.
        /// </summary>
        /// <param name="nameAliasId">The name alias identifier.</param>
        /// <returns><see cref="NameAlias" />.</returns>
        public NameAlias NameAliasesGetById(int nameAliasId)
        {
            p_NameAlias nameAlias = _nameAliasRepo.Get(n => n.NameAliasId == nameAliasId).SingleOrDefault();

            return (nameAlias == null) ? null : MapNameAlias(nameAlias);
        }

        /// <summary>
        /// Adds new NameAlias.
        /// </summary>
        /// <param name="nameAlias"><see cref="NameAlias" /> to add.</param>
        /// <returns>Added <see cref="NameAlias" />.</returns>
        public NameAlias NameAliasesAdd(NameAlias nameAlias)
        {
            p_NameAlias pNameAlias = new p_NameAlias();
            MigrateNameAlias(nameAlias, pNameAlias);

            _nameAliasRepo.Insert(pNameAlias);
            _sanctionListUnitOfWork.Execute();

            nameAlias.NameAliasId = pNameAlias.NameAliasId;

            // Set OriginalId to system id, if it is null
            if (nameAlias.OriginalNameAliasId == null)
            {
                pNameAlias.OriginalNameAliasId = nameAlias.OriginalNameAliasId = pNameAlias.NameAliasId;
                _sanctionListUnitOfWork.GetRepository<p_NameAlias>().Update(pNameAlias);
                _sanctionListUnitOfWork.Execute();
            }

            return nameAlias;
        }

        /// <summary>
        /// "Updates NameAlias entity.
        /// </summary>
        /// <param name="nameAlias"><see cref="NameAlias" /> to update.</param>
        /// <returns>Updated <see cref="NameAlias" />.</returns>
        /// <exception cref="System.ArgumentException">The name alias could not be found.;nameAlias</exception>
        public NameAlias NameAliasesUpdate(NameAlias nameAlias)
        {
            p_NameAlias pNameAlias = _nameAliasRepo.GetById(nameAlias.NameAliasId);
            if (pNameAlias == null)
            {
                throw new ArgumentException("The name alias could not be found.", "nameAlias");
            }

            // If remark is not empty AND Value is empty nor null
            if (nameAlias.Remark != null && String.IsNullOrEmpty(nameAlias.Remark.Value))
            {
                // If remark is not null, delete it by its id
                if (pNameAlias.Remark != null)
                {
                    _remarkRepo.Delete(pNameAlias.Remark.RemarkId);
                }

                // set Remark to null, which means the reference will be removed
                nameAlias.Remark = null;
            }

            MigrateNameAlias(nameAlias, pNameAlias);

            _nameAliasRepo.Update(pNameAlias);
            _sanctionListUnitOfWork.Execute();

            return nameAlias;
        }

        /// <summary>
        /// Removes NameAlias.
        /// </summary>
        /// <param name="nameAliasId">Id of <see cref="BND.Websites.BackOffice.SanctionListsManagement.Entities.NameAlias" /> to be removed.</param>
        public void NameAliasesRemove(int nameAliasId)
        {
            _nameAliasRepo.Delete(nameAliasId);
            _sanctionListUnitOfWork.Execute();
        }

        #endregion


        #region [Address]

        /// <summary>
        /// Returns list of <see cref="Address" />es by <see cref="Entity" /> id.
        /// </summary>
        /// <param name="entityId">Id of the <see cref="Entity" /> that is related to addresses</param>
        /// <returns>Array of <see cref="Address" /> entities.</returns>
        public Address[] AddressesGet(int entityId)
        {
            IEnumerable<p_Address> addresses = _addressRepo.Get(a => a.EntityId == entityId);

            return addresses.Select(a => MapAddress(a)).ToArray();
        }

        /// <summary>
        /// Adds new <see cref="Address" />.
        /// </summary>
        /// <param name="address"><see cref="Address" /> to be added.</param>
        /// <returns>Added <see cref="Address" />.</returns>
        public Address AddressesAdd(Address address)
        {
            p_Address pAddress = new p_Address();
            MigrateAddress(address, pAddress);

            _addressRepo.Insert(pAddress);
            _sanctionListUnitOfWork.Execute();

            address.AddressId = pAddress.AddressId;

            // Set OriginalId to system id, if it is null
            if (address.OriginalAddressId == null)
            {
                pAddress.OriginalAddressId = address.OriginalAddressId = pAddress.AddressId;
                _sanctionListUnitOfWork.GetRepository<p_Address>().Update(pAddress);
                _sanctionListUnitOfWork.Execute();
            }

            return address;
        }

        /// <summary>
        /// Updates <see cref="Address" />.
        /// </summary>
        /// <param name="address"><see cref="Address" /> to be updated.</param>
        /// <returns>Updated <see cref="Address" />.</returns>
        /// <exception cref="System.ArgumentException">The address could not be found.;address</exception>
        public Address AddressesUpdate(Address address)
        {
            p_Address pAddress = _addressRepo.GetById(address.AddressId);
            if (pAddress == null)
            {
                throw new ArgumentException("The address could not be found.", "address");
            }

            if (address.Remark != null && String.IsNullOrEmpty(address.Remark.Value))
            {
                if (pAddress.Remark != null)
                {
                    _remarkRepo.Delete(pAddress.Remark.RemarkId);
                }

                address.Remark = null;
            }

            MigrateAddress(address, pAddress);

            _addressRepo.Update(pAddress);
            _sanctionListUnitOfWork.Execute();

            return address;
        }

        /// <summary>
        /// Removes <see cref="Address" />.
        /// </summary>
        /// <param name="addressId">Id of the <see cref="Address" /> to be removed.</param>
        public void AddressesRemove(int addressId)
        {
            p_Address address = _sanctionListUnitOfWork.GetRepository<p_Address>().GetById(addressId);
            if (address != null)
            {
                _sanctionListUnitOfWork.GetRepository<p_Address>().Delete(addressId);
                _sanctionListUnitOfWork.GetRepository<p_Remark>().Delete(address.RemarkId);
                _sanctionListUnitOfWork.Execute();
            }
        }

        /// <summary>
        /// Gets address information by identifier.
        /// </summary>
        /// <param name="addressId">The Addresses identifier.</param>
        /// <returns>Added <see cref="Address" />.</returns>
        public Address AddressesGetById(int addressId)
        {
            p_Address address = _addressRepo.Get(a => a.AddressId == addressId).SingleOrDefault();

            return (address == null) ? null : MapAddress(address);
        }

        #endregion


        #region [Birth]

        /// <summary>
        /// Gets array of <see cref="Birth" />,
        /// </summary>
        /// <param name="entityId">Id if the <see cref="Entity" /> that is related to Births.</param>
        /// <returns>Array of <see cref="Birth" />.</returns>
        public Birth[] BirthsGet(int entityId)
        {
            IEnumerable<p_Birth> births = _birthRepo.Get(b => b.EntityId == entityId);

            return births.Select(b => MapBirth(b)).ToArray();
        }

        /// <summary>
        /// Adds new <see cref="Birth" />.
        /// </summary>
        /// <param name="birth"><see cref="Birth" /> object to be added.</param>
        /// <returns>Added <see cref="Birth" />.</returns>
        public Birth BirthsAdd(Birth birth)
        {
            p_Birth pBirth = new p_Birth();
            MigrateBirth(birth, pBirth);

            _birthRepo.Insert(pBirth);
            _sanctionListUnitOfWork.Execute();

            birth.BirthId = pBirth.BirthId;

            // Set OriginalId to system id, if it is null
            if (birth.OriginalBirthId == null)
            {
                pBirth.OriginalBirthId = birth.OriginalBirthId = pBirth.BirthId;
                _sanctionListUnitOfWork.GetRepository<p_Birth>().Update(pBirth);
                _sanctionListUnitOfWork.Execute();
            }

            return birth;
        }

        /// <summary>
        /// Birthses the update.
        /// </summary>
        /// <param name="birth">The birth.</param>
        /// <returns>Entities.Birth.</returns>
        /// <exception cref="System.ArgumentException">The birth could not be found.;birth</exception>
        public Birth BirthsUpdate(Birth birth)
        {
            p_Birth pBirth = _birthRepo.GetById(birth.BirthId);
            if (pBirth == null)
            {
                throw new ArgumentException("The birth could not be found.", "birth");
            }

            if (birth.Remark != null && String.IsNullOrEmpty(birth.Remark.Value))
            {
                if (pBirth.Remark != null)
                {
                    _remarkRepo.Delete(pBirth.Remark.RemarkId);
                }

                birth.Remark = null;
            }

            MigrateBirth(birth, pBirth);

            _birthRepo.Update(pBirth);
            _sanctionListUnitOfWork.Execute();

            return birth;
        }

        /// <summary>
        /// Removes <see cref="Birth" />.
        /// </summary>
        /// <param name="birthId">Id of <see cref="Birth" /> to be removed.</param>
        public void BirthsRemove(int birthId)
        {
            p_Birth birth = _birthRepo.GetById(birthId);
            if (birth != null)
            {
                _birthRepo.Delete(birthId);
                _remarkRepo.Delete(birth.RemarkId);
                _sanctionListUnitOfWork.Execute();
            }
        }

        /// <summary>
        /// Gets birth information by identifier.
        /// </summary>
        /// <param name="birthId">The Birth identifier.</param>
        /// <returns>Get <see cref="Birth" />.</returns>
        public Birth BirthsGetById(int birthId)
        {
            p_Birth birth = _birthRepo.Get(b => b.BirthId == birthId).SingleOrDefault();

            return (birth == null) ? null : MapBirth(birth);
        }
        #endregion


        #region [Bank]

        /// <summary>
        /// Gets array of <see cref="Bank" />.
        /// </summary>
        /// <param name="entityId">Id if the <see cref="Entity" /> that is related to Banks.</param>
        /// <returns>Array of <see cref="Bank" />.</returns>
        public Bank[] BanksGet(int entityId)
        {
            IEnumerable<p_Bank> banks = _bankRepo.Get(b => b.EntityId == entityId);

            return banks.Select(b => MapBank(b)).ToArray();
        }

        /// <summary>
        /// Adds new <see cref="Bank" />.
        /// </summary>
        /// <param name="bank"><see cref="Bank" /> to be added.</param>
        /// <returns>Added <see cref="Bank" />.</returns>
        public Bank BanksAdd(Bank bank)
        {
            p_Bank pBank = new p_Bank();
            MigrateBank(bank, pBank);

            _bankRepo.Insert(pBank);
            _sanctionListUnitOfWork.Execute();

            bank.BankId = pBank.BankId;

            // Set OriginalId to system id, if it is null
            if (bank.OriginalBankId == null)
            {
                pBank.OriginalBankId = bank.OriginalBankId = pBank.BankId;
                _sanctionListUnitOfWork.GetRepository<p_Bank>().Update(pBank);
                _sanctionListUnitOfWork.Execute();
            }

            return bank;
        }

        /// <summary>
        /// Updates <see cref="Bank" />.
        /// </summary>
        /// <param name="bank"><see cref="Bank" /> to be updated</param>
        /// <returns>Updated <see cref="Bank" />.</returns>
        /// <exception cref="System.ArgumentException">The bank could not be found.;bank</exception>
        public Bank BanksUpdate(Bank bank)
        {
            p_Bank pBank = _bankRepo.GetById(bank.BankId);
            if (pBank == null)
            {
                throw new ArgumentException("The bank could not be found.", "bank");
            }

            if (bank.Remark != null && String.IsNullOrEmpty(bank.Remark.Value))
            {
                if (pBank.Remark != null)
                {
                    _remarkRepo.Delete(pBank.Remark.RemarkId);
                }

                bank.Remark = null;
            }

            MigrateBank(bank, pBank);

            _sanctionListUnitOfWork.GetRepository<p_Bank>().Update(pBank);
            _sanctionListUnitOfWork.Execute();

            return bank;
        }

        /// <summary>
        /// Removes <see cref="Bank" />.
        /// </summary>
        /// <param name="bankId">Id of <see cref="Bank" /> to be removed.</param>
        public void BanksRemove(int bankId)
        {
            p_Bank bank = _bankRepo.GetById(bankId);
            if (bank != null)
            {
                _bankRepo.Delete(bankId);
                _remarkRepo.Delete(bank.RemarkId);
                _sanctionListUnitOfWork.Execute();
            }
        }

        /// <summary>
        /// Gets bank information by identifier.
        /// </summary>
        /// <param name="bankId">The Bank identifier.</param>
        /// <returns>Get <see cref="Bank" />.</returns>
        public Bank BanksGetById(int bankId)
        {
            p_Bank bank = _bankRepo.Get(b => b.BankId == bankId).SingleOrDefault();

            return (bank == null) ? null : MapBank(bank);
        }

        #endregion


        #region [Citizenship]

        /// <summary>
        /// Gets array of <see cref="Citizenship" />.
        /// </summary>
        /// <param name="entityId">Id if the <see cref="Entity" /> that is related to Citizenships.</param>
        /// <returns>Array of <see cref="Citizenship" />.</returns>
        public Citizenship[] CitizenshipsGet(int entityId)
        {
            IEnumerable<p_Citizenship> citizenships = _citizenshipRepo.Get(c => c.EntityId == entityId);

            return citizenships.Select(c => MapCitizenship(c)).ToArray();
        }

        /// <summary>
        /// Adds new <see cref="Citizenship" />.
        /// </summary>
        /// <param name="citizenship"><see cref="Citizenship" /> to add.</param>
        /// <returns>Added <see cref="Citizenship" />.</returns>
        public Citizenship CitizenshipsAdd(Citizenship citizenship)
        {
            p_Citizenship pCitizenship = new p_Citizenship();
            MigrateCitizenship(citizenship, pCitizenship);

            _citizenshipRepo.Insert(pCitizenship);
            _sanctionListUnitOfWork.Execute();

            citizenship.CitizenshipId = pCitizenship.CitizenshipId;

            // Set OriginalId to system id, if it is null
            if (citizenship.OriginalCitizenshipId == null)
            {
                pCitizenship.OriginalCitizenshipId = citizenship.OriginalCitizenshipId = pCitizenship.CitizenshipId;
                _sanctionListUnitOfWork.GetRepository<p_Citizenship>().Update(pCitizenship);
                _sanctionListUnitOfWork.Execute();
            }

            return citizenship;
        }

        /// <summary>
        /// Updates <see cref="Citizenship" />.
        /// </summary>
        /// <param name="citizenship"><see cref="Citizenship" /> to be updated.</param>
        /// <returns>Updated <see cref="Citizenship" />.</returns>
        /// <exception cref="System.ArgumentException">The citizenship could not be found.;citizenship</exception>
        public Citizenship CitizenshipsUpdate(Citizenship citizenship)
        {
            p_Citizenship pCitizenship = _sanctionListUnitOfWork.GetRepository<p_Citizenship>().GetById(citizenship.CitizenshipId);
            if (pCitizenship == null)
            {
                throw new ArgumentException("The citizenship could not be found.", "citizenship");
            }

            if (citizenship.Remark != null && String.IsNullOrEmpty(citizenship.Remark.Value))
            {
                if (pCitizenship.Remark != null)
                {
                    _remarkRepo.Delete(pCitizenship.Remark.RemarkId);
                }

                citizenship.Remark = null;
            }

            MigrateCitizenship(citizenship, pCitizenship);

            _citizenshipRepo.Update(pCitizenship);
            _sanctionListUnitOfWork.Execute();

            return citizenship;
        }

        /// <summary>
        /// Removes <see cref="Citizenship" />.
        /// </summary>
        /// <param name="citizenshipId">ID of <see cref="Citizenship" /> to be removed.</param>
        public void CitizenshipsRemove(int citizenshipId)
        {
            p_Citizenship citizenship = _sanctionListUnitOfWork.GetRepository<p_Citizenship>().GetById(citizenshipId);
            if (citizenship != null)
            {
                _citizenshipRepo.Delete(citizenshipId);
                _remarkRepo.Delete(citizenship.RemarkId);
                _sanctionListUnitOfWork.Execute();
            }
        }

        /// <summary>
        /// Gets citizenship information by identifier.
        /// </summary>
        /// <param name="citizenshipId">The Citizenship identifier.</param>
        /// <returns>Get <see cref="Citizenship" />.</returns>
        public Citizenship CitizenshipsGetById(int citizenshipId)
        {
            p_Citizenship citizenship = _citizenshipRepo.Get(c => c.CitizenshipId == citizenshipId).SingleOrDefault();

            return (citizenship == null) ? null : MapCitizenship(citizenship);
        }
        #endregion


        #region [ContactInfo]

        /// <summary>
        /// Gets the collection of contact information by entity identifier.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns>Collection of contact information.</returns>
        public ContactInfo[] ContactInfoGet(int entityId)
        {
            IEnumerable<p_ContactInfo> contactInfo = _contactInfoRepo.Get(c => c.EntityId == entityId);

            return contactInfo.Select(c => MapContactInfo(c)).ToArray();
        }

        /// <summary>
        /// Gets contact information by identifier.
        /// </summary>
        /// <param name="contactInfoId">The ContactInfo identifier.</param>
        /// <returns>Collection of contact information.</returns>
        public ContactInfo ContactInfoGetById(int contactInfoId)
        {
            p_ContactInfo contactInfo = _contactInfoRepo.Get(c => c.ContactInfoId == contactInfoId).SingleOrDefault();

            return (contactInfo == null) ? null : MapContactInfo(contactInfo);
        }

        /// <summary>
        /// Inserts a contact information data to database.
        /// </summary>
        /// <param name="contactInfo">A contact information to insert.</param>
        /// <returns>The inserted contact Information.</returns>
        public ContactInfo ContactInfoAdd(ContactInfo contactInfo)
        {
            p_ContactInfo pContactInfo = new p_ContactInfo();
            MigrateContactInfo(contactInfo, pContactInfo);

            _contactInfoRepo.Insert(pContactInfo);
            _sanctionListUnitOfWork.Execute();

            contactInfo.ContactInfoId = pContactInfo.ContactInfoId;

            // Set OriginalId to system id, if it is null
            if (contactInfo.OriginalContactInfoId == null)
            {
                pContactInfo.OriginalContactInfoId = contactInfo.OriginalContactInfoId = pContactInfo.ContactInfoId;
                _sanctionListUnitOfWork.GetRepository<p_ContactInfo>().Update(pContactInfo);
                _sanctionListUnitOfWork.Execute();
            }

            return contactInfo;
        }

        /// <summary>
        /// Updates a contact information in database.
        /// </summary>
        /// <param name="contactInfo">A contact information to update.</param>
        /// <returns>The updated contact information.</returns>
        /// <exception cref="System.ArgumentException">The contact info could not be found.;contactInfo</exception>
        public ContactInfo ContactInfoUpdate(ContactInfo contactInfo)
        {
            p_ContactInfo pContactInfo = _contactInfoRepo.GetById(contactInfo.ContactInfoId);
            if (pContactInfo == null)
            {
                throw new ArgumentException("The contact info could not be found.", "contactInfo");
            }

            if (contactInfo.Remark != null && String.IsNullOrEmpty(contactInfo.Remark.Value))
            {
                if (pContactInfo.Remark != null)
                {
                    _remarkRepo.Delete(pContactInfo.Remark.RemarkId);
                }

                contactInfo.Remark = null;
            }

            MigrateContactInfo(contactInfo, pContactInfo);

            _contactInfoRepo.Update(pContactInfo);
            _sanctionListUnitOfWork.Execute();


            return contactInfo;
        }

        /// <summary>
        /// Deletes a contact information from database by contact information identifier.
        /// </summary>
        /// <param name="contactInfoId">The contact information identifier.</param>
        public void ContactInfoRemove(int contactInfoId)
        {
            p_ContactInfo contactInfo = _sanctionListUnitOfWork.GetRepository<p_ContactInfo>().GetById(contactInfoId);
            if (contactInfo != null)
            {
                _contactInfoRepo.Delete(contactInfoId);
                _remarkRepo.Delete(contactInfo.RemarkId);
                _sanctionListUnitOfWork.Execute();
            }
        }

        #endregion


        #region [Identification]

        /// <summary>
        /// Gets the collection of indentification by entity identifier.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns>Collection of identifications.</returns>
        public Identification[] IdentificationsGet(int entityId)
        {
            IEnumerable<p_Identification> identifications = _identificationRepo.Get(i => i.EntityId == entityId);

            return identifications.Select(i => MapIdentification(i)).ToArray();
        }

        /// <summary>
        /// Inserts an identification data to database.
        /// </summary>
        /// <param name="identification">An identification to insert.</param>
        /// <returns>The inserted identification.</returns>
        public Identification IdentificationsAdd(Identification identification)
        {
            p_Identification pIdentification = new p_Identification();
            MigrateIdentification(identification, pIdentification);

            _identificationRepo.Insert(pIdentification);
            _sanctionListUnitOfWork.Execute();

            identification.IdentificationId = pIdentification.IdentificationId;

            // Set OriginalId to system id, if it is null
            if (identification.OriginalIdentificationId == null)
            {
                pIdentification.OriginalIdentificationId = identification.OriginalIdentificationId = pIdentification.IdentificationId;
                _sanctionListUnitOfWork.GetRepository<p_Identification>().Update(pIdentification);
                _sanctionListUnitOfWork.Execute();
            }

            return identification;
        }

        /// <summary>
        /// Updates an identification data in database.
        /// </summary>
        /// <param name="identification">An identification to update.</param>
        /// <returns>The updated identification.</returns>
        /// <exception cref="System.ArgumentException">The identification could not be found.;identification</exception>
        public Identification IdentificationsUpdate(Identification identification)
        {
            p_Identification pIdentification = _identificationRepo.GetById(identification.IdentificationId);
            if (pIdentification == null)
            {
                throw new ArgumentException("The identification could not be found.", "identification");
            }

            if (identification.Remark != null && String.IsNullOrEmpty(identification.Remark.Value))
            {
                if (pIdentification.Remark != null)
                {
                    _remarkRepo.Delete(pIdentification.Remark.RemarkId);
                }

                identification.Remark = null;
            }

            MigrateIdentification(identification, pIdentification);

            _identificationRepo.Update(pIdentification);
            _sanctionListUnitOfWork.Execute();

            return identification;
        }

        /// <summary>
        /// Removes an identification from database.
        /// </summary>
        /// <param name="identificationId">The identification identifier.</param>
        public void IdentificationsRemove(int identificationId)
        {
            p_Identification identification = _identificationRepo.GetById(identificationId);
            if (identification != null)
            {
                _identificationRepo.Delete(identificationId);
                _remarkRepo.Delete(identification.RemarkId);
                _sanctionListUnitOfWork.Execute();
            }
        }

        /// <summary>
        /// Gets contact information by identifier.
        /// </summary>
        /// <param name="identificationId">The identification identifier.</param>
        /// <returns>Get <see cref="ContactInfo" />.</returns>
        public Identification IdentificationsGetById(int identificationId)
        {
            p_Identification identification = _identificationRepo.Get(i => i.IdentificationId == identificationId).SingleOrDefault();

            return (identification == null) ? null : MapIdentification(identification);
        }

        #endregion


        #region [Entity]

        /// <summary>
        /// Gets collection of entities with some limitation that you want.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <param name="offset">The starting record of entities data in database.</param>
        /// <param name="limit">The amount of entities data that you want.</param>
        /// <returns>Collection of entities.</returns>
        public Entity[] EntitiesGet(int? listTypeId, int? offset = null, int? limit = null)
        {
            Page<p_Entity> page = null;
            if (offset.HasValue || limit.HasValue)
            {
                page = new Page<p_Entity>
                {
                    Limit = limit,
                    Offset = offset,
                    OrderBy = e => e.OrderBy(o => o.EntityId)
                };
            }
            IEnumerable<p_Entity> entities = (listTypeId.HasValue)
                          ? _entityRepo.Get(l => l.ListTypeId == listTypeId.Value, page)
                          : _entityRepo.Get(page: page);

            return entities.Select(e => MapEntity(e)).ToArray();
        }

        /// <summary>
        /// Gets an entity data by entity identifier.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns>An entity.</returns>
        public Entity EntitiesGetById(int entityId)
        {
            p_Entity entity = _entityRepo.Get(e => e.EntityId == entityId).SingleOrDefault();

            return (entity == null) ? null : MapEntity(entity);
        }

        /// <summary>
        /// Gets the relate regulation.
        /// </summary>
        /// <param name="regulation">The regulation.</param>
        /// <returns>p_Regulation.</returns>
        private p_Regulation GetRelateRegulation(Regulation regulation)
        {
            p_Regulation pRegulation = _relationRegulation.FirstOrDefault(r => r.PublicationTitle == regulation.PublicationTitle && r.ListTypeId == regulation.ListType.ListTypeId);
            if (pRegulation == null)
            {
                pRegulation = new p_Regulation();
                MigrateRegulation(regulation, pRegulation);
                _relationRegulation.Add(pRegulation);
            }
            return pRegulation;
        }

        /// <summary>
        /// Inserts an entity data to database.
        /// </summary>
        /// <param name="entity">an entity to insert.</param>
        /// <returns>The inserted entity.</returns>
        public Entity EntitiesAdd(Entity entity)
        {
            _relationRegulation = new List<p_Regulation>();


            p_Remark pRemark = null;
            if (entity.Remark != null)
            {
                pRemark = new p_Remark();
                pRemark.Value = entity.Remark.Value;
            }

            p_Entity pEntity = new p_Entity();
            pEntity.OriginalEntityId = entity.OriginalEntityId;
            if (entity.Regulation.RegulationId != default(int))
            {
                pEntity.RegulationId = entity.Regulation.RegulationId;
            }
            else
            {
                //pEntity.Regulation = new p_Regulation();
                //MigrateRegulation(entity.Regulation, pEntity.Regulation);
                pEntity.Regulation = GetRelateRegulation(entity.Regulation);
            }
            pEntity.SubjectTypeId = entity.SubjectType.SubjectTypeId;
            pEntity.StatusId = entity.Status.StatusId;
            pEntity.ListTypeId = entity.ListType.ListTypeId;
            pEntity.ListArchiveId = entity.ListArchiveId;
            pEntity.Remark = pRemark;

            // Inserts related entities if any.
            if (entity.Addresses != null && entity.Addresses.Count > 0)
            {
                ICollection<p_Address> pAddresses = new List<p_Address>();
                p_Address pAddress;
                foreach (Address address in entity.Addresses)
                {
                    pAddress = new p_Address();
                    MigrateAddress(address, pAddress);
                    pAddresses.Add(pAddress);
                }

                pEntity.Addresses = pAddresses;
            }
            if (entity.Banks != null && entity.Banks.Count > 0)
            {
                ICollection<p_Bank> pBanks = new List<p_Bank>();
                p_Bank pBank;
                foreach (Bank bank in entity.Banks)
                {
                    pBank = new p_Bank();
                    MigrateBank(bank, pBank);
                    pBanks.Add(pBank);
                }

                pEntity.Banks = pBanks;
            }
            if (entity.Births != null && entity.Births.Count > 0)
            {
                ICollection<p_Birth> pBirths = new List<p_Birth>();
                p_Birth pBirth;
                foreach (Birth birth in entity.Births)
                {
                    pBirth = new p_Birth();
                    MigrateBirth(birth, pBirth);
                    pBirths.Add(pBirth);
                }

                pEntity.Births = pBirths;
            }
            if (entity.Citizenships != null && entity.Citizenships.Count > 0)
            {
                ICollection<p_Citizenship> pCitizenships = new List<p_Citizenship>();
                p_Citizenship pCitizenship;
                foreach (Citizenship citizenship in entity.Citizenships)
                {
                    pCitizenship = new p_Citizenship();
                    MigrateCitizenship(citizenship, pCitizenship);
                    pCitizenships.Add(pCitizenship);
                }

                pEntity.Citizenships = pCitizenships;
            }
            if (entity.ContactInfo != null && entity.ContactInfo.Count > 0)
            {
                ICollection<p_ContactInfo> pContactInfoes = new List<p_ContactInfo>();
                p_ContactInfo pContactInfo;
                foreach (ContactInfo contactInfo in entity.ContactInfo)
                {
                    pContactInfo = new p_ContactInfo();
                    MigrateContactInfo(contactInfo, pContactInfo);
                    pContactInfoes.Add(pContactInfo);
                }

                pEntity.ContactInfo = pContactInfoes;
            }
            if (entity.Identifications != null && entity.Identifications.Count > 0)
            {
                ICollection<p_Identification> pIdentifications = new List<p_Identification>();
                p_Identification pIdentification;
                foreach (Identification identification in entity.Identifications)
                {
                    pIdentification = new p_Identification();
                    MigrateIdentification(identification, pIdentification);
                    pIdentifications.Add(pIdentification);
                }

                pEntity.Identifications = pIdentifications;
            }
            if (entity.NameAliases != null && entity.NameAliases.Count > 0)
            {
                ICollection<p_NameAlias> pNameAliases = new List<p_NameAlias>();
                p_NameAlias pNameAlias;
                foreach (NameAlias nameAlias in entity.NameAliases)
                {
                    pNameAlias = new p_NameAlias();
                    MigrateNameAlias(nameAlias, pNameAlias);
                    pNameAliases.Add(pNameAlias);
                }

                pEntity.NameAliases = pNameAliases;
            }

            _entityRepo.Insert(pEntity);
            _sanctionListUnitOfWork.Execute();

            // Clear relation regulation object.
            _relationRegulation.Clear();
            _relationRegulation = null;

            entity = MapEntity(pEntity);
            // Set OriginalId to system id, if it is null
            if (entity.OriginalEntityId == null)
            {
                pEntity.OriginalEntityId = entity.OriginalEntityId = pEntity.EntityId;
                _sanctionListUnitOfWork.GetRepository<p_Entity>().Update(pEntity);
                _sanctionListUnitOfWork.Execute();
            }

            return entity;
        }

        /// <summary>
        /// Updates entity in database.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <returns>The updated entity.</returns>
        /// <exception cref="System.ArgumentException">The entity could not be found.;contactInfo</exception>
        public Entity EntitiesUpdate(Entity entity)
        {
            p_Entity pEntity = _entityRepo.GetById(entity.EntityId);
            if (pEntity == null)
            {
                throw new ArgumentException("The entity could not be found.", "entity");
            }

            if (entity.Remark != null && String.IsNullOrEmpty(entity.Remark.Value))
            {
                if (pEntity.Remark != null)
                {
                    _remarkRepo.Delete(pEntity.Remark.RemarkId);
                }

                entity.Remark = null;
            }

            // mapping values
            pEntity.StatusId = entity.Status.StatusId;
            pEntity.SubjectTypeId = entity.SubjectType.SubjectTypeId;
            pEntity.RegulationId = entity.Regulation.RegulationId;
            if (entity.Remark != null)
            {
                if (pEntity.Remark != null)
                {
                    pEntity.Remark.Value = entity.Remark.Value;
                }
                else
                {
                    pEntity.Remark = new p_Remark { Value = entity.Remark.Value };
                }
            }

            _entityRepo.Update(pEntity);
            _sanctionListUnitOfWork.Execute();


            return entity;
        }

        /// <summary>
        /// Changes status of entities to another status in one call.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <param name="fromStatusId">From status identifier.</param>
        /// <param name="toStatusId">To status identifier.</param>
        /// <returns>The amount of records affected.</returns>
        public int EntitiesChangeStatus(int listTypeId, int fromStatusId, int toStatusId)
        {
            IEnumerable<p_Entity> entities = _entityRepo.Get(e => e.ListTypeId == listTypeId && e.StatusId == fromStatusId);
            entities.ToList().ForEach(e => e.StatusId = toStatusId);

            return _sanctionListUnitOfWork.Execute();
        }

        /// <summary>
        /// Deletes an entity from database.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        public void EntitiesRemove(int entityId)
        {
            p_Entity entity = _entityRepo.GetById(entityId);
            if (entity != null)
            {
                RemoveEntity(entity);
                _sanctionListUnitOfWork.Execute();
            }
        }

        /// <summary>
        /// Delete the entities from database by status identifier.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <param name="statusId">The status identifier to delete.</param>
        public void EntitiesRemoveByStatus(int listTypeId, int statusId)
        {
            foreach (p_Entity entity in _entityRepo.Get(e => e.ListTypeId == listTypeId && e.StatusId == statusId))
            {
                RemoveEntity(entity);
            }

            _sanctionListUnitOfWork.Execute();
        }

        /// <summary>
        /// Get amount of entities from database by list type identifier.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <returns>Amount of entities.</returns>
        public int EntitiesCount(int? listTypeId)
        {
            if (listTypeId != null)
            {
                return _entityRepo.GetCount(e => e.ListTypeId == listTypeId);
            }
            return _entityRepo.GetCount();
        }

        /// <summary>
        /// Gets collection of entities by condition with some limitation that you want.
        /// </summary>
        /// <param name="searchEntity">The search entity.</param>
        /// <param name="offset">The starting record of entities data in database.</param>
        /// <param name="limit">The amount of entities data that you want.</param>
        /// <returns>Collection of entities.</returns>
        public Entity[] EntitiesGet(SearchEntity searchEntity, int? offset = null, int? limit = null)
        {
            // Set Page
            Page<p_Entity> page = null;
            if (offset.HasValue || limit.HasValue)
            {
                page = new Page<p_Entity>
                {
                    Limit = limit,
                    Offset = offset,
                    OrderBy = e => e.OrderBy(o => o.EntityId)
                };
            }

            // Get query
            IQueryable<p_Entity> entities = EntitiesGetCondition(searchEntity, page);

            return entities.ToList().Select(MapEntity).ToArray();
        }

        /// <summary>
        /// Get amount of entities from database by list type identifier.
        /// </summary>
        /// <param name="searchEntity">The search entity.</param>
        /// <returns>Amount of entities.</returns>
        public int EntitiesCount(SearchEntity searchEntity)
        {
            return EntitiesGetCondition(searchEntity).Count();
        }

        #endregion


        #region [REMARK]

        /// <summary>
        /// Deletes remark from database.
        /// </summary>
        /// <param name="remark">The remark.</param>
        public void RemarksRemove(Remark remark)
        {
            _remarkRepo.Delete(new p_Remark { RemarkId = remark.RemarkId, Value = remark.Value });
            _sanctionListUnitOfWork.Execute();
        }

        #endregion


        #region [ListArchive]

        /// <summary>
        /// Inserts a list archive data to database.
        /// </summary>
        /// <param name="listArchive">A list archive to insert.</param>
        /// <returns>The inserted list archive data.</returns>
        public ListArchive ListArchiveAdd(ListArchive listArchive)
        {
            p_ListArchive pListArchive = new p_ListArchive
            {
                Date = listArchive.Date,
                File = listArchive.File
            };

            _listArchiveRepo.Insert(pListArchive);
            _sanctionListUnitOfWork.Execute();

            listArchive.ListArchiveId = pListArchive.ListArchiveId;

            return listArchive;
        }

        #endregion


        #region [SanctionListDetail]

        /// <summary>
        /// Gets all sanction lists basic information.
        /// </summary>
        /// <returns>Collection of sanction lists basic information.</returns>
        public SanctionListDetail[] SanctionListDetailGet()
        {
            ListType[] listTypes = ListTypesGet();
            SanctionListDetail[] sanctionListDetails = new SanctionListDetail[listTypes.Length];

            for (int i = 0; i < listTypes.Length; i++)
            {
                int currentListType = listTypes[i].ListTypeId;
                sanctionListDetails[i] = SanctionListDetailGet(currentListType);
            }

            return sanctionListDetails;
        }

        /// <summary>
        /// Sets a sanction list basic information by list type id.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <returns>A sanction list basic information.</returns>
        public SanctionListDetail SanctionListDetailGet(int listTypeId)
        {
            return new SanctionListDetail
            {
                ListType = ListTypesGetById(listTypeId),
                Update = UpdatesGet(listTypeId),
                EntitiesCount = _entityRepo.GetCount(e => e.ListTypeId == listTypeId)
            };
        }
        #endregion


        #region [Migrate methods]

        /// <summary>
        /// Converts <see cref="Regulation" /> entity to <see cref="p_Regulation" /> poco.
        /// </summary>
        /// <param name="regulation">Migrated object.</param>
        /// <param name="regulationResult">Migration result.</param>
        private void MigrateRegulation(Regulation regulation, p_Regulation regulationResult)
        {
            regulationResult.Programme = regulation.Programme;
            regulationResult.RegulationTitle = regulation.RegulationTitle;
            regulationResult.RegulationDate = regulation.RegulationDate;
            regulationResult.PublicationTitle = regulation.PublicationTitle;
            regulationResult.PublicationDate = regulation.PublicationDate;
            regulationResult.PublicationUrl = regulation.PublicationUrl;
            regulationResult.ListTypeId = regulation.ListType.ListTypeId;
            if (regulation.Remark != null)
            {
                if (regulationResult.Remark != null)
                {
                    regulationResult.Remark.Value = regulation.Remark.Value;
                }
                else
                {
                    regulationResult.Remark = new p_Remark { Value = regulation.Remark.Value };
                }
            }
        }

        /// <summary>
        /// Converts <see cref="NameAlias" /> entity to <see cref="p_NameAlias" /> poco.
        /// </summary>
        /// <param name="nameAlias">Migrated object.</param>
        /// <param name="nameAliasResult">Migration result.</param>
        private void MigrateNameAlias(NameAlias nameAlias, p_NameAlias nameAliasResult)
        {
            nameAliasResult.OriginalNameAliasId = nameAlias.OriginalNameAliasId;
            nameAliasResult.EntityId = nameAlias.EntityId;

            if (nameAlias.Regulation.RegulationId != default(int))
            {
                nameAliasResult.RegulationId = nameAlias.Regulation.RegulationId;
            }
            else
            {
                nameAliasResult.Regulation = GetRelateRegulation(nameAlias.Regulation);
            }

            nameAliasResult.LastName = nameAlias.LastName;
            nameAliasResult.FirstName = nameAlias.FirstName;
            nameAliasResult.MiddleName = nameAlias.MiddleName;
            nameAliasResult.WholeName = nameAlias.WholeName;
            nameAliasResult.PrefixName = nameAlias.PrefixName;
            nameAliasResult.GenderId = nameAlias.Gender.GenderId;
            nameAliasResult.Title = nameAlias.Title;
            nameAliasResult.LanguageIso3 = (nameAlias.Language == null) ? null : nameAlias.Language.Iso3;

            if (nameAlias.Remark != null)
            {
                if (nameAliasResult.Remark != null)
                {
                    nameAliasResult.Remark.Value = nameAlias.Remark.Value;
                }
                else
                {
                    nameAliasResult.Remark = new p_Remark { Value = nameAlias.Remark.Value };
                }
            }

            nameAliasResult.Quality = nameAlias.Quality;
            nameAliasResult.Function = nameAlias.Function;
        }

        /// <summary>
        /// Converts <see cref="Address" /> entity to <see cref="p_Address" /> poco.
        /// </summary>
        /// <param name="address">Migrated object.</param>
        /// <param name="addressResult">Migration result.</param>
        private void MigrateAddress(Address address, p_Address addressResult)
        {
            addressResult.OriginalAddressId = address.OriginalAddressId;
            addressResult.EntityId = address.EntityId;

            if (address.Regulation.RegulationId != default(int))
            {
                addressResult.RegulationId = address.Regulation.RegulationId;
            }
            else
            {
                addressResult.Regulation = GetRelateRegulation(address.Regulation);
            }

            addressResult.Number = address.Number;
            addressResult.Street = address.Street;
            addressResult.Zipcode = address.Zipcode;
            addressResult.City = address.City;
            addressResult.CountryIso3 = (address.Country == null) ? null : address.Country.Iso3;
            if (address.Remark != null)
            {
                if (addressResult.Remark != null)
                {
                    addressResult.Remark.Value = address.Remark.Value;
                }
                else
                {
                    addressResult.Remark = new p_Remark { Value = address.Remark.Value };
                }
            }
        }

        /// <summary>
        /// Converts <see cref="Birth" /> entity to <see cref="p_Birth" /> poco.
        /// </summary>
        /// <param name="birth">Migrated object.</param>
        /// <param name="birthResult">Migration result.</param>
        private void MigrateBirth(Birth birth, p_Birth birthResult)
        {
            birthResult.OriginalBirthId = birth.OriginalBirthId;
            birthResult.EntityId = birth.EntityId;

            if (birth.Regulation.RegulationId != default(int))
            {
                birthResult.RegulationId = birth.Regulation.RegulationId;
            }
            else
            {
                birthResult.Regulation = GetRelateRegulation(birth.Regulation);
            }

            birthResult.Year = birth.Year;
            birthResult.Month = birth.Month;
            birthResult.Day = birth.Day;
            birthResult.Place = birth.Place;
            birthResult.CountryIso3 = (birth.Country == null) ? null : birth.Country.Iso3;
            if (birth.Remark != null)
            {
                if (birthResult.Remark != null)
                {
                    birthResult.Remark.Value = birth.Remark.Value;
                }
                else
                {
                    birthResult.Remark = new p_Remark { Value = birth.Remark.Value };
                }
            }
        }

        /// <summary>
        /// Converts <see cref="Bank" /> entity to <see cref="p_Bank" /> poco.
        /// </summary>
        /// <param name="bank">Migrated object.</param>
        /// <param name="bankResult">Migration result.</param>
        private void MigrateBank(Bank bank, p_Bank bankResult)
        {
            bankResult.EntityId = bank.EntityId;
            bankResult.BankName = bank.BankName;
            bankResult.OriginalBankId = bank.OriginalBankId;
            bankResult.AccountHolderName = bank.AccountHolderName;
            bankResult.AccountNumber = bank.AccountNumber;
            bankResult.CountryIso3 = (bank.Country == null) ? null : bank.Country.Iso3;
            bankResult.Swift = bank.Swift;
            bankResult.Iban = bank.Iban;
            if (bank.Remark != null)
            {
                if (bankResult.Remark != null)
                {
                    bankResult.Remark.Value = bank.Remark.Value;
                }
                else
                {
                    bankResult.Remark = new p_Remark { Value = bank.Remark.Value };
                }
            }
        }

        /// <summary>
        /// Converts <see cref="Citizenship" /> entity to <see cref="p_Citizenship" /> poco.
        /// </summary>
        /// <param name="citizenship">Migrated object.</param>
        /// <param name="citizenshipResult">Migration result.</param>
        private void MigrateCitizenship(Citizenship citizenship, p_Citizenship citizenshipResult)
        {
            citizenshipResult.OriginalCitizenshipId = citizenship.OriginalCitizenshipId;
            citizenshipResult.EntityId = citizenship.EntityId;

            if (citizenship.Regulation.RegulationId != default(int))
            {
                citizenshipResult.RegulationId = citizenship.Regulation.RegulationId;
            }
            else
            {
                citizenshipResult.Regulation = GetRelateRegulation(citizenship.Regulation);
            }

            if (citizenship.Country != null)
            {
                citizenshipResult.CountryIso3 = citizenship.Country.Iso3;
            }

            if (citizenship.Remark != null)
            {
                if (citizenshipResult.Remark != null)
                {
                    citizenshipResult.Remark.Value = citizenship.Remark.Value;
                }
                else
                {
                    citizenshipResult.Remark = new p_Remark { Value = citizenship.Remark.Value };
                }
            }
        }

        /// <summary>
        /// Converts <see cref="ContactInfo" /> entity to <see cref="p_ContactInfo" /> poco.
        /// </summary>
        /// <param name="contactInfo">Migrated object.</param>
        /// <param name="contactInfoResult">Migration result.</param>
        private void MigrateContactInfo(ContactInfo contactInfo, p_ContactInfo contactInfoResult)
        {
            contactInfoResult.OriginalContactInfoId = contactInfo.OriginalContactInfoId;
            contactInfoResult.EntityId = contactInfo.EntityId;

            if (contactInfo.Regulation.RegulationId != default(int))
            {
                contactInfoResult.RegulationId = contactInfo.Regulation.RegulationId;
            }
            else
            {
                contactInfoResult.Regulation = GetRelateRegulation(contactInfo.Regulation);
            }

            contactInfoResult.ContactInfoTypeId = contactInfo.ContactInfoType.ContactInfoTypeId;
            contactInfoResult.Value = contactInfo.Value;
            if (contactInfo.Remark != null)
            {
                if (contactInfoResult.Remark != null)
                {
                    contactInfoResult.Remark.Value = contactInfo.Remark.Value;
                }
                else
                {
                    contactInfoResult.Remark = new p_Remark { Value = contactInfo.Remark.Value };
                }
            }
        }

        /// <summary>
        /// Converts <see cref="Identification" /> entity to <see cref="p_Identification" /> poco.
        /// </summary>
        /// <param name="identification">Migrated object.</param>
        /// <param name="identificationResult">Migration result.</param>
        private void MigrateIdentification(Identification identification, p_Identification identificationResult)
        {
            identificationResult.OriginalIdentificationId = identification.OriginalIdentificationId;
            identificationResult.EntityId = identification.EntityId;

            if (identification.Regulation.RegulationId != default(int))
            {
                identificationResult.RegulationId = identification.Regulation.RegulationId;
            }
            else
            {
                identificationResult.Regulation = GetRelateRegulation(identification.Regulation);
            }

            identificationResult.IdentificationTypeId = identification.IdentificationType.IdentificationTypeId;
            identificationResult.DocumentNumber = identification.DocumentNumber;
            identificationResult.IssueCity = identification.IssueCity;
            identificationResult.IssueCountryIso3 = (identification.IssueCountry == null) ? null : identification.IssueCountry.Iso3;
            identificationResult.IssueDate = (!identification.IssueDate.HasValue) ? null : identification.IssueDate;
            identificationResult.ExpiryDate = (!identification.ExpiryDate.HasValue) ? null : identification.ExpiryDate;
            if (identification.Remark != null)
            {
                if (identificationResult.Remark != null)
                {
                    identificationResult.Remark.Value = identification.Remark.Value;
                }
                else
                {
                    identificationResult.Remark = new p_Remark { Value = identification.Remark.Value };
                }
            }
        }

        #endregion


        #region [Mapping methods]

        /// <summary>
        /// Maps the  <see cref="p_EnumCountry"/> poco to
        /// <see cref="Country"/> entity.
        /// </summary>
        /// <param name="country">The <see cref="p_EnumCountry"/> poco.</param>
        /// <returns><see cref="Country"/> entity.</returns>
        private Country MapCountry(p_EnumCountry country)
        {
            return (country == null)
                   ? null
                   : new Country
                     {
                         Iso2 = country.Iso2,
                         Iso3 = country.Iso3,
                         Name = country.Name,
                         NiceName = country.NiceName,
                         NumCode = country.NumCode,
                         PhoneCode = country.PhoneCode
                     };
        }

        /// <summary>
        /// Maps the  <see cref="p_EnumContactInfoType" /> poco to
        /// <see cref="ContactInfoType" /> entity.
        /// </summary>
        /// <param name="contactInfoType">Type of the contact information.</param>
        /// <returns><see cref="ContactInfoType" /> entity.</returns>
        private ContactInfoType MapContactInfoType(p_EnumContactInfoType contactInfoType)
        {
            return (contactInfoType == null)
                   ? null
                   : new ContactInfoType
                     {
                         ContactInfoTypeId = contactInfoType.ContactInfoTypeId,
                         Name = contactInfoType.Name
                     };
        }

        /// <summary>
        /// Maps the  <see cref="p_EnumIdentificationType" /> poco to
        /// <see cref="IdentificationType" /> entity.
        /// </summary>
        /// <param name="identificationType">Type of the identification.</param>
        /// <returns><see cref="IdentificationType" /> entity.</returns>
        private IdentificationType MapIdentificationType(p_EnumIdentificationType identificationType)
        {
            return (identificationType == null)
                   ? null
                   : new IdentificationType
                     {
                         Description = identificationType.Description,
                         IdentificationTypeId = identificationType.IdentificationTypeId,
                         Name = identificationType.Name
                     };
        }

        /// <summary>
        /// Maps the  <see cref="p_EnumListType" /> poco to
        /// <see cref="ListType" /> entity.
        /// </summary>
        /// <param name="listType">Type of the list.</param>
        /// <returns><see cref="ListType" /> entity.</returns>
        private ListType MapListType(p_EnumListType listType)
        {
            return (listType == null)
                   ? null
                   : new ListType
                     {
                         Description = listType.Description,
                         ListTypeId = listType.ListTypeId,
                         Name = listType.Name
                     };
        }

        /// <summary>
        /// Maps the  <see cref="p_EnumGender" /> poco to
        /// <see cref="Gender" /> entity.
        /// </summary>
        /// <param name="gender">The gender.</param>
        /// <returns><see cref="Gender" /> entity.</returns>
        private Gender MapGender(p_EnumGender gender)
        {
            return (gender == null)
                   ? null
                   : new Gender
                     {
                         GenderId = gender.GenderId,
                         Name = gender.Name
                     };
        }

        /// <summary>
        /// Maps the  <see cref="p_EnumLanguage" /> poco to
        /// <see cref="Language" /> entity.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <returns><see cref="Language" /> entity.</returns>
        private Language MapLanguage(p_EnumLanguage language)
        {
            return (language == null)
                   ? null
                   : new Language
                     {
                         Iso2 = language.Iso2,
                         Iso3 = language.Iso3,
                         Name = language.Name
                     };
        }

        /// <summary>
        /// Maps the  <see cref="p_EnumStatus" /> poco to
        /// <see cref="Status" /> entity.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns><see cref="Status" /> entity.</returns>
        private Status MapStatus(p_EnumStatus status)
        {
            return new Status
            {
                Name = status.Name,
                StatusId = status.StatusId
            };
        }

        /// <summary>
        /// Maps the  <see cref="p_EnumSubjectType" /> poco to
        /// <see cref="SubjectType" /> entity.
        /// </summary>
        /// <param name="subjectType">Type of the subject.</param>
        /// <returns><see cref="SubjectType" /> entity.</returns>
        private SubjectType MapSubjectType(p_EnumSubjectType subjectType)
        {
            return new SubjectType
            {
                Name = subjectType.Name,
                SubjectTypeId = subjectType.SubjectTypeId
            };
        }

        /// <summary>
        /// Maps the  <see cref="p_Remark" /> poco to
        /// <see cref="Remark" /> entity.
        /// </summary>
        /// <param name="remark">The remark.</param>
        /// <returns><see cref="Remark" /> entity.</returns>
        private Remark MapRemark(p_Remark remark)
        {
            return (remark == null)
                   ? null
                   : new Remark
                   {
                       RemarkId = remark.RemarkId,
                       Value = remark.Value
                   };
        }

        /// <summary>
        /// Maps the  <see cref="p_Regulation" /> poco to
        /// <see cref="Regulation" /> entity.
        /// </summary>
        /// <param name="regulation">The regulation.</param>
        /// <returns><see cref="Regulation" /> entity.</returns>
        private Regulation MapRegulation(p_Regulation regulation)
        {
            return new Regulation
            {
                RegulationId = regulation.RegulationId,
                RegulationTitle = regulation.RegulationTitle,
                RegulationDate = regulation.RegulationDate,
                PublicationTitle = regulation.PublicationTitle,
                PublicationDate = regulation.PublicationDate,
                PublicationUrl = regulation.PublicationUrl,
                Programme = regulation.Programme,
                Remark = MapRemark(regulation.Remark)
            };
        }

        /// <summary>
        /// Maps the  <see cref="p_Address" /> poco to
        /// <see cref="Address" /> entity.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns><see cref="Address" /> entity.</returns>
        private Address MapAddress(p_Address address)
        {
            return new Address
            {
                AddressId = address.AddressId,
                City = address.City,
                Country = MapCountry(address.EnumCountry),
                EntityId = address.EntityId,
                Number = address.Number,
                OriginalAddressId = address.OriginalAddressId,
                Regulation = MapRegulation(address.Regulation),
                Remark = MapRemark(address.Remark),
                Street = address.Street,
                Zipcode = address.Zipcode
            };
        }

        /// <summary>
        /// Maps the  <see cref="p_Bank" /> poco to
        /// <see cref="Bank" /> entity.
        /// </summary>
        /// <param name="bank">The bank.</param>
        /// <returns><see cref="Bank" /> entity.</returns>
        private Bank MapBank(p_Bank bank)
        {
            return new Bank
            {
                AccountHolderName = bank.AccountHolderName,
                AccountNumber = bank.AccountNumber,
                BankId = bank.BankId,
                BankName = bank.BankName,
                Country = MapCountry(bank.EnumCountry),
                EntityId = bank.EntityId,
                Iban = bank.Iban,
                Remark = MapRemark(bank.Remark),
                Swift = bank.Swift
            };
        }

        /// <summary>
        /// Maps the  <see cref="p_Birth" /> poco to
        /// <see cref="Birth" /> entity.
        /// </summary>
        /// <param name="birth">The birth.</param>
        /// <returns><see cref="Birth" /> entity.</returns>
        private Birth MapBirth(p_Birth birth)
        {
            return new Birth
            {
                BirthId = birth.BirthId,
                Day = birth.Day,
                Month = birth.Month,
                OriginalBirthId = birth.OriginalBirthId,
                Place = birth.Place,
                Year = birth.Year,
                Country = MapCountry(birth.EnumCountry),
                Remark = MapRemark(birth.Remark),
                EntityId = birth.EntityId,
                Regulation = MapRegulation(birth.Regulation)
            };
        }

        /// <summary>
        /// Maps the  <see cref="p_Citizenship" /> poco to
        /// <see cref="Citizenship" /> entity.
        /// </summary>
        /// <param name="citizenship">The citizenship.</param>
        /// <returns><see cref="Citizenship" /> entity.</returns>
        private Citizenship MapCitizenship(p_Citizenship citizenship)
        {
            return new Citizenship
            {
                CitizenshipId = citizenship.CitizenshipId,
                Country = MapCountry(citizenship.EnumCountry),
                EntityId = citizenship.EntityId,
                OriginalCitizenshipId = citizenship.OriginalCitizenshipId,
                Regulation = MapRegulation(citizenship.Regulation),
                Remark = MapRemark(citizenship.Remark)
            };
        }

        /// <summary>
        /// Maps the  <see cref="p_ContactInfo" /> poco to
        /// <see cref="ContactInfo" /> entity.
        /// </summary>
        /// <param name="contactInfo">The contact information.</param>
        /// <returns><see cref="ContactInfo" /> entity.</returns>
        private ContactInfo MapContactInfo(p_ContactInfo contactInfo)
        {
            return new ContactInfo
            {
                ContactInfoId = contactInfo.ContactInfoId,
                ContactInfoType = MapContactInfoType(contactInfo.EnumContactInfoType),
                EntityId = contactInfo.EntityId,
                OriginalContactInfoId = contactInfo.OriginalContactInfoId,
                Regulation = MapRegulation(contactInfo.Regulation),
                Remark = MapRemark(contactInfo.Remark),
                Value = contactInfo.Value
            };
        }

        /// <summary>
        /// Maps the  <see cref="p_Identification" /> poco to
        /// <see cref="Identification" /> entity.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <returns><see cref="Identification" /> entity.</returns>
        private Identification MapIdentification(p_Identification identification)
        {
            return new Identification
            {
                DocumentNumber = identification.DocumentNumber,
                EntityId = identification.EntityId,
                IdentificationId = identification.IdentificationId,
                IdentificationType = MapIdentificationType(identification.EnumIdentificationType),
                IssueCity = identification.IssueCity,
                IssueCountry = MapCountry(identification.EnumCountry),
                IssueDate = identification.IssueDate,
                ExpiryDate = identification.ExpiryDate,
                OriginalIdentificationId = identification.OriginalIdentificationId,
                Regulation = MapRegulation(identification.Regulation),
                Remark = MapRemark(identification.Remark)
            };
        }

        ///// <summary>
        ///// Maps the  <see cref="p_ListArchive" /> poco to
        ///// <see cref="ListArchive" /> entity.
        ///// </summary>
        ///// <param name="listArchive">The list archive.</param>
        ///// <returns><see cref="ListArchive" /> entity.</returns>
        //private ListArchive MapListArchive(p_ListArchive listArchive)
        //{
        //    return (listArchive == null)
        //           ? null
        //           : new ListArchive
        //             {
        //                 Date = listArchive.Date,
        //                 File = listArchive.File,
        //                 ListArchiveId = listArchive.ListArchiveId
        //             };
        //}

        /// <summary>
        /// Maps the  <see cref="p_NameAlias" /> poco to
        /// <see cref="NameAlias" /> entity.
        /// </summary>
        /// <param name="nameAlias">The name alias.</param>
        /// <returns><see cref="NameAlias" /> entity.</returns>
        private NameAlias MapNameAlias(p_NameAlias nameAlias)
        {
            return new NameAlias
            {
                NameAliasId = nameAlias.NameAliasId,
                OriginalNameAliasId = nameAlias.OriginalNameAliasId,
                EntityId = nameAlias.EntityId,
                Regulation = MapRegulation(nameAlias.Regulation),
                FirstName = nameAlias.FirstName,
                Function = nameAlias.Function,
                Gender = MapGender(nameAlias.EnumGender),
                Language = MapLanguage(nameAlias.EnumLanguage),
                LastName = nameAlias.LastName,
                MiddleName = nameAlias.MiddleName,
                PrefixName = nameAlias.PrefixName,
                Quality = nameAlias.Quality,
                Remark = MapRemark(nameAlias.Remark),
                Title = nameAlias.Title,
                WholeName = nameAlias.WholeName
            };
        }

        /// <summary>
        /// Maps the  <see cref="p_Entity" /> poco to
        /// <see cref="Entity" /> entity.
        /// </summary>
        /// <param name="pEntity">The p entity.</param>
        /// <returns><see cref="Entity" /> entity.</returns>
        private Entity MapEntity(p_Entity pEntity)
        {
            Entity entity = new Entity
            {
                Addresses = (!pEntity.Addresses.Any())
                    ? new List<Address>()
                    : pEntity.Addresses.Select(MapAddress).ToList(),
                Banks = (!pEntity.Banks.Any())
                    ? new List<Bank>()
                    : pEntity.Banks.Select(MapBank).ToList(),
                Births = (!pEntity.Births.Any())
                    ? new List<Birth>()
                    : pEntity.Births.Select(MapBirth).ToList(),
                Citizenships = (!pEntity.Citizenships.Any())
                    ? new List<Citizenship>()
                    : pEntity.Citizenships.Select(MapCitizenship).ToList(),
                ContactInfo = (!pEntity.ContactInfo.Any())
                    ? new List<ContactInfo>()
                    : pEntity.ContactInfo.Select(MapContactInfo).ToList(),
                EntityId = pEntity.EntityId,
                Identifications = (!pEntity.Identifications.Any())
                    ? new List<Identification>()
                    : pEntity.Identifications.Select(i => MapIdentification(i))
                        .ToList(),
                ListArchiveId = pEntity.ListArchiveId,
                ListType = MapListType(pEntity.EnumListType),
                NameAliases = (!pEntity.NameAliases.Any())
                    ? new List<NameAlias>()
                    : pEntity.NameAliases.Select(n => MapNameAlias(n)).ToList(),
                OriginalEntityId = pEntity.OriginalEntityId
            };


            if (pEntity.Regulation != null)
            {
                entity.Regulation = MapRegulation(pEntity.Regulation);
            }
            else
            {
                entity.Regulation = new Regulation { RegulationId = pEntity.RegulationId };
            }

            entity.Remark = MapRemark(pEntity.Remark);

            if (pEntity.EnumStatus != null)
            {
                entity.Status = MapStatus(pEntity.EnumStatus);
            }
            else
            {
                entity.Status = new Status { StatusId = pEntity.StatusId };
            }

            if (pEntity.EnumSubjectType != null)
            {
                entity.SubjectType = MapSubjectType(pEntity.EnumSubjectType);
            }
            else
            {
                entity.SubjectType = new SubjectType { SubjectTypeId = pEntity.SubjectTypeId };
            }

            return entity;
        }

        #endregion

        #region [Private Methods]

        /// <summary>
        /// Deletes an entity and all related data in same time from database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        private void RemoveEntity(p_Entity entity)
        {
            List<p_Remark> remarks = new List<p_Remark>();
            remarks.AddRange(entity.Addresses.Select(a => a.Remark));
            remarks.AddRange(entity.Banks.Select(b => b.Remark));
            remarks.AddRange(entity.Births.Select(b => b.Remark));
            remarks.AddRange(entity.Citizenships.Select(c => c.Remark));
            remarks.AddRange(entity.ContactInfo.Select(c => c.Remark));
            remarks.AddRange(entity.Identifications.Select(i => i.Remark));
            remarks.AddRange(entity.NameAliases.Select(n => n.Remark));
            remarks.Add(entity.Remark);

            if (!entity.Regulation.Entities.Any(e => e.EntityId != entity.EntityId) &&
                !entity.Regulation.Addresses.Except(entity.Addresses).Any() &&
                !entity.Regulation.Births.Except(entity.Births).Any() &&
                !entity.Regulation.Citizenships.Except(entity.Citizenships).Any() &&
                !entity.Regulation.ContactInfo.Except(entity.ContactInfo).Any() &&
                !entity.Regulation.Identifications.Except(entity.Identifications).Any() &&
                !entity.Regulation.NameAliases.Except(entity.NameAliases).Any())
            {
                remarks.Add(entity.Regulation.Remark);
                _regulationRepo.Delete(entity.RegulationId);
            }

            _addressRepo.Delete(entity.Addresses);
            _bankRepo.Delete(entity.Banks);
            _birthRepo.Delete(entity.Births);
            _citizenshipRepo.Delete(entity.Citizenships);
            _contactInfoRepo.Delete(entity.ContactInfo);
            _identificationRepo.Delete(entity.Identifications);
            _nameAliasRepo.Delete(entity.NameAliases);
            _entityRepo.Delete(entity.EntityId);
            _remarkRepo.Delete(remarks.Where(r => r != null));
        }

        /// <summary>
        /// Get the query with condition of the searching.
        /// </summary>
        /// <param name="searchEntity">The search entity.</param>
        /// <param name="page">The page as p_Entity type.</param>
        /// <returns>IQueryable&lt;p_Entity&gt;.</returns>
        /// <exception cref="System.InvalidOperationException">Must has at least 'Limit' or 'Offset'</exception>
        private IQueryable<p_Entity> EntitiesGetCondition(SearchEntity searchEntity, Page<p_Entity> page = null)
        {
            // Initial query with ListTypeId and lastName
            IQueryable<p_Entity> query = _entityRepo.GetQueryable(l => l.ListTypeId == searchEntity.SanctionListType.ListTypeId);

            // If has LastName then add where clause
            if (!String.IsNullOrEmpty(searchEntity.LastName))
            {
                var lastNames = searchEntity.LastName.Split(' ');
                query = query.Where(x => x.NameAliases.Any(a => lastNames.Any(b => a.LastName.StartsWith(b))));
            }

            // If has FirstName then add where clause
            if (!String.IsNullOrEmpty(searchEntity.FirstName))
            {
                var firstNames = searchEntity.FirstName.Split(' ');
                query = query.Where(x =>
                    x.NameAliases.Any(na => na.WholeName.StartsWith(searchEntity.FirstName))
                    || x.NameAliases.Any(a => firstNames.Any(b => a.FirstName.StartsWith(b))));
            }

            //If has BirthDate then add where cause
            if (searchEntity.BirthDate.HasValue)
            {
                //Settings reduce and add Up of datetime (+- 3 year)
                DateTime dateTimeStart = searchEntity.BirthDate.Value.AddYears(-3);
                int startYear = dateTimeStart.Year;
                //int startMonth = dateTimeStart.Month;
                //int startDay = dateTimeStart.Day;
                DateTime dateTimeEnd = searchEntity.BirthDate.Value.AddYears(+3);
                int endYear = dateTimeEnd.Year;

                //Initial query with Settings data year,month,day are not be empty
                IQueryable<p_Birth> birthQueryable = _birthRepo.GetQueryable(x => x.EntityId > 0
                                                                               && x.Year.Value > 0
                                                                               && x.Month.Value > 0
                                                                               && x.Day.Value > 0
                                                                             );

                // Validate period date for check next step
                var birth = birthQueryable.Where(x =>
                     (DbFunctions.CreateDateTime(x.Year, x.Month, x.Day, 0, 0, 0) >= dateTimeStart)
                    && (DbFunctions.CreateDateTime(x.Year, x.Month, x.Day, 0, 0, 0) <= dateTimeEnd));

                //Final query 
                query = query.Where(x => !x.Births.Any()
                    || x.Births.Any(bi1 => birth.Contains(bi1))
                    || x.Births.Any(b => (b.Year >= startYear && b.Year <= endYear && (b.Month == null || b.Day == null)) || b.Year == null));
            }

            if (page != null)
            {
                if (!page.Limit.HasValue && !page.Offset.HasValue)
                {
                    throw new InvalidOperationException("Must has at least 'Limit' or 'Offset'");
                }

                query = page.OrderBy(query);

                if (page.Offset.HasValue)
                {
                    query = query.Skip(page.Offset.Value);
                }

                if (page.Limit.HasValue)
                {
                    query = query.Take(page.Limit.Value);
                }
            }

            return query;
        }

        #endregion
    }

    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}