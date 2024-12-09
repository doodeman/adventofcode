
namespace Day9;

public static class Day9
{
    public static void Do()
    {
        //string input = "2333133121414131402"; 
        string input = File.ReadAllText("input");

        var blocks = ParseInput(input);

        var blockCountSum = blocks.Sum(b => b.FreeSpace + b.Size);
        var maxVal = blocks.Max(b => b.Id); 

        var arr = new string[blockCountSum];

        var currPos = 0; 
        foreach(var b in blocks)
        {
            for (int j = 0; j < b.Size; j++)
            {
                arr[currPos] = b.Id.ToString();
                currPos++; 
            }
            for (int j = 0; j < b.FreeSpace; j++)
            {
                arr[currPos] = ".";
                currPos++;
            }
        }

        //Part1(arr);
        Part2(arr, maxVal); 
    }

    public static void Part1(string[] arr)
    {
        var currPos = 0;
        for (var i = arr.Length - 1; i >= 0; i--)
        {

            if (arr[i] == ".")
            {
                continue;
            }
            string currVal = arr[currPos];
            while (currVal != "." && currPos < i)
            {
                currPos++;
                currVal = arr[currPos];
            }
            arr[currPos] = arr[i];
            arr[i] = currVal;
            if (i == currPos)
            {
                break;
            }
        }
        Console.WriteLine(Checksum(arr));
        Console.ReadLine();
    }

    public static void Part2(string[] arr, int maxVal)
    {
        List<string> alreadyMoved = new List<string>();
       
        //VisualizeArr(arr);
        int current = maxVal;
        while (true)
        {
            string currentS = current.ToString(); 
            if (alreadyMoved.Contains(currentS))
                continue;

            int fileStart = -1; 
            for(int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == currentS)
                {
                    fileStart = i;
                    break; 
                }
            }
            if (fileStart == -1)
            {
                break; 
            }
            

            var fileSize = GetFileLength(arr, fileStart);

            var freeStart = GetFirstFreeSpace(arr, fileSize);
            if (freeStart == -1 || freeStart > fileStart)
            {
                current--;
                continue; 
            }

            for(int i = 0; i < fileSize; i++)
            {
                alreadyMoved.Add(arr[fileStart]); 
                var swapVal = arr[i + freeStart];
                arr[i + freeStart] = arr[fileStart + i];
                arr[fileStart + i] = swapVal;
            }
            //VisualizeArr(arr, new List<int> { fileStart });
            current--; 
        }

        Console.WriteLine(Checksum(arr));
        Console.ReadLine();
    }

    public static long Checksum(string[] arr)
    {
        long checkSum = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            var val = arr[i];
            if (val == ".")
                continue;
            checkSum += i * int.Parse(val.ToString());
        }
        return checkSum; 
    }


    public static int GetFileLength(string[] arr, int pos)
    {
        var fileId = arr[pos];

        int length = 0; 
        while (pos + length < arr.Length && arr[pos + length] == fileId)
        {
            length++;
        }
        return length; 
    }

    public static int GetFirstFreeSpace(string[] arr, int size)
    {
        var currPos = 0;
        bool found = false; 

        while (!found && currPos < arr.Length)
        {
            while (arr[currPos] != ".")
            {
                currPos++;
            }

            int startOfBlock = currPos;
            bool allFree = true;
            for (int i = 0; i < size; i++)
            {
                if (currPos + i > arr.Length-1)
                {
                    allFree = false; 
                    break;
                }
                if (arr[currPos + i] != ".")
                {
                    allFree = false;
                    break;
                }
            }
            if (allFree)
            {
                return startOfBlock; 
            }
            else
            {
                currPos++;
            }
        }
        return -1; 
    }

    public static List<Block> ParseInput(string input, int id = 0)
    {
        var ret = new List<Block>(); 
        while (input.Length > 0)
        {
            int size = int.Parse(input.Substring(0, 1));
            input = input.Substring(1);
            int free = 0;
            if (input.Length > 0)
            {
                free = int.Parse(input.Substring(0, 1));
                input = input.Substring(1);
            }
            ret.Add(new Block { Id = id, FreeSpace = free, Size = size });
            id++;
        }
        
        return ret; 
    }

    public static void VisualizeArr(string[] arr, List<int> highlights = null)
    {
        var original = Console.BackgroundColor;
        for(int i = 0; i < arr.Length; i++)
        {
            if (highlights != null && highlights.Contains(i))
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            Console.Write(arr[i]);
            Console.BackgroundColor = original;
        }
        Console.WriteLine();
    }
}

public class Block
{
    public int Id { get;set; }
    public int Size { get;set;}
    public int FreeSpace { get; set; }
}
