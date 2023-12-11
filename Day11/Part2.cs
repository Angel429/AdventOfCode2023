namespace Day11
{
    static internal class Part2
    {
        public static void Execute()
        {
            var image = File.ReadLines("input.txt").Select(x => x.ToCharArray().ToList()).ToList();

            var emptyRows = Enumerable.Repeat(true, image.Count).ToArray();
            var emptyColumns = Enumerable.Repeat(true, image[0].Count).ToArray();
            var galaxiesCoords = new List<(int Row, int Column)>();

            for (int rowIndex = 0; rowIndex < emptyRows.Length; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < emptyColumns.Length; columnIndex++)
                {
                    if (image[rowIndex][columnIndex] == '#')
                    {
                        emptyRows[rowIndex] = false;
                        emptyColumns[columnIndex] = false;
                        galaxiesCoords.Add((rowIndex, columnIndex));
                    }
                }
            }

            var sum = 0L;

            for (int galaxy1Index = 0; galaxy1Index < galaxiesCoords.Count; galaxy1Index++)
            {
                for (int galaxy2Index = galaxy1Index + 1; galaxy2Index < galaxiesCoords.Count; galaxy2Index++)
                {
                    var galaxy1Row = galaxiesCoords[galaxy1Index].Row + 999_999L * emptyRows.Where((x, i) => i < galaxiesCoords[galaxy1Index].Row && x).Count();
                    var galaxy1Column = galaxiesCoords[galaxy1Index].Column + 999_999L * emptyColumns.Where((x, i) => i < galaxiesCoords[galaxy1Index].Column && x).Count();
                    var galaxy2Row = galaxiesCoords[galaxy2Index].Row + 999_999L * emptyRows.Where((x, i) => i < galaxiesCoords[galaxy2Index].Row && x).Count();
                    var galaxy2Column = galaxiesCoords[galaxy2Index].Column + 999_999L * emptyColumns.Where((x, i) => i < galaxiesCoords[galaxy2Index].Column && x).Count();
                    sum += Math.Abs(galaxy1Row - galaxy2Row) + Math.Abs(galaxy1Column - galaxy2Column);
                }
            }

            Console.WriteLine(sum);
        }
    }
}
