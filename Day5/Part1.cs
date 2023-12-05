using System.Text.RegularExpressions;

namespace Day5
{
    static internal class Part1
    {
        public static void Execute()
        {
            var seedsRegex = new Regex("^seeds:( (?<seedId>\\d+))+$", RegexOptions.Compiled);
            var somethingToSomethingMapRegex = new Regex("^(?<sourceMap>\\w+)-to-(?<destinationMap>\\w+) map:$", RegexOptions.Compiled);
            var mapRegex = new Regex("^(?<destinationId>\\d+) (?<sourceId>\\d+) (?<range>\\d+)$", RegexOptions.Compiled);
            
            var linesEnumerable = File.ReadLines("input.txt");

            var firstLine = linesEnumerable.First();
            var seedsMatch = seedsRegex.Match(firstLine);
            var currentValues = seedsMatch.Groups["seedId"].Captures.Select(x => long.Parse(x.ValueSpan)).ToHashSet();

            var nextValues = new HashSet<long>();

            foreach (var line in linesEnumerable.Skip(3))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                
                if (somethingToSomethingMapRegex.IsMatch(line))
                {
                    currentValues.UnionWith(nextValues);
                    nextValues.Clear();
                } else
                {
                    var match = mapRegex.Match(line);
                    var destinationId = long.Parse(match.Groups["destinationId"].ValueSpan);
                    var sourceId = long.Parse(match.Groups["sourceId"].ValueSpan);
                    var range = long.Parse(match.Groups["range"].ValueSpan);

                    var currentValuesToRemove = new List<long>();
                    foreach (var currentValue in currentValues)
                    {
                        if (currentValue >= sourceId && currentValue < sourceId + range)
                        {
                            nextValues.Add(destinationId + currentValue - sourceId);
                            currentValuesToRemove.Add(currentValue);
                        }
                    }
                    if (currentValuesToRemove.Count > 0)
                    {
                        currentValues.RemoveWhere(currentValuesToRemove.Contains);
                    }
                }
            }

            currentValues.UnionWith(nextValues);
            nextValues.Clear();

            Console.WriteLine(currentValues.Min());
        }
    }
}
