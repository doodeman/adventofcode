namespace AOCTests;


public class ProcessStoneTest
{
    [Fact]
    public void Process_stone_should_return_correct_value()
    {
        var res = Day11.Day11.ProcessStone("2");
        Assert.Single(res);
        Assert.Equal("4048", res[0]);
    }
}
