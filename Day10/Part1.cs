namespace Day10
{
    static internal class Part1
    {
        public static void Execute()
        {
            var map = File.ReadAllLines("input.txt");

            (int, int) startingPoint = (-1, -1);
            for (int row = 0; row < map.Length; row++)
            {
                var startingColumnIndex = map[row].IndexOf('S');
                if (startingColumnIndex != -1)
                {
                    startingPoint = (row, startingColumnIndex);
                    break;
                }
            }

            var previousPoint = startingPoint;
            var currentPoint = (-1, -1);
            var totalLoopSize = 0;

            if (startingPoint.Item1 > 0 && new[] { '7', '|', 'F' }.Contains(map[startingPoint.Item1 - 1][startingPoint.Item2])) {
                currentPoint = (startingPoint.Item1 - 1, startingPoint.Item2);
            } else if (startingPoint.Item2 < map[startingPoint.Item1].Length - 1 && new[] { 'J', '-', '7' }.Contains(map[startingPoint.Item1][startingPoint.Item2 + 1]))
            {
                currentPoint = (startingPoint.Item1, startingPoint.Item2 + 1);
            } else if (startingPoint.Item1 < map.Length - 1 && new[] { 'J', '|', 'L' }.Contains(map[startingPoint.Item1 + 1][startingPoint.Item2]))
            {
                currentPoint = (startingPoint.Item1 + 1, startingPoint.Item2);
            } else if (startingPoint.Item2 > 0)
            {
                currentPoint = (startingPoint.Item1, startingPoint.Item2 - 1);
            }

            do
            {
                var tempCurrentPoint = currentPoint;
                currentPoint = map[currentPoint.Item1][currentPoint.Item2] switch
                {
                    '|' when currentPoint.Item1 - 1 == previousPoint.Item1 => (currentPoint.Item1 + 1, currentPoint.Item2),
                    '|' when currentPoint.Item1 + 1 == previousPoint.Item1 => (currentPoint.Item1 - 1, currentPoint.Item2),
                    '-' when currentPoint.Item2 - 1 == previousPoint.Item2 => (currentPoint.Item1, currentPoint.Item2 + 1),
                    '-' when currentPoint.Item2 + 1 == previousPoint.Item2 => (currentPoint.Item1, currentPoint.Item2 - 1),
                    'L' when currentPoint.Item1 - 1 == previousPoint.Item1 => (currentPoint.Item1, currentPoint.Item2 + 1),
                    'L' when currentPoint.Item2 + 1 == previousPoint.Item2 => (currentPoint.Item1 - 1, currentPoint.Item2),
                    'J' when currentPoint.Item1 - 1 == previousPoint.Item1 => (currentPoint.Item1, currentPoint.Item2 - 1),
                    'J' when currentPoint.Item2 - 1 == previousPoint.Item2 => (currentPoint.Item1 - 1, currentPoint.Item2),
                    '7' when currentPoint.Item1 + 1 == previousPoint.Item1 => (currentPoint.Item1, currentPoint.Item2 - 1),
                    '7' when currentPoint.Item2 - 1 == previousPoint.Item2 => (currentPoint.Item1 + 1, currentPoint.Item2),
                    'F' when currentPoint.Item1 + 1 == previousPoint.Item1 => (currentPoint.Item1, currentPoint.Item2 + 1),
                    'F' when currentPoint.Item2 + 1 == previousPoint.Item2 => (currentPoint.Item1 + 1, currentPoint.Item2),
                };
                previousPoint = tempCurrentPoint;
                totalLoopSize++;
            } while (currentPoint != startingPoint);

            Console.WriteLine(totalLoopSize / 2 + 1);
        }
    }
}
