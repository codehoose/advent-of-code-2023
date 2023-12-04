using System;
using System.Collections.Generic;

namespace Solutions
{
    public class Day3 : BaseSolution
    {
        protected override void OnRun()
        {
            DoPuzzle("data\\day3\\day3test.txt");
            DoPuzzle("data\\day3\\input.txt");
        }

        void DoPuzzle(string fileName)
        {
            string[] lines = fileName.ReadAllLines(x=> x.Replace('.', ' '));
            int sumOfPartNumbers = 0;

            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];
                string numStr = "";
                int startX = -1;
                for (int x = 0; x < line.Length; x++)
                {
                    if (char.IsDigit(line[x]))
                    {
                        if (startX < 0) startX = x;
                        numStr += line[x];
                    }
                    else if (!char.IsDigit(line[x]) && numStr.Length > 0)
                    {
                        int num = int.Parse(numStr);
                        if (TouchesSymbol(lines, numStr, startX, y))
                        {
                            sumOfPartNumbers += num;
                        }
                        startX = -1;
                        numStr = "";
                    }
                }

                if (numStr.Length > 0)
                {
                    int num = int.Parse(numStr);
                    if (TouchesSymbol(lines, numStr, startX, y))
                    {
                        sumOfPartNumbers += num;
                    }
                }
            }

            WriteLine("\n--- NUMBER SUM ---");
            WriteLine($"{sumOfPartNumbers}");
        }

        private bool TouchesSymbol(string[] lines, string numStr, int startX, int line)
        {
            int sx = startX - 1;
            int sy = line - 1;

            for (int y = sy; y < sy + 3; y++)
            {
                for (int x = sx; x < sx + numStr.Length + 2; x++)
                {
                    if (y >= 0 && y < lines.Length)
                    {
                        string testLine = lines[y];
                        if (x >= 0 && x < lines[y].Length && IsSymbol(testLine[x]))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        bool IsSymbol(char c)
        {
            return c > 32 && c < 127 && !char.IsDigit(c) && !char.IsLetter(c);
        }
    }
}
