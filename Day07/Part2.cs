namespace Day07
{
    static internal class Part2
    {
        public static void Execute()
        {
            var hands = File.ReadAllLines("input.txt")
                .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                .Select(x => new Hand(x[0], int.Parse(x[1]), true))
                .ToList();

            hands.Sort();

            var sum = hands.Select((hand, i) => hand.Bid * (i + 1)).Sum();

            Console.WriteLine(sum);
        }
    }
}
