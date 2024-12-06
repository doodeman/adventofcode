using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Day6;

public class Day6
{
    private int GuardX { get; set; }
    private int GuardY { get; set; }
    private Direction GuardDirection { get; set; }
    private int[,] Grid { get; set; }
    private int[,] Visited { get; set; }
    private int IterationCount { get; set; }
    private bool EnableVisualization = true;
    private bool Part1 = false; 


    public void Solve()
    {
        var result = ParseGrid(File.ReadAllLines("input_real"));
        if (Part1)
        {
            Grid = result.Grid;
            RunSimulation(result);
            Console.WriteLine(CountVisited());
        }
        else
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int obstacleCount = 0;
            int runs = 0;
            List<(int, int)> addedObstacles = new List<(int, int)> ();
            int totalRuns = result.Grid.GetLength(0) * result.Grid.GetLength(1);
            Console.WriteLine(totalRuns);
            for (int y = 0; y < result.Grid.GetLength(0); y++)
            {
                for (int x = 0; x < result.Grid.GetLength(1); x++)
                {
                    if (result.Grid[y,x] == 0)
                    {
                        Grid = (int[,])result.Grid.Clone();
                        Grid[y, x] = 1;
                        var infinite = RunSimulation(result); 
                        if (infinite)
                        {
                            addedObstacles.Add((y, x));
                            obstacleCount++;
                        }
                        runs++;
                        if (runs % 100 == 0)
                        {
                            Console.WriteLine(runs);
                        }
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine(obstacleCount);
            //Visualize(addedObstacles);
        }
        Console.ReadLine();
    }

    private bool RunSimulation(GridParseResult? result)
    {
        Visited = new int[Grid.GetLength(0), Grid.GetLength(1)];
        GuardX = result.GuardX;
        GuardY = result.GuardY;
        GuardDirection = result.Direction;
        IterationCount = 0;

        Visited[GuardY, GuardX] = 1;
        //Visualize();
        while (true)
        {
            if (IterationCount > 15000)
            {
                return true; 
            }
            //Visualize();
            var (fX, fY) = GetForwardSquare();
            if (IsOffTheMap(fX, fY))
            {
                break;
            }
            int turnAttempts = 0; 
            while (IsObstacle(fX, fY))
            {
                GuardDirection = Turn();
                turnAttempts++; 
                (fX, fY) = GetForwardSquare();
                if (turnAttempts > 4)
                {
                    Visualize();
                }
                
            }
            GuardX = fX;
            GuardY = fY;
            Visited[GuardY, GuardX] = 1;
            IterationCount++;
        }
        return false; 
    }

    private bool IsObstacle(int x, int y)
    {
        return Grid[y, x] == 1;
    }

    private (int x, int y) GetForwardSquare()
    {
        return GuardDirection switch
        {
            Direction.Up => (GuardX, GuardY - 1),
            Direction.Down => (GuardX, GuardY + 1),
            Direction.Left => (GuardX-1, GuardY),
            Direction.Right => (GuardX+1, GuardY)
        };
    }

    private bool IsOffTheMap(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Grid.GetLength(1) || y >= Grid.GetLength(0))
            return true;
        return false; 
    }

    private Direction Turn()
    {
        return GuardDirection switch
        {
            Direction.Up => Direction.Right,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            Direction.Right => Direction.Down
        };
    }

    private static GridParseResult ParseGrid(string[] lines)
    {
        if (lines == null || lines.Length == 0)
            throw new ArgumentException("Input cannot be null or empty");

        int height = lines.Length;
        int width = lines[0].Length;

        if (lines.Any(line => line.Length != width))
            throw new ArgumentException("All lines must have the same length");

        var result = new GridParseResult
        {
            Grid = new int[height, width]
        };

        // Parse the grid
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                char currentChar = lines[y][x];

                // Check for obstacles
                if (currentChar == '#')
                {
                    result.Grid[y, x] = 1;
                }
                // Check for direction symbols
                else if (IsDirectionSymbol(currentChar))
                {
                    result.GuardX = x;
                    result.GuardY = y; 
                    result.Direction = ParseDirection(currentChar);
                }
                // Everything else is 0
                else
                {
                    result.Grid[y, x] = 0;
                }
            }
        }

        return result;
    }
    private static bool IsDirectionSymbol(char c)
    {
        return c == '^' || c == 'v' || c == '<' || c == '>';
    }

    private static Direction ParseDirection(char c)
    {
        if (c == '^')
            return Direction.Up;
        if (c == 'v')
            return Direction.Down;
        if (c == '>')
            return Direction.Right;
        if (c == '<')
            return Direction.Left;
        return Direction.Up;
    }

    private static char DirectionToChar(Direction d)
    {
        return d switch { Direction.Up => '^', Direction.Down => 'v', Direction.Right => '>', Direction.Left => '<' };
    }

    private int CountVisited()
    {
        int count = 0; 
        for (int y = 0; y < Visited.GetLength(0); y++)
        {
            for (int x = 0; x < Visited.GetLength(1); x++)
            {
                if (Visited[y, x] == 1)
                    count++;
            }
        }
        return count; 
    }

    private void Visualize(List<(int, int)> addedObstacles = null)
    {
        if (!EnableVisualization)
            return;
        for (int y = 0; y < Grid.GetLength(0); y++)
        {
            for (int x = 0; x < Grid.GetLength(1); x++)
            {
                int value = Grid[y, x];
                if (y == GuardY && x == GuardX)
                {
                    Console.Write(DirectionToChar(GuardDirection));
                }
                else
                {
                    var printVal = value == 0 ? '.' : 'O';
                    if (addedObstacles != null && addedObstacles.Contains((y, x)))
                    {
                        printVal = 'X';
                    }
                    Console.Write(printVal);
                }
            }
            Console.WriteLine(); // New line after each row
        }
        Console.WriteLine($"Iteration {IterationCount}");
        Console.ReadLine();
        Console.Clear();
    }
}

public class GridParseResult
{
    public int[,] Grid { get; set; }
    public int GuardX { get; set; }
    public int GuardY { get; set; }
    public Direction Direction { get; set; }
}

public enum Direction
{
    Up, Left, Right, Down
}