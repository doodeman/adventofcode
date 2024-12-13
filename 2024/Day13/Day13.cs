namespace Day13;


public static class Day13
{
    public static void Solve(bool part2 = false)
    {
        var machines = Machine.ParseMachines(File.ReadAllLines("input_big").ToList(), part2);

        long tokens = 0;
        foreach(var machine in machines)
        {
            (var aSolve, var bSolve) = MachineSolver.SolveMachine(machine);
            if (aSolve > -1 && bSolve > -1)
            {
                tokens = tokens + (aSolve*3 + bSolve*1);
            }
        }
        Console.WriteLine($"Tokens: {tokens}");
    }
}
