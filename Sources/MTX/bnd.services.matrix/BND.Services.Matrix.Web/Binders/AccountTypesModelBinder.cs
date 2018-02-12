using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

using BND.Services.Infrastructure.Common.Extensions;
using BND.Services.Matrix.Entities;

namespace BND.Services.Matrix.Web.Binders
{
    /// <summary>
    /// The account types model binder.
    /// </summary>
    public class AccountTypesModelBinder : BaseModelBinder, IModelBinder
    {
        /// <summary>
        /// Binds the model to a value by using the specified controller context and binding context.
        /// </summary>
        /// <param name="actionContext">The <see cref="HttpActionContext"/>.</param>
        /// <param name="bindingContext">The <see cref="ModelBindingContext"/></param>
        /// <returns>
        /// true if model binding is successful or false if model binding is unsuccessful.
        /// </returns>
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var valueProviderResult = GetValueProviderResult<AccountTypeList>(actionContext, bindingContext);

            // No fields specified
            if (valueProviderResult == null) return false;

            try
            {
                var validModel = bindingContext.ModelState.IsValid;

                if (validModel)
                {
                    var rawValueString = valueProviderResult.RawValue as string;

                    var fields = new AccountTypeList();
                    bindingContext.Model = fields;
                    
                }

                return validModel;
            }
            catch (Exception ex)
            {
                AddModelError<AccountTypeList>(bindingContext, "InvalidExtraFieldsErrorKey", Resources.Web.Common.ModelValidationFilterAttributeErrorMessage, ex);

                return false;
            }
        }
    }
}