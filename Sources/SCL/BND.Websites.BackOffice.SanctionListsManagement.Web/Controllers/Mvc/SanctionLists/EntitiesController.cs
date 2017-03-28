using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BND.Websites.BackOffice.SanctionListsManagement.Entities;
using Newtonsoft.Json;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Controllers.Mvc.SanctionLists
{
    /// <summary>
    /// Logic connected to entites directly
    /// </summary>
    public class EntitiesController : SanctionListsBaseController
    {
        /// <summary>
        /// Entity add form
        /// </summary>
        /// <param name="listTypeId">Id of the list that entity will belong to</param>
        /// <returns>Rendered view</returns>
        [HttpGet]
        [Route("SanctionLists/{listTypeId:int}/EntityAdd")]
        public ActionResult EntityAdd(int listTypeId, int? entityId)
        {
            return EntityAddEdit(false, listTypeId, entityId);
        }

        /// <summary>
        /// Entity edit form
        /// </summary>
        /// <param name="listTypeId">Id of the list that entity belongs to</param>
        /// <param name="entityId">Id of the entity to edit.</param>
        /// <returns>Rendered view</returns>
        [HttpGet]
        [Route("SanctionLists/{listTypeId:int}/EntityEdit/{entityId}")]
        public ActionResult EntityEdit(int listTypeId, int? entityId)
        {
            return EntityAddEdit(true, listTypeId, entityId);
        }

        /// <summary>
        /// Displays view for add and edit entity.
        /// </summary>
        /// <param name="editMode"></param>
        /// <param name="listTypeId"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        private ActionResult EntityAddEdit(bool editMode, int listTypeId, int? entityId)
        {
            try
            {
                // Check if allowed
                Setting setting = base._sanctionListsBll.SettingsGet(listTypeId, "ManualUpdate");
                int manualUpdate = setting == null ? 0 : Convert.ToInt16(setting.Value);

                if (manualUpdate == 0)
                {
                    // Response.StatusCode = 404;
                    // return View();
                    throw new HttpException(404, "");
                }

                if (editMode)
                {
                    if (entityId != null)
                    {
                        Entity entity = base._sanctionListsBll.EntitiesGetById((int)entityId);

                        if (entity != null)
                        {
                            ViewBag.Entity = JsonConvert.SerializeObject(entity);
                        }
                        else
                        {
                            throw new HttpException(404, "");
                        }
                    }
                    else
                    {
                        throw new HttpException(404, "");
                    } 
                }
                else
                {
                    ViewBag.Entity = JsonConvert.SerializeObject(new Entity());
                }

                ViewBag.ListTypeId = listTypeId;

                ViewBag.ListTypeName = base._sanctionListsBll.ListTypesGet().FirstOrDefault(w => w.ListTypeId == listTypeId).Name;
                ViewBag.Countries = JsonConvert.SerializeObject(base._sanctionListsBll.CountriesGet());
                ViewBag.IdentificationTypes = JsonConvert.SerializeObject(base._sanctionListsBll.IdentificationTypesGet());
                ViewBag.SubjectTypes = JsonConvert.SerializeObject(base._sanctionListsBll.SubjectTypesGet());
                ViewBag.Regulations = JsonConvert.SerializeObject(base._sanctionListsBll.RegulationsGet(Convert.ToInt32(listTypeId)));
                ViewBag.ContactInfoTypes = JsonConvert.SerializeObject(base._sanctionListsBll.ContactInfoTypesGet());
                ViewBag.Genders = JsonConvert.SerializeObject(base._sanctionListsBll.GendersGet());
                ViewBag.Statuses = JsonConvert.SerializeObject(base._sanctionListsBll.StatusesGet());
                ViewBag.Languages = JsonConvert.SerializeObject(base._sanctionListsBll.LanguagesGet());
            }
            catch (HttpException)
            {
                throw;
            }
            catch (Exception ex)
            {
                base.Log(ex);
            }
            
            return View("EntityAdd");
        }


    }
}