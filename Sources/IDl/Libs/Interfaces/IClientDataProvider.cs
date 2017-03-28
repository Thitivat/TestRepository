using System;
using System.Collections.Generic;
using System.Text;

namespace BND.Services.Payments.iDeal.Interfaces
{
    public interface IClientDataProvider
    {
        string GetClientNameByIban(string iban);
    }
}
