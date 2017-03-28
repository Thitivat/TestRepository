using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.iDeal.Dal.Enums
{
    /// <summary>
    /// Enum EnumBookingStatus contrain all status that will happen on booking process.
    /// </summary>
    public enum EnumBookingStatus
    {
        /// <summary>
        /// The status is not booking yet
        /// </summary>
        NotBooked,
        /// <summary>
        /// The status is in booking process
        /// </summary>
        Booking,
        /// <summary>
        /// The status is already booked to matrix
        /// </summary>
        Booked
    }
}
