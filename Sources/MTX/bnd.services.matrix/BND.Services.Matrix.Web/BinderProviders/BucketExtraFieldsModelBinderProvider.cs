using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using BND.Services.Infrastructure.Common.Interfaces;
using BND.Services.Matrix.Entities.QueryStringModels;
using BND.Services.Matrix.Web.Binders;

namespace BND.Services.Matrix.Web.BinderProviders
{
    /// <summary>
    /// The bucket extra fields model binder provider.
    /// </summary>
    public class BucketExtraFieldsModelBinderProvider : ModelBinderProvider, IModelBinderProvider
    {
        /// <summary>
        /// Finds a binder for the given type.
        /// </summary>
        /// <param name="configuration">A <paramref name="configuration"/> object.</param>
        /// <param name="modelType">The type of the model to bind against.</param>
        /// <returns>
        /// A binder, which can attempt to bind this type. Or null if the binder knows statically that it will never be able to bind the type.
        /// </returns>
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return modelType == typeof(BucketExtraFields) ? new BucketExtraFieldsModelBinder() : null;
        }
    }
}