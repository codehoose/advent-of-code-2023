using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Day3 : BaseSolution
    {
        protected override void OnRun()
        {
            //DoPuzzle("data\\day3\\day3test.txt");
            //DoPuzzle("data\\day3\\input.txt");
            DoPuzzleGears("data\\day3\\input.txt");
        }

        void DoPuzzleGears(string fileName)
        {
            string[] lines = fileName.ReadAllLines(x => x.Replace('.', ' '));
            Dictionary<string, List<int>> possibleGears = new Dictionary<string, List<int>>();

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
                            Tuple<int, int> pos = GetSymbolPosition(lines, numStr, startX, y);

                            string key = $"{pos.Item1}-{pos.Item2}";
                            if (!possibleGears.ContainsKey(key))
                            {
                                possibleGears.Add(key, new List<int>());
                            }

                            possibleGears[key].Add(num);
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
                        Tuple<int, int> pos = GetSymbolPosition(lines, numStr, startX, y);

                        string key = $"{pos.Item1}-{pos.Item2}";
                        if (!possibleGears.ContainsKey(key))
                        {
                            possibleGears.Add(key, new List<int>());
                        }

                        possibleGears[key].Add(num);
                    }
                }
            }

            int total = possibleGears.Values.Where(list => list.Count == 2)
                                            .Sum(list => list[0] * list[1]);   


            WriteLine("\n--- NUMBER SUM ---");
            WriteLine($"{total}");
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

        private Tuple<int, int> GetSymbolPosition(string[] lines, string numStr, int startX, int line)
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
                            return new Tuple<int, int>(x, y);
                        }
                    }
                }
            }

            return new Tuple<int, int>(-1, -1);
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
