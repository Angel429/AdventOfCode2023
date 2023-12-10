namespace Day10
{
    static internal class Part2
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
            (int, int) currentPoint;

            if (startingPoint.Item1 > 0 && new[] { '7', '|', 'F' }.Contains(map[startingPoint.Item1 - 1][startingPoint.Item2]))
            {
                currentPoint = (startingPoint.Item1 - 1, startingPoint.Item2);
            }
            else if (startingPoint.Item2 < map[startingPoint.Item1].Length - 1 && new[] { 'J', '-', '7' }.Contains(map[startingPoint.Item1][startingPoint.Item2 + 1]))
            {
                currentPoint = (startingPoint.Item1, startingPoint.Item2 + 1);
            }
            else if (startingPoint.Item1 < map.Length - 1 && new[] { 'J', '|', 'L' }.Contains(map[startingPoint.Item1 + 1][startingPoint.Item2]))
            {
                currentPoint = (startingPoint.Item1 + 1, startingPoint.Item2);
            }
            else if (startingPoint.Item2 > 0)
            {
                currentPoint = (startingPoint.Item1, startingPoint.Item2 - 1);
            } else
            {
                throw new Exception("Whut?");
            }

            var visitedPoints = new HashSet<(int, int)>
            {
                startingPoint,
                currentPoint
            };

            while (currentPoint != startingPoint)
            {
                var lastPoint = currentPoint;
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
                previousPoint = lastPoint;
                visitedPoints.Add(currentPoint);
            }

            var startingPointConnectsToUp = startingPoint.Item1 > 0 && visitedPoints.Contains((startingPoint.Item1 - 1, startingPoint.Item2)) && new[] { '7', '|', 'F' }.Contains(map[startingPoint.Item1 - 1][startingPoint.Item2]);
            var startingPointConnectsToRight = startingPoint.Item2 < map[startingPoint.Item1].Length - 1 && visitedPoints.Contains((startingPoint.Item1, startingPoint.Item2 + 1)) && new[] { 'J', '-', '7' }.Contains(map[startingPoint.Item1][startingPoint.Item2 + 1]);
            var startingPointConnectsToDown = startingPoint.Item1 < map.Length - 1 && visitedPoints.Contains((startingPoint.Item1 + 1, startingPoint.Item2)) && new[] { 'L', '|', 'J' }.Contains(map[startingPoint.Item1 + 1][startingPoint.Item2]);
            var startingPointConnectsToLeft = startingPoint.Item2 > 0 && visitedPoints.Contains((startingPoint.Item1, startingPoint.Item2 - 1)) && new[] { 'F', '-', 'L' }.Contains(map[startingPoint.Item1][startingPoint.Item2 - 1]);

            if (startingPointConnectsToUp)
            {
                if (startingPointConnectsToRight)
                {
                    map[startingPoint.Item1] = map[startingPoint.Item1].Replace('S', 'L');
                } else if (startingPointConnectsToDown)
                {
                    map[startingPoint.Item1] = map[startingPoint.Item1].Replace('S', '|');
                } else
                {
                    map[startingPoint.Item1] = map[startingPoint.Item1].Replace('S', 'J');
                }
            } else if (startingPointConnectsToRight)
            {
                if (startingPointConnectsToDown)
                {
                    map[startingPoint.Item1] = map[startingPoint.Item1].Replace('S', 'F');
                } else if (startingPointConnectsToLeft)
                {
                    map[startingPoint.Item1] = map[startingPoint.Item1].Replace('S', '-');
                }
            } else
            {
                map[startingPoint.Item1] = map[startingPoint.Item1].Replace('S', '7');
            }

            var tilesEnclosed = 0;
            for (int row = 0; row < map.Length; row++)
            {
                for (int column = 0; column < map[row].Length; column++)
                {
                    if (!visitedPoints.Contains((row, column)))
                    {
                        var oddParts = 0;
                        var lastEdge = '\0';

                        var loopPartsUp = 0;
                        for (int newRow = row - 1; newRow >= 0; newRow--)
                        {
                            if (visitedPoints.Contains((newRow, column)))
                            {
                                if (map[newRow][column] == '-')
                                {
                                    loopPartsUp++;
                                    lastEdge = '\0';
                                } else if (new[] {'J', 'L' }.Contains(map[newRow][column]))
                                {
                                    lastEdge = map[newRow][column];
                                } else if ((lastEdge == 'J' && map[newRow][column] == 'F') || (lastEdge == 'L' && map[newRow][column] == '7'))
                                {
                                    loopPartsUp++;
                                    lastEdge = '\0';
                                } else if (map[newRow][column] != '|')
                                {
                                    lastEdge = '\0';
                                }
                            }
                        }
                        if (loopPartsUp % 2 != 0) oddParts++;

                        lastEdge = '\0';
                        var loopPartsRight = 0;
                        for (int newColumn = column + 1; newColumn < map[row].Length; newColumn++)
                        {
                            if (visitedPoints.Contains((row, newColumn)))
                            {
                                if (map[row][newColumn] == '|')
                                {
                                    loopPartsRight++;
                                    lastEdge = '\0';
                                }
                                else if (new[] { 'L', 'F' }.Contains(map[row][newColumn]))
                                {
                                    lastEdge = map[row][newColumn];
                                }
                                else if ((lastEdge == 'L' && map[row][newColumn] == '7') || (lastEdge == 'F' && map[row][newColumn] == 'J'))
                                {
                                    loopPartsRight++;
                                    lastEdge = '\0';
                                }
                                else if (map[row][newColumn] != '-')
                                {
                                    lastEdge = '\0';
                                }
                            }
                        }
                        if (loopPartsRight % 2 != 0) oddParts++;

                        lastEdge = '\0';
                        var loopPartsDown = 0;
                        for (int newRow = row + 1; newRow < map.Length; newRow++)
                        {
                            if (visitedPoints.Contains((newRow, column)))
                            {
                                if (map[newRow][column] == '-')
                                {
                                    loopPartsDown++;
                                    lastEdge = '\0';
                                }
                                else if (new[] { '7', 'F' }.Contains(map[newRow][column]))
                                {
                                    lastEdge = map[newRow][column];
                                }
                                else if ((lastEdge == '7' && map[newRow][column] == 'L') || (lastEdge == 'F' && map[newRow][column] == 'J'))
                                {
                                    loopPartsDown++;
                                    lastEdge = '\0';
                                }
                                else if (map[newRow][column] != '|')
                                {
                                    lastEdge = '\0';
                                }
                            }
                        }
                        if (loopPartsDown % 2 != 0) oddParts++;

                        lastEdge = '\0';
                        var loopPartsLeft = 0;
                        for (int newColumn = column - 1; newColumn >= 0; newColumn--)
                        {
                            if (visitedPoints.Contains((row, newColumn)))
                            {
                                if (map[row][newColumn] == '|')
                                {
                                    loopPartsLeft++;
                                    lastEdge = '\0';
                                }
                                else if (new[] { 'J', '7' }.Contains(map[row][newColumn]))
                                {
                                    lastEdge = map[row][newColumn];
                                }
                                else if ((lastEdge == 'J' && map[row][newColumn] == 'F') || (lastEdge == '7' && map[row][newColumn] == 'L'))
                                {
                                    loopPartsLeft++;
                                    lastEdge = '\0';
                                }
                                else if (map[row][newColumn] != '-')
                                {
                                    lastEdge = '\0';
                                }
                            }
                        }
                        if (loopPartsLeft % 2 != 0) oddParts++;

                        if (oddParts == 4)
                        {
                            tilesEnclosed++;
                        }
                    }
                }
            }

            Console.WriteLine(tilesEnclosed);
        }
    }
}
