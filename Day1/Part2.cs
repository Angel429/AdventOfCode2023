using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Day1
{
    internal class Part2
    {
        private static ReadOnlyDictionary<string, int> _numbers;

        static Part2()
        {
            _numbers = new Dictionary<string, int>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 }
            }.AsReadOnly();
        }

        public static void Execute()
        {
            var regex = new Regex("^.*(?<first>(one|two|three|four|five|six|seven|eight|nine)|\\d).*(?<last>(one|two|three|four|five|six|seven|eight|nine)|\\d).*$", RegexOptions.Compiled);
            var sum = 0;
            foreach (var line in File.ReadLines("input.txt"))
            {
                var match = regex.Match(line);
                sum += 10 * (char.IsNumber(match.Groups["first"].Value[0]) ? match.Groups["first"].Value[0] - '0' : _numbers[match.Groups["first"].Value]);
                sum += char.IsNumber(match.Groups["last"].Value[0]) ? match.Groups["last"].Value[0] - '0' : _numbers[match.Groups["last"].Value];
            }

            Console.WriteLine(sum);
        }
    }
}
