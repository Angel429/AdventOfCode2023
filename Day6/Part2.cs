namespace Day6
{
    static internal class Part2
    {
        public static void Execute()
        {
            var lines = File.ReadLines("input.txt");
            var time = long.Parse(lines.First().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Skip(1).Aggregate((prev, next) => prev + next));
            var distance = long.Parse(lines.Skip(1).First().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Skip(1).Aggregate((prev, next) => prev + next));

            var squareRoot = Math.Sqrt(time * time - 4 * distance);
            var minimumPressedButtonTime = (int)Math.Ceiling((time - squareRoot) / 2d);
            var maximumPressedButtonTime = (int)Math.Floor((time + squareRoot) / 2d);

            Console.WriteLine(maximumPressedButtonTime - minimumPressedButtonTime + 1);
        }
    }
}
