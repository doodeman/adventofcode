namespace Day7;

public static class Day7
{
    public static void Solve()
    {
        var equations = ParseInputFile("input");
        Console.WriteLine($"recursive {Recursive.Solve(equations)}");
        Console.WriteLine($"Iterative {Iterative.Solve(equations)}");
        Console.ReadLine();
    }

    public static List<Equation> ParseInputFile(string filePath)
    {
        var equations = new List<Equation>();

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(':');
                if (parts.Length != 2)
                    continue;

                var equation = new Equation();

                if (long.TryParse(parts[0].Trim(), out long total))
                {
                    equation.Total = total;
                }
                else
                {
                    continue; 
                }

                string[] numberStrings = parts[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (string numStr in numberStrings)
                {
                    if (long.TryParse(numStr, out long number))
                    {
                        equation.Numbers.Add(number);
                    }
                }

                equations.Add(equation);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Input file not found");
        }

        return equations;
    }
}