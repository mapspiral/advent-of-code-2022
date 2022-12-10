internal class Processor
{
    private readonly Action<long, long> _cycleProcessor;
    private long _registerX;
    private long _cycle;
    public Processor( Action<long, long> cycleProcessor)
    {
        _cycleProcessor = cycleProcessor;
    }

    public void Run(IEnumerable<string> input)
    {
        _cycle = 0L;
        _registerX = 1L;
        foreach (var instruction in input.Select(CreateInstruction))
        {
            _cycle++;
            _cycleProcessor(_cycle, _registerX);
            if (instruction.Instruction == "noop")
            {
                continue;
            }

            _cycle++;
            _cycleProcessor(_cycle, _registerX);
            _registerX += instruction.Increment!.Value;
        }
    }

    Line CreateInstruction(string line)
        => line.Split(' ', StringSplitOptions.RemoveEmptyEntries) is var parts && parts.Length == 1
            ? new Line(parts[0])
            : new Line(parts[0], int.Parse(parts[1]));
        
    private record Line(string Instruction, int? Increment = null);
}