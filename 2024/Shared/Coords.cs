namespace Shared;

public class Coords<T>
{
    public int X { get; set; }
    public int Y { get; set; }
    public T Value { get; set; }
    public Coords(int x, int y, T value)
    {
        X = x;
        Y = y;
        Value = value;
    }
}
