namespace day4;

public static class Day4pt2
{
    public static void Solve()
    {
        var matrix = ParseFile("input");
        var xmasCounter = 0;
        for (int x = 0; x < matrix[0].Count; x++)
        {
            for (int y = 0; y < matrix.Count; y++)
            {
                if (matrix[y][x] == 'A')
                {
                    var cross = GetCross(x, y, matrix);
                    if (cross.All(s => s == "MAS" || s == "SAM"))
                    {
                        xmasCounter = xmasCounter + 1; 
                    }
                }
            }
        }
        Console.WriteLine(xmasCounter);
    }

    public static List<string> GetCross(int pX, int pY, List<List<char>> matrix)
    {
        var right = GetDiagonalRight(pX - 1, pY - 1, matrix);
        var left = GetDiagonalLeft(pX + 1, pY - 1, matrix);
        List<string> strs = [
            right,
            left
         ];
        return strs;
    }

    public static string GetDiagonalRight(int xStart, int yStart, List<List<char>> matrix)
    {
        int xLength = matrix[0].Count;
        int yLength = matrix.Count;
        if (xStart < 0 || xStart > xLength || yStart < 0 || yStart > yLength)
        {
            return "";
        }
        var str = "";
        for (int i = 0; i < 3; i++)
        {
            if (xStart + i >= xLength || yStart + i >= yLength)
            {
                break;
            }
            str = str + matrix[yStart + i][xStart + i];
        }
        return str;
    }

    public static string GetDiagonalLeft(int xStart, int yStart, List<List<char>> matrix)
    {
        int xLength = matrix[0].Count;
        int yLength = matrix.Count;
        if (xStart < 0 || xStart >= xLength || yStart < 0 || yStart > yLength)
        {
            return "";
        }
        var str = "";
        for (int i = 0; i < 3; i++)
        {
            if (xStart - i < 0 || yStart + i >= yLength)
            {
                break;
            }
            str = str + matrix[yStart + i][xStart - i];
        }
        return str;
    }

    public static List<List<char>> ParseFile(string filePath)
    {
        // Initialize the result list
        var result = new List<List<char>>();

        try
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            // Process each line
            foreach (string line in lines)
            {
                var charList = new List<char>();

                // Convert each character in the line to a list entry
                foreach (char c in line.Trim())
                {
                    charList.Add(c);
                }

                // Add the line's character list to the result
                result.Add(charList);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            throw;
        }

        return result;
    }
}
