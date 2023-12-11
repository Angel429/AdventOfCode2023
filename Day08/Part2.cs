namespace Day08
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
                currentLCM = FindLCM(currentLCM, loopLength);
            }

            Console.WriteLine(currentLCM);
        }

        // Least Common Multiple calculation
        // Formula from Wikipedia
        // https://en.wikipedia.org/wiki/Least_common_multiple#Using_the_greatest_common_divisor
        private static long FindLCM(long a, long b)
        {
            return a / FindGCD(a, b) * b;
        }

        // Greatest Common Divisor calculation using Euclid's algorithm
        // Formula from Wikipedia
        // https://en.wikipedia.org/wiki/Greatest_common_divisor#Euclid's_algorithm
        // https://en.wikipedia.org/wiki/Euclidean_algorithm
        private static long FindGCD(long a, long b)
        {
            if (b < a) return FindGCD(b, a);

            while (a > 0 && b > 0)
            {
                (a, b) = (b, a % b);
            }

            return a;
        }
    }
}
