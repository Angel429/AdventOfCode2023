namespace Day6
{
    static internal class Part1
    {
        public static void Execute()
        {
            var lines = File.ReadLines("input.txt");
            var times = lines.First().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Skip(1).Select(int.Parse).ToList();
            var distances = lines.Skip(1).First().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Skip(1).Select(int.Parse).ToList();

            var prod = 1L;

            for (int i = 0; i < times.Count; i++)
            {
                var time = times[i];
                var distance = distances[i];

                var squareRoot = Math.Sqrt(time * time - 4 * distance);
                var minimumPressedButtonTime = (int) Math.Ceiling((time - squareRoot) / 2d);
                var maximumPressedButtonTime = (int) Math.Floor((time + squareRoot) / 2d);

                prod *= maximumPressedButtonTime - minimumPressedButtonTime + 1;
            }

            Console.WriteLine(prod);
        }
    }
}
