[Puzzle(2022, 10)]
public sealed class Puzzle10 : PuzzleBase<string>
{
    public override string Solution1(IEnumerable<string> input)
    {
        var signalStrength = 0L;
        var increment = 20;
        var program = new Processor((c, r) =>
        {
            if (c % increment == 0)
            {
                increment += 40;
                signalStrength += r * c;
            }
        });
        program.Run(input);
        return signalStrength.ToString();
    }

    public override string Solution2(IEnumerable<string> input)
    {
        var output = string.Empty;
        var program = new Processor((c, r) =>
        {
            var pixelIndex = (c - 1) % 40;
            output += pixelIndex >= r - 1 && pixelIndex <= r + 1
                ? '#'
                : '.';
            if (c % 40 == 0)
            {
                output += Environment.NewLine;
            }
        });
        program.Run(input);
        return output.Trim();
    }
}