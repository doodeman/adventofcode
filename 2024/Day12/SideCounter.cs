using Shared;

namespace Day12;

public static class SideCounter
{
    public static int CountSides(List<Coords<char>> coords)
    {
        coords = coords.Select(c =>
        {
            c.X = (c.X+1) * 2;
            c.Y = (c.Y+1) * 2;
            return c;
        }).ToList();

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
            foreach(var e in extra)
            {
                matrix[c.Y+e.Item1,c.X+e.Item2] = true;
            }
        }
        var edgeCount = WalkTheLilGuy(matrix);

        //find holes in the shape and then walk them
        var holes = HoleFinder.FindHoles(coords);
        var internalSides = 0; 
        foreach(var hole in holes)
        {
            bool[,] holeMatrix = new bool[yMax, xMax];
            //GridUtils.FlipBoolMatrix(holeMatrix);
            foreach (var cell in hole)
            {
                holeMatrix[cell.Y, cell.X] = true;
            }
            var sides = WalkTheLilGuy(holeMatrix);
            internalSides = internalSides + sides;
        }

        if (internalSides > 0)
        {
            //Visualize(matrix);
            //Console.WriteLine($"Adding {internalSides} extra internal sides");
        }
        
        return edgeCount + internalSides; 
    }

    private static int WalkTheLilGuy(bool[,] matrix)
    {
        //The lil guy wants to keep the "wall" of the shape on his right hand at all times
        //He will always attempt to turn right if possible. 
        //If he cannot turn right, he will try to forward
        //If he cannot go forward, he will go left. 
        //Every time he changes direction, increment counter by one. 
        //Once the lil guy reaches his starting point, he takes a nap. 
        
        int counter = 0;

        var direction = Direction.Up; 
        var lilGuy = FindStartingPos(matrix);
        var startX = lilGuy.X;
        var startY = lilGuy.Y;

        //Visualize(matrix, lilGuy);

        var newDirection = MoveTheLilGuy(matrix, lilGuy, direction); 
        if (newDirection != direction)
        {
            counter++; 
        }
        direction = newDirection;

        //Visualize(matrix, lilGuy);

        while (!(lilGuy.X == startX && lilGuy.Y == startY))
        {
            newDirection = MoveTheLilGuy(matrix, lilGuy, direction);
            if (newDirection != direction)
            {
                counter++;
            }
            direction = newDirection;
            //Visualize(matrix, lilGuy);
        }
        return counter; 
    }

    //Returns the direction he moved in
    private static Direction MoveTheLilGuy(bool[,] matrix, Coords<bool> lilGuy, Direction currDir)
    {
        var dir = currDir; 
        //If the lil guy does not have a block to his right, he will turn to the right. 
        if (!GridUtils.GetValueInDirection(lilGuy, GridUtils.GetRight(lilGuy, currDir), matrix)) 
        {
             dir = GridUtils.GetRight(lilGuy, currDir);
        }
        //If the lil guy has a block to his front, he will try to turn to the right, and then the left 
        else if (GridUtils.GetValueInDirection(lilGuy, currDir, matrix))
        {
            if (!GridUtils.GetValueInDirection(lilGuy, GridUtils.GetRight(lilGuy, currDir), matrix))
            {
                dir = GridUtils.GetRight(lilGuy, currDir); 
            }
            else
            {
                dir = GridUtils.GetLeft(lilGuy, currDir);
            }
        }
        //If the lil guy has a block to his right and no block to his front, he will happily walk forward and dir is not changed
        GridUtils.Move(lilGuy, dir);
        return dir; 
    }


    private static Coords<bool> FindStartingPos(bool[,] matrix)
    {
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                if (matrix[y,x])
                {
                    while (matrix[y, x])
                    {
                        x--;
                    }
                    return new Coords<bool>(x, y, true);
                }
            }
        }
        throw new Exception("Oh dear");
    }

    private static void Visualize(bool[,] matrix, Coords<bool> lilGuy = null)
    {
        Console.Clear();
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                if (lilGuy != null && x == lilGuy.X && y == lilGuy.Y)
                {
                    Console.Write('\u263A');
                }
                else
                {
                    Console.Write(matrix[y, x] ? "X" : " ");
                }
                
            }
            Console.WriteLine();
        }

        Thread.Sleep(10);
    }
}
