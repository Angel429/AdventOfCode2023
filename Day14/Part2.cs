namespace Day14
{
    static internal class Part2
    {
        public static void Execute()
        {
            var rocks = File.ReadAllLines("input.txt").Select(x => x.ToCharArray()).ToArray();

            var cache = new Dictionary<string, int>();
            var iterationNumber = 0;
            var foundCycle = false;

            while(iterationNumber < 1000000000)
            {
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

                for (int rowIndex = 0; rowIndex < rocks.Length; rowIndex++)
                {
                    for (int columnIndex = 1; columnIndex < rocks[0].Length; columnIndex++)
                    {
                        if (rocks[rowIndex][columnIndex] == 'O')
                        {
                            var newColumn = columnIndex - 1;
                            while (newColumn >= 0 && rocks[rowIndex][newColumn] == '.')
                            {
                                newColumn--;
                            }
                            rocks[rowIndex][columnIndex] = '.';
                            rocks[rowIndex][newColumn + 1] = 'O';
                        }
                    }
                }

                for (int rowIndex = rocks.Length - 2; rowIndex >= 0; rowIndex--)
                {
                    for (int columnIndex = 0; columnIndex < rocks[0].Length; columnIndex++)
                    {
                        if (rocks[rowIndex][columnIndex] == 'O')
                        {
                            var newRow = rowIndex + 1;
                            while (newRow < rocks.Length && rocks[newRow][columnIndex] == '.')
                            {
                                newRow++;
                            }
                            rocks[rowIndex][columnIndex] = '.';
                            rocks[newRow - 1][columnIndex] = 'O';
                        }
                    }
                }

                for (int rowIndex = 0; rowIndex < rocks.Length; rowIndex++)
                {
                    for (int columnIndex = rocks[0].Length - 2; columnIndex >= 0; columnIndex--)
                    {
                        if (rocks[rowIndex][columnIndex] == 'O')
                        {
                            var newColumn = columnIndex + 1;
                            while (newColumn < rocks[0].Length && rocks[rowIndex][newColumn] == '.')
                            {
                                newColumn++;
                            }
                            rocks[rowIndex][columnIndex] = '.';
                            rocks[rowIndex][newColumn - 1] = 'O';
                        }
                    }
                }

                if (!foundCycle)
                {
                    var newEntry = rocks.Select(x => new string(x)).Aggregate((acum, next) => acum + next);
                    if (cache.ContainsKey(newEntry))
                    {
                        foundCycle = true;
                        var cycleSize = iterationNumber - cache[newEntry];
                        iterationNumber = 1000000001 - ((1000000000 - iterationNumber) % cycleSize);
                    }
                    else
                    {
                        cache.Add(newEntry, iterationNumber);
                        iterationNumber++;
                    }
                }
                else
                {
                    iterationNumber++;
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
