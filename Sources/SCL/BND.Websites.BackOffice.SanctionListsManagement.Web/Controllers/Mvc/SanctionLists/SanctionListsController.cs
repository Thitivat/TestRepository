using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using BND.Websites.BackOffice.SanctionListsManagement.Entities;
using BND.Websites.BackOffice.SanctionListsManagement.Web.Common;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Controllers.Mvc.SanctionLists
{
    /// <summary>
    /// Sanction lists related actions
    /// </summary>
    public class SanctionListsController : SanctionListsBaseController
    {
        /// <summary>
        /// Main action of the controller. It displays list of all sanction lists.
        /// </summary>
        /// <returns>Rendered view.</returns>
        public ActionResult Index()
        {
            try
            {
                SanctionListDetail[] sanctionListsDetails = base._sanctionListsBll.SanctionListDetailGet();

                ViewBag.sanctionLists = JsonConvert.SerializeObject(sanctionListsDetails);
            }
            catch (Exception ex)
            {
                base.Log(ex);
            }

            return View();
        }

        /// <summary>
        /// Displays view woth detaisl of sanction list.
        /// </summary>
        /// <param name="listTypeId">Sanction list Id</param>
        /// <returns>Rendered view.</returns>
        [Route("SanctionLists/{listTypeId:int}")]
        public ActionResult GetById(int listTypeId)
        {
            try
            {
                ListType[] listTypes = base._sanctionListsBll.ListTypesGet();
                if (listTypes == null || listTypes.Length == 0)
                {
                    return RedirectToAction("Index");
                }

                ListType listType = listTypes.FirstOrDefault(l => l.ListTypeId == listTypeId);
                if (listType == null)
                {
                    return RedirectToAction("Index");
                }

                LogActivity(listTypeId, "Opened page: SanctionLists list");

                ViewBag.ListDetails = JsonConvert.SerializeObject(base._sanctionListsBll.SanctionListDetailGet(listTypeId));

                ViewBag.ListType = listType;
                // UpdateMode is for button, it's mean Automatic or Manual update.
                Setting setting = base._sanctionListsBll.SettingsGet(listTypeId, "ManualUpdate");
                ViewBag.ManualUpdate = setting == null ? 0 : Convert.ToInt16(setting.Value);
                setting = base._sanctionListsBll.SettingsGet(listTypeId, "AutoUpdate");
                ViewBag.AutoUpdate = setting == null ? 0 : Convert.ToInt16(setting.Value);

                ViewBag.Entities = JsonConvert.SerializeObject(base._sanctionListsBll.EntitiesGet(listTypeId, 0, 25)); //listId
                ViewBag.EntitiesCount = JsonConvert.SerializeObject(base._sanctionListsBll.EntitiesCount(listTypeId)); //listId

                return View("Details",
                        new
                        {
                            ListTypeObject = listType,
                            ListTypeItems = listTypes.Select(l => new SelectListItem
                            {
                                Text = l.Name,
                                Value = l.ListTypeId.ToString(),
                                Selected = l.ListTypeId == listTypeId
                            })
                        }
            );
            }
            catch (Exception ex)
            {
                base.Log(ex);

                return View("Index");
            }
        }

        /// <summary>
        /// Dispalys page for updating EuSanctionList.
        /// </summary>
        /// <param name="listTypeId">Id of the list</param>
        /// <returns>Rendered view.</returns>
        [Route("SanctionLists/{listTypeId:int}/EntityUpdateAuto")]
        public ActionResult EuUpdateList(int listTypeId)
        {
            try
            {
                ViewBag.ListTypeName = base._sanctionListsBll.ListTypesGet().FirstOrDefault(w => w.ListTypeId == listTypeId).Name;
                ViewBag.ListTypeId = listTypeId;
                ViewBag.IsUpdating = "None";

                // Check update status, if someone updated another one just only to see the status.
                AutoUpdateStatus status = HttpContext.Application[ViewBag.ListTypeName + "UpdateStatus"] as AutoUpdateStatus;
                if (status != null)
                {
                    ViewBag.IsUpdating = status.Status;
                }

                return View(ViewBag.ListTypeName + "Autoupdate");
            }
            catch (Exception ex)
            {
                base.Log(ex);

                return View("Index");
            }
        }

        /// <summary>
        /// The action for search page. It displays criteria form.
        /// </summary>
        /// <returns>Search view with anonymous object</returns>
        [OutputCache(Duration = 0)]
        public ActionResult Search()
        {
            ListType[] listTypes = base._sanctionListsBll.ListTypesGet();
            ListType listType = listTypes.FirstOrDefault();
            ViewBag.ListType = JsonConvert.SerializeObject(listTypes);
            ViewBag.SearchEntity = JsonConvert.SerializeObject(new SearchEntity());

            return View();
        }

        /// <summary>
        /// Gets the entity details by entity identifier.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns>ActionResult.</returns>
        [Route("SanctionLists/SearchEntities/{entityId:int}")]
        public ActionResult GetByEntityId(int entityId)
        {
            Entity entity = _sanctionListsBll.EntitiesGetById(entityId);

            var jsonStr = JsonConvert.SerializeObject(entity, Formatting.Indented);

            return Json(jsonStr.Replace("\r\n", "<br>").Replace(" ", "&emsp;&ensp;"), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Exports the specified list type identifier.
        /// </summary>
        /// <param name="listTypeId">The list type identifier.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Export(int listTypeId, string lastName, string firstName = null, DateTime? birthDate = null)
        {
            SearchEntity searchEntity = new SearchEntity
            {
                LastName = lastName,
                FirstName = firstName,
                BirthDate = birthDate,
                SanctionListType = new ListType { ListTypeId = listTypeId }
            };

            Entity[] entities = base._sanctionListsBll.EntitiesGet(searchEntity);
            int records = _sanctionListsBll.EntitiesCount(searchEntity);
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc =
                JsonConvert.DeserializeXmlNode("{\"SanctionList\":" + JsonConvert.SerializeObject(entities) + "}",
                    "root");
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.FirstChild);

            string listTypeName = base._sanctionListsBll.ListTypesGet().FirstOrDefault(w => w.ListTypeId == listTypeId).Name;
            
            var commentString = "\r\nSearch Condition\r\nList Type : " + listTypeName + "\r\nLast Name : " +
                                searchEntity.LastName;
            if (firstName != null)
            {
                commentString = commentString + "\r\nFirst Name : " + searchEntity.FirstName; 
            }
            if (birthDate != null)
            {
                string dt = birthDate.ToString();

                dt = DateTime.Parse(dt).Year + "-" + DateTime.Parse(dt).Month + "-" + DateTime.Parse(dt).Day;
                commentString = commentString + "\r\nDate of Birth : " + dt;
            }

            XmlComment xmlComment =
                xmlDoc.CreateComment(commentString + "\r\nThis document contains [" + records + "] record(s)\r\n");
            xmlDoc.InsertAfter(xmlComment, xmlDoc.FirstChild);

            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + "_SanctionLists.xml";

            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(xmlDoc.InnerXml);

            byte[] file =
                Encoding.UTF8.GetBytes("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
                                       XDocument.Parse(xmlDoc.InnerXml));

            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}