namespace AOCTests;

public class Day23Test
{
    [Fact]
    public void ParseShouldReturnCorrectValues()
    {
        var lines =  """
                       kh-tc
                       qp-kh
                       de-cg
                       ka-co
                       yn-aq
                       qp-ub
                       cg-tb
                       vc-aq
                       tb-ka
                       wh-tc
                       yn-cg
                       kh-ub
                       ta-co
                       de-co
                       tc-td
                       tb-wq
                       wh-td
                       ta-ka
                       td-qp
                       aq-cg
                       wq-ub
                       ub-vc
                       de-ta
                       wq-aq
                       wq-vc
                       wh-yn
                       ka-de
                       kh-ta
                       co-tc
                       wh-qp
                       tb-vc
                       td-yn
                       """.Split("\n");

        var nodes = Day23.Day23.Parse(lines.ToList());
        
        Assert.True(nodes.ContainsKey("ta"));
        var node = nodes["ta"];
        Assert.Equal(4, node.Links.Count);
    }
}