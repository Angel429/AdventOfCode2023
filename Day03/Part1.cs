namespace Day03
{
    static internal class Part1
    {
        public static void Execute()
        {
            var sum = 0;
            var allLines = File.ReadAllLines("input.txt");

            for (int lineIndex = 0; lineIndex < allLines.Length; lineIndex++)
            {
                var currentState = NumberState.NO_NUMBER_FOUND;
                var numberStartsIndex = -1;

                for (int characterInLineIndex = 0; characterInLineIndex < allLines[lineIndex].Length; characterInLineIndex++)
                {
                    switch (currentState)
                    {
                        case NumberState.NO_NUMBER_FOUND:
                            if (char.IsNumber(allLines[lineIndex][characterInLineIndex]))
                            {
                                numberStartsIndex = characterInLineIndex;
                                if (IsNumberAdjacentToSymbol(allLines, lineIndex, characterInLineIndex))
                                {
                                    currentState = NumberState.FOUND_NUMBER_WITH_SYMBOL;
                                }
                                else
                                {
                                    currentState = NumberState.FOUND_NUMBER_WITHOUT_SYMBOL;
                                }
                            }
                            break;
                        case NumberState.FOUND_NUMBER_WITHOUT_SYMBOL:
                            if (char.IsNumber(allLines[lineIndex][characterInLineIndex]))
                            {
                                if (IsNumberAdjacentToSymbol(allLines, lineIndex, characterInLineIndex))
                                {
                                    currentState = NumberState.FOUND_NUMBER_WITH_SYMBOL;
                                }
                            }
                            else
                            {
                                currentState = NumberState.NO_NUMBER_FOUND;
                            }
                            break;
                        case NumberState.FOUND_NUMBER_WITH_SYMBOL:
                            if (!char.IsNumber(allLines[lineIndex][characterInLineIndex]))
                            {
                                sum += int.Parse(allLines[lineIndex].AsSpan(new Range(numberStartsIndex, characterInLineIndex)));
                                currentState = NumberState.NO_NUMBER_FOUND;
                            }
                            break;
                    }
                }

                if (currentState == NumberState.FOUND_NUMBER_WITH_SYMBOL)
                {
                    sum += int.Parse(allLines[lineIndex].AsSpan(new Range(numberStartsIndex, allLines[lineIndex].Length)));
                }
            }

            Console.WriteLine(sum);
        }

        private static bool IsNumberAdjacentToSymbol(string[] allLines, int lineIndex, int characterInLineIndex)
        {
            var canCheckUp = lineIndex > 0;
            var canCheckLeft = characterInLineIndex > 0;
            var canCheckRight = characterInLineIndex < allLines.Length - 1;
            var canCheckDown = lineIndex < allLines.Length - 1;

            // Check previous line
            if (canCheckUp)
            {
                if (canCheckLeft && IsSymbol(allLines[lineIndex - 1][characterInLineIndex - 1]))
                {
                    return true;
                }

                if (IsSymbol(allLines[lineIndex - 1][characterInLineIndex]))
                {
                    return true;
                }

                if (canCheckRight && IsSymbol(allLines[lineIndex - 1][characterInLineIndex + 1]))
                {
                    return true;
                }
            }

            // Check current line
            if (canCheckLeft && IsSymbol(allLines[lineIndex][characterInLineIndex - 1]))
            {
                return true;
            }

            if (canCheckRight && IsSymbol(allLines[lineIndex][characterInLineIndex + 1]))
            {
                return true;
            }

            // Check lower line
            if (canCheckDown)
            {
                if (canCheckLeft && IsSymbol(allLines[lineIndex + 1][characterInLineIndex - 1]))
                {
                    return true;
                }

                if (IsSymbol(allLines[lineIndex + 1][characterInLineIndex]))
                {
                    return true;
                }

                if (canCheckRight && IsSymbol(allLines[lineIndex + 1][characterInLineIndex + 1]))
                {
                    return true;
                }
            }

            // If there are no lines... there are no symbols
            return false;
        }

        private static bool IsSymbol(char character)
        {
            return !char.IsNumber(character) && character != '.';
        }
    }
}
