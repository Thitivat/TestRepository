
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace BND.Services.IbanStore.Models
{
    /// <summary>
    /// Class IBan.
    /// </summary>
    public class Iban
    {
        #region [Filed]

        private readonly Dictionary<string, int> _letterNumber = new Dictionary<string, int>()
        {
            {"A", 10},
            {"B", 11},
            {"C", 12},
            {"D", 13},
            {"E", 14},
            {"F", 15},
            {"G", 16}, 
            {"H", 17}, 
            {"I", 18},
            {"J", 19},
            {"K", 20},
            {"L", 21},
            {"M", 22},
            {"N", 23},
            {"O", 24},
            {"P", 25},
            {"Q", 26},
            {"R", 27},
            {"S", 28},
            {"T", 29},
            {"U", 30},
            {"V", 31},
            {"W", 32},
            {"X", 33},
            {"Y", 34},
            {"Z", 35}
        };

        #endregion

        #region [Properties]
        /// <summary>
        /// Gets or sets the name of the bban file.
        /// </summary>
        /// <value>The name of the bban file.</value>
        public string BbanFileName { get; set; }

        /// <summary>
        /// Gets or sets the iban identifier.
        /// </summary>
        /// <value>The iban identifier.</value>
        public int IbanId { get; set; }

        /// <summary>
        /// Gets or sets the uid.
        /// </summary>
        /// <value>The uid.</value>
        public string Uid { get; set; }

        /// <summary>
        /// Gets or sets the uid prefix.
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
        public string Code
        {
            get
            {
                return String.Format("{0}{1}{2}{3:D10}", CountryCode, CheckSum, BankCode, Convert.ToInt32(Bban));
            }
        }

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

        #endregion

        #region [Constructor]

        /// <summary>
        /// Initializes a new instance of the <see cref="Iban"/> class.
        /// </summary>
        /// <param name="countrycode">The countrycode.</param>
        /// <param name="bankCode">The bank code.</param>
        /// <param name="bban">The bban.</param>
        public Iban(string countrycode, string bankCode, string bban)
        {
            CountryCode = countrycode;
            BankCode = bankCode;
            Bban = bban;
            CheckSum = CalculateCheckSum();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Iban"/> class.
        /// </summary>
        /// <param name="countrycode">The countrycode.</param>
        /// <param name="bankCode">The bank code.</param>
        /// <param name="bban">The bban.</param>
        /// <param name="checkSum">The check sum.</param>
        public Iban(string countrycode, string bankCode, string bban, string checkSum)
        {
            CountryCode = countrycode;
            BankCode = bankCode;
            Bban = bban;
            CheckSum = checkSum.ToString().Length == 1 ? string.Format("0{0}", checkSum) : checkSum.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Iban"/> class.
        /// </summary>
        /// <param name="ibanString">The iban string.</param>
        public Iban(string ibanString)
        {
            // call for validate iban string if invlid will throw exception.
            IbanValidate(ibanString);
            // call to split iban string after validate.
            IbanSplit(ibanString);
        }

        #endregion

        #region [Private Methods]

        /// <summary>
        /// Calculates the check sum.
        /// </summary>
        /// <returns>System.String.</returns>
        private string CalculateCheckSum()
        {
            // Set Bank code, Country code and BBAN to be string
            string bbanCharacter = string.Format("{0}0{1}{2}", BankCode, Bban, CountryCode).ToUpper();
            string bbanNumber = bbanCharacter;

            // Loop character of set of Bank code, Country code and BBAN
            foreach (char item in bbanCharacter)
            {
                // Find number value from English letter
                if (_letterNumber.ContainsKey(item.ToString()))
                {
                    // Set value to variable
                    int letterNumber = _letterNumber[item.ToString()];

                    // Replace character with value
                    bbanNumber = bbanNumber.Replace(item.ToString(), letterNumber.ToString());
                }
            }

            // Add two zeros at the end of character
            bbanNumber = string.Format("{0}00", bbanNumber);

            // Convert all calculate number from string to type of BigInteger
            BigInteger bbanCalculate = BigInteger.Parse(bbanNumber);

            // Mode calculate number with 97
            BigInteger rest = bbanCalculate % 97;

            // Minus the result with 98
            BigInteger checkSum = 98 - rest;

            // Return check sum value
            return string.Format("{0:D2}", checkSum);
        }

        /// <summary>
        /// Ibans the split.
        /// </summary>
        /// <param name="ibanString">The iban string.</param>
        private void IbanSplit(string ibanString)
        {
            // XX (Country code) YY (Check sum) ZZZZ (Bank code) 0 BBAN 9 digits (number only)

            CountryCode = ibanString.Substring(0, 2).ToUpper();
            CheckSum = ibanString.Substring(2, 2);
            BankCode = ibanString.Substring(4, 4);
            Bban = ibanString.Substring(9, 9);
        }

        /// <summary>
        /// Ibans the validate.
        /// </summary>
        /// <param name="iban">The iban.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static void IbanValidate(string iban)
        {
            try
            {
                // Normalize Iban by remove space and set all to upper case
                iban = iban.Replace(" ", string.Empty).ToUpper();

                // Get country code
                string countryCode = iban.Substring(0, 2);

                // Get country rule by country code that get from Iban
                // Check that the total IBAN length is correct as per the country. If not, the IBAN is invalid
                IbanCountryRule countryRule = GetCountryRules().FirstOrDefault(f => f.CountryCode == countryCode);

                // If cannot be found any country rule by country code then throw
                if (countryRule == null)
                {
                    throw new ArgumentException("Invalid IBAN country.");
                }

                // Check IBAN length
                if (iban.Length != countryRule.IbanLength)
                {
                    throw new ArgumentException("Invalid IBAN country length.");
                }

                // Check general IBAN structure by using RegEx
                if (!Regex.IsMatch(iban.Remove(0, 4), countryRule.RegEx))
                {
                    throw new ArgumentException("Invalid IBAN Structure.");
                }

                // Move 4 initial characters to the end of iban string
                // Replace each letter in the string with 2 digits, thereby expanding the string, where A=10, B=11, ..., Z=35.
                string ibanFormat = iban.Substring(4) + iban.Substring(0, 4);
                ibanFormat = Regex.Replace(ibanFormat, @"\D", r => (r.Value[0] - 55).ToString());

                // Interpret the string as a decimal integer and compute the remainder of that number on division by 97
                int remainder = 0;
                while (ibanFormat.Length >= 7)
                {
                    remainder = int.Parse(remainder + ibanFormat.Substring(0, 7)) % 97;
                    ibanFormat = ibanFormat.Substring(7);
                }  

                remainder = int.Parse(remainder + ibanFormat) % 97;

                if (remainder != 1)
                {
                    throw new ArgumentException("Invalid IBAN");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }

        /// <summary>
        /// Gets the country rules.
        /// </summary>
        /// <returns>List&lt;IbanCountryRule&gt;.</returns>
        private static List<IbanCountryRule> GetCountryRules()
        {
            return new List<IbanCountryRule>
            {
                new IbanCountryRule("AD", 24, @"\d{8}[a-zA-Z0-9]{12}", "AD1200012030200359100100"),
                new IbanCountryRule("AL", 28, @"\d{8}[a-zA-Z0-9]{16}", "AL47212110090000000235698741"),
                new IbanCountryRule("AT", 20, @"\d{16}", "AT611904300234573201"),
                new IbanCountryRule("BA", 20, @"\d{16}", "BA391290079401028494"),
                new IbanCountryRule("BE", 16, @"\d{12}", "BE68539007547034"),
                new IbanCountryRule("BG", 22, @"[A-Z]{4}\d{6}[a-zA-Z0-9]{8}", "BG80BNBG96611020345678"),
                new IbanCountryRule("CH", 21, @"\d{5}[a-zA-Z0-9]{12}", "CH9300762011623852957"),
                new IbanCountryRule("CY", 28, @"\d{8}[a-zA-Z0-9]{16}", "CY17002001280000001200527600"),
                new IbanCountryRule("CZ", 24, @"\d{20}", "CZ6508000000192000145399"),
                new IbanCountryRule("DE", 22, @"\d{18}", "DE89370400440532013000"),
                new IbanCountryRule("DK", 18, @"\d{14}", "DK5000400440116243"),
                new IbanCountryRule("EE", 20, @"\d{16}", "EE382200221020145685"),
                new IbanCountryRule("ES", 24, @"\d{20}", "ES9121000418450200051332"),
                new IbanCountryRule("FI", 18, @"\d{14}", "FI2112345600000785"),
                new IbanCountryRule("FO", 18, @"\d{14}", "FO6264600001631634"),
                new IbanCountryRule("FR", 27, @"\d{10}[a-zA-Z0-9]{11}\d\d", "FR1420041010050500013M02606"),
                new IbanCountryRule("GB", 22, @"[A-Z]{4}\d{14}", "GB29NWBK60161331926819"),
                new IbanCountryRule("GI", 23, @"[A-Z]{4}[a-zA-Z0-9]{15}", "GI75NWBK000000007099453"),
                new IbanCountryRule("GL", 18, @"\d{14}", "GL8964710001000206"),
                new IbanCountryRule("GR", 27, @"\d{7}[a-zA-Z0-9]{16}", "GR1601101250000000012300695"),
                new IbanCountryRule("HR", 21, @"\d{17}", "HR1210010051863000160"),
                new IbanCountryRule("HU", 28, @"\d{24}", "HU42117730161111101800000000"),
                new IbanCountryRule("IE", 22, @"[A-Z]{4}\d{14}", "IE29AIBK93115212345678"),
                new IbanCountryRule("IL", 23, @"\d{19}", "IL620108000000099999999"),
                new IbanCountryRule("IS", 26, @"\d{22}", "IS140159260076545510730339"),
                new IbanCountryRule("IT", 27, @"[A-Z]\d{10}[a-zA-Z0-9]{12}", "IT60X0542811101000000123456"),
                new IbanCountryRule("LB", 28, @"\d{4}[a-zA-Z0-9]{20}", "LB62099900000001001901229114"),
                new IbanCountryRule("LI", 21, @"\d{5}[a-zA-Z0-9]{12}", "LI21088100002324013AA"),
                new IbanCountryRule("LT", 20, @"\d{16}", "LT121000011101001000"),
                new IbanCountryRule("LU", 20, @"\d{3}[a-zA-Z0-9]{13}", "LU280019400644750000"),
                new IbanCountryRule("LV", 21, @"[A-Z]{4}[a-zA-Z0-9]{13}", "LV80BANK0000435195001"),
                new IbanCountryRule("MC", 27, @"\d{10}[a-zA-Z0-9]{11}\d\d", "MC1112739000700011111000h79"),
                new IbanCountryRule("ME", 22, @"\d{18}", "ME25505000012345678951"),
                new IbanCountryRule("MK", 19, @"\d{3}[a-zA-Z0-9]{10}\d\d", "MK07300000000042425"),
                new IbanCountryRule("MT", 31, @"[A-Z]{4}\d{5}[a-zA-Z0-9]{18}", "MT84MALT011000012345MTLCAST001S"),
                new IbanCountryRule("MU", 30, @"[A-Z]{4}\d{19}[A-Z]{3}", "MU17BOMM0101101030300200000MUR"),
                new IbanCountryRule("NL", 18, @"[A-Z]{4}\d{10}", "NL91ABNA0417164300"),
                new IbanCountryRule("NO", 15, @"\d{11}", "NO9386011117947"),
                new IbanCountryRule("PL", 28, @"\d{8}[a-zA-Z0-9]{16}", "PL27114020040000300201355387"),
                new IbanCountryRule("PT", 25, @"\d{21}", "PT50000201231234567890154"),
                new IbanCountryRule("RO", 24, @"[A-Z]{4}[a-zA-Z0-9]{16}", "RO49AAAA1B31007593840000"),
                new IbanCountryRule("RS", 22, @"\d{18}", "RS35260005601001611379"),
                new IbanCountryRule("SA", 24, @"\d{2}[a-zA-Z0-9]{18}", "SA0380000000608010167519"),
                new IbanCountryRule("SE", 24, @"\d{20}", "SE4550000000058398257466"),
                new IbanCountryRule("SI", 19, @"\d{15}", "SI56191000000123438"),
                new IbanCountryRule("SK", 24, @"\d{20}", "SK3112000000198742637541"),
                new IbanCountryRule("SM", 27, @"[A-Z]\d{10}[a-zA-Z0-9]{12}", "SM86U0322509800000000270100"),
                new IbanCountryRule("TN", 24, @"\d{20}", "TN5914207207100707129648"),
                new IbanCountryRule("TR", 26, @"\d{5}[a-zA-Z0-9]{17}", "TR330006100519786457841326")
            };
        }

        #endregion
    }
}
