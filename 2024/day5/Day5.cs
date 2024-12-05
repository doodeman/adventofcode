using System.Collections.Generic;

namespace day5;

public static class Day5
{
    public static void SolveDay5()
    {
        var (pairs, sequences) = ParseFile("input");

        var rules = pairs.Select(p => new Rule { Before = p.Item1, After = p.Item2 }).ToList();

        var correctSequences = new List<List<int>>();
        var incorrectSequences = new List<List<int>>();
        // for every update
        foreach (var list in sequences)
        {
            bool isCorrect = true;
            // for every page P in update 
            foreach (var (page, i) in list.Select((page, index) => (page, index)))
            {
                if (!isCorrect)
                {
                    break;
                }
                var pageRules = rules.Where(r => r.Before == page).ToList();
                foreach (var rule in pageRules)
                {
                    if (list.Contains(rule.After) && list.Contains(rule.Before))
                    {
                        if (list.IndexOf(rule.After) < list.IndexOf(rule.Before))
                        {
                            isCorrect = false;
                        }
                    }
                }
            }
            if (isCorrect)
            {
                correctSequences.Add(list);
            }
            else
            {
                incorrectSequences.Add(FixSequence(list, rules));
            }
        }
        Console.WriteLine(MiddleSum(correctSequences));
        Console.WriteLine(MiddleSum(incorrectSequences));
    }

    public static int MiddleSum(List<List<int>> sequences)
    {
        int middleSum = 0;
        foreach (var sequence in sequences)
        {
            var middleIndex = (int)Math.Floor((decimal)(sequence.Count / 2));
            middleSum = middleSum + sequence[middleIndex];
        }
        return middleSum;
    }

    public static List<int> FixSequence(List<int> list, List<Rule> rules)
    {

        foreach (var (page, i) in list.Select((page, index) => (page, index)))
        {
            var pageRules = rules.Where(r => r.Before == page).ToList();
            foreach (var rule in pageRules)
            {
                if (list.Contains(rule.After) && list.Contains(rule.Before))
                {
                    var afterIndex = list.IndexOf(rule.After);
                    var beforeIndex = list.IndexOf(rule.Before);
                    if (afterIndex < beforeIndex)
                    {
                        list[afterIndex] = rule.Before; 
                        list[beforeIndex] = rule.After;
                        return FixSequence(list, rules);
                    }
                }
            }
        }
        return list;
    }

    public static (List<(int, int)> pairs, List<List<int>> sequences) ParseFile(string filePath)
    {
        var pairs = new List<(int, int)>();
        var sequences = new List<List<int>>();
        bool parsingPairs = true;

        // Read all lines from the file
        string[] lines = File.ReadAllLines(filePath);
        
        foreach (string line in lines)
        {
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(line))
            {
                parsingPairs = false;  // Empty line indicates transition to sequences
                continue;
            }

            if (parsingPairs)
            {
                // Parse pairs section
                var parts = line.Split('|', StringSplitOptions.TrimEntries);
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int first) &&
                    int.TryParse(parts[1], out int second))
                {
                    pairs.Add((first, second));
                }
            }
            else
            {
                // Parse sequences section
                var numbers = line.Split(',', StringSplitOptions.TrimEntries)
                                .Select(s => int.TryParse(s, out int num) ? num : throw new FormatException($"Invalid number in sequence: {s}"))
                                .ToList();
                sequences.Add(numbers);
            }
        }

        return (pairs, sequences);
    }
}

public class Rule
{
    public int Before { get; set; }
    public int After { get;set; }
}
