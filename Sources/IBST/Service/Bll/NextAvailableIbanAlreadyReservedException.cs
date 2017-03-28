using BND.Services.IbanStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.IbanStore.Service.Bll
{
    public class NextAvailableIbanAlreadyReservedException : Exception
    {
        /// <summary>
        /// Gets the reserved iban.
        /// </summary>
        /// <value>The reserved iban.</value>
        public Iban ReservedIban { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NextAvailableIbanAlreadyReservedException"/> class.
        /// </summary>
        /// <param name="iban">The iban.</param>
        public NextAvailableIbanAlreadyReservedException(Iban iban)
            : this(iban, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NextAvailableIbanAlreadyReservedException"/> class.
        /// </summary>
        /// <param name="iban">The iban.</param>
        /// <param name="message">The message.</param>
        public NextAvailableIbanAlreadyReservedException(Iban iban, string message)
            : this(iban, message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NextAvailableIbanAlreadyReservedException"/> class.
        /// </summary>
        /// <param name="iban">The iban.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public NextAvailableIbanAlreadyReservedException(Iban iban, string message, Exception innerException)
            :base(message, innerException)
        {
            ReservedIban = iban;
        }

    }
}
