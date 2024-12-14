using Day14;
using NuGet.Frameworks;

namespace AOCTests;

public class Day14Test
{
    [Fact]
    public void GetQuadrantsShouldReturnCorrectValues()
    {
        var quadrants = Quadrant.GetQuadrants(11, 7);

        Assert.Equal(4, quadrants.Count);
        Assert.Equal(0, quadrants[0].XMin);
        Assert.Equal(0, quadrants[0].YMin);
        Assert.Equal(5, quadrants[0].XMax);
        Assert.Equal(3, quadrants[0].YMax);

        Assert.Equal(0, quadrants[1].XMin);
        Assert.Equal(4, quadrants[1].YMin);
        Assert.Equal(5, quadrants[1].XMax);
        Assert.Equal(7, quadrants[1].YMax);

        Assert.Equal(6, quadrants[2].XMin);
        Assert.Equal(0, quadrants[2].YMin);
        Assert.Equal(11,quadrants[2].XMax);
        Assert.Equal(3, quadrants[2].YMax);

        Assert.Equal(6, quadrants[3].XMin);
        Assert.Equal(4, quadrants[3].YMin);
        Assert.Equal(11,quadrants[3].XMax);
        Assert.Equal(7, quadrants[3].YMax);
    }

    [Fact]
    public void RobotShouldIterateCorrectly()
    {
        int width = 11;
        int height = 7; 
        var robot = new Robot(new Coords(2, 4), new Coords(2, -3));
        Assert.True(new Coords(2, 4).Equals(robot.Pos));
        robot.Iterate(width, height);
        Assert.True(new Coords(4, 1).Equals(robot.Pos));
        robot.Iterate(width, height);
        Assert.True(new Coords(6, 5).Equals(robot.Pos));
        robot.Iterate(width, height);
        Assert.True(new Coords(8, 2).Equals(robot.Pos)); 
        robot.Iterate(width, height);
        Assert.True(new Coords(10, 6).Equals(robot.Pos));
        robot.Iterate(width, height);
        Assert.True(new Coords(1, 3).Equals(robot.Pos));
        
    }

    [Fact]
    public void RunShouldReturnCorrectValues()
    {
        var robots = Day14.Day14.Parse(File.ReadAllLines("input").ToList());
        //part 1
        int width = 11;
        int height = 7;

        Day14.Day14.Run(width, height, 100, robots);

        int[,] robotCount = new int[height, width];
        
        foreach(var robot in robots)
        {
            robotCount[robot.Pos.Y, robot.Pos.X] = robotCount[robot.Pos.Y, robot.Pos.X] + 1;
        }

        Assert.Equal(2, robotCount[0, 6]);
        Assert.Equal(1, robotCount[0, 9]);
        Assert.Equal(1, robotCount[2, 0]);
    }
}
