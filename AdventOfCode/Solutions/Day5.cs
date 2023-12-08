using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    internal class Day5 : BaseSolution
    {
        string[] maps = new string[]
        {
            "seed-to-soil",
            "soil-to-fertilizer",
            "fertilizer-to-water",
            "water-to-light",
            "light-to-temperature",
            "temperature-to-humidity",
            "humidity-to-location"
        };

        protected override void OnRun()
        {
            Solution2();
        }

        void Solution2()
        {
            string[] lines = "data\\day5\\input.txt".ReadAllLines();
            Planting planting = new Planting(lines);
            long[] values = planting.Seeds.ToArray();
            long lowest = long.MaxValue;
            long seed = 0;
            long originalSeed = 0;

            for (int i = 0; i < values.Length; i += 2)
            {
                long start = values[i];
                long length = values[i + 1];

                for (long j = start; j < start + length; j++)
                {
                    long percent = j / (start + length) * 100;
                    Console.Write($"Seed Loop #{1 + i / 2},  %complete {percent}                \r");

                    seed = j;
                    long value = seed;
                    foreach (var key in maps)
                    {
                        value = GetMappedValue(value, planting.Ranges[key]);
                    }

                    if (value < lowest)
                    {
                        lowest = value;
                        originalSeed = seed;
                    }
                }
            }

            WriteLine($"{originalSeed} = {lowest}");
        }

        private long GetMappedValue(long seed, List<PlantRange> ranges)
        {
            var possibleRanges = ranges.FirstOrDefault(r => r.IsInRange(seed));
            if (possibleRanges == null) return seed;
            return possibleRanges.GetDestination(seed);
        }

        private static void GetMappedValues(Planting planting, List<Tuple<long, long>> mappings, string key, long seed)
        {
            var ranges = planting.Ranges[key].Where(r => r.IsInRange(seed)).ToArray();
            if (ranges.Length != 1)
            {
                mappings.Add(new Tuple<long, long>(seed, seed));
            }
            else
            {
                mappings.Add(new Tuple<long, long>(seed, ranges[0].GetDestination(seed)));
            }
        }

        void Solution1()
        {
            string[] lines = "data\\day5\\input.txt".ReadAllLines();
            Planting planting = new Planting(lines);

            long[] values = planting.Seeds.ToArray();

            List<Tuple<long, long>> mappings = new List<Tuple<long, long>>();
            foreach (var key in maps)
            {
                foreach (var seed in values)
                {
                    var ranges = planting.Ranges[key].Where(r => r.IsInRange(seed)).ToArray();
                    if (ranges.Length != 1)
                    {
                        mappings.Add(new Tuple<long, long>(seed, seed));
                    }
                    else
                    {
                        mappings.Add(new Tuple<long, long>(seed, ranges[0].GetDestination(seed)));
                    }
                }

                values = mappings.Select(x => x.Item2).ToArray();

                if (key != "humidity-to-location")
                    mappings.Clear();
            }

            long[] orderedList = mappings.Select(x => x.Item2).OrderBy(x => x).ToArray();
            WriteLine($"Smallest range {orderedList[0]}");
        }
    }

    internal class Planting
    {
        public List<long> Seeds { get; private set; } = new List<long>();

        public Dictionary<string, List<PlantRange>> Ranges { get; private set; } = new Dictionary<string, List<PlantRange>>();

        public Planting(string[] lines)
        {
            int i = 0;
            while (i < lines.Length)
            {
                string line = lines[i];
                if (line.StartsWith("seeds:"))
                {
                    Seeds.AddRange(line.Substring("seeds: ".Length)
                                       .StripAndTrim(' ')
                                       .Select(x => long.Parse(x)));
                    i++;
                }
                else if (line.EndsWith(" map:"))
                {
                    string title = line.Replace(" map:", "").Trim();
                    Ranges.Add(title, new List<PlantRange>());
                    i++;
                    do
                    {
                        long[] rangeLine = lines[i].StripAndTrim(' ')
                                                   .Select(x => long.Parse(x))
                                                   .ToArray();

                        PlantRange range = new PlantRange(rangeLine[1], rangeLine[0], rangeLine[2]);
                        Ranges[title].Add(range);
                        i++;
                    } while (i < lines.Length && !lines[i].EndsWith(" map:"));
                }
            }
        }
    }

    internal class PlantRange
    {
        public long SourceStart { get; private set; }

        public long DestinationStart { get; private set; }

        public long Length { get; private set; }

        public bool IsInRange(long number)
        {
            long offset = number - SourceStart;
            if (offset < 0 || offset > Length) return false;
            return true;
        }

        public long GetDestination(long number)
        {
            if (!IsInRange(number)) return number;
            long offset = number - SourceStart;
            return DestinationStart + offset;
        }

        public PlantRange(long source, long destination, long length)
        {
            SourceStart = source;
            DestinationStart = destination;
            Length = length;
        }
    }
}
