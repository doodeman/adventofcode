namespace day4;

public static class Day4
{
    public static void Solve()
    {
        var matrix = ParseFile("input");
        var strs = new List<string>();
        for (int x = 0; x < matrix[0].Count; x++)
        {
            for (int y = 0; y < matrix.Count; y++)
            {
                if (matrix[y][x] == 'X')
                {
                    strs.AddRange(GetAllSurrounding(x, y, matrix));
                }
            }
        }
        strs = strs.Where(s => s == "XMAS" || s == "SAMX").ToList();
        Console.WriteLine(strs.Count());
    }

    public static List<string> GetAllSurrounding(int pX, int pY, List<List<char>> matrix)
    {
        List<string> strs = [
            GetHorizontal(pX, pY, matrix),
            GetHorizontal(pX-3, pY, matrix),
            GetVertical(pX, pY, matrix),
            GetVertical(pX, pY-3, matrix),
            GetDiagonalRight(pX, pY, matrix),
            GetDiagonalRight(pX - 3, pY - 3, matrix),
            GetDiagonalLeft(pX, pY, matrix),
            GetDiagonalLeft(pX + 3, pY - 3, matrix)
         ];
        return strs;
    }

    public static string GetHorizontal(int xStart, int pY, List<List<char>> matrix)
    {
        List<char> line = matrix[pY];
        if (xStart < 0 || xStart >= line.Count)
        {
            return ""; 
        }
        var str = ""; 
        for(int x = xStart; x < xStart + 4; x++)
        {
            if (x >= line.Count)
            {
                break;
            }
            str = str + line[x];
        }
        return str; 
    }

    public static string GetVertical(int pX, int yStart, List<List<char>> matrix)
    {
        if (yStart < 0 || yStart >= matrix.Count)
        {
            return ""; 
        }
        var str = ""; 
        for (int y = yStart; y < yStart + 4; y++)
        {
            if (y >= matrix.Count)
            {
                break;
            }
            str = str + matrix[y][pX];
        }
        return str; 
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
        for (int i = 0; i < 4; i++)
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
        for (int i = 0; i < 4; i++)
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
