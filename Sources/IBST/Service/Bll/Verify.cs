using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BND.Services.IbanStore.Service.Bll
{
    public static class Verify
    {
        #region [Public Methods]
        /// <summary>
        /// This class implement function to check file format of Bban file and return result as a collection of bban number.
        /// For the beginning, the system accept Bban file in type of .csv
        /// </summary>
        /// <param name="bbanFile">The Bban in type of byte array.</param>
        /// <returns>collection of bban number.</returns>
        /// <exception cref="System.ArgumentNullException">bbanFile</exception>
        /// <exception cref="BND.Services.IbanStore.Service.IbanOperationException">3001;Invalid BBAN file format.</exception>
        public static IEnumerable<int> CheckFileFormat(byte[] bbanFile)
        {
            // check bban data must not null and length must not equal 0.
            if (bbanFile == null || bbanFile.Length == 0)
            {
                throw new ArgumentNullException("bbanFile");
            }

            var result = new List<int>();

            try
            {
                using (var memStream = new MemoryStream(bbanFile))
                {
                    using (var reader = new StreamReader(memStream, Encoding.UTF8))
                    {
                        string line;
                        int lineNumber = 1;

                        // read line by line.
                        while ((line = reader.ReadLine()) != null)
                        {
                            // try parse the line to integer.
                            int bban;
                            if (!Int32.TryParse(line, out bban))
                            {
                                // if line is not integer will throw exception with message.
                                throw new InvalidCastException(String.Format("Line:{0}, Data:{1}", lineNumber, line));
                            }

                            // add bban value to result.
                            result.Add(bban);
                            lineNumber++;
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new IbanOperationException(3001,string.Format( "Invalid BBAN file format. :{0}",ex.Message), ex);
            }
        }

        /// <summary>
        /// This class implement function to validate Bban by using algorithm divide by 11.
        /// </summary>
        /// <param name="bban">The Bban in type of integer.</param>
        /// <returns><c>true</c> response true if valid data, <c>false</c> response false if  invalid data.</returns>
        /// <exception cref="IbanOperationException">3002;Invalid BBAN format</exception>
        public static bool CheckBBan11Proof(int bban)
        {
            try
            {
                // Check bban value should over than zero
                if (bban <= 0)
                {
                    throw new ArgumentException("BBAN is less than or equal zero", "bban");
                }

                // Check length of Bban, it should be 9.
                if ((int)(Math.Log10(decimal.ToDouble(bban)) + 1) != 9)
                {
                    throw new ArgumentException(String.Format("BBAN: {0} length shoule be 9", bban), "bban");
                }

                // Set variable for 11 proof calculation
                Decimal sum = 0;
                int weight = 9;

                // Loop one by one digit of Bban
                foreach (char item in bban.ToString())
                {
                    // Set digit of Bban multiple to weight
                    var result = int.Parse(item.ToString()) * weight;
                    // Remove weight by one
                    weight--;

                    // Sum value of calculation
                    sum += result;
                }

                // Find round number by sum devide by 11
                Decimal roundNo = sum / 11;

                // Check round number
                if (roundNo % 1 != 0)
                {
                    throw new ArgumentException(String.Format("BBAN: {0} is not round number", bban), "bban");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new IbanOperationException(3002, String.Format("Invalid BBAN format, {0}.", ex.Message), ex);
            }
        }

        /// <summary>
        /// This private class implement function hash. This class will use by private method "IsBbanFileDuplicate".
        /// </summary>
        /// <param name="bbanFile">The Bban in type of byte array.</param>
        /// <returns>Hash result</returns>
        public static string CreateHash(byte[] bbanFile)
        {
            using (var sha = SHA256.Create())
            {
                byte[] computedHash = sha.ComputeHash(bbanFile);
                return Convert.ToBase64String(computedHash);
            }
        }
        #endregion
    }
}
