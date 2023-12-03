namespace Day3
{
    static internal class Part2
    {
        public static void Execute()
        {
            var sum = 0L;
            var allLines = File.ReadAllLines("input.txt");

            for (int lineIndex = 0; lineIndex < allLines.Length; lineIndex++)
            {
                for (int characterInLineIndex = 0; characterInLineIndex < allLines[lineIndex].Length; characterInLineIndex++)
                {
                    if (allLines[lineIndex][characterInLineIndex] == '*')
                    {
                        var gearRatio = GetGearRatio(allLines, lineIndex, characterInLineIndex);
                        if (gearRatio > 0)
                        {
                            sum += gearRatio;
                        }
                    }
                }
            }

            Console.WriteLine(sum);
        }

        private static long GetGearRatio(string[] allLines, int lineIndex, int characterInLineIndex)
        {
            var canCheckUp = lineIndex > 0;
            var canCheckLeft = characterInLineIndex > 0;
            var canCheckRight = characterInLineIndex < allLines.Length - 1;
            var canCheckDown = lineIndex < allLines.Length - 1;

            var numbers = new List<int>();

            // Check previous line
            if (canCheckUp)
            {
                var numberIndexes = new List<int>();
                if (canCheckLeft && char.IsNumber(allLines[lineIndex - 1][characterInLineIndex - 1]))
                {
                    numberIndexes.Add(characterInLineIndex - 1);
                }

                if (char.IsNumber(allLines[lineIndex - 1][characterInLineIndex]))
                {
                    numberIndexes.Add(characterInLineIndex);
                }

                if (canCheckRight && char.IsNumber(allLines[lineIndex - 1][characterInLineIndex + 1]))
                {
                    numberIndexes.Add(characterInLineIndex + 1);
                }

                switch(numberIndexes.Count)
                {
                    case 1:
                    case 3:
                        numbers.Add(GetCompleteNumber(allLines, lineIndex - 1, numberIndexes[0]));
                        break;
                    case 2:
                        numbers.Add(GetCompleteNumber(allLines, lineIndex - 1, numberIndexes[0]));
                        if (numberIndexes[1] - numberIndexes[0] > 1)
                        {
                            numbers.Add(GetCompleteNumber(allLines, lineIndex - 1, numberIndexes[1]));
                        }
                        break;
                }
            }

            // Check current line
            if (canCheckLeft && char.IsNumber(allLines[lineIndex][characterInLineIndex - 1]))
            {
                numbers.Add(GetCompleteNumber(allLines, lineIndex, characterInLineIndex - 1));
            }

            if (canCheckRight && char.IsNumber(allLines[lineIndex][characterInLineIndex + 1]))
            {
                numbers.Add(GetCompleteNumber(allLines, lineIndex, characterInLineIndex + 1));
            }

            // Check lower line
            if (canCheckDown)
            {
                var numberIndexes = new List<int>();
                if (canCheckLeft && char.IsNumber(allLines[lineIndex + 1][characterInLineIndex - 1]))
                {
                    numberIndexes.Add(characterInLineIndex - 1);
                }

                if (char.IsNumber(allLines[lineIndex + 1][characterInLineIndex]))
                {
                    numberIndexes.Add(characterInLineIndex);
                }

                if (canCheckRight && char.IsNumber(allLines[lineIndex + 1][characterInLineIndex + 1]))
                {
                    numberIndexes.Add(characterInLineIndex + 1);
                }

                switch (numberIndexes.Count)
                {
                    case 1:
                    case 3:
                        numbers.Add(GetCompleteNumber(allLines, lineIndex + 1, numberIndexes[0]));
                        break;
                    case 2:
                        numbers.Add(GetCompleteNumber(allLines, lineIndex + 1, numberIndexes[0]));
                        if (numberIndexes[1] - numberIndexes[0] > 1)
                        {
                            numbers.Add(GetCompleteNumber(allLines, lineIndex + 1, numberIndexes[1]));
                        }
                        break;
                }
            }

            if (numbers.Count == 2)
            {
                return numbers[0] * numbers[1];
            }

            return -1;
        }

        private static int GetCompleteNumber(string[] allLines, int lineIndex, int characterInLineIndex)
        {
            var numberAlmostStartsIndex = characterInLineIndex;
            var numberAlmostEndsIndex = characterInLineIndex;

            while (numberAlmostStartsIndex >= 0 && char.IsNumber(allLines[lineIndex][numberAlmostStartsIndex]))
            {
                numberAlmostStartsIndex--;
            }

            while (numberAlmostEndsIndex < allLines[lineIndex].Length && char.IsNumber(allLines[lineIndex][numberAlmostEndsIndex]))
            {
                numberAlmostEndsIndex++;
            }

            return int.Parse(allLines[lineIndex].AsSpan(numberAlmostStartsIndex + 1, numberAlmostEndsIndex - numberAlmostStartsIndex - 1));
        }
    }
}
