namespace Day14
{
    static internal class Part1
    {
        public static void Execute()
        {
            var rocks = File.ReadAllLines("input.txt").Select(x => x.ToCharArray()).ToArray();

            for (int rowIndex = 1; rowIndex < rocks.Length; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < rocks[0].Length; columnIndex++)
                {
                    if (rocks[rowIndex][columnIndex] == 'O')
                    {
                        var newRow = rowIndex - 1;
                        while (newRow >= 0 && rocks[newRow][columnIndex] == '.')
                        {
                            newRow--;
                        }
                        rocks[rowIndex][columnIndex] = '.';
                        rocks[newRow + 1][columnIndex] = 'O';
                    }
                }
            }

            var sum = 0;

            for (int rowIndex = 0; rowIndex < rocks.Length; rowIndex++)
            {
                sum += rocks[rowIndex].Count(x => x == 'O') * (rocks.Length - rowIndex);
            }

            Console.WriteLine(sum);
        }
    }
}
