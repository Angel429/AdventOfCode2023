namespace Day12
{
    static internal class Part1
    {
        public static void Execute()
        {
            var sum = 0;

            foreach (var line in File.ReadLines("input.txt"))
            {
                var symbols = line.Split()[0];
                var numbers = line.Split()[1].Split(',').Select(int.Parse).ToArray();

                var currentIteration = symbols.ToList();

                var questionMarksPosition = currentIteration.Select((value, position) => (Value: value, Position: position)).Where(x => x.Value == '?').Select(x => x.Position).ToList();

                var count = 0;
                var keepLooping = true;
                currentIteration = currentIteration.ConvertAll(x => x == '?' ? '#' : x);
                while (keepLooping)
                {
                    if (IsCompatible(currentIteration, numbers))
                    {
                        count++;
                    }

                    var currentQuestionMarkPositionIndex = 0;
                    while (currentIteration[questionMarksPosition[currentQuestionMarkPositionIndex]] == '.')
                    {
                        currentIteration[questionMarksPosition[currentQuestionMarkPositionIndex]] = '#';
                        currentQuestionMarkPositionIndex++;
                        if (currentQuestionMarkPositionIndex == questionMarksPosition.Count)
                        {
                            keepLooping = false;
                            break;
                        }
                    }
                    if (keepLooping)
                    {
                        currentIteration[questionMarksPosition[currentQuestionMarkPositionIndex]] = '.';
                    }
                }
                sum += count;
            }

            Console.WriteLine(sum);
        }

        private static bool IsCompatible(List<char> symbols, int[] numbers)
        {
            var currentNumberIndex = -1;
            var currentNumber = -1;
            var currentPads = 0;
            var checkingForPads = false;

            foreach (var symbol in symbols)
            {
                if (checkingForPads)
                {
                    if (symbol == '#')
                    {
                        currentPads++;
                        if (currentPads > currentNumber)
                        {
                            return false;
                        }
                    }
                    else if (symbol == '.')
                    {
                        if (currentPads < currentNumber)
                        {
                            return false;
                        }
                        checkingForPads = false;
                    }
                }
                else
                {
                    if (symbol == '#')
                    {
                        currentNumberIndex++;
                        if (currentNumberIndex == numbers.Length)
                        {
                            return false;
                        }
                        currentNumber = numbers[currentNumberIndex];
                        checkingForPads = true;
                        currentPads = 1;
                    }
                }
            }

            return currentNumberIndex == numbers.Length - 1 && currentPads == numbers[^1];
        }
    }
}
