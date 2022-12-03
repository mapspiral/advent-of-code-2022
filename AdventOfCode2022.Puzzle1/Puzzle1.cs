using AdventOfCode.Shared;

internal sealed class Puzzle1 : PuzzleBase<int>
{
    protected override int Solution1(IEnumerable<string> input)
    {
        var elves = new List<Elf>();
        elves.Add(new Elf());
        foreach (var calories in input.ParseAs<int?>())
        {
            if (!calories.HasValue)
            {
                elves.Add(new Elf());
                continue;
            }
            elves.Last().Add(calories.Value);
        }

        return elves.Max(x => x.Calories);
    }

    protected override int Solution2(IEnumerable<string> input)
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
}