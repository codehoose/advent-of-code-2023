using System;

namespace Solutions
{
    public class Day1 : BaseSolution
    {
        protected override void OnRun()
        {
            string[] lines = Args[0].ReadAllLines();
            string[] digitsOnly = lines.StripAlpha();

            int total = 0;
            foreach (var line in digitsOnly)
            {
                int lineValue = GetFirstAndLast(line);
                WriteLine(lineValue.ToString());
                total += lineValue;
            }

            Console.WriteLine($"Total: {total}");
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
