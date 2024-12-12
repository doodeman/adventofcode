using Shared;

namespace AOCTests;

public class GridUtilsTest
{
    [Fact]
    public void GetAllAdjacentSquaresShouldReturnCorrectValues()
    {
        var matrix = new bool[3, 3];
        var result = GridUtils.GetAllAdjacentSquares(new Coords<bool>(1, 1, false), matrix);
         
        Assert.Equal(8, result.Count);
        Assert.Contains(result, c => c.X == 0 && c.Y == 0);
        Assert.Contains(result, c => c.X == 2 && c.Y == 2);
        Assert.Contains(result, c => c.X == 0 && c.Y == 1);
        Assert.Contains(result, c => c.X == 0 && c.Y == 0);
    }

    [Fact] 
    public void GetAllAdjacentSquaresShouldNotReturnOOB()
    {
        var matrix = new bool[2, 2];
        var result = GridUtils.GetAllAdjacentSquares(new Coords<bool>(0, 0, false), matrix);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void CountAdjacentSquaresShouldReturnCorrectValue()
    {
        List<Coords<bool>> coords = new List<Coords<bool>>();
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                coords.Add(new Coords<bool>(x, y, true));
            }
        }

        var result = GridUtils.CountAdjacentSquares(new Coords<bool>(1, 1, true), coords);
        Assert.Equal(4, result);

        var coordsList = new List<Coords<char>>
        {
            new Coords<char>(0, 0, 'a'),
            new Coords<char>(0, 1, 'a'),
            new Coords<char>(1, 0, 'a'),
            new Coords<char>(1, 1, 'a'),
        };


    }
}
