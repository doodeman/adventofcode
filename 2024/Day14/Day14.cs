using Shared;
using System.Numerics;

namespace Day14;

public static class Day14
{
    public static void Solve()
    {
        //Solve("input", 11, 7);
        Solve("input_big", 101, 103);
    }

    public static void Solve(string inputFile, int width, int height)
    {
        var robots = Parse(File.ReadAllLines(inputFile).ToList());

        Run(width, height, int.MaxValue, robots);
        var count = CountRobots(robots, width, height);
        Console.WriteLine($"Part 1 {count}");
    }

    public static int CountRobots(List<Robot> robots, int width, int height)
    {
        List<int> quadrantCounts = new List<int>();
        foreach(var q in Quadrant.GetQuadrants(width, height))
        {
            int qCount = 0; 
            foreach(var r in robots)
            {
                if (r.Pos.X >= q.XMin && r.Pos.X < q.XMax
                 && r.Pos.Y >= q.YMin && r.Pos.Y < q.YMax)
                {
                    qCount++; 
                }
            }
            quadrantCounts.Add(qCount);
        }

        int ret = quadrantCounts[0]; 
        for (int i = 1; i < quadrantCounts.Count; i++)
        {
            ret = ret * quadrantCounts[i];
        }
        return ret; 
    }

    public static void Run(int width, int height, int iterations, List<Robot> robots)
    {
        var strings = new List<string>(); 
        for (int i = 0; i < iterations; i++)
        {
            robots.ForEach(robot => robot.Iterate(width, height));
            Visualize2String(robots, width, height, i);
            strings.Add(" ");
        }
        //File.WriteAllLines("output", strings.ToArray());
    }

    public static void Visualize2String(List<Robot> robots, int width, int height, int inum)
    {
        /*
        var xList = robots.Select(x => x.Pos.X).Distinct().ToList();
        xList.Sort();
        var maxContX = FindLongestContiguousSequence(xList);

        var yList = robots.Select(y => y.Pos.Y).Distinct().ToList(); 
        yList.Sort();
        var maxContY = FindLongestContiguousSequence(yList);
        */

        Dictionary<(int, int), bool> positions = new Dictionary<(int, int), bool>();
        foreach (Robot robot in robots)
        {
            positions[(robot.Pos.Y, robot.Pos.X)] = true;
        }



        if (positions.Values.Count == 500)
        {
            Console.Clear();
            Console.WriteLine(inum);
            List<string> strings = new List<string>();
            for (int y = 0; y < height; y++)
            {
                var s = "";
                for (int x = 0; x < width; x++)
                {
                    if (robots.Any(r => r.Pos.X == x && r.Pos.Y == y))
                    {
                        s = s + "X";
                    }
                    else
                    {
                        s = s + " ";
                    }
                }
                strings.Add(s);
            }
            strings.ForEach(s => Console.WriteLine(s));
            Console.ReadLine();
        }
    }

    public static List<Robot> Parse(List<string> lines)
    {
        var robots = new List<Robot>(); 
        foreach(var line in lines)
        {
            if (line.Length == 0)
            {
                continue; 
            }
            var split = line.Split(' ');

            var posSplit = split[0].Split('=')[1].Split(',');
            var velSplit = split[1].Split('=')[1].Split(',');
            var xVel = int.Parse(velSplit[0]);
            var yVel = int.Parse(velSplit[1]);
            var xPos = int.Parse(posSplit[0]);
            var yPos = int.Parse(posSplit[1]);

            robots.Add(new Robot(new Coords(xPos, yPos), new Coords(xVel, yVel)));
        }
        return robots; 
    }

    public static int FindLongestContiguousSequence(List<int> sortedList)
    {
        if (sortedList == null || sortedList.Count() == 0)
            return 0;

        int maxLength = 1;
        int currentLength = 1;
        int maxStart = 0;
        int currentStart = 0;

        // Iterate through the array looking for sequences where next number = current + 1
        for (int i = 1; i < sortedList.Count(); i++)
        {
            if (sortedList[i] == sortedList[i - 1] + 1)
            {
                currentLength++;

                // Update max if current sequence is longer
                if (currentLength > maxLength)
                {
                    maxLength = currentLength;
                    maxStart = currentStart;
                }
            }
            else
            {
                // Reset sequence tracking when we find a gap
                currentLength = 1;
                currentStart = i;
            }
        }

        return maxLength; 
    }

}


public class Robot
{
    public Robot(Coords pos, Coords vel)
    {
        Pos = pos; 
        Vel = vel;
    }
    public Coords Pos { get; set; }
    public Coords Vel { get; set; }

    public void Iterate(int width, int height)
    {
        Pos.X = WrapAroundCorrection((Pos.X + Vel.X), width-1);
        Pos.Y = WrapAroundCorrection((Pos.Y + Vel.Y), height-1); 
    }

    private int WrapAroundCorrection(int x, int max)
    {
        int ret = x; 
        if (x < 0)
        {
            return max+1 + x; 
        }
        if (x > max)
        {
            return x % max-1; 
        }
        return x; 
    }
}

public class Coords
{
    public int X;
    public int Y;
    public Coords(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Coords other)
    {
        return other.X == X && other.Y == Y;
    }
}

public class Quadrant
{
    public int XMin;
    public int YMin;
    public int XMax;
    public int YMax; 

    public Quadrant(int xMin, int yMin, int xMax, int yMax)
    {
        XMin = xMin;
        YMin = yMin;
        XMax = xMax;
        YMax = yMax;
    }

    public static List<Quadrant> GetQuadrants(int width, int height)
    {
        List<Quadrant> quadrants = new List<Quadrant>();
        var xModifier = width / 2;
        var yModifier = height / 2;
        for (int x = 0; x <= 1; x++)
        {
            for (int y = 0; y <= 1; y++)
            {
                quadrants.Add(new Quadrant
                (
                    0 + x * (xModifier + 1),
                    0 + y * (yModifier + 1),
                    x == 0 ? xModifier : width,
                    y == 0 ? yModifier: height
                ));
            }
        }
        return quadrants; 
    }
}