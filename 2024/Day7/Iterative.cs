namespace Day7;

public static class Iterative
{
    public static long Solve(List<Equation> equations)
    {
        var valid = new List<Equation>();
        foreach (var equation in equations)
        {
            var num = new List<long>();
            num.AddRange(equation.Numbers);


            var permutations = GetPermutations(equation.Numbers.Count());

            foreach (var permutation in permutations)
            {
                long result = equation.Numbers[0];
                for (int i = 1; i < equation.Numbers.Count(); i++)
                {
                    if (permutation[i - 1] == Operation.Multiply)
                    {
                        result = result * equation.Numbers[i];
                    }
                    if (permutation[i - 1] == Operation.Add)
                    {
                        result = result + equation.Numbers[i];
                    }
                    if (permutation[i - 1] == Operation.Concat)
                    {
                        result = long.Parse(result.ToString() + equation.Numbers[i].ToString());
                    }
                }
                if (result == equation.Total)
                {
                    valid.Add(equation);
                    break;
                }
            }
        }
        long sum = 0;
        foreach (var equation in valid)
        {
            sum += equation.Total;
        }
        return sum;
    }

    public static List<List<Operation>> GetPermutations(int n)
    {
        return GetPermutations(0, n, new List<Operation> { Operation.Multiply, Operation.Add, Operation.Concat }, new List<List<Operation>>());
    }

    public static List<List<Operation>> GetPermutations(int current, int n, List<Operation> operations, List<List<Operation>> lists)
    {
        //Trivial case - n = 1: Return a.Count lists with one of each element
        if (current == n)
        {
            return lists;
        }

        if (current == 1)
        {
            var ret = new List<List<Operation>>();
            operations.ForEach(x => ret.Add(new List<Operation> { x }));
            return GetPermutations(current + 1, n, operations, ret);
        }

        var newLists = new List<List<Operation>>();
        foreach (var list in lists)
        {
            foreach (var operation in operations)
            {
                var newList = new List<Operation>();
                newList.AddRange(list);
                newList.Add(operation);
                newLists.Add(newList);
            }
        }

        return GetPermutations(current + 1, n, operations, newLists);
    }
}
