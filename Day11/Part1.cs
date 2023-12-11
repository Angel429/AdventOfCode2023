namespace Day10
{
    static internal class Part1
    {
        public static void Execute()
        {
            var image = File.ReadLines("input.txt").Select(x => x.ToCharArray().ToList()).ToList();

            var emptyRows = Enumerable.Repeat(true, image.Count).ToArray();
            var emptyColumns = Enumerable.Repeat(true, image[0].Count).ToArray();

            for (int rowIndex = 0; rowIndex < emptyRows.Length; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < emptyColumns.Length; columnIndex++)
                {
                    if (image[rowIndex][columnIndex] == '#')
                    {
                        emptyRows[rowIndex] = false;
                        emptyColumns[columnIndex] = false;
                    }
                }
            }

            foreach (var row in image)
            {
                for (int columnIndex = row.Count - 1; columnIndex >= 0; columnIndex--)
                {
                    if (emptyColumns[columnIndex])
                    {
                        row.Insert(columnIndex, '.');
                    }
                }
            }

            for (var rowIndex = emptyRows.Length - 1; rowIndex >= 0; rowIndex--)
            {
                if (emptyRows[rowIndex])
                {
                    image.Insert(rowIndex, Enumerable.Repeat('.', image[0].Count).ToList());
                }
            }

            var galaxiesCoords = new List<(int Row, int Column)>();

            for (int rowIndex = 0; rowIndex < image.Count; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < image[0].Count; columnIndex++)
                {
                    if (image[rowIndex][columnIndex] == '#')
                    {
                        galaxiesCoords.Add((rowIndex, columnIndex));
                    }
                }
            }

            var sum = 0;

            for (int galaxy1Index = 0; galaxy1Index < galaxiesCoords.Count; galaxy1Index++)
            {
                for (int galaxy2Index = galaxy1Index + 1; galaxy2Index < galaxiesCoords.Count; galaxy2Index++)
                {
                    sum += Math.Abs(galaxiesCoords[galaxy1Index].Row - galaxiesCoords[galaxy2Index].Row) + Math.Abs(galaxiesCoords[galaxy1Index].Column - galaxiesCoords[galaxy2Index].Column);
                }
            }

            Console.WriteLine(sum);
        }
    }
}
