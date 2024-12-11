using System.Diagnostics;

namespace Day11;

public static class Day11
{
    public static void Solve()
    {
        var input = "125 17";
        var stones = input.Split(' ').ToList();


        TreeCache cache = new TreeCache(); 

        var sw = new Stopwatch();
        for (int i = 0; i < 25; i++)
        {
            sw.Restart();
            var current = new List<string[]>();


            int j = 0; 
            while (j < stones.Count())
            {
                var cacheResult = cache.Get(stones);

                if(cacheResult.Count > 0)
                {
                    foreach (var result in cacheResult)
                    {
                        current.Add(result);
                        j++;
                    }
                }
                else
                {
                    var s = stones[j];
                    var val = ProcessStone(s);
                    current.Add(val);
                    j++;
                }
            }
            sw.Stop();
            cache.Add(stones, current);
            stones = current.SelectMany(x => x).ToList();
            Console.WriteLine($"Round { i+1 }: Stone count: {stones.Count} {sw.ElapsedMilliseconds}ms");

        }
        Console.WriteLine(stones.Count());
        Console.ReadLine();
    }

    public static string[] ProcessStone(string s)
    {
        if (s == "0")
        {
            return ["1"]; 
        }
        if (s.Length % 2 == 0)
        {
            var s1 = s.Substring(0, s.Length / 2);
            var second = s.Substring(s.Length / 2);
            while (second.Length > 1 && second[0] == '0')
            {
                second = second.Substring(1);
            }
            var s2 = second;
            return [ s1, s2 ];
        }
        var num = long.Parse(s);
        num = num * 2024;
        return [ num.ToString() ];
    }
}
