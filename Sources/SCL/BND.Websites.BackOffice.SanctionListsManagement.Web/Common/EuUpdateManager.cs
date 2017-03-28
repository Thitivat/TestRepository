using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Xml;
using BND.Websites.BackOffice.SanctionListsManagement.Business.Implementations;
using BND.Websites.BackOffice.SanctionListsManagement.Business.Interfaces;
using BND.Websites.BackOffice.SanctionListsManagement.Business.Parsers;
using BND.Websites.BackOffice.SanctionListsManagement.Entities;
using BND.Websites.BackOffice.SanctionListsManagement.Entities.EuEntities;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Common
{
    /// <summary>
    /// The EuUpdateManager class provides the methods to update Eu sanctions list, the process for update the 
    /// sanctions list is, 
    /// step 1 : check pubDate from rss of eu and latest data in databse if newer will goto 
    /// step 2 : Parse the Global xml to EuEntity object, 
    /// step 3 : Loop EnEntity and maping to Entity object then add Entity to database with Updating status.
    /// step 4 : After add all entities change status of old entities that are Active to Removed.
    /// step 5 : Change status of all new entities from Updating to Active.
    /// step 6 : Removed all entities that have status Removed.
    /// step 7 : Update pubDate to Updates table and save global xml file to ListArchive table.
    /// step 8 : if something happen, removed all entities that have status Updating.
    /// </summary>
    public class EuUpdateManager : IDisposable
    {
        #region [Fields]

        /// <summary>
        /// The static string of double new line.
        /// </summary>
        private static string DOUBLE_NEWLINE = "\n\n";

        /// <summary>
        /// The id of Eu list type.
        /// </summary>
        private static int EU_LIST_TYPE_ID = 1;
        
        /// <summary>
        /// The id of active status.
        /// </summary>
        private static int STATUS_ACTIVE = 1;

        /// <summary>
        /// The id of removed status.
        /// </summary>
        private static int STATUS_REMOVED = 2;

        /// <summary>
        /// Id of updating status.
        /// </summary>
        private static int STATUS_UPDATING = 3;

        /// <summary>
        /// The application global variable name.
        /// </summary>
        private static string GLOBAL_STATUS_NAME = "EUUpdateStatus";

        /// <summary>
        /// The instance of Listype object for Eu.
        /// </summary>
        private ListType _euListType;

        /// <summary>
        /// The instance of Status object for updating state.
        /// </summary>
        private Status _statusUpdating;

        /// <summary>
        /// The collection of SubjectType object, etc. Person, Enterprise.
        /// </summary>
        private ICollection<SubjectType> _subjectTypes;

        /// <summary>
        /// The collection of Gender object.
        /// </summary>
        private ICollection<Gender> _genders;

        /// <summary>
        /// The collection of Language object.
        /// </summary>
        private ICollection<Language> _languases;

        /// <summary>
        /// The collection of Country object.
        /// </summary>
        private ICollection<Country> _countries;

        /// <summary>
        /// The collection of IdentificationType object, etc. PASSPORT, ID, ...
        /// </summary>
        private IdentificationType _identificationTypesOther;

        /// <summary>
        /// The collection of ContactInfoType object, etc. PHONE, FAX, ...
        /// </summary>
        private ICollection<ContactInfoType> _contactInfoTypes;

        /// <summary>
        /// The instance object of interface of SanctionListsBll class, this class provides methods for communication
        /// with database.
        /// </summary>
        private ISanctionListsBll _sanctionBll;

        /// <summary>
        /// The current name of user that login to system.
        /// </summary>
        private string _currentUserLogin;

        /// <summary>
        /// The instance object of class EuUpdateProgress, this class provies methods to set and get current status
        /// of update sanctions lists process.
        /// </summary>
        private EuUpdateProgress _updateProgress;

        /// <summary>
        /// The instance object of Logger, this clas provies mothods to log activity to database.
        /// </summary>
        private Logger _logger;

        /// <summary>
        /// The collection of Regulation class, for keep regulations that can be reused in many objects, 
        /// etc. Entity, NameAlias, Address,...
        /// </summary>
        private List<Regulation> _regulations;

        /// <summary>
        /// The flag to protect to call disposing many time.
        /// </summary>
        private bool _disposed = false;
        #endregion

        #region [Constructor]

        /// <summary>
        /// Initializes a new instance of the <see cref="EuUpdateManager"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        public EuUpdateManager(string username)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            _currentUserLogin = username;

            _logger = Logger.GetLogger(
                System.Configuration.ConfigurationManager.ConnectionStrings["SanctionListsDb"].ConnectionString);

            _sanctionBll = new SanctionListsBll(
                System.Configuration.ConfigurationManager.ConnectionStrings["SanctionListsDb"].ConnectionString);
            
            _updateProgress = new EuUpdateProgress();

            // Load enumerable table just first tiem and use them for through process.
            LoadEnumerableTable();
        }

        #endregion

        #region [Public Methods]
        
        /// <summary>
        /// The UpdateList method 
        /// </summary>
        /// <param name="rssUrl">The RSS URL.</param>
        public void UpdateList(string rssUrl)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            XmlDocument globalXml = null;

            // To store the current entity progressing, if get error will save this id to log.
            int currentEntityId = 0;

            try
            {
                // Check the current status of updating list, if still update will throw 
                if (CheckStatusIsUpdating())
                {
                    // Log
                    LogActivity("List is updating");

                    _updateProgress.Update(UpdateState.Finished, 100);
                    UpdateStatusToGlobal("Sanctions list is updating ", status: "Finished");
                    return;
                }
                // Log
                LogActivity("Start automatic update sanctions list");

                string globalUrl;
                string globalFsdUrl;
                string globalDateTimeFormat = _sanctionBll.SettingsGet(_euListType.ListTypeId, "GlobalDateTimeFormat").Value;

                _updateProgress.Update(UpdateState.Checking);

                UpdateStatusToGlobal("Checking Rss publication date...");
                DateTime rssPublicDate = DateTime.UtcNow;
                if (CheckEuIsUpdated(rssUrl, out rssPublicDate, out globalUrl, out globalFsdUrl))
                {
                    // Log
                    LogActivity("Automatic update is finished, list is already updated.");
                    _updateProgress.Update(UpdateState.Finished, 100);
                    UpdateStatusToGlobal(String.Format("List is already updated (publication date is {0}).", rssPublicDate.ToString()), status: "Finished");
                    return;
                }

                // Remove temporary data, in status is updating.
                _updateProgress.Update(UpdateState.RemoveTemporary);
                UpdateStatusToGlobal("Remove temporary data...");
                _sanctionBll.EntitiesRemoveByStatus(_euListType.ListTypeId, _statusUpdating.StatusId);

                _updateProgress.Update(UpdateState.DownloadXml);
                UpdateStatusToGlobal("Loading global xml...");

                globalXml = new XmlDocument();
                globalXml.Load(globalUrl);

                // Add Global xml to database.
                int listArchiveId = AddGlobalXml(globalXml);

                decimal progress = 0;
                decimal running = 0;
                int entityNum = 0;
                
                _updateProgress.Update(UpdateState.Parsing);
                UpdateStatusToGlobal("Start parsing...");
                
                Debug.WriteLine("start : "+DateTime.Now.ToString("yyyy-MM-dd HHmmss"));

                using (EuParser euParser = EuParser.Parse(globalDateTimeFormat, globalXml))
                {
                    

                    entityNum = euParser.Entities.Count;

                    _updateProgress.Update(UpdateState.AddSanctionList);
                    UpdateStatusToGlobal("Add sanctions to Database...");
                    Entity entity = null;

                    foreach (EuEntity parserEntity in euParser.Entities)
                    {
                        Debug.WriteLine("EntityId : " + parserEntity.EntityId);

                        // assign current entityid for save to log when get error.
                        currentEntityId = parserEntity.EntityId;

                        entity = AddEntity(listArchiveId, parserEntity);
                        entity = _sanctionBll.EntitiesAdd(entity);

                        // update regulationid from returned regulation object of bll to local regulation _regulations.
                        UpdateRegulationId(entity);

                        // calculate the progress
                        running++;
                        progress = Math.Truncate((running / euParser.Entities.Count) * 100);
                        _updateProgress.Update(UpdateState.AddSanctionList, progress);
                        UpdateStatusToGlobal(String.Format("Add sanctions to Database...({0}/{1})", running, entityNum));
                    }
                }

                _updateProgress.Update(UpdateState.ChangeActiveToRemove);
                UpdateStatusToGlobal("Change status current sanctions lists to Removed...");
                // change entity status from active to removed
                _sanctionBll.EntitiesChangeStatus(_euListType.ListTypeId, STATUS_ACTIVE, STATUS_REMOVED);

                _updateProgress.Update(UpdateState.ChangeUpdateToActive);
                UpdateStatusToGlobal("Change status new sanctions lists to Active...");
                // update status for all new entity from updating to active.
                _sanctionBll.EntitiesChangeStatus(_euListType.ListTypeId, STATUS_UPDATING, STATUS_ACTIVE);

                _updateProgress.Update(UpdateState.RemoveSanctionlist);
                UpdateStatusToGlobal("Removed the old sanction list...");
                // remove all old entity that status is removed.
                _sanctionBll.EntitiesRemoveByStatus(_euListType.ListTypeId, STATUS_REMOVED);

                UpdateToUpdatesTable(listArchiveId, rssPublicDate);

                _updateProgress.Update(UpdateState.Finished, 100);
                UpdateStatusToGlobal(String.Format("Finished, {0} entities added.", entityNum), status: "Finished");

                // Log
                LogActivity(String.Format("Automatic update is finished, {0} entities is added, publication date is {0}", entityNum, rssPublicDate.ToString()));
            }
            catch (Exception ex)
            {   
                _updateProgress.Update(UpdateState.RemoveSanctionlist);
                UpdateStatusToGlobal("Error : System will removed the last one...", HttpStatusCode.InternalServerError, "Finished");
                // remove all new entity that just insert, if got an error.
                _sanctionBll.EntitiesRemoveByStatus(_euListType.ListTypeId, STATUS_UPDATING);
                throw new Exception("Get error on entityId : " + currentEntityId, ex);
            }
            finally
            {
                if (globalXml != null)
                {
                    globalXml = null;
                }
            }
        }

        #endregion

        #region [Private Methods]

        /// <summary>
        /// Checks the pubDate field of Eu sanctions lists compare with data that stored in database,
        /// if data that already updated will return true, if not will return false wiht date that update
        /// and global xml url.
        /// </summary>
        /// <param name="rssUrl">The RSS URL.</param>
        /// <param name="rssPublicDate">The RSS public date.</param>
        /// <param name="globalUrl">The global URL.</param>
        /// <param name="globalFsdUrl">The global FSD URL.</param>
        /// <returns><c>true</c> if data is already updated, <c>false</c> otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">rssUrl</exception>
        /// <exception cref="System.Exception">
        /// Rss did not have the pubDate
        /// or
        /// Cannot find the Global Xml File in Rss.
        /// </exception>
        private bool CheckEuIsUpdated(string rssUrl, out DateTime rssPublicDate,
            out string globalUrl, out string globalFsdUrl)
        {
            if (String.IsNullOrEmpty(rssUrl))
            {
                throw new ArgumentNullException("rssUrl");
            }
            globalUrl = "";
            globalFsdUrl = "";

            Setting setting = _sanctionBll.SettingsGet(_euListType.ListTypeId, "pubDateFormat");
            string pubDateFormat = setting.Value;

            // The RSS of Eu sanction list they create wrong format for pubDate.
            // RSS formatter have the rule for pubDate must be follow this format "ddd MMM dd HH:mm:ss Z yyyy".
            // In Eu they create pubDate in this format "yyyy-mm-dd"
            CustomDateFormat customDate = new CustomDateFormat();
            customDate.AddCustomFormat("pubDate", pubDateFormat);

            XmlReader reader = new CustomXmlReader(rssUrl, customDate);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            SyndicationElementExtension synEl = feed.ElementExtensions.FirstOrDefault(e => e.OuterName == "pubDate");
            if (synEl == null)
            {
                throw new Exception("Rss did not have the pubDate");
            }
            string pubDate = synEl.GetObject<string>();
            rssPublicDate = DateTime.ParseExact(pubDate, customDate.GetCustomFormat("pubDate"), CultureInfo.InvariantCulture);

            // Get global xml and global fsd xml url.
            SyndicationItem item = feed.Items.FirstOrDefault(f => f.Summary.Text.IndexOf("Global XML File") > -1);
            if (item == null)
            {
                throw new Exception("Cannot find the Global Xml File in Rss.");
            }
            globalUrl = item.Links.FirstOrDefault().Uri.AbsoluteUri;

            item = feed.Items.FirstOrDefault(f => f.Summary.Text.IndexOf("Global XML-XSD File") > -1);
            if (item != null)
            {
                globalFsdUrl = item.Links.FirstOrDefault().Uri.AbsoluteUri;
            }

            // Get pubDate from DataBase.
            Update euUpdated = _sanctionBll.UpdatesGet(EU_LIST_TYPE_ID);
            if (euUpdated == null)
            {
                return false;
            }
            DateTime dateFromDb = euUpdated.PublicDate;

            // Compare the date.
            bool isUpdated = (dateFromDb.Date >= rssPublicDate.Date);

            return isUpdated;
        }

        /// <summary>
        /// This method will return the regulation that already created from previous process to use the same
        /// regulation for all entities by check RetulationTitle property, if it's not found method will create 
        /// new Regulation and store in _regulations fileds.
        /// </summary>
        /// <param name="euRegulation">The eu regulation.</param>
        /// <returns>Regulation.</returns>
        private Regulation GetOrAddNewRegulation(EuRegulation euRegulation)
        {
            Regulation regulation = _regulations.FirstOrDefault(f => f.RegulationTitle == euRegulation.RegulationTitle);
            if (regulation == null)
            {
                regulation = new Regulation
                {
                    Programme = euRegulation.Programme,
                    PublicationDate = euRegulation.PublicationDate,
                    PublicationTitle = euRegulation.PublicationTitle,
                    PublicationUrl = euRegulation.PublicationUrl,
                    RegulationDate = euRegulation.RegulationDate,
                    RegulationTitle = euRegulation.RegulationTitle,
                    ListType = _euListType
                };
                _regulations.Add(regulation);
            }
            return regulation;
        }

        /// <summary>
        /// Updates the RegulationId in to _regulations filed, that come from data that returned 
        /// after call BLL EntityAdd method.
        /// </summary>
        /// <param name="entity">The entity.</param>
        private void UpdateRegulationId(Entity entity)
        {
            List<Regulation> regulations = new List<Regulation>();
            regulations.Add(entity.Regulation);
            regulations.AddRange(entity.Addresses.Select(a => a.Regulation));
            regulations.AddRange(entity.Births.Select(b => b.Regulation));
            regulations.AddRange(entity.Citizenships.Select(c => c.Regulation));
            regulations.AddRange(entity.ContactInfo.Select(c => c.Regulation));
            regulations.AddRange(entity.Identifications.Select(i => i.Regulation));
            regulations.AddRange(entity.NameAliases.Select(n => n.Regulation));

            // Update latest regulations to memory.
            regulations.ForEach(r =>
            {
                _regulations.ForEach(r2 =>
                {
                    if (r != null && r2.PublicationTitle == r.PublicationTitle)
                    {
                        r2.RegulationId = r.RegulationId;
                    }
                });
            });
        }

        /// <summary>
        /// Create list of Address from EuAddress that from EuParser.
        /// </summary>
        /// <param name="euAddresses">The eu addresses.</param>
        /// <returns>List&lt;Address&gt;.</returns>
        /// <exception cref="System.Exception"></exception>
        private List<Address> AddAddresses(List<EuAddress> euAddresses)
        {
            try
            {
                if (euAddresses == null)
                {
                    return null;
                }
                List<Address> result = new List<Address>();

                foreach (EuAddress euAddress in euAddresses)
                {
                    //Add Address
                    Address address = new Address
                    {
                        City = euAddress.City,
                        Country = GetCountryByIso3(euAddress.Country),
                        Number = euAddress.Number,
                        OriginalAddressId = euAddress.AddressId,
                        Regulation = GetOrAddNewRegulation(euAddress.Regulation),
                        Remark = String.IsNullOrEmpty(euAddress.Remark) ? null : new Remark
                            {
                                Value = euAddress.Remark
                            },
                        Street = euAddress.Street,
                        Zipcode = euAddress.Zipcode,
                        Other = euAddress.Other
                    };
                    
                    result.Add(address);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Cannot add Addresses data.\n{0}", ex.Message));
            }
        }

        /// <summary>
        /// Create list of NameAlias from EuNameAlias that from EuParser.
        /// </summary>
        /// <param name="subjectType">Type of the subject.</param>
        /// <param name="euNameAliases">The eu name aliases.</param>
        /// <returns>List&lt;NameAlias&gt;.</returns>
        /// <exception cref="System.Exception"></exception>
        private List<NameAlias> AddNameAliases(string subjectType, List<EuNameAlias> euNameAliases)
        {
            try
            {
                if (euNameAliases == null)
                {
                    return null;
                }
                List<NameAlias> result = new List<NameAlias>();
                foreach (EuNameAlias euNameAlias in euNameAliases)
                {
                    //Add NameAlias
                    NameAlias nameAlias = new NameAlias
                    {
                        FirstName = euNameAlias.FirstName,
                        Function = euNameAlias.Function,
                        Gender = GetGenderByShortName(subjectType, euNameAlias.Gender),
                        Language = GetLanguageByIso2(euNameAlias.Language),
                        LastName = euNameAlias.LastName,
                        MiddleName = euNameAlias.MiddleName,
                        OriginalNameAliasId = euNameAlias.NameAliasId,
                        Regulation = GetOrAddNewRegulation(euNameAlias.Regulation),
                        Remark = String.IsNullOrEmpty(euNameAlias.Remark) ? null : new Remark
                            {
                                Value = euNameAlias.Remark
                            },
                        Title = euNameAlias.Title,
                        WholeName = euNameAlias.WholeName
                    };
                    result.Add(nameAlias);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Cannot add NameAliases data.\n{0}", ex.Message));
            }
        }

        /// <summary>
        /// Create list of Birth from EuBirth that from EuParser.
        /// </summary>
        /// <param name="euBirths">The eu births.</param>
        /// <returns>List&lt;Birth&gt;.</returns>
        /// <exception cref="System.Exception"></exception>
        private List<Birth> AddBirths(List<EuBirth> euBirths)
        {
            try
            {
                if (euBirths == null)
                {
                    return null;
                }
                List<Birth> result = new List<Birth>();

                foreach (EuBirth euBirth in euBirths)
                {
                    //Add Birth
                    Birth birth = new Birth
                    {
                        Country = GetCountryByIso3(euBirth.Country),
                        Day = euBirth.Day,
                        Month = euBirth.Month,
                        Year = euBirth.Year,
                        OriginalBirthId = euBirth.BirthId,
                        Place = euBirth.Place,
                        Regulation = GetOrAddNewRegulation(euBirth.Regulation),
                        Remark = String.IsNullOrEmpty(euBirth.Remark) ? null : new Remark
                        {
                            Value = euBirth.Remark
                        }
                    };
                    result.Add(birth);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Cannot add Births data.\n{0}", ex.Message));
            }
        }

        private string MakeRemarkCannotPase(string remark, string parseName, string parseData)
        {
            return remark  + String.Format("The {0} '{1}' cannot be parse.", parseName, parseData);
        }

        /// <summary>
        /// Create list of Citizenship from EuCitizenship that from EuParser.
        /// </summary>
        /// <param name="euCitizenships">The eu citizenships.</param>
        /// <returns>List&lt;Citizenship&gt;.</returns>
        /// <exception cref="System.Exception"></exception>
        private List<Citizenship> AddCitizenships(List<EuCitizenship> euCitizenships)
        {
            try
            {
                if (euCitizenships == null)
                {
                    return null;
                }
                List<Citizenship> result = new List<Citizenship>();

                foreach (EuCitizenship euCitizenship in euCitizenships)
                {
                    Country country = GetCountryByIso3(euCitizenship.Country);
                    string comment = "";
                    comment = (country != null) ? "" : MakeRemarkCannotPase(comment, "Country", euCitizenship.Country);
                    
                    Remark remark = String.IsNullOrEmpty(euCitizenship.Remark) &&
                                    String.IsNullOrEmpty(comment)
                                        ? null : new Remark
                                            {
                                                Value = euCitizenship.Remark + comment
                                            };

                    //Add Citizenship
                    Citizenship citizenship = new Citizenship
                    {
                        Country = country,
                        OriginalCitizenshipId = euCitizenship.CitizenshipId,
                        Regulation = GetOrAddNewRegulation(euCitizenship.Regulation),
                        Remark = remark
                    };
                    result.Add(citizenship);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Cannot add Citizenships data.\n{0}", ex.Message));
            }
        }

        /// <summary>
        /// Create list of Identification from EuIdentification that from EuParser.
        /// </summary>
        /// <param name="euIdentifications">The eu identifications.</param>
        /// <returns>List&lt;Identification&gt;.</returns>
        /// <exception cref="System.Exception"></exception>
        private List<Identification> AddIdentifications(List<EuIdentification> euIdentifications)
        {
            try
            {
                if (euIdentifications == null)
                {
                    return null;
                }
                List<Identification> result = new List<Identification>();

                foreach (EuIdentification euIdentification in euIdentifications)
                {
                    //Add Identification
                    Identification identification = new Identification
                    {
                        IssueCountry = GetCountryByIso3(euIdentification.Country),
                        DocumentNumber = euIdentification.DocumentNumber,
                        IdentificationType = _identificationTypesOther,
                        IssueCity = euIdentification.IssueCity,
                        IssueDate = euIdentification.IssueDate,
                        OriginalIdentificationId = euIdentification.IdentificationId,
                        Regulation = GetOrAddNewRegulation(euIdentification.Regulation),
                        Remark = String.IsNullOrEmpty(euIdentification.Remark) ? null : new Remark
                        {
                            Value = euIdentification.Remark
                        }
                    };
                    result.Add(identification);
                }
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(String.Format("Cannot add Identifications data.\n{0}", ex.Message));
            }
        }

        /// <summary>
        /// Create Entity from EuEntity that from EuParser.
        /// </summary>
        /// <param name="listArchiveId">The list archive identifier.</param>
        /// <param name="euEntity">The eu entity.</param>
        /// <returns>Entity.</returns>
        /// <exception cref="System.Exception"></exception>
        private Entity AddEntity(int listArchiveId, EuEntity euEntity)
        {
            try
            {
                //Add Entity
                Entity entity = new Entity
                {
                    ListType = _euListType,
                    ListArchiveId = listArchiveId,
                    OriginalEntityId = euEntity.EntityId,
                    Regulation = GetOrAddNewRegulation(euEntity.Regulation),
                    Status = _statusUpdating,
                    SubjectType = GetSubjectTypeByShortName(euEntity.SubjectType),
                    Remark = String.IsNullOrEmpty(euEntity.Remark)?null : new Remark
                            {
                                Value = euEntity.Remark
                            },
                    NameAliases = AddNameAliases(euEntity.SubjectType, euEntity.NameAliases),
                    Addresses = AddAddresses(euEntity.Addresses),
                    Births = AddBirths(euEntity.Births),
                    Citizenships = AddCitizenships(euEntity.Citizens),
                    Identifications = AddIdentifications(euEntity.Identifications)
                };

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Cannot add Entity data.\n{0}", ex.Message));
            }

        }

        /// <summary>
        /// Loads the enumerable table to memory in one time, for reduce communicate with database.
        /// </summary>
        private void LoadEnumerableTable()
        {
            _euListType = _sanctionBll.ListTypesGet().Single(f => f.Name.ToUpper() == "EU");
            _statusUpdating = _sanctionBll.StatusesGet().Single(f => f.StatusId == STATUS_UPDATING);
            _subjectTypes = _sanctionBll.SubjectTypesGet();
            _genders = _sanctionBll.GendersGet();
            _languases = _sanctionBll.LanguagesGet();
            _countries = _sanctionBll.CountriesGet();
            _identificationTypesOther = _sanctionBll.IdentificationTypesGet().Single(f => f.Name == "Other");
            _contactInfoTypes = _sanctionBll.ContactInfoTypesGet();
            _regulations = _sanctionBll.RegulationsGet(_euListType.ListTypeId).ToList();
        }

        /// <summary>
        /// Get SubjectType object from subjectType string data of parser.
        /// </summary>
        /// <param name="subjectType">Type of the subject.</param>
        /// <returns>SubjectType.</returns>
        private SubjectType GetSubjectTypeByShortName(string subjectType)
        {
            if (_subjectTypes == null)
            {
                return null;
            }

            if (subjectType == "P")
            {
                return _subjectTypes.First(f => f.Name == "Person");
            }
            else if (subjectType == "E")
            {
                return _subjectTypes.First(f => f.Name == "Enterprise");
            }
            else
            {
                //TODO:: concern in case of cannot parse subject type.
                return null;
            }
        }

        /// <summary>
        /// Get Gender object from gender string data of parser.
        /// </summary>
        /// <param name="subjectType">Type of the subject.</param>
        /// <param name="gender">The gender.</param>
        /// <returns>Gender.</returns>
        private Gender GetGenderByShortName(string subjectType, string gender)
        {
            if (_genders == null)
            {
                return null;
            }

            // in case of Enterprise gender should be Not Applicable.
            if (subjectType == "E")
            {
                // In case of enterprise xml will set gender is empty.
                return _genders.First(f => f.Name == "Not Applicable");
            }

            if (gender == "M")
            {
                return _genders.First(f => f.Name == "Male");
            }
            else if (gender == "F")
            {
                return _genders.First(f => f.Name == "Female");
            }
            else if (gender == "")
            {
                return _genders.First(f => f.Name == "Not Known");
            }
            else
            {
                //TODO:: concern in case of canot parse gender.
                return _genders.First(f => f.Name == "Not Applicable");
            }
        }

        /// <summary>
        /// Get Language object from language string data of parser.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <returns>Language.</returns>
        private Language GetLanguageByIso2(string language)
        {
            if (_languases == null)
            {
                return null;
            }

            try
            {
                return _languases.First(f => f.Iso2 == language);
            }
            catch (Exception)
            {
                //TODO:: concern in case of canot parse languase.
                return null;
            }
            
        }

        /// <summary>
        /// Get Country object from country string data of parser.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <returns>Country.</returns>
        private Country GetCountryByIso3(string country)
        {
            if (_countries == null || String.IsNullOrEmpty(country))
            {
                return null;
            }
            return _countries.FirstOrDefault(f => f.Iso3 == country);
        }

        /// <summary>
        /// Updates the status to application global variable.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="responseCode">The response code.</param>
        /// <param name="status">The status.</param>
        private void UpdateStatusToGlobal(string message,
            HttpStatusCode responseCode = HttpStatusCode.OK, string status = "Updating")
        {
            _updateProgress.SetStatus(message, status, (int)responseCode);

            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application[GLOBAL_STATUS_NAME] = _updateProgress.GetStatus();
            HttpContext.Current.Application.UnLock();
        }

        /// <summary>
        /// Checks the status is updating from application global variable, to prevent call method two times.
        /// </summary>
        /// <returns><c>true</c> if status is updating, <c>false</c> otherwise.</returns>
        private bool CheckStatusIsUpdating()
        {
            AutoUpdateStatus updatestatus = HttpContext.Current.Application[GLOBAL_STATUS_NAME] as AutoUpdateStatus;
            if (updatestatus != null && updatestatus.Status == "Updating")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates lastest date and file to Updates table.
        /// </summary>
        /// <param name="listArchiveId">The list archive identifier.</param>
        /// <param name="rssPublicDate">The RSS public date.</param>
        private void UpdateToUpdatesTable(int listArchiveId, DateTime rssPublicDate)
        {
            // update information to Update table.
            Update update = new Update
            {
                ListType = _euListType,
                PublicDate = rssPublicDate,
                UpdatedDate = DateTime.UtcNow,
                Username = _currentUserLogin,
                ListArchiveId = listArchiveId
            };
            _sanctionBll.UpdatesSet(update);
        }

        /// <summary>
        /// Adds the global XML to ListArchive table.
        /// </summary>
        /// <param name="XmlFile">The XML file.</param>
        /// <returns>System.Int32.</returns>
        private int AddGlobalXml(XmlDocument XmlFile)
        {
            Encoding encoding = Encoding.UTF8;
            byte[] xml = null;
            try
            {
                xml = encoding.GetBytes(XmlFile.OuterXml);
                // update information to ListArchive table.
                ListArchive listArchive = new ListArchive
                {
                    Date = DateTime.UtcNow,
                    File = xml
                };
                listArchive = _sanctionBll.ListArchiveAdd(listArchive);
                return listArchive.ListArchiveId;
            }
            finally
            {
                xml = null;
            }
        }


        /// <summary>
        /// Logs the activity of each step.
        /// </summary>
        /// <param name="description">The description.</param>
        private void LogActivity(string description)
        {
            _logger.LogActivity(
                _currentUserLogin,
                1,
                _euListType.ListTypeId,
                description);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_euListType != null)
                {
                    _euListType = null;
                }
                if (_statusUpdating != null)
                {
                    _statusUpdating = null;
                }
                if (_subjectTypes != null)
                {
                    _subjectTypes = null;
                }
                if (_genders != null)
                {
                    _genders = null;
                }
                if (_languases != null)
                {
                    _languases = null;
                }
                if (_countries != null)
                {
                    _countries = null;
                }
                if (_identificationTypesOther != null)
                {
                    _identificationTypesOther = null;
                }
                if (_contactInfoTypes != null)
                {
                    _contactInfoTypes = null;
                }
                if (_sanctionBll != null)
                {
                    _sanctionBll = null;
                }
                if (_updateProgress != null)
                {
                    _updateProgress = null;
                }
                if (_logger != null)
                {
                    _logger = null;
                }
                if (_regulations != null)
                {
                    _regulations = null;
                }
            }

            _disposed = true;
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}