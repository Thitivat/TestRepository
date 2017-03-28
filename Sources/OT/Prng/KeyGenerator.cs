using System;
using System.Security.Cryptography;
using System.Text;

namespace BND.Services.Security.OTP.Prng
{
    /// <summary>
    /// This KeyGenerator class provides function for generate pseudorandom code as digits, characters and keys.
    /// It's use RNGCryptoServiceProvider to generate random keys.
    /// </summary>
    public static class KeyGenerator
    {
        /// <summary>
        /// The allowed digits to generate key.
        /// </summary>
        private static string _allowDigits = "0123456789";
        /// <summary>
        /// The allowed letters to generate key.
        /// </summary>
        private static string _allowLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        /// The allowed symbol to generate key.
        /// </summary>
        private static string _allowSymbol = "-_";

        /// <summary>
        /// This method intended to generate random numbers with specific length by use RNGCryptoServiceProvider.
        /// </summary>
        /// <param name="length">The desired digit length.</param>
        public static string RandomDigits(int length)
        {
            return GenerateKey(length, _allowDigits.ToCharArray());
        }

        /// <summary>
        /// This method intended to generate random characters with specific length by use RNGCryptoServiceProvider..
        /// </summary>
        /// <param name="length">The desired character length.</param>
        public static string RandomChars(int length)
        {
            return GenerateKey(length, _allowLetters.ToCharArray());
        }

        /// <summary>
        /// This method intended to generate random keys with specific length by use RNGCryptoServiceProvider..
        /// </summary>
        /// <param name="length">The desired keys length.</param>
        public static string RandomKey(int length)
        {
            return GenerateKey(length, String.Concat(_allowDigits, _allowLetters, _allowSymbol).ToCharArray());
        }

        /// <summary>
        /// This method intended to generate key by specific length and desired characters.
        /// </summary>
        /// <param name="length">The desired keys length.</param>
        /// <param name="allowedCharacters">The allowed characters.</param>
        private static string GenerateKey(int length, char[] allowedCharacters)
        {
            if (length < 1)
            {
                throw new ArgumentException("The length value must be greater than 1.");
            }

            byte[] data = new byte[length];
            // create random byte array by use RNGCryptoServiceProvider.
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(length);
            // append text.
            foreach (byte b in data)
            {
                result.Append(allowedCharacters[b % allowedCharacters.Length]);
            }
            return result.ToString();
        }
    }
}
