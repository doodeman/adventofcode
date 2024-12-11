using Xunit;

namespace Day11.Tests;

public class Day11Tests
{
    [Fact]
    public void Add_SingleLevel_CanRetrieve()
    {
        // Arrange
        var cache = new TreeCache();
        var key = new List<string> { "root" };
        var values = new List<string[]> { new[] { "value1", "value2" } };

        // Act
        cache.Add(key, values);
        var result = cache.Get(key.ToList());

        // Assert
        Assert.Single(result);
        Assert.Equal(2, result[0].Length);
        Assert.Equal("value1", result[0][0]);
        Assert.Equal("value2", result[0][1]);
    }

    [Fact]
    public void Preexisting_Child_Insert()
    {
        // Arrange
        var cache = new TreeCache();
        var key = new List<string> { "root", "child", "grandchild" };
        var values = new List<string[]> {
            new[] { "value1" },
            new[] { "value2" },
            new[] { "value3" }
        };

        var key2 = new List<string> { "child", "anewchild" };
        var values2 = new List<string[]>
        {
            new[] { "value2" },
            new[] { "value4" }
        };

        // Act
        cache.Add(key, values);
        cache.Add(key2, values2); 
        var result = cache.Get(key.ToList());
        var result2 = cache.Get(key2.ToList()); 

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal("value1", result[0][0]);
        Assert.Equal("value2", result[1][0]);
        Assert.Equal("value3", result[2][0]);

        Assert.Equal("value2", result2[0][0]);
        Assert.Equal("value4", result2[1][0]);
    }

    [Fact]
    public void Add_MultipleLevel_CanRetrieveComplete()
    {
        // Arrange
        var cache = new TreeCache();
        var key = new List<string> { "root", "child", "grandchild" };
        var values = new List<string[]> {
            new[] { "value1" },
            new[] { "value2" },
            new[] { "value3" }
        };

        // Act
        cache.Add(key, values);
        var result = cache.Get(key.ToList());

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal("value1", result[0][0]);
        Assert.Equal("value2", result[1][0]);
        Assert.Equal("value3", result[2][0]);
    }

    [Fact]
    public void Get_PartialPath_ReturnsPartialResult()
    {
        // Arrange
        var cache = new TreeCache();
        var key = new List<string> { "root", "child", "grandchild" };
        var values = new List<string[]> {
            new[] { "value1" },
            new[] { "value2", "value2.1" },
            new[] { "value3" }
        };
        cache.Add(key, values);

        // Act
        var searchKey = new List<string>{ "root", "child" };
        var result = cache.Get(searchKey.ToList());

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("value1", result[0][0]);
        Assert.Equal("value2", result[1][0]);
        Assert.Equal("value2.1", result[1][1]);
    }

    [Fact]
    public void Get_NonexistentPath_ReturnsEmptyResult()
    {
        // Arrange
        var cache = new TreeCache();
        var key = new List<string> { "root", "child" };
        var values = new List<string[]> {
            new[] { "value1" },
            new[] { "value2" }
        };
        cache.Add(key, values);

        // Act
        var searchKey = new Stack<string>(new[] { "nonexistent", "path" });
        var result = cache.Get(searchKey.ToList());

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Add_MultipleValuesPerNode_PreservesAllValues()
    {
        // Arrange
        var cache = new TreeCache();
        var key = new List<string> { "root", "child" };
        var values = new List<string[]> {
            new[] { "value1", "value1.1", "value1.2" },
            new[] { "value2", "value2.1", "value2.2" }
        };

        // Act
        cache.Add(key, values);
        var result = cache.Get(key.ToList());

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(3, result[0].Length);
        Assert.Equal(3, result[1].Length);
        Assert.Equal("value1", result[0][0]);
        Assert.Equal("value1.1", result[0][1]);
        Assert.Equal("value1.2", result[0][2]);
        Assert.Equal("value2", result[1][0]);
        Assert.Equal("value2.1", result[1][1]);
        Assert.Equal("value2.2", result[1][2]);
    }
}
