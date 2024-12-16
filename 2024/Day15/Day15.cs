using Shared;

namespace Day15;

public class Day15
{
    public static void Solve()
    {
        (var robot, var grid, var moves, var entityList) = Parse("input_big");

        foreach (var move in moves)
        {
            robot.Move(grid, move);
        }
        var gpsSum = entityList.Where(e => e.Type == Type.Box)
                               .Select(e => (e.Pos.Y * 100) + e.Pos.X)
                               .Sum();
        Console.WriteLine(gpsSum);
    }

    public static (Entity, Entity[,], List<Direction>, List<Entity>) Parse(string inputFile)
    {
        var lines = File.ReadAllLines(inputFile);


        var gridLines = lines.Where(l => l.Length > 0 && l[0] == '#').ToList(); 

        var ySize = gridLines.Count();
        var xSize = gridLines[0].Length;
        var entityGrid = new Entity[ySize, xSize];
        var entityList = new List<Entity>(); 
        Entity robot = null; 
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                if (gridLines[y][x] == '#')
                {
                    entityGrid[y, x] = new Entity(x, y, Type.Wall);
                    entityList.Add(entityGrid[y, x]);
                }
                if (gridLines[y][x] == '@')
                {
                    robot = new Entity(x, y, Type.Robot);
                    entityGrid[y, x] = robot;
                    entityList.Add(robot); 
                }
                if (gridLines[y][x] == 'O')
                {
                    entityGrid[y, x] = new Entity(x, y, Type.Box);
                    entityList.Add(entityGrid[y, x]);
                }
            }
        }

        var moveLines = lines.ToList();
        moveLines.RemoveRange(0, gridLines.Count()); 
        var moves = new List<Direction>(); 
        foreach(var line in moveLines)
        {
            foreach (char c in line)
            {
                if (c == '\n')
                {
                    continue;
                }
                moves.Add(c switch
                {
                    '^' => Direction.Up,
                    'v' => Direction.Down,
                    '<' => Direction.Left,
                    '>' => Direction.Right
                });
            }

        }
        return (robot, entityGrid, moves, entityList);
    }

    public static void Visualize(Entity[,] entityGrid, Direction direction)
    {
        Console.WriteLine(direction);

        for (int y = 0; y < entityGrid.GetLength(0); y++)
        {
            for (int x = 0;  x < entityGrid.GetLength(1); x++)
            {
                var val = entityGrid[y,x];
                if (val == null)
                {
                    Console.Write(".");
                    continue; 
                }
                if (val.Type == Type.Robot)
                {
                    Console.Write("@");
                }
                if (val.Type == Type.Wall)
                {
                    Console.Write("#");
                }
                if (val.Type == Type.Box)
                {
                    Console.Write("O");
                }
            }
            Console.WriteLine("\n");
        }
        Console.ReadLine(); 
    }
}

public class Entity
{
    public Entity (int x, int y, Type type)
    {
        Pos = new Coords<Entity>(x, y, this); 
        Type = type; 
    }
    public Coords<Entity> Pos { get; set; }

    public Type Type { get; set; }

    public bool Move(Entity[,] grid, Direction dir)
    {
        if (Type == Type.Wall)
        {
            return false; 
        }
        var thingInWay = GridUtils.GetValueInDirection(Pos, dir, grid);
        bool pushed = true; 
        if (thingInWay != null)
        {
            pushed = thingInWay.Move(grid, dir);
        }
        if (pushed)
        {
            (var y, var x) = GridUtils.GetCoordsInDirection(Pos, dir);
            grid[Pos.Y, Pos.X] = null; 
            Pos.X = x;
            Pos.Y = y;
            grid[y, x] = this;
        }
        return pushed; 
    }

}

public enum Type
{
    Robot, Box, Wall
}