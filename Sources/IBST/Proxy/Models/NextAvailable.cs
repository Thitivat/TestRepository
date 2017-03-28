using BND.Services.IbanStore.Models;
using System;

namespace BND.Services.IbanStore.Proxy.Models
{
    /// <summary>
    /// Class NextAvailable.
    /// </summary>
   public class NextAvailable
   {
       /// <summary>
       /// The name of BBAN file which was uploaded to IbanStore.
       /// </summary>
       /// <value>The name of the bban file.</value>
       public string BbanFileName { get; set; }
       /// <summary>
       /// IBAN identifier of record.
       /// </summary>
       /// <value>The iban identifier.</value>
       public int IBanId { get; set; }

       /// <summary>
       /// Unique identifier for hooking up to client.
       /// </summary>
       /// <value>The uid.</value>
       public string Uid { get; set; }

       /// <summary>
       /// Application name for grouping unique identifier.
       /// </summary>
       /// <value>The uid prefix.</value>
       public string UidPrefix { get; set; }

       /// <summary>
       /// Gets or sets the reserved time.
       /// </summary>
       /// <value>The reserved time.</value>
       public DateTime? ReservedTime { get; set; }

       /// <summary>
       /// Gets the code.
       /// </summary>
       /// <value>The code.</value>
       public string Code { get; set; }

       /// <summary>
       /// CountryCode is must 2 digit ex. "NL"
       /// </summary>
       /// <value>The country code.</value>
       public string CountryCode { get; set; }

       /// <summary>
       /// checksum is response of calculation digit this property is call method CalculateCheckSum
       /// </summary>
       /// <value>The check sum.</value>
       public string CheckSum { get; private set; }

       /// <summary>
       /// BankCode ex. BNDA
       /// </summary>
       /// <value>The bank code.</value>
       public string BankCode { get; set; }

       /// <summary>
       /// Bban Code is must have 9 digit
       /// </summary>
       /// <value>The b ban.</value>
       public string Bban { get; set; }

       /// <summary>
       /// Gets or sets the current iban history.
       /// </summary>
       /// <value>The current iban history.</value>
       public IbanHistory CurrentIbanHistory { get; set; }
    }
}
