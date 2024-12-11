using System.Collections.Concurrent;

namespace Day11;

public class TreeCache
{    
    private ConcurrentDictionary<string, Node> nodes = new ConcurrentDictionary<string, Node>();
    private bool disable = false;
    public long cacheHits = 00;
    public int longestCacheHit = 0; 

    public TreeCache(bool disable)
    {
        this.disable = disable;
    }

    public void Add(List<string> key, List<string[]> values)
    {
        if (disable || !key.Any())
        {
            return;
        }

        var keyQueue = new Queue<string>(key);
        var valueQueue = new Queue<string[]>(values);

        var rootKey = keyQueue.Dequeue();
        var rootValue = valueQueue.Dequeue();

        if (!nodes.TryGetValue(rootKey, out var currentNode))
        {
            currentNode = new Node(rootKey, rootValue);
            nodes.TryAdd(rootKey, currentNode);
        }

        while (keyQueue.Any())
        {
            var nextKey = keyQueue.Dequeue();
            var nextValue = valueQueue.Dequeue();

            // Look for existing child
            var matchingChild = currentNode.Children.FirstOrDefault(child => child.Key == nextKey);

            if (matchingChild == null)
            {
                // Check if node exists in global dictionary
                if (!nodes.TryGetValue(nextKey, out matchingChild))
                {
                    // Create new node if it doesn't exist anywhere
                    matchingChild = new Node(nextKey, nextValue);
                    nodes.TryAdd(rootKey, currentNode);
                }
                currentNode.Children.Add(matchingChild);
            }

            currentNode = matchingChild;
        }
    }

    /*
     * Gets the value from the cache. 
     * If the value only partially exists, returns the partial value and the depth of the hit.
     */
    public (List<string[]> values, int matchedCount) Get(List<string> key)
    {
        var keyQ = new Queue<string>(key);

        if (disable || !keyQ.Any())
        {
            return (new List<string[]>(), 0);
        }

        var currKey = keyQ.Dequeue(); 
        if (!nodes.TryGetValue(currKey, out var node))
        {
            return (new List<string[]>(), 0);
        }

        cacheHits++;
        Queue<string[]> ret = new Queue<string[]>();
        ret.Enqueue(node.Val);
        int matchCount = 1;

        while (keyQ.Any())
        {
            currKey = keyQ.Dequeue();
            node = node.Children.FirstOrDefault(node => node.Key == currKey);
            if (node != null)
            {
                ret.Enqueue(node.Val);
                matchCount++;
            }
            else
            {
                break;
            }
        }
        if (matchCount > longestCacheHit)
        {
            longestCacheHit = matchCount;
        }
        return (ret.ToList(), matchCount);
    }
}

public class Node
{
    public Node(string key, string[] val)
    {
        Key = key; 
        Val = val;
        Children = new List<Node>();
    }
    public string Key { get; set; }
    public string[] Val { get;set;}

    public List<Node> Children { get; set; }
}