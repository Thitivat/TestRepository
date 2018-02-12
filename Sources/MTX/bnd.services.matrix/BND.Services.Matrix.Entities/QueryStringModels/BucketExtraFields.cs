using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BND.Services.Matrix.Entities.Interfaces;
using BND.Services.Matrix.Enums;

namespace BND.Services.Matrix.Entities.QueryStringModels
{
    /// <summary>
    /// The bucket extra fields.
    /// </summary>
    public class BucketExtraFields : IQueryStringModel
    {
        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        public EnumBucketExtraField Fields { get; set; }

    }
}
