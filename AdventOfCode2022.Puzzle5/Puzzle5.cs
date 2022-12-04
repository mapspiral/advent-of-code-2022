using AdventOfCode.Shared;

internal sealed class Puzzle5 : PuzzleBase<string>
{    
    protected override string Solution1(IEnumerable<string> input)
    {
        // initial state
        var lines = input.ToArray();
        var (movementStartIndex, stacks) = ParseStacks(lines);

        // movement
        foreach (var movement in lines.Skip(movementStartIndex).Select(ParseMovement))
        {
            for (var currentCreate = 0; currentCreate < movement.Quantity; currentCreate++)
            {
                var crate = stacks[movement.From].Pop();
                stacks[movement.To].Push(crate);
            }
        }
        return new string(stacks.Select(x => x.Peek()).ToArray());
    }

    protected override string Solution2(IEnumerable<string> input)
    {
        // initial state
        var lines = input.ToArray();
        var (movementStartIndex, stacks) = ParseStacks(lines);
        
        // movement
        foreach (var movement in lines.Skip(movementStartIndex).Select(ParseMovement))
        {
            var crates = Enumerable
                .Range(0, movement.Quantity)
                .Select(_ => stacks[movement.From].Pop())
                .Reverse()
                .ToArray();
            foreach (var crate in crates)
            {
                stacks[movement.To].Push(crate);
            }
        }
        return new string(stacks.Select(x => x.Peek()).ToArray());
    }
    
    private static (int MovementStartIndex, Stack<char>[] ExtractedStacks) ParseStacks(string[] lines)
    {
        var stackConfiguration = lines.TakeWhile(x => !string.IsNullOrEmpty(x)).Reverse().ToArray();
        var stackCount = stackConfiguration
            .First()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => Convert.ToInt32(x))
            .Count();

        var stacks = Enumerable.Range(0, stackCount).Select(x => new Stack<char>()).ToArray();
        foreach (var layer in stackConfiguration.Skip(1))
        {
            for (int row = 0; row < stackCount; row++)
            {
                if (layer[1 + (4 * row)] is var crate && crate != ' ')
                {
                    stacks[row].Push(crate);
                }
            }
        }
        return (stackConfiguration.Length + 1, stacks);
    }

    private static Movement ParseMovement(string line)
    {
        var parts = line.Split(' ');
        return new Movement(Convert.ToInt32(parts[1]), Convert.ToInt32(parts[3]) - 1, Convert.ToInt32(parts[5]) - 1);
    }

    private record Movement(int Quantity, int From, int To);
}