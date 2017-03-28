using System;
using System.Collections.Generic;

namespace BND.Services.IbanStore.ApiTest
{
    public class IbanComparer : IEqualityComparer<Models.Iban>
    {
        public bool Equals(Models.Iban x, Models.Iban y)
        {
            return x.BankCode == y.BankCode &&
                   x.Bban == y.Bban &&
                   x.CheckSum == y.CheckSum &&
                   x.Code == y.Code &&
                   x.CountryCode == y.CountryCode &&
                   IbanHistoryEquals(x.CurrentIbanHistory, y.CurrentIbanHistory);
        }

        public int GetHashCode(Models.Iban obj)
        {
            return obj.GetHashCode();
        }

        private bool IbanHistoryEquals(Models.IbanHistory x, Models.IbanHistory y)
        {
            return (x == null && y == null) ||
                   (x.ChangedBy == y.ChangedBy &&
                    DateTime.Compare(x.ChangedDate, y.ChangedDate) == 0 &&
                    x.Context == y.Context &&
                    x.Id == y.Id &&
                    x.Remark == y.Remark &&
                    x.Status == y.Status);
        }
    }
}
