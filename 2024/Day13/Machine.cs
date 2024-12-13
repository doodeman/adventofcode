namespace Day13;

public class Machine
{
    public Machine()
    {
        Buttons = new List<Button>(); 
    }
    public List<Button> Buttons { get; set; }
    public long PrizeX { get; set; }
    public long PrizeY { get; set; }

    public static List<Machine> ParseMachines(List<string> input, bool part2)
    {
        var machines = new List<Machine>();
        var machine = new Machine();
        foreach (var line in input)
        {
            if (line.Length == 0)
                continue;
            if (line[0] == 'B')
            {
                var button = new Button();
                var split = line.Split(" ");
                button.X = int.Parse(split[2].Split("+")[1].Split(",")[0]); //lmao
                button.Y = int.Parse(split[3].Split("+")[1].Split(",")[0]);
                machine.Buttons.Add(button);
            }
            else
            {
                var split = line.Split(" ");
                machine.PrizeX = int.Parse(split[1].Split("=")[1].Split(",")[0]);
                machine.PrizeY = int.Parse(split[2].Split("=")[1].Split(",")[0]);
                if (part2)
                {
                    machine.PrizeX = machine.PrizeX + 10000000000000;
                    machine.PrizeY = machine.PrizeY + 10000000000000;
                }
                machines.Add(machine);
                machine = new Machine();
            }
        }
        return machines;
    }
}

public class Button
{
    public long X { get; set; }
    public long Y { get; set; }
}