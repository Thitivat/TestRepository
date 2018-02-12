using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using BND.Services.Infrastructure.Common.Interfaces;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Web.Binders;

namespace BND.Services.Matrix.Web.BinderProviders
{
    /// <summary>
    /// The account types model binder provider.
    /// </summary>
    public class AccountTypesModelBinderProvider : ModelBinderProvider, IModelBinderProvider
    {
        /// <summary>
        /// Finds a binder for the given type.
        /// </summary>
        /// <returns>
        /// A binder, which can attempt to bind this type. Or null if the binder knows statically that it will never be able to bind the type.
        /// </returns>
        /// <param name="configuration">A configuration object.</param><param name="modelType">The type of the model to bind against.</param>
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return modelType == typeof(AccountTypeList) ? new AccountTypesModelBinder() : null;
        }
    }
}