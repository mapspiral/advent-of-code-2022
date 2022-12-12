[Puzzle(2022, 3)]
public sealed class Puzzle03 : PuzzleBase<int>
{
    public override int Solution1(IEnumerable<string> input)
    {
        return input.Select(l => GetItemPriority(GetCommonItem(l))).Sum();

        char GetCommonItem(string tempLine)
        {
            var compartment1 = tempLine.Take(tempLine.Length / 2).ToArray();
            var compartment2 = tempLine.Skip(tempLine.Length / 2).ToArray();
            var intersection = compartment1.Intersect(compartment2);
            return intersection.First();
        }
    }

    public override int Solution2(IEnumerable<string> input)
    {
        return input.Chunk(3).Select(l => GetItemPriority(GetCommonItem(l))).Sum();

        char GetCommonItem(ICollection<string> groups)
        {
            SortedSet<char>? remainingItems = null;
            foreach (var itemsInBackpack in groups.Select(x => new SortedSet<char>(x)))
            {
                remainingItems = remainingItems == null
                    ? itemsInBackpack
                    : new SortedSet<char>(remainingItems.Intersect(itemsInBackpack));
            }

            return remainingItems?.First() ?? '?';
        }
    }

    private int GetItemPriority(char item)
    {
        return item switch
        {
            >= 'a' and <= 'z' => 1 + item - 'a',
            >= 'A' and <= 'Z' => 27 + item - 'A',
            _ => -1,
        };
    }
}