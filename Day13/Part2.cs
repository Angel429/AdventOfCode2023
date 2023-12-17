namespace Day13
{
    static internal class Part2
    {
        public static void Execute()
        {
            var notesEnumerator = File.ReadLines("input.txt").GetEnumerator();
            var notes = new List<string[]>();
            var currentNote = new List<string>();

            while (notesEnumerator.MoveNext())
            {
                if (string.IsNullOrWhiteSpace(notesEnumerator.Current))
                {
                    notes.Add([.. currentNote]);
                    currentNote.Clear();
                }
                else
                {
                    currentNote.Add(notesEnumerator.Current);
                }
            }

            if (currentNote.Count > 0)
            {
                notes.Add([.. currentNote]);
                currentNote.Clear();
            }

            var sum = 0L;
            foreach (var note in notes)
            {
                var isRowTheAnswer = false;
                for (int rowIndex = 0; !isRowTheAnswer && rowIndex < note.Length; rowIndex++)
                {
                    var distance = 0;
                    var remainingSmudges = 1;
                    while (true)
                    {
                        if (rowIndex - distance < 0 || rowIndex + distance + 1 == note.Length)
                        {
                            if (remainingSmudges == 0)
                            {
                                if (isRowTheAnswer)
                                {
                                    sum += 100L * (rowIndex + 1);
                                }
                            }
                            else
                            {
                                isRowTheAnswer = false;
                            }
                            break;
                        }
                        else if (note[rowIndex - distance].Zip(note[rowIndex + distance + 1]).Count(x => x.First != x.Second) <= remainingSmudges)
                        {
                            var smudgesFound = note[rowIndex - distance].Zip(note[rowIndex + distance + 1]).Count(x => x.First != x.Second);

                            if (remainingSmudges > 0 && smudgesFound <= remainingSmudges)
                            {
                                remainingSmudges -= smudgesFound;
                            }
                            isRowTheAnswer = true;
                            distance++;
                        }
                        else
                        {
                            isRowTheAnswer = false;
                            break;
                        }
                    }
                }

                var isColumnTheAnswer = false;
                if (!isRowTheAnswer)
                {
                    for (int columnIndex = 0; !isColumnTheAnswer && columnIndex < note[0].Length; columnIndex++)
                    {
                        var distance = 0;
                        var remainingSmudges = 1;
                        while (true)
                        {
                            if (columnIndex - distance < 0 || columnIndex + distance + 1 == note[0].Length)
                            {
                                if (remainingSmudges == 0)
                                {
                                    if (isColumnTheAnswer)
                                    {
                                        sum += columnIndex + 1;
                                    }
                                }
                                else
                                {
                                    isColumnTheAnswer = false;
                                }
                                break;
                            }
                            var leftColumn = note.Select(x => x[columnIndex - distance]);
                            var rightColumn = note.Select(x => x[columnIndex + distance + 1]);

                            if (leftColumn.Zip(rightColumn).Count(x => x.First != x.Second) <= remainingSmudges)
                            {
                                var smudgesFound = leftColumn.Zip(rightColumn).Count(x => x.First != x.Second);

                                if (remainingSmudges > 0 && smudgesFound <= remainingSmudges)
                                {
                                    remainingSmudges -= smudgesFound;
                                }
                                isColumnTheAnswer = true;
                                distance++;
                            }
                            else
                            {
                                isColumnTheAnswer = false;
                                break;
                            }
                        }
                    }
                }

                if (!isRowTheAnswer && !isColumnTheAnswer)
                {

                }
            }

            Console.WriteLine(sum);
        }
    }
}
