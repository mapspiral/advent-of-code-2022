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
            var skip = stacks[movement.From].Count - movement.Quantity;

            stacks[movement.To].InsertRange(stacks[movement.To].Count, stacks[movement.From].Skip(skip).Reverse());
            stacks[movement.From].RemoveRange(skip, movement.Quantity);
        }
        return new string(stacks.Select(x => x.Last()).ToArray());
    }

    protected override string Solution2(IEnumerable<string> input)
    {
        // initial state
        var lines = input.ToArray();
        var (movementStartIndex, stacks) = ParseStacks(lines);
        
        // movement
        foreach (var movement in lines.Skip(movementStartIndex).Select(ParseMovement))
        {
            var skip = stacks[movement.From].Count - movement.Quantity;

            stacks[movement.To].InsertRange(stacks[movement.To].Count, stacks[movement.From].Skip(skip));
            stacks[movement.From].RemoveRange(skip, movement.Quantity);
        }
        return new string(stacks.Select(x => x.Last()).ToArray());
    }
    
    private static (int MovementStartIndex, List<char>[] ExtractedStacks) ParseStacks(string[] lines)
    {
        var stackConfiguration = lines.TakeWhile(x => !string.IsNullOrEmpty(x)).Reverse().ToArray();
        var stackCount = stackConfiguration
            .First()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => Convert.ToInt32(x))
            .Count();

        var stacks = Enumerable.Range(0, stackCount).Select(x => new List<char>()).ToArray();
        foreach (var layer in stackConfiguration.Skip(1))
        {
            for (int row = 0; row < stackCount; row++)
            {
                if (layer[1 + (4 * row)] is var crate && crate != ' ')
                {
                    stacks[row].Add(crate);
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