namespace Day15
{
    static internal class Part1
    {
        public static void Execute()
        {
            var sequence = File.ReadAllLines("input.txt").First();

            var sum = 0;
            foreach (var step in sequence.Split(','))
            {
                var hash = 0;
                foreach (var character in step)
                {
                    hash = (hash + character) * 17 % 256;
                }
                sum += hash;
            }

            Console.WriteLine(sum);
        }
    }
}
