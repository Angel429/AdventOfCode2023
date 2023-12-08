using System.Collections.ObjectModel;

namespace Day7
{
    internal class Hand : IComparable<Hand>
    {
        public readonly string Cards;
        public readonly int Bid;
        private readonly bool JacksAreJokers;
        private readonly ReadOnlyDictionary<char, int> CardsValues;

        public Hand(string cards, int bid, bool jacksAreJokers)
        {
            Cards = cards;
            Bid = bid;
            JacksAreJokers = jacksAreJokers;
            CardsValues = new ReadOnlyDictionary<char, int>(new Dictionary<char, int>
            {
                { '2', 2 },
                { '3', 3 },
                { '4', 4 },
                { '5', 5 },
                { '6', 6 },
                { '7', 7 },
                { '8', 8 },
                { '9', 9 },
                { 'T', 10 },
                { 'J', jacksAreJokers ? 1 : 11 },
                { 'Q', 12 },
                { 'K', 13 },
                { 'A', 14 }
            });
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

            if (JacksAreJokers && xValues.ContainsKey('J'))
            {
                var cardsWithoutJack = xValues.Where(x => x.Key != 'J');
                
                if (cardsWithoutJack.Any())
                {
                    var maxCard = cardsWithoutJack.MaxBy(x => x.Value).Key;
                    xValues[maxCard] += xValues['J'];
                    xValues.Remove('J');
                } else
                {
                    xValues['A'] = xValues['J'];
                    xValues.Remove('J');
                }
            }

            foreach (var card in other.Cards)
            {
                var currentCount = yValues.GetValueOrDefault(card);
                yValues[card] = currentCount + 1;
            }

            if (JacksAreJokers && yValues.ContainsKey('J'))
            {
                var cardsWithoutJack = yValues.Where(x => x.Key != 'J');

                if (cardsWithoutJack.Any())
                {
                    var maxCard = cardsWithoutJack.MaxBy(x => x.Value).Key;
                    yValues[maxCard] += yValues['J'];
                    yValues.Remove('J');
                }
                else
                {
                    yValues['A'] = yValues['J'];
                    yValues.Remove('J');
                }
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
                var xCardValue = CardsValues[Cards[i]];
                var yCardValue = CardsValues[other.Cards[i]];

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
