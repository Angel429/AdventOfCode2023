using System.Text.RegularExpressions;

namespace Day4
{
    static internal class Part2
    {
        public static void Execute()
        {
            var scratchcardsAmount = new Dictionary<int, int>();
            var regex = new Regex("^Card\\s+(?<scratchcardId>\\d+):(\\s+(?<winningNumbers>\\d+))+ \\|(\\s+(?<numbersYouHave>\\d+))+$", RegexOptions.Compiled);

            foreach (var line in File.ReadLines("input.txt"))
            {
                var match = regex.Match(line);

                var scratchcardId = int.Parse(match.Groups["scratchcardId"].Value);
                if (scratchcardsAmount.ContainsKey(scratchcardId))
                {
                    scratchcardsAmount[scratchcardId]++;
                }
                else
                {
                    scratchcardsAmount[scratchcardId] = 1;
                }

                var winningNumbers = match.Groups["winningNumbers"].Captures.Select(x => x.Value).ToHashSet();
                var numbersYouHave = match.Groups["numbersYouHave"].Captures.Select(x => x.Value).ToHashSet();

                var ammountOfWinningNumbers = winningNumbers.Intersect(numbersYouHave).Count();

                if (ammountOfWinningNumbers > 0)
                {
                    foreach (var newScratchcardId in Enumerable.Range(scratchcardId + 1, ammountOfWinningNumbers))
                    {
                        if (scratchcardsAmount.ContainsKey(newScratchcardId))
                        {
                            scratchcardsAmount[newScratchcardId] += scratchcardsAmount[scratchcardId];
                        } else
                        {
                            scratchcardsAmount[newScratchcardId] = scratchcardsAmount[scratchcardId];
                        }
                    }
                }
            }

            Console.WriteLine(scratchcardsAmount.Sum(x => x.Value));
        }
    }
}
