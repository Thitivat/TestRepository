namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The operation type item extensions.
    /// </summary>
    internal static class OperationTypeExtensions
    {
        /// <summary>
        /// Converts a <see cref="FiveDegrees.CashAccount.OperationTypeItem"/> to a <see cref="Entities.OperationType"/>
        /// </summary>
        /// <param name="cashAccountOperationTypeItem"> The <see cref="FiveDegrees.CashAccount.OperationTypeItem"/> </param>
        /// <returns>
        /// The <see cref="Entities.OperationType"/>.
        /// </returns>
        internal static Entities.OperationType ToEntity(this FiveDegrees.CashAccount.OperationTypeItem cashAccountOperationTypeItem)
        {
            return new Entities.OperationType()
            {
                Id = cashAccountOperationTypeItem.Id,
                IdSpecified = cashAccountOperationTypeItem.IdSpecified,
                Name = cashAccountOperationTypeItem.Name,
                Description = cashAccountOperationTypeItem.Description
            };
        }

        /// <summary>
        /// Converts a <see cref="Entities.OperationType"/> to a <see cref="FiveDegrees.CashAccount.OperationTypeItem"/>
        /// </summary>
        /// <param name="entityOperationTypeItem"> The <see cref="Entities.OperationType"/>. </param>
        /// <returns>
        /// The <see cref="FiveDegrees.CashAccount.OperationTypeItem"/>.
        /// </returns>
        internal static FiveDegrees.CashAccount.OperationTypeItem ToMatrixModel(this Entities.OperationType entityOperationTypeItem)
        {
            return new FiveDegrees.CashAccount.OperationTypeItem()
            {
                Id = entityOperationTypeItem.Id,
                IdSpecified = entityOperationTypeItem.IdSpecified,
                Name = entityOperationTypeItem.Name,
                Description = entityOperationTypeItem.Description
            };
        }
    }
}
