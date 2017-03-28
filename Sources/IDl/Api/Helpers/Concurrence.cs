using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BND.Services.Payments.iDeal.Api.Helpers
{
    /// <summary>
    /// Class Concurrence is a helper class for solving concurrent issue.
    /// </summary>
    public static class Concurrence
    {
        /// <summary>
        /// The lock object for using in lock statement.
        /// </summary>
        public static readonly object lockObj = new object();
    }
}