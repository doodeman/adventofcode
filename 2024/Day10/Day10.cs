namespace Day10;

public class Day10
{
    public static void Solve()
    {
        var matrix = Parse("input_big");
        var score = Part1.Solve(matrix);
        Console.WriteLine($"Pt 1 {score}");
        var score2 = Part2.Solve(matrix);
        Console.WriteLine($"Pt 2 {score2}");
    }

    public static int[,] Parse(string input)
    {
        var lines = File.ReadAllLines(input);
        var ySize = lines.Length;
        var xSize = lines[0].Length;

        var ret = new int[ySize, xSize];

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                var c = lines[y][x].ToString();
                if (c == ".")
                {
                    ret[y, x] = -1;
                }
                else
                {
                    ret[y, x] = int.Parse(lines[y][x].ToString());
                }
            }
        }
        return ret; 
    }
}