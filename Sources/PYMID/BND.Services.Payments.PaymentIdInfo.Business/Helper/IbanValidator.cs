using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BND.Services.Payments.PaymentIdInfo.Business.Helper
{
    /// <summary>
    /// Class IbanValidator.
    /// </summary>
    public class IbanValidator
    {
        /// <summary>
        /// Validates the specified ibanstring.
        /// </summary>
        /// <param name="ibanstring">The ibanstring.</param>
        /// <returns><c>true</c> if iban is valid, <c>false</c> otherwise.</returns>
        public static bool Validate(string ibanstring)
        {
            IBANValidationData validationdata = ValidateWithDetails(ibanstring);

            return validationdata.IsValid;
        }
        /// <summary>
        /// Determines whether the specified ibanstring is dutch.
        /// </summary>
        /// <param name="ibanstring">The ibanstring.</param>
        /// <returns><c>true</c> if the specified ibanstring is dutch; otherwise, <c>false</c>.</returns>
        public static bool IsDutch(string ibanstring)
        {
            string iban = ibanstring.Replace(" ", string.Empty).ToUpper();
            string countrycode = iban.Substring(0, 2);

            return countrycode == "NL";
        }
        /// <summary>
        ///     Some countries are not allowed by AFM.
        /// </summary>
        public static bool BlackListed(string ibanstring)
        {
            string iban = ibanstring.Replace(" ", string.Empty).ToUpper();
            string countrycode = iban.Substring(0, 2);

            // Switzerland / Monaco
            return countrycode == "CH" || countrycode == "MC";
        }
        //http://en.wikipedia.org/wiki/IBAN
        //1. Check that the total IBAN length is correct as per the country. If not, the IBAN is invalid
        //2. Move the four initial characters to the end of the string
        //3. Replace each letter in the string with two digits, thereby expanding the string, where A = 10, B = 11, ..., Z = 35
        //4. Interpret the string as a decimal integer and compute the remainder of that number on division by 97
        public static IBANValidationData ValidateWithDetails(string ibaninputstring)
        {
            try
            {
                //normalize
                string iban = ibaninputstring.Replace(" ", string.Empty).ToUpper();

                string countrycode = iban.Substring(0, 2);

                //1. Check that the total IBAN length is correct as per the country. If not, the IBAN is invalid
                IBANCountryRule countryrule = GetCountryRules().First(x => x.CountryCode == countrycode);

                if (countryrule == null)
                {
                    return new IBANValidationData(false, countrycode,
                        string.Format("Landspecifieke regels kon niet worden opgehaald! {0}", countrycode));
                }

                //check iban length
                if (iban.Length != countryrule.IBANLength)
                {
                    return new IBANValidationData(false, countrycode,
                        string.Format(
                            "IBAN lengte is niet correct. Geleverd IBAN lengte: {0}. Toegestaan: {1}",
                            iban.Length, countryrule.IBANLength));
                }

                //check general iban structure using regex
                if (!Regex.IsMatch(iban.Remove(0, 4), countryrule.RegEx)) // check country specific structure
                {
                    return new IBANValidationData(false, countrycode, "Iban structuur is niet correct");
                }

                //2- Move the four initial characters to the end of the string.
                //3- Replace each letter in the string with two digits, thereby expanding the string, where A=10, B=11, ..., Z=35. 
                string ibanreformatted = iban.Substring(4) + iban.Substring(0, 4);
                ibanreformatted = Regex.Replace(ibanreformatted, @"\D", m => (m.Value[0] - 55).ToString());

                //4. Interpret the string as a decimal integer and compute the remainder of that number on division by 97
                int remainder = 0;
                while (ibanreformatted.Length >= 7)
                {
                    remainder = int.Parse(remainder + ibanreformatted.Substring(0, 7)) % 97;
                    ibanreformatted = ibanreformatted.Substring(7);
                }

                remainder = int.Parse(remainder + ibanreformatted) % 97;

                if (remainder != 1)
                    return new IBANValidationData
                    {
                        IsValid = false,
                        ValidationMessage = "IBAN is niet correct"
                    };

                return new IBANValidationData(true, countrycode, "IBAN is correct");
            }
            catch (Exception)
            {
                return new IBANValidationData
                {
                    IsValid = false,
                    ValidationMessage = "IBAN is niet correct"
                };
            }
        }

        public static List<IBANCountryRule> GetCountryRules()
        {
            var countryrules = new List<IBANCountryRule>
            {
                new IBANCountryRule("AD", 24, @"\d{8}[a-zA-Z0-9]{12}", "AD1200012030200359100100"),
                new IBANCountryRule("AL", 28, @"\d{8}[a-zA-Z0-9]{16}", "AL47212110090000000235698741"),
                new IBANCountryRule("AT", 20, @"\d{16}", "AT611904300234573201"),
                new IBANCountryRule("BA", 20, @"\d{16}", "BA391290079401028494"),
                new IBANCountryRule("BE", 16, @"\d{12}", "BE68539007547034"),
                new IBANCountryRule("BG", 22, @"[A-Z]{4}\d{6}[a-zA-Z0-9]{8}", "BG80BNBG96611020345678"),
                new IBANCountryRule("CH", 21, @"\d{5}[a-zA-Z0-9]{12}", "CH9300762011623852957"),
                new IBANCountryRule("CY", 28, @"\d{8}[a-zA-Z0-9]{16}", "CY17002001280000001200527600"),
                new IBANCountryRule("CZ", 24, @"\d{20}", "CZ6508000000192000145399"),
                new IBANCountryRule("DE", 22, @"\d{18}", "DE89370400440532013000"),
                new IBANCountryRule("DK", 18, @"\d{14}", "DK5000400440116243"),
                new IBANCountryRule("EE", 20, @"\d{16}", "EE382200221020145685"),
                new IBANCountryRule("ES", 24, @"\d{20}", "ES9121000418450200051332"),
                new IBANCountryRule("FI", 18, @"\d{14}", "FI2112345600000785"),
                new IBANCountryRule("FO", 18, @"\d{14}", "FO6264600001631634"),
                new IBANCountryRule("FR", 27, @"\d{10}[a-zA-Z0-9]{11}\d\d", "FR1420041010050500013M02606"),
                new IBANCountryRule("GB", 22, @"[A-Z]{4}\d{14}", "GB29NWBK60161331926819"),
                new IBANCountryRule("GI", 23, @"[A-Z]{4}[a-zA-Z0-9]{15}", "GI75NWBK000000007099453"),
                new IBANCountryRule("GL", 18, @"\d{14}", "GL8964710001000206"),
                new IBANCountryRule("GR", 27, @"\d{7}[a-zA-Z0-9]{16}", "GR1601101250000000012300695"),
                new IBANCountryRule("HR", 21, @"\d{17}", "HR1210010051863000160"),
                new IBANCountryRule("HU", 28, @"\d{24}", "HU42117730161111101800000000"),
                new IBANCountryRule("IE", 22, @"[A-Z]{4}\d{14}", "IE29AIBK93115212345678"),
                new IBANCountryRule("IL", 23, @"\d{19}", "IL620108000000099999999"),
                new IBANCountryRule("IS", 26, @"\d{22}", "IS140159260076545510730339"),
                new IBANCountryRule("IT", 27, @"[A-Z]\d{10}[a-zA-Z0-9]{12}", "IT60X0542811101000000123456"),
                new IBANCountryRule("LB", 28, @"\d{4}[a-zA-Z0-9]{20}", "LB62099900000001001901229114"),
                new IBANCountryRule("LI", 21, @"\d{5}[a-zA-Z0-9]{12}", "LI21088100002324013AA"),
                new IBANCountryRule("LT", 20, @"\d{16}", "LT121000011101001000"),
                new IBANCountryRule("LU", 20, @"\d{3}[a-zA-Z0-9]{13}", "LU280019400644750000"),
                new IBANCountryRule("LV", 21, @"[A-Z]{4}[a-zA-Z0-9]{13}", "LV80BANK0000435195001"),
                new IBANCountryRule("MC", 27, @"\d{10}[a-zA-Z0-9]{11}\d\d", "MC1112739000700011111000h79"),
                new IBANCountryRule("ME", 22, @"\d{18}", "ME25505000012345678951"),
                new IBANCountryRule("MK", 19, @"\d{3}[a-zA-Z0-9]{10}\d\d", "MK07300000000042425"),
                new IBANCountryRule("MT", 31, @"[A-Z]{4}\d{5}[a-zA-Z0-9]{18}", "MT84MALT011000012345MTLCAST001S"),
                new IBANCountryRule("MU", 30, @"[A-Z]{4}\d{19}[A-Z]{3}", "MU17BOMM0101101030300200000MUR"),
                new IBANCountryRule("NL", 18, @"[A-Z]{4}\d{10}", "NL91ABNA0417164300"),
                new IBANCountryRule("NO", 15, @"\d{11}", "NO9386011117947"),
                new IBANCountryRule("PL", 28, @"\d{8}[a-zA-Z0-9]{16}", "PL27114020040000300201355387"),
                new IBANCountryRule("PT", 25, @"\d{21}", "PT50000201231234567890154"),
                new IBANCountryRule("RO", 24, @"[A-Z]{4}[a-zA-Z0-9]{16}", "RO49AAAA1B31007593840000"),
                new IBANCountryRule("RS", 22, @"\d{18}", "RS35260005601001611379"),
                new IBANCountryRule("SA", 24, @"\d{2}[a-zA-Z0-9]{18}", "SA0380000000608010167519"),
                new IBANCountryRule("SE", 24, @"\d{20}", "SE4550000000058398257466"),
                new IBANCountryRule("SI", 19, @"\d{15}", "SI56191000000123438"),
                new IBANCountryRule("SK", 24, @"\d{20}", "SK3112000000198742637541"),
                new IBANCountryRule("SM", 27, @"[A-Z]\d{10}[a-zA-Z0-9]{12}", "SM86U0322509800000000270100"),
                new IBANCountryRule("TN", 24, @"\d{20}", "TN5914207207100707129648"),
                new IBANCountryRule("TR", 26, @"\d{5}[a-zA-Z0-9]{17}", "TR330006100519786457841326")
            };

            return countryrules;
        }

        public class IBANCountryRule
        {
            public IBANCountryRule()
            {
            }

            public IBANCountryRule(string countrycode, int ibanlength, string regex, string sample)
            {
                CountryCode = countrycode;
                IBANLength = ibanlength;
                RegEx = regex;
                Sample = sample;
            }

            public string CountryCode { get; set; }
            public int IBANLength { get; set; }
            public string RegEx { get; set; }
            public string Sample { get; set; }
        }

        public class IBANValidationData
        {
            public IBANValidationData()
            {
            }

            public IBANValidationData(bool isvalid, string countrycode, string validationmessage)
            {
                IsValid = isvalid;
                CountryCode = countrycode;
                ValidationMessage = validationmessage;
            }

            public bool IsValid { get; set; }
            public string CountryCode { get; set; }
            public string ValidationMessage { get; set; }

            public IBANCountryRule CountryRule
            {
                get
                {
                    IBANCountryRule countryrule = null;

                    if (!string.IsNullOrEmpty(CountryCode))
                    {
                        countryrule =
                            IbanValidator.GetCountryRules().First(x => x.CountryCode == CountryCode);
                    }

                    return countryrule;
                }
            }
        }
    }
}
