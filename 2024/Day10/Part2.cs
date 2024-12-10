namespace Day10;

public class Part2
{
    public static int Solve(int[,] matrix)
    {
        var ySize = matrix.GetLength(0);
        var xSize = matrix.GetLength(1);

        int counter = 0;
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; (x < xSize); x++)
            {
                if (matrix[y, x] == 0)
                {
                    counter = counter + ScoreTrailhead(matrix, x, y);
                }
            }
        }
        return counter;
    }

    public static int ScoreTrailhead(int[,] matrix, int x, int y)
    {
        int counter = 0;

        var trails = BFS_AllPossible(matrix, x, y, new Trail(x, y, 0)).Where((t => t.Steps.Last().Val == 9));
        foreach (var item in trails)
        {
            //Visualize(matrix, null, item);
        }
        counter += trails.Count();
        return counter;
    }

    public static List<Trail> BFS_AllPossible(int[,] matrix, int x, int y, Trail trail)
    {
        var newStep = new TrailStep(x, y, matrix[y, x]);
        trail.Steps.Add(newStep);
        if (matrix[y, x] == 9)
        {
            return new List<Trail> { trail };
        }

        var retList = new List<Trail>();
        List<Coords> adjacent = new List<Coords>
        {
            new Coords(x+1,y),
            new Coords(x-1,y),
            new Coords(x,y-1),
            new Coords(x,y+1)
        };
        foreach (var next in adjacent)
        {
            bool notOutOfBoundsY = next.Y < matrix.GetLength(0) && next.Y >= 0;
            bool notOutOfBoundsX = next.X < matrix.GetLength(1) && next.X >= 0;
            if (notOutOfBoundsY && notOutOfBoundsX)
            {
                bool isValueOneGreaterThanCurrent = matrix[next.Y, next.X] == matrix[y, x] + 1;
                bool isLocationAlreadyVisited = trail.Contains(next.X, next.Y);
                bool isLocationViable = !isLocationAlreadyVisited && isValueOneGreaterThanCurrent;
                if (isLocationViable)
                {
                    retList.AddRange(BFS_AllPossible(matrix, next.X, next.Y, new Trail(trail)));
                }
            }
        }
        return retList;
    }
}
