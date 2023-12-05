using System.Text.RegularExpressions;

namespace Day5
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
            var currentValues = new HashSet<RangeLong>(new RangeLongComparer());
            for (int seedIdIndex = 0; seedIdIndex < seedsMatch.Groups["seedId"].Captures.Count; seedIdIndex++)
            {
                var seedId = long.Parse(seedsMatch.Groups["seedId"].Captures[seedIdIndex].ValueSpan);
                var range = int.Parse(seedsMatch.Groups["range"].Captures[seedIdIndex].ValueSpan);
                currentValues.Add(new RangeLong(seedId, seedId + range));
            }

            var nextValues = new HashSet<RangeLong>(new RangeLongComparer());

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

                    var currentValuesToAdd = new List<RangeLong>();
                    var currentValuesToRemove = new List<RangeLong>();
                    foreach (var currentValue in currentValues)
                    {
                        (var rangeInside, var rangesOutside) = SplitRange(new RangeLong(sourceId, sourceId + range), currentValue);
                        
                        if (rangeInside != null)
                        {
                            nextValues.Add(new RangeLong(destinationId + rangeInside.Start - sourceId, destinationId + rangeInside.End - sourceId));
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

            var a = currentValues.Where(x => x.Start <= 0).ToList();
            Console.WriteLine(currentValues.Min(x => x.Start));
        }

        private static (RangeLong? rangeInside, RangeLong[]? rangesOutside) SplitRange(RangeLong rangeOfReference, RangeLong rangeToSplit)
        {
            if (rangeToSplit.Start < rangeOfReference.Start)
            {
                if (rangeToSplit.End < rangeOfReference.Start)
                {
                    return (null, [rangeToSplit]);
                } else if (rangeToSplit.End <= rangeOfReference.End)
                {
                    return (new(rangeOfReference.Start, rangeToSplit.End), [new(rangeToSplit.Start, rangeOfReference.Start)]);
                } else
                {
                    return (new(rangeOfReference.Start, rangeOfReference.End), [new(rangeToSplit.Start, rangeOfReference.Start), new(rangeOfReference.End, rangeToSplit.End)]);
                }
            } else
            {
                if (rangeToSplit.Start > rangeOfReference.End)
                {
                    return (null, [rangeToSplit]);
                } else if (rangeToSplit.End <= rangeOfReference.End)
                {
                    return (new(rangeToSplit.Start, rangeToSplit.End), null);
                } else
                {
                    return (new(rangeToSplit.Start, rangeOfReference.End), [new(rangeOfReference.End, rangeToSplit.End)]);
                }
            }
        }
    }
}
