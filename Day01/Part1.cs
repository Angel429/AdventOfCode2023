namespace Day01
{
    static internal class Part1
    {
        public static void Execute()
        {
            var sum = 0;
            foreach (var line in File.ReadLines("input.txt"))
            {
                sum += 10 * (line.First(char.IsNumber) - '0');
                sum += line.Last(char.IsNumber) - '0';
            }

            Console.WriteLine(sum);
        }
    }
}
