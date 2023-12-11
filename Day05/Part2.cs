using System.Text.RegularExpressions;

namespace Day05
{
    static internal class Part2
    {
        public static void Execute()
        {
            var seedsRegex = new Regex("^seeds:( (?<seedId>\\d+) (?<range>\\d+))+$", RegexOptions.Compiled);
            var somethingToSomethingMapRegex = new Regex("^(?<sourceMap>\\w+)-to-(?<destinationMap>\\w+) map:$", RegexOptions.Compiled);
            var mapRegex = new Regex("^(?<destinationId>\\d+) (?<sourceId>\\d+) (?<range>\\d+)$", RegexOptions.Compiled);

            var linesEnumerable = File.ReadLines("input.txt");

            var firstLine = linesEnumerable.First();
            var seedsMatch = seedsRegex.Match(firstLine);
            var currentValues = new HashSet<(long Start, long Range)>();
            var a = currentValues.FirstOrDefault();
            for (int seedIdIndex = 0; seedIdIndex < seedsMatch.Groups["seedId"].Captures.Count; seedIdIndex++)
            {
                var seedId = long.Parse(seedsMatch.Groups["seedId"].Captures[seedIdIndex].ValueSpan);
                var range = int.Parse(seedsMatch.Groups["range"].Captures[seedIdIndex].ValueSpan);
                currentValues.Add((seedId, range));
            }

            var nextValues = new HashSet<(long Start, long Range)>();

            foreach (var line in linesEnumerable.Skip(3))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (somethingToSomethingMapRegex.IsMatch(line))
                {
                    currentValues.UnionWith(nextValues);
                    nextValues.Clear();
                }
                else
                {
                    var match = mapRegex.Match(line);
                    var destinationId = long.Parse(match.Groups["destinationId"].ValueSpan);
                    var sourceId = long.Parse(match.Groups["sourceId"].ValueSpan);
                    var range = long.Parse(match.Groups["range"].ValueSpan);

                    var currentValuesToAdd = new List<(long Start, long Range)>();
                    var currentValuesToRemove = new List<(long Start, long Range)>();
                    foreach (var currentValue in currentValues)
                    {
                        (var rangeInside, var rangesOutside) = SplitRange((sourceId, range), currentValue);

                        if (rangeInside != null)
                        {
                            nextValues.Add((destinationId + rangeInside.Value.Start - sourceId, rangeInside.Value.Range));
                            if (rangesOutside != null)
                            {
                                currentValuesToAdd.AddRange(rangesOutside);
                            }
                            currentValuesToRemove.Add(currentValue);
                        }
                    }
                    if (currentValuesToRemove.Count > 0)
                    {
                        currentValues.RemoveWhere(currentValuesToRemove.Contains);
                    }
                    if (currentValuesToAdd.Count > 0)
                    {
                        foreach (var newValue in currentValuesToAdd)
                        {
                            currentValues.Add(newValue);
                        }
                    }
                }
            }

            currentValues.UnionWith(nextValues);
            nextValues.Clear();

            Console.WriteLine(currentValues.Min(x => x.Start));
        }

        private static ((long Start, long Range)? rangeInside, (long Start, long Range)[]? rangesOutside) SplitRange((long Start, long Range) rangeOfReference, (long Start, long Range) rangeToSplit)
        {
            if (rangeToSplit.Start < rangeOfReference.Start)
            {
                if (rangeToSplit.Start + rangeToSplit.Range < rangeOfReference.Start)
                {
                    return (null, [rangeToSplit]);
                }
                else if (rangeToSplit.Start + rangeToSplit.Range <= rangeOfReference.Start + rangeOfReference.Range)
                {
                    return (new(rangeOfReference.Start, rangeToSplit.Start + rangeToSplit.Range - rangeOfReference.Start), [new(rangeToSplit.Start, rangeOfReference.Start - rangeToSplit.Start)]);
                }
                else
                {
                    return (rangeOfReference, [new(rangeToSplit.Start, rangeOfReference.Start - rangeToSplit.Start), new(rangeOfReference.Start + rangeOfReference.Range, rangeToSplit.Start + rangeToSplit.Range - rangeOfReference.Start - rangeOfReference.Range)]);
                }
            }
            else
            {
                if (rangeToSplit.Start > rangeOfReference.Start + rangeOfReference.Range)
                {
                    return (null, [rangeToSplit]);
                }
                else if (rangeToSplit.Start + rangeToSplit.Range <= rangeOfReference.Start + rangeOfReference.Range)
                {
                    return (rangeToSplit, null);
                }
                else
                {
                    return (new(rangeToSplit.Start, rangeOfReference.Start + rangeOfReference.Range - rangeToSplit.Start), [new(rangeOfReference.Start + rangeOfReference.Range, rangeToSplit.Start + rangeToSplit.Range - rangeOfReference.Start - rangeOfReference.Range)]);
                }
            }
        }
    }
}
