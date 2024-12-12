using Shared;
namespace Day12;

public class Day12
{
    public static void Solve()
    {
        var matrix = Parse("input");

        var regions = GetRegions(matrix);

        var regionsSum = regions.Select(r => GetRegionValue(r.Coords)).Sum();
        Console.WriteLine($"Part 1: {regionsSum}");
        var regionsSumPart2 = regions.Select(r => GetRegionValue(r.Coords, true)).Sum();
        Console.WriteLine($"Part 2: {regionsSumPart2}");
    }

    public static char[,] Parse(string inputFile)
    {
        var lines = File.ReadAllLines(inputFile);
        var ret = new char[lines.Length, lines[0].Length];
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                ret[y,x] = lines[y][x];
            }
        }
        return ret;
    }

    public static List<Region> GetRegions(char[,] matrix)
    {
        var visited = new bool[matrix.GetLength(0), matrix.GetLength(1)];
        var regions = new List<Region>();
        int id = 32; 
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                if (!visited[y,x])
                {
                    regions.Add(GetRegion(matrix, visited, x, y));
                }
            }
        }
        return regions; 
    }

    public static Region GetRegion(char[,] matrix, bool[,] visited, int xStart, int yStart)
    {
        var region = new Region(matrix[yStart, xStart]);

        //Oh yeah you know it it's BFS time baybeeee
        List<Coords<char>> toVisit = new List<Coords<char>> { new Coords<char>(xStart, yStart, matrix[yStart, xStart]) };
        visited[yStart, xStart] = true; 
        while (toVisit.Count > 0)
        {
            var curr = toVisit.First();
            toVisit.RemoveAt(0);

            region.Coords.Add(curr);

            var adjacent = GridUtils.GetAllAdjacentSquares(curr, matrix);
            foreach (Coords<char> next in adjacent)
            {
                if (visited[next.Y, next.X])
                {
                    continue; 
                }
                if (matrix[next.Y, next.X] != region.Value)
                {
                    continue;
                }
                if (region.Coords.Any(x => next.X == x.X && next.Y == x.Y))
                {
                    continue; 
                }
                visited[next.Y, next.X] = true;
                toVisit.Add(next);
            }
        }
        return region;
    }

    public static int GetRegionValue(List<Coords<char>> squares, bool part2 = false)
    {
        if (part2)
        {
            return SideCounter.CountSides(squares) * squares.Count();
        }
        return GetRegionCircumference(squares) * squares.Count(); 
    }

    public static int GetRegionCircumference(List<Coords<char>> squares)
    {
        return squares.Select(c =>
        {
            var adjacent = GridUtils.CountAdjacentSquares(c, squares);
            return 4 - adjacent;
        }).Sum(); 
    }
}

public class Region
{
    public Region(char value)
    {
        Coords = new List<Coords<char>>();
        Value = value;
    }
    public char Value { get; set; }
    public List<Coords<char>> Coords { get; set; }
}
