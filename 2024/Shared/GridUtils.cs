using System.Xml;

namespace Shared;

public static class GridUtils
{
    //Note: only squares in cardinal directions (up, down, left, right)
    public static List<Coords<T>> GetAllAdjacentSquares<T>(Coords<T> coords, T[,] matrix)
    {
        List<Coords<T>> ret = new List<Coords<T>>();
        int width = matrix.GetLength(1);
        int height = matrix.GetLength(0);

        List<(int, int)> directions = new List<(int, int)> {
            (0, 1),
            (0, -1),
            (1, 0),
            (-1, 0)
        };
        foreach (var (dx, dy) in directions)
        {
            int newX = coords.X + dx;
            int newY = coords.Y + dy;
            bool isOrigin = newX == coords.X && newY == coords.Y;
            bool isOutOfLowerBounds = !(newX >= 0 && newY >= 0);
            bool isOutOfHigherBounds = !(newY < height && newX < width);
            bool isValid = !isOrigin && !isOutOfLowerBounds && !isOutOfHigherBounds;
            if (isValid)
                ret.Add(new Coords<T>(newX, newY, matrix[newY, newX]));
        }
        return ret;
    }

    //Counts adjacent squares. Does not count diagonally adjacent. 
    public static int CountAdjacentSquares<T>(Coords<T> c, List<Coords<T>> coords)
    {
        List<(int, int)> squares2Check = new List<(int, int)>
            {
                (c.X+0, c.Y+1),
                (c.X+1, c.Y+0),
                (c.X-1, c.Y+0),
                (c.X+0, c.Y-1)
            };
        return coords.Count(c2 =>
        {
            int counter = 0;
            foreach (var sq in squares2Check)
            {
                if (c2.X == sq.Item1 && c2.Y == sq.Item2)
                    return true;
            }
            return false;
        });
    }

    public static T GetValueInDirection<T>(Coords<T> pos, Direction dir, T[,] matrix)
    {
        return dir switch
        {
            Direction.Up => matrix[pos.Y - 1, pos.X],
            Direction.Down => matrix[pos.Y + 1, pos.X],
            Direction.Left => matrix[pos.Y, pos.X - 1],
            Direction.Right => matrix[pos.Y, pos.X + 1]
        };
    }

    public static (int, int) GetCoordsInDirection<T>(Coords<T> pos, Direction dir)
    {
        return dir switch
        {
            Direction.Up => (pos.Y - 1, pos.X),
            Direction.Down => (pos.Y + 1, pos.X),
            Direction.Left => (pos.Y, pos.X - 1),
            Direction.Right => (pos.Y, pos.X + 1)
        };
    }

    //Gets right from the perspective of direction
    public static Direction GetRight<T>(Coords<T> pos, Direction dir)
    {
        return dir switch
        {
            Direction.Up => Direction.Right,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            Direction.Right => Direction.Down,
        };
    }

    //Gets left from the perspective of direction
    public static Direction GetLeft<T>(Coords<T> pos, Direction dir)
    {
        return dir switch
        {
            Direction.Up => Direction.Left,
            Direction.Down => Direction.Right,
            Direction.Left => Direction.Down,
            Direction.Right => Direction.Up,
        };
    }

    public static Direction GetDirection<T, T2>(Coords<T> pos1, Coords<T2> pos2)
    {
        var xDiff = pos1.X - pos2.X;
        var yDiff = pos1.Y - pos2.Y;

        if (xDiff > 0 && yDiff == 0)
        {
            return Direction.Right; 
        } 
        if (xDiff < 0 && yDiff == 0)
        {
            return Direction.Left;
        }
        if (yDiff < 0 && xDiff == 0)
        {
            return Direction.Up;
        }
        if (yDiff > 0 && xDiff == 0)
        {
            return Direction.Down; 
        }
        throw new Exception("oh no");
    }

    public static void Move<T>(Coords<T> pos, Direction dir)
    {
        var newCoords = dir switch
        {
            Direction.Up => (pos.Y - 1, pos.X),
            Direction.Down => (pos.Y + 1, pos.X),
            Direction.Left => (pos.Y, pos.X - 1),
            Direction.Right => (pos.Y, pos.X + 1),
        };
        pos.X = newCoords.Item2;
        pos.Y = newCoords.Item1;
    }

    //Finds a empty cell
    public static Coords<bool> FindStartingPos(bool[,] matrix)
    {
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                if (!matrix[y, x])
                {
                    return new Coords<bool>(x, y, true);
                }
            }
        }
        throw new Exception("Oh dear");
    }

    public static void FlipBoolMatrix(bool[,] matrix)
    {
        //Flip every bit in the matrix
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                matrix[y, x] = !matrix[y, x];
            }
        }
    }

    public static T[,] CopyMatrix<T>(T[,]matrix)
    {
        var newMatrix = new T[matrix.GetLength(0), matrix.GetLength(1)];
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                newMatrix[y, x] = matrix[y, x];
            }
        }
        return newMatrix;
    }
}
