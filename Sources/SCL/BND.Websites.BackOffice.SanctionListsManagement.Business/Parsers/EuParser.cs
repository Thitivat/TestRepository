using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using BND.Websites.BackOffice.SanctionListsManagement.Entities.EuEntities;

namespace BND.Websites.BackOffice.SanctionListsManagement.Business.Parsers
{
    /// <summary>
    /// Class EuParser provides static method to create and parse the sanctions data from Global Xml to Entity object.
    /// The global xml file contains all information about entity, regulation, nameAlias, address, birth, citizenships
    /// identification and contactInfo. But for more information about identification and contactInfo will come from 
    /// global fsd xml file.
    /// </summary>
    public class EuParser : IDisposable
    {
        #region [Fields]
        /// <summary>
        /// The _global XML
        /// </summary>
        private XmlDocument _globalXml;

        /// <summary>
        /// The _entities
        /// </summary>
        private List<EuEntity> _entities;

        /// <summary>
        /// The _current entity identifier
        /// </summary>
        private string _currentEntityId = "";

        /// <summary>
        /// The _current tag name
        /// </summary>
        private string _currentTagName = "";

        // ReSharper disable once NotAccessedField.Local
        private string _globalDateFormat;
        #endregion

        #region [Properties]
        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>The entities.</value>
        public List<EuEntity> Entities
        {
            get
            {
                return _entities;
            }
            set
            {
                _entities = value;
            }
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="EuParser" /> class.
        /// </summary>
        /// <param name="globalDateFormat">The global date format.</param>
        /// <param name="global">The global.</param>
        /// <exception cref="Exception">Tags or element WHOLE\\ENTITY not found or no data.</exception>
        /// <exception cref="System.Exception">Tags or element WHOLE\\ENTITY not found or no data.</exception>
        private EuParser(string globalDateFormat, XmlDocument global)
        {
            _entities = new List<EuEntity>();
            _globalXml = global;
            _globalDateFormat = globalDateFormat;

            XmlNodeList entityList = _globalXml.SelectNodes("/WHOLE/ENTITY");
            if (entityList == null || entityList.Count == 0)
            {
                throw new Exception("Tags or element WHOLE\\ENTITY not found or no data.");
            }
            foreach (XmlNode entityNode in entityList)
            {
                _entities.Add(ParseEntity(entityNode));
            }
        }
        #endregion

        #region [Public Methods]

        /// <summary>
        /// Updates the entity object by global FSD xml file, fsd file will have more information about sanctions list
        /// than global xml. This method will get data from that file and parse via update to entity object. 
        /// </summary>
        /// <param name="dateTimeFormat">The date time format.</param>
        /// <param name="globalXml">The global XML.</param>
        /// <param name="entity">The entity.</param>
        public void UpdateEntityByGlobalFsd(string dateTimeFormat, XmlDocument globalXml, EuEntity entity)
        {
            // TODO finished this method.
        }

        #endregion

        #region [Static Methods]
        /// <summary>
        /// Parses the specified global XML by send the Url to method.
        /// </summary>
        /// <param name="globalDateFormat">The global date format.</param>
        /// <param name="globalXmlUrl">The global XML URL.</param>
        /// <returns>EuParser.</returns>
        /// <exception cref="System.ArgumentNullException">globalXmlUrl</exception>
        public static EuParser Parse(string globalDateFormat, string globalXmlUrl)
        {
            if (String.IsNullOrWhiteSpace(globalXmlUrl))
            {
                throw new ArgumentNullException("globalXmlUrl");
            }

            // Load Global xml file.
            XmlDocument globalXml = new XmlDocument();
            globalXml.Load(globalXmlUrl);

            return new EuParser(globalDateFormat, globalXml);
        }

        /// <summary>
        /// Parses the specified global XML by send the xml document.
        /// </summary>
        /// <param name="dateTimeFormat">The date time format.</param>
        /// <param name="globalXml">The global XML.</param>
        /// <returns>EuParser.</returns>
        /// <exception cref="System.ArgumentNullException">globalXmlUrl</exception>
        public static EuParser Parse(string dateTimeFormat, XmlDocument globalXml)
        {
            if (globalXml == null)
            {
                throw new ArgumentNullException("globalXml");
            }

            return new EuParser(dateTimeFormat, globalXml);
        }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _globalXml = null;
            if (_entities != null)
            {
                _entities.Clear();
                _entities = null;
            }
        }

        #endregion

        #region [Private Methods]
        /// <summary>
        /// Gets the attribute from xml and throw proper exception message to parent.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="isRequire">if set to <c>true</c> [is require].</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentNullException">Attribute not found.</exception>
        private string GetAttribute(XmlNode node, string attributeName, bool isRequire = false)
        {
            if (node.Attributes != null && node.Attributes[attributeName] == null)
            {
                if (isRequire)
                {
                    throw new ArgumentNullException(
                        String.Format("EntityId: {0}, {1}\\{2}", _currentEntityId, _currentTagName, attributeName),
                        "Attribute not found.");
                }

                return null;
            }

            // ReSharper disable once PossibleNullReferenceException
            return node.Attributes[attributeName].Value;
        }

        /// <summary>
        /// Gets the element from xml and throw proper exception message to parent.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="isRequire">if set to <c>true</c> [is require].</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentNullException">Element not found.</exception>
        private string GetElement(XmlNode node, string elementName, bool isRequire = false)
        {
            if (node[elementName] == null)
            {
                if (isRequire)
                {
                    throw new ArgumentNullException(
                        String.Format("EntityId: {0}, {1}\\{2}", _currentEntityId, _currentTagName, elementName),
                        "Element not found.");
                }

                return null;
            }
            return node[elementName].InnerText;
        }

        /// <summary>
        /// Gets the date time data from string and throw proper exception message to parent.
        /// </summary>
        /// <param name="datetimeString">The datetime string.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="isRequire">if set to <c>true</c> [is require].</param>
        /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
        /// <exception cref="System.ArgumentException">Cannot parse type date time.</exception>
        private DateTime? GetDateTime(string datetimeString, string fieldName, bool isRequire = false)
        {
            try
            {
                if (String.IsNullOrEmpty(datetimeString) && !isRequire)
                {
                    return null;
                }
                return DateTime.ParseExact(datetimeString, datetimeString, CultureInfo.InstalledUICulture);
            }
            catch
            {
                throw new ArgumentException(
                    "Cannot parse type date time.",
                    String.Format("EntityId: {0}, {1}\\{2}", _currentEntityId, _currentTagName, fieldName));
            }
        }
        /// <summary>
        /// Parses the regulation from xml to EuRegulation obejct.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>EuRegulation.</returns>
        private EuRegulation ParseRegulation(XmlNode node)
        {
            EuRegulation regulation = new EuRegulation
            {
                RegulationTitle = GetAttribute(node, "legal_basis"),
                RegulationDate = GetDateTime(GetAttribute(node, "reg_date"), "reg_date")
            };

            if (regulation.RegulationDate != null) regulation.PublicationDate = regulation.RegulationDate.Value;
            regulation.PublicationTitle = regulation.RegulationTitle;
            regulation.PublicationUrl = GetAttribute(node, "pdf_link");
            regulation.Programme = GetAttribute(node, "programme");

            return regulation;
        }

        /// <summary>
        /// Parses the Entity from xml to EuEntity obejct.
        /// </summary>
        /// <param name="entityNode">The entity node.</param>
        /// <returns>EuEntity.</returns>
        private EuEntity ParseEntity(XmlNode entityNode)
        {
            EuEntity entity = new EuEntity { EntityId = Int32.Parse(GetAttribute(entityNode, "Id", true)) };

            // Set property of Entity
            _currentEntityId = entity.EntityId.ToString();
            _currentTagName = "ENTITY";

            // Parse regulation
            entity.Regulation = ParseRegulation(entityNode);

            // Set property of Entity
            entity.SubjectType = GetAttribute(entityNode, "Type", true);
            entity.Remark = GetAttribute(entityNode, "remark");

            // Parse NameAlias
            _currentTagName = "NAME";
            XmlNodeList nameList = entityNode.SelectNodes("NAME");
            if (nameList != null)
                foreach (XmlNode nameAliasNode in nameList)
                {
                    entity.NameAliases.Add(ParseGlobalNameAlias(nameAliasNode));
                }

            // Parse Birth
            _currentTagName = "BIRTH";
            XmlNodeList birthList = entityNode.SelectNodes("BIRTH");
            if (birthList != null)
                foreach (XmlNode birthNode in birthList)
                {
                    entity.Births.Add(ParseBirth(birthNode));
                }

            // Parse Citizen
            _currentTagName = "CITIZEN";
            XmlNodeList citizenList = entityNode.SelectNodes("CITIZEN");
            if (citizenList != null)
                foreach (XmlNode citizenNode in citizenList)
                {
                    entity.Citizens.Add(ParseCitizen(citizenNode));
                }

            // Parse Address & ContactInfo
            _currentTagName = "ADDRESS";
            XmlNodeList addressList = entityNode.SelectNodes("ADDRESS");
            if (addressList != null)
                foreach (XmlNode addressNode in addressList)
                {
                    entity.Addresses.Add(ParseAddress(addressNode));

                    // contract info belong to address tag in "OTHER" tag.
                    // we didn't parse the contract info from global, we will get it from FSD.
                    //entity.ContactInfo.AddRange(ParseContactInfo(addressNode));
                }

            // Parse Identification
            _currentTagName = "PASSPORT";
            XmlNodeList identificationList = entityNode.SelectNodes("PASSPORT");
            if (identificationList != null)
                foreach (XmlNode identificationNode in identificationList)
                {
                    entity.Identifications.Add(ParseIdentification(identificationNode));
                }

            return entity;
        }

        /// <summary>
        /// Parses the NameAlias from xml to EuNameAlias obejct.
        /// </summary>
        /// <param name="nameAliasNode">The name alias node.</param>
        /// <returns>EuNameAlias.</returns>
        private EuNameAlias ParseGlobalNameAlias(XmlNode nameAliasNode)
        {
            EuNameAlias nameAliases = new EuNameAlias
            {
                Regulation = ParseRegulation(nameAliasNode),
                NameAliasId = Int32.Parse(GetAttribute(nameAliasNode, "Id", true)),
                EntityId = Int32.Parse(GetAttribute(nameAliasNode, "Entity_id", true)),
                FirstName = GetElement(nameAliasNode, "FIRSTNAME", true),
                LastName = GetElement(nameAliasNode, "LASTNAME", true),
                MiddleName = GetElement(nameAliasNode, "MIDDLENAME", true),
                WholeName = GetElement(nameAliasNode, "WHOLENAME", true),
                Gender = GetElement(nameAliasNode, "GENDER", true),
                Title = GetElement(nameAliasNode, "TITLE", true),
                Language = GetElement(nameAliasNode, "LANGUAGE", true),
                Function = GetElement(nameAliasNode, "FUNCTION", true),
                Quality = null,
                Remark = GetAttribute(nameAliasNode, "remark")
            };

            // Parse regulation

            // Set property of NameAlias
            //strong

            return nameAliases;
        }

        /// <summary>
        /// Extracts the birth date from xml string to day, moth and year separately.
        /// </summary>
        /// <param name="tagData">The tag data.</param>
        /// <param name="day">The day.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns>System.String.</returns>
        private string ExtractBirthDateFromTag(string tagData, out int? day, out int? month, out int? year)
        {
            #region [Sample Data]
            /*
             *	<DATE>1942-03-14</DATE>
             *	<DATE>1924</DATE>
             *	<DATE>1957 (approximative)</DATE>
             *	<DATE/> 
             *	<DATE>29-2-1969</DATE> *** this pattern cannot parse with DateTime, and will store in remark
             */
            #endregion

            day = null;
            month = null;
            year = null;
            string remark;
            string pattern = @"^(?<date>[\d-]+)( \((?<remark>.+)\))?$";

            try
            {
                Match match = Regex.Match(tagData, pattern);
                if (match.Success)
                {
                    string date = match.Groups["date"].Value;
                    if (date.Length == 4)
                    {
                        year = Int32.Parse(date);
                    }
                    else
                    {
                        DateTime dt = DateTime.Parse(date);
                        day = dt.Day;
                        month = dt.Month;
                        year = dt.Year;
                    }
                    remark = match.Groups["remark"].Value;
                }
                else
                {
                    remark = tagData;
                }
            }
            catch
            {
                //TODO: notification to log or something.
                // put data that cannot parse to remark.
                remark = tagData;
            }
            return remark;
        }

        /// <summary>
        /// Parses the Birth from xml to EuBirth obejct.
        /// </summary>
        /// <param name="birthNode">The birth node.</param>
        /// <returns>EuBirth.</returns>
        private EuBirth ParseBirth(XmlNode birthNode)
        {
            EuBirth birth = new EuBirth();
            int? day;
            int? month;
            int? year;

            // Parse regulation
            birth.Regulation = ParseRegulation(birthNode);

            // Set property of Birth
            birth.BirthId = Int32.Parse(GetAttribute(birthNode, "Id", true));
            birth.EntityId = Int32.Parse(GetAttribute(birthNode, "Entity_id", true));
            birth.Remark = ExtractBirthDateFromTag(GetElement(birthNode, "DATE"), out day, out month, out year);
            birth.Day = day;
            birth.Month = month;
            birth.Year = year;
            birth.Place = GetElement(birthNode, "PLACE", true);
            birth.Country = GetElement(birthNode, "COUNTRY", true);

            return birth;
        }

        /// <summary>
        /// Parses the Citizenship from xml to EuCitizenship obejct.
        /// </summary>
        /// <param name="citizenNode">The citizen node.</param>
        /// <returns>EuCitizenship.</returns>
        private EuCitizenship ParseCitizen(XmlNode citizenNode)
        {
            EuCitizenship citizen = new EuCitizenship
            {
                Regulation = ParseRegulation(citizenNode),
                Country = GetElement(citizenNode, "COUNTRY", true),
                Remark = GetAttribute(citizenNode, "remark")
            };

            return citizen;
        }

        /// <summary>
        /// Parses the Address from xml to EuAddress obejct.
        /// </summary>
        /// <param name="addressNode">The address node.</param>
        /// <returns>EuAddress.</returns>
        private EuAddress ParseAddress(XmlNode addressNode)
        {
            EuAddress address = new EuAddress
            {
                Regulation = ParseRegulation(addressNode),
                AddressId = Int32.Parse(GetAttribute(addressNode, "Id", true)),
                Number = GetElement(addressNode, "NUMBER", true),
                Street = GetElement(addressNode, "STREET", true),
                Zipcode = GetElement(addressNode, "ZIPCODE", true),
                City = GetElement(addressNode, "CITY", true),
                Country = GetElement(addressNode, "COUNTRY", true),
                Remark = GetAttribute(addressNode, "remark"),
                Other = GetElement(addressNode, "OTHER", true)
            };

            return address;
        }

        /// <summary>
        /// Parses the Identification from xml to EuIdentification obejct.
        /// </summary>
        /// <param name="identificationNode">The identification node.</param>
        /// <returns>EuIdentification.</returns>
        private EuIdentification ParseIdentification(XmlNode identificationNode)
        {
            EuIdentification identification = new EuIdentification();

            // Parse regulation
            identification.Regulation = ParseRegulation(identificationNode);

            // Set property of Identification
            identification.IdentificationId = Int32.Parse(GetAttribute(identificationNode, "Id", true));
            // we didn't parse the number of identification just store all, we will get it from FSD.
            identification.DocumentNumber = GetElement(identificationNode, "NUMBER", true);
            identification.Country = GetElement(identificationNode, "COUNTRY", true);
            identification.Remark = GetAttribute(identificationNode, "remark");

            return identification;
        }
        #endregion
    }
}
