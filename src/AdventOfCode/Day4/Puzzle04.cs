[Puzzle(2022, 4)]
public sealed class Puzzle04 : PuzzleBase<int>
{
    public override int Solution1(IEnumerable<string> input)
    {
        return input.Select(GetRanges).Where(Contains).Count();

        bool Contains(int[][] ranges)
        {
            var first = ranges[0];
            var second = ranges[1];
            return (first[0] >= second[0]) && (first[1] <= second[1])
                   || (second[0] >= first[0]) && (second[1] <= first[1]);
        }
    }
    
    public override int Solution2(IEnumerable<string> input)
    {
        return input.Select(GetRanges).Where(Overlaps).Count();

        bool Overlaps(int[][] ranges)
        {
            var first = ranges[0];
            var second = ranges[1];
            return first[0] >= second[0] && first[0] <= second[1] 
               || first[1] >= second[0] && first[1] <= second[1]
               || second[0] >= first[0] && second[0] <= first[1] 
               || second[1] >= first[0] && second[1] <= first[1];
        }
    }
    
    private int[][] GetRanges(string input) => input
        .Split(',')
        .ToArray()
            .Select(x => x
            .Split('-')
        .Select(a => Convert.ToInt32(a))
            .ToArray())
        .ToArray();
        
}