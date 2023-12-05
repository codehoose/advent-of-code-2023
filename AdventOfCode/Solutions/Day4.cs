using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions
{
    public class Day4 : BaseSolution
    {
        protected override void OnRun()
        {
            //Part1();
            Part2();
        }

        void Part1()
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

        void Part2()
        {
            List<Card> cards = Args[0].ReadAllLines()
                                      .Select(line => Deserialize(line))
                                      .ToList();
            List<Card> winners = GetWinners(cards);
            int total = cards.Sum(c => c.Winners); // top-level winners
            do
            {
                total += winners.Sum(c => c.Winners);
                winners = GetWinners(winners);
            }
            while (winners.Count >= 0);

            WriteLine($"Total: {total}");
        }

        List<Card> GetWinners(List<Card> cards)
        {
            List<Card> winners = new List<Card>();
            cards.ForEach(card =>
            {
                if (card.Winners > 0)
                {
                    for (int i = card.ID; i <= card.ID + card.Winners - 1; i++)
                    {
                        winners.Add(cards[i]);
                    }
                }
            });

            return winners;
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

    [DebuggerDisplay("{ID} - {Winners}")]
    internal class Card
    {
        public int ID { get; private set; }

        public int Winners
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

                return score;
            }
        }

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
