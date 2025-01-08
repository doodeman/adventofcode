namespace Day19;

public class Day19
{
    private static Dictionary<string, long> cache = new();
    public static void Solve()
    {
        (var available, var desired) = ParseInput(File.ReadAllLines("input").ToList());

        long sum = desired.Sum(d => IsPatternPossible("", d, available));
        Console.WriteLine(sum);
    }

    public static long IsPatternPossible(string current, string pattern, List<string> available)
    {
        if (pattern == string.Empty)
            return 1;

        if (cache.TryGetValue(pattern, out long value))
        {
            return value; 
        }
        
        long count = 0; 
        foreach (var avail in available)
        {
            var len = avail.Length;
            if (pattern.Length < len) continue; 
            if (avail == pattern.Substring(0, len))
            {
                count += IsPatternPossible(current + avail, pattern.Substring(len), available);
            }
        }

        cache.Add(pattern, count); 
        return count; 
    }

    //Ret 1: List of available patterns
    //Ret 2: List of desired designs
    public static (List<string>, List<string>) ParseInput(List<string> lines)
    {
        var avail = lines[0].Split(", ");

        lines.RemoveAt(0);
        lines.RemoveAt(0);

        return (avail.ToList(), lines);
    }
}