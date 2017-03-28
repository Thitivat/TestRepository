using BND.Websites.BackOffice.SanctionListsManagement.Entities;

namespace BND.Websites.BackOffice.SanctionListsManagement.Business.Interfaces
{
    /// <summary>
    /// Interface ISanctionListsBll
    /// </summary>
    public interface ISanctionListsBll
    {
        #region [Enums]

        /// <summary>
        /// Gets list of ContractInfoType object.
        /// </summary>
        /// <returns>ContactInfoType[].</returns>
        ContactInfoType[] ContactInfoTypesGet();

        /// <summary>
        /// Gets list of Country object.
        /// </summary>
        /// <returns>Country[].</returns>
        Country[] CountriesGet();

        /// <summary>
        /// Gets list of Gender object.
        /// </summary>
        /// <returns>Gender[].</returns>
        Gender[] GendersGet();

        /// <summary>
        /// Gets list of IdentificationType object.
        /// </summary>
        /// <returns>IdentificationType[].</returns>
        IdentificationType[] IdentificationTypesGet();

        /// <summary>
        /// Gets list of Language object.
        /// </summary>
        /// <returns>Language[].</returns>
        Language[] LanguagesGet();

        /// <summary>
        /// Gets list of ListType object.
        /// </summary>
        /// <returns>ListType[].</returns>
        ListType[] ListTypesGet();

        /// <summary>
        /// Gets information about ListType object by passing the ListTypeId.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <returns>ListType.</returns>
        ListType ListTypesGetById(int listTypeId);

        /// <summary>
        /// Gets list of Status object.
        /// </summary>
        /// <returns>Status[].</returns>
        Status[] StatusesGet();

        /// <summary>
        /// Gets list of SubjectType object.
        /// </summary>
        /// <returns>SubjectType[].</returns>
        SubjectType[] SubjectTypesGet();

        #endregion


        #region [Update]

        /// <summary>
        /// Gets information about Update object by passing the ListTypeId.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <returns>Gets <see cref="Update"/>.</returns>
        Update UpdatesGet(int listTypeId);

        /// <summary>
        /// Updates information about Update object, if object doesn't exist create a new one.
        /// </summary>
        /// <param name="update">The update object.</param>
        /// <returns>Sets <see cref="Update"/>.</returns>
        Update UpdatesSet(Update update);

        #endregion


        #region [Log]

        /// <summary>
        /// Gets list of Log object by passing listTypeId if null will be return all of Log, and can get data by
        /// specific the offset of page and limitation to show in the page
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <param name="offset">The offset or page number.</param>
        /// <param name="limit">The limitation of number of record per page.</param>
        /// <returns>Gets array of  <see cref="Log"/>.</returns>
        Log[] LogsGet(int? listTypeId = null, int? offset = null, int? limit = null);

        /// <summary>
        /// Gets record count of Los object.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <returns>System.Int32.</returns>
        int LogsCount(int? listTypeId);

        /// <summary>
        /// Adds Log object.
        /// </summary>
        /// <param name="log">The log object.</param>
        /// <returns>Added <see cref="Log"/>.</returns>
        Log LogsAdd(Log log);

        #endregion


        #region [Setting]

        /// <summary>
        /// Get information about Setting object by passing listTypeId and key name of Setting.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <param name="key">The key name of setting.</param>
        /// <returns>Setting.</returns>
        Setting SettingsGet(int listTypeId, string key);

        #endregion


        #region [Regulation]

        /// <summary>
        /// Gets list of Regulation by ListTypeId.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <param name="offset">The offset or page number.</param>
        /// <param name="limit">The limitation of number of record per page.</param>
        /// <returns>Regulation[].</returns>
        Regulation[] RegulationsGet(int listTypeId, int? offset = null, int? limit = null);

        /// <summary>
        /// Gets record count of regulations object.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <returns>System.Int32.</returns>
        int RegulationsCount(int? listTypeId);

        /// <summary>
        /// Gets information about Regulation by PublicationTitle.
        /// </summary>
        /// <param name="publicationTitle">The publication title.</param>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <returns>Regulation.</returns>
        Regulation RegulationsGetByPublicationTitle(string publicationTitle, int listTypeId);

        /// <summary>
        /// Adds Regulation object.
        /// </summary>
        /// <param name="regulation">The regulation object.</param>
        /// <returns>Regulation.</returns>
        Regulation RegulationsAdd(Regulation regulation);

        #endregion


        #region [NameAlias]

        /// <summary>
        /// Returns <see cref="NameAlias"/> objects by entity id.
        /// </summary>
        /// <param name="entityId">Id if the <see cref="Entity"/> that is related to nameAliases.</param>
        /// <returns>Array of<see cref="NameAlias"/>.</returns>
        NameAlias[] NameAliasesGet(int entityId);

        /// <summary>
        /// Adds new NameAlias.
        /// </summary>
        /// <param name="nameAlias"><see cref="NameAlias"/> to add.</param>
        /// <returns>Added <see cref="NameAlias"/>.</returns>
        NameAlias NameAliasesAdd(NameAlias nameAlias);

        /// <summary>
        /// "Updates NameAlias entity.
        /// </summary>
        /// <param name="nameAlias"><see cref="NameAlias"/> to update.</param>
        /// <returns>Updated <see cref="NameAlias"/>.</returns>
        NameAlias NameAliasesUpdate(NameAlias nameAlias);

        /// <summary>
        /// Removes NameAlias.
        /// </summary>
        /// <param name="nameAliasId">Id of <see cref="BND.Websites.BackOffice.SanctionListsManagement.Entities.NameAlias" /> to be removed.</param>
        void NameAliasesRemove(int nameAliasId);

        /// <summary>
        /// Returns <see cref="NameAlias" /> object by id.
        /// </summary>
        /// <param name="nameAliasId">The name alias identifier.</param>
        /// <returns>Get <see cref="NameAlias" />.</returns>
        NameAlias NameAliasesGetById(int nameAliasId);

        #endregion


        #region [Address]

        /// <summary>
        /// Returns list of <see cref="Address"/>es by <see cref="Entity"/> id.
        /// </summary>
        /// <param name="entityId">Id of the <see cref="Entity"/> that is related to addresses</param>
        /// <returns>Array of <see cref="Address"/>.</returns>
        Address[] AddressesGet(int entityId);

        /// <summary>
        /// Adds new <see cref="Address"/>.
        /// </summary>
        /// <param name="address"><see cref="Address"/> to be added.</param>
        /// <returns>Added <see cref="Address"/>.</returns>
        Address AddressesAdd(Address address);

        /// <summary>
        /// Updates <see cref="Address"/>.
        /// </summary>
        /// <param name="address"><see cref="Address"/> to be updated.</param>
        /// <returns>Updated <see cref="Address"/>.</returns>
        Address AddressesUpdate(Address address);

        /// <summary>
        /// Removes <see cref="Address"/>.
        /// </summary>
        /// <param name="addressId">Id of the <see cref="Address"/> to be removed.</param>
        void AddressesRemove(int addressId);

        /// <summary>
        /// Gets address information by identifier.
        /// </summary>
        /// <param name="addressId">The Addresses identifier.</param>
        /// <returns>Added <see cref="Address"/>.</returns>
        Address AddressesGetById(int addressId);

        #endregion


        #region [Birth]

        /// <summary>
        /// Gets array of <see cref="Birth"/>,
        /// </summary>
        /// <param name="entityId">Id if the <see cref="Entity"/> that is related to Births.</param>
        /// <returns>Array of <see cref="Birth"/>.</returns>
        Birth[] BirthsGet(int entityId);

        /// <summary>
        /// Adds new <see cref="Birth"/>.
        /// </summary>
        /// <param name="birth"><see cref="Birth"/> object to be added.</param>
        /// <returns>Added <see cref="Birth"/>.</returns>
        Birth BirthsAdd(Birth birth);

        /// <summary>
        /// Birthses the update.
        /// </summary>
        /// <param name="birth">The birth.</param>
        /// <returns>Birth.</returns>
        Birth BirthsUpdate(Birth birth);

        /// <summary>
        /// Removes <see cref="Birth"/>.
        /// </summary>
        /// <param name="birthId">Id of <see cref="Birth"/> to be removed.</param>
        void BirthsRemove(int birthId);

        /// <summary>
        /// Gets birth information by identifier.
        /// </summary>
        /// <param name="birthId">The Birth identifier.</param>
        /// <returns>Get <see cref="Birth"/>.</returns>
        Birth BirthsGetById(int birthId);

        #endregion


        #region [Bank]

        /// <summary>
        /// Gets array of <see cref="Bank"/>.
        /// </summary>
        /// <param name="entityId">Id if the <see cref="Entity"/> that is related to Banks.</param>
        /// <returns>Array of <see cref="Bank"/>.</returns>
        Bank[] BanksGet(int entityId);

        /// <summary>
        /// Adds new <see cref="Bank"/>.
        /// </summary>
        /// <param name="bank"><see cref="Bank"/> to be added.</param>
        /// <returns>Added <see cref="Bank"/>.</returns>
        Bank BanksAdd(Bank bank);

        /// <summary>
        /// Updates <see cref="Bank"/>.
        /// </summary>
        /// <param name="bank"><see cref="Bank"/> to be updated</param>
        /// <returns>Updated <see cref="Bank"/>.</returns>
        Bank BanksUpdate(Bank bank);

        /// <summary>
        /// Removes <see cref="Bank"/>.
        /// </summary>
        /// <param name="bankId">Id of <see cref="Bank"/> to be removed.</param>
        void BanksRemove(int bankId);

        /// <summary>
        /// Gets bank information by identifier.
        /// </summary>
        /// <param name="bankId">The Bank identifier.</param>
        /// <returns>Get <see cref="Bank"/>.</returns>
        Bank BanksGetById(int bankId);

        #endregion


        #region [Citizenship]

        /// <summary>
        /// Gets array of <see cref="Citizenship"/>.
        /// </summary>
        /// <param name="entityId">Id if the <see cref="Entity"/> that is related to Citizenships.</param>
        /// <returns>Array of <see cref="Citizenship"/>.</returns>
        Citizenship[] CitizenshipsGet(int entityId);

        /// <summary>
        /// Adds new <see cref="Citizenship"/>.
        /// </summary>
        /// <param name="citizenship"><see cref="Citizenship"/> to add.</param>
        /// <returns>Added <see cref="Citizenship"/>.</returns>
        Citizenship CitizenshipsAdd(Citizenship citizenship);

        /// <summary>
        /// Updates <see cref="Citizenship"/>.
        /// </summary>
        /// <param name="citizenship"><see cref="Citizenship"/> to be updated.</param>
        /// <returns>Updated <see cref="Citizenship"/>.</returns>
        Citizenship CitizenshipsUpdate(Citizenship citizenship);

        /// <summary>
        /// Removes <see cref="Citizenship"/>.
        /// </summary>
        /// <param name="citizenshipId">ID of <see cref="Citizenship"/> to be removed.</param>
        void CitizenshipsRemove(int citizenshipId);

        /// <summary>
        /// Gets citizenship information by identifier.
        /// </summary>
        /// <param name="citizenshipId">The Citizenship identifier.</param>
        /// <returns>Get <see cref="Citizenship"/>.</returns>
        Citizenship CitizenshipsGetById(int citizenshipId);

        #endregion


        #region [ContactInfo]

        /// <summary>
        /// Gets the collection of contact information by entity identifier.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns>Collection of contact information.</returns>
        ContactInfo[] ContactInfoGet(int entityId);
        /// <summary>
        /// Inserts a contact information data to database.
        /// </summary>
        /// <param name="contactInfo">A contact information to insert.</param>
        /// <returns>Added <see cref="ContactInfo"/>.</returns>
        ContactInfo ContactInfoAdd(ContactInfo contactInfo);
        /// <summary>
        /// Updates a contact information in database.
        /// </summary>
        /// <param name="contactInfo">A contact information to update.</param>
        /// <returns>Updated <see cref="ContactInfo"/>.</returns>
        ContactInfo ContactInfoUpdate(ContactInfo contactInfo);
        /// <summary>
        /// Deletes a contact information from database by contact information identifier.
        /// </summary>
        /// <param name="contactInfoId">The contact information identifier.</param>
        void ContactInfoRemove(int contactInfoId);
        /// <summary>
        /// Gets contact information by identifier.
        /// </summary>
        /// <param name="contactInfoId">The ContactInfo identifier.</param>
        /// <returns>Get <see cref="ContactInfo"/>.</returns>
        ContactInfo ContactInfoGetById(int contactInfoId);

        #endregion


        #region [Identification]

        /// <summary>
        /// Gets the collection of identification by entity identifier.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns>Get array of <see cref="Identification"/>.</returns>
        Identification[] IdentificationsGet(int entityId);
        /// <summary>
        /// Inserts an identification data to database.
        /// </summary>
        /// <param name="identification">An identification to insert.</param>
        /// <returns>Get <see cref="Identification"/>.</returns>
        Identification IdentificationsAdd(Identification identification);
        /// <summary>
        /// Updates an identification data in database.
        /// </summary>
        /// <param name="identification">An identification to update.</param>
        /// <returns>Updated <see cref="Identification"/>.</returns>
        Identification IdentificationsUpdate(Identification identification);
        /// <summary>
        /// Removes an identification from database.
        /// </summary>
        /// <param name="identificationId">The identification identifier.</param>
        void IdentificationsRemove(int identificationId);

        /// <summary>
        /// Gets contact information by identifier.
        /// </summary>
        /// <param name="identificationId">The identification identifier.</param>
        /// <returns>Get <see cref="ContactInfo" />.</returns>
        Identification IdentificationsGetById(int identificationId);
        #endregion


        #region [Entity]

        /// <summary>
        /// Gets collection of entities with some limitation that you want.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <param name="offset">The starting record of entities data in database.</param>
        /// <param name="limit">The amount of entities data that you want.</param>
        /// <returns>Collection of entities.</returns>
        Entity[] EntitiesGet(int? listTypeId = null, int? offset = null, int? limit = null);
        /// <summary>
        /// Gets an entity data by entity identifier.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns>An entity.</returns>
        Entity EntitiesGetById(int entityId);
        /// <summary>
        /// Inserts an entity data to database.
        /// </summary>
        /// <param name="entity">an entity to insert.</param>
        /// <returns>The inserted entity.</returns>
        Entity EntitiesAdd(Entity entity);
        /// <summary>
        /// Changes status of entities to another status in one call.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <param name="fromStatusId">From status identifier.</param>
        /// <param name="toStatusId">To status identifier.</param>
        /// <returns>The amount of records affected.</returns>
        int EntitiesChangeStatus(int listTypeId, int fromStatusId, int toStatusId);
        /// <summary>
        /// Deletes an entity from database.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        void EntitiesRemove(int entityId);
        /// <summary>
        /// Delete the entities from database by status identitfier.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <param name="statusId">The status identifier to delete.</param>
        void EntitiesRemoveByStatus(int listTypeId, int statusId);
        /// <summary>
        /// Get amount of entities from database by list type identifier.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <returns>Amount of entities.</returns>
        int EntitiesCount(int? listTypeId);
        /// <summary>
        /// Updates entity in database.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <returns>The updated entity.</returns>
        /// <exception cref="System.ArgumentException">The entity could not be found.;contactInfo</exception>
        Entity EntitiesUpdate(Entity entity);

        /// <summary>
        /// Delete the entities from database by status identitfier.
        /// </summary>
        /// <param name="searchEntity">The search entity.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns>Entity[].</returns>
        Entity[] EntitiesGet(SearchEntity searchEntity, int? offset = null, int? limit = null);

        /// <summary>
        /// Get amount of entities from database by list type identifier.
        /// </summary>
        /// <param name="searchEntity">The search entity.</param>
        /// <returns>Amount of entities.</returns>
        int EntitiesCount(SearchEntity searchEntity);

        #endregion


        #region [ListArchive]

        /// <summary>
        /// Inserts a list archive data to database.
        /// </summary>
        /// <param name="listArchive">A list archive to insert.</param>
        /// <returns>The inserted list archive data.</returns>
        ListArchive ListArchiveAdd(ListArchive listArchive);

        #endregion


        #region [SanctionListDetail]

        /// <summary>
        /// Gets all sanction lists basic information.
        /// </summary>
        /// <returns>Collection of sanction lists basic information.</returns>
        SanctionListDetail[] SanctionListDetailGet();
        /// <summary>
        /// Sets a sanction list basic information by list type id.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <returns>A sanction list basic information.</returns>
        SanctionListDetail SanctionListDetailGet(int listTypeId);


        #endregion

    }
}
