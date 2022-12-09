using System.Diagnostics;

namespace AdventOfCode.Shared;

public abstract class PuzzleBase<TResult> : PuzzleBase
{
    public override (string Solution1, string Solution2) Solve(FileInfo input)
    {
        var data = File.ReadAllLines(input.FullName);
        var solution1 = Solution1(data);
        var solution2 = Solution2(data);
        return (Convert.ToString(solution1) ?? string.Empty, Convert.ToString(solution2) ?? string.Empty);
        //     
        //
        // Console.WriteLine($"{GetType()}");
        // Console.WriteLine(new String('=', GetType().ToString().Length));
        // Console.WriteLine("Solution 1");
        // Console.WriteLine($"\tSample: {Timed(() => Solution1(sample))}");
        // Console.WriteLine($"\tInput : {Timed(() => Solution1(input))}");
        // Console.WriteLine("Solution 2");
        // Console.WriteLine($"\tSample: {Timed(() => Solution2(sample))}");
        // Console.WriteLine($"\tInput : {Timed(() => Solution2(input))}");
    }

    public virtual TResult? Solution1(IEnumerable<string> input) => default;

    public virtual TResult? Solution2(IEnumerable<string> input) => default;
    
    private static string Timed(Func<TResult?> operation)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var result = operation();
        stopwatch.Stop();
        return $"{result} ({stopwatch.Elapsed})";
    }
}