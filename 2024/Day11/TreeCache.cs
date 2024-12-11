namespace Day11;

public class TreeCache
{    
    private Dictionary<string, Node> nodes = new Dictionary<string, Node>();
    private bool disable = true; 


    public TreeCache()
    {
    }

    public void Add(List<string> key, List<string[]> values)
    {
        if (disable)
        {
            return; 
        }
        var keyStack = new Queue<string>(key);
        var valueStack = new Queue<string[]>(values);
        Add(keyStack, valueStack);
    }

    private List<Node> Add(Queue<string> key, Queue<string[]> value)
    {
        if (disable || !key.Any())
        {
            return new List<Node>();
        }
        var currKey = key.Dequeue();
        var currValue = value.Dequeue();


        //See if our key has a node, else, create it
        if (!nodes.TryGetValue(currKey, out var currNode))
        {
            currNode = new Node(currKey, currValue);
            nodes.Add(currKey, currNode);
            currNode.Children = Add(key, value, currNode);
            return new List<Node> { currNode };
        }
        currNode.Children = Add(key, value, currNode);
        return new List<Node> { currNode };
    }

    private List<Node> Add(Queue<string> key, Queue<string[]> value, Node currNode)
    {
        if (disable || !key.Any())
        {
            return new List<Node>(); 
        }
        var currKey = key.Dequeue();
        var currValue = value.Dequeue();

        //See if our node has children matching the next key
        var matchingChild = currNode.Children.FirstOrDefault(exists => exists.Key == currKey);
        if (matchingChild == null)
        {
            //If we don't have a child matching our key, see if there is a node for that key
            if (nodes.TryGetValue(currKey, out matchingChild))
            {
                //link the preexisting node
                currNode.Children.Add(matchingChild);
            }
        }
        if (matchingChild != null)
        {
            matchingChild.Children = Add(key, value, matchingChild);
            return new List<Node> { matchingChild };
        }
        else
        {
            var newChild = new Node(currKey, currValue);
            nodes.Add(currKey, newChild);
            currNode.Children.Add(newChild);

            var children = Add(key, value, newChild);
            newChild.Children = children;
            return currNode.Children;
        }
    }

    /*
     * Gets the value from the cache. 
     * If the value only partially exists, returns the partial value and the depth of the hit.
     */
    public List<string[]> Get(List<string> key)
    {
        var keyQ = new Queue<string>(key);

        var currKey = keyQ.Dequeue(); 
        Node node;
        if (disable || !nodes.TryGetValue(currKey, out node))
        {
            return new List<string[]>(); 
        }
        Queue<string[]> ret= new Queue<string[]>();
        ret.Enqueue(node.Val);
        while (keyQ.Any())
        {
            currKey = keyQ.Dequeue();
            node = node.Children.FirstOrDefault(node => node.Key == currKey);
            if (node != null)
            {
                ret.Enqueue(node.Val);
            }
            else
            {
                break; 
            }
        }
        return ret.ToList(); 
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