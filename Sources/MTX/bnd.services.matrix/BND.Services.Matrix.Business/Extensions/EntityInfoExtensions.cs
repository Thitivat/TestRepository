namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The entity info extensions.
    /// </summary>
    internal static class EntityInfoExtensions
    {
        /// <summary>
        /// Converts a <paramref name="item"/> to a <see cref="Entities.EntityInfoItem"/>
        /// </summary>
        /// <typeparam name="T"> The type</typeparam>
        /// <param name="item"> The T item </param>
        /// <returns>
        /// The <see cref="Entities.EntityInfoItem"/>.
        /// </returns>
        internal static Entities.EntityInfoItem ToEntityInfoItemEntity<T>(this T item) where T : class
        {
            return new Entities.EntityInfoItem()
            {
                Created = item.GetType().GetProperty("Created").GetValue(item).ToEventDateItemEntity(),
                Changed = item.GetType().GetProperty("Changed").GetValue(item).ToEventDateItemEntity()
            };
        }

        /*/// <summary>
        /// Converts a <see cref="FiveDegrees.CashAccount.EntityInfoItem"/> to a <see cref="Entities.EntityInfoItem"/>
        /// </summary>
        /// <param name="item"> The <see cref="FiveDegrees.CashAccount.EntityInfoItem"/>. </param>
        /// <returns>
        /// The <see cref="Entities.EntityInfoItem"/>.
        /// </returns>
        internal static Entities.EntityInfoItem ToEntity(this FiveDegrees.CashAccount.EntityInfoItem item)
        {
            return new Entities.EntityInfoItem()
            {
                Created = item.Created.ToEntity(),
                Changed = item.Changed.ToEntity()
            };
        }*/
    }
}
