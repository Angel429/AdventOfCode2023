using System.Collections.ObjectModel;

namespace Day7
{
    internal class Hand : IComparable<Hand>
    {
        private static readonly ReadOnlyDictionary<char, int> _cardsValues;

        static Hand()
        {
            _cardsValues = new ReadOnlyDictionary<char, int>(new Dictionary<char, int>
            {
                { '2', 0 },
                { '3', 1 },
                { '4', 2 },
                { '5', 3 },
                { '6', 4 },
                { '7', 5 },
                { '8', 6 },
                { '9', 7 },
                { 'T', 8 },
                { 'J', 9 },
                { 'Q', 10 },
                { 'K', 11 },
                { 'A', 12 }
            });
        }

        public readonly string Cards;
        public readonly int Bid;

        public Hand(string cards, int bid)
        {
            Cards = cards;
            Bid = bid;
        }

        public int CompareTo(Hand other)
        {
            var xValues = new Dictionary<char, int>(Cards.Length);
            var yValues = new Dictionary<char, int>(other.Cards.Length);

            foreach (var card in Cards)
            {
                var currentCount = xValues.GetValueOrDefault(card);
                xValues[card] = currentCount + 1;
            }

            foreach (var card in other.Cards)
            {
                var currentCount = yValues.GetValueOrDefault(card);
                yValues[card] = currentCount + 1;
            }

            var orderedCountOfXCards = xValues.Values.OrderDescending().ToList();
            var orderedCountOfYCards = yValues.Values.OrderDescending().ToList();

            for (int i = 0; i < orderedCountOfXCards.Count; i++)
            {
                if (orderedCountOfXCards[i] > orderedCountOfYCards[i])
                {
                    return 1;
                }

                if (orderedCountOfXCards[i] < orderedCountOfYCards[i])
                {
                    return -1;
                }
            }

            for (int i = 0; i < Cards.Length; i++)
            {
                var xCardValue = _cardsValues[Cards[i]];
                var yCardValue = _cardsValues[other.Cards[i]];

                if (xCardValue > yCardValue)
                {
                    return 1;
                }

                if (xCardValue < yCardValue)
                {
                    return -1;
                }
            }

            return 0;
        }
    }
}
