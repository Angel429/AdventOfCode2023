namespace Day13
{
    static internal class Part1
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
                } else
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
                    while (true)
                    {
                        if (rowIndex - distance < 0 || rowIndex + distance + 1 == note.Length)
                        {
                            if (isRowTheAnswer)
                            {
                                sum += 100L * (rowIndex + 1);
                            }
                            break;
                        } else if (note[rowIndex - distance].SequenceEqual(note[rowIndex + distance + 1]))
                        {
                            isRowTheAnswer = true;
                            distance++;
                        } else
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
                        while (true)
                        {
                            if (columnIndex - distance < 0 || columnIndex + distance + 1 == note[0].Length)
                            {
                                if (isColumnTheAnswer)
                                {
                                    sum += columnIndex + 1;
                                }
                                break;
                            }
                            else if (note.Select(x => x[columnIndex - distance]).SequenceEqual(note.Select(x => x[columnIndex + distance + 1])))
                            {
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
            }

            Console.WriteLine(sum);
        }
    }
}
