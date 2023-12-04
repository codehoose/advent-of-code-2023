using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions
{
    public class Day4 : BaseSolution
    {
        protected override void OnRun()
        {
            int total = 0;
            Args[0].ReadAllLines()
                   .Select(line => Deserialize(line))
                   .ForEach(card =>
                   {
                       WriteLine($"Card #{card.ID} score {card.Score}");
                       total += card.Score;
                   });

            WriteLine($"Total: {total}");
        }

        Card Deserialize(string line)
        {
            string[] lineSplit = line.StripAndTrim(':');
            int gameId = int.Parse(lineSplit[0].Substring("Game ".Length).Trim());
            Card card = new Card(gameId);
            string[] sides = lineSplit[1].StripAndTrim('|');
            card.Winning.AddRange(Parse(sides[0]));
            card.Numbers.AddRange(Parse(sides[1]));
            return card;
        }

        IEnumerable<int> Parse(string line)
        {
            return line.StripAndTrim(' ')
                       .Select(x => int.Parse(x));
        }

    }

    internal class Card
    {
        public int ID { get; private set; }

        public int Score
        {
            get
            {
                int score = 0;
                foreach (var num in Numbers)
                {
                    if (Winning.Contains(num))
                    {
                        score++;
                    }
                }

                if (score == 0) return 0;

                return (int)Math.Pow(2, score - 1);
            }
        }

        public List<int> Winning { get; private set; } = new List<int>();

        public List<int> Numbers { get; private set; } = new List<int>();

        public Card(int id)
        {
            ID = id;
        }
    }
}
