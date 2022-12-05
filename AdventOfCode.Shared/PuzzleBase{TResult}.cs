using System.Diagnostics;

namespace AdventOfCode.Shared;

public abstract class PuzzleBase<TResult>
{
    public void Solve()
    {
        Console.WriteLine($"{GetType()}");
        Console.WriteLine(new String('=', GetType().ToString().Length));
        Console.WriteLine("Solution 1");
        Console.WriteLine($"\tSample: {Timed(() => Solution1(File.ReadAllLines("sample.txt")))}");
        Console.WriteLine($"\tInput : {Timed(() => Solution1(File.ReadAllLines("input.txt")))}");
        Console.WriteLine("Solution 2");
        Console.WriteLine($"\tSample: {Timed(() => Solution2(File.ReadAllLines("sample.txt")))}");
        Console.WriteLine($"\tInput : {Timed(() => Solution2(File.ReadAllLines("input.txt")))}");
    }

    protected virtual TResult? Solution1(IEnumerable<string> input) => default;

    protected virtual TResult? Solution2(IEnumerable<string> input) => default;
    
    private static string Timed(Func<TResult?> operation)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var result = operation();
        stopwatch.Stop();
        return $"{result} ({stopwatch.Elapsed})";
    }
}