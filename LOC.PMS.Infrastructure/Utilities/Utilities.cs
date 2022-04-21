using System;
using System.Collections.Generic;
using System.Text;

namespace LOC.PMS.Infrastructure.Utilities
{
    class Utilities
    {
        private static Random RNG = new Random();

        private static HashSet<string> Results = new HashSet<string>();

        public string CreateUnique16DigitString()
        {
            var result = Create16DigitString();
            while (!Results.Add(result))
            {
                result = Create16DigitString();
            }

            return result;
        }

        private string Create16DigitString()
        {
            var builder = new StringBuilder();
            while (builder.Length < 15)
            {
                builder.Append(RNG.Next(10).ToString());
            }
            return builder.ToString();
        }

    }
}
