using Day12;
using Shared;

namespace AOCTests;

public class Day12Test
{
    [Fact]
    public void GetRegionShouldOnlyAddEachOnce()
    {
        char[,] matrix = new char[3,3];
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                matrix[y, x] = 'A';
            }
        }

        bool[,] visited = new bool[3,3];

        var region = Day12.Day12.GetRegion(matrix, visited, 0, 0);

        Assert.Equal(9, region.Coords.Count());
    }

    [Fact]
    public void GetRegionValueShouldGiveCorrectValues()
    {
        var coords = new List<Coords<char>>
        {
            new Coords<char>(0, 0, 'a'),
            new Coords<char>(1, 0, 'a'),
            new Coords<char>(0, 1, 'a'),
            new Coords<char>(-1, 0, 'a'),
            new Coords<char>(0, -1, 'a'),
        };

        var value = Day12.Day12.GetRegionValue(coords);

        Assert.Equal(60, value);
    }

    [Fact] 
    public void GetRegionCircumferenceShouldGiveCorrectValue()
    {
        var coords = new List<Coords<char>>
        {
            new Coords<char>(0, 0, 'a'),
            new Coords<char>(0, 1, 'a'),
            new Coords<char>(1, 1, 'a'),
            new Coords<char>(1, 0, 'a'),
        };

        var circumference = Day12.Day12.GetRegionCircumference(coords);
        Assert.Equal(8, circumference);
    }
}
