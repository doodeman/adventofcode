using Shared;

namespace Day12;

public static class HoleFinder
{
    //Finds out if a shape contains holes by "pouring water" (i.e BFS around the edges)
    public static List<List<Coords<bool>>> FindHoles(List<Coords<char>> coords)
    {
        //Do the thing where we blow up an array to 2x size again. Violates DRY but I couldn't possibly care right now. 
        var xMax = coords.Max(c => c.X) + 4;
        var yMax = coords.Max(c => c.Y) + 4;

        var matrix = new bool[yMax, xMax];

        foreach (var c in coords)
        {
            matrix[c.Y, c.X] = true;
            List<(int, int)> extra = new List<(int, int)> {
                (0, 1),
                (1, 0),
                (1, 1),
            };
            foreach (var e in extra)
            {
                matrix[c.Y + e.Item1, c.X + e.Item2] = true;
            }
        }
        bool[,] visited = new bool[matrix.GetLength(0), matrix.GetLength(1)]; 


        //Surprise, it's BFS again. Do a BFS from the edge to find the outline of the shape itself 
        List<Coords<bool>> toVisit = new List<Coords<bool>> { new Coords<bool>(0, 0, false) };
        visited[0, 0] = true;

        //Again, all this repeated use of BFS could probably be functionized so as to not violated DRY but I don't care right now
        int counter = 0; 
        while (toVisit.Count > 0)
        {
            var curr = toVisit.First();
            toVisit.RemoveAt(0);

            counter++; 

            var adjacent = GridUtils.GetAllAdjacentSquares(curr, matrix);
            foreach (Coords<bool> next in adjacent)
            {
                if (visited[next.Y, next.X])
                {
                    continue;
                }
                if (matrix[next.Y, next.X])
                {
                    continue;
                }
                visited[next.Y, next.X] = true;
                toVisit.Add(next);
            }
        }

        //If the union of the outside BFS and the main matrix contains less cells than the matrix size - We have holes inside the shape 
        int allMinusHoles = 0;
        for (int y = 0; y < visited.GetLength(0); y++)
        {
            for (int x = 0; x < visited.GetLength(1); x++)
            {
                if (visited[y, x] || matrix[y,x])
                {
                    allMinusHoles++;
                }
            }
        }

        if (allMinusHoles < yMax * xMax)
        {
            //Find the holes
            (var holeCells, var allHolesMatrix) = FindHolesInShape(matrix, visited);

            //We now have what cells are holes - We need to map out which holes are discrete
            List<List<Coords<bool>>> holes = new List<List<Coords<bool>>>();
            //Start a BFS at a hole cell; Each BFS reveals a discrete hole. Do this until there are no remaining unmapped holes. 
            GridUtils.FlipBoolMatrix(allHolesMatrix);
            while (holes.SelectMany(x => x).Count() < holeCells)
            {
                var holeMatrix = MapShapeBFS(allHolesMatrix);

                var aNewHole = new List<Coords<bool>>();
                for (int y = 0; y < matrix.GetLength(0); y++)
                {
                    for (int x = 0; x < matrix.GetLength(1); x++)
                    {
                        if (holeMatrix[y,x])
                        {
                            aNewHole.Add(new Coords<bool>(x, y, true));
                            allHolesMatrix[y, x] = true; 
                        }
                    }
                }
                holes.Add(aNewHole);
            }
            //We now have a discrete collection of holes. 
            return holes;
        }
        return new List<List<Coords<bool>>>(); 
    }

    public static (int, bool[,]) FindHolesInShape(bool[,] inMatrix, bool[,] inVisited)
    {
        bool[,] matrix = new bool[inMatrix.GetLength(0), inMatrix.GetLength(1)];
        //Flip every bit in the matrix to create a negative image
        for (int y = 0; y < inMatrix.GetLength(0); y++)
        {
            for (int x = 0; x < inMatrix.GetLength(1); x++)
            {
                matrix[y, x] = !inMatrix[y, x];
            }
        }

        var holes = new bool[inMatrix.GetLength(0), inMatrix.GetLength(1)];
        //it's BFS again, this time we map the inside of the shape
        var shape = MapShapeBFS(matrix);
        int foundCount = 0; 
        //We have all the cells in the shape - Anything that isn't outside the shape and part of the shape is a hole 
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                if (!inVisited[y, x] && !shape[y, x])
                {
                    foundCount++;
                    holes[y, x] = true;
                }
            }
        }
        return (foundCount, holes); 
    }

    public static bool[,] MapShapeBFS(bool[,] matrix)
    {
        var startPos = GridUtils.FindStartingPos(matrix);
        List<Coords<bool>> toVisit = new List<Coords<bool>> { startPos };
        var visited = new bool[matrix.GetLength(0), matrix.GetLength(1)];
        visited[startPos.Y, startPos.X] = true;

        while (toVisit.Count > 0)
        {
            var curr = toVisit.First();
            toVisit.RemoveAt(0);


            var adjacent = GridUtils.GetAllAdjacentSquares(curr, matrix);
            foreach (Coords<bool> next in adjacent)
            {
                if (visited[next.Y, next.X])
                {
                    continue;
                }
                if (matrix[next.Y, next.X])
                {
                    continue;
                }
                visited[next.Y, next.X] = true;
                toVisit.Add(next);
            }
        }
        return visited; 
    }

    //used for debugging, currently unreferenced
    public static void VisualizeBFS(bool[,] matrix, bool[,] visited, List<List<Coords<bool>>> holes = null)
    {
        var flatHoles = new List<Coords<bool>>();
        if (holes != null)
        {
            flatHoles = holes.SelectMany(x => x).ToList(); 
        }
        var backgroundColor = Console.BackgroundColor;
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                if (flatHoles.Any(h => h.X == x && h.Y == y))
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write("O");
                }
                else if (visited[y,x])
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(matrix[y, x] ? "X" : " ");
                }
                Console.BackgroundColor = backgroundColor;
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
