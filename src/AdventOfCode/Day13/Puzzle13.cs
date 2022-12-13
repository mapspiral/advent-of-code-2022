using System.Collections;

[Puzzle(2022, 13)]
public sealed class Puzzle13 : PuzzleBase<long>
{
    public override long Solution1(IEnumerable<string> input)
    {
        var result = 0;
        var pairs = input.Chunk(3).ToArray();
        for (var i = 1; i <= pairs.Length; i++)
        {
            var left = new ValueList(pairs[i - 1][0]);
            var right = new ValueList(pairs[i - 1][1]);
            var isEqual = left.HasCorrectOrder(right);
            if (isEqual ?? false)
            {
                result += i;
            }
        }

        return result;
    }

    public override long Solution2(IEnumerable<string> input)
    {
        var pairs = input
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => new ValueList(x))
            .OrderBy(x => x, new Sorter())
            .ToList();
        var m1 = pairs.FindIndex(x => x.Text == "[[2]]") + 1;
        var m2 = pairs.FindIndex(x => x.Text == "[[6]]") + 1;
        return m1 * m2;
    }
}