using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

using BND.Services.Infrastructure.Common.Extensions;
using BND.Services.Matrix.Entities.QueryStringModels;
using BND.Services.Matrix.Enums;

namespace BND.Services.Matrix.Web.Binders
{
    /// <summary>
    /// The bucket extra fields model binder.
    /// </summary>
    public class BucketExtraFieldsModelBinder : BaseModelBinder, IModelBinder
    {
        /// <summary>
        /// The bind model.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        /// <param name="bindingContext">
        /// The binding context.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var valueProviderResult = GetValueProviderResult<BucketExtraFields>(actionContext, bindingContext);

            // No fields specified
            if (valueProviderResult == null) return false;

            try
            {
                var validModel = bindingContext.ModelState.IsValid;

                if (validModel)
                {
                    var rawValueString = valueProviderResult.RawValue as string;
                    var enumList = rawValueString.ParseEnum<EnumBucketExtraField>().ToValues();

                    var fields = new BucketExtraFields();
                    foreach (var field in enumList)
                    {
                        fields.Fields = fields.Fields | field;
                    }

                    bindingContext.Model = fields;
                }

                return validModel;
            }
            catch (Exception ex)
            {
                AddModelError<BucketExtraFields>(bindingContext, "InvalidExtraFieldsErrorKey", Resources.Web.Common.ModelValidationFilterAttributeErrorMessage, ex);

                return false;
            }
        }
    }
}