namespace Day09
{
    static internal class Part1
    {
        public static void Execute()
        {
            var sum = 0;
            foreach (var history in File.ReadLines("input.txt"))
            {
                var currentSequence = history.Split().Select(int.Parse).ToArray();
                var allSequences = new List<int[]>
                {
                    currentSequence
                };

                while (currentSequence.Any(x => x != 0))
                {
                    var nextSequence = new int[currentSequence.Length - 1];

                    for (int i = 0; i < nextSequence.Length; i++)
                    {
                        nextSequence[i] = currentSequence[i + 1] - currentSequence[i];
                    }

                    allSequences.Add(nextSequence);
                    currentSequence = nextSequence;
                }

                var currentNewValue = 0;
                for (int i = allSequences.Count - 2; i >= 0; i--)
                {
                    currentNewValue += allSequences[i][^1];
                }

                sum += currentNewValue;
            }

            Console.WriteLine(sum);
        }
    }
}
