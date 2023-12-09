namespace Day8
{
    static internal class Part1
    {
        public static void Execute()
        {
            var linesEnum = File.ReadLines("input.txt");

            var directions = linesEnum.First();
            var graph = linesEnum.Skip(2).ToDictionary(x => x[0..3], x => (x[7..10], x[12..15]));

            var currentNode = "AAA";
            var currentDirectionIndex = 0;

            while (currentNode != "ZZZ")
            {
                currentNode = directions[currentDirectionIndex % directions.Length] == 'L' ? graph[currentNode].Item1 : graph[currentNode].Item2;
                currentDirectionIndex++;
            }

            Console.WriteLine(currentDirectionIndex);
        }
    }
}
