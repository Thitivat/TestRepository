
using System.Collections.Generic;
using System.Linq;

using BND.Services.Infrastructure.Common.Extensions;

namespace BND.Services.Matrix.Business.Extensions
{
    internal static class ReturnBucketExtensions
    {
        internal static FiveDegrees.PaymentService.ReturnBucketRowItem ToMatrixModel(this Entities.ReturnBucketRowItem item)
        {
            return new FiveDegrees.PaymentService.ReturnBucketRowItem
            {
                RowId = item.RowId,
                ReturnCode = item.SepaReturnCode.ParseEnum<FiveDegrees.PaymentService.SepaReturnCodes>()
            };
        }

        internal static FiveDegrees.PaymentService.ReturnBucketRowItem[] ToMatrixModels(this List<Entities.ReturnBucketRowItem> items)
        {
            return items.Select(item => item.ToMatrixModel()).ToArray();
        }

        internal static FiveDegrees.PaymentService.CreateReturnBucketItem ToMatrixModel(this Entities.ReturnBucketItem item)
        {
            return new FiveDegrees.PaymentService.CreateReturnBucketItem
            {
                Source = item.Source.ToString().ParseEnum<FiveDegrees.PaymentService.PaymentSources>(),
                OperationCode = item.OperationCode.ToString().ParseEnum<FiveDegrees.PaymentService.BankOperationCodes>(),
                ReturnBucketId = item.ReturnBucketId,
                BucketRowIds = item.ReturnBucketRowItems.ToMatrixModels()
            };
        }
    }
}

