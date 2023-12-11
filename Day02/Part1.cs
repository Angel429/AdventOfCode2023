using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Day02
{
    static internal class Part1
    {
        private static readonly ReadOnlyDictionary<string, int> _maxColorAmounts;

        static Part1()
        {
            _maxColorAmounts = new ReadOnlyDictionary<string, int>(new Dictionary<string, int>
            {
                { "red", 12 },
                { "green", 13 },
                { "blue", 14 }
            });
        }

        public static void Execute()
        {
            var sum = 0;
            var gameRegex = new Regex("Game (?<gameId>\\d+): (?<games>.*)", RegexOptions.Compiled);
            var colorsRegex = new Regex("(?<amount>\\d+) (?<color>.*)", RegexOptions.Compiled);

            foreach (var line in File.ReadLines("input.txt"))
            {
                var gameMatch = gameRegex.Match(line);
                var isGamePossible = true;

                foreach (var game in gameMatch.Groups["games"].Value.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    foreach (var colorGroup in game.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                    {
                        var colorsMatch = colorsRegex.Match(colorGroup);
                        var color = colorsMatch.Groups["color"].Value;
                        var amount = int.Parse(colorsMatch.Groups["amount"].Value);

                        if (amount > _maxColorAmounts[color])
                        {
                            isGamePossible = false;
                            break;
                        }
                    }

                    if (!isGamePossible)
                    {
                        break;
                    }
                }

                if (isGamePossible)
                {
                    sum += int.Parse(gameMatch.Groups["gameId"].Value);
                }
            }

            Console.WriteLine(sum);
        }
    }
}
