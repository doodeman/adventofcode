namespace Day23;

public static class Day23
{
    public static void Solve()
    {
        var nodes = Parse(File.ReadAllLines("input_smol").ToList());

        var sets = new List<List<Node>>(); 
        foreach (var node in nodes.Values)
        {
            sets.Add(FindInterconnectedSets(node));    
        }
        
        Console.WriteLine(Day1(sets));

        var password = string.Join(',', sets.OrderByDescending(s => s.Count)
            .First()
            .Select(s => s.Name)
            .OrderBy(s => s)); 
        Console.WriteLine(password);
    }

    public static int Day1(List<List<Node>> sets)
    {
        var filtered = sets
                .Where(s => s.Count == 3)
                .Where(s => s.Any(x => x.Name[0] == 't'));

        return filtered.Count(); 
    }

    public static List<Node> FindInterconnectedSets(Node root)
    {   
        var nodes = new List<Node> { root };
        foreach (var link in root.Links)
        {
            if (nodes.All(n => n.IsLinked(link)))
            {
                nodes.Add(link);
            }
        }

        return nodes;
    }

    public static Dictionary<string, Node> Parse(List<string> lines)
    {
        var nodes = new Dictionary<string, Node>();
        foreach (var line in lines)
        {
            if (line.Length == 0) continue;
            var parts = line.Split('-');
            foreach (var part in parts)
            {
                if (!nodes.ContainsKey(part))
                {
                    nodes.Add(part, new Node(part));
                }
            }

            foreach (var part in parts)
            {
                var node = nodes[part];
                node.Links .AddRange(parts
                    .Where(p => p != part && !node.Links.Any(n => n.Name == p))
                    .Select(p => nodes[p])
                    .ToList());
            }
            
        }
        return nodes;
    }
}

public class Node
{
    public string Name { get; set; }
    public List<Node> Links { get; set; }

    public Node(string name)
    {
        Links = new List<Node>();
        Name = name;
    }

    public bool IsLinked(Node other)
    {
        return Links.Any(n => n.Name == other.Name);
    }
}