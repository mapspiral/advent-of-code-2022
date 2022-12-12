[Puzzle(2022, 12)]
public sealed class Puzzle12 : PuzzleBase<long>
{
    public override long Solution1(IEnumerable<string> input)
    {
        var area = new Area(input);
        return area.Map[area.Start].Visit(area, null, 0);
    }

    public override long Solution2(IEnumerable<string> input)
    {
        var area = new Area(input);
        return area.Map.Where(x => x.Value.Height == 'a').Min(x => x.Value.Visit(area, null, 0));
    }
}