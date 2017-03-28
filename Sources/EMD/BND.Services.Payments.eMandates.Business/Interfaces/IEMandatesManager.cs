using BND.Services.Payments.eMandates.Entities;
using BND.Services.Payments.eMandates.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.Business.Interfaces
{
    public interface IEMandatesManager
    {
        DirectoryModel GetDirectory();

        TransactionResponseModel CreateTransaction(NewTransactionModel newTransaction);

        EnumQueryStatus GetTransactionStatus(string transactionId);
    }
}
