using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Solutions
{
    internal static class StringExtensions
    {
        public static string[] StripAndTrim(this string str, char ch)
        {
            return StripAndTrim(str, new char[] { ch });
        }

        public static string[] StripAndTrim(this string str, char[] separators)
        {
            return str.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                      .Select(x => x.Trim())
                      .Where(x => !string.IsNullOrEmpty(x))
                      .ToArray();
        }

        public static string[] StripAlpha(this string[] strings)
        {
            List<string> output = new List<string>();
            foreach (string s in strings)
            {
                output.Add(StripAlpha(s));
            }

            return output.ToArray();
        }

        public static string StripAlpha(this string str)
        {
            Regex reg = new Regex(@"\d");
            MatchCollection matches = reg.Matches(str);
            StringBuilder sb = new StringBuilder();
            foreach (Match match in matches)
            {
                sb.Append(match.Value);
            }

            return sb.ToString();
        }
    }
}
