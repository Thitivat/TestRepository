using System;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

using BND.Services.Infrastructure.Common.Extensions;
using BND.Services.Matrix.Entities.QueryStringModels;
using BND.Services.Matrix.Enums;

namespace BND.Services.Matrix.Web.Binders
{
    /// <summary>
    /// The bucket filter model binder.
    /// </summary>
    public class BucketFilterModelBinder : BaseModelBinder, IModelBinder
    {
        /// <summary>
        /// Binds the model to a value by using the specified controller context and binding context.
        /// </summary>
        /// <returns>
        /// true if model binding is successful; otherwise, false.
        /// </returns>
        /// <param name="actionContext">The action context.</param><param name="bindingContext">The binding context.</param>
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var filters = GetFiltersModel<BucketItemFilters>(actionContext, bindingContext);

            if (filters == null) return false;

            if (filters.Source.HasValue && filters.Source.Value.ToValues().Any(x => !Enum.IsDefined(typeof(EnumBucketItemSources), x)))
            {
                AddModelError<MessageFilters>(bindingContext, "InvalidBucketItemFiltersDirectionErrorKey", "Invalid values specified for bucket source query string parameter!");
            }

            if (filters.Operation.HasValue && filters.Operation.Value.ToValues().Any(x => !Enum.IsDefined(typeof(EnumBucketItemOperations), x)))
            {
                AddModelError<MessageFilters>(bindingContext, "InvalidBucketItemFiltersTypeErrorKey", "Invalid values specified for bucket operation query string parameter!");
            }

            if (filters.Status.HasValue && filters.Status.Value.ToValues().Any(x => !Enum.IsDefined(typeof(EnumBucketItemStatuses), x)))
            {
                AddModelError<MessageFilters>(bindingContext, "InvalidBucketItemFiltersStatusErrorKey", "Invalid values specified for bucket status query string parameter!");
            }

            if (filters.Type.HasValue && filters.Type.Value.ToValues().Any(x => !Enum.IsDefined(typeof(EnumBucketItemTypes), x)))
            {
                AddModelError<MessageFilters>(bindingContext, "InvalidBucketItemFiltersTypeErrorKey", "Invalid values specified for bucket type query string parameter!");
            }

            bindingContext.Model = filters;

            return true;
        }
    }
}