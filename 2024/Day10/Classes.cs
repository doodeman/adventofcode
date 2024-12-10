namespace Day10;

public class Trail
{
    public Trail(int x, int y, int val)
    {
        Steps = new List<TrailStep> { new TrailStep(x, y, val) };
    }

    public Trail(Trail trail)
    {
        Steps = new List<TrailStep>();
        Steps.AddRange(trail.Steps);
    }

    public List<TrailStep> Steps { get; set; }
    public bool Contains(int x, int y)
    {
        return Steps.Any(s => s.X == x && s.Y == y);
    }
    public bool Successful()
    {
        return Steps.Any(s => s.Val == 9);
    }
}

public class Coords
{
    public int X { get; set; }
    public int Y { get; set; }
    public Coords(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class TrailStep
{
    public TrailStep(int x, int y, int val)
    {
        X = x;
        Y = y;
        Val = val;
    }
    public int X { get; set; }
    public int Y { get; set; }
    public int Val { get; set; }
}