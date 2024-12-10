namespace Day10;

public static class Part1
{
    public static int Solve(int[,] matrix)
    {
        var ySize = matrix.GetLength(0);
        var xSize = matrix.GetLength(1);

        List<(int, int)> destinations = new List<(int, int)>();
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; (x < xSize); x++)
            {
                if (matrix[y, x] == 9)
                {
                    destinations.Add((x, y));
                }
            }
        }

        int counter = 0;
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; (x < xSize); x++)
            {
                if (matrix[y, x] == 0)
                {
                    counter = counter + ScoreTrailhead(matrix, x, y, destinations);
                }
            }
        }
        return counter;
    }

    public static int ScoreTrailhead(int[,] matrix, int x, int y, List<(int, int)> destinations)
    {
        int counter = 0;
        foreach (var destination in destinations)
        {
            if (BFS(matrix, x, y, destination.Item1, destination.Item2))
            {
                counter++;
            }
        }
        return counter;
    }

    public static bool BFS(int[,] matrix, int x, int y, int tX, int tY)
    {
        int[,] visited = new int[matrix.GetLength(0), matrix.GetLength(1)];

        bool found = false;
        List<Coords> toVisit = new List<Coords> { new Coords(x, y) };
        while (toVisit.Count > 0 && !found)
        {
            var curr = toVisit.First();
            toVisit.RemoveAt(0);

            visited[curr.Y, curr.X] = 1;
            if (curr.X == tX && curr.Y == tY)
            {
                found = true;
                break;
            }

            List<Coords> adjacent = new List<Coords>
            {
                new Coords(curr.X-1,curr.Y),
                new Coords(curr.X+1,curr.Y),
                new Coords(curr.X  ,curr.Y-1),
                new Coords(curr.X  ,curr.Y+1)
            };
            foreach (Coords next in adjacent)
            {
                bool notOutOfBoundsY = next.Y < matrix.GetLength(0) && next.Y >= 0;
                bool notOutOfBoundsX = next.X < matrix.GetLength(1) && next.X >= 0;
                if (notOutOfBoundsY && notOutOfBoundsX)
                {
                    bool isValueOneGreaterThanCurrent = matrix[next.Y, next.X] == matrix[curr.Y, curr.X] + 1;
                    bool isLocationAlreadyVisited = visited[next.Y, next.X] == 0;
                    bool isLocationViable = isLocationAlreadyVisited && isValueOneGreaterThanCurrent;
                    if (isLocationViable)
                    {
                        toVisit.Add(next);
                    }
                }

            }
        }
        return found;
    }
}
