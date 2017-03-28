using BND.Services.Security.OTP.Prng;
using NUnit.Framework;
using System;

namespace BND.Services.Security.OTP.PrngTest
{
    [TestFixture]
    public class KeyGeneratorTest
    {
        private static int _length = 6;
        private static int _count = 500000;
        private static double _ratio = 0.5;
        private static string[] _items = new string[_count];
        [Test]
        public void Test_RandomDigits_Success()
        {
            string result;
            int time = Convert.ToInt32(((double)(Math.Pow(10, _length)) * _ratio));
            for (int i = 0; i < time; i++)
            {
                result = KeyGenerator.RandomDigits(_length);
                _items[i] = result;
            }
        }

        [Test]
        public void Test_RandomChars_Success()
        {
            string result;
            int time = Convert.ToInt32(((double)(Math.Pow(10, _length)) * _ratio));
            for (int i = 0; i < time; i++)
            {
                result = KeyGenerator.RandomChars(_length);
                _items[i] = result;
            }
        }

        [Test]
        public void Test_RandomKey_Success()
        {
            string result;
            int time = Convert.ToInt32(((double)(Math.Pow(10, _length)) * _ratio));
            for (int i = 0; i < time; i++)
            {
                result = KeyGenerator.RandomKey(_length);
                _items[i] = result;
            }
        }

        [Test]
        public void Test_RandomDigits_Fail_invalidLength()
        {
            Assert.Throws<ArgumentException>(() => KeyGenerator.RandomDigits(-1));
        }
    }
}
