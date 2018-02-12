using System;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The event date item extensions.
    /// </summary>
    internal static class EventDateItemExtensions
    {
        /// <summary>
        /// Converts a <paramref name="item"/> to a <see cref="Entities.EventDateItem"/>
        /// </summary>
        /// <typeparam name="T"> The type</typeparam>
        /// <param name="item"> The T item  </param>
        /// <returns>
        /// The <see cref="Entities.EventDateItem"/>.
        /// </returns>
        internal static Entities.EventDateItem ToEventDateItemEntity<T>(this T item) where T : class
        {
            return new Entities.EventDateItem()
            {
                EventDate = (DateTime?)item.GetType().GetProperty("EventDate").GetValue(item),
                EventDateSpecified = (bool)item.GetType().GetProperty("EventDateSpecified").GetValue(item),
                UserId = (int?)item.GetType().GetProperty("UserId").GetValue(item),
                UserIdSpecified = (bool)item.GetType().GetProperty("UserIdSpecified").GetValue(item),
                UserName = (string)item.GetType().GetProperty("UserName").GetValue(item)
            };
        }

        /*/// <summary>
        /// Converts a <see cref="FiveDegrees.CashAccount.EventDateItem"/> to a <see cref="Entities.EventDateItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.CashAccount.EventDateItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.EventDateItem"/>.
        /// </returns>
        internal static Entities.EventDateItem ToEntity(this FiveDegrees.CashAccount.EventDateItem item)
        {
            return new Entities.EventDateItem()
            {
                EventDate = item.EventDate,
                EventDateSpecified = item.EventDateSpecified,
                UserId = item.UserId,
                UserIdSpecified = item.UserIdSpecified,
                UserName = item.UserName
            };
        }*/
    }
}
