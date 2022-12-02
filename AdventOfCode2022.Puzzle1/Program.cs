using AdventOfCode.Shared;

int Solution1(string[] input)
{
    var elves = new List<Elf>();
    var currentElf = new Elf();
    foreach (var calories in input.ParseAs<int?>())
    {
        if (calories.HasValue)
        {
            currentElf.Add(calories.Value);            
        }
        else
        {
            elves.Add(currentElf);
            currentElf = new Elf();
        }
    }
    return elves.Max(x => x.Calories);
}

int Solution2(string[] input)
{
    var elves = new List<Elf>();
    var currentElf = new Elf();
    foreach (var calories in input.ParseAs<int?>())
    {
        if (calories.HasValue)
        {
            currentElf.Add(calories.Value);            
        }
        else
        {
            elves.Add(currentElf);
            currentElf = new Elf();
        }
    }
    return elves.OrderByDescending(x => x.Calories).Take(3).Sum(x => x.Calories);
}

var sampleInput = File.ReadAllLines("sample.txt");
Console.WriteLine("Solution sample:");
Console.WriteLine(Solution1(sampleInput));
Console.WriteLine(Solution2(sampleInput));

var input = File.ReadAllLines("input.txt");
Console.WriteLine("Solution:");
Console.WriteLine(Solution1(input));
Console.WriteLine(Solution2(input));

Console.ReadKey();

internal sealed class Elf
{
    public int Calories { get; private set; }

    public void Add(int calories)
    {
        Calories += calories;
    }
}
