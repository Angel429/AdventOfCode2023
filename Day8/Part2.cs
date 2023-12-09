namespace Day8
{
    static internal class Part2
    {
        public static void Execute()
        {
            var linesEnum = File.ReadLines("input.txt");

            var directions = linesEnum.First();
            var graph = linesEnum.Skip(2).ToDictionary(x => x[0..3], x => (x[7..10], x[12..15]));

            var currentNodes = graph.Keys.Where(x => x.EndsWith('A')).ToArray();

            var loopLengths = new int[currentNodes.Length];

            for (var i = 0; i < currentNodes.Length; i++)
            {
                var currentDirectionIndex = 0;
                while (!currentNodes[i].EndsWith('Z'))
                {
                    currentNodes[i] = directions[currentDirectionIndex % directions.Length] == 'L' ? graph[currentNodes[i]].Item1 : graph[currentNodes[i]].Item2;
                    currentDirectionIndex++;
                }
                loopLengths[i] = currentDirectionIndex;
            }

            var currentLCM = (long)loopLengths[0];

            foreach (var loopLength in loopLengths.Skip(1).Select(x => (long)x))
            {
                var gcd = findGCD([currentLCM, loopLength], 2);
                currentLCM = currentLCM / gcd * loopLength;
            }

            Console.WriteLine(currentLCM);
        }

        // Functions to find the Greatest Common Divisor, tweaked to support long integers
        // From: https://www.geeksforgeeks.org/gcd-two-array-numbers/
        private static long findGCD(long[] arr, int n)
        {
            long result = arr[0];
            for (int i = 1; i < n; i++)
            {
                result = gcd(arr[i], result);

                if (result == 1)
                {
                    return 1;
                }
            }

            return result;
        }
        private static long gcd(long a, long b)
        {
            if (a == 0)
                return b;
            return gcd(b % a, a);
        }
    }
}
