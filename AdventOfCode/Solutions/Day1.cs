using System;
using System.Collections.Generic;

namespace Solutions
{
    public class Day1 : BaseSolution
    {
        private static string[] NAMES = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

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
                int index = -1;
                for (int i = 0; i < NAMES.Length; i++)
                {
                    int pos = output.IndexOf(NAMES[i]);
                    if (pos > -1 && pos < lowestPos)
                    {
                        lowestPos = pos;
                        index = i;
                    }
                }

                if (lowestPos < str.Length)
                {
                    output = output.Replace(NAMES[index], (index + 1).ToString());
                }
            }

            return output;
        }

        bool ContainsName(string str)
        {
            foreach(var name in NAMES)
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
