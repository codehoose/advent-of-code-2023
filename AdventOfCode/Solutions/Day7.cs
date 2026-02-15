using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Solutions
{
    internal class Day7 : BaseSolution
    {
        protected override void OnRun()
        {
            //Solution1(new string[] { "32T3K 765",
            //                        "T55J5 684",
            //                        "KK677 28",
            //                        "KTJJT 220",
            //                        "QQQJA 483"});   
            Solution1(Args[0].ReadAllLines());
        }

        void Solution1(string[] lines)
        {
            List<CardHand> hands = lines.Select(x => x.StripAndTrim(' '))
                                        .Select(x => new CardHand(x[0], int.Parse(x[1])))
                                        //.OrderBy(x => x.GetRank())
                                        .ToList();
            List<CardHand> bucketedHands = new List<CardHand>();

            Dictionary<HandRanking, List<CardHand>> bucket = new Dictionary<HandRanking, List<CardHand>>();
            EnumExtensions.ToEnumerable<HandRanking>()
                          .ForEach(rank =>
                          {
                              List<CardHand> temp = hands.Where(hand => hand.GetRank() == rank).ToList();
                              Sort(temp);
                              bucketedHands.AddRange(temp);


                              //bucket.Add(rank, new List<CardHand>( ));
                          });

            // BUCKET THEM

            //bool swapped;
            //do
            //{
            //    swapped = false;
            //    for (int i = hands.Count - 1; i > 0; i--)
            //    {
            //        if (hands[i].GetRank() == hands[i - 1].GetRank() && hands[i].IsSmallerThan(hands[i - 1]))
            //        {
            //            CardHand tmp = hands[i];
            //            hands.RemoveAt(i);
            //            hands.Insert(i - 1, tmp);
            //            swapped = true;
            //        }
            //    }
            //} while (swapped);


            int total = 0;
            for (int i = 0; i < bucketedHands.Count; i++)
            {
                total += (i + 1) * bucketedHands[i].Bid;
            }
            WriteLine($"Total: {total}"); // Tried: 249,869,784
                                          //        249,526,299 (too high)  
                                          //        249,661,928
                                          //        249888795
                                          //        249889571
        }

        private static void Sort(List<CardHand> hands)
        {
            for (int i = 1; i < hands.Count; i++)
            {
                CardHand val = hands[i];
                for (int j = i - 1; j >= 0;)
                {
                    if (hands[i].GetRank() == hands[i - 1].GetRank() && hands[i].IsSmallerThan(hands[i - 1]))
                    {
                        hands[j + 1] = hands[j];
                        j--;
                        hands[j + 1] = val;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    internal enum HandRanking
    {
        HighestCard = 0,
        OnePair = 1,
        TwoPair = 2,
        ThreeOfAKind = 3,
        FullHouse = 4,
        FourOfAKind = 5,
        FiveOfAKind = 6
    }

    [DebuggerDisplay("{_hand} {_bid} {HandType}")]
    internal class CardHand
    {
        private readonly string _hand;
        private readonly int _bid;
        private static string CARDTYPES = "23456789TJQKA";
        //private const int FIVE_OF_A_KIND = 6;
        //private const int FOUR_OF_A_KIND = 5;
        //private const int FULL_HOUSE = 4;
        //private const int THREE_OF_A_KIND = 3;
        //private const int TWO_PAIR = 2;
        //private const int ONE_PAIR = 1;
        //private const int HIGHEST_CARD = 0;

        public int Bid => _bid;

        public string HandType
        {
            get
            {
                switch (GetRank())
                {
                    case HandRanking.FiveOfAKind:
                        return "Five of a kind";
                    case HandRanking.FourOfAKind:
                        return "Four of a kind";
                    case HandRanking.FullHouse:
                        return "Full house";
                    case HandRanking.ThreeOfAKind:
                        return "Three of a kind";
                    case HandRanking.TwoPair:
                        return "Two pairs";
                    case HandRanking.OnePair:
                        return "One pair";
                }

                return "Highest Card";
            }
        }

        public bool IsSmallerThan(CardHand card)
        {
            for (int i = 0; i < 5; i++)
            {
                if (GetCardRank(_hand[i]) < GetCardRank(card._hand[i])) return true;
            }

            return false;
        }

        public CardHand(string hand, int bid)
        {
            _hand = hand;
            _bid = bid;
        }


        public HandRanking GetRank()
        {
            if (FiveOfAKind())
                return HandRanking.FiveOfAKind;

            if (XOfAKind(4))
                return HandRanking.FourOfAKind;

            if (XOfAKind(3) && XOfAKind(2))
                return HandRanking.FullHouse;

            if (XOfAKind(3))
                return HandRanking.ThreeOfAKind;

            if (IsTwoPair())
                return HandRanking.TwoPair;

            if (XOfAKind(2))
                return HandRanking.OnePair;

            return HandRanking.HighestCard;
        }


        bool XOfAKind(int x)
        {
            for (int i = 0; i < _hand.Length; i++)
            {
                if (_hand.Count(ch => ch == _hand[i]) == x) return true;
            }

            return false;
        }

        bool IsTwoPair()
        {
            List<char> cards = new List<char>();

            for (int i = 0; i < _hand.Length; i++)
            {
                if (_hand.Count(ch => ch == _hand[i]) == 2 && !cards.Contains(_hand[i])) cards.Add(_hand[i]);
            }

            return cards.Count == 2;
        }

        int HighestCard() // Highest card index - see CARDTYPES;
        {
            return _hand.Select(x => GetCardRank(x))
                        .OrderBy(x => x)
                        .Last();
        }

        bool FiveOfAKind()
        {
            bool isFiveOfAKind = true;
            int firstRank = GetCardRank(_hand[0]);
            for (int i = 1; i < 5; i++)
            {
                isFiveOfAKind &= (GetCardRank(_hand[i]) == firstRank);
            }
            return isFiveOfAKind;
        }


        int GetCardRank(char ch)
        {
            return CARDTYPES.IndexOf(ch);
        }
    }
}
