using System.Diagnostics;

namespace Day11;

public static class Day11
{
    public static int ThreadCount = 8; 
    public static void Solve()
    {
        var input = "3279 998884 1832781 517 8 18864 28 0";
        var stonesList = input.Split(' ').ToList();

        var stones = new Dictionary<string, long>(); 
        foreach (var stone in stonesList)
        {
            if (stones.TryGetValue(stone, out var s))
            {
                stones[stone] = stones[stone] + 1; 
            }
            else
            {
                stones[stone] = 1;
            }
        }

        //TreeCache cache = new TreeCache(false); 
        Dictionary<string, string[]> cache = new Dictionary<string, string[]>(); 

        var sw = new Stopwatch();
        long count = 0;
        for (int i = 0; i < 75; i++)
        {
            sw.Restart();

            (count, var result) = ProcessStones2(stones, cache);
            stones = result; 

            sw.Stop();
            Console.WriteLine($"Round {i + 1}: Stone count: {count} {sw.ElapsedMilliseconds}ms");
        }
        Console.WriteLine(count);
        Console.ReadLine();
    }

    public static (long, Dictionary<string, long>) ProcessStones2(Dictionary<string, long> stones, Dictionary<string, string[]> cache)
    {
        Dictionary<string, long> results = new Dictionary<string, long>();
        long stoneCount = 0; 
        foreach(var stone in stones.Keys)
        {
            string[] res; 
            if (cache.TryGetValue(stone, out var processedStone)) 
            {
                res = processedStone;
            }
            else
            {
                res = ProcessStone(stone);
                cache.Add(stone, res);
            }
            AddOrIncrementDict(res, results, stones[stone]);
        }
        foreach (var s in results.Keys)
        {
            stoneCount = stoneCount + results[s];
        }
        return (stoneCount, results);
    }

    public static void AddOrIncrementDict(string[] stones, Dictionary<string, long> dict, long count)
    {
        foreach (var stone in stones)
        {
            if (dict.ContainsKey(stone))
            {
                dict[stone] = dict[stone] + count;
            }
            else
            {
                dict[stone] = count;
            }
        }
        
    }

    public static List<string> ProcessStones(List<string> stones, TreeCache cache)
    {
        var current = new List<string[]>(stones.Count);
        int j = 0;
        while (j < stones.Count())
        {
            var (cacheResult, matchedCount) = cache.Get(stones.Skip(j).ToList());

            if (cacheResult.Count > 0)
            {
                current.AddRange(cacheResult);
                j += matchedCount;
            }
            else
            {
                current.Add(ProcessStone(stones[j]));
                j++;
            }
        }
        cache.Add(stones, current);
        var processedStones = new List<string>(current.Count * 2);
        foreach (var s in current)
        {
            processedStones.AddRange(s);
        }
        stones = current.SelectMany(x => x).ToList();
        (stones, processedStones) = (processedStones, stones);
        return stones; 
    }

    public static string[] ProcessStone(string s)
    {
        if (s == "0")
        {
            return new[] { "1" };
        }

        if (s.Length % 2 == 0)
        {
            var midPoint = s.Length / 2;
            var s1 = s[..midPoint];
            var s2 = s[midPoint..].TrimStart('0');
            return new[] { s1, s2.Length == 0 ? "0" : s2 };
        }

        return new[] { (long.Parse(s) * 2024).ToString() };
    }
}
