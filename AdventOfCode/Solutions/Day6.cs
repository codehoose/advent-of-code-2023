using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    internal class Day6 : BaseSolution
    {
        protected override void OnRun()
        {
            Solution1(Args[0].ReadAllLines());
        }

        void Solution1(string[] lines)
        {
            int total = 1;
            List<TimeAndDistance> result = new List<TimeAndDistance>();
            for (int i = 0; i < lines.Length; i += 2)
            {
                int[] times = GetNumbers(lines[i]);
                int[] distance = GetNumbers(lines[i + 1]);

                for (int j = 0; j < times.Length; j++)
                {
                    var temp = new TimeAndDistance(times[j], distance[j]);
                    total *= temp.NumberOfWins;
                }
            }
            WriteLine($"{total}");
        }

        int[] GetNumbers(string line)
        {
            return line.StripAndTrim(' ')
                       .Select(x => int.TryParse(x, out int output) ? output : -1)
                       .Where(x => x > 0)
                       .ToArray();
        }
    }

    internal class TimeAndDistance
    {
        public int Time { get; private set; }

        public int Distance { get; private set; }

        public int NumberOfWins
        {
            get
            {
                int total = 0;
                for (int i = 0; i <= Time; i++)
                {
                    int value = i * (Time - i);
                    total = value > Distance ? total + 1 : total;
                }

                return total;
            }

        }

        public TimeAndDistance(int time, int distance)
        {
            Time = time;
            Distance = distance;
        }
    }
}
