using System;
using System.Collections.Generic;

namespace Solutions
{
    public class Day1 : BaseSolution
    {
        private static Dictionary<string, string> NAMEMAP = new Dictionary<string, string>()
        {
            { "one", "o1e" },
            {"two", "t2o" },
            {"three", "t3e" },
            {"four","f4r" },
            {"five", "f5e" },
            {"six", "s6x" },
            {"seven", "s7n" },
            {"eight", "e8t" },
            {"nine", "n9e" }
        };

        protected override void OnRun()
        {
            string[] lines = Args[0].ReadAllLines();
            string[] linesNums = ConvertWordsToNumbers(lines);
            string[] digitsOnly = linesNums.StripAlpha();

            int total = 0;
            for(int i = 0; i < digitsOnly.Length; i++)
            {
                string line = digitsOnly[i];
                int lineValue = GetFirstAndLast(line);
                WriteLine($"{lines[i]} -> {linesNums[i]} -> {lineValue}");
                total += lineValue;
            }

            Console.WriteLine($"Total: {total}");
        }

        string[] ConvertWordsToNumbers(string[] str)
        {
            List<string> output = new List<string>();
            foreach (var s in str)
            {
                output.Add(ConvertWordToNumber(s));
            }

            return output.ToArray();
        }

        string ConvertWordToNumber(string str)
        {
            string output = str;
            while (ContainsName(output))
            {
                int lowestPos = output.Length;
                string key = string.Empty;
                //for (int i = 0; i < NAMES.Length; i++)
                foreach(string number in NAMEMAP.Keys)
                {
                    int pos = output.IndexOf(number);
                    if (pos > -1 && pos < lowestPos)
                    {
                        lowestPos = pos;
                        key = number;
                    }
                }

                if (lowestPos < str.Length)
                {
                    output = output.Replace(key, NAMEMAP[key]);
                }
            }

            return output;
        }

        bool ContainsName(string str)
        {
            foreach(var name in NAMEMAP.Keys)
            {
                if (str.Contains(name)) return true;
            }

            return false;
        }

        int GetFirstAndLast(string str)
        {
            if (str.Length == 0)
            {
                return 0;
            }

            int s1 = 10 * (str[0] - '0');
            int s2 = str[str.Length - 1] - '0';
            return s1 + s2;
        }
    }
}
