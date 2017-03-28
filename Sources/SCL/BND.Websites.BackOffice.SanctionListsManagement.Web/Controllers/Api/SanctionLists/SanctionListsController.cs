using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Http;
using System.Xml;
using BND.Websites.BackOffice.SanctionListsManagement.Entities;
using BND.Websites.BackOffice.SanctionListsManagement.Web.Common;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Controllers.Api.SanctionLists
{

    /// <summary>
    /// API actions on SanctionLists.
    /// </summary>
    public class SanctionListsController : SanctionListsBaseController
    {
        /// <summary>
        /// Runs automatic update of Sanction List.
        /// </summary>
        /// <param name="listTypeId">List type id</param>
        [HttpPost]
        [Route("api/SanctionLists/{listTypeId}/Updates")]
        public string UpdatesList(int listTypeId)
        {
            // TODO modify it to be generic, now we have generic url but logs are only for EU etc.
            // TODO: Async call update process.
            string ListTypeName =
                _sanctionListsBll.ListTypesGet().FirstOrDefault(w => w.ListTypeId == listTypeId).Name;

            string result = "";

            try
            {
                // Log
                LogActivity(listTypeId, String.Format("Entering to {0} page.", ListTypeName));

                using (EuUpdateManager euManager = new EuUpdateManager(HttpContext.Current.User.Identity.Name))
                {
                    Setting setting = _sanctionListsBll.SettingsGet(listTypeId, "RssUrl");
                    euManager.UpdateList(setting.Value);
                }

                result = "Update is successfully finished.";
            }
            catch (Exception ex)
            {
                Log(ex);

                AutoUpdateStatus updatestatus = new AutoUpdateStatus
                {
                    Message = "Cannot parse the Sanctions List because " + ex.Message,
                    ResponseCode = 500,
                    Status = "Finished",
                    Progress = 0
                };
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application[ListTypeName + "UpdateStatus"] = updatestatus;
                HttpContext.Current.Application.UnLock();

                result = "Error while updating.";
            }

            return result;
        }

        /// <summary>
        /// Gets status of current updateing process.
        /// </summary>
        /// <param name="listTypeId">List type id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/SanctionLists/{listTypeId}/Updates")]
        public AutoUpdateStatus GetUpdateStatus(int listTypeId)
        {
            string ListTypeName =
                _sanctionListsBll.ListTypesGet().FirstOrDefault(w => w.ListTypeId == listTypeId).Name;

            AutoUpdateStatus status = HttpContext.Current.Application[ListTypeName + "UpdateStatus"] as AutoUpdateStatus;
            if (status == null || status.Status == "Finished")
            {
                status = new AutoUpdateStatus
                {
                    Message = "There is no active update in progress.",
                    ResponseCode = 200,
                    Status = "",
                    Progress = 0
                };
            }

            return status;
        }

        /// <summary>
        /// Gets RSS and parses it
        /// </summary>
        /// <param name="listTypeId">List Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/SanctionLists/{listTypeId}/Rss")]
        public RssResponse GetRss(int listTypeId)
        {
            try
            {
                RssResponse response = new RssResponse();

                string ListTypeName =
                    _sanctionListsBll.ListTypesGet().FirstOrDefault(w => w.ListTypeId == listTypeId).Name;
                Setting setting = _sanctionListsBll.SettingsGet(listTypeId, "RssUrl");
                if (setting == null)
                {
                    // didn't have rss to show;
                    response.Title = "No Rss";
                    return response;
                }
                string rssUrl = setting.Value;
                setting = _sanctionListsBll.SettingsGet(listTypeId, "pubDateFormat");
                string pubDateFormat = setting.Value;

                // The RSS of Eu sanction list they create wrong format for pubDate.
                // RSS formatter have the rule for pubDate must be follow this format "ddd MMM dd HH:mm:ss Z yyyy".
                // In Eu they create pubDate in this format "yyyy-mm-dd"
                CustomDateFormat customDate = new CustomDateFormat();
                customDate.AddCustomFormat("pubDate", pubDateFormat);

                XmlReader reader = new CustomXmlReader(rssUrl, customDate);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                // Eu have the information of pubDate in first section, but NL didn't have that information.
                SyndicationElementExtension synEl = feed.ElementExtensions.FirstOrDefault(e => e.OuterName == "pubDate");
                response.PubDate = (synEl == null ? "" : synEl.GetObject<string>());
                //DateTime.ParseExact(pubDate, dateSetting.First().DateFormat, CultureInfo.InvariantCulture);

                response.Language = feed.Language;
                response.Title = feed.Title.Text;
                response.Description = feed.Description.Text;
                int itemNo = 1;
                foreach (SyndicationItem item in feed.Items)
                {
                    response.Items.Add(new RssItem
                    {
                        No = itemNo++,
                        Title = item.Title.Text,
                        PubDate = item.PublishDate.DateTime.ToString(),
                        Link = item.Links.FirstOrDefault().Uri.AbsoluteUri,
                        Content = (item.Content == null ? item.Summary.Text : item.Copyright.Text)
                    });
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Search entities base on criteria .
        /// </summary>
        /// <param name="listTypeId">Id of the list that this obejct belongs to.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns>System.Object.</returns>
        [HttpGet]
        [Route("api/SanctionLists/SearchEntities")]
        public object SearchByCriteria(int listTypeId, string lastName, string firstName = null,
            DateTime? birthDate = null, int? offset = null, int? limit = null)
        {
            SearchEntity searchEntity = new SearchEntity
            {
                LastName = lastName,
                FirstName = firstName,
                BirthDate = birthDate,
                SanctionListType = new ListType {ListTypeId = listTypeId}
            };
            Entity[] entities = _sanctionListsBll.EntitiesGet(searchEntity, offset, limit);
            int entitiesCount = _sanctionListsBll.EntitiesCount(searchEntity);

            return new {Entities = entities, EntitiesCount = entitiesCount};
        }

    }

}
