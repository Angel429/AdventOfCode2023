using System.Text.RegularExpressions;

namespace Day4
{
    static internal class Part1
    {
        public static void Execute()
        {
            var sum = 0;
            var regex = new Regex("^Card\\s+\\d+:(\\s+(?<winningNumbers>\\d+))+ \\|(\\s+(?<numbersYouHave>\\d+))+$", RegexOptions.Compiled);

            foreach (var line in File.ReadLines("input.txt"))
            {
                var match = regex.Match(line);

                var winningNumbers = match.Groups["winningNumbers"].Captures.Select(x => x.Value).ToHashSet();
                var numbersYouHave = match.Groups["numbersYouHave"].Captures.Select(x => x.Value).ToHashSet();

                var ammountOfWinningNumbers = winningNumbers.Intersect(numbersYouHave).Count();

                if (ammountOfWinningNumbers > 0)
                {
                    sum += 1 << (ammountOfWinningNumbers - 1);
                }
            }

            Console.WriteLine(sum);
        }
    }
}
