using System;
using System.Collections.Generic;
using System.Text;
using BND.Services.Payments.iDeal.Models;

namespace BND.Services.Payments.iDeal.Interfaces
{
    public interface IBookingManager
    {
        /// <summary>
        /// Books to matrix.
        /// </summary>
        /// <param name="bookingInfo">The booking information.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="transactonId"></param>
        /// <param name="entranceCode"></param>
        /// <returns>System.Int32.</returns>
        int BookToMatrix(BookingModel bookingInfo, string accessToken, string transactonId, string entranceCode);
    }
}
