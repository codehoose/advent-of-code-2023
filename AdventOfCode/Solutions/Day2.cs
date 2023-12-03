using System.Collections.Generic;

namespace Solutions
{
    public class Day2 : BaseSolution
    {
        protected override void OnRun()
        {
            Solution2();
        }

        void Solution2()
        {
            List<Game> games = Deserialize(Args[0]);
            Set required = new Set(12, 13, 14);
            int total = 0;
            foreach (var g in games)
            {
                var min = g.Minimum;
                var power = min.Power;
                total += power;
            }

            WriteLine($"Total: {total}");
        }

        void Solution1()
        {
            List<Game> games = Deserialize(Args[0]);
            Set required = new Set(12, 13, 14);
            int total = 0;
            foreach (var g in games)
            {
                bool possible = true;

                foreach (var s in g.Sets)
                {
                    possible &= IsPossible(s, required);
                }

                if (possible)
                {
                    WriteLine($"{g.ID}");
                    total += g.ID;
                }
            }

            WriteLine($"Total: {total}");
        }

        bool IsPossible(Set set1, Set set2)
        {
            if (set1.Red == 0 && set1.Green == 0 && set1.Blue == 0) return false;
            return set1.Red <= set2.Red && set1.Green <= set2.Green && set1.Blue <= set2.Blue;
        }

        List<Game> Deserialize(string filePath)
        {
            List<Game> games = new List<Game>();
            string[] lines = filePath.ReadAllLines();

            foreach (string line in lines)
            {
                string[] game = line.StripAndTrim(':');
                string[] sets = game[1].StripAndTrim(';');
                List<Set> setList = DeserializeSets(sets);
                int gameId = int.Parse(game[0].Substring("Game ".Length));
                Game gameActual = new Game(gameId);
                gameActual.Sets.AddRange(setList);
                games.Add(gameActual);

            }

            return games;
        }

        List<Set> DeserializeSets(string[] sets)
        {
            List<Set> output = new List<Set>();
            foreach (string set in sets)
            {
                output.Add(DeserializeSet(set));
            }

            return output;
        }

        Set DeserializeSet(string set)
        {
            int red = 0;
            int green = 0;
            int blue = 0;
            string[] colours = set.StripAndTrim(',');

            foreach (string colour in colours)
            {
                int number = int.Parse(colour.Substring(0, colour.IndexOf(" ")));

                if (colour.EndsWith("red"))
                {
                    red += number;
                }
                else if (colour.EndsWith("green"))
                {
                    green += number;
                }
                else if (colour.EndsWith("blue"))
                {
                    blue += number;
                }
            }

            return new Set(red, green, blue);
        }
    }

    internal class Game
    {
        public int ID { get; private set; }

        public Set Totals {
            get
            {
                int red = 0, blue = 0, green = 0;
                foreach (var set in Sets)
                {
                    red += set.Red;
                    green += set.Green;
                    blue += set.Blue;
                }

                return new Set(red, green, blue);
            } 
        }

        public Set Minimum
        {
            get
            {
                int red = int.MinValue, green = int.MinValue, blue = int.MinValue;
                foreach (var set in Sets)
                {
                    if (set.Red > red) red = set.Red;
                    if (set.Green > green) green = set.Green;
                    if (set.Blue > blue) blue = set.Blue;
                }

                return new Set(red, green, blue);
            }
        }

        public List<Set> Sets { get; private set; } = new List<Set>();

        internal Game(int id)
        {
            ID = id;
        }
    }

    internal class Set
    {
        public int Red { get; set; }

        public int Green { get; set; }

        public int Blue { get; set; }

        public int Power => Red * Green * Blue;

        public Set(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
    }
}
