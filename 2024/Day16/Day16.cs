using Shared;

namespace Day16;

public class Day16
{
    public static void Solve()
    {
        (var grid, var start, var end) = Parse("input");
        Console.WriteLine(Search(grid, start, end)); 
    }

    public static int Search(bool[,] grid, Coords<Direction> start, Coords<Direction> end)
    {
        bool[,] visited = new bool[grid.GetLength(0), grid.GetLength(1)];
        int visitedCount = 0; 
        for (var y = 0; y < grid.GetLength(0); y++)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                var val = grid[y, x];
                visited[y,x] = val;
                if (val) 
                    visitedCount++;
            }
        }

        var winners = new List<(int, Coords<Direction>)>(); 
        var toVisit = new List<(int, Coords<Direction>)> { (0, start) }; 
        while(visitedCount < grid.GetLength(0) * grid.GetLength(1))
        {
            if (toVisit.Count == 0)
            {
                return winners.Min(w => w.Item1); 
            }
            var curr = toVisit[0];
            toVisit.RemoveAt(0);

            visited[curr.Item2.Y, curr.Item2.X] = true; 

            if(curr.Item2.Y == end.Y && curr.Item2.X == end.X)
            {
                winners.Add(curr);
            }

            var adjacents = GridUtils.GetAllAdjacentSquares(new Coords<bool>(curr.Item2.X, curr.Item2.Y, true), grid); 
            foreach(var adjacent in adjacents)
            {
                if (visited[adjacent.Y, adjacent.X])
                {
                    continue; 
                }
                var direction = GridUtils.GetDirection(curr.Item2, adjacent);
                int cost = curr.Item1; 
                if (direction == curr.Item2.Value)
                {
                    cost = cost + 1; 
                }
                else
                {
                    cost = cost + 1000; 
                }
                toVisit.Add((cost, new Coords<Direction>(adjacent.X, adjacent.Y, direction)));
            }
            VisualizeBFS(grid, visited);
            Console.ReadLine();
        }
        return winners.Min(w => w.Item1);
    }

    public static (bool[,], Coords<Direction>, Coords<Direction>) Parse(string input)
    {
        var lines = File.ReadAllLines(input);

        var height = lines.Count();
        var width = lines[0].Count();
        Coords<Direction> start = null;
        Coords<Direction> end = null; 
        var grid = new bool[height, width];
        for (var y = 0; y < height; y++) 
        {
            for (var x = 0; x < width; x++)
            {
                if (lines[y][x] == '#')
                {
                    grid[y, x] = true; 
                }
                else
                {
                    grid[y, x] = false;
                }

                if (lines[y][x] == 'S')
                {
                    start = new Coords<Direction>(x, y, Direction.Right); 
                }
                if (lines[y][x] == 'E')
                {
                    end = new Coords<Direction>(x, y, Direction.Right);
                }
            }
        }
        return (grid, start, end);
    }

    public static void VisualizeBFS(bool[,] matrix, bool[,] visited)
    {
        var backgroundColor = Console.BackgroundColor;
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                if (visited[y, x])
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
