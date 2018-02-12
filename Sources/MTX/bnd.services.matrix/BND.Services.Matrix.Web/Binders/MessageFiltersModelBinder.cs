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
    /// The gateway message filters model binder.
    /// </summary>
    public class MessageFiltersModelBinder : BaseModelBinder, IModelBinder
    {
        /// <summary>
        /// Binds the model to a value by using the specified controller context and binding context.
        /// </summary>
        /// <returns>true if model binding is successful; otherwise, false.</returns>
        /// <param name="actionContext">The action context.</param>
        /// <param name="bindingContext">The binding context.</param>
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var filters = GetFiltersModel<MessageFilters>(actionContext, bindingContext);

            if (filters == null) return false;

            if (filters.Direction.HasValue && filters.Direction.Value.ToValues().Any(x => !Enum.IsDefined(typeof(EnumMessageDirections), x)))

            {
                AddModelError<MessageFilters>(bindingContext, "InvalidMessageFiltersDirectionErrorKey", "Invalid values specified for message direction query string parameter!");
            }

            if (filters.Status.HasValue && filters.Status.Value.ToValues().Any(x => !Enum.IsDefined(typeof(EnumMessageStatuses), x)))
            {
                AddModelError<MessageFilters>(bindingContext, "InvalidMessageFiltersStatusErrorKey", "Invalid values specified for message status query string parameter!");
            }

            if (filters.Type.HasValue && filters.Type.Value.ToValues().Any(x => !Enum.IsDefined(typeof(EnumMessageTypes), x)))
            {
                AddModelError<MessageFilters>(bindingContext, "InvalidMessageFiltersTypeErrorKey", "Invalid values specified for message type query string parameter!");
            }

            bindingContext.Model = filters;

            return true;
        }
    }
}