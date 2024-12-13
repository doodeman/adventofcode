using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Day13;

public static class MachineSolver
{
    public static (long, long) SolveMachine(Machine machine)
    {
        var button1 = machine.Buttons[0];
        var button2 = machine.Buttons[1];
        Matrix<double> A = DenseMatrix.OfArray(new double[,] 
        {
            { button1.X, button2.X },
            { button1.Y, button2.Y }
        });
        var b = Vector<double>.Build.Dense(new double[] { machine.PrizeX, machine.PrizeY});
        var x = A.Solve(b);

        return (RoundIfNearInteger(x[0]), RoundIfNearInteger(x[1]));
    }

    public static long RoundIfNearInteger(double value, double tolerance = 0.001d)
    {
        double roundedValue = Math.Round(value);
        if (Math.Abs(value - roundedValue) <= tolerance)
        {
            Console.WriteLine($"Value {value} returning {roundedValue}");
            return (long)roundedValue;
        }

        Console.WriteLine($"Value {value} returning -1");
        return -1;
    }
}
