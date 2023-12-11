using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Day02
{
    static internal class Part2
    {
        public static void Execute()
        {
            var sum = 0;
            var gameRegex = new Regex("Game (?<gameId>\\d+): (?<games>.*)", RegexOptions.Compiled);
            var colorsRegex = new Regex("(?<amount>\\d+) (?<color>.*)", RegexOptions.Compiled);

            foreach (var line in File.ReadLines("input.txt"))
            {
                var minimumColors = new Dictionary<string, int>();
                var gameMatch = gameRegex.Match(line);

                foreach (var game in gameMatch.Groups["games"].Value.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    foreach (var colorGroup in game.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                    {
                        var colorsMatch = colorsRegex.Match(colorGroup);
                        var color = colorsMatch.Groups["color"].Value;
                        var amount = int.Parse(colorsMatch.Groups["amount"].Value);

                        if (!minimumColors.ContainsKey(color))
                        {
                            minimumColors.Add(color, amount);
                        }
                        else if (minimumColors[color] < amount)
                        {
                            minimumColors[color] = amount;
                        }
                    }
                }

                sum += minimumColors.Values.Aggregate((prev, next) => prev * next);
            }

            Console.WriteLine(sum);
        }
    }
}
