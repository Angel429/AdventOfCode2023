using System.Collections.ObjectModel;

namespace Day1
{
    static internal class Part2
    {
        private readonly static ReadOnlyDictionary<string, int> _numbers;

        static Part2()
        {
            _numbers = new Dictionary<string, int>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 }
            }.AsReadOnly();
        }

        public static void Execute()
        {
            var sum = 0;
            foreach (var line in File.ReadLines("input.txt"))
            {
                sum += 10 * FindFirstStringOrNumber(line);
                sum += FindLastStringOrNumber(line);
            }

            Console.WriteLine(sum);
        }

        private static int FindFirstStringOrNumber(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    return line[i] - '0';
                } else
                {
                    foreach (var number in _numbers)
                    {
                        if (i + number.Key.Length <= line.Length && line.AsSpan(i..(i + number.Key.Length)).Equals(number.Key, StringComparison.Ordinal))
                        {
                            return number.Value;
                        }
                    }
                }
            }

            throw new Exception("This is finen't");
        }

        private static int FindLastStringOrNumber(string line)
        {
            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(line[i]))
                {
                    return line[i] - '0';
                }
                else
                {
                    foreach (var number in _numbers)
                    {
                        if (i + number.Key.Length <= line.Length && line.AsSpan(i..(i + number.Key.Length)).Equals(number.Key, StringComparison.Ordinal))
                        {
                            return number.Value;
                        }
                    }
                }
            }

            throw new Exception("This is finen't");
        }
    }
}
