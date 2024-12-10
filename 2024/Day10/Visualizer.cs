namespace Day10;

public static class Visualizer
{
    public static void Visualize(int[,] matrix, int[,] visited = null, Trail trail = null)
    {
        Console.WriteLine();
        var ySize = matrix.GetLength(0);
        var xSize = matrix.GetLength(1);
        var originalColor = Console.ForegroundColor;
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; (x < xSize); x++)
            {
                if (visited != null && visited[y, x] != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                if (trail != null && trail.Contains(x, y))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                if (matrix[y, x] == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                if (matrix[y, x] == 9)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (matrix[y, x] == -1)
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write(matrix[y, x]);
                }

                Console.ForegroundColor = originalColor;
            }
            Console.WriteLine();
        }
    }
}
