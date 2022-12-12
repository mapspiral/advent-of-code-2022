using System.ComponentModel.Design;

[Puzzle(2022, 1)]
public sealed class Puzzle01 : PuzzleBase<int>
{
    public override int Solution1(IEnumerable<string> input)
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

    public override int Solution2(IEnumerable<string> input)
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
        elves.Add(currentElf);

        return elves.OrderByDescending(x => x.Calories).Take(3).Sum(x => x.Calories);
    }
}