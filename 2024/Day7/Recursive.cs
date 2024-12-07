namespace Day7;

public static class Recursive
{
    public static long Solve(List<Equation> equations)
    {
        List<Equation> valid = new List<Equation>();

        foreach (var equation in equations)
        {
            var results = Calculate(equation.Numbers);
            if (results.Any(r => r == equation.Total))
            {
                valid.Add(equation);
            }
        }
        long sum = 0;
        foreach (var equation in valid)
        {
            sum += equation.Total;
        }
        return sum;
    }

    public static List<long> Calculate(List<long> numbers)
    {
        var results = new List<long> { numbers[0] };
        return Calculate(numbers.Slice(1, numbers.Count - 1), results, new List<Operation> { Operation.Multiply, Operation.Add, Operation.Concat });
    }

    public static List<long> Calculate(List<long> numbers, List<long> results, List<Operation> operations)
    {
        if (numbers.Count == 0)
        {
            return results;
        }
        List<long> newLists = new List<long>();
        foreach (var r in results)
        {
            long number = numbers[0];
            foreach (var o in operations)
            {
                if (o == Operation.Multiply)
                {
                    newLists.Add(number * r);
                }
                if (o == Operation.Add)
                {
                    newLists.Add(number + r);
                }
                if (o == Operation.Concat)
                {
                    newLists.Add(long.Parse(number.ToString() + r.ToString()));
                }
            };
        }
        return Calculate(numbers.Slice(1, numbers.Count - 1), newLists, operations);

    }
}
