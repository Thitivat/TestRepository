using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BND.Websites.BackOffice.SanctionListsManagement.Entities;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Controllers.Api.SanctionLists
{
    /// <summary>
    /// API actions on Entities.
    /// </summary>
    public class EntitiesController : SanctionListsBaseController
    {
        /// <summary>
        /// Adds new Entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listTypeId">Id of the list that this object belongs to.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/SanctionLists/{listTypeId}/Entities")]
        public Entity Post([FromBody]Entity entity, int listTypeId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            Entity newEntity = _sanctionListsBll.EntitiesAdd(entity);

            // Log
            LogActivity(entity.ListType.ListTypeId, "Added new entity. Id: " + newEntity.EntityId);

            // Add info in Update
            // TODO discuss if we need to update it while adding entity or while adding/editing any of its sub entities
            _sanctionListsBll.UpdatesSet(new Update
            {
                ListType = new ListType { ListTypeId = listTypeId},
                Username = User.Identity.Name,
                UpdatedDate = DateTime.UtcNow,
                PublicDate = DateTime.UtcNow
            });

            return newEntity;
        }

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity">Updated entity.</param>
        /// <param name="listTypeId">Id of the list that this entity belongs to.</param>
        /// <param name="entityId">Id of the entity.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}")]
        public Entity UpdateEntity([FromBody]Entity entity, int listTypeId, int entityId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            // Check if parameters from URL match parameters inside the entity
            if (entity.EntityId != entityId || entity.ListType.ListTypeId != listTypeId)
            {
                // If not, throw exception
                throw new HttpException(400, "Wrong parameters");
            }

            // Get entity from DB
            Entity entityUpdate = _sanctionListsBll.EntitiesGetById(entityId);

            // Set new values in the entity
            entityUpdate.SubjectType.SubjectTypeId = entity.SubjectType.SubjectTypeId;
            entityUpdate.Status.StatusId = entity.Status.StatusId;
            entityUpdate.Regulation.RegulationId = entity.Regulation.RegulationId;
            entityUpdate.Remark = entity.Remark;

            // Update in DB
            Entity updatedEntity = _sanctionListsBll.EntitiesUpdate(entityUpdate);

            // Log
            LogActivity(listTypeId, "Updated entity. Id: " + updatedEntity.EntityId);

            return updatedEntity;
        }

        /// <summary>
        /// Returns list of entities.
        /// </summary>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/SanctionLists/{listTypeId}/Entities")]
        public object Get(int? listTypeId = null, int? offset=null, int? limit=null)
        {
            Entity[] entities = _sanctionListsBll.EntitiesGet(listTypeId, offset, limit); //listId
            int entitiesCount = _sanctionListsBll.EntitiesCount(listTypeId); //listId

            return new { Entities = entities, EntitiesCount = entitiesCount };
        }

        /// <summary>
        /// Adds new NameAlias.
        /// </summary>
        /// <param name="nameAlias"></param>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/NameAliases")]
        public NameAlias AddNameAlias([FromBody]NameAlias nameAlias, int listTypeId, int entityId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            nameAlias.EntityId = entityId;

            NameAlias newNameAlias = _sanctionListsBll.NameAliasesAdd(nameAlias);

            // Log
            LogActivity(listTypeId, "Added new name alias. Id: " + newNameAlias.NameAliasId);

            return newNameAlias;
        }

        /// <summary>
        /// Edit NameAlias.
        /// </summary>
        /// <param name="nameAlias"></param>
        /// <param name="listTypeId">Id of the list that this object belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <param name="nameAliasId">Id of the nameAlias to edit.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/NameAliases/{nameAliasId}")]
        public NameAlias UpdateNameAlias([FromBody]NameAlias nameAlias, int listTypeId, int entityId, int nameAliasId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            // Check if parameters from URL match parameters inside the entity
            if (nameAlias.EntityId != entityId || nameAlias.NameAliasId != nameAliasId)
            {
                // If not, throw exception
                throw new HttpException(400, "Wrong parameters");
            }

            // Get entity from DB
            NameAlias nameAliasUpdate = _sanctionListsBll.NameAliasesGetById(nameAliasId);

            // Set new values in the entity
            nameAliasUpdate.FirstName = nameAlias.FirstName;
            nameAliasUpdate.Function = nameAlias.Function;
            nameAliasUpdate.Gender = nameAlias.Gender;
            nameAliasUpdate.Language = nameAlias.Language;
            nameAliasUpdate.LastName = nameAlias.LastName;
            nameAliasUpdate.MiddleName = nameAlias.MiddleName;
            nameAliasUpdate.PrefixName = nameAlias.PrefixName;
            nameAliasUpdate.Quality = nameAlias.Quality;
            nameAliasUpdate.Title = nameAlias.Title;
            nameAliasUpdate.WholeName = nameAlias.WholeName;
            nameAliasUpdate.Regulation.RegulationId = nameAlias.Regulation.RegulationId;
            nameAliasUpdate.Remark = nameAlias.Remark;

            // Update in DB
            NameAlias updatedNameAlias = _sanctionListsBll.NameAliasesUpdate(nameAliasUpdate);

            // Log
            LogActivity(listTypeId, "Updated nameAlias. Id: " + updatedNameAlias.NameAliasId);

            return updatedNameAlias;
        }

        /// <summary>
        /// Deletes NameAlias.
        /// </summary>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="nameAliasId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/NameAliases/{nameAliasId}")]
        public HttpResponseMessage DeleteNameAlias(int entityId, int listTypeId, int nameAliasId)
        {
            _sanctionListsBll.NameAliasesRemove(nameAliasId);

            // Log
            LogActivity(listTypeId, "Deleted name alias. Id: " + nameAliasId);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Adds new Citizenship.
        /// </summary>
        /// <param name="citizenship"></param>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Citizenships")]
        public Citizenship AddCitizenship([FromBody]Citizenship citizenship, int listTypeId, int entityId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            citizenship.EntityId = entityId;

            Citizenship newCitizenship = _sanctionListsBll.CitizenshipsAdd(citizenship);

            // Log
            LogActivity(listTypeId, "Added new citizenship. Id: " + newCitizenship.CitizenshipId);

            return newCitizenship;
        }

        /// <summary>
        /// Edit Citizenship.
        /// </summary>
        /// <param name="citizenship">Citizenship to be updated.</param>
        /// <param name="listTypeId">Id of the list that this object belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <param name="citizenshipId">Id of the citizenship to edit.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Citizenships/{citizenshipId}")]
        public Citizenship UpdateCitizenship([FromBody]Citizenship citizenship, int listTypeId, int entityId, int citizenshipId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            // Check if parameters from URL match parameters inside the entity
            if (citizenship.EntityId != entityId || citizenship.CitizenshipId != citizenshipId)
            {
                // If not, throw exception
                throw new HttpException(400, "Wrong parameters");
            }

            // Get entity from DB
            Citizenship citizenshipUpdate = _sanctionListsBll.CitizenshipsGetById(citizenshipId);

            // Set new values in the entity
            citizenshipUpdate.Country = citizenship.Country;
            citizenshipUpdate.Regulation.RegulationId = citizenship.Regulation.RegulationId;
            citizenshipUpdate.Remark = citizenship.Remark;

            // Update in DB
            Citizenship updatedCitizenship = _sanctionListsBll.CitizenshipsUpdate(citizenshipUpdate);

            // Log
            LogActivity(listTypeId, "Updated citizenship. Id: " + updatedCitizenship.CitizenshipId);

            return updatedCitizenship;
        }

        /// <summary>
        /// Deletes Citizenship.
        /// </summary>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="citizenshipId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Citizenships/{citizenshipId}")]
        public HttpResponseMessage DeleteCitizenship(int entityId, int listTypeId, int citizenshipId)
        {
            _sanctionListsBll.CitizenshipsRemove(citizenshipId);

            // Log
            LogActivity(listTypeId, "Deleted citizenship. Id: " + citizenshipId);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Adds new Address.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Addresses")]
        public Address AddAddress([FromBody]Address address, int listTypeId, int entityId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            address.EntityId = entityId;

            Address newAddress = _sanctionListsBll.AddressesAdd(address);

            // Log
            LogActivity(listTypeId, "Added new address. Address Id: " + newAddress.AddressId);

            return newAddress;
        }

        /// <summary>
        /// Edit Address.
        /// </summary>
        /// <param name="address">Address to be updated.</param>
        /// <param name="listTypeId">Id of the list that this object belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <param name="addressId">Id of the address to edit.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Addresses/{addressId}")]
        public Address UpdateAddress([FromBody]Address address, int listTypeId, int entityId, int addressId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            // Check if parameters from URL match parameters inside the entity
            if (address.EntityId != entityId || address.AddressId != addressId)
            {
                // If not, throw exception
                throw new HttpException(400, "Wrong parameters");
            }

            // Get entity from DB
            Address addressUpdate = _sanctionListsBll.AddressesGetById(addressId);

            // Set new values in the entity
            addressUpdate.City = address.City;
            addressUpdate.Country = address.Country;
            addressUpdate.Number = address.Number;
            addressUpdate.Other = address.Other;
            addressUpdate.Street = address.Street;
            addressUpdate.Zipcode = address.Zipcode;


            addressUpdate.Regulation.RegulationId = address.Regulation.RegulationId;
            addressUpdate.Remark = address.Remark;

            // Update in DB
            Address updatedAddress = _sanctionListsBll.AddressesUpdate(addressUpdate);

            // Log
            LogActivity(listTypeId, "Updated address. Id: " + updatedAddress.AddressId);

            return updatedAddress;
        }

        /// <summary>
        /// Removes Address.
        /// </summary>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Addresses/{addressId}")]
        public HttpResponseMessage DeleteAddress(int listTypeId, int entityId, int addressId)
        {
            _sanctionListsBll.AddressesRemove(addressId);

            // Log
            LogActivity(listTypeId, "Deleted address. Id: " + addressId);


            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Adds new Identification.
        /// </summary>
        /// <param name="identification"></param>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Identifications")]
        public Identification AddIdentification([FromBody]Identification identification, int listTypeId, int entityId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            identification.EntityId = entityId;

            Identification newIdentification = _sanctionListsBll.IdentificationsAdd(identification);

            // Log
            LogActivity(listTypeId, "Added new identification. Id: " + newIdentification.IdentificationId);


            return newIdentification;
        }

        /// <summary>
        /// Edit Identification.
        /// </summary>
        /// <param name="address">Identification to be updated.</param>
        /// <param name="listTypeId">Id of the list that this object belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <param name="identificationId">Id of the identification to edit.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Identifications/{identificationId}")]
        public Identification UpdateIdentification([FromBody]Identification identification, int listTypeId, int entityId, int identificationId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            // Check if parameters from URL match parameters inside the entity
            if (identification.EntityId != entityId || identification.IdentificationId != identificationId)
            {
                // If not, throw exception
                throw new HttpException(400, "Wrong parameters");
            }

            // Get entity from DB
            Identification identificationUpdate = _sanctionListsBll.IdentificationsGetById(identificationId);

            // Set new values in the entity
            identificationUpdate.DocumentNumber = identification.DocumentNumber;
            identificationUpdate.ExpiryDate = identification.ExpiryDate;
            identificationUpdate.IdentificationType = identification.IdentificationType;
            identificationUpdate.IssueCity = identification.IssueCity;
            identificationUpdate.IssueCountry = identification.IssueCountry;
            identificationUpdate.IssueDate = identification.IssueDate;

            identificationUpdate.Regulation.RegulationId = identification.Regulation.RegulationId;
            identificationUpdate.Remark = identification.Remark;

            // Update in DB
            Identification updatedIdentification = _sanctionListsBll.IdentificationsUpdate(identificationUpdate);

            // Log
            LogActivity(listTypeId, "Updated identification. Id: " + updatedIdentification);

            return updatedIdentification;
        }

        /// <summary>
        /// Removes Identification.
        /// </summary>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <param name="identificationId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Identifications/{identificationId}")]
        public HttpResponseMessage DeleteIdentification(int listTypeId, int entityId, int identificationId)
        {
            _sanctionListsBll.IdentificationsRemove(identificationId);

            // Log
            LogActivity(listTypeId, "Deleted identification. Id: " + identificationId);


            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Adds new ContactInfo.
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Contactinfos")]
        public ContactInfo AddContactInfo([FromBody]ContactInfo contactInfo, int listTypeId, int entityId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            contactInfo.EntityId = entityId;

            ContactInfo newContactInfo = _sanctionListsBll.ContactInfoAdd(contactInfo);

            // Log
            LogActivity(listTypeId, "Added new contactInfo. Id: " + newContactInfo.ContactInfoId);


            return newContactInfo;
        }

        /// <summary>
        /// Update ContactInfo.
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <param name="listTypeId">Id of the list that this object belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <param name="contactInfoId">Id of the contactInfo to edit.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Contactinfos/{contactInfoId}")]
        public ContactInfo UpdateContactInfo([FromBody]ContactInfo contactInfo, int listTypeId, int entityId, int contactInfoId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            // Check if parameters from URL match parameters inside the entity
            if (contactInfo.EntityId != entityId || contactInfo.ContactInfoId != contactInfoId)
            {
                // If not, throw exception
                throw new HttpException(400, "Wrong parameters");
            }

            // Get entity from DB
            ContactInfo ContactInfoUpdate = _sanctionListsBll.ContactInfoGetById(contactInfoId);

            // Set new values in the entity
            ContactInfoUpdate.ContactInfoType.ContactInfoTypeId = contactInfo.ContactInfoType.ContactInfoTypeId;
            ContactInfoUpdate.Value = contactInfo.Value;
            ContactInfoUpdate.Regulation.RegulationId = contactInfo.Regulation.RegulationId;
            ContactInfoUpdate.Remark = contactInfo.Remark;

            // Update in DB
            ContactInfo updatedContactInfo = _sanctionListsBll.ContactInfoUpdate(ContactInfoUpdate);

            // Log
            LogActivity(listTypeId, "Updated contactInfo. Id: " + updatedContactInfo.ContactInfoId);

            return updatedContactInfo;
        }

        /// <summary>
        /// Removes ContactInfo.
        /// </summary>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <param name="contactInfoId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Contactinfos/{contactInfoId}")]
        public HttpResponseMessage DeleteContactInfo(int listTypeId, int entityId, int contactInfoId)
        {
            _sanctionListsBll.ContactInfoRemove(contactInfoId);

            // Log
            LogActivity(listTypeId, "Deleted Contactinfo. Id: " + contactInfoId);


            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Adds new Birth.
        /// </summary>
        /// <param name="birth"></param>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Births")]
        public Birth AddBirth([FromBody]Birth birth, int listTypeId, int entityId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            birth.EntityId = entityId;

            Birth newBirth = _sanctionListsBll.BirthsAdd(birth);

            // Log
            LogActivity(listTypeId, "Added new bith. Id: " + newBirth.BirthId);

            return newBirth;
        }

        /// <summary>
        /// Edit Birth.
        /// </summary>
        /// <param name="birth">Birth to be updated.</param>
        /// <param name="listTypeId">Id of the list that this object belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <param name="birthId">Id of the birth to edit.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Births/{birthId}")]
        public Birth UpdateBirth([FromBody]Birth birth, int listTypeId, int entityId, int birthId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            // Check if parameters from URL match parameters inside the entity
            if (birth.EntityId != entityId || birth.BirthId != birthId)
            {
                // If not, throw exception
                throw new HttpException(400, "Wrong parameters");
            }

            // Get entity from DB
            Birth birthUpdate = _sanctionListsBll.BirthsGetById(birthId);

            // Set new values in the entity
            birthUpdate.Country = birth.Country;
            birthUpdate.Day = birth.Day;
            birthUpdate.Month = birth.Month;
            birthUpdate.Place = birth.Place;
            birthUpdate.Year = birth.Year;

            birthUpdate.Regulation.RegulationId = birth.Regulation.RegulationId;
            birthUpdate.Remark = birth.Remark;

            // Update in DB
            Birth updatedBirth = _sanctionListsBll.BirthsUpdate(birthUpdate);

            // Log
            LogActivity(listTypeId, "Updated birth. Id: " + updatedBirth);

            return updatedBirth;
        }

        /// <summary>
        /// Removes Birth.
        /// </summary>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <param name="birthId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Births/{birthId}")]
        public HttpResponseMessage DeleteBirth(int listTypeId, int entityId, int birthId)
        {
            _sanctionListsBll.BirthsRemove(birthId);

            // Log
            LogActivity(listTypeId, "Deleted birth. Id: " + birthId);


            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Add new Bank.
        /// </summary>
        /// <param name="bank"></param>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Banks")]
        public Bank AddBank([FromBody]Bank bank, int listTypeId, int entityId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            bank.EntityId = entityId;

            Bank newBank = _sanctionListsBll.BanksAdd(bank);

            // Log
            LogActivity(listTypeId, "Added new bank. Id: " + newBank.BankId);

            return newBank;
        }

        /// <summary>
        /// Edit Bank.
        /// </summary>
        /// <param name="bank">Bank to be updated.</param>
        /// <param name="listTypeId">Id of the list that this object belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <param name="bankId">Id of the bank to edit.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Banks/{bankId}")]
        public Bank UpdateBank([FromBody]Bank bank, int listTypeId, int entityId, int bankId)
        {
            // Check if allowed
            isManualUpdateAllowed(listTypeId);

            // Check if parameters from URL match parameters inside the entity
            if (bank.EntityId != entityId || bank.BankId != bankId)
            {
                // If not, throw exception
                throw new HttpException(400, "Wrong parameters");
            }

            // Get entity from DB
            Bank bankUpdate = _sanctionListsBll.BanksGetById(bankId);

            // Set new values in the entity
            bankUpdate.Country = bank.Country;
            bankUpdate.AccountHolderName = bank.AccountHolderName;
            bankUpdate.AccountNumber = bank.AccountNumber;
            bankUpdate.BankName = bank.BankName;
            bankUpdate.Country = bank.Country;
            bankUpdate.Iban = bank.Iban;
            bankUpdate.Swift = bank.Swift;

            bankUpdate.Remark = bank.Remark;

            // Update in DB
            Bank updatedBank = _sanctionListsBll.BanksUpdate(bankUpdate);

            // Log
            LogActivity(listTypeId, "Updated bank. Id: " + updatedBank);

            return updatedBank;
        }

        /// <summary>
        /// Removes Bank.
        /// </summary>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="entityId">Id of the entity that objects belongs to.</param>
        /// <param name="bankId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/SanctionLists/{listTypeId}/Entities/{entityId}/Banks/{bankId}")]
        public HttpResponseMessage DeleteBank(int listTypeId, int entityId, int bankId)
        {
            _sanctionListsBll.BanksRemove(bankId);

            // Log
            LogActivity(listTypeId, "Deleted bank. Id: " + bankId);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private void isManualUpdateAllowed(int listTypeId)
        {
            Setting setting = _sanctionListsBll.SettingsGet(listTypeId, "ManualUpdate");
            int manualUpdate = setting == null ? 0 : Convert.ToInt16(setting.Value);

            if (manualUpdate == 0)
            {
                throw new HttpException(404, "");
            }

        }
    }
}
